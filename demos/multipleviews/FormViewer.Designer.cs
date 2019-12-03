namespace SpiralLab.Sirius
{
    partial class FormViewer
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
            this.siriusViewerForm1 = new SpiralLab.Sirius.SiriusViewerForm();
            this.siriusViewerForm2 = new SpiralLab.Sirius.SiriusViewerForm();
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
            this.splitContainer1.Panel1.Controls.Add(this.siriusViewerForm1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.siriusViewerForm2);
            this.splitContainer1.Size = new System.Drawing.Size(892, 544);
            this.splitContainer1.SplitterDistance = 451;
            this.splitContainer1.TabIndex = 0;
            // 
            // siriusViewerForm1
            // 
            this.siriusViewerForm1.AliasName = "NoName";
            this.siriusViewerForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.siriusViewerForm1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.siriusViewerForm1.Location = new System.Drawing.Point(0, 0);
            this.siriusViewerForm1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.siriusViewerForm1.Name = "siriusViewerForm1";
            this.siriusViewerForm1.Progress = 0;
            this.siriusViewerForm1.Size = new System.Drawing.Size(451, 544);
            this.siriusViewerForm1.TabIndex = 0;
            // 
            // siriusViewerForm2
            // 
            this.siriusViewerForm2.AliasName = "NoName";
            this.siriusViewerForm2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.siriusViewerForm2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.siriusViewerForm2.Location = new System.Drawing.Point(0, 0);
            this.siriusViewerForm2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.siriusViewerForm2.Name = "siriusViewerForm2";
            this.siriusViewerForm2.Progress = 0;
            this.siriusViewerForm2.Size = new System.Drawing.Size(437, 544);
            this.siriusViewerForm2.TabIndex = 0;
            // 
            // FormViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 544);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormViewer";
            this.Text = "FormViewer";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private SiriusViewerForm siriusViewerForm1;
        private SiriusViewerForm siriusViewerForm2;
    }
}