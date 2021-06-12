namespace SpiralLab.Sirius.Nguyen
{
    partial class FormIPG
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
            this.siriusIPGYLPTypeE1 = new SpiralLab.Sirius.SiriusIPGYLPTypeE();
            this.SuspendLayout();
            // 
            // siriusIPGYLPTypeE1
            // 
            this.siriusIPGYLPTypeE1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.siriusIPGYLPTypeE1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.siriusIPGYLPTypeE1.Laser = null;
            this.siriusIPGYLPTypeE1.Location = new System.Drawing.Point(0, 0);
            this.siriusIPGYLPTypeE1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.siriusIPGYLPTypeE1.Name = "siriusIPGYLPTypeE1";
            this.siriusIPGYLPTypeE1.Size = new System.Drawing.Size(522, 706);
            this.siriusIPGYLPTypeE1.TabIndex = 0;
            // 
            // FormIPG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 706);
            this.Controls.Add(this.siriusIPGYLPTypeE1);
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormIPG";
            this.Text = "DigitalIO";
            this.ResumeLayout(false);

        }

        #endregion

        private SiriusIPGYLPTypeE siriusIPGYLPTypeE1;
    }
}