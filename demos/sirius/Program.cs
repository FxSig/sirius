using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    public static class Program
    {
        
        public static Form MainForm = null;
        public static string ProjectName = null;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Mutex mutex = new Mutex(true, "SpiralLab.Sirius", out bool createdNew);
            if (!createdNew)
            {
                if (DialogResult.Yes != MessageBox.Show($"Executing program already ...{Environment.NewLine}Do you want to execute another program ? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    return;                
            }
            
            //sirius library 초기화
            SpiralLab.Core.Initialize();

            string[] args = Environment.GetCommandLineArgs();
            string configFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", "sirius.ini");

            //설정 파일에서 실행 프로젝트 객체 생성
            string projectName;
            if (1 == args.Length)
                projectName = NativeMethods.ReadIni<string>(configFileName, "PROJECT", "MainForm");
            else
                projectName = args[1];
            Type projectType = Type.GetType(projectName.Trim());
            if (null == projectType)
            {
                MessageBox.Show($"Can't create target project : {projectName} at {configFileName}", "Critical");
                return;
            }
            //메인 폼 생성
            ProjectName = projectName;
            Program.MainForm = Activator.CreateInstance(projectType) as Form ;
            if (null == Program.MainForm)
            {
                MessageBox.Show($"Can't create target project : {projectName} at {configFileName}", "Critical");
                return;
            }
            try
            {
                Application.Run(MainForm);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Critical Exception - (c)SpiralLab", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
            }
            mutex.ReleaseMutex();
        }
    }
}
