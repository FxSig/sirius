using System;
using System.Windows.Forms;

namespace SpiralLab.Sirius.FCEU
{
    public partial class FormDigitalIO : Form
    {
        FormDInput formDInput;
        FormDOutput formDOutput;

        public FormDigitalIO()
        {
            InitializeComponent();
            
            this.formDInput = new FormDInput();
            this.formDOutput = new FormDOutput();

            this.SwitchForm(panLeft, formDInput);
            this.SwitchForm(panRight, formDOutput);
            this.VisibleChanged += FormDigitalIO_VisibleChanged1;
            this.Load += FormDigitalIO_Load;
        }

        private void FormDigitalIO_Load(object sender, EventArgs e)
        {
            var formMain = Program.MainForm as FormMain;
            this.formDInput.Rtc = formMain.FormEditor.SiriusEditor.Rtc;
            this.formDOutput.Rtc = formMain.FormEditor.SiriusEditor.Rtc;
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
