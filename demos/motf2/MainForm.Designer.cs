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
            this.components = new System.ComponentModel.Container();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblEncXCnt = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblEncXmm = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblEncYCnt = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblEncYmm = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.mnuEncReset = new System.Windows.Forms.ToolStripMenuItem();
            this.lblMarker = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.siriusEditorForm1 = new SpiralLab.Sirius.SiriusEditorForm();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Top;
            this.statusStrip1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblEncXCnt,
            this.lblEncXmm,
            this.toolStripStatusLabel3,
            this.lblEncYCnt,
            this.lblEncYmm,
            this.toolStripSplitButton1,
            this.lblMarker});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1128, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(80, 15);
            this.toolStripStatusLabel1.Text = "Encoder X";
            // 
            // lblEncXCnt
            // 
            this.lblEncXCnt.AutoSize = false;
            this.lblEncXCnt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEncXCnt.Name = "lblEncXCnt";
            this.lblEncXCnt.Size = new System.Drawing.Size(80, 15);
            this.lblEncXCnt.Text = "0 cnt";
            // 
            // lblEncXmm
            // 
            this.lblEncXmm.AutoSize = false;
            this.lblEncXmm.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEncXmm.Name = "lblEncXmm";
            this.lblEncXmm.Size = new System.Drawing.Size(80, 15);
            this.lblEncXmm.Text = "0 mm";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.AutoSize = false;
            this.toolStripStatusLabel3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(80, 15);
            this.toolStripStatusLabel3.Text = "Encoder Y";
            // 
            // lblEncYCnt
            // 
            this.lblEncYCnt.AutoSize = false;
            this.lblEncYCnt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEncYCnt.Name = "lblEncYCnt";
            this.lblEncYCnt.Size = new System.Drawing.Size(80, 15);
            this.lblEncYCnt.Text = "0 cnt";
            // 
            // lblEncYmm
            // 
            this.lblEncYmm.AutoSize = false;
            this.lblEncYmm.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEncYmm.Name = "lblEncYmm";
            this.lblEncYmm.Size = new System.Drawing.Size(80, 15);
            this.lblEncYmm.Text = "0 mm";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEncReset});
            this.toolStripSplitButton1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(21, 19);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            // 
            // mnuEncReset
            // 
            this.mnuEncReset.Name = "mnuEncReset";
            this.mnuEncReset.Size = new System.Drawing.Size(229, 34);
            this.mnuEncReset.Text = "Encoder Reset";
            this.mnuEncReset.Click += new System.EventHandler(this.mnuEncReset_Click);
            // 
            // lblMarker
            // 
            this.lblMarker.AutoSize = false;
            this.lblMarker.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarker.Name = "lblMarker";
            this.lblMarker.Size = new System.Drawing.Size(96, 15);
            this.lblMarker.Text = "Marker";
            this.lblMarker.Click += new System.EventHandler(this.lblMarker_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // siriusEditorForm1
            // 
            this.siriusEditorForm1.AliasName = "NoName";
            this.siriusEditorForm1.AllowDrop = true;
            this.siriusEditorForm1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.siriusEditorForm1.BackColor = System.Drawing.SystemColors.Control;
            this.siriusEditorForm1.Document = null;
            this.siriusEditorForm1.EnablePens = true;
            this.siriusEditorForm1.FileName = "(Empty)";
            this.siriusEditorForm1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.siriusEditorForm1.HidePropertyGrid = false;
            this.siriusEditorForm1.Index = ((uint)(0u));
            this.siriusEditorForm1.Laser = null;
            this.siriusEditorForm1.Location = new System.Drawing.Point(0, 26);
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
            this.siriusEditorForm1.Size = new System.Drawing.Size(1128, 889);
            this.siriusEditorForm1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1128, 917);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.siriusEditorForm1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "MOTF Demo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SiriusEditorForm siriusEditorForm1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblEncYCnt;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel lblEncXCnt;
        private System.Windows.Forms.ToolStripStatusLabel lblEncXmm;
        private System.Windows.Forms.ToolStripStatusLabel lblEncYmm;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem mnuEncReset;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripStatusLabel lblMarker;
    }
}

