
namespace SpiralLab.Sirius
{
    partial class CorrectionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CorrectionForm));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn2DCtb = new System.Windows.Forms.Button();
            this.btn3DCtb = new System.Windows.Forms.Button();
            this.btn2DCt5 = new System.Windows.Forms.Button();
            this.btn3DCt5 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn2DCtb);
            this.flowLayoutPanel1.Controls.Add(this.btn3DCtb);
            this.flowLayoutPanel1.Controls.Add(this.btn2DCt5);
            this.flowLayoutPanel1.Controls.Add(this.btn3DCt5);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(46, 19);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(10);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(271, 169);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btn2DCtb
            // 
            this.btn2DCtb.Location = new System.Drawing.Point(3, 3);
            this.btn2DCtb.Name = "btn2DCtb";
            this.btn2DCtb.Size = new System.Drawing.Size(250, 32);
            this.btn2DCtb.TabIndex = 0;
            this.btn2DCtb.Text = "2D Field Correction (.ctb)";
            this.btn2DCtb.UseVisualStyleBackColor = true;
            this.btn2DCtb.Click += new System.EventHandler(this.btn2DCtb_Click);
            // 
            // btn3DCtb
            // 
            this.btn3DCtb.Location = new System.Drawing.Point(3, 41);
            this.btn3DCtb.Name = "btn3DCtb";
            this.btn3DCtb.Size = new System.Drawing.Size(250, 32);
            this.btn3DCtb.TabIndex = 1;
            this.btn3DCtb.Text = "3D Field Correction (.ctb)";
            this.btn3DCtb.UseVisualStyleBackColor = true;
            this.btn3DCtb.Click += new System.EventHandler(this.btn3DCtb_Click);
            // 
            // btn2DCt5
            // 
            this.btn2DCt5.Location = new System.Drawing.Point(3, 79);
            this.btn2DCt5.Name = "btn2DCt5";
            this.btn2DCt5.Size = new System.Drawing.Size(250, 32);
            this.btn2DCt5.TabIndex = 2;
            this.btn2DCt5.Text = "2D Field Correction (.ct5)";
            this.btn2DCt5.UseVisualStyleBackColor = true;
            this.btn2DCt5.Click += new System.EventHandler(this.btn2DCt5_Click);
            // 
            // btn3DCt5
            // 
            this.btn3DCt5.Location = new System.Drawing.Point(3, 117);
            this.btn3DCt5.Name = "btn3DCt5";
            this.btn3DCt5.Size = new System.Drawing.Size(250, 32);
            this.btn3DCt5.TabIndex = 3;
            this.btn3DCt5.Text = "3D Field Correction (.ct5)";
            this.btn3DCt5.UseVisualStyleBackColor = true;
            this.btn3DCt5.Click += new System.EventHandler(this.btn3DCt5_Click);
            // 
            // CorrectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 208);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "CorrectionForm";
            this.Text = "Scanner Field Correction - (c)SpiralLab";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btn2DCtb;
        private System.Windows.Forms.Button btn3DCtb;
        private System.Windows.Forms.Button btn2DCt5;
        private System.Windows.Forms.Button btn3DCt5;
    }
}