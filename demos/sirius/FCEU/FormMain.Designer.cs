namespace SpiralLab.Sirius.FCEU
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.panFooter = new System.Windows.Forms.Panel();
            this.btnHistory = new System.Windows.Forms.Button();
            this.btnRecipe = new System.Windows.Forms.Button();
            this.btnSetup = new System.Windows.Forms.Button();
            this.btnLaser = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnAuto = new System.Windows.Forms.Button();
            this.panTop = new System.Windows.Forms.Panel();
            this.lblProject = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.btnMaximize = new System.Windows.Forms.Button();
            this.lblTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panHeader = new System.Windows.Forms.Panel();
            this.btnAbort = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.lsbErrWarn = new System.Windows.Forms.ListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panMenu = new System.Windows.Forms.Panel();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblVisionComm = new System.Windows.Forms.Label();
            this.lblRecipe = new System.Windows.Forms.Label();
            this.lblMenu = new System.Windows.Forms.Label();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.panBody = new System.Windows.Forms.Panel();
            this.panFooter.SuspendLayout();
            this.panTop.SuspendLayout();
            this.panHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panFooter
            // 
            this.panFooter.Controls.Add(this.btnHistory);
            this.panFooter.Controls.Add(this.btnRecipe);
            this.panFooter.Controls.Add(this.btnSetup);
            this.panFooter.Controls.Add(this.btnLaser);
            this.panFooter.Controls.Add(this.btnExit);
            this.panFooter.Controls.Add(this.btnAuto);
            this.panFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panFooter.Location = new System.Drawing.Point(0, 918);
            this.panFooter.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panFooter.Name = "panFooter";
            this.panFooter.Size = new System.Drawing.Size(1280, 87);
            this.panFooter.TabIndex = 7;
            // 
            // btnHistory
            // 
            this.btnHistory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHistory.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistory.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistory.Image = global::Spirallab.Sirius.Properties.Resources.activity_history_48px;
            this.btnHistory.Location = new System.Drawing.Point(364, 8);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(82, 72);
            this.btnHistory.TabIndex = 13;
            this.btnHistory.Text = "&History";
            this.btnHistory.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnHistory.UseVisualStyleBackColor = true;
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // btnRecipe
            // 
            this.btnRecipe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRecipe.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btnRecipe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecipe.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecipe.Image = global::Spirallab.Sirius.Properties.Resources.micro_sd_48px;
            this.btnRecipe.Location = new System.Drawing.Point(100, 8);
            this.btnRecipe.Name = "btnRecipe";
            this.btnRecipe.Size = new System.Drawing.Size(82, 72);
            this.btnRecipe.TabIndex = 12;
            this.btnRecipe.Text = "&Recipe";
            this.btnRecipe.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRecipe.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRecipe.UseVisualStyleBackColor = true;
            this.btnRecipe.Click += new System.EventHandler(this.btnRecipe_Click);
            // 
            // btnSetup
            // 
            this.btnSetup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetup.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btnSetup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetup.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetup.Image = global::Spirallab.Sirius.Properties.Resources.adjust_48px;
            this.btnSetup.Location = new System.Drawing.Point(276, 8);
            this.btnSetup.Name = "btnSetup";
            this.btnSetup.Size = new System.Drawing.Size(82, 72);
            this.btnSetup.TabIndex = 11;
            this.btnSetup.Text = "&Setup";
            this.btnSetup.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSetup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSetup.UseVisualStyleBackColor = true;
            this.btnSetup.Click += new System.EventHandler(this.btnSetup_Click);
            // 
            // btnLaser
            // 
            this.btnLaser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLaser.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btnLaser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLaser.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLaser.Image = global::Spirallab.Sirius.Properties.Resources.sirius;
            this.btnLaser.Location = new System.Drawing.Point(188, 8);
            this.btnLaser.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLaser.Name = "btnLaser";
            this.btnLaser.Size = new System.Drawing.Size(82, 72);
            this.btnLaser.TabIndex = 10;
            this.btnLaser.Text = "&Sirius™";
            this.btnLaser.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLaser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLaser.UseVisualStyleBackColor = true;
            this.btnLaser.Click += new System.EventHandler(this.btnLaser_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Image = global::Spirallab.Sirius.Properties.Resources.shutdown_48px;
            this.btnExit.Location = new System.Drawing.Point(1186, 8);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(82, 72);
            this.btnExit.TabIndex = 8;
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnAuto
            // 
            this.btnAuto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAuto.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btnAuto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAuto.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAuto.Image = global::Spirallab.Sirius.Properties.Resources.collage_48px;
            this.btnAuto.Location = new System.Drawing.Point(12, 8);
            this.btnAuto.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(82, 72);
            this.btnAuto.TabIndex = 3;
            this.btnAuto.Text = "&Auto";
            this.btnAuto.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAuto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAuto.UseVisualStyleBackColor = true;
            this.btnAuto.Click += new System.EventHandler(this.btnAuto_Click);
            // 
            // panTop
            // 
            this.panTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.panTop.Controls.Add(this.lblProject);
            this.panTop.Controls.Add(this.lblVersion);
            this.panTop.Controls.Add(this.btnMaximize);
            this.panTop.Controls.Add(this.lblTime);
            this.panTop.Controls.Add(this.label1);
            this.panTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panTop.Location = new System.Drawing.Point(0, 0);
            this.panTop.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panTop.Name = "panTop";
            this.panTop.Size = new System.Drawing.Size(1280, 34);
            this.panTop.TabIndex = 8;
            this.panTop.DoubleClick += new System.EventHandler(this.panTop_DoubleClick);
            this.panTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panTop_MouseDown);
            this.panTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panTop_MouseMove);
            // 
            // lblProject
            // 
            this.lblProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProject.AutoSize = true;
            this.lblProject.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProject.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblProject.Location = new System.Drawing.Point(964, 9);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(102, 16);
            this.lblProject.TabIndex = 4;
            this.lblProject.Text = "Project : (Unknown)";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblVersion.Location = new System.Drawing.Point(339, 9);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(43, 16);
            this.lblVersion.TabIndex = 3;
            this.lblVersion.Text = "Ver 1.0";
            // 
            // btnMaximize
            // 
            this.btnMaximize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaximize.Image = global::Spirallab.Sirius.Properties.Resources.full_screen_24px;
            this.btnMaximize.Location = new System.Drawing.Point(1238, 5);
            this.btnMaximize.Name = "btnMaximize";
            this.btnMaximize.Size = new System.Drawing.Size(29, 23);
            this.btnMaximize.TabIndex = 2;
            this.btnMaximize.UseVisualStyleBackColor = true;
            this.btnMaximize.Click += new System.EventHandler(this.btnMaximize_Click);
            // 
            // lblTime
            // 
            this.lblTime.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.SystemColors.Control;
            this.lblTime.Location = new System.Drawing.Point(622, 9);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(36, 16);
            this.lblTime.TabIndex = 1;
            this.lblTime.Text = "12:00";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(325, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "2021 COPYRIGHT TO (c)SPIRALLAB. ALL RIGHTS RESERVED.";
            // 
            // panHeader
            // 
            this.panHeader.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panHeader.Controls.Add(this.btnAbort);
            this.panHeader.Controls.Add(this.btnReset);
            this.panHeader.Controls.Add(this.lsbErrWarn);
            this.panHeader.Controls.Add(this.pictureBox1);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(0, 34);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(1280, 121);
            this.panHeader.TabIndex = 10;
            // 
            // btnAbort
            // 
            this.btnAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbort.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAbort.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btnAbort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbort.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbort.Image = global::Spirallab.Sirius.Properties.Resources.stop_sign_40px;
            this.btnAbort.Location = new System.Drawing.Point(1172, 19);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(82, 82);
            this.btnAbort.TabIndex = 13;
            this.btnAbort.Text = "Abort";
            this.btnAbort.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAbort.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Image = global::Spirallab.Sirius.Properties.Resources.reset_48px;
            this.btnReset.Location = new System.Drawing.Point(1084, 19);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(82, 82);
            this.btnReset.TabIndex = 12;
            this.btnReset.Text = "Reset";
            this.btnReset.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lsbErrWarn
            // 
            this.lsbErrWarn.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lsbErrWarn.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsbErrWarn.FormattingEnabled = true;
            this.lsbErrWarn.ItemHeight = 16;
            this.lsbErrWarn.Location = new System.Drawing.Point(293, 11);
            this.lsbErrWarn.Name = "lsbErrWarn";
            this.lsbErrWarn.Size = new System.Drawing.Size(631, 100);
            this.lsbErrWarn.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Spirallab.Sirius.Properties.Resources.spirallablogo;
            this.pictureBox1.Location = new System.Drawing.Point(18, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(237, 107);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.Gainsboro;
            this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 155);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1280, 1);
            this.splitter1.TabIndex = 11;
            this.splitter1.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.Color.Gainsboro;
            this.splitter2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(0, 917);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(1280, 1);
            this.splitter2.TabIndex = 13;
            this.splitter2.TabStop = false;
            // 
            // panMenu
            // 
            this.panMenu.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panMenu.Controls.Add(this.lblUser);
            this.panMenu.Controls.Add(this.lblVisionComm);
            this.panMenu.Controls.Add(this.lblRecipe);
            this.panMenu.Controls.Add(this.lblMenu);
            this.panMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panMenu.Location = new System.Drawing.Point(0, 156);
            this.panMenu.Name = "panMenu";
            this.panMenu.Size = new System.Drawing.Size(1280, 32);
            this.panMenu.TabIndex = 15;
            // 
            // lblUser
            // 
            this.lblUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUser.AutoSize = true;
            this.lblUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblUser.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Location = new System.Drawing.Point(915, 7);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(115, 16);
            this.lblUser.TabIndex = 10;
            this.lblUser.Text = "User : (Unknown)";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblUser.Click += new System.EventHandler(this.lblUser_Click);
            // 
            // lblVisionComm
            // 
            this.lblVisionComm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVisionComm.AutoSize = true;
            this.lblVisionComm.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVisionComm.Location = new System.Drawing.Point(1119, 7);
            this.lblVisionComm.Name = "lblVisionComm";
            this.lblVisionComm.Size = new System.Drawing.Size(126, 16);
            this.lblVisionComm.TabIndex = 9;
            this.lblVisionComm.Text = "Vision : (Unknown)";
            this.lblVisionComm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRecipe
            // 
            this.lblRecipe.AutoSize = true;
            this.lblRecipe.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecipe.Location = new System.Drawing.Point(174, 7);
            this.lblRecipe.Name = "lblRecipe";
            this.lblRecipe.Size = new System.Drawing.Size(127, 16);
            this.lblRecipe.TabIndex = 8;
            this.lblRecipe.Text = "Recipe: (Unknown)";
            this.lblRecipe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMenu
            // 
            this.lblMenu.AutoSize = true;
            this.lblMenu.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenu.Location = new System.Drawing.Point(16, 7);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(122, 16);
            this.lblMenu.TabIndex = 7;
            this.lblMenu.Text = "Menu : (Unknown)";
            this.lblMenu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitter3
            // 
            this.splitter3.BackColor = System.Drawing.Color.Gainsboro;
            this.splitter3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter3.Location = new System.Drawing.Point(0, 188);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(1280, 1);
            this.splitter3.TabIndex = 16;
            this.splitter3.TabStop = false;
            // 
            // panBody
            // 
            this.panBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panBody.Location = new System.Drawing.Point(0, 189);
            this.panBody.Name = "panBody";
            this.panBody.Size = new System.Drawing.Size(1280, 728);
            this.panBody.TabIndex = 18;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1280, 1005);
            this.Controls.Add(this.panBody);
            this.Controls.Add(this.splitter3);
            this.Controls.Add(this.panMenu);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panHeader);
            this.Controls.Add(this.panFooter);
            this.Controls.Add(this.panTop);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormMain";
            this.Text = "Sirius (c)SPIRALLAB";
            this.panFooter.ResumeLayout(false);
            this.panTop.ResumeLayout(false);
            this.panTop.PerformLayout();
            this.panHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panMenu.ResumeLayout(false);
            this.panMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panFooter;
        private System.Windows.Forms.Panel panTop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Button btnMaximize;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Panel panMenu;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.Panel panBody;
        private System.Windows.Forms.Button btnAuto;
        private System.Windows.Forms.Button btnSetup;
        private System.Windows.Forms.Button btnLaser;
        private System.Windows.Forms.ListBox lsbErrWarn;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Label lblRecipe;
        private System.Windows.Forms.Button btnRecipe;
        private System.Windows.Forms.Label lblVisionComm;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.Label lblProject;
        private System.Windows.Forms.Label lblUser;
    }
}