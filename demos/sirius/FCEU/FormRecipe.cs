using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Numerics;

namespace SpiralLab.Sirius.FCEU
{
    public partial class FormRecipe : Form
    {
        int index = -1;
        string name;

        public FormRecipe()
        {
            InitializeComponent();

            this.VisibleChanged += FormRecipe_VisibleChanged;
            dgvRecipe.CellClick += DgvRecipe_CellClick;

            this.siriusViewerForm1.Document = new DocumentDefault();
        }

        private void FormRecipe_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.UpdateDir();
                var formMain = Program.MainForm as SpiralLab.Sirius.FCEU.FormMain;
                var svc = formMain.Seq.Service as LaserService;
                dgvRecipe.ClearSelection();
                if (svc.RecipeNo > 0)
                {
                    index = svc.RecipeNo-1;
                    dgvRecipe.Rows[index].Selected = true;
                }
            }
            else
            {

            }
        }
        private void DgvRecipe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var newIndex = int.Parse(dgvRecipe.Rows[e.RowIndex].Cells[0].Value?.ToString());
            var newName = dgvRecipe.Rows[e.RowIndex].Cells[1].Value?.ToString();
            if (this.index == newIndex)
                return;

            this.index = newIndex;
            this.name = newName;
            this.siriusViewerForm1.Document.Action.ActNew();

            var fileName = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"FILE", "RECIPE");
            string recipeFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "recipes", $"{index}", fileName);
            if (!File.Exists(recipeFileName))
                return;

            var form = new ProgressForm()
            {
                Message = $"Loading Recipe To Preview : [{index}]: {name}",
                Percentage = 0,
            };
            form.Show(this);
            Application.DoEvents();
            try
            {
                var doc = DocumentSerializer.OpenSirius(recipeFileName);
                this.siriusViewerForm1.Document = doc;
                this.siriusViewerForm1.AliasName = $" [{index}] {name} ";
                this.siriusViewerForm1.FileName = recipeFileName;
            }
            finally
            {
                form.Close();
            }
        }

        public static List<string> GetList()
        {
            var list = new List<string>();
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "recipes", "recipe.ini");
            var count = NativeMethods.ReadIni<int>(fileName, $"RECIPE", "COUNT");
            for (int i = 1; i <= count; i++)
            {
                string name = NativeMethods.ReadIni<string>(fileName, $"{i}", "NAME");
                list.Add(name);
            }
            return list;
        }

        /// <summary>
        /// 디렉토리 재귀 복사
        /// </summary>
        /// <param name="source">소스 디렉토리</param>
        /// <param name="target">타켓 디렉토리</param>
        internal static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
                CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
            foreach (FileInfo file in source.GetFiles())
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
        }

        /// <summary>
        /// 있는 레시피를 복사한다
        /// </summary>
        /// <param name="sourceRecipeDir">원본 레시피 폴더 이름 1,2,3</param>
        /// <param name="sourceRecipeName">원본 레시피 이름</param>
        /// <param name="targetRecipeDir">대상 레시피 폴더 이름 1,2,3</param>
        /// <param name="targetRecipeName">원본 레시피 이름 (ini 파일에 기록됨)</param>
        /// <returns></returns>
        public static bool RecipeCopy(string sourceRecipeDir, string sourceRecipeName,  string targetRecipeDir, string targetRecipeName)
        {
            var root = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "recipes");
            var src = $"{root}\\{sourceRecipeDir}";
            var target = $"{root}\\{targetRecipeDir}";
            DirectoryInfo diSrc = new DirectoryInfo(src);
            DirectoryInfo diTarget = new DirectoryInfo(target);
            if (!diTarget.Exists)
                diTarget.Create();
            CopyFilesRecursively(diSrc, diTarget);

            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "recipes", "recipe.ini");
            //string sourceRecipeName = NativeMethods.ReadIni<string>(fileName, src, "NAME");
            NativeMethods.WriteIni<string>(fileName, targetRecipeDir, "NAME", targetRecipeName);
            Logger.Log(Logger.Type.Warn, $"recipe has copied from [{sourceRecipeDir}]{sourceRecipeName} to [{targetRecipeDir}]{targetRecipeName}");
            return true;
        }

        private void UpdateDir()
        {
            var list = GetList();
            dgvRecipe.SuspendLayout();
            dgvRecipe.Rows.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                int rowId = dgvRecipe.Rows.Add();
                DataGridViewRow rowIndex = dgvRecipe.Rows[rowId];
                rowIndex.Cells[0].Value = i + 1;
                rowIndex.Cells[1].Value = list[i];
            }
            dgvRecipe.ResumeLayout();
        }

        private void btnRecipeChange_Click(object sender, EventArgs e)
        {
            var formMain = Program.MainForm as SpiralLab.Sirius.FCEU.FormMain;
            var svc = formMain.Seq.Service as LaserService;
            if (formMain.Seq.IsBusy)
            {
                var mb = new MessageBoxOk();
                mb.ShowDialog("LASER IS BUSY !", $"Turn laser off at first !!!");
                return;
            }

            if (string.IsNullOrEmpty(this.name) || this.index < 0)
                return;
            {
                var mb = new MessageBoxYesNo();
                if (DialogResult.Yes != mb.ShowDialog("Recipe Change", $"Do you want to change recipe to [{this.index}]: {this.name} ?"))
                    return;
            }

            bool success = svc.RecipeChange(this.index);
            {
                var mb = new MessageBoxOk();
                if (success)
                {
                    mb.ShowDialog("Recipe", $"Success to change recipe by [{this.index}]: {this.name} ", 10);
                }
                else
                    mb.ShowDialog("Recipe", $"Fail to change recipe by [{this.index}]: {this.name} ", 10);
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            var formMain = Program.MainForm as SpiralLab.Sirius.FCEU.FormMain;
            var svc = formMain.Seq.Service as LaserService;
            if (formMain.Seq.IsBusy)
            {
                var mb = new MessageBoxOk();
                mb.ShowDialog("LASER IS BUSY !", $"Turn laser off at first !!!");
                return;
            }
            var form = new FormRecipeCopy();
            form.ShowDialog();
            this.UpdateDir();
        }
    }
}