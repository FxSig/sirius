using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SpiralLab.Sirius.Default
{
    public partial class FormMotor : Form
    {
        short axis = -1;
        short index = -1;
        int rowIndex;
        int columnIndex;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public FormMotor()
        {
            InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(Form_Closing);
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            this.timer?.Stop();
        }

        private void DgvAxes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            this.dgvIndexTable.EndEdit();
            this.axis = (short)e.RowIndex;
            this.index = 0;
            this.UpdateIndexTable();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.UpdateStatus();
        }

        private void UpdateStatus()
        {
       
        }
        private void DgvAxes_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
         
        }

        private void FormMotor_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.UpdateStatus();
                this.timer.Enabled = true;
            }
            else
                this.timer.Enabled = false;
        }

        private void UpdateIndexTable()
        {
            
        }

        private void DgvIndexTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            this.index = (short)e.RowIndex;
            rowIndex = e.RowIndex;
            columnIndex = e.ColumnIndex;         
        }

        private void DgvIndexTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void btnMotorHome_Click(object sender, EventArgs e)
        {
           
        }

        private void btnServo_Click(object sender, EventArgs e)
        {
          
        }

        private void btnMotorReset_Click(object sender, EventArgs e)
        {

        }

        private void btnMotorStop_Click(object sender, EventArgs e)
        {

        }

        private void btnMoveToIndex_Click(object sender, EventArgs e)
        {

        }

        private void btnCopyPosition_Click(object sender, EventArgs e)
        {
          
        }


        private void btnJog_Click(object sender, EventArgs e)
        {
          
        }

        private void btnWriteAll_Click(object sender, EventArgs e)
        {
            
        }
    }
}
