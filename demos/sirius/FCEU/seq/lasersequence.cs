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
        public IMarker MarkerSystemTeach { get; set; }
        public IMarker MarkerFieldCorrection { get; set; }
        public IMarker MarkerRef { get; set; }

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
                this.VisionComm?.Dispose();
                IsTerminated = true;
                this.thread?.Join();
            }
            this.disposed = true;
        }

        public bool Initialize()
        {
            bool success = true;
            #region RTC �ʱ�ȭ
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
            success &= Rtc.Initialize(kFactor, laserMode, ct5FileName);
            Rtc.CtlFrequency(50 * 1000, 2); // laser frequency : 50KHz, pulse width : 2usec
            Rtc.CtlSpeed(100, 100); // default jump and mark speed : 100mm/s
            Rtc.CtlDelay(10, 100, 200, 200, 0); // scanner and laser delays
            Editor.Rtc = Rtc;
            #endregion

            #region ������ �ҽ� �ʱ�ȭ
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

            var scannerRotateAngle = NativeMethods.ReadIni<float>(FormMain.ConfigFileName, $"RTC", "ROTATE");
            #region ��Ŀ ���� (���� ��Ŀ ���)            
            this.Marker = new MarkerFCEU(0);
            this.Marker.Name = "FCEU Marker";
            this.Marker.OnFinished += Marker_OnFinished;
            this.Marker.ScannerRotateAngle = scannerRotateAngle;
            Editor.Marker = this.Marker;

            this.MarkerSystemTeach = new MarkerDefault(0);
            this.MarkerSystemTeach.Name = "System Teach";
            this.MarkerSystemTeach.OnFinished += SystemTeach_OnFinished;

            this.MarkerFieldCorrection = new MarkerDefault(0);
            this.MarkerFieldCorrection.Name = "Field Correction";
            this.MarkerFieldCorrection.OnFinished += FieldCorrection_OnFinished;

            this.MarkerRef = new MarkerRef(0);
            this.MarkerRef.Name = "Reference Mark 1/2";
            this.MarkerRef.OnFinished += Ref_OnFinished;
            this.MarkerRef.ScannerRotateAngle = scannerRotateAngle;
            #endregion

            this.VisionComm = new VisionTcpClient("localhost", 9999);
            this.VisionComm.Start();

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
                            Errors.TryAdd(err, 0);
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
            Defect1,
            Defect2,
            SystemTeach,
            FieldCorrection,
            Ref1,
            Ref2,
        }
        public bool Start(Process proc)
        {
            if (this.IsBusy)
            {
                formMain.Seq.Error(ErrEnum.Busy);
                Logger.Log(Logger.Type.Error, $"try to start mark but busy !");
                return false;
            }

            bool success = true;
            var markerArg = new MarkerArgDefault();
            markerArg.Rtc = this.Rtc;
            markerArg.Laser = this.Laser;

            switch (proc)
            {
                case Process.Defect1://right
                    {
                        if (this.IsBusy)
                        {
                            formMain.Seq.Error(ErrEnum.Busy);
                            Logger.Log(Logger.Type.Error, $"try to mark but marker is busy !");
                            return false;
                        }
                        var doc = Marker.Document;
                        int index = 1;
                        var name = $"Defect{index}";

                        var layer = doc.Layers.NameOf(name);
                        if (null != doc || null == layer || layer.Count < 2)
                        {
                            Logger.Log(Logger.Type.Error, $"try to mark but data is not assigned yet");
                            return false;
                        }
                        Marker.Tag = $"{index}";
                        //�̹� �ҷ� ����Ÿ�� �о� marker cloned doc �� ����Ÿ ����Ͽ����Ƿ� ready ȣ�����

                        //offset
                        var refLayer = doc.Layers.NameOf($"Ref{index}");
                        var br = refLayer.BoundRect;
                        this.Marker.MarkerArg.Offsets.Clear();
                        //������ �̹� ���Ǿ� ����?
                        //this.Marker.MarkerArg.Offsets.Add(new Offset(-br.Center.X, -br.Center.Y));

                        this.Warn(WarnEnum.StartingToMark);
                        Logger.Log(Logger.Type.Warn, $"trying to start mark defect1 (right side)");
                        success &= Marker.Start();
                    }
                    break;
                case Process.Defect2://left
                    {
                        if (this.IsBusy)
                        {
                            formMain.Seq.Error(ErrEnum.Busy);
                            Logger.Log(Logger.Type.Error, $"try to mark but marker is busy !");
                            return false;
                        }
                        var doc = Marker.Document;
                        int index = 2;
                        var name = $"Defect{index}";

                        var layer = doc.Layers.NameOf(name);
                        if (null != doc || null == layer || layer.Count < 2)
                        {
                            Logger.Log(Logger.Type.Error, $"try to mark but data is not assigned yet");
                            return false;
                        }
                        Marker.Tag = $"{index}";
                        //�̹� �ҷ� ����Ÿ�� �о� marker cloned doc �� ����Ÿ ����Ͽ����Ƿ� ready ȣ�����

                        //offset
                        var refLayer = doc.Layers.NameOf($"Ref{index}");
                        var br = refLayer.BoundRect;
                        this.Marker.MarkerArg.Offsets.Clear();
                        //������ �̹� ���Ǿ� ����?
                        //this.Marker.MarkerArg.Offsets.Add(new Offset(-br.Center.X, -br.Center.Y));

                        this.Warn(WarnEnum.StartingToMark);
                        Logger.Log(Logger.Type.Warn, $"trying to start mark defect2 (left side)");
                        success &= Marker.Start();
                    }
                    break;
                case Process.SystemTeach:
                    {
                        if (this.IsBusy)
                        {
                            formMain.Seq.Error(ErrEnum.Busy);
                            Logger.Log(Logger.Type.Error, $"try to mark system teach but marker is busy !");
                            return false;
                        }
                        var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "setup", "systemteach.sirius");
                        if (!File.Exists(fileName))
                        {
                            Error(ErrEnum.Recipe);
                            Logger.Log(Logger.Type.Error, $"try to mark system teach but file doesnt exist : {fileName}");
                            return false;
                        }
                        var doc = DocumentSerializer.OpenSirius(fileName);
                        if (null == doc)
                        {
                            Error(ErrEnum.Recipe);
                            Logger.Log(Logger.Type.Error, $"try to mark system teach but fail to open : {fileName}");
                            return false;
                        }
                        markerArg.Document = doc;
                        if (false == MarkerSystemTeach.Ready(markerArg))
                        {
                            Logger.Log(Logger.Type.Error, $"try to mark system teach but fail to ready");
                            return false;
                        }
                        this.Warn(WarnEnum.SystemTeachToMark);
                        Logger.Log(Logger.Type.Warn, $"trying to start mark system teach at center");
                        success &= MarkerSystemTeach.Start();
                    }
                    break;
                case Process.FieldCorrection:
                    {
                        if (this.IsBusy)
                        {
                            formMain.Seq.Error(ErrEnum.Busy);
                            Logger.Log(Logger.Type.Error, $"try to mark field correction but marker is busy !");
                            return false;
                        }
                        var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "setup", "systemteach.sirius");
                        if (!File.Exists(fileName))
                        {
                            Error(ErrEnum.Recipe);
                            Logger.Log(Logger.Type.Error, $"try to mark  field correction but file doesnt exist : {fileName}");
                            return false;
                        }
                        var doc = DocumentSerializer.OpenSirius(fileName);
                        if (null == doc)
                        {
                            formMain.Seq.Error(ErrEnum.Recipe);
                            Logger.Log(Logger.Type.Error, $"try to mark field correction but fail to open : {fileName}");
                            return false;
                        }
                        markerArg.Document = doc;
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
                        if (false == MarkerFieldCorrection.Ready(markerArg))
                        {
                            Logger.Log(Logger.Type.Error, $"try to mark field correction but fail to ready");
                            return false;
                        }
                        this.Warn(WarnEnum.ScannerFieldCorrectionToMark);
                        Logger.Log(Logger.Type.Warn, $"trying to start mark field correction ");
                        success &= MarkerFieldCorrection.Start();
                    }
                    break;
                case Process.Ref1:
                    {
                        var doc = Editor.Document;
                        markerArg.Document = doc;
                        int index = 1;
                        MarkerRef.Tag = $"{index}";
                        var layer = doc.Layers.NameOf($"Ref{index}");
                        var br = layer.BoundRect;
                        markerArg.Offsets.Add(new Offset(-br.Center.X, -br.Center.Y));
                        if (false == MarkerRef.Ready(markerArg))
                        {
                            Logger.Log(Logger.Type.Error, $"try to mark ref1 (right) but fail to ready");
                            return false;
                        }
                        this.Warn(WarnEnum.Reference1Mark);
                        Logger.Log(Logger.Type.Warn, $"trying to start mark reference #1");
                        success &= MarkerRef.Start();
                    }
                    break;
                case Process.Ref2:
                    {
                        var doc = Editor.Document;
                        markerArg.Document = doc;
                        int index = 2;
                        MarkerRef.Tag = $"{index}";
                        var layer = doc.Layers.NameOf($"Ref{index}");
                        var br = layer.BoundRect;
                        markerArg.Offsets.Add(new Offset(-br.Center.X, -br.Center.Y));
                        if (false == MarkerRef.Ready(markerArg))
                        {
                            Logger.Log(Logger.Type.Error, $"try to mark ref2 (left) but fail to ready");
                            return false;
                        }
                        this.Warn(WarnEnum.Reference1Mark);
                        Logger.Log(Logger.Type.Warn, $"trying to start mark reference #2");
                        success &= MarkerRef.Start();
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
            MarkerSystemTeach.Reset();
            MarkerFieldCorrection.Reset();
            MarkerRef.Reset();
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
            MarkerSystemTeach.Stop();
            MarkerFieldCorrection.Stop();
            MarkerRef.Stop();
        }
        public void Loop()
        {
            do
            {
                bool ready = true;
                ready &= Rtc.CtlGetStatus(RtcStatus.NoError);
                ready &= !Laser.IsError;
                ready &= this.formMain.FormCurrent == this.formMain.FormAuto;
                ready &= !isFieldCorrecting;
                this.IsReady = ready;

                bool busy = false;
                busy |= Rtc.CtlGetStatus(RtcStatus.Busy);
                busy |= Laser.IsBusy;
                this.IsBusy = busy;

                bool error = false;
                error |= !Rtc.CtlGetStatus(RtcStatus.NoError);
                error |= Laser.IsError;
                lock(SyncRoot)
                    error |= Errors.Count > 0;
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

                Thread.Sleep(5);
            } while (!IsTerminated);
        }

        private void Marker_OnFinished(IMarker sender, IMarkerArg markerArg)
        {
            var marker = sender as IMarker;
            switch ((string)marker.Tag)
            {
                case "1":
                    //await
                    VisionComm.Send(MessageProtocol.LASER_READ_HATCHING_01_FINISH);
                    break;
                case "2":
                    //await
                    VisionComm.Send(MessageProtocol.LASER_READ_HATCHING_02_FINISH);
                    break;
            }
            marker.Tag = null; //reset
        }
        private void SystemTeach_OnFinished(IMarker sender, IMarkerArg markerArg)
        {
            var marker = sender as IMarker;
            //await
            VisionComm.Send(MessageProtocol.LASER_SCANNER_SYSTEM_TEACH_FINISH);
        }
        private void FieldCorrection_OnFinished(IMarker sender, IMarkerArg markerArg)
        {
            var marker = sender as IMarker;
            //await
            VisionComm.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_FINISH);
        }
        private void Ref_OnFinished(IMarker sender, IMarkerArg markerArg)
        {
            var marker = sender as IMarker;
            switch((string)marker.Tag)
            {
                case "1":
                    //await
                    VisionComm.Send(MessageProtocol.LASER_SCANNER_REF_01_IMAGE_FINISH);
                    break;
                case "2":
                    //await
                    VisionComm.Send(MessageProtocol.LASER_SCANNER_REF_02_IMAGE_FINISH);
                    break;
            }
        }
    }
}