namespace CustomEditor
{
    partial class CustomViewerForm
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblName = new System.Windows.Forms.ToolStripStatusLabel();
            this.pgbProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.lblXPos = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblYPos = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblEntityCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblRenderTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblFileName = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.btnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnZoomFit = new System.Windows.Forms.ToolStripButton();
            this.btnPan = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.GLcontrol = new SharpGL.OpenGLControl();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GLcontrol)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblName,
            this.pgbProgress,
            this.lblXPos,
            this.lblYPos,
            this.lblEntityCount,
            this.lblRenderTime,
            this.lblFileName});
            this.statusStrip1.Location = new System.Drawing.Point(0, 285);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(384, 15);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(47, 10);
            this.lblName.Text = "NoName";
            // 
            // pgbProgress
            // 
            this.pgbProgress.AutoSize = false;
            this.pgbProgress.Name = "pgbProgress";
            this.pgbProgress.Size = new System.Drawing.Size(40, 9);
            this.pgbProgress.Step = 1;
            // 
            // lblXPos
            // 
            this.lblXPos.AutoSize = false;
            this.lblXPos.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblXPos.Name = "lblXPos";
            this.lblXPos.Size = new System.Drawing.Size(70, 10);
            this.lblXPos.Text = "X: 0.000";
            // 
            // lblYPos
            // 
            this.lblYPos.AutoSize = false;
            this.lblYPos.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYPos.Name = "lblYPos";
            this.lblYPos.Size = new System.Drawing.Size(70, 10);
            this.lblYPos.Text = "Y: 0.000";
            // 
            // lblEntityCount
            // 
            this.lblEntityCount.AutoSize = false;
            this.lblEntityCount.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntityCount.Name = "lblEntityCount";
            this.lblEntityCount.Size = new System.Drawing.Size(70, 10);
            this.lblEntityCount.Text = "Selected: 0";
            // 
            // lblRenderTime
            // 
            this.lblRenderTime.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenderTime.Name = "lblRenderTime";
            this.lblRenderTime.Size = new System.Drawing.Size(65, 10);
            this.lblRenderTime.Text = "Render: 0 ms";
            // 
            // lblFileName
            // 
            this.lblFileName.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(47, 10);
            this.lblFileName.Text = "NoName";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnZoomOut,
            this.btnZoomIn,
            this.btnZoomFit,
            this.btnPan,
            this.toolStripSeparator9});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(384, 17);
            this.toolStrip1.TabIndex = 11;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomOut.Image = global::CustomEditor.Properties.Resources.icons8_zoom_out_filled_16;
            this.btnZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(23, 14);
            this.btnZoomOut.Text = "toolStripButton3";
            this.btnZoomOut.ToolTipText = "Zoom Out";
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomIn.Image = global::CustomEditor.Properties.Resources.icons8_zoom_in_16;
            this.btnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(23, 14);
            this.btnZoomIn.Text = "toolStripButton2";
            this.btnZoomIn.ToolTipText = "Zoom In";
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // btnZoomFit
            // 
            this.btnZoomFit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomFit.Image = global::CustomEditor.Properties.Resources.icons8_zoom_to_extents_filled_16;
            this.btnZoomFit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomFit.Name = "btnZoomFit";
            this.btnZoomFit.Size = new System.Drawing.Size(23, 14);
            this.btnZoomFit.Text = "toolStripButton1";
            this.btnZoomFit.ToolTipText = "Zoom Fit";
            this.btnZoomFit.Click += new System.EventHandler(this.btnZoomFit_Click);
            // 
            // btnPan
            // 
            this.btnPan.CheckOnClick = true;
            this.btnPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPan.Image = global::CustomEditor.Properties.Resources.hand_move;
            this.btnPan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPan.Name = "btnPan";
            this.btnPan.Size = new System.Drawing.Size(23, 14);
            this.btnPan.Text = "toolStripButton1";
            this.btnPan.ToolTipText = "Pan";
            this.btnPan.Click += new System.EventHandler(this.btnPan_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 17);
            // 
            // GLcontrol
            // 
            this.GLcontrol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GLcontrol.DrawFPS = false;
            this.GLcontrol.FrameRate = 60;
            this.GLcontrol.Location = new System.Drawing.Point(0, 11);
            this.GLcontrol.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.GLcontrol.Name = "GLcontrol";
            this.GLcontrol.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.GLcontrol.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.GLcontrol.RenderTrigger = SharpGL.RenderTrigger.Manual;
            this.GLcontrol.Size = new System.Drawing.Size(384, 279);
            this.GLcontrol.TabIndex = 12;
            // 
            // CustomViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 450);
            this.Controls.Add(this.GLcontrol);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CustomViewerForm";
            this.Text = "(c)SpiralLab - Custom Viewer";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GLcontrol)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblName;
        private System.Windows.Forms.ToolStripProgressBar pgbProgress;
        private System.Windows.Forms.ToolStripStatusLabel lblXPos;
        private System.Windows.Forms.ToolStripStatusLabel lblYPos;
        private System.Windows.Forms.ToolStripStatusLabel lblEntityCount;
        private System.Windows.Forms.ToolStripStatusLabel lblRenderTime;
        private System.Windows.Forms.ToolStripStatusLabel lblFileName;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnZoomOut;
        private System.Windows.Forms.ToolStripButton btnZoomIn;
        private System.Windows.Forms.ToolStripButton btnZoomFit;
        private System.Windows.Forms.ToolStripButton btnPan;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        public SharpGL.OpenGLControl GLcontrol;
    }
}
