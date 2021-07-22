namespace SpiralLab.Sirius.FCEU
{
    partial class FormRecipe
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnRecipeChange = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.siriusViewerForm1 = new SpiralLab.Sirius.SiriusViewerForm();
            this.dgvRecipe = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecipe)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRecipeChange
            // 
            this.btnRecipeChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRecipeChange.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRecipeChange.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btnRecipeChange.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnRecipeChange.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnRecipeChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecipeChange.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecipeChange.Image = global::Spirallab.Sirius.Properties.Resources.clone_24px;
            this.btnRecipeChange.Location = new System.Drawing.Point(929, 688);
            this.btnRecipeChange.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRecipeChange.Name = "btnRecipeChange";
            this.btnRecipeChange.Size = new System.Drawing.Size(68, 64);
            this.btnRecipeChange.TabIndex = 8;
            this.btnRecipeChange.Text = "Recipe Change";
            this.btnRecipeChange.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRecipeChange.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRecipeChange.UseVisualStyleBackColor = true;
            this.btnRecipeChange.Click += new System.EventHandler(this.btnRecipeChange_Click);
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
            this.splitContainer1.Panel2.Controls.Add(this.dgvRecipe);
            this.splitContainer1.Size = new System.Drawing.Size(1024, 768);
            this.splitContainer1.SplitterDistance = 700;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 10;
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
            this.siriusViewerForm1.Size = new System.Drawing.Size(700, 768);
            this.siriusViewerForm1.TabIndex = 11;
            // 
            // dgvRecipe
            // 
            this.dgvRecipe.AllowUserToAddRows = false;
            this.dgvRecipe.AllowUserToDeleteRows = false;
            this.dgvRecipe.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvRecipe.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecipe.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRecipe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecipe.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dgvRecipe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecipe.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgvRecipe.Location = new System.Drawing.Point(0, 0);
            this.dgvRecipe.MultiSelect = false;
            this.dgvRecipe.Name = "dgvRecipe";
            this.dgvRecipe.RowHeadersVisible = false;
            this.dgvRecipe.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvRecipe.RowTemplate.Height = 25;
            this.dgvRecipe.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRecipe.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvRecipe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecipe.Size = new System.Drawing.Size(322, 768);
            this.dgvRecipe.TabIndex = 8;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "No";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 50;
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
            // FormRecipe
            // 
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.btnRecipeChange);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormRecipe";
            this.Text = "Recipe";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecipe)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnRecipeChange;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private SiriusViewerForm siriusViewerForm1;
        private System.Windows.Forms.DataGridView dgvRecipe;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}