using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SpiralLab.Sirius.Default
{
    public partial class FormDInput : Form
    {
        public IRtc Rtc { get; set; }
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public FormDInput()
        {
            InitializeComponent();

            string iniFile = Config.ConfigRtcExtIoFileName;
            string[] dInputNames = new string[16];
            for (int i = 0; i < 16; i++)
                dInputNames[i] = NativeMethods.ReadIni<string>(iniFile, "DIN", $"{i}");

            this.dgvInput.SuspendLayout();
            this.dgvInput.RowCount = dInputNames.Length;
            for (int i = 0; i < dgvInput.RowCount; i++)
            {
                dgvInput.Rows[i].Cells[0].Value = i;
                dgvInput.Rows[i].Cells[1].Value = dInputNames[i];
            }
            this.dgvInput.ResumeLayout();

            this.dgvInput.CellPainting += DgvInput_CellPainting;
            this.VisibleChanged += FormDInput_VisibleChanged;
            timer.Interval = 100;
        }

        private void FormDInput_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.dgvInput.ClearSelection();
                this.UpdateStatus();
                this.timer.Enabled = true;
            }
            else
            {
                this.timer.Enabled = false;
                this.dgvInput.ClearSelection();
            }
        }

        private void UpdateStatus()
        {
            if (null == this.Rtc)
                return;
            this.Rtc.CtlReadData<uint>(ExtensionChannel.ExtDI16, out uint bits);
            for (int i = 0; i < dgvInput.RowCount; i++)
            {
                bool onOff;
                if (0x00 != (bits & (0x01 << i)))
                    onOff = true;
                else
                    onOff = false;
                this.dgvInput.Rows[i].Cells[2].Value = onOff ? "ON" : "OFF";
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.UpdateStatus();
        }

        private void DgvInput_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (2 != e.ColumnIndex)
                return;
            DataGridViewRow row = dgvInput.Rows[e.RowIndex];
            if (null == row.Cells[2].Value)
                return;
            if (row.Cells[2].Value.ToString() == "ON")
            {
                row.Cells[2].Style.BackColor = Color.Lime;
                row.Cells[2].Style.ForeColor = Color.Black;
            }
            else
            {
                row.Cells[2].Style.BackColor = Color.Green;
                row.Cells[2].Style.ForeColor = Color.White;
            }
        }

    }
}
