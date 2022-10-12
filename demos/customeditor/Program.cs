using SpiralLab.Sirius;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomEditor
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var doc = new DocumentDefault();

            var editor = new SpiralLab.Sirius.CustomEditorForm();
            editor.Document = doc;
            Application.Run(editor);
            // or
            //var viewer = new SpiralLab.Sirius.CustomViewerForm();
            //viewer.Document = doc;
            //Application.Run(viewer);
        }
    }
}
