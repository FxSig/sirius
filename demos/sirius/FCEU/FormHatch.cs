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
    public partial class FormHatch : Form
    {
        public bool IsHatchable
        { 
            get { return this.chbEnable.Checked; }
            set { 
                this.chbEnable.Checked = value;
                groupBox1.Enabled = value;
            } 
        }
        public float Interval
        { 
            get { return float.Parse(txtInterval.Text); }
            set { txtInterval.Text = $"{value:F3}"; } 
        }
        public float Exclude
        {
            get { return float.Parse(txtExclude.Text); }
            set { txtExclude.Text = $"{value:F3}"; }
        }
        public float Angle1
        {
            get { return float.Parse(txtAngle1.Text); }
            set { txtAngle1.Text = $"{value:F3}"; }
        }
        public float Angle2
        {
            get { return float.Parse(txtAngle2.Text); }
            set { txtAngle2.Text = $"{value:F3}"; }
        }
        public uint Repeat
        {
            get { return uint.Parse(nudRepeat.Value.ToString()); }
            set { nudRepeat.Value = value; }
        }
        public SpiralLab.Sirius.HatchMode Mode
        { 
            get
            {
                if (rdbSingle.Checked)
                    return SpiralLab.Sirius.HatchMode.Line;
                else if (rdbCross.Checked)
                    return SpiralLab.Sirius.HatchMode.CrossLine;
                else
                    return SpiralLab.Sirius.HatchMode.Line;
            }
            set
            {
                switch(value)
                {
                    case SpiralLab.Sirius.HatchMode.Line:
                        rdbSingle.Checked = true;
                        rdbCross.Checked = false;
                        txtAngle1.Enabled = true;
                        txtAngle2.Enabled = false;
                        break;
                    case SpiralLab.Sirius.HatchMode.CrossLine:
                        rdbSingle.Checked = false;
                        rdbCross.Checked = true;
                        txtAngle1.Enabled = true;
                        txtAngle2.Enabled = true;
                        break;
                }    
            }
        }

        private bool isMouseDown;
        private System.Drawing.Point mouseDownLocation;

        public FormHatch()
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

        private void rdbSingle_Click(object sender, EventArgs e)
        {
            txtAngle1.Enabled = true;
            txtAngle2.Enabled = false;
        }

        private void rdbCross_Click(object sender, EventArgs e)
        {
            txtAngle1.Enabled = true;
            txtAngle2.Enabled = true;
        }
    }
}
