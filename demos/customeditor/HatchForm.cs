using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    internal partial class HatchForm : Form
    {
        public HatchMode Mode { get; set; }
        public float Angle { get; set; }
        public float Interval { get; set; }
        public float Exclude { get; set; }

        public HatchForm()
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.KeyDown += HatchForm_KeyDown;

            cbbMode.DataSource = Enum.GetValues(typeof(HatchMode));
            this.txtInterval.Text = "0.1";
            this.Interval = 0.1f;
        }

        private void HatchForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void cbbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Enum.TryParse<HatchMode>(cbbMode.SelectedValue.ToString(), out HatchMode mode);
            this.Mode = mode;
        }

        private void txtAngle_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(txtAngle.Text, out float angle))
                this.Angle = angle;
        }

        private void txtInterval_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(txtInterval.Text, out float interval))
                this.Interval = interval;
        }

        private void txtExclude_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(txtExclude.Text, out float exclude))
                this.Exclude = exclude;
        }
    }
}
