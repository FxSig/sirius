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
        
        /// <summary>
        /// 메인 폼 전역 객체
        /// </summary>
        public static Form MainForm = null;

        /// <summary>
        /// 프로젝트 이름 전역 문자열
        /// </summary>
        public static string ProjectName = null;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 프로그램 중복 실행 여부 처리
            Mutex mutex = new Mutex(true, "SpiralLab.Sirius", out bool createdNew);
            if (!createdNew)
            {
                if (DialogResult.Yes != MessageBox.Show($"Executing program already ...{Environment.NewLine}Do you want to execute another program ? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    return;                
            }
            
            // (c) SpiralLAB 시리우스 라이브러리 초기화
            SpiralLab.Core.Initialize();

            // 명령행 인자
            string[] args = Environment.GetCommandLineArgs();

            // 설정 파일에서 실행 프로젝트 객체 생성
            string configFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", "sirius.ini");
            string projectName;
            // 명령행 인자 있으면 대상 프로젝트 인스턴스 생성
            // 없으면 sirius.ini 파일에 지정된 대상 프로젝트 인스턴스 생성
            if (1 == args.Length)
                projectName = NativeMethods.ReadIni<string>(configFileName, "PROJECT", "MainForm");
            else
                projectName = args[1];

            // 생성할 인스턴스 타입 변환
            Type projectType = Type.GetType(projectName.Trim());
            if (null == projectType)
            {
                MessageBox.Show($"Can't create target project : {projectName} at {configFileName}", "Critical");
                return;
            }
            // 인스턴스 생성 (메인 윈폼)
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
