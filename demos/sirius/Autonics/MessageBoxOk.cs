using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralLab.Sirius.Autonics
{
    /// <summary>
    /// 대화상자 (Ok)
    /// </summary>
    public partial class MessageBoxOk : Form
    {
        /// <summary>
        /// 제목
        /// </summary>
        public string Title
        {
            get { return this.lblTitle.Text; }
            set { this.lblTitle.Text = value; }
        }
        /// <summary>
        /// 본문
        /// </summary>
        public string Message
        {
            get { return this.txtMessage.Text; }
            set { this.txtMessage.Text = value; }
        }
        /// <summary>
        /// 기본값 Color.Control
        /// </summary>
        public Color TitleBgColor
        {
            get { return this.panel1.BackColor; }
            set { this.panel1.BackColor = value; }
        }
        /// <summary>
        /// 기본값 Color.ControlText
        /// </summary>
        public Color TitleFgColor
        {
            get { return this.lblTitle.ForeColor; }
            set { this.lblTitle.ForeColor = value; }
        }

        int closedSecs;
        int secs;

        private bool isMouseDown;
        private System.Drawing.Point mouseDownLocation;

        /// <summary>
        /// 생성자
        /// </summary>
        public MessageBoxOk()
        {
            InitializeComponent();

            lblTitle.MouseDown += (o, e) => { if (e.Button == MouseButtons.Left) { isMouseDown = true; mouseDownLocation = e.Location; } };
            lblTitle.MouseMove += (o, e) => { if (isMouseDown) Location = new System.Drawing.Point(Location.X + (e.X - mouseDownLocation.X), Location.Y + (e.Y - mouseDownLocation.Y)); };
            lblTitle.MouseUp += (o, e) => { if (e.Button == MouseButtons.Left) { isMouseDown = false; mouseDownLocation = e.Location; } };
        }

        /// <summary>
        /// Drop Shadow (그림자) 효과
        /// </summary>
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

        /// <summary>
        /// 대화상자 출력 (Modal)
        /// </summary>
        /// <returns></returns>
        public new DialogResult ShowDialog()
        {
            return base.ShowDialog();
        }

        /// <summary>
        /// 대화상자 출력 (Modal)
        /// </summary>
        /// <param name="title">제목</param>
        /// <param name="message">본문</param>
        /// <param name="closedSecs">자동 닫기 시간 (초)</param>
        /// <returns></returns>
        public DialogResult ShowDialog(string title, string message, int closedSecs=0)
        {
            this.Title = title;
            this.Message = message;
            this.closedSecs = closedSecs;
            if (this.closedSecs > 0)
            {
                this.btnOk.Text = $"&Ok ({closedSecs})";
                this.secs = 0;
                var timer = new System.Windows.Forms.Timer();
                timer.Tick += Timer_Tick;
                timer.Interval = 1000;
                timer.Enabled = true;
            }
            else
            {
                this.btnOk.Text = $"&Ok";
            }

            return this.ShowDialog();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.secs++;
            this.btnOk.Text = $"&Ok ({this.closedSecs - this.secs})";
            if (this.secs >= this.closedSecs)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        #region 마우스로 폼 드래그
        //private Point mouseDownLocation;
        //private void lblTitle_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == System.Windows.Forms.MouseButtons.Left)
        //    {
        //        this.mouseDownLocation = e.Location;
        //    }
        //}
        //private void lblTitle_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == System.Windows.Forms.MouseButtons.Left)
        //    {
        //        this.Left = e.X + this.Left - this.mouseDownLocation.X;
        //        this.Top = e.Y + this.Top - this.mouseDownLocation.Y;
        //    }
        //}
        #endregion
    }
}
