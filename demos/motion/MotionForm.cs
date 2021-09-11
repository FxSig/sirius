﻿using System;
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

    public enum Dummy
    { }

    public partial class MotionForm : Form
    {
        IDInput<Dummy> DigitalInput;
        IDOutput<Dummy> DigitalOutput;
        IMotor MotorZ;

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public MotionForm()
        {
            InitializeComponent();

            this.dgvInput.SuspendLayout();
            this.dgvInput.RowCount = 16;
            for (int i = 0; i < dgvInput.RowCount; i++)
            {
                dgvInput.Rows[i].Cells[0].Value = i;
                //dgvInput.Rows[i].Cells[1].Value = 
            }
            this.dgvInput.ResumeLayout();

            this.dgvOutput.SuspendLayout();
            this.dgvOutput.RowCount = 16;
            for (int i = 0; i < dgvOutput.RowCount; i++)
            {
                dgvOutput.Rows[i].Cells[0].Value = i;
                //dgvOutput.Rows[i].Cells[1].Value = 
            }
            this.dgvOutput.ResumeLayout();

            this.Load += MotionForm_Load;
            this.dgvInput.CellPainting += DgvInput_CellPainting;
            this.dgvOutput.CellPainting += DgvOutput_CellPainting;
            this.dgvOutput.CellDoubleClick += DgvOutput_CellDoubleClick;

            timer.Interval = 100;
            timer.Tick += Timer_Tick;
        }

        private void MotionForm_Load(object sender, EventArgs e)
        {

            DigitalInput = new AjinExtekDInput<Dummy>(0, "D.IN");
            DigitalInput.Initialize();

            DigitalOutput = new AjinExtekDOutput<Dummy>(0, "D.OUT");
            DigitalOutput.Initialize();

            MotorZ = new MotorAjinExtek(0, "Z Axis");
            MotorZ.Initialize();

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
                DigitalOutput.Update();
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

                //if (MotorZ.IsNegLimit)
                //    lblNeg.BackColor = Color.Red;
                //else
                //    lblNeg.BackColor = Color.Maroon;

                //if (MotorZ.IsPosLimit)
                //    lblPos.BackColor = Color.Red;
                //else
                //    lblPos.BackColor = Color.Maroon;
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
            if (row.Cells[2].Value.ToString() == "ON")
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
            if (row.Cells[2].Value.ToString() == "ON")
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
            dgvOutput.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "ajinextek motor parameter data files (*.dat)|*.dat|All Files (*.*)|*.*";
            dlg.Title = "Open to motor parameter file";
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
            var acc = float.Parse(textBox4.Text);
            MotorZ.CtlMoveAbs(pos, vel, acc);
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
    }
}
