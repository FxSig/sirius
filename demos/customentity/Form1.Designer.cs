namespace SpiralLab.Sirius
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnCustomEntity = new System.Windows.Forms.ToolStripButton();
            this.siriusEditorForm1 = new SpiralLab.Sirius.SiriusEditorForm();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCustomEntity});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(929, 31);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnCustomEntity
            // 
            this.btnCustomEntity.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCustomEntity.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustomEntity.Image = ((System.Drawing.Image)(resources.GetObject("btnCustomEntity.Image")));
            this.btnCustomEntity.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCustomEntity.Name = "btnCustomEntity";
            this.btnCustomEntity.Size = new System.Drawing.Size(28, 28);
            this.btnCustomEntity.Text = "Custom Entity";
            this.btnCustomEntity.ToolTipText = "Your Custome Entity";
            this.btnCustomEntity.Click += new System.EventHandler(this.btnCustomEntity_Click);
            // 
            // siriusEditorForm1
            // 
            this.siriusEditorForm1.AliasName = "NoName";
            this.siriusEditorForm1.AllowDrop = true;
            this.siriusEditorForm1.BackColor = System.Drawing.SystemColors.Control;
            this.siriusEditorForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.siriusEditorForm1.Document = null;
            this.siriusEditorForm1.EnablePens = true;
            this.siriusEditorForm1.FileName = "(Empty)";
            this.siriusEditorForm1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.siriusEditorForm1.HidePropertyGrid = false;
            this.siriusEditorForm1.Index = ((uint)(0u));
            this.siriusEditorForm1.Laser = null;
            this.siriusEditorForm1.Location = new System.Drawing.Point(0, 31);
            this.siriusEditorForm1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.siriusEditorForm1.Marker = null;
            this.siriusEditorForm1.Motors = null;
            this.siriusEditorForm1.MotorZ = null;
            this.siriusEditorForm1.Name = "siriusEditorForm1";
            this.siriusEditorForm1.PowerMap = null;
            this.siriusEditorForm1.PowerMeter = null;
            this.siriusEditorForm1.Progress = 0;
            this.siriusEditorForm1.Rtc = null;
            this.siriusEditorForm1.RtcExtension1Input = null;
            this.siriusEditorForm1.RtcExtension1Output = null;
            this.siriusEditorForm1.RtcExtension2Output = null;
            this.siriusEditorForm1.RtcPin2Input = null;
            this.siriusEditorForm1.RtcPin2Output = null;
            this.siriusEditorForm1.Size = new System.Drawing.Size(929, 579);
            this.siriusEditorForm1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 610);
            this.Controls.Add(this.siriusEditorForm1);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Create Custom Entity - (c)SpiralLAB";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnCustomEntity;
        private SpiralLab.Sirius.SiriusEditorForm siriusEditorForm1;
    }
}

