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
            this.SiriusEditor.OnCorrection2D += SiriusEditor_OnCorrection2D;

            //SiriusEditor.OnDocumentOpen += SiriusEditor_OnDocumentOpen;
            //SiriusEditor.OnDocumentSave += SiriusEditor_OnDocumentSave;
        }

        private void SiriusEditor_OnDocumentOpen(object sender)
        {
            var formMain = Program.MainForm as SpiralLab.Sirius.FCEU.FormMain;
            var seq = formMain.Seq;
            var svc = seq.Service as LaserService;

            if (svc.RecipeNo <= 0)
            {
                //invalid ?
                return;
            }

            var fileName = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"FILE", "RECIPE");
            string recipeFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "recipes", $"{svc.RecipeNo}", fileName);
            if (!File.Exists(recipeFileName))
            {
                //invalid ?
                return;
            }
            SiriusEditor.OnOpen(recipeFileName);

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

            //if (svc.RecipeNo <= 0)
            //{
            //    //invalid ?
            //    return;
            //}

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
                sfd.FileName = string.Empty;
                sfd.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "recipes", $"{svc.RecipeNo}");
                DialogResult result = sfd.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    if (true == SiriusEditor.OnSaveAs(sfd.FileName))
                    {
                        var mb = new MessageBoxOk();
                        mb.ShowDialog("Document Save", $"Success to save : {SiriusEditor.FileName}", 10);
                    }
                    else
                    {
                        var mb = new MessageBoxOk();
                        mb.ShowDialog("Document Save", $"Fail to save : {SiriusEditor.FileName}", 10);
                    }
                }
            }
            else
            {
                {
                    var mb = new MessageBoxYesNo();
                    if (DialogResult.Yes != mb.ShowDialog("Warning !", $"Do you really want to save as {SiriusEditor.FileName} ?"))
                        return;
                }
                if (true == SiriusEditor.OnSave(SiriusEditor.FileName))
                {
                    var mb = new MessageBoxOk();
                    mb.ShowDialog("Document Save", $"Success to save : {SiriusEditor.FileName}", 10);
                }
                else
                {
                    var mb = new MessageBoxOk();
                    mb.ShowDialog("Document Save", $"Fail to save : {SiriusEditor.FileName}", 10);
                }
            }
        }

        private void SiriusEditor_OnCorrection2D(object sender, EventArgs e)
        {
            return;

            //example codes
            //var rtc = this.SiriusEditor.Rtc;

            //int rows = 5;
            //int cols = 5;

            //float interval = 10.0f;
            //var correction2D = new RtcCorrection2D(rtc.KFactor, rows, cols, interval, rtc.CorrectionFiles[0], string.Empty);
            //float left = -interval * (float)(int)(cols / 2);
            //float top = interval * (float)(int)(rows / 2);
            //var rand = new Random();
            //for (int row = 0; row < rows; row++)
            //{
            //    for (int col = 0; col < cols; col++)
            //    {
            //        correction2D.AddRelative(row, col,
            //            new Vector2(
            //                left + col * interval,
            //                top - row * interval),
            //            new Vector2(
            //                rand.Next(20) / 1000.0f - 0.01f,
            //                rand.Next(20) / 1000.0f - 0.01f)
            //            );
            //    }
            //}
            //var form2D = new Correction2DForm(correction2D);
            //form2D.OnApply += Form2D_OnApply;
            //form2D.ShowDialog(this);
        }

        //private void Form2D_OnApply(object sender, EventArgs e)
        //{
        //    var form = sender as Correction2DForm;
        //    string ct5FileName = form.RtcCorrection.TargetCorrectionFile;
        //    if (!File.Exists(ct5FileName))
        //        return;
        //    var mb = new MessageBoxYesNo();
        //    if (DialogResult.Yes != mb.ShowDialog("Warning !", "Do you really want to apply new correction file ?"))
        //        return;
        //    var rtc = this.SiriusEditor.Rtc;
        //    bool success = true;
        //    success = rtc.CtlLoadCorrectionFile(CorrectionTableIndex.Table1, ct5FileName);
        //    success = rtc.CtlSelectCorrection(CorrectionTableIndex.Table1);
        //    if (success)
        //    {
        //        //update ini file
        //        var iniFileName = FormMain.ConfigFileName;
        //        NativeMethods.WriteIni<string>(iniFileName, $"RTC", "CORRECTION", Path.GetFileName(ct5FileName));
        //        var mb2 = new MessageBoxOk();
        //        mb2.ShowDialog("Correction", $"Correction file has changed to {iniFileName}");
        //    }
        //}
    }
}