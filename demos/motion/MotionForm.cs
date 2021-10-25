using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{

    public enum NoName
    { }

    public partial class MotionForm : Form
    {
        IDInput DigitalInput;
        IDOutput DigitalOutput;
        IMotor MotorZ;
        string[] dInputNames = new string[16];
        string[] dOutputNames = new string[16];

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public MotionForm()
        {
            InitializeComponent();

            this.dgvInput.CellPainting += DgvInput_CellPainting;
            this.dgvOutput.CellPainting += DgvOutput_CellPainting;
            this.dgvOutput.CellDoubleClick += DgvOutput_CellDoubleClick;
            this.Load += MotionForm_Load;
            this.FormClosed += MotionForm_FormClosed;

            timer.Interval = 100;
            timer.Tick += Timer_Tick;
        }

        private void MotionForm_Load(object sender, EventArgs e)
        {
            UpdateNames();

            var motorParameterFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "motion", "ajinmotorparameterfile.mot");
            MotorAjinExtek.LoadParameterFile(motorParameterFile);

            MotorZ = new MotorAjinExtek(0, "Z Axis");
            MotorZ.Initialize();

            DigitalInput = new AjinExtekDInput(0, "D.IN");
            //DigitalInput = new AdlinkDInput(0, "D.IN", 7230, 0);
            //DigitalInput = new RtcDInput(rtc, 0, "D.IN");
            DigitalInput.Initialize();

            DigitalOutput = new AjinExtekDOutput(0, "D.OUT");
            //DigitalOutput = new AdlinkDOutput(0, "D.OUT", 7230, 0);
            //DigitalOutput = new RtcDOutput(rtc, 0, "D.OUT");
            DigitalOutput.Initialize();

            timer.Enabled = true;
        }

        private void MotionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer.Enabled = false;
            MotorZ.Dispose();
        }

        private void UpdateNames()
        {
            string iniFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", "handlerio.ini");
            dInputNames = new string[16];
            dOutputNames = new string[16];
            for (int i = 0; i < 16; i++)
            {
                dInputNames[i] = NativeMethods.ReadIni<string>(iniFile, "DIN", $"{i}");
                dOutputNames[i] = NativeMethods.ReadIni<string>(iniFile, "DOUT", $"{i}");
            }

            this.dgvInput.SuspendLayout();
            this.dgvInput.RowCount = 16;
            for (int i = 0; i < dgvInput.RowCount; i++)
            {
                dgvInput.Rows[i].Cells[0].Value = i;
                dgvInput.Rows[i].Cells[1].Value = dInputNames[i];
            }
            this.dgvInput.ResumeLayout();

            this.dgvOutput.SuspendLayout();
            this.dgvOutput.RowCount = 16;
            for (int i = 0; i < dgvOutput.RowCount; i++)
            {
                dgvOutput.Rows[i].Cells[0].Value = i;
                dgvOutput.Rows[i].Cells[1].Value = dOutputNames[i];
            }
            this.dgvOutput.ResumeLayout();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.UpdateStatus();
        }
        private void UpdateStatus()
        {
            {
                DigitalInput.Update();
                DigitalInput.GetChannel(0, out ushort bits);
                dgvInput.SuspendLayout();
                for (int i = 0; i < dgvInput.RowCount; i++)
                {
                    bool onOff;
                    if (0x00 != (bits & (0x01 << i)))
                        onOff = true;
                    else
                        onOff = false;
                    this.dgvInput.Rows[i].Cells[2].Value = onOff ? "ON" : "OFF";
                }
                dgvInput.ResumeLayout();
            }
            {

                DigitalOutput.GetChannel(0, out ushort bits);
                dgvOutput.SuspendLayout();
                for (int i = 0; i < dgvOutput.RowCount; i++)
                {
                    bool onOff;
                    if (0x00 != (bits & (0x01 << i)))
                        onOff = true;
                    else
                        onOff = false;
                    this.dgvOutput.Rows[i].Cells[2].Value = onOff ? "ON" : "OFF";
                }
                dgvOutput.ResumeLayout();
            }

            {
                MotorZ.Update();

                if (MotorZ.IsServoOn)
                    lblServo.BackColor = Color.Lime;
                else
                    lblServo.BackColor = Color.Green;
                if (MotorZ.IsHomeSearched)
                    lblHome.BackColor = Color.Lime;
                else
                    lblHome.BackColor = Color.Green;
                if (MotorZ.IsServoAlarm)
                    lblAlarm.BackColor = Color.Red;
                else
                    lblAlarm.BackColor = Color.Maroon;
                if (MotorZ.IsDriving)
                    lblDriving.BackColor = Color.Red;
                else
                    lblDriving.BackColor = Color.Maroon;
                if (MotorZ.IsCCwSenOn)
                    lblNeg.BackColor = Color.Red;
                else
                    lblNeg.BackColor = Color.Maroon;
                if (MotorZ.IsCwSenOn)
                    lblPos.BackColor = Color.Red;
                else
                    lblPos.BackColor = Color.Maroon;
                if (MotorZ.IsOrgSenOn)
                    lblORG.BackColor = Color.Lime;
                else
                    lblORG.BackColor = Color.Green;
            }
        }

        private void DgvInput_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (2 != e.ColumnIndex)
                return;
            dgvInput.SuspendLayout();
            DataGridViewRow row = dgvInput.Rows[e.RowIndex];
            if (row.Cells[2].Value?.ToString() == "ON")
            {
                row.Cells[2].Style.BackColor = Color.Lime;
                row.Cells[2].Style.ForeColor = Color.White;
            }
            else
            {
                row.Cells[2].Style.BackColor = Color.Green;
                row.Cells[2].Style.ForeColor = Color.White;
            }
            dgvInput.ResumeLayout();
        }
        private void DgvOutput_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (2 != e.ColumnIndex)
                return;
            dgvOutput.SuspendLayout();
            DataGridViewRow row = dgvOutput.Rows[e.RowIndex];
            if (row.Cells[2].Value?.ToString() == "ON")
            {
                row.Cells[2].Style.BackColor = Color.Red;
                row.Cells[2].Style.ForeColor = Color.White;
            }
            else
            {
                row.Cells[2].Style.BackColor = Color.Maroon;
                row.Cells[2].Style.ForeColor = Color.White;
            }
            dgvOutput.ResumeLayout();
        }

        private void DgvOutput_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (2 != e.ColumnIndex)
                return;
            uint bitPosition = uint.Parse(dgvOutput.Rows[e.RowIndex].Cells[0].Value.ToString());
            var ioName = dgvOutput.Rows[e.RowIndex].Cells[1].Value;
            bool onOff = dgvOutput.Rows[e.RowIndex].Cells[2].Value.Equals("ON");
            if (onOff)
                DigitalOutput.OutOff(bitPosition);
            else
                DigitalOutput.OutOn(bitPosition);
            DigitalOutput.Update();
            dgvOutput.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "ajinextek motor parameter data files (*.dat)|*.dat|All Files (*.*)|*.*";
            dlg.Title = "Open to motor parameter file";
            dlg.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "motion");
            DialogResult result = dlg.ShowDialog();
            if (result != DialogResult.OK)
                return;
            if (MotorAjinExtek.LoadParameterFile(dlg.FileName))
                textBox1.Text = dlg.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var pos = float.Parse(textBox2.Text);
            var vel = float.Parse(textBox3.Text);
            MotorZ.CtlMoveAbs(pos, vel);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MotorZ.CtlHomeSearch();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MotorZ.CtlMoveStop();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MotorZ.CtlReset();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MotorZ.CtlServo(true);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MotorZ.CtlServo(false);
        }

        private void button8_MouseDown(object sender, MouseEventArgs e)
        {
            var vel = float.Parse(textBox3.Text);
            MotorZ.CtlMoveJog(-Math.Abs(vel));
        }

        private void button8_MouseUp(object sender, MouseEventArgs e)
        {
            MotorZ.CtlMoveStop();
        }

        private void button9_MouseDown(object sender, MouseEventArgs e)
        {
            var vel = float.Parse(textBox3.Text);
            MotorZ.CtlMoveJog(Math.Abs(vel));
        }

        private void button9_MouseUp(object sender, MouseEventArgs e)
        {
            MotorZ.CtlMoveStop();
        }
    }
}
