using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace SpiralLab.Sirius.FCEU
{
    public partial class FormMain : Form
    {
        public static string ConfigFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", "fceu.ini");

        public LaserSeq Seq { get; private set; }
        public Form FormCurrent { get; private set; }
        public FormAuto FormAuto { get; private set; }
        public FormEditor FormEditor { get; private set; }
        public FormSetup FormSetup { get; private set; }

        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();      

        public FormMain()
        {
            InitializeComponent();

            this.FormAuto = new FormAuto();
            this.FormEditor = new FormEditor();
            this.FormSetup = new FormSetup();

            this.Load += FormMain_Load;
            this.Shown += FormMain_Shown;
            this.FormClosing += FormMain_FormClosing;
            lsbErrWarn.DrawItem += LsbErrWarn_DrawItem;
        }

        private void LsbErrWarn_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
                return;
            lsbErrWarn.SuspendLayout();
            e.DrawBackground();
            Brush myBrush = Brushes.Black;
            if (this.lsbErrWarn.Items[e.Index].ToString().Contains("ER:"))
                myBrush = Brushes.Red;
            e.Graphics.DrawString(this.lsbErrWarn.Items[e.Index].ToString(), e.Font, myBrush, e.Bounds);
            //e.DrawFocusRectangle();
            lsbErrWarn.ResumeLayout();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 20; i++)
                this.lsbErrWarn.Items.Add(string.Empty);
            this.SequenceInit();
        }
        private bool SequenceInit()
        {
            this.Seq = new LaserSeq()
            {
                Editor = this.FormEditor.SiriusEditor,
                Viewer = this.FormAuto.SiriusViewer,
            };

            Seq.Editor.OnDocumentSourceChanged -= SiriusEditorForm_OnDocumentSourceChanged;
            Seq.Editor.OnDocumentSourceChanged += SiriusEditorForm_OnDocumentSourceChanged;
            return this.Seq.Initialize();
        }
        private void SiriusEditorForm_OnDocumentSourceChanged(object sender, IDocument doc)
        {
            // 변경된 문서 소스를 상대에게 통지하여 업데이트
            this.FormAuto.SiriusViewer.Document = doc;
            this.FormEditor.SiriusEditor.Document = doc;
        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            var mb = new MessageBoxYesNo();
            if (DialogResult.Yes != mb.ShowDialog("Warning !", "Do you really want to exit the program ?"))
            {
                e.Cancel = true;
                return;
            }
            Logger.Log(Logger.Type.Warn, $"program is terminating by the user");
            this.timer1.Enabled = false;
            this.Seq?.Dispose();
        }
        public void UpdateVersionInfo()
        {
            var name = Assembly.GetExecutingAssembly().GetName();
            this.lblVersion.Text = $"V{name.Version.ToString()}";
        }
        private void FormMain_Shown(object sender, EventArgs e)
        {
            this.SwitchForm(this.panBody, this.FormAuto);
            timer1.Tick += Timer1_Tick;
            timer1.Interval = 200;
            timer1.Enabled = true;
        }

        #region 마우스로 폼 드래그
        private System.Drawing.Point mouseDownLocation;
        private void panTop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.mouseDownLocation = e.Location;
            }
        }
        private void panTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.Left = e.X + this.Left - this.mouseDownLocation.X;
                this.Top = e.Y + this.Top - this.mouseDownLocation.Y;
            }
        }
        #endregion
        #region 폼 외곽에 Drop Shadow 효과
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
        #endregion
        #region 폼 화면 전환
        public void SwitchForm(Panel destination, Form target)
        {
            if (this.FormCurrent == target)
                return;

            destination.SuspendLayout();

            foreach (Form f in destination.Controls)
            {
                f.Visible = false;
                f.Hide();
            }
            destination.Controls.Clear();
            target.SuspendLayout();
            target.TopLevel = false;
            target.FormBorderStyle = FormBorderStyle.None;
            target.Dock = DockStyle.Fill;
            target.Visible = true;
            destination.Controls.Add(target);
            target.ResumeLayout();
            destination.ResumeLayout();
            lblMenu.Text = $"Menu: {target.Text}";
            this.FormCurrent = target;
            Logger.Log(Logger.Type.Debug, $"main screen switched to {target.Text}");
        } 
        #endregion
        private void btnAuto_Click(object sender, EventArgs e)
        {
            this.SwitchForm(this.panBody, this.FormAuto);            
        }
        private void btnLaser_Click(object sender, EventArgs e)
        {
            this.SwitchForm(this.panBody, this.FormEditor);
        }
        private void btnSetup_Click(object sender, EventArgs e)
        {
            this.SwitchForm(this.panBody, this.FormSetup);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnMaximize_Click(object sender, EventArgs e)
        {
            switch (this.WindowState)
            {
                case FormWindowState.Normal:
                    this.WindowState = FormWindowState.Maximized;
                    break;
                case FormWindowState.Maximized:
                    this.WindowState = FormWindowState.Normal;
                    break;
            }
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("H:mm:ss tt");
            lblRecipe.Text = $"Recipe : [{Seq.RecipeNo}] ";

            lsbErrWarn.SuspendLayout();
            int count = 0;
            lock (Seq.SyncRoot)
            {
                foreach (ErrEnum err in Seq.Errors)
                {
                    if (0 != err)
                    {
                        var name = AttrHelper.Description( err).Description;
                        var msg = $"ER:{(int)err:0000} {name}";
                        if (0 != string.Compare((string)this.lsbErrWarn.Items[count], msg))
                            this.lsbErrWarn.Items[count] = msg;
                        count++;
                    }
                }
                foreach (WarnEnum warn in Seq.Warns)
                {
                    if (0 != warn)
                    {
                        var name = AttrHelper.Description(warn).Description;
                        string msg = $"WR:{(int)warn:0000} {name}";
                        if (0 != string.Compare((string)this.lsbErrWarn.Items[count], msg))
                            this.lsbErrWarn.Items[count] = msg;
                        count++;
                    }
                }
                for (int i = count; i < 20; i++)
                    this.lsbErrWarn.Items[i] = string.Empty;
            }
            lsbErrWarn.ResumeLayout();
        }
        private void panTop_DoubleClick(object sender, EventArgs e)
        {
            switch (this.WindowState)
            {
                case FormWindowState.Normal:
                    this.WindowState = FormWindowState.Maximized;
                    break;
                case FormWindowState.Maximized:
                    this.WindowState = FormWindowState.Normal;
                    break;
            }
        }

        //public enum LogType { Info, Warn, Error};
        //public void Log(string message, LogType type = LogType.Info)
        //{
        //    switch(type)
        //    {
        //        case LogType.Info:
        //            this.lstBoxLog.Items.Add($"{DateTime.Now:HH:mm:ss}: " + message);
        //            break;
        //        case LogType.Warn:
        //            this.lstBoxLog.Items.Add($"{DateTime.Now:HH:mm:ss}: [WR] " + message);
        //            break;
        //        case LogType.Error:
        //            this.lstBoxLog.Items.Add($"{DateTime.Now:HH:mm:ss}: [ER] " + message);
        //            break;
        //    }
        //    while (lstBoxLog.Items.Count > 1000)
        //    {
        //        lstBoxLog.Items.RemoveAt(0);
        //    }
        //    int visibleItems = lstBoxLog.ClientSize.Height / lstBoxLog.ItemHeight;
        //    lstBoxLog.TopIndex = Math.Max(lstBoxLog.Items.Count - visibleItems + 1, 0);
        //}

        private void btnReset_Click(object sender, EventArgs e)
        {
            Seq.Reset();
        }
        private void btnAbort_Click(object sender, EventArgs e)
        {
            Seq.Stop();
        }
    }
}
