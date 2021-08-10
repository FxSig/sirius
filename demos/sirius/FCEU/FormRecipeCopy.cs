using System;
using System.IO;
using System.Windows.Forms;

namespace SpiralLab.Sirius.FCEU
{
    public partial class FormRecipeCopy : Form
    {
        int recipeSrcNo;
        string recipeSrc;
        int recipeTargetNo;
        string recipeTarget;

        public FormRecipeCopy()
        {
            InitializeComponent();
            this.VisibleChanged += FormRecipes_VisibleChanged;
            this.dgvRecipeSrc.CellClick += DgvRecipeSrc_CellClick;
            this.dgvRecipeTarget.CellClick += DgvRecipeTarget_CellClick;
        }

        #region 폼 외곽에 Drop Shadow 효과
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
        #endregion

        private void FormRecipes_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                txtTargetRecipeName.Text = string.Empty;
                this.UpdateRecipeDir();
            }
            else
            {
            }
        }
       
        private void DgvRecipeSrc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            this.recipeSrcNo = int.Parse(dgvRecipeSrc.Rows[e.RowIndex].Cells[0].Value.ToString());
            this.recipeSrc = dgvRecipeSrc.Rows[e.RowIndex].Cells[1].Value?.ToString();
            txtTargetRecipeName.Text = this.recipeSrc + " Copy";
        }
        private void DgvRecipeTarget_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            this.recipeTargetNo = int.Parse(dgvRecipeTarget.Rows[e.RowIndex].Cells[0].Value.ToString());
            this.recipeTarget = dgvRecipeTarget.Rows[e.RowIndex].Cells[1].Value?.ToString();
        }

        private void UpdateRecipeDir()
        {
            var recipes = FormRecipe.GetList();
            dgvRecipeSrc.SuspendLayout();
            dgvRecipeSrc.Rows.Clear();
            for (int i = 0; i < recipes.Count; i++)
            {
                int rowId = dgvRecipeSrc.Rows.Add();
                DataGridViewRow rowIndex = dgvRecipeSrc.Rows[rowId];
                rowIndex.Cells[0].Value = i+1;
                rowIndex.Cells[1].Value = recipes[i];
            }
            dgvRecipeSrc.ResumeLayout();
            dgvRecipeSrc.ClearSelection();

            dgvRecipeTarget.SuspendLayout();
            dgvRecipeTarget.Rows.Clear();
            for (int i = 0; i < recipes.Count; i++)
            {
                int rowId = dgvRecipeTarget.Rows.Add();
                DataGridViewRow rowIndex = dgvRecipeTarget.Rows[rowId];
                rowIndex.Cells[0].Value = i + 1;
                rowIndex.Cells[1].Value = recipes[i];
            }
            dgvRecipeTarget.ResumeLayout();
            dgvRecipeTarget.ClearSelection();
        }

        private void btnCopyRecipe_Click(object sender, EventArgs e)
        {
            if (this.recipeSrcNo == this.recipeTargetNo)
                return;
            this.recipeTarget = txtTargetRecipeName.Text;
            if (string.IsNullOrEmpty(this.recipeSrc) || string.IsNullOrEmpty(this.recipeTarget))
                return;

            //if (Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "recipes", $"{this.recipeTarget}")))
            //{
            //    var mb = new MessageBoxYesNo();
            //    if (DialogResult.Yes != mb.ShowDialog("Recipe Warning", $"Target recipe is already exist !!! Do you really want to overwrite {this.group}/{this.recipeTarget} ?"))
            //        return;
            //}
            {
                var mb = new MessageBoxYesNo();
                if (DialogResult.Yes != mb.ShowDialog("Recipe Copy", $"Do you want to copy recipe : [{this.recipeSrcNo}]{this.recipeSrc} -> [{recipeTargetNo}] {this.recipeTarget} ?"))
                    return;
            }
            { 
                var mb = new MessageBoxOk();
                if (FormRecipe.RecipeCopy($"{this.recipeSrcNo}", this.recipeSrc, $"{this.recipeTargetNo}", this.recipeTarget))
                {
                    mb.ShowDialog("Recipe Copy", $"Success to copy recipe : [{this.recipeSrcNo}]{this.recipeSrc} -> [{recipeTargetNo}] {this.recipeTarget}", 10);
                    this.UpdateRecipeDir();
                }
                else
                    mb.ShowDialog("Recipe Copy", $"Success to copy recipe : [{this.recipeSrcNo}]{this.recipeSrc} -> [{recipeTargetNo}] {this.recipeTarget}");
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
