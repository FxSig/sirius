namespace SpiralLab.Sirius.FCEU
{
    partial class FormEditor
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
            this.siriusEditorForm1 = new SpiralLab.Sirius.SiriusEditorForm();
            this.SuspendLayout();
            // 
            // siriusEditorForm1
            // 
            this.siriusEditorForm1.AliasName = "Laser 1";
            this.siriusEditorForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.siriusEditorForm1.Document = null;
            this.siriusEditorForm1.FileName = "NoName";
            this.siriusEditorForm1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.siriusEditorForm1.HidePropertyGrid = false;
            this.siriusEditorForm1.Index = ((uint)(0u));
            this.siriusEditorForm1.Laser = null;
            this.siriusEditorForm1.Location = new System.Drawing.Point(0, 0);
            this.siriusEditorForm1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.siriusEditorForm1.Marker = null;
            this.siriusEditorForm1.MotorZ = null;
            this.siriusEditorForm1.Name = "siriusEditorForm1";
            this.siriusEditorForm1.Progress = 0;
            this.siriusEditorForm1.Rtc = null;
            this.siriusEditorForm1.Size = new System.Drawing.Size(1268, 757);
            this.siriusEditorForm1.TabIndex = 0;
            // 
            // FormEditor
            // 
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1268, 757);
            this.Controls.Add(this.siriusEditorForm1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormEditor";
            this.Text = "Sirius™";
            this.ResumeLayout(false);

        }

        #endregion

        private SiriusEditorForm siriusEditorForm1;
    }
}