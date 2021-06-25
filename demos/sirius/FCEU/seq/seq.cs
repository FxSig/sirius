using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SpiralLab.Sirius.FCEU
{
    
    public class LaserSeq 
        : IDisposable
    {
        public string Name { get; set; }
        public object SyncRoot { get; private set; }
        public float Fov { get; private set; } = 120;
        public IRtc Rtc { get; set; }
        public ILaser Laser { get; set; }

        public IMarker Marker { get; set; }
        public IMarker MarkerSystemTeach { get; set; }
        public IMarker MarkerFieldCorrection { get; set; }
        public IMarker MarkerRef { get; set; }

        public HashSet<ErrEnum> Errors { get; private set; }
        public HashSet<WarnEnum> Warns { get; private set; }
        public SiriusEditorForm Editor { get; set; }
        public SiriusViewerForm Viewer { get; set; }
        public int RecipeNo { get; private set; }

        public bool IsReady { get; private set; }
        public bool IsBusy { get; private set; }
        public bool IsError { get; private set; }
        public bool IsTerminated { get; set; }

        public int FieldCorrectionRows;
        public int FieldCorrectionCols;
        public float FieldCorrectionInterval;

        VisionTcpClient visionComm;

        SpiralLab.Sirius.FCEU.FormMain formMain;
        Thread thread;
        bool isFieldCorrecting;
        bool disposed = false;

        public LaserSeq()
        {
            Name = "FCEU Laser Seq";
            SyncRoot = new object();
            Errors = new HashSet<ErrEnum>();
            Warns = new HashSet<WarnEnum>();
            RecipeNo = 0;
            IsTerminated = false;
            this.formMain = Program.MainForm as SpiralLab.Sirius.FCEU.FormMain;
        }

        ~LaserSeq()
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
                this.visionComm?.Dispose();
                IsTerminated = true;
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
            success &= Rtc.Initialize(kFactor, laserMode, ct5FileName);
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
            #endregion

            this.RecipeClear();

            this.visionComm = new VisionTcpClient("192.168.0.1", 9999);
            this.visionComm.Connect(); //비동기???

            this.thread = new Thread(this.Loop);
            this.thread.Name = $"Sequence Loop";
            this.thread.Start();

            if (!success)
                Error(ErrEnum.Initialize);

            return success;
        }

        readonly int ErrWarnLimit = 10;
        public void Error(ErrEnum err, bool clear = false)
        {
            lock(this.SyncRoot)
            {
                if (!clear)
                {
                    if (!Errors.Contains(err))
                    {
                        if (Errors.Count <= ErrWarnLimit)
                            Errors.Add(err);
                    }
                }
                else
                {
                    Errors.Remove(err);
                }
            }
        }
        public void Warn(WarnEnum warn, bool clear = false)
        {
            lock (this.SyncRoot)
            {
                if (!clear)
                {
                    if (!Warns.Contains(warn))
                    {
                        if (Warns.Count <= ErrWarnLimit)
                            Warns.Add(warn);
                    }
                }
                else
                {
                    Warns.Remove(warn);
                }
            }
        }

        public void RecipeClear()
        {
            RecipeNo = 0;
            var doc = new Sirius.DocumentDefault();
            Program.MainForm.Invoke(new MethodInvoker(delegate ()
            {
                Viewer.Document = doc;
                Editor.Document = doc;
            }));
        }
        public bool RecipeChange(int no)
        {
            if (this.IsBusy)
            {
                this.Error(ErrEnum.Busy);
                Logger.Log(Logger.Type.Error, $"try to change recipe buy busy !");
                return false;
            }
            this.RecipeClear();
            bool success = true;
            string recipeFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "recipes", $"{no}", "laser.sirius");
            if (!File.Exists(recipeFileName))
            {
                this.Error(ErrEnum.Recipe);
                return false;
            }
            var doc = DocumentSerializer.OpenSirius(recipeFileName);
            if (null == doc)
            {
                this.Error(ErrEnum.Recipe);
                return false;
            }

            var markerArg = new MarkerArgDefault();
            markerArg.Document = doc;
            markerArg.Rtc = this.Rtc;
            markerArg.Laser = this.Laser;
            markerArg.ScannerRotateAngle = NativeMethods.ReadIni<float>(FormMain.ConfigFileName, $"RTC", "ROTATE");                        
            success &= Marker.Ready(markerArg);
            if (success)
            {
                Program.MainForm.Invoke(new MethodInvoker(delegate ()
                {
                    Editor.Document = doc;
                    //Viewer.Document = doc; 자동 !
                }));
                RecipeNo = no;
                this.Warn(WarnEnum.RecipeChanged);
            }
            else
            {
                this.Error(ErrEnum.Recipe);
            }
            return success;
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
            markerArg.ScannerRotateAngle = NativeMethods.ReadIni<float>(FormMain.ConfigFileName, $"RTC", "ROTATE");

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
                        //이미 불량 데이타를 읽어 marker cloned doc 에 데이타 기록하였으므로 ready 호출않함

                        //offset
                        var refLayer = doc.Layers.NameOf($"Ref{index}");
                        var br = refLayer.BoundRect;
                        this.Marker.MarkerArg.Offsets.Clear();
                        this.Marker.MarkerArg.Offsets.Add(new Offset(-br.Center.X, -br.Center.Y));

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
                        //이미 불량 데이타를 읽어 marker cloned doc 에 데이타 기록하였으므로 ready 호출않함

                        //offset
                        var refLayer = doc.Layers.NameOf($"Ref{index}");
                        var br = refLayer.BoundRect;
                        this.Marker.MarkerArg.Offsets.Clear();
                        this.Marker.MarkerArg.Offsets.Add(new Offset(-br.Center.X, -br.Center.Y));

                        this.Warn(WarnEnum.StartingToMark);
                        Logger.Log(Logger.Type.Warn, $"trying to start mark defect2 (left side)");
                        success &= Marker.Start();
                    }
                    break;
                case Process.SystemTeach:
                    {
                        var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "setup", "systemteach.sirius");
                        if (this.IsBusy)
                        {
                            formMain.Seq.Error(ErrEnum.Busy);
                            Logger.Log(Logger.Type.Error, $"try to mark system teach but marker is busy !");
                            return false;
                        }
                        var doc = DocumentSerializer.OpenSirius(fileName);
                        if (null == doc)
                        {
                            formMain.Seq.Error(ErrEnum.Recipe);
                            Logger.Log(Logger.Type.Error, $"try to mark system teach but fail to clone document");
                            return false;
                        }
                        markerArg.Document = doc;
                        markerArg.ScannerRotateAngle = 0;
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
                        var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "setup", "systemteach.sirius");
                        if (this.IsBusy)
                        {
                            formMain.Seq.Error(ErrEnum.Busy);
                            Logger.Log(Logger.Type.Error, $"try to mark field correction but marker is busy !");
                            return false;
                        }
                        var doc = DocumentSerializer.OpenSirius(fileName);
                        if (null == doc)
                        {
                            formMain.Seq.Error(ErrEnum.Recipe);
                            Logger.Log(Logger.Type.Error, $"try to mark field correction but fail to clone document");
                            return false;
                        }
                        markerArg.Document = doc;
                        markerArg.ScannerRotateAngle = 0;
                        float left = -FieldCorrectionInterval * (float)(int)(FieldCorrectionCols / 2);
                        float top = FieldCorrectionInterval * (float)(int)(FieldCorrectionRows / 2);
                        for (int row = 0; row < FieldCorrectionRows; row++)
                        {
                            for (int col = 0; col < FieldCorrectionCols; col++)
                            {
                                markerArg.Offsets.Add(new Offset(
                                        left + col * FieldCorrectionInterval,
                                        top - row * FieldCorrectionInterval));
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
                            Logger.Log(Logger.Type.Error, $"try to mark ref1 but fail to ready");
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
                            Logger.Log(Logger.Type.Error, $"try to mark ref2 but fail to ready");
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

                if (!this.visionComm.IsConnected)
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
                    visionComm.Send(MessageProtocol.LASER_READ_HATCHING_01_FINISH);
                    break;
                case "2":
                    visionComm.Send(MessageProtocol.LASER_READ_HATCHING_02_FINISH);
                    break;
            }
            marker.Tag = null; //reset
        }
        private void SystemTeach_OnFinished(IMarker sender, IMarkerArg markerArg)
        {
            var marker = sender as IMarker;
            visionComm.Send(MessageProtocol.LASER_SCANNER_SYSTEM_TEACH_FINISH);
        }
        private void FieldCorrection_OnFinished(IMarker sender, IMarkerArg markerArg)
        {
            var marker = sender as IMarker;
            visionComm.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_FINISH);
        }
        public bool ReadScannerFieldCorrection()
        {
            //비전에서 기록한 보정 측정 정보
            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "setup", "scannerfieldcorrection.txt");
            if (false == File.Exists(fileName))
            {
                this.Error(ErrEnum.VisionFieldCorrectionOpen);
                return false;
            }
            int rows = 9;
            int cols = 9;
            float interval = 10;
            var correction2D = new RtcCorrection2D(Rtc.KFactor, rows, cols, interval, Rtc.CorrectionFiles[0], string.Empty);
            var form2D = new Correction2DForm(correction2D);
            form2D.OnApply += Form2D_OnApply;
            isFieldCorrecting = true;
            Warn(WarnEnum.ScannerFieldCorrectioning);
            form2D.ShowDialog();
            isFieldCorrecting = false;
            Warn(WarnEnum.ScannerFieldCorrectioning, true);
            return true;
        }
        private void Form2D_OnApply(object sender, EventArgs e)
        {
            var form = sender as Correction2DForm;
            string ct5FileName = form.RtcCorrection.TargetCorrectionFile;
            if (!File.Exists(ct5FileName))
                return;
            var mb = new MessageBoxYesNo();
            if (DialogResult.Yes != mb.ShowDialog("Warning !", $"Do you really want to apply new correction file {ct5FileName} ?"))
                return;
            var rtc = this.Rtc;
            bool success = true;
            success &= rtc.CtlLoadCorrectionFile(CorrectionTableIndex.Table1, ct5FileName);
            success &= rtc.CtlSelectCorrection(CorrectionTableIndex.Table1);
            if (success)
            {
                //update ini file
                var iniFileName = FormMain.ConfigFileName;
                NativeMethods.WriteIni<string>(iniFileName, $"RTC", "CORRECTION", Path.GetFileName(ct5FileName));
                Logger.Log(Logger.Type.Warn, $"Correction file has changed to {ct5FileName}");
                this.Warn(WarnEnum.ScannerFieldCorrectionChanged);
                var mb2 = new MessageBoxOk();
                mb2.ShowDialog("Correction", $"Correction file has changed to {ct5FileName}");
            }
        }
        private void Ref_OnFinished(IMarker sender, IMarkerArg markerArg)
        {
            var marker = sender as IMarker;
            switch((string)marker.Tag)
            {
                case "1":
                    visionComm.Send(MessageProtocol.LASER_SCANNER_REF_01_IMAGE_FINISH);
                    break;
                case "2":
                    visionComm.Send(MessageProtocol.LASER_SCANNER_REF_02_IMAGE_FINISH);
                    break;
            }
        }

        /// <summary>
        /// 화면 편집기 업데이트
        /// </summary>
        /// <param name="index">1(오른쪽), 2(왼쪽)</param>
        /// <param name="group">그룹 객체들</param>
        /// <returns></returns>
        public bool PrepareDefectInEditor(int index, Group group)
        {
            // 오토 화면 
            //if (this.formMain.FormCurrent != this.formMain.FormAuto)
            //    return false;

            var doc = Editor.Document; //에디터의 doc 를 대상으로
            var name = $"Defect{index}";
            var layer = doc.Layers.NameOf(name);
            if (null == layer || 0 == layer.Count || !(layer[0] is IPen))
            {
                Error(ErrEnum.NoDefectLayer);
                Logger.Log(Logger.Type.Error, $"target layer or pen entity is not exist : {name}");
                return false;
            }

            Program.MainForm.Invoke(new MethodInvoker( delegate ()
            {
                var deleteEntities = new List<IEntity>(layer.Count);
                foreach(var entity in layer)
                {
                    if (!(entity.EntityType is IPen))
                        deleteEntities.Add(entity);
                }
                doc.Action.ActEntityDelete(deleteEntities);
                doc.Action.ActEntityAdd(group, layer);
                doc.Action.UndoRedoClear(); //100 undo 개수 제한이 있지만 메모리 소비가 클수있음 ? save memory
                Editor.View.Render();
            }));
            return true;
        }
        public bool PrepareDefectInMarker(int index, Group group)
        {
            // 오토 화면 
            //if (this.formMain.FormCurrent != this.formMain.FormAuto)
            //    return false;

            var doc = Marker.Document; //복제된 doc 를 대상으로
            var name = $"Defect{index}";
            var layer = doc.Layers.NameOf(name);
            if (null == layer || 0 == layer.Count || !(layer[0] is IPen))
            {
                Error(ErrEnum.NoDefectLayer);
                Logger.Log(Logger.Type.Error, $"target layer or pen entity is not exist : {name}");
                return false;
            }
            Program.MainForm.Invoke(new MethodInvoker(delegate ()
            {
                // 첫번째 객체를 제외하고 모두 삭제
                var deleteEntities = new List<IEntity>(layer.Count);
                foreach (var entity in layer)
                {
                    if (!(entity.EntityType is IPen))
                        deleteEntities.Add(entity);
                }
                doc.Action.ActEntityDelete(deleteEntities);
                doc.Action.ActEntityAdd(group, layer);                
                doc.Action.UndoRedoClear(); //100 undo 개수 제한이 있지만 메모리 소비가 클수있음 ? save memory
            }));
            return true;
        }
        public bool ReadDefectFromFile(string fileName, out Group group)
        {
            bool success = true;
            group = new Group();
            group.Name = $"Defect";
            group.IsEnableFastRendering = true;
            if (!File.Exists(fileName))
            {
                Error(ErrEnum.VisionDataOpen);
                Logger.Log(Logger.Type.Error, $"fail to open vision defect file : {fileName}");
                return false;
            }
            
            int counts = 0;
            Warn(WarnEnum.VisionDataOpen);
            try
            {
                using (var stream = new StreamReader(fileName))
                {
                    while (!stream.EndOfStream)
                    {
                        var line = stream.ReadLine();
                        if (string.IsNullOrEmpty(line))
                            continue;

                        //if (line.ElementAt(0) == ';')
                        //    continue;
                        //string[] tokens = line.Split(',');
                        //float x = float.Parse(tokens[0]);
                        //float y = float.Parse(tokens[1]);
                        //float angle = float.Parse(tokens[2]);

                        //var poly = new LwPolyline();
                        //poly.Add(new LwPolyLineVertex(55.3005f, 125.1903f, 0));
                        //poly.Add(new LwPolyLineVertex(80.5351f, 161.2085f, 0));
                        //poly.Add(new LwPolyLineVertex(129.8027f, 148.6021f, -1.3108f));
                        //poly.Add(new LwPolyLineVertex(107.5722f, 109.5824f, 0.8155f));
                        //poly.Add(new LwPolyLineVertex(77.5310f, 89.7724f, 0));
                        //poly.IsClosed = true;
                        //poly.IsHatchable = true;
                        //poly.HatchAngle = 90;
                        //poly.HatchInterval = 0.2f;
                        //poly.HatchExclude = 0.1f;
                        //poly.Regen();
                        //Program.MainForm.Invoke(new MethodInvoker(delegate ()
                        //{
                        //    editor.Document.Action.ActEntityAdd(poly);
                        //    editor.View.Render();

                        //}));
                        counts++;
                    }
                }
                Logger.Log(Logger.Type.Info, $"success to open defect file : {counts} counts at {fileName}");
            }
            catch(Exception ex)
            {
                Logger.Log(Logger.Type.Error, $"fail to open defect file : {fileName} : {ex.Message}");
                success &= false;
            }
            finally
            {
                if (success)
                {
                    group.Regen();
                    //x 정렬 오름차순
                    group.Sort(delegate (IEntity e1, IEntity e2)
                    {
                        return e1.BoundRect.Center.X.CompareTo(e2.BoundRect.Center.X);
                    });
                    Warn(WarnEnum.VisionDataOpen, true);
                }
            }
            return success;
        }
    }
}