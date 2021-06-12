namespace SpiralLab.Sirius.Nguyen
{
    partial class FormDigitalIO
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panLeft = new System.Windows.Forms.Panel();
            this.panRight = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panLeft);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panRight);
            this.splitContainer1.Size = new System.Drawing.Size(1042, 782);
            this.splitContainer1.SplitterDistance = 521;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 1;
            // 
            // panLeft
            // 
            this.panLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panLeft.Location = new System.Drawing.Point(0, 0);
            this.panLeft.Name = "panLeft";
            this.panLeft.Size = new System.Drawing.Size(521, 782);
            this.panLeft.TabIndex = 2;
            // 
            // panRight
            // 
            this.panRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panRight.Location = new System.Drawing.Point(0, 0);
            this.panRight.Name = "panRight";
            this.panRight.Size = new System.Drawing.Size(518, 782);
            this.panRight.TabIndex = 2;
            // 
            // FormDigitalIO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 782);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormDigitalIO";
            this.Text = "DigitalIO";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panLeft;
        private System.Windows.Forms.Panel panRight;
    }
}