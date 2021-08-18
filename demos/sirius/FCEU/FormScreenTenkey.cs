using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SpiralLab.Sirius.FCEU
{
    public partial class FormScreenTenkey : Form
    {
        public FormScreenTenkey()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (User.Level ==  UserLevel.Operator)
            //{
            //    var mb = new MessageBoxOk();
            //    mb.ShowDialog("Warning !!! User Level", $"You do not have user privileges. Please log in again with elevated privileges.");
            //        return;
            //}
            {
                var mb = new MessageBoxYesNo();
                if (DialogResult.Yes != mb.ShowDialog("Warning !!! System Teach", $"Do you really want to mark with system teach ?"))
                    return;
            }
            var formMain = Program.MainForm as SpiralLab.Sirius.FCEU.FormMain;
            var seq = formMain.Seq;
            var svc = seq.Service as LaserService;
            seq.Start(LaserSequence.Process.SystemTeach);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var mb = new MessageBoxYesNo();
            if (DialogResult.Yes != mb.ShowDialog("Warning !!! Scanner Field Correction", $"Do you really want to mark with scanner field correction ?"))
                return;
            var formMain = Program.MainForm as SpiralLab.Sirius.FCEU.FormMain;
            var seq = formMain.Seq;
            var svc = seq.Service as LaserService;

            //read from ReadScannerFieldCorrection(file)
            //svc.FieldCorrectionInterval;
            //svc.FieldCorrectionCols;
            //svc.FieldCorrectionRows;

            seq.Start(LaserSequence.Process.FieldCorrection);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (User.Level == UserLevel.Operator)
            {
                var mb = new MessageBoxOk();
                mb.ShowDialog("Warning !!! User Level", $"You do not have user privileges. Please log in again with elevated privileges.");
                return;
            }

            var formMain = Program.MainForm as SpiralLab.Sirius.FCEU.FormMain;
            var seq = formMain.Seq;
            var svc = seq.Service as LaserService;

            var iniFileName = FormMain.ConfigFileName;
            string fullFilePath = NativeMethods.ReadIni<string>(iniFileName, $"FILE", "CORRECTION");

            var dlg = new OpenFileDialog();
            dlg.Filter = "scanner correction data file (*.txt)|*.txt|All Files (*.*)|*.*";
            dlg.Title = "Open Scanner Correction Data File";
            dlg.InitialDirectory = fullFilePath;
            dlg.FileName = $"scanner_calibration_{svc.FieldCorrectionRows}v{ svc.FieldCorrectionCols}.txt";
            DialogResult result = dlg.ShowDialog();
            if (result != DialogResult.OK)
                return;

            svc.ReadScannerFieldCorrection(dlg.FileName);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var formMain = Program.MainForm as SpiralLab.Sirius.FCEU.FormMain;
            var seq = formMain.Seq;
            var svc = seq.Service as LaserService;

            var defRoot = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"FILE", "DEFECT");
            //var defFile = Path.Combine(defRoot, $"{svc.RecipeName}\\temp\\share_result_data_02.txt");
            var dlg = new OpenFileDialog();
            dlg.FileName = "share_result_data_02.txt";
            dlg.InitialDirectory = Path.Combine(defRoot, $"{svc.RecipeName}\\temp");
            dlg.Filter = "vision defect left files (*.txt)|*.txt|All Files (*.*)|*.*";
            dlg.Title = "Open Vision Defect File ...";
            DialogResult result = dlg.ShowDialog();
            if (result != DialogResult.OK)
                return;
            if (svc.ReadDefectFromFile(2, dlg.FileName, out var group, out float dx, out float dy, out float angle))
            {
                if (svc.PrepareDefectInEditor(2, group, dx, dy, angle))
                {
                    Logger.Log(Logger.Type.Warn, $"manually defect (left side) file loaded : {dlg.FileName}");
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (User.Level == UserLevel.Operator)
            {
                var mb = new MessageBoxOk();
                mb.ShowDialog("Warning !!! User Level", $"You do not have user privileges. Please log in again with elevated privileges.");
                return;
            }
            {
                var mb = new MessageBoxYesNo();
                if (DialogResult.Yes != mb.ShowDialog("Warning !!! Left Reference Mark", $"Do you want to mark with left reference ?"))
                    return;
            }
            var formMain = Program.MainForm as SpiralLab.Sirius.FCEU.FormMain;
            var seq = formMain.Seq;
            var svc = seq.Service as LaserService;
            seq.Start(LaserSequence.Process.Ref_Left);
        }
        private void button9_Click(object sender, EventArgs e)
        {
            if (User.Level == UserLevel.Operator)
            {
                var mb = new MessageBoxOk();
                mb.ShowDialog("Warning !!! User Level", $"You do not have user privileges. Please log in again with elevated privileges.");
                return;
            }
            {
                var mb = new MessageBoxYesNo();
                if (DialogResult.Yes != mb.ShowDialog("Warning !!! Left Defect Mark", $"Do you want to mark with left defect ?"))
                    return;
            }
            var formMain = Program.MainForm as SpiralLab.Sirius.FCEU.FormMain;
            var seq = formMain.Seq;
            var svc = seq.Service as LaserService;
            seq.Start(LaserSequence.Process.Defect_Left);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var formMain = Program.MainForm as SpiralLab.Sirius.FCEU.FormMain;
            var seq = formMain.Seq;
            var svc = seq.Service as LaserService;

            var defRoot = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"FILE", "DEFECT");
            //var defFile = Path.Combine(defRoot, $"{svc.RecipeName}\\temp\\share_result_data_01.txt");
            var dlg = new OpenFileDialog();
            dlg.FileName = "share_result_data_01.txt";
            dlg.InitialDirectory = Path.Combine(defRoot, $"{svc.RecipeName}\\temp");
            dlg.Filter = "vision defect right files (*.txt)|*.txt|All Files (*.*)|*.*";
            dlg.Title = "Open Vision Defect File ...";
            DialogResult result = dlg.ShowDialog();
            if (result != DialogResult.OK)
                return;
            if (svc.ReadDefectFromFile(1, dlg.FileName, out var group, out float dx, out float dy, out float angle))
            {
                if (svc.PrepareDefectInEditor(1, group, dx, dy, angle))
                {
                    Logger.Log(Logger.Type.Warn, $"manually defect (right side) file loaded : {dlg.FileName}");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (User.Level == UserLevel.Operator)
            {
                var mb = new MessageBoxOk();
                mb.ShowDialog("Warning !!! User Level", $"You do not have user privileges. Please log in again with elevated privileges.");
                return;
            }
            {
                var mb = new MessageBoxYesNo();
                if (DialogResult.Yes != mb.ShowDialog("Warning !!! Right Reference Mark", $"Do you want to mark with right reference ?"))
                    return;
            }
            var formMain = Program.MainForm as SpiralLab.Sirius.FCEU.FormMain;
            var seq = formMain.Seq;
            var svc = seq.Service as LaserService;
            seq.Start(LaserSequence.Process.Ref_Right);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (User.Level == UserLevel.Operator)
            {
                var mb = new MessageBoxOk();
                mb.ShowDialog("Warning !!! User Level", $"You do not have user privileges. Please log in again with elevated privileges.");
                return;
            }
            {
                var mb = new MessageBoxYesNo();
                if (DialogResult.Yes != mb.ShowDialog("Warning !!! Right Defect Mark", $"Do you want to mark with right defect ?"))
                    return;
            }
            var formMain = Program.MainForm as SpiralLab.Sirius.FCEU.FormMain;
            var seq = formMain.Seq;
            var svc = seq.Service as LaserService;
            seq.Start(LaserSequence.Process.Defect_Right);
        }
    }
}
