namespace SpiralLab.Sirius
{
    partial class MainForm
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
            this.siriusViewerForm1 = new SpiralLab.Sirius.SiriusViewerForm();
            this.SuspendLayout();
            // 
            // siriusViewerForm1
            // 
            this.siriusViewerForm1.AliasName = "NoName";
            this.siriusViewerForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.siriusViewerForm1.Document = null;
            this.siriusViewerForm1.FileName = "NoName";
            this.siriusViewerForm1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.siriusViewerForm1.Index = ((uint)(0u));
            this.siriusViewerForm1.Location = new System.Drawing.Point(0, 0);
            this.siriusViewerForm1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.siriusViewerForm1.Name = "siriusViewerForm1";
            this.siriusViewerForm1.Progress = 0;
            this.siriusViewerForm1.Size = new System.Drawing.Size(908, 608);
            this.siriusViewerForm1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 608);
            this.Controls.Add(this.siriusViewerForm1);
            this.Name = "MainForm";
            this.Text = "Your Sirius Viewer - (c)SpiralLab";
            this.ResumeLayout(false);

        }

        #endregion

        private SpiralLab.Sirius.SiriusViewerForm siriusViewerForm1;
    }
}

