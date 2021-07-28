using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SpiralLab.Sirius.FCEU
{
    public partial class FormDOutput : Form
    {
        public IRtc Rtc { get; set; }

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public FormDOutput()
        {
            InitializeComponent();
            string iniFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", "extio.ini");
            string[] dOutputNames = new string[16];
            for (int i = 0; i < 16; i++)
                dOutputNames[i] = NativeMethods.ReadIni<string>(iniFile, "DOUT", $"{i}");
            this.dgvOutput.SuspendLayout();
            this.dgvOutput.RowCount = dOutputNames.Length;
            for (int i = 0; i < dgvOutput.RowCount; i++)
            {
                dgvOutput.Rows[i].Cells[0].Value = i;
                dgvOutput.Rows[i].Cells[1].Value = dOutputNames[i];
            }
            this.dgvOutput.ResumeLayout();
            this.dgvOutput.CellPainting += DgvOutput_CellPainting;
            this.dgvOutput.CellDoubleClick += DgvOutput_CellDoubleClick;
            this.VisibleChanged += FormDOutput_VisibleChanged;
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
        }

        private void FormDOutput_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.dgvOutput.ClearSelection();
                this.UpdateStatus();
                this.timer.Enabled = true;
            }
            else
            {
                this.timer.Enabled = false;
                this.dgvOutput.ClearSelection();
            }
        }

        private void UpdateStatus()
        {
            if (null == this.Rtc)
                return;
            dgvOutput.ClearSelection();
            this.Rtc.CtlReadData<uint>(ExtensionChannel.ExtDO16, out uint bits);
            for (int i = 0; i < dgvOutput.RowCount; i++)
            {
                bool onOff;
                if (0x00 != (bits & (0x01 << i)))
                    onOff = true;
                else
                    onOff = false;
                this.dgvOutput.Rows[i].Cells[2].Value = onOff ? "ON" : "OFF";
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.UpdateStatus();
        }

        private void DgvOutput_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (2 != e.ColumnIndex)
                return;
            DataGridViewRow row = dgvOutput.Rows[e.RowIndex];
            if (null == row.Cells[2].Value)
                return;
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
        }
   
        private void DgvOutput_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (2 != e.ColumnIndex)
                return;
            if (null == dgvOutput.Rows[e.RowIndex].Cells[2].Value)
                return;
            int bitPosition = int.Parse(dgvOutput.Rows[e.RowIndex].Cells[0].Value.ToString());
            var ioName = dgvOutput.Rows[e.RowIndex].Cells[1].Value;
            bool onOff = dgvOutput.Rows[e.RowIndex].Cells[2].Value.Equals("ON");
            this.Rtc?.CtlWriteExtDO16((ushort)bitPosition, !onOff);
            dgvOutput.ClearSelection();
        }

    }
}
