using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SpiralLab.Sirius.Nguyen
{
    public partial class FormDInput : Form
    {
        int counts;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public FormDInput()
        {
            InitializeComponent();
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
            dgvInput.SuspendLayout();
            DataGridViewRow row = dgvInput.Rows[e.RowIndex];
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
            dgvInput.ResumeLayout();
        }

    }
}
