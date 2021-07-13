namespace SpiralLab.Sirius
{
    partial class FormMain
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnEditor2 = new System.Windows.Forms.Button();
            this.btnEditor1 = new System.Windows.Forms.Button();
            this.btnMain = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnEditor2);
            this.panel2.Controls.Add(this.btnEditor1);
            this.panel2.Controls.Add(this.btnMain);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 647);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(934, 91);
            this.panel2.TabIndex = 1;
            // 
            // btnEditor2
            // 
            this.btnEditor2.Location = new System.Drawing.Point(222, 9);
            this.btnEditor2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnEditor2.Name = "btnEditor2";
            this.btnEditor2.Size = new System.Drawing.Size(100, 75);
            this.btnEditor2.TabIndex = 5;
            this.btnEditor2.Text = "&Editor #2";
            this.btnEditor2.UseVisualStyleBackColor = true;
            this.btnEditor2.Click += new System.EventHandler(this.btnEditor2_Click);
            // 
            // btnEditor1
            // 
            this.btnEditor1.Location = new System.Drawing.Point(116, 10);
            this.btnEditor1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnEditor1.Name = "btnEditor1";
            this.btnEditor1.Size = new System.Drawing.Size(100, 75);
            this.btnEditor1.TabIndex = 4;
            this.btnEditor1.Text = "&Editor #1";
            this.btnEditor1.UseVisualStyleBackColor = true;
            this.btnEditor1.Click += new System.EventHandler(this.btnEditor1_Click);
            // 
            // btnMain
            // 
            this.btnMain.Location = new System.Drawing.Point(9, 9);
            this.btnMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnMain.Name = "btnMain";
            this.btnMain.Size = new System.Drawing.Size(100, 75);
            this.btnMain.TabIndex = 3;
            this.btnMain.Text = "&Main";
            this.btnMain.UseVisualStyleBackColor = true;
            this.btnMain.Click += new System.EventHandler(this.btnMain_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(934, 34);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(443, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "This is a demo for multiple views and editor screen. powered by spirallab.sirius." +
    "";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 34);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(934, 613);
            this.panel3.TabIndex = 6;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 738);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormMain";
            this.Text = "(c)Demo for Multiple Laser/Scanner - (c)SpiralLab";
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnEditor1;
        private System.Windows.Forms.Button btnMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnEditor2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
    }
}

