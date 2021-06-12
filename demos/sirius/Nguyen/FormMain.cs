using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace SpiralLab.Sirius.Nguyen
{
    public partial class FormMain : Form
    {
        public static string ConfigFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", "nguyen.ini");
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
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.SiriusLibInit();
            this.SwitchForm(this.panBody, this.FormAuto);
        }
        private bool SiriusLibInit()
        {
            //create default document
            var doc = new Sirius.DocumentDefault();
            this.FormAuto.SiriusViewer.Document = doc;
            this.FormEditor.SiriusEditor.Document = doc;
            // document changed event handler
            this.FormEditor.SiriusEditor.OnDocumentSourceChanged += SiriusEditorForm_OnDocumentSourceChanged;

            bool success = true;
            #region create/init RTC card
            IRtc rtc = null;
            var rtcTypeName = NativeMethods.ReadIni<string>(ConfigFileName, $"RTC", "TYPE");
            switch (rtcTypeName)
            {
                case "Rtc5":
                    rtc = new Rtc5(0);
                    rtc.Index = 0;
                    rtc.Name = NativeMethods.ReadIni<string>(ConfigFileName, $"RTC", "NAME");
                    break;
            }
            if (null == rtc)
            {
                var mb = new Default.MessageBoxOk();
                mb.ShowDialog("Critical", $"Can't create rtc instance : {rtcTypeName} at {ConfigFileName}");
                return false;
            }
            var kFactor = NativeMethods.ReadIni<float>(ConfigFileName, $"RTC", "KFACTOR");
            var fileName = NativeMethods.ReadIni<string>(ConfigFileName, $"RTC", "CORRECTION");
            var ct5FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", fileName);
            success &= rtc.Initialize(kFactor, LaserMode.Yag1, ct5FileName);
            rtc.CtlFrequency(50 * 1000, 2); // 50KHz,  2usec
            rtc.CtlSpeed(100, 100); // 100mm/s
            rtc.CtlDelay(10, 100, 200, 200, 0); // 
            this.FormEditor.SiriusEditor.Rtc = rtc;
            #endregion

            #region create/init IPG laser source
            ILaser laser = null;
            var laserTypeName = NativeMethods.ReadIni<string>(ConfigFileName, $"LASER", "TYPE");
            switch (laserTypeName)
            {
                case "IPGYLPTypeE":
                    laser = new IPGYLPTypeE();
                    laser.Index = 0;
                    laser.Name = NativeMethods.ReadIni<string>(ConfigFileName, $"LASER", "NAME");
                    break;
            }
            if (null == laser)
            {
                var mb = new Default.MessageBoxOk();
                mb.ShowDialog("Critical", $"Can't create laser instance : {laserTypeName} at {ConfigFileName}");
                return false;
            }
            laser.Rtc = rtc;
            var maxPower = NativeMethods.ReadIni<float>(ConfigFileName, $"LASER", "MAXPOWER");
            laser.MaxPowerWatt = maxPower;
            success &= laser.Initialize();
            var defaultPower = NativeMethods.ReadIni<float>(ConfigFileName, $"LASER", "DEFAULTPOWER");
            success &= laser.CtlPower(defaultPower);
            this.FormEditor.SiriusEditor.Laser = laser;
            #endregion

            #region 마커 초기화
            var marker = new MarkerDefault(0);
            this.FormEditor.SiriusEditor.Marker = marker;
            #endregion
            return success;
        }
        private void SiriusEditorForm_OnDocumentSourceChanged(object sender, IDocument doc)
        {
            // 변경된 문서 소스를 상대에게 통지하여 업데이트
            this.FormAuto.SiriusViewer.Document = doc;
            this.FormEditor.SiriusEditor.Document = doc;
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
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            var mb = new MessageBoxYesNo();
            if (DialogResult.Yes != mb.ShowDialog("Warning !", "Do you really want to exit the program ?"))
            {
                e.Cancel = true;
                return;
            }
            this.timer1.Enabled = false;
            Logger.Log(Logger.Type.Warn, $"mmi is terminating by the user");
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
    }
}
