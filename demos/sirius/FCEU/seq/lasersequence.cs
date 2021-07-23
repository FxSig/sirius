using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SpiralLab.Sirius.FCEU
{
    
    public class LaserSequence  
        : SpiralLab.ISequenceLaser
        , IDisposable
    {
        
        public IServiceLaser Service { get; set; }
        public IServiceHandler ServiceHandler { get; set; }

        public string Name { get; set; }
        public object SyncRoot { get; private set; }
        public float Fov { get; private set; } = 120;
        public IRtc Rtc { get; set; }
        public ILaser Laser { get; set; }
        public IMarker Marker { get; set; }

        public ConcurrentDictionary<ErrEnum, byte> Errors { get; private set; }
        public ConcurrentDictionary<WarnEnum, byte> Warns { get; private set; }


        public SiriusEditorForm Editor { get; set; }
        public SiriusViewerForm Viewer { get; set; }


        public bool IsReady { get; private set; }
        public bool IsBusy { get; private set; }
        public bool IsError { get; private set; }
        public bool IsTerminated { get; set; }

        public VisionTcpClient VisionComm { get; private set; }

        public object Tag { get; set; }

        /// <summary>
        /// 스캐너 보정 진행중
        /// </summary>
        internal bool isFieldCorrecting;
        SpiralLab.Sirius.FCEU.FormMain formMain;
        Thread thread;
        bool disposed = false;

        public LaserSequence()
        {
            Name = "FCEU Laser Seq";
            SyncRoot = new object();
            Errors = new ConcurrentDictionary<ErrEnum, byte>();
            Warns = new ConcurrentDictionary<WarnEnum, byte>();

            IsTerminated = false;
            this.formMain = Program.MainForm as SpiralLab.Sirius.FCEU.FormMain;
        }
        ~LaserSequence()
        {
            if (this.disposed)
                return;
            this.Dispose(false);
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
                return;
            if (disposing)
            {
                IsTerminated = true;
                this.VisionComm?.Dispose();
                this.VisionComm = null;
                this.thread?.Join();
            }
            this.disposed = true;
        }

        public bool Initialize()
        {
            bool success = true;
            #region RTC 초기화
            var rtcTypeName = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"RTC", "TYPE");
            Type rtcType = Type.GetType(rtcTypeName.Trim());
            if (null == rtcType)
            {
                var mb = new Default.MessageBoxOk();
                mb.ShowDialog("Critical", $"Can't create rtc instance : {rtcType.ToString()} at {FormMain.ConfigFileName}");
                success &= false;
            }
            this.Rtc = Activator.CreateInstance(rtcType) as IRtc;
            var rtcName = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"RTC", "NAME");
            Rtc.Name = rtcName;
            Rtc.Index = 0;
            var kFactor = NativeMethods.ReadIni<float>(FormMain.ConfigFileName, $"RTC", "KFACTOR");
            var ct5FileName = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"RTC", "CORRECTION");
            var laserModeTypeName = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"RTC", "LASERMODE");
            LaserMode laserMode = (LaserMode)Enum.Parse(typeof(LaserMode), laserModeTypeName.Trim());
            var fullCorrectionFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", ct5FileName);
            this.Fov = NativeMethods.ReadIni<float>(FormMain.ConfigFileName, $"RTC", "FOV");
            success &= Rtc.Initialize(kFactor, laserMode, fullCorrectionFilePath);
            Rtc.CtlFrequency(50 * 1000, 2); // laser frequency : 50KHz, pulse width : 2usec
            Rtc.CtlSpeed(100, 100); // default jump and mark speed : 100mm/s
            Rtc.CtlDelay(10, 100, 200, 200, 0); // scanner and laser delays
            Editor.Rtc = Rtc;
            #endregion

            #region 레이저 소스 초기화
            var laserTypeName = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"LASER", "TYPE");
            Type laserType = Type.GetType(laserTypeName.Trim());
            if (null == laserType)
            {
                var mb = new Default.MessageBoxOk();
                mb.ShowDialog("Critical", $"Can't create laser instance : {laserType.ToString()} at {FormMain.ConfigFileName}");
                success &= false;
            }
            this.Laser = Activator.CreateInstance(laserType) as ILaser;
            //ILaser laser = new Sirius.LaserVirtual(0, "virtual", 20.0f);
            var laserName = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"LASER", "NAME");
            Laser.Rtc = Rtc;
            Laser.Index = 0;
            Laser.Name = laserName;
            var maxPower = NativeMethods.ReadIni<float>(FormMain.ConfigFileName, $"LASER", "MAXPOWER");
            Laser.MaxPowerWatt = maxPower;
            success &= Laser.Initialize();
            success &= Laser.CtlPower(10);
            Editor.Laser = Laser;
            #endregion

            #region 마커 지정 (전용 마커 사용)            
            this.Marker = new MarkerFCEU(0);
            this.Marker.Name = "FCEU Marker";
            this.Marker.OnFinished += Marker_OnFinished;
            var scannerRotateAngle = NativeMethods.ReadIni<float>(FormMain.ConfigFileName, $"RTC", "ROTATE");
            this.Marker.ScannerRotateAngle = scannerRotateAngle;
            this.Marker.Tag = SpiralLab.Sirius.FCEU.LaserSequence.Process.Default;
            Editor.Marker = this.Marker;
            #endregion

            #region 비전 통신 초기화
            var visionIp = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"VISION", "IP");
            var visionPort = NativeMethods.ReadIni<int>(FormMain.ConfigFileName, $"VISION", "PORT");
            this.VisionComm = new VisionTcpClient(visionIp, visionPort);
            this.VisionComm.Start(); 
            #endregion

            this.thread = new Thread(this.Loop);
            this.thread.Name = $"Sequence Loop";
            this.thread.Start();
            if (!success)
                Error(ErrEnum.Initialize);

            this.Service = new LaserService(this);
            return success;
        }

        readonly int ErrWarnLimit = 10;
        public void Error(ErrEnum err, bool clear = false)
        {
            lock(this.SyncRoot)
            {
                if (!clear)
                {
                    if (!Errors.ContainsKey(err))
                    {
                        if (Errors.Count <= ErrWarnLimit)
                        {
                            Errors.TryAdd(err, 0);
                            VisionComm?.Send(MessageProtocol.LASER_STATUS_ERR_NG);
                        }
                    }
                }
                else
                {
                    Errors.TryRemove(err, out var val);
                }
            }
        }
        public void Warn(WarnEnum warn, bool clear = false)
        {
            lock (this.SyncRoot)
            {
                if (!clear)
                {
                    if (!Warns.ContainsKey(warn))
                    {
                        if (Warns.Count <= ErrWarnLimit)
                            Warns.TryAdd(warn, 0);
                    }
                }
                else
                {
                    Warns.TryRemove(warn, out var val);
                }
            }
        }

        public enum Process
        {
            /// <summary>
            /// 기본값
            /// </summary>
            Default = 0,
            /// <summary>
            /// 우 (해칭 절반)
            /// </summary>
            Defect_Right,
            /// <summary>
            /// 좌 (해칭 절반)
            /// </summary>
            Defect_Left,
            /// <summary>
            /// 중심 마크
            /// </summary>
            SystemTeach,
            /// <summary>
            /// 스캐너 보정 마크 (SystemTeach 사용하여 offset 생성)
            /// </summary>
            FieldCorrection,
            /// <summary>
            /// 우 (도면 절반)
            /// </summary>
            Ref_Right,
            /// <summary>
            /// 좌 (도면 절반)
            /// </summary>
            Ref_Left,
        }
        public bool Start(SpiralLab.Sirius.FCEU.LaserSequence.Process proc)
        {
            if (this.IsBusy)
            {
                formMain.Seq.Error(ErrEnum.Busy);
                Logger.Log(Logger.Type.Error, $"try to start mark but busy : {proc.ToString()}");
                return false;
            }
            bool success = true;
            switch (proc)
            {
                case Process.Defect_Right://right
                    {
                        var doc = Editor.Document;
                        var markerArg = new MarkerArgDefault();
                        markerArg.Rtc = this.Rtc;
                        markerArg.Laser = this.Laser;
                        markerArg.Document = doc;
                        markerArg.RtcListType = ListType.Auto;
                        var defLayerRight = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"LAYER", "DEFECT_RIGHT");
                        var layer = doc.Layers.NameOf(defLayerRight);
                        var br = layer.BoundRect;
                        markerArg.Offsets.Add(new Offset(-br.Center.X, -br.Center.Y)); 
                        var scannerRotateAngle = NativeMethods.ReadIni<float>(FormMain.ConfigFileName, $"RTC", "ROTATE");
                        this.Marker.ScannerRotateAngle = scannerRotateAngle;
                        this.Marker.Tag = proc;
                        if (false == Marker.Ready(markerArg))
                        {
                            Logger.Log(Logger.Type.Error, $"try to mark defect (right side) but fail to ready");
                            return false;
                        }
                        this.Warn(WarnEnum.StartingToMark);
                        this.Warn(WarnEnum.DefectMarkRight);
                        Logger.Log(Logger.Type.Warn, $"trying to start mark defect (right side)");
                        success &= Marker.Start();
                    }
                    break;
                case Process.Defect_Left://left
                    {
                        var doc = Editor.Document;
                        var markerArg = new MarkerArgDefault();
                        markerArg.Rtc = this.Rtc;
                        markerArg.Laser = this.Laser;
                        markerArg.Document = doc;
                        markerArg.RtcListType = ListType.Auto;
                        var defLayerLeft = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"LAYER", "DEFECT_LEFT");
                        var layer = doc.Layers.NameOf(defLayerLeft);
                        var br = layer.BoundRect;
                        markerArg.Offsets.Add(new Offset(-br.Center.X, -br.Center.Y));
                        var scannerRotateAngle = NativeMethods.ReadIni<float>(FormMain.ConfigFileName, $"RTC", "ROTATE");
                        this.Marker.ScannerRotateAngle = scannerRotateAngle;
                        this.Marker.Tag = proc;
                        if (false == Marker.Ready(markerArg))
                        {
                            Logger.Log(Logger.Type.Error, $"try to mark defect (left side) but fail to ready");
                            return false;
                        }
                        this.Warn(WarnEnum.StartingToMark);
                        this.Warn(WarnEnum.DefectMarkLeft);
                        Logger.Log(Logger.Type.Warn, $"trying to start mark defect (left side)");
                        success &= Marker.Start();
                    }
                    break;
                case Process.SystemTeach:
                    {
                        var fileName = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"FILE", "SYSTEMTEACH");
                        var siriusFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "setup", fileName);
                        if (!File.Exists(siriusFileName))
                        {
                            Error(ErrEnum.Recipe);
                            Logger.Log(Logger.Type.Error, $"try to mark system teach but file doesn't exist : {siriusFileName}");
                            return false;
                        }
                        var doc = DocumentSerializer.OpenSirius(siriusFileName);
                        if (null == doc)
                        {
                            Error(ErrEnum.Recipe);
                            Logger.Log(Logger.Type.Error, $"try to mark system teach but fail to open : {siriusFileName}");
                            return false;
                        }

                        var markerArg = new MarkerArgDefault();
                        markerArg.Rtc = this.Rtc;
                        markerArg.Laser = this.Laser;
                        markerArg.Document = doc;
                        markerArg.RtcListType = ListType.Auto;
                        var scannerRotateAngle = NativeMethods.ReadIni<float>(FormMain.ConfigFileName, $"RTC", "ROTATE");
                        this.Marker.ScannerRotateAngle = scannerRotateAngle;
                        this.Marker.Tag = proc;
                        if (false == Marker.Ready(markerArg))
                        {
                            Logger.Log(Logger.Type.Error, $"try to mark system teach but fail to ready");
                            return false;
                        }
                        this.Warn(WarnEnum.StartingToMark);
                        this.Warn(WarnEnum.SystemTeachToMark);
                        Logger.Log(Logger.Type.Warn, $"trying to start mark system teach at center");
                        success &= Marker.Start();
                    }
                    break;
                case Process.FieldCorrection:
                    {
                        var fileName = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"FILE", "SYSTEMTEACH");
                        var siriusFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "setup", fileName);
                        if (!File.Exists(siriusFileName))
                        {
                            Error(ErrEnum.Recipe);
                            Logger.Log(Logger.Type.Error, $"try to mark  field correction but file doesnt exist : {siriusFileName}");
                            return false;
                        }
                        var doc = DocumentSerializer.OpenSirius(siriusFileName);
                        if (null == doc)
                        {
                            formMain.Seq.Error(ErrEnum.Recipe);
                            Logger.Log(Logger.Type.Error, $"try to mark field correction but fail to open : {siriusFileName}");
                            return false;
                        }
                        var markerArg = new MarkerArgDefault();
                        markerArg.Rtc = this.Rtc;
                        markerArg.Laser = this.Laser;
                        markerArg.Document = doc;
                        markerArg.RtcListType = ListType.Auto;
                        this.Marker.ScannerRotateAngle = 0;
                        this.Marker.Tag = proc;

                        var svc = Service as LaserService;
                        float left = -svc.FieldCorrectionInterval * (float)(int)(svc.FieldCorrectionCols / 2);
                        float top = svc.FieldCorrectionInterval * (float)(int)(svc.FieldCorrectionRows / 2);
                        for (int row = 0; row < svc.FieldCorrectionRows; row++)
                        {
                            for (int col = 0; col < svc.FieldCorrectionCols; col++)
                            {
                                markerArg.Offsets.Add(new Offset(
                                        left + col * svc.FieldCorrectionInterval,
                                        top - row * svc.FieldCorrectionInterval));
                            }
                        }
                        if (false == Marker.Ready(markerArg))
                        {
                            Logger.Log(Logger.Type.Error, $"try to mark field correction but fail to ready");
                            return false;
                        }
                        this.Warn(WarnEnum.StartingToMark);
                        this.Warn(WarnEnum.ScannerFieldCorrectionToMark);
                        Logger.Log(Logger.Type.Warn, $"trying to start mark field correction ");
                        success &= Marker.Start();
                    }
                    break;
                case Process.Ref_Right:
                    {
                        var doc = Editor.Document;
                        var markerArg = new MarkerArgDefault();
                        markerArg.Rtc = this.Rtc;
                        markerArg.Laser = this.Laser;
                        markerArg.Document = doc;
                        markerArg.RtcListType = ListType.Auto;
                        var refLayerRight = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"LAYER", "REF_RIGHT");
                        var layer = doc.Layers.NameOf(refLayerRight);
                        if (null == layer)
                        {
                            Logger.Log(Logger.Type.Error, $"target layer is not exist : {refLayerRight}");
                            return false;
                        }
                        var br = layer.BoundRect;
                        markerArg.Offsets.Add(new Offset(-br.Center.X, -br.Center.Y));
                        var scannerRotateAngle = NativeMethods.ReadIni<float>(FormMain.ConfigFileName, $"RTC", "ROTATE");
                        this.Marker.ScannerRotateAngle = scannerRotateAngle;
                        this.Marker.Tag = proc;
                        if (false == Marker.Ready(markerArg))
                        {
                            Logger.Log(Logger.Type.Error, $"try to mark reference (right side) but fail to ready");
                            return false;
                        }
                        this.Warn(WarnEnum.StartingToMark);
                        this.Warn(WarnEnum.ReferenceMarkRight);
                        Logger.Log(Logger.Type.Warn, $"trying to start mark reference (right side)");
                        success &= Marker.Start();
                    }
                    break;
                case Process.Ref_Left:
                    {
                        var doc = Editor.Document;
                        var markerArg = new MarkerArgDefault();
                        markerArg.Rtc = this.Rtc;
                        markerArg.Laser = this.Laser;
                        markerArg.Document = doc;
                        markerArg.RtcListType = ListType.Auto;
                        var refLayerLeft = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"LAYER", "REF_LEFT");
                        var layer = doc.Layers.NameOf(refLayerLeft);
                        if (null == layer)
                        {
                            Logger.Log(Logger.Type.Error, $"target layer is not exist : {refLayerLeft}");
                            return false;
                        }
                        var br = layer.BoundRect;
                        markerArg.Offsets.Add(new Offset(-br.Center.X, -br.Center.Y));
                        var scannerRotateAngle = NativeMethods.ReadIni<float>(FormMain.ConfigFileName, $"RTC", "ROTATE");
                        this.Marker.ScannerRotateAngle = scannerRotateAngle;
                        this.Marker.Tag = proc;
                        if (false == Marker.Ready(markerArg))
                        {
                            Logger.Log(Logger.Type.Error, $"try to mark reference (left side) but fail to ready");
                            return false;
                        }
                        this.Warn(WarnEnum.StartingToMark);
                        this.Warn(WarnEnum.ReferenceMarkLeft);
                        Logger.Log(Logger.Type.Warn, $"trying to start mark reference (left side)");
                        success &= Marker.Start();
                    }
                    break;
            }
            return success;
        }
        public void Reset()
        {
            Rtc.CtlReset();
            Laser.CtlReset();
            Marker.Reset();
            lock (this.SyncRoot)
            {
                this.Warns.Clear();
                this.Errors.Clear();
            }
        }
        public void Stop()
        {
            //Rtc.CtlAbort();
            Marker.Stop();
        }
        public void Loop()
        {
            do
            {
                bool ready = true;
                ready &= Marker.IsReady;
                ready &= this.formMain.FormCurrent == this.formMain.FormAuto;
                ready &= !isFieldCorrecting; // 스캐너 보정중에는 ready off
                this.IsReady = ready;

                bool busy = false;
                busy |= Rtc.CtlGetStatus(RtcStatus.Busy);
                busy |= Marker.IsBusy;
                if (this.IsBusy != busy)
                {
                    if (busy)
                    {
                        //busy on
                        //VisionComm?.Send(MessageProtocol.LASER_STATUS_BUSY_OK);
                    }
                    else
                    {
                        //busy off
                    }
                }
                this.IsBusy = busy;

                bool error = false;
                error |= Marker.IsError;
                lock(SyncRoot)
                    error |= Errors.Count > 0;
                if (this.IsError != error)
                {
                    if (error)
                    {
                        //error
                    }
                }
                this.IsError = error;

                if (this.formMain.FormCurrent != this.formMain.FormAuto)
                    this.Warn(WarnEnum.Auto);
                else
                    this.Warn(WarnEnum.Auto, true);

                if (!this.VisionComm.IsConnected)
                    this.formMain.Seq.Warn(WarnEnum.VisionCommunication);
                else
                    this.formMain.Seq.Warn(WarnEnum.VisionCommunication, true);

                if (IsReady)
                    this.Warn(WarnEnum.Ready);
                else
                    this.Warn(WarnEnum.Ready, true);

                if (IsBusy)
                    this.Warn(WarnEnum.Busy);
                else
                    this.Warn(WarnEnum.Busy, true);

                Thread.Sleep(10);
            } while (!IsTerminated);
        }

        private void Marker_OnFinished(IMarker sender, IMarkerArg markerArg)
        {
            this.Warn(WarnEnum.StartingToMark, true);
            var marker = sender as IMarker;
            switch ((SpiralLab.Sirius.FCEU.LaserSequence.Process)marker.Tag)
            {
                case Process.SystemTeach:
                    if (markerArg.IsSuccess) 
                        VisionComm.Send(MessageProtocol.LASER_SCANNER_SYSTEM_TEACH_FINISH);
                    else
                        VisionComm.Send(MessageProtocol.LASER_SCANNER_SYSTEM_TEACH_NG);
                    break;
                case Process.FieldCorrection:
                    if (markerArg.IsSuccess)
                        VisionComm.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_FINISH);
                    else
                        VisionComm.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_NG);
                    break;
                case Process.Ref_Right:
                    if (markerArg.IsSuccess)
                        VisionComm.Send(MessageProtocol.LASER_SCANNER_REF_01_IMAGE_FINISH);
                    else
                        VisionComm.Send(MessageProtocol.LASER_SCANNER_REF_01_IMAGE_NG);
                    break;
                case Process.Ref_Left:
                    if (markerArg.IsSuccess)
                        VisionComm.Send(MessageProtocol.LASER_SCANNER_REF_02_IMAGE_FINISH);
                    else
                        VisionComm.Send(MessageProtocol.LASER_SCANNER_REF_02_IMAGE_NG);
                    break;
                case Process.Defect_Right:
                    if (markerArg.IsSuccess)
                        VisionComm.Send(MessageProtocol.DO_HATCHING_01_FINISH);
                    else
                        VisionComm.Send(MessageProtocol.DO_HATCHING_01_START_NG);
                    break;
                case Process.Defect_Left:
                    if (markerArg.IsSuccess)
                        VisionComm.Send(MessageProtocol.DO_HATCHING_02_FINISH);
                    else
                        VisionComm.Send(MessageProtocol.DO_HATCHING_02_START_NG);
                    break;                
            }
            marker.Tag = SpiralLab.Sirius.FCEU.LaserSequence.Process.Default; //reset to user manual mode or 0
            var scannerRotateAngle = NativeMethods.ReadIni<float>(FormMain.ConfigFileName, $"RTC", "ROTATE");
            this.Marker.ScannerRotateAngle = scannerRotateAngle;
        }
    }
}