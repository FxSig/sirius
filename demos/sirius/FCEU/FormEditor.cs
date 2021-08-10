using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Numerics;

namespace SpiralLab.Sirius.FCEU
{
    public partial class FormEditor : Form
    {
        public SiriusEditorForm SiriusEditor
        {
            get
            {
                return this.siriusEditorForm1;
            }
        }
        public FormEditor()
        {
            InitializeComponent();

            this.SiriusEditor.OnDocumentOpen += SiriusEditor_OnDocumentOpen;
            this.SiriusEditor.OnDocumentSave += SiriusEditor_OnDocumentSave;
            this.SiriusEditor.OnCorrection2D += SiriusEditor_OnCorrection2D;
        }

        private void SiriusEditor_OnDocumentOpen(object sender)
        {
            var formMain = Program.MainForm as SpiralLab.Sirius.FCEU.FormMain;
            var seq = formMain.Seq;
            var svc = seq.Service as LaserService;

            if (svc.RecipeNo <= 0)
            {
                var ofd = new OpenFileDialog();
                ofd.Filter = "Sirius data files (*.sirius)|*.sirius|All Files (*.*)|*.*";
                ofd.Title = "Open File";
                ofd.FileName = string.Empty;
                ofd.Multiselect = false;
                ofd.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "recipes");
                DialogResult result = ofd.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    if (SiriusEditor.OnOpen(ofd.FileName))
                    {
                        var mb2 = new MessageBoxOk();
                        mb2.ShowDialog("Open File", $"Success to open file : {ofd.FileName}");
                    }
                    else
                    {
                        var mb2 = new MessageBoxOk();
                        mb2.ShowDialog("Open File", $"Fail to open file : {ofd.FileName}");
                    }
                }
                return;
            }

            var fileName = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"FILE", "RECIPE");
            string recipeFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "recipes", $"{svc.RecipeNo}", fileName);
            if (File.Exists(recipeFileName))
            {
                if (SiriusEditor.OnOpen(recipeFileName))
                {
                    var mb2 = new MessageBoxOk();
                    mb2.ShowDialog("Open File", $"Success to open file [{svc.RecipeNo}] {svc.RecipeName} at {recipeFileName}", 10);
                }
                else
                {
                    var mb2 = new MessageBoxOk();
                    mb2.ShowDialog("Open File", $"Fail to open file [{svc.RecipeNo}] {svc.RecipeName} at {recipeFileName}");
                }
            }

            //var ofd = new OpenFileDialog();
            //ofd.Filter = "Sirius data files (*.sirius)|*.sirius|All Files (*.*)|*.*";
            //ofd.Title = "Open File";
            //ofd.FileName = string.Empty;
            //ofd.Multiselect = false;
            //DialogResult result = ofd.ShowDialog(this);
            //if (result == DialogResult.OK)
            //{
            //    SiriusEditor.OnOpen(ofd.FileName);
            //}
        }

        private void SiriusEditor_OnDocumentSave(object sender)
        {
            var formMain = Program.MainForm as SpiralLab.Sirius.FCEU.FormMain;
            var seq = formMain.Seq;
            var svc = seq.Service as LaserService;

            if (svc.RecipeNo <= 0)
            {
                //invalid ?
                SiriusEditor.OnSave("");
                return;
            }

            //var fileName = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"FILE", "RECIPE");
            //string recipeFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "recipes", $"{svc.RecipeNo}", fileName);
            //if (!File.Exists(recipeFileName))
            //{
            //    //invalid ?
            //    return;
            //}
            //SiriusEditor.OnSave(recipeFileName);

            if (string.IsNullOrEmpty(SiriusEditor.FileName))
            {
                var sfd = new SaveFileDialog();
                sfd.Filter = "Sirius data files (*.sirius)|*.sirius|All Files (*.*)|*.*";
                sfd.Title = "Save As ...";
                sfd.FileName = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"FILE", "RECIPE");
                sfd.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "recipes", $"{svc.RecipeNo}");
                DialogResult result = sfd.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    if (true == SiriusEditor.OnSaveAs(sfd.FileName))
                    {
                        var mb = new MessageBoxOk();
                        mb.ShowDialog("Document Save", $"Success to save file : {sfd.FileName}", 10);
                    }
                    else
                    {
                        var mb = new MessageBoxOk();
                        mb.ShowDialog("Document Save", $"Fail to save file : {sfd.FileName}");
                    }
                }
            }
            else
            {
                {
                    var mb = new MessageBoxYesNo();
                    if (DialogResult.Yes != mb.ShowDialog("Warning !", $"Do you really want to save [{svc.RecipeNo}] {svc.RecipeName} at {SiriusEditor.FileName} ?"))
                        return;
                }
                if (true == SiriusEditor.OnSave(SiriusEditor.FileName))
                {
                    var mb = new MessageBoxOk();
                    mb.ShowDialog("Document Save", $"Success to save [{svc.RecipeNo}] {svc.RecipeName}", 10);
                }
                else
                {
                    var mb = new MessageBoxOk();
                    mb.ShowDialog("Document Save", $"Fail to save [{svc.RecipeNo}] {svc.RecipeName}");
                }
            }
        }

        private void SiriusEditor_OnCorrection2D(object sender, EventArgs e)
        {
            return;

            var formMain = Program.MainForm as SpiralLab.Sirius.FCEU.FormMain;
            var seq = formMain.Seq;
            var svc = seq.Service as LaserService;

            //example codes
            var rtc = this.SiriusEditor.Rtc;

            int rows = svc.FieldCorrectionRows;
            int cols = svc.FieldCorrectionCols;
            float interval = svc.FieldCorrectionInterval;
            var correction2D = new RtcCorrection2D(rtc.KFactor, rows, cols, interval, rtc.CorrectionFiles[0], string.Empty);
            float left = -interval * (float)(int)(cols / 2);
            float top = interval * (float)(int)(rows / 2);
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    correction2D.AddRelative(row, col,
                        new Vector2(
                            left + col * interval,
                            top - row * interval),
                         Vector2.Zero
                        );
                }
            }
            var form2D = new Correction2DForm(correction2D);
            form2D.OnApply += Form2D_OnApply;
            form2D.ShowDialog(this);
        }

        private void Form2D_OnApply(object sender, EventArgs e)
        {
            var form = sender as Correction2DForm;
            string ct5FileName = form.RtcCorrection.TargetCorrectionFile;
            if (!File.Exists(ct5FileName))
                return;
            var mb = new MessageBoxYesNo();
            if (DialogResult.Yes != mb.ShowDialog("Warning !", "Do you really want to apply new correction file ?"))
                return;
            var rtc = this.SiriusEditor.Rtc;
            bool success = true;
            success = rtc.CtlLoadCorrectionFile(CorrectionTableIndex.Table1, ct5FileName);
            success = rtc.CtlSelectCorrection(CorrectionTableIndex.Table1);
            if (success)
            {
                //update ini file
                var iniFileName = FormMain.ConfigFileName;
                NativeMethods.WriteIni<string>(iniFileName, $"RTC", "CORRECTION", Path.GetFileName(ct5FileName));
                var mb2 = new MessageBoxOk();
                mb2.ShowDialog("Correction", $"Correction file has changed to {iniFileName}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (null == SiriusEditor.Document)
                return;
            var form = new FormHatch();

            string extData = SiriusEditor.Document.ExtensionData;
            if (string.IsNullOrEmpty(extData))
            {
                Logger.Log(Logger.Type.Error, $"document extension data (hatch) is empty !");
                return;
            }
            var tempFileName = Path.GetTempFileName();
            using (StreamWriter sw = new StreamWriter(tempFileName))
            {
                sw.Write(extData);
            }
            bool isHatchable = NativeMethods.ReadIni<bool>(tempFileName, $"HATCH", "HATCHABLE");
            HatchMode hatchMode = (HatchMode)NativeMethods.ReadIni<int>(tempFileName, $"HATCH", "MODE");
            float hatchAngle = NativeMethods.ReadIni<float>(tempFileName, $"HATCH", "ANGLE");
            float hatch2Angle = NativeMethods.ReadIni<float>(tempFileName, $"HATCH", "ANGLE2");
            float hatchInterval = NativeMethods.ReadIni<float>(tempFileName, $"HATCH", "INTERVAL");
            float hatchExclude = NativeMethods.ReadIni<float>(tempFileName, $"HATCH", "EXCLUDE");
            uint repeat = NativeMethods.ReadIni<uint>(tempFileName, $"HATCH", "REPEAT");
            if (repeat <= 1)
                repeat = 1;
            if (hatchInterval <= 0)
                isHatchable = false;

            form.IsHatchable = isHatchable;
            form.Angle1 = hatchAngle;
            form.Angle2 = hatch2Angle;
            form.Interval = hatchInterval;
            form.Exclude = hatchExclude;
            form.Mode = hatchMode;
            form.Repeat = repeat;

            if (form.ShowDialog() != DialogResult.OK)
                return;

            isHatchable = form.IsHatchable;
            hatchAngle = form.Angle1;
            hatch2Angle = form.Angle2;
            hatchInterval = form.Interval;
            hatchExclude = form.Exclude;
            hatchMode = form.Mode;
            repeat = form.Repeat;

            NativeMethods.WriteIni<bool>(tempFileName, $"HATCH", "HATCHABLE", isHatchable);
            NativeMethods.WriteIni<int>(tempFileName, $"HATCH", "MODE", (int)hatchMode);
            NativeMethods.WriteIni<float>(tempFileName, $"HATCH", "ANGLE", hatchAngle);
            NativeMethods.WriteIni<float>(tempFileName, $"HATCH", "ANGLE2", hatch2Angle);
            NativeMethods.WriteIni<float>(tempFileName, $"HATCH", "INTERVAL", hatchInterval);
            NativeMethods.WriteIni<float>(tempFileName, $"HATCH", "EXCLUDE", hatchExclude);
            NativeMethods.WriteIni<uint>(tempFileName, $"HATCH", "REPEAT", repeat);


            //읽어서 doc ext 에 설정
            extData = File.ReadAllText(tempFileName);
            SiriusEditor.Document.ExtensionData = extData;
            //done
        }
    }
}