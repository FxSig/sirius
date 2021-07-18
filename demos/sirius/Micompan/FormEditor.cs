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

namespace SpiralLab.Sirius.Micompan
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
            //this.SiriusEditor.OnCorrection3D += SiriusEditor_OnCorrection3D;
        }

        private void SiriusEditor_OnCorrection2D(object sender, EventArgs e)
        {
            //example codes
            var rtc = this.SiriusEditor.Rtc;

            var rows = NativeMethods.ReadIni<int>(FormMain.ConfigFileName, $"CORRECTION", "ROWS");
            var cols = NativeMethods.ReadIni<int>(FormMain.ConfigFileName, $"CORRECTION", "COLS");
            float interval = NativeMethods.ReadIni<float>(FormMain.ConfigFileName, $"CORRECTION", "INTERVAL");

            var correction2D = new RtcCorrection2D(rtc.KFactor, rows, cols, interval, rtc.CorrectionFiles[0], string.Empty);
            float left = -interval * (float)(int)(cols / 2);
            float top = interval * (float)(int)(rows / 2);
            var rand = new Random();
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

        private void SiriusEditor_OnCorrection3D(object sender, EventArgs e)
        {
            //example codes
            var rtc = this.SiriusEditor.Rtc;

            var rows = NativeMethods.ReadIni<int>(FormMain.ConfigFileName, $"CORRECTION", "ROWS");
            var cols = NativeMethods.ReadIni<int>(FormMain.ConfigFileName, $"CORRECTION", "COLS");
            float interval = NativeMethods.ReadIni<float>(FormMain.ConfigFileName, $"CORRECTION", "INTERVAL");
            float upper = NativeMethods.ReadIni<float>(FormMain.ConfigFileName, $"CORRECTION", "UPPER");
            float lower = NativeMethods.ReadIni<float>(FormMain.ConfigFileName, $"CORRECTION", "LOWER");

            var correction3D = new RtcCorrection3D(rtc.KFactor, rows, cols, interval, upper, lower, rtc.CorrectionFiles[0], string.Empty);
            float left = -interval * (float)(int)(cols / 2);
            float top = interval * (float)(int)(rows / 2);
            var rand = new Random();
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    correction3D.AddRelative(row, col,
                        new Vector3(
                            left + col * interval,
                            top - row * interval,
                            upper),
                        new Vector3(0,0,
                            upper)
                        );
                }
            }
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    correction3D.AddRelative(row, col,
                        new Vector3(
                            left + col * interval,
                            top - row * interval,
                            lower),
                        new Vector3(0,0,
                            lower)
                        );
                }
            }
            var form3D = new Correction3DForm(correction3D);
            form3D.OnApply += Form3D_OnApply;
            form3D.ShowDialog(this);
        }

        private void Form3D_OnApply(object sender, EventArgs e)
        {
            var form = sender as Correction3DForm;
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
    }
}