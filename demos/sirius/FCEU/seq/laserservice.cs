using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralLab.Sirius.FCEU
{
    /// <summary>
    /// 레이저 서비스 객체
    /// </summary>
    public class LaserService
        : SpiralLab.IServiceLaser
    {
        public string Name { get; set; }
        public object Tag { get; set; }
        public int RecipeNo { get; internal set; }
        public string RecipeName { get; internal set; }
        public bool IsVisionConnected
        {
            get
            {
                return seq.VisionComm.IsConnected;
            }
        }
        public int FieldCorrectionRows { get; set; }
        public int FieldCorrectionCols { get; set; }
        public float FieldCorrectionInterval { get; set; }

        LaserSequence seq;

        public LaserService(LaserSequence seq)
        {
            this.Name = "FCEU Laser Service";
            this.seq = seq;
            this.RecipeClear();
        }
        public void RecipeClear()
        {
            RecipeNo = -1;
            RecipeName = string.Empty;
            var doc = new Sirius.DocumentDefault();

            Program.MainForm.Invoke(new MethodInvoker(delegate ()
            {
                seq.Editor.Document = doc;
                //seq.Viewer.Document = doc;

                //사용자 정의 펜 생성
                //var pen = new FCEUPen();
                //seq.Editor.OnDocumentPenNew += SiriusEditorForm1_OnDocumentPenNew;
                //기본 펜 생성 
                var pen = new PenDefault();
                doc.Action.ActEntityAdd(pen);
            }));

        }
        //private void SiriusEditorForm1_OnDocumentPenNew(object sender)
        //{
        //    // 사용자 정의 펜 엔티티를 생성
        //    var pen = new FCEUPen();
        //    seq.Editor.OnPenNew(pen);
        //}
        public bool RecipeChange(int no)
        {
            if (seq.IsBusy)
            {
                seq.Error(ErrEnum.Busy);
                Logger.Log(Logger.Type.Error, $"try to change recipe buy busy !");
                return false;
            }
            this.RecipeClear();
            bool success = true;
            string recipeFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "recipes", $"{no}", "laser.sirius");
            if (!File.Exists(recipeFileName))
            {
                Logger.Log(Logger.Type.Error, $"fail to change recipe to [{no}]: {recipeFileName}");
                seq.Error(ErrEnum.RecipeChange);
                return false;
            }
            string iniFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "recipes", "recipe.ini");
            string recipeName = NativeMethods.ReadIni<string>(iniFileName, $"{no}", "NAME");
            var form = new ProgressForm()
            {
                Message = $"Loading Recipe : [{no}] {recipeName}" + Environment.NewLine,
                Percentage = 0,
            };
            Program.MainForm.Invoke(new MethodInvoker(delegate ()
            {
                form.Show(seq.Editor);
            }));
            var doc = DocumentSerializer.OpenSirius(recipeFileName);
            if (null == doc)
            {
                seq.Error(ErrEnum.RecipeChange);
                Logger.Log(Logger.Type.Error, $"fail to change recipe to [{no}]: {recipeName}");
                Program.MainForm.Invoke(new MethodInvoker(delegate ()
                {
                    form.Close();
                }));
                return false;
            }

            var markerArg = new MarkerArgDefault();
            markerArg.Document = doc;
            markerArg.Rtc = seq.Rtc;
            markerArg.Laser = seq.Laser;
            markerArg.RtcListType = ListType.Auto;
            Program.MainForm.Invoke(new MethodInvoker(delegate ()
            {
                form.Percentage = 50;
            }));
            success &= seq.Marker.Ready(markerArg);
            if (success)
            {
                Program.MainForm.Invoke(new MethodInvoker(delegate ()
                {
                    Application.DoEvents();
                    seq.Editor.Document = doc; //updated !
                    seq.Editor.FileName = recipeFileName;
                    seq.Viewer.FileName = recipeFileName;
                    //Viewer.Document = doc; 자동 !
                    Application.DoEvents();
                    Program.MainForm.BeginInvoke(new MethodInvoker(delegate ()
                    {
                        form.Percentage = 100;
                    }));
                }));
                RecipeNo = no;
                RecipeName = recipeName;
                seq.Warn(WarnEnum.RecipeChanged);
                Logger.Log(Logger.Type.Warn, $"recipe has changed to [{no}]: {recipeName}");
            }
            else
            {
                // turn off ready status
                //seq.Editor.Document = null;
                seq.Marker.Clear();
                seq.Error(ErrEnum.RecipeChange);
                Logger.Log(Logger.Type.Warn, $"fail to change recipe to [{no}]: {recipeName}");
            }
            Program.MainForm.Invoke(new MethodInvoker(delegate ()
            {
                form.Close();
            }));
            return success;
        }
        public bool ReadScanFieldCorrectionInterval(out int rows, out int cols, out float interval)
        {
            rows = cols = 0;
            interval = 0;
            if (seq.isFieldCorrecting)
            {
                seq.Error(ErrEnum.VisionFieldCorrectionOpen);
                Logger.Log(Logger.Type.Error, $"another field correction form is activating");
                return false;
            }
            string rootPath = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"FILE", "CORRECTION");
            string fileFullPath = Path.Combine(rootPath, $"scanner_matrix_gap_data.txt");
            if (false == File.Exists(fileFullPath))
            {
                seq.Error(ErrEnum.VisionFieldCorrectionOpen);
                Logger.Log(Logger.Type.Error, $"try to open field correction file interval but failed : {fileFullPath}");
                return false;
            }

            string line = string.Empty;
            using (var stream = new StreamReader(fileFullPath))
            {
                while (!stream.EndOfStream)
                {
                    line = stream.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        continue;
                    if (line.StartsWith(";"))
                        continue;
                    //3,5,2,2
                    string[] tokens = line.Split(new char[] { ',', ';' });
                    if (4 != tokens.Length)
                    {
                        Logger.Log(Logger.Type.Error, $"invalid file format: {line} at {fileFullPath}");
                        return false;
                    }
                    rows = int.Parse(tokens[0]) ;
                    cols = int.Parse(tokens[1]);
                    interval = float.Parse(tokens[2]);
                    return true;
                }
            }
            return false;
        }
        public bool ReadScannerFieldCorrection(string fileFullPath = "")
        {
            if (seq.isFieldCorrecting)
            {
                seq.Error(ErrEnum.VisionFieldCorrectionOpen);
                Logger.Log(Logger.Type.Error, $"another field correction form is activating");
                return false;
            }
            //비전에서 기록한 보정 측정 정보
            if (string.IsNullOrEmpty(fileFullPath))
            {
                string rootPath = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"FILE", "CORRECTION");
                //scanner_calibration_5v5.txt
                fileFullPath = Path.Combine(rootPath, $"scanner_calibration_{this.FieldCorrectionRows}v{this.FieldCorrectionCols}.txt");
            }
            if (false == File.Exists(fileFullPath))
            {
                seq.Error(ErrEnum.VisionFieldCorrectionOpen);
                Logger.Log(Logger.Type.Error, $"try to open field correction file but failed : {fileFullPath}");
                return false;
            }

            int rows = this.FieldCorrectionRows;
            int cols = this.FieldCorrectionCols;
            float interval = this.FieldCorrectionInterval;
            string sourceFile = seq.Rtc.CorrectionFiles[0];
            string targetFile = string.Empty;
            var correction2D = new RtcCorrection2D(seq.Rtc.KFactor, rows, cols, interval, sourceFile, targetFile);
            float left = -interval * (float)(int)(cols / 2);
            float top = interval * (float)(int)(rows / 2);
            
            string line = string.Empty;
            var list = new List<Vector2>(rows * cols);
            using (var stream = new StreamReader(fileFullPath))
            {
                while (!stream.EndOfStream)
                {
                    line = stream.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        continue;
                    if (line.StartsWith(";"))
                        continue;
                    
                    string[] tokens = line.Split(new char[] { ',', ';' });
                    if (3 != tokens.Length)
                    {
                        Logger.Log(Logger.Type.Error, $"invalid file format: {line} at {fileFullPath}");
                        return false;
                    }
                    //[0], x, y
                    //int no = int.Parse(tokens[0]); 
                    float dx = float.Parse(tokens[1]);
                    float dy = float.Parse(tokens[2]);
                    list.Add(new Vector2(dx, dy));
                }
            }

            if (list.Count != rows*cols)
            {
                Logger.Log(Logger.Type.Error, $"field correcction data counts are mismatched. rows X cols= {this.FieldCorrectionRows} X {this.FieldCorrectionCols} but readed: {list.Count}");
                return false;
            }

            //180 rotate and reverse order
            list.Reverse();
            left = -interval * (float)(int)(cols / 2);
            top = interval * (float)(int)(rows / 2);
            int index = 0;
            for (int row=0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    correction2D.AddRelative(row, col,
                        new Vector2(left + col * interval, top - row * interval),
                        new Vector2(-list[index].X, -list[index].Y) // xy 비전 좌표값 반전
                        );
                    index++;
                }
            }

            var form2D = new Correction2DForm(correction2D);
            form2D.OnApply += Form2D_OnApply;
            form2D.OnClose += Form2D_OnClose;
            seq.isFieldCorrecting = true; //ready off
            seq.Warn(WarnEnum.ScannerFieldCorrectioning);
            Program.MainForm.Invoke(new MethodInvoker(delegate ()
            {
                form2D.Show(seq.Editor);
            }));
            return true;
        }
        private void Form2D_OnApply(object sender, EventArgs e)
        {
            var form = sender as Correction2DForm;
            string ct5FileName = form.RtcCorrection.TargetCorrectionFile;
            if (!File.Exists(ct5FileName))
            {
                Logger.Log(Logger.Type.Error, $"try to change correction file but not exist : {ct5FileName}");
                return;
            }
            var mb = new MessageBoxYesNo();
            if (DialogResult.Yes != mb.ShowDialog("Warning !", $"Do you really want to apply new correction file {ct5FileName} ?"))
                return;
            var rtc = seq.Rtc;
            bool success = true;
            success &= rtc.CtlLoadCorrectionFile(CorrectionTableIndex.Table1, ct5FileName);
            success &= rtc.CtlSelectCorrection(CorrectionTableIndex.Table1);
            if (success)
            {
                //update ini file
                var iniFileName = FormMain.ConfigFileName;
                NativeMethods.WriteIni<string>(iniFileName, $"RTC", "CORRECTION", Path.GetFileName(ct5FileName));
                seq.Warn(WarnEnum.ScannerFieldCorrectionChanged);
                Logger.Log(Logger.Type.Warn, $"correction file has changed to {ct5FileName} at {iniFileName}");
                var mb2 = new MessageBoxOk();
                mb2.ShowDialog("Correction", $"correction file has changed to {ct5FileName} at {iniFileName}");
            }
        }
        private void Form2D_OnClose(object sender, EventArgs e)
        {
            seq.isFieldCorrecting = false;
            seq.Warn(WarnEnum.ScannerFieldCorrectioning, true);
        }

        /// <summary>
        /// 화면 편집기 업데이트
        /// </summary>
        /// <param name="index">1(오른쪽), 2(왼쪽)</param>
        /// <param name="group">그룹 객체들</param>
        /// <returns></returns>
        public bool PrepareDefectInEditor(int index, Group group)
        {
            if (seq.IsBusy)
            {
                Logger.Log(Logger.Type.Error, $"trying to prepare defect data into editor but busy");
                return false;
            }
            var doc = seq.Editor.Document; //에디터의 doc 를 대상으로
            Layer layer = null;
            var defLayerRight = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"LAYER", "DEFECT_RIGHT");
            var defLayerLeft = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"LAYER", "DEFECT_LEFT");
            switch (index)
            {
                case 1:
                    layer = doc.Layers.NameOf(defLayerRight);
                    break;
                case 2:
                    layer = doc.Layers.NameOf(defLayerLeft);
                    break;
                default:
                    Logger.Log(Logger.Type.Error, $"invalid left/right id: {index}");
                    return false;
            }
            if (null == layer || 0 == layer.Count)
            {
                seq.Error(ErrEnum.NoDefectLayer);
                Logger.Log(Logger.Type.Error, $"target layer name is not exist : {defLayerRight} or {defLayerLeft}");
                return false;
            }
            if (!(layer[0] is IPen))
            {
                seq.Error(ErrEnum.NoDefectLayer);
                Logger.Log(Logger.Type.Error, $"target layer is not start with pen entity");
                return false;
            }

            Program.MainForm.BeginInvoke(new MethodInvoker(delegate ()
            {
                var form = new ProgressForm()
                {
                    Message = $"Creating Defect Data ..." + Environment.NewLine,
                    Percentage = 0,
                };
                form.Show(seq.Editor);
                var deleteEntities = new List<IEntity>(layer.Count);
                foreach (var entity in layer)
                {
                    if (!(entity is IPen))
                        deleteEntities.Add(entity);
                }
                form.Percentage = 10;
                Application.DoEvents();
                doc.Action.ActEntityDelete(deleteEntities);
                Application.DoEvents();
                doc.Action.ActEntityAdd(group, layer);
                Application.DoEvents();
                form.Message += "Adding To Document ..." + Environment.NewLine;
                doc.Action.SelectedEntity = null;
                form.Percentage = 50;
                Application.DoEvents();
                doc.Action.UndoRedoClear(); //100 undo 개수 제한이 있지만 메모리 소비가 클수있음 ? save memory
                form.Message += $"Regening ..." + Environment.NewLine;
                Application.DoEvents();
                Program.MainForm.BeginInvoke(new MethodInvoker(delegate ()
                {
                    seq.Editor.View.Render();
                }));
                form.Percentage = 100;
                for (int i = 0; i < 100; i++)
                    Application.DoEvents();
                form.Close();
            }));
            return true;
        }
        //public bool PrepareDefectInMarker(int index, Group group)
        //{
        //    // 오토 화면 
        //    //if (this.formMain.FormCurrent != this.formMain.FormAuto)
        //    //    return false;
        //    if (seq.IsBusy)
        //    {
        //        seq.Error(ErrEnum.Busy);
        //        Logger.Log(Logger.Type.Error, $"trying to change defect info into marker but busy");
        //        return false;
        //    }

        //    //var doc = seq.Marker.Document; //복제된 doc 를 대상으로 ??
        //    var doc = seq.Marker.Document; //에디터의  doc 를 대상으로 ?

        //    Layer layer = null;
        //    var defLayerRight = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"LAYER", "DEFECT_RIGHT");
        //    var defLayerLeft = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"LAYER", "DEFECT_LEFT");
        //    switch (index)
        //    {
        //        case 1:
        //            layer = doc.Layers.NameOf(defLayerRight);
        //            break;
        //        case 2:
        //            layer = doc.Layers.NameOf(defLayerLeft);
        //            break;
        //    }
        //    if (null == layer || 0 == layer.Count)
        //    {
        //        seq.Error(ErrEnum.NoDefectLayer);
        //        Logger.Log(Logger.Type.Error, $"target layer name is not exist : {defLayerRight} or {defLayerLeft}");
        //        return false;
        //    }
        //    if (!(layer[0] is IPen))
        //    {
        //        seq.Error(ErrEnum.NoDefectLayer);
        //        Logger.Log(Logger.Type.Error, $"target layer is not start with pen entity");
        //        return false;
        //    }

        //    //Program.MainForm.Invoke(new MethodInvoker(delegate ()
        //    //{
        //    // 첫번째 객체를 제외하고 모두 삭제
        //    var deleteEntities = new List<IEntity>(layer.Count);
        //        foreach (var entity in layer)
        //        {
        //            if (!(entity is IPen))
        //                deleteEntities.Add(entity);
        //        }
        //        doc.Action.ActEntityDelete(deleteEntities);
        //        doc.Action.ActEntityAdd(group, layer);
        //        doc.Action.UndoRedoClear(); //100 undo 개수 제한이 있지만 메모리 소비가 클수있음 ? save memory
        //    //}));
        //    return true;
        //}

        /// <summary>
        /// Defect 파일 열고 분석
        /// </summary>
        /// <param name="index">1(오른쪽), 2(왼쪽)</param>
        /// <param name="fileName">비전 검사 결과 파일 이름</param>
        /// <param name="group">그룹 객체들</param>
        /// <returns></returns>
        public bool ReadDefectFromFile(int index, string fileName, out Group group)
        {
            group = null;
            ProgressForm form = new ProgressForm();
            Program.MainForm.Invoke(new MethodInvoker(delegate ()
            {
                form.Message = $"Reading From Defect File: {fileName}";
                form.Percentage = 0;
                form.Show(seq.Editor);
            }));

            if (!File.Exists(fileName))
            {
                Program.MainForm.Invoke(new MethodInvoker(delegate ()
                {
                    form.Close();
                }));

                seq.Error(ErrEnum.VisionDefectDataOpen);
                Logger.Log(Logger.Type.Error, $"fail to open vision defect file : {fileName}");
                return false;
            }

            var editor = seq.Editor;
            var doc = editor.Document; //에디터의 doc 를 대상
            bool isHatchable = false;
            HatchMode hatchMode = HatchMode.Line;
            float hatchAngle = 90;
            float hatch2Angle = 0;
            float hatchInterval = 0.1f;
            float hatchExclude = 0;

            string extData = doc.ExtensionData;
            if (string.IsNullOrEmpty(extData))
            {
                Logger.Log(Logger.Type.Error, $"document extension data (hatch) is empty !");
            }
            else
            {
                var tempFileName = Path.GetTempFileName();
                using (StreamWriter sw = new StreamWriter(tempFileName))
                {
                    sw.Write(extData);
                }
                isHatchable = NativeMethods.ReadIni<bool>(tempFileName, $"HATCH", "HATCHABLE");
                hatchMode = (HatchMode)NativeMethods.ReadIni<int>(tempFileName, $"HATCH", "MODE");
                hatchAngle = NativeMethods.ReadIni<float>(tempFileName, $"HATCH", "ANGLE");
                hatch2Angle = NativeMethods.ReadIni<float>(tempFileName, $"HATCH", "ANGLE2");
                hatchInterval = NativeMethods.ReadIni<float>(tempFileName, $"HATCH", "INTERVAL");
                hatchExclude = NativeMethods.ReadIni<float>(tempFileName, $"HATCH", "EXCLUDE");
            }

            bool success = true;
            group = new Group();
            group.Name = $"Defects";
            group.IsEnableFastRendering = true;
            group.IsHitTest = false; //선택 않되도록
            group.Align = Alignment.Center;
            int counts = 0;
            seq.Warn(WarnEnum.VisionDataOpening);
            try
            {
                using (var stream = new StreamReader(fileName))
                {
                    LwPolyline polyline = null;
                    while (!stream.EndOfStream)
                    {
                        string line = stream.ReadLine();
                        if (string.IsNullOrEmpty(line))
                            continue;
                        else if (line.StartsWith(";")) //주석
                            continue;
                        else if (line.StartsWith("POLYLINE_BEGIN"))
                        {
                            Debug.Assert(null == polyline);
                            string[] tokens = line.Split(',');
                            polyline = new LwPolyline();
                            if (tokens.Length > 1)
                                polyline.Name = tokens[1];
                        }
                        else if (line.StartsWith("POLYLINE_END"))
                        {
                            Debug.Assert(null != polyline);
                            if (polyline.Count >= 3)
                            {
                                polyline.IsClosed = true;
                                polyline.HatchMode = hatchMode;
                                polyline.IsHatchable = isHatchable;
                                polyline.HatchAngle = hatchAngle;
                                polyline.HatchInterval = hatchInterval;
                                polyline.HatchExclude = hatchExclude;
                            }
                            polyline.Regen();
                            group.Add(polyline);

                            counts++;
                            //// 하나씩 혹은 한번에 전체를 ?
                            //Program.MainForm.Invoke(new MethodInvoker(delegate ()
                            //{
                            //    docFceu.Action.ActEntityAdd(polyline);
                            //    editor.View.Render();
                            //    form.Percentage = counts++ / max * 100;
                            //}));

                            polyline = null;
                        }
                        else
                        {
                            string[] tokens = line.Split(',');
                            float x = float.Parse(tokens[0]);
                            float y = float.Parse(tokens[1]);
                            polyline.Add(new LwPolyLineVertex(x, y));
                        }
                    }
                }
                Logger.Log(Logger.Type.Info, $"success to open defect file : {counts} counts at {fileName}");
            }
            catch (Exception ex)
            {
                Logger.Log(Logger.Type.Error, $"fail to open defect file : {fileName} : {ex.Message}");
                success &= false;
            }
            finally
            {
                if (success)
                {
                    Program.MainForm.BeginInvoke(new MethodInvoker(delegate ()
                    {
                        form.Message = $"Regening ... ";
                    }));
                    group.Regen();
                    //x 정렬 오름차순
                    //group.Sort(delegate (IEntity e1, IEntity e2)
                    //{
                    //    return e1.BoundRect.Center.X.CompareTo(e2.BoundRect.Center.X);
                    //});
                    switch (index)
                    {
                        case 1: //right
                            {
                                var refLayerRight = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"LAYER", "REF_RIGHT");
                                var layer = doc.Layers.NameOf(refLayerRight);
                                if (null == layer)
                                {
                                    Logger.Log(Logger.Type.Error, $"target reference layer is not exist : {refLayerRight}");
                                }
                                else
                                {
                                    var br = layer.BoundRect;
                                    group.Transit(br.Center);
                                }
                            }
                            break;
                        case 2: //left
                            {
                                var refLayerLeft = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"LAYER", "REF_LEFT");
                                var layer = doc.Layers.NameOf(refLayerLeft);
                                if (null == layer)
                                {
                                    Logger.Log(Logger.Type.Error, $"target reference layer is not exist : {refLayerLeft}");
                                }
                                else
                                {
                                    var br = layer.BoundRect;
                                    group.Transit(br.Center);
                                }
                            }
                            break;                             
                    }
                    //
                    seq.Warn(WarnEnum.VisionDataOpening, true);
                }
                Program.MainForm.BeginInvoke(new MethodInvoker(delegate ()
                {
                    form.Close();
                }));
            }
            return success;
        }
    }
}