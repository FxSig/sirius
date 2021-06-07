using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace SpiralLab.Sirius.Default
{
    public partial class FormSetup : Form
    {
        
        public enum Part
        {
            [Description("디지털 입출력")]
            DigitalIO,

            [Description("모터 셋업")]
            Motors,
        }

        Form formMotors;
        Form formDIO;
        
        public FormSetup()
        {
            InitializeComponent();

            var descriptions = new DescriptionAttributes<Part>().Descriptions.ToList();
            this.cbbLeft.Items.AddRange(descriptions.ToArray());
            this.cbbRight.Items.AddRange(descriptions.ToArray());

            this.cbbLeft.SelectedIndexChanged += cbbLeft_SelectedIndexChanged;
            this.cbbRight.SelectedIndexChanged += cbbRight_SelectedIndexChanged;
            this.Load += FormSetup_Load;
            this.formMotors = new FormMotor();
            this.formDIO = new FormDigitalIO();
        }

        private void FormSetup_Load(object sender, EventArgs e)
        {
            cbbLeft.SelectedIndex = 0;
            cbbRight.SelectedIndex = 1;
        }
        #region 폼 화면 전환
        public void SwitchForm(Panel destination, Form target)
        {
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
        }
        private void SwitchPanelByPart(Panel panel, int index)
        {
            Part partEnum = (Part)index;
            switch (partEnum)
            {
                case Part.Motors:
                    this.SwitchForm(panel, formMotors);
                    break;
                case Part.DigitalIO:
                    this.SwitchForm(panel, formDIO);
                    break;
            }
        }
        #endregion
        private void cbbLeft_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != panRight.Tag)
                if ((int)panRight.Tag == cbbLeft.SelectedIndex)
                    cbbRight.SelectedIndex = (int)panLeft.Tag;
            this.SwitchPanelByPart(panLeft, cbbLeft.SelectedIndex);
            panLeft.Tag = cbbLeft.SelectedIndex;
        }

        private void cbbRight_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != panLeft.Tag)
                if ((int)panLeft.Tag == cbbRight.SelectedIndex)
                    cbbLeft.SelectedIndex = (int)panRight.Tag;
            this.SwitchPanelByPart(panRight, cbbRight.SelectedIndex);
            panRight.Tag = cbbRight.SelectedIndex;
        }
    }
}
