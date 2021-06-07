using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    public static class Program
    {
        /// <summary>
        /// 설정 파일
        /// </summary>
        public static string ConfigFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", "sirius.ini");
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            //sirius library 초기화
            SpiralLab.Core.Initialize();
            //설정 파일에서 실행 프로젝트 객체를 읽고       
            var projectName = NativeMethods.ReadIni<string>(ConfigFileName, "PROJECT", "MainForm");
            Type projectType = Type.GetType(projectName.Trim());
            if (null == projectType)
            {
                var mb = new Default.MessageBoxOk();
                mb.ShowDialog("Critical", $"Can't create project : {projectType.ToString()} at {ConfigFileName}");
                return;
            }
            //프로젝트 객체를 생성
            Form mainForm = Activator.CreateInstance(projectType) as Form ;
            if (null == mainForm)
            {
                var mb = new Default.MessageBoxOk();
                mb.ShowDialog("Critical", $"Can't create project : {projectName} at {ConfigFileName}");
                return;
            }
            //실행
            Application.Run(mainForm);
        }
    }
}
