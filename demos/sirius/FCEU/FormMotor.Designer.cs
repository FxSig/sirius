namespace SpiralLab.Sirius.FCEU
{
    partial class FormMotor
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvAxes = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnServo = new System.Windows.Forms.Button();
            this.btnMotorStop = new System.Windows.Forms.Button();
            this.btnJog = new System.Windows.Forms.Button();
            this.btnMotorReset = new System.Windows.Forms.Button();
            this.btnMotorHome = new System.Windows.Forms.Button();
            this.dgvIndexTable = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbMtIdx = new System.Windows.Forms.Label();
            this.btnWriteAll = new System.Windows.Forms.Button();
            this.btnMoveToIndex = new System.Windows.Forms.Button();
            this.btnCopyPosition = new System.Windows.Forms.Button();
            this.buttonCalculator = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAxes)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIndexTable)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvAxes);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvIndexTable);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(618, 622);
            this.splitContainer1.SplitterDistance = 235;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgvAxes
            // 
            this.dgvAxes.AllowUserToAddRows = false;
            this.dgvAxes.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvAxes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAxes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAxes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAxes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAxes.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvAxes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAxes.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgvAxes.Location = new System.Drawing.Point(0, 0);
            this.dgvAxes.MultiSelect = false;
            this.dgvAxes.Name = "dgvAxes";
            this.dgvAxes.ReadOnly = true;
            this.dgvAxes.RowHeadersVisible = false;
            this.dgvAxes.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvAxes.RowTemplate.Height = 25;
            this.dgvAxes.RowTemplate.ReadOnly = true;
            this.dgvAxes.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAxes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvAxes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvAxes.Size = new System.Drawing.Size(618, 158);
            this.dgvAxes.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "No";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 30;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "Name";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Position";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 80;
            // 
            // Column4
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Column4.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column4.HeaderText = "Servo";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 40;
            // 
            // Column5
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Column5.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column5.HeaderText = "CCW";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column5.Width = 40;
            // 
            // Column6
            // 
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Column6.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column6.HeaderText = "CW";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column6.Width = 40;
            // 
            // Column7
            // 
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Column7.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column7.HeaderText = "ORG";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column7.Width = 40;
            // 
            // Column8
            // 
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Column8.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column8.HeaderText = "Home";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column8.Width = 40;
            // 
            // Column9
            // 
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Column9.DefaultCellStyle = dataGridViewCellStyle7;
            this.Column9.HeaderText = "Drv";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column9.Width = 40;
            // 
            // Column10
            // 
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Column10.DefaultCellStyle = dataGridViewCellStyle8;
            this.Column10.HeaderText = "Alarm";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column10.Width = 40;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnServo);
            this.panel1.Controls.Add(this.btnMotorStop);
            this.panel1.Controls.Add(this.btnJog);
            this.panel1.Controls.Add(this.btnMotorReset);
            this.panel1.Controls.Add(this.btnMotorHome);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 158);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(618, 77);
            this.panel1.TabIndex = 1;
            // 
            // btnServo
            // 
            this.btnServo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnServo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnServo.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btnServo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnServo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnServo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnServo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnServo.Image = global::Spirallab.Sirius.Properties.Resources.sync_settings_24px;
            this.btnServo.Location = new System.Drawing.Point(161, 5);
            this.btnServo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnServo.Name = "btnServo";
            this.btnServo.Size = new System.Drawing.Size(89, 64);
            this.btnServo.TabIndex = 10;
            this.btnServo.Text = "Servo On/Off";
            this.btnServo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnServo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnServo.UseVisualStyleBackColor = false;
            this.btnServo.Click += new System.EventHandler(this.btnServo_Click);
            // 
            // btnMotorStop
            // 
            this.btnMotorStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMotorStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMotorStop.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btnMotorStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnMotorStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnMotorStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMotorStop.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMotorStop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnMotorStop.Image = global::Spirallab.Sirius.Properties.Resources.stop_30px;
            this.btnMotorStop.Location = new System.Drawing.Point(538, 6);
            this.btnMotorStop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnMotorStop.Name = "btnMotorStop";
            this.btnMotorStop.Size = new System.Drawing.Size(68, 64);
            this.btnMotorStop.TabIndex = 9;
            this.btnMotorStop.Text = "Stop";
            this.btnMotorStop.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMotorStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnMotorStop.UseVisualStyleBackColor = true;
            this.btnMotorStop.Click += new System.EventHandler(this.btnMotorStop_Click);
            // 
            // btnJog
            // 
            this.btnJog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnJog.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btnJog.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnJog.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnJog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJog.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJog.Image = global::Spirallab.Sirius.Properties.Resources.step_out_24px;
            this.btnJog.Location = new System.Drawing.Point(13, 6);
            this.btnJog.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnJog.Name = "btnJog";
            this.btnJog.Size = new System.Drawing.Size(68, 64);
            this.btnJog.TabIndex = 8;
            this.btnJog.Text = "Jog";
            this.btnJog.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnJog.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnJog.UseVisualStyleBackColor = true;
            this.btnJog.Click += new System.EventHandler(this.btnJog_Click);
            // 
            // btnMotorReset
            // 
            this.btnMotorReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMotorReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMotorReset.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btnMotorReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnMotorReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnMotorReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMotorReset.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMotorReset.Image = global::Spirallab.Sirius.Properties.Resources.synchronize_24px;
            this.btnMotorReset.Location = new System.Drawing.Point(465, 6);
            this.btnMotorReset.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnMotorReset.Name = "btnMotorReset";
            this.btnMotorReset.Size = new System.Drawing.Size(68, 64);
            this.btnMotorReset.TabIndex = 7;
            this.btnMotorReset.Text = "Reset";
            this.btnMotorReset.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMotorReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnMotorReset.UseVisualStyleBackColor = true;
            this.btnMotorReset.Click += new System.EventHandler(this.btnMotorReset_Click);
            // 
            // btnMotorHome
            // 
            this.btnMotorHome.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMotorHome.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btnMotorHome.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnMotorHome.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnMotorHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMotorHome.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMotorHome.Image = global::Spirallab.Sirius.Properties.Resources.refresh_24px;
            this.btnMotorHome.Location = new System.Drawing.Point(87, 6);
            this.btnMotorHome.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnMotorHome.Name = "btnMotorHome";
            this.btnMotorHome.Size = new System.Drawing.Size(68, 64);
            this.btnMotorHome.TabIndex = 6;
            this.btnMotorHome.Text = "Home";
            this.btnMotorHome.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMotorHome.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnMotorHome.UseVisualStyleBackColor = true;
            this.btnMotorHome.Click += new System.EventHandler(this.btnMotorHome_Click);
            // 
            // dgvIndexTable
            // 
            this.dgvIndexTable.AllowUserToAddRows = false;
            this.dgvIndexTable.AllowUserToDeleteRows = false;
            this.dgvIndexTable.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvIndexTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvIndexTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvIndexTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIndexTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.Column13});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvIndexTable.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgvIndexTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvIndexTable.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgvIndexTable.Location = new System.Drawing.Point(0, 0);
            this.dgvIndexTable.MultiSelect = false;
            this.dgvIndexTable.Name = "dgvIndexTable";
            this.dgvIndexTable.RowHeadersVisible = false;
            this.dgvIndexTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvIndexTable.RowTemplate.Height = 25;
            this.dgvIndexTable.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvIndexTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvIndexTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvIndexTable.Size = new System.Drawing.Size(618, 309);
            this.dgvIndexTable.TabIndex = 5;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "No";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 30;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Position";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 80;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Velocity";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column13
            // 
            this.Column13.HeaderText = "Group";
            this.Column13.Name = "Column13";
            this.Column13.Width = 45;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbMtIdx);
            this.panel2.Controls.Add(this.btnWriteAll);
            this.panel2.Controls.Add(this.btnMoveToIndex);
            this.panel2.Controls.Add(this.btnCopyPosition);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 309);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(618, 77);
            this.panel2.TabIndex = 4;
            // 
            // lbMtIdx
            // 
            this.lbMtIdx.AutoSize = true;
            this.lbMtIdx.Location = new System.Drawing.Point(138, 37);
            this.lbMtIdx.Name = "lbMtIdx";
            this.lbMtIdx.Size = new System.Drawing.Size(42, 16);
            this.lbMtIdx.TabIndex = 10;
            this.lbMtIdx.Text = "label1";
            // 
            // btnWriteAll
            // 
            this.btnWriteAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWriteAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnWriteAll.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btnWriteAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnWriteAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnWriteAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWriteAll.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWriteAll.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnWriteAll.Image = global::Spirallab.Sirius.Properties.Resources.database_export_24px;
            this.btnWriteAll.Location = new System.Drawing.Point(538, 6);
            this.btnWriteAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnWriteAll.Name = "btnWriteAll";
            this.btnWriteAll.Size = new System.Drawing.Size(68, 64);
            this.btnWriteAll.TabIndex = 9;
            this.btnWriteAll.Text = "Write All";
            this.btnWriteAll.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnWriteAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnWriteAll.UseVisualStyleBackColor = true;
            this.btnWriteAll.Click += new System.EventHandler(this.btnWriteAll_Click);
            // 
            // btnMoveToIndex
            // 
            this.btnMoveToIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMoveToIndex.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMoveToIndex.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btnMoveToIndex.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnMoveToIndex.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnMoveToIndex.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMoveToIndex.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveToIndex.Image = global::Spirallab.Sirius.Properties.Resources.step_over_24px;
            this.btnMoveToIndex.Location = new System.Drawing.Point(13, 6);
            this.btnMoveToIndex.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnMoveToIndex.Name = "btnMoveToIndex";
            this.btnMoveToIndex.Size = new System.Drawing.Size(68, 64);
            this.btnMoveToIndex.TabIndex = 8;
            this.btnMoveToIndex.Text = "Move To Index";
            this.btnMoveToIndex.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMoveToIndex.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnMoveToIndex.UseVisualStyleBackColor = true;
            this.btnMoveToIndex.Click += new System.EventHandler(this.btnMoveToIndex_Click);
            // 
            // btnCopyPosition
            // 
            this.btnCopyPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyPosition.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCopyPosition.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btnCopyPosition.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnCopyPosition.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnCopyPosition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopyPosition.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyPosition.Image = global::Spirallab.Sirius.Properties.Resources.clone_24px;
            this.btnCopyPosition.Location = new System.Drawing.Point(465, 6);
            this.btnCopyPosition.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCopyPosition.Name = "btnCopyPosition";
            this.btnCopyPosition.Size = new System.Drawing.Size(68, 64);
            this.btnCopyPosition.TabIndex = 7;
            this.btnCopyPosition.Text = "Copy Position";
            this.btnCopyPosition.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCopyPosition.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCopyPosition.UseVisualStyleBackColor = true;
            this.btnCopyPosition.Click += new System.EventHandler(this.btnCopyPosition_Click);
            // 
            // buttonCalculator
            // 
            this.buttonCalculator.Location = new System.Drawing.Point(0, 0);
            this.buttonCalculator.Name = "buttonCalculator";
            this.buttonCalculator.Size = new System.Drawing.Size(75, 23);
            this.buttonCalculator.TabIndex = 0;
            // 
            // FormMotor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(618, 622);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormMotor";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAxes)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIndexTable)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnMotorStop;
        private System.Windows.Forms.Button btnJog;
        private System.Windows.Forms.DataGridView dgvAxes;
        private System.Windows.Forms.DataGridView dgvIndexTable;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnWriteAll;
        private System.Windows.Forms.Button btnMoveToIndex;
        private System.Windows.Forms.Button btnCopyPosition;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.Button buttonCalculator;
        private System.Windows.Forms.Button btnServo;
        private System.Windows.Forms.Button btnMotorReset;
        private System.Windows.Forms.Button btnMotorHome;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column13;
        private System.Windows.Forms.Label lbMtIdx;
    }
}
