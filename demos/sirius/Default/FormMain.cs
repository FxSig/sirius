using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace SpiralLab.Sirius.Default
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// 현재 활성 폼
        /// </summary>
        public Form FormCurrent { get; private set; }
        /// <summary>
        /// 오토 화면 (뷰어 + a) 
        /// </summary>
        public FormAuto FormAuto { get; private set; }
        /// <summary>
        /// 레시피 화면
        /// </summary>
        public FormRecipe FormRecipe { get; private set; }
        /// <summary>
        /// 레이저(편집기)화면
        /// </summary>
        public FormLaser FormLaser { get; private set; }
        /// <summary>
        /// 셋업 화면
        /// </summary>
        public FormSetup FormSetup { get; private set; }
        /// <summary>
        /// 이력 조회 화면
        /// </summary>
        public FormHistory FormHistory { get; private set; }

        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();

        public FormMain()
        {
            InitializeComponent();

            this.FormAuto = new FormAuto();
            this.FormRecipe = new FormRecipe();
            this.FormLaser = new FormLaser();
            this.FormSetup = new FormSetup();
            this.FormHistory = new FormHistory();

            this.Load += FormMain_Load;
            this.Shown += FormMain_Shown;
            this.FormClosing += FormMain_FormClosing;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.SiriusLibInit();
            this.SwitchForm(this.panBody, this.FormAuto);
        }

        /// <summary>
        /// 외부 설정 파일(ini)에 의해 시리우스 라이브러리 초기화
        /// </summary>
        /// <returns></returns>
        private bool SiriusLibInit()
        {
            #region 시리우스 문서 생성
            var doc = new Sirius.DocumentDefault();
            this.FormAuto.SiriusViewer.Document = doc;
            this.FormLaser.SiriusEditor.Document = doc;
            // 소스 문서(IDocument) 가 변경될경우 다른 멀티 뷰에 이를 통지하는 이벤트 핸들러 등록
            this.FormLaser.SiriusEditor.OnDocumentSourceChanged += SiriusEditorForm_OnDocumentSourceChanged; 
            #endregion

            bool success = true;
            #region RTC 초기화
            uint i = 0;
            //var rtcCounts = NativeMethods.ReadIni<int>(Program.ConfigFileName, "RTC", "COUNTS");
            //for (uint i = 0; i < rtcCounts; i++)
            //{
                var rtcTypeName = NativeMethods.ReadIni<string>(Program.ConfigFileName, $"RTC{i}", "TYPE");
                Type rtcType = Type.GetType(rtcTypeName.Trim());
                if (null == rtcType)
                {
                    var mb = new Default.MessageBoxOk();
                    mb.ShowDialog("Critical", $"Can't create rtc instance : {rtcType.ToString()} at {Program.ConfigFileName}");
                    success &= false;
                }
                var rtc = Activator.CreateInstance(rtcType) as IRtc;
                var rtcName = NativeMethods.ReadIni<string>(Program.ConfigFileName, $"RTC{i}", "NAME");
                rtc.Name = rtcName;
                rtc.Index = i;
                var kFactor = NativeMethods.ReadIni<float>(Program.ConfigFileName, $"RTC{i}", "KFACTOR");
                var ct5FileName = NativeMethods.ReadIni<string>(Program.ConfigFileName, $"RTC{i}", "CORRECTION");
                var laserModeTypeName = NativeMethods.ReadIni<string>(Program.ConfigFileName, $"RTC{i}", "LASERMODE");
                LaserMode laserMode = (LaserMode)Enum.Parse(typeof(LaserMode), laserModeTypeName.Trim());
                success &= rtc.Initialize(kFactor, laserMode, ct5FileName);  
                rtc.CtlFrequency(50 * 1000, 2); // laser frequency : 50KHz, pulse width : 2usec
                rtc.CtlSpeed(100, 100); // default jump and mark speed : 100mm/s
                rtc.CtlDelay(10, 100, 200, 200, 0); // scanner and laser delays
                
                this.FormLaser.SiriusEditor.Rtc = rtc;
            //}
            #endregion

            #region 레이저 소스 초기화
            //var laserCounts = NativeMethods.ReadIni<int>(Program.ConfigFileName, "LASER", "COUNTS");
            //for (uint i = 0; i < laserCounts; i++)
            //{
                var laserTypeName = NativeMethods.ReadIni<string>(Program.ConfigFileName, $"LASER{i}", "TYPE");
                Type laserType = Type.GetType(laserTypeName.Trim());
                if (null == laserType)
                {
                    var mb = new Default.MessageBoxOk();
                    mb.ShowDialog("Critical", $"Can't create laser instance : {laserType.ToString()} at {Program.ConfigFileName}");
                    success &= false;
                }
                var laser = Activator.CreateInstance(laserType) as ILaser;
                //ILaser laser = new Sirius.LaserVirtual(0, "virtual", 20.0f);
                var laserName = NativeMethods.ReadIni<string>(Program.ConfigFileName, $"LASER{i}", "NAME");
                laser.Index = i;
                laser.Name = laserName;
                var maxPower = NativeMethods.ReadIni<float>(Program.ConfigFileName, $"LASER{i}", "MAXPOWER");
                laser.MaxPowerWatt = maxPower;
                success &= laser.Initialize();
                success &= laser.CtlPower(rtc, 10);

                this.FormLaser.SiriusEditor.Laser = laser;
            //}
            this.FormLaser.SiriusEditor.Laser = laser;
            #endregion

            #region 마커 지정
            var marker = new MarkerDefault(0);
            this.FormLaser.SiriusEditor.Marker = marker;
            #endregion
            return success;
        }
        private void SiriusEditorForm_OnDocumentSourceChanged(object sender, IDocument doc)
        {
            // 변경된 문서 소스를 상대에게 통지하여 업데이트
            this.FormAuto.SiriusViewer.Document = doc;
            this.FormLaser.SiriusEditor.Document = doc;
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
            timer1.Interval = 1000;
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
        private void btnRecipe_Click(object sender, EventArgs e)
        {
            this.SwitchForm(this.panBody, this.FormRecipe);
        }
        private void btnLaser_Click(object sender, EventArgs e)
        {
            this.SwitchForm(this.panBody, this.FormLaser);
        }
        private void btnSetup_Click(object sender, EventArgs e)
        {
            this.SwitchForm(this.panBody, this.FormSetup);
        }
        private void btnHistory_Click(object sender, EventArgs e)
        {
            this.SwitchForm(this.panBody, this.FormHistory);
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
            Logger.Log(Logger.Type.Warn, $"program is terminating by the user");
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
