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
            Mutex mutex = new Mutex(true, "SpiralLab.Sirius", out bool createdNew);
            if (!createdNew)
            {
                MessageBox.Show($"Another program now executing...", "Critical");
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            //sirius library 초기화
            SpiralLab.Core.Initialize();
            var configFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", "sirius.ini");
            //설정 파일에서 실행 프로젝트 객체 생성
            var projectName = NativeMethods.ReadIni<string>(configFileName, "PROJECT", "MainForm");
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
                MessageBox.Show(ex.Message, "Exception - (c)SpiralLab", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            mutex.ReleaseMutex();
        }
    }
}
