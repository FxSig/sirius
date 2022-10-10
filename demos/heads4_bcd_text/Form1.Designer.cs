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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.siriusEditorForm1 = new SpiralLab.Sirius.SiriusEditorForm();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.siriusEditorForm2 = new SpiralLab.Sirius.SiriusEditorForm();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.siriusEditorForm3 = new SpiralLab.Sirius.SiriusEditorForm();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.siriusEditorForm4 = new SpiralLab.Sirius.SiriusEditorForm();
            this.button1 = new System.Windows.Forms.Button();
            this.cbbIndex = new System.Windows.Forms.ComboBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.HotTrack = true;
            this.tabControl1.ItemSize = new System.Drawing.Size(72, 30);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1008, 761);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.siriusEditorForm1);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Size = new System.Drawing.Size(1000, 723);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Laser 1";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            this.siriusEditorForm1.Location = new System.Drawing.Point(3, 4);
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
            this.siriusEditorForm1.Size = new System.Drawing.Size(994, 715);
            this.siriusEditorForm1.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.siriusEditorForm2);
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Size = new System.Drawing.Size(1000, 723);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Laser 2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // siriusEditorForm2
            // 
            this.siriusEditorForm2.AliasName = "NoName";
            this.siriusEditorForm2.AllowDrop = true;
            this.siriusEditorForm2.BackColor = System.Drawing.SystemColors.Control;
            this.siriusEditorForm2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.siriusEditorForm2.Document = null;
            this.siriusEditorForm2.EnablePens = true;
            this.siriusEditorForm2.FileName = "(Empty)";
            this.siriusEditorForm2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.siriusEditorForm2.HidePropertyGrid = false;
            this.siriusEditorForm2.Index = ((uint)(0u));
            this.siriusEditorForm2.Laser = null;
            this.siriusEditorForm2.Location = new System.Drawing.Point(3, 4);
            this.siriusEditorForm2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.siriusEditorForm2.Marker = null;
            this.siriusEditorForm2.Motors = null;
            this.siriusEditorForm2.MotorZ = null;
            this.siriusEditorForm2.Name = "siriusEditorForm2";
            this.siriusEditorForm2.PowerMap = null;
            this.siriusEditorForm2.PowerMeter = null;
            this.siriusEditorForm2.Progress = 0;
            this.siriusEditorForm2.Rtc = null;
            this.siriusEditorForm2.RtcExtension1Input = null;
            this.siriusEditorForm2.RtcExtension1Output = null;
            this.siriusEditorForm2.RtcExtension2Output = null;
            this.siriusEditorForm2.RtcPin2Input = null;
            this.siriusEditorForm2.RtcPin2Output = null;
            this.siriusEditorForm2.Size = new System.Drawing.Size(994, 715);
            this.siriusEditorForm2.TabIndex = 2;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.siriusEditorForm3);
            this.tabPage3.Location = new System.Drawing.Point(4, 34);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1000, 723);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Laser 3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // siriusEditorForm3
            // 
            this.siriusEditorForm3.AliasName = "NoName";
            this.siriusEditorForm3.AllowDrop = true;
            this.siriusEditorForm3.BackColor = System.Drawing.SystemColors.Control;
            this.siriusEditorForm3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.siriusEditorForm3.Document = null;
            this.siriusEditorForm3.EnablePens = true;
            this.siriusEditorForm3.FileName = "(Empty)";
            this.siriusEditorForm3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.siriusEditorForm3.HidePropertyGrid = false;
            this.siriusEditorForm3.Index = ((uint)(0u));
            this.siriusEditorForm3.Laser = null;
            this.siriusEditorForm3.Location = new System.Drawing.Point(3, 3);
            this.siriusEditorForm3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.siriusEditorForm3.Marker = null;
            this.siriusEditorForm3.Motors = null;
            this.siriusEditorForm3.MotorZ = null;
            this.siriusEditorForm3.Name = "siriusEditorForm3";
            this.siriusEditorForm3.PowerMap = null;
            this.siriusEditorForm3.PowerMeter = null;
            this.siriusEditorForm3.Progress = 0;
            this.siriusEditorForm3.Rtc = null;
            this.siriusEditorForm3.RtcExtension1Input = null;
            this.siriusEditorForm3.RtcExtension1Output = null;
            this.siriusEditorForm3.RtcExtension2Output = null;
            this.siriusEditorForm3.RtcPin2Input = null;
            this.siriusEditorForm3.RtcPin2Output = null;
            this.siriusEditorForm3.Size = new System.Drawing.Size(994, 717);
            this.siriusEditorForm3.TabIndex = 1;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.siriusEditorForm4);
            this.tabPage4.Location = new System.Drawing.Point(4, 34);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1000, 723);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Laser 4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // siriusEditorForm4
            // 
            this.siriusEditorForm4.AliasName = "NoName";
            this.siriusEditorForm4.AllowDrop = true;
            this.siriusEditorForm4.BackColor = System.Drawing.SystemColors.Control;
            this.siriusEditorForm4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.siriusEditorForm4.Document = null;
            this.siriusEditorForm4.EnablePens = true;
            this.siriusEditorForm4.FileName = "(Empty)";
            this.siriusEditorForm4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.siriusEditorForm4.HidePropertyGrid = false;
            this.siriusEditorForm4.Index = ((uint)(0u));
            this.siriusEditorForm4.Laser = null;
            this.siriusEditorForm4.Location = new System.Drawing.Point(3, 3);
            this.siriusEditorForm4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.siriusEditorForm4.Marker = null;
            this.siriusEditorForm4.Motors = null;
            this.siriusEditorForm4.MotorZ = null;
            this.siriusEditorForm4.Name = "siriusEditorForm4";
            this.siriusEditorForm4.PowerMap = null;
            this.siriusEditorForm4.PowerMeter = null;
            this.siriusEditorForm4.Progress = 0;
            this.siriusEditorForm4.Rtc = null;
            this.siriusEditorForm4.RtcExtension1Input = null;
            this.siriusEditorForm4.RtcExtension1Output = null;
            this.siriusEditorForm4.RtcExtension2Output = null;
            this.siriusEditorForm4.RtcPin2Input = null;
            this.siriusEditorForm4.RtcPin2Output = null;
            this.siriusEditorForm4.Size = new System.Drawing.Size(994, 717);
            this.siriusEditorForm4.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(550, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 28);
            this.button1.TabIndex = 4;
            this.button1.Text = "Test Mark";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbbIndex
            // 
            this.cbbIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbIndex.FormattingEnabled = true;
            this.cbbIndex.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cbbIndex.Location = new System.Drawing.Point(400, 4);
            this.cbbIndex.Name = "cbbIndex";
            this.cbbIndex.Size = new System.Drawing.Size(60, 24);
            this.cbbIndex.TabIndex = 5;
            // 
            // btnOpen
            // 
            this.btnOpen.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpen.Location = new System.Drawing.Point(468, 3);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(80, 28);
            this.btnOpen.TabIndex = 6;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 761);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.cbbIndex);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "(c)SpiralLab.Sirius Demo For i3Engineering";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private SpiralLab.Sirius.SiriusEditorForm siriusEditorForm3;
        private SpiralLab.Sirius.SiriusEditorForm siriusEditorForm2;
        private SpiralLab.Sirius.SiriusEditorForm siriusEditorForm4;
        private SpiralLab.Sirius.SiriusEditorForm siriusEditorForm1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbbIndex;
        private System.Windows.Forms.Button btnOpen;
    }
}

