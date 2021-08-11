
namespace SpiralLab.Sirius.FCEU
{
    partial class FormHatch
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nudRepeat = new System.Windows.Forms.NumericUpDown();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.rdbCross = new System.Windows.Forms.RadioButton();
            this.rdbSingle = new System.Windows.Forms.RadioButton();
            this.txtAngle2 = new System.Windows.Forms.TextBox();
            this.txtAngle1 = new System.Windows.Forms.TextBox();
            this.txtExclude = new System.Windows.Forms.TextBox();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.chbEnable = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRepeat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.nudRepeat);
            this.groupBox1.Controls.Add(this.pictureBox3);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.rdbCross);
            this.groupBox1.Controls.Add(this.rdbSingle);
            this.groupBox1.Controls.Add(this.txtAngle2);
            this.groupBox1.Controls.Add(this.txtAngle1);
            this.groupBox1.Controls.Add(this.txtExclude);
            this.groupBox1.Controls.Add(this.txtInterval);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Location = new System.Drawing.Point(8, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(596, 306);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(473, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 16);
            this.label1.TabIndex = 16;
            this.label1.Text = "Repeat :";
            // 
            // nudRepeat
            // 
            this.nudRepeat.Location = new System.Drawing.Point(476, 176);
            this.nudRepeat.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudRepeat.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRepeat.Name = "nudRepeat";
            this.nudRepeat.Size = new System.Drawing.Size(95, 22);
            this.nudRepeat.TabIndex = 15;
            this.nudRepeat.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::SpiralLab.Sirius.Demo.Properties.Resources.cross;
            this.pictureBox3.Location = new System.Drawing.Point(515, 21);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(24, 24);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 13;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::SpiralLab.Sirius.Demo.Properties.Resources.single;
            this.pictureBox2.Location = new System.Drawing.Point(389, 21);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 24);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 12;
            this.pictureBox2.TabStop = false;
            // 
            // rdbCross
            // 
            this.rdbCross.AutoSize = true;
            this.rdbCross.Location = new System.Drawing.Point(476, 51);
            this.rdbCross.Name = "rdbCross";
            this.rdbCross.Size = new System.Drawing.Size(95, 20);
            this.rdbCross.TabIndex = 11;
            this.rdbCross.Text = "Cross Lines";
            this.rdbCross.UseVisualStyleBackColor = true;
            this.rdbCross.Click += new System.EventHandler(this.rdbCross_Click);
            // 
            // rdbSingle
            // 
            this.rdbSingle.AutoSize = true;
            this.rdbSingle.Checked = true;
            this.rdbSingle.Location = new System.Drawing.Point(356, 51);
            this.rdbSingle.Name = "rdbSingle";
            this.rdbSingle.Size = new System.Drawing.Size(90, 20);
            this.rdbSingle.TabIndex = 10;
            this.rdbSingle.TabStop = true;
            this.rdbSingle.Text = "Single Line";
            this.rdbSingle.UseVisualStyleBackColor = true;
            this.rdbSingle.Click += new System.EventHandler(this.rdbSingle_Click);
            // 
            // txtAngle2
            // 
            this.txtAngle2.Location = new System.Drawing.Point(320, 225);
            this.txtAngle2.Name = "txtAngle2";
            this.txtAngle2.Size = new System.Drawing.Size(100, 22);
            this.txtAngle2.TabIndex = 8;
            this.txtAngle2.Text = "0.000";
            // 
            // txtAngle1
            // 
            this.txtAngle1.Location = new System.Drawing.Point(320, 176);
            this.txtAngle1.Name = "txtAngle1";
            this.txtAngle1.Size = new System.Drawing.Size(100, 22);
            this.txtAngle1.TabIndex = 7;
            this.txtAngle1.Text = "0.000";
            // 
            // txtExclude
            // 
            this.txtExclude.Location = new System.Drawing.Point(130, 255);
            this.txtExclude.Name = "txtExclude";
            this.txtExclude.Size = new System.Drawing.Size(100, 22);
            this.txtExclude.TabIndex = 5;
            this.txtExclude.Text = "0.000";
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(130, 113);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(100, 22);
            this.txtInterval.TabIndex = 4;
            this.txtInterval.Text = "0.000";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SpiralLab.Sirius.Demo.Properties.Resources.hatch;
            this.pictureBox1.Location = new System.Drawing.Point(17, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(313, 267);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.Control;
            this.lblTitle.Location = new System.Drawing.Point(4, 6);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(592, 19);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "Hatch Options";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkSlateGray;
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Location = new System.Drawing.Point(8, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(596, 31);
            this.panel1.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(394, 356);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 32);
            this.button1.TabIndex = 12;
            this.button1.Text = "&Okay";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(504, 356);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 32);
            this.button2.TabIndex = 13;
            this.button2.Text = "&Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // chbEnable
            // 
            this.chbEnable.AutoSize = true;
            this.chbEnable.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbEnable.Location = new System.Drawing.Point(25, 363);
            this.chbEnable.Name = "chbEnable";
            this.chbEnable.Size = new System.Drawing.Size(111, 20);
            this.chbEnable.TabIndex = 15;
            this.chbEnable.Text = "Enable Hatch";
            this.chbEnable.UseVisualStyleBackColor = true;
            // 
            // FormHatch
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 395);
            this.Controls.Add(this.chbEnable);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormHatch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hatch";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRepeat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton rdbCross;
        private System.Windows.Forms.RadioButton rdbSingle;
        private System.Windows.Forms.TextBox txtAngle2;
        private System.Windows.Forms.TextBox txtAngle1;
        private System.Windows.Forms.TextBox txtExclude;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudRepeat;
        private System.Windows.Forms.CheckBox chbEnable;
    }
}