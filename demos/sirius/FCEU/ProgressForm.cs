using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralLab.Sirius.FCEU
{
    public partial class ProgressForm : Form
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
        /// 0~100
        /// </summary>
        public float Percentage
        {
            get
            {
                return this.progressBar1.Value;
            }
            set
            {
                this.progressBar1.Value = (int)value;
            }
        }

        public ProgressForm()
        {
            InitializeComponent();

            this.Load += ProgressForm_Load;
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            this.CenterToParent();
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
    }
}
