using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SpiralLab.Sirius.Default
{
    public partial class FormDOutput : Form
    {
        int counts;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public FormDOutput()
        {
            InitializeComponent();
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
            dgvOutput.SuspendLayout();
            DataGridViewRow row = dgvOutput.Rows[e.RowIndex];
            if (row.Cells[2].Value.ToString() == "ON")
            {
                row.Cells[2].Style.BackColor = Color.Red;
                row.Cells[2].Style.ForeColor = Color.Black;
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
            
        }

    }
}
