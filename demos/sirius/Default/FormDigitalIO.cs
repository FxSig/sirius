using System;
using System.Windows.Forms;

namespace SpiralLab.Sirius.Default
{
    public partial class FormDigitalIO : Form
    {
        Form formDInput;
        Form formDOutput;

        public FormDigitalIO()
        {
            InitializeComponent();
            
            this.formDInput = new FormDInput();
            this.formDOutput = new FormDOutput();

            this.SwitchForm(panLeft, formDInput);
            this.SwitchForm(panRight, formDOutput);
            this.VisibleChanged += FormDigitalIO_VisibleChanged1;
        }

        private void FormDigitalIO_VisibleChanged1(object sender, System.EventArgs e)
        {
            this.formDInput.Visible = this.Visible;
            this.formDOutput.Visible = this.Visible;
            if (this.Visible)
            {
            }
            else
            {
            }
        }


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
    }
}
