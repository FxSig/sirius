/*
 *                                                            ,--,      ,--,                              
 *             ,-.----.                                     ,---.'|   ,---.'|                              
 *   .--.--.   \    /  \     ,---,,-.----.      ,---,       |   | :   |   | :      ,---,           ,---,.  
 *  /  /    '. |   :    \ ,`--.' |\    /  \    '  .' \      :   : |   :   : |     '  .' \        ,'  .'  \ 
 * |  :  /`. / |   |  .\ :|   :  :;   :    \  /  ;    '.    |   ' :   |   ' :    /  ;    '.    ,---.' .' | 
 * ;  |  |--`  .   :  |: |:   |  '|   | .\ : :  :       \   ;   ; '   ;   ; '   :  :       \   |   |  |: | 
 * |  :  ;_    |   |   \ :|   :  |.   : |: | :  |   /\   \  '   | |__ '   | |__ :  |   /\   \  :   :  :  / 
 *  \  \    `. |   : .   /'   '  ;|   |  \ : |  :  ' ;.   : |   | :.'||   | :.'||  :  ' ;.   : :   |    ;  
 *   `----.   \;   | |`-' |   |  ||   : .  / |  |  ;/  \   \'   :    ;'   :    ;|  |  ;/  \   \|   :     \ 
 *   __ \  \  ||   | ;    '   :  ;;   | |  \ '  :  | \  \ ,'|   |  ./ |   |  ./ '  :  | \  \ ,'|   |   . | 
 *  /  /`--'  /:   ' |    |   |  '|   | ;\  \|  |  '  '--'  ;   : ;   ;   : ;   |  |  '  '--'  '   :  '; | 
 * '--'.     / :   : :    '   :  |:   ' | \.'|  :  :        |   ,/    |   ,/    |  :  :        |   |  | ;  
 *   `--'---'  |   | :    ;   |.' :   : :-'  |  | ,'        '---'     '---'     |  | ,'        |   :   /   
 *             `---'.|    '---'   |   |.'    `--''                              `--''          |   | ,'    
 *               `---`            `---'                                                        `----'   
 * 
 *
 * Scanner Field Correction / 스캐너 필드 보정
 * 
 * 스캐너 필드 왜곡 보정을 지원해주는 윈폼을 생성하여 처리한다
 * Author : hong chan, choi / hcchoi@spirallab.co.kr (http://spirallab.co.kr)
 * 
 */


using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    class Program2
    {
        static float kfactor = (float)Math.Pow(2, 20) / 60.0f;
        static IRtc rtc = null;

        [STAThread]
        static void Main2(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SpiralLab.Core.Initialize();

            #region initialize RTC 
            //var rtc = new RtcVirtual(0); //create Rtc for dummy (가상 RTC 카드)
            rtc = new Rtc5(0); //create Rtc5 controller
            //var rtc = new Rtc6(0); //create Rtc6 controller
            //var rtc = new Rtc6Ethernet(0, "192.168.0.100", "255.255.255.0"); //Rtc6 Ethernet
            //var rtc = new Rtc6SyncAxis(0, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "syncAxis", "syncAXISConfig.xml")); //Scanlab XLSCAN 
            float fov = 60.0f;    // scanner field of view : 60mm            
            float kfactor = (float)Math.Pow(2, 20) / fov; // k factor (bits/mm) = 2^20 / fov
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            rtc.Initialize(kfactor, LaserMode.Yag5, correctionFile);    // correction file (스캐너 보정 파일)
            #endregion

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Testcase for spirallab.sirius. powered by hcchoi@spirallab.co.kr (http://spirallab.co.kr)");
                Console.WriteLine("");
                Console.WriteLine("'C' : create new field correction for 3D (ct5)");
                Console.WriteLine("'F' : create new field correction for 3D with WinForms (ct5)");
                Console.WriteLine("'D' : create new field correction for 3D (ctb)");
                Console.WriteLine("'G' : create new field correction for 3D with WinForms (ctb)");
                Console.WriteLine("'Q' : quit");
                Console.WriteLine("");
                Console.Write("select your target : ");
                key = Console.ReadKey(false);
                if (key.Key == ConsoleKey.Q)
                    break;
                Console.WriteLine("");
                switch (key.Key)
                {
                    case ConsoleKey.C:
                        string result = CreateFieldCorrection();
                        Console.WriteLine("");
                        Console.WriteLine(result);
                        break;
                    case ConsoleKey.F:
                        CreateFieldCorrectionWithWinForms();
                        break;
                    case ConsoleKey.D:
                        string result2 = CreateFieldCorrection2();
                        Console.WriteLine("");
                        Console.WriteLine(result2);
                        break;
                    case ConsoleKey.G:
                        CreateFieldCorrectionWithWinForms2();
                        break;
                }

            } while (true);
        }
      
        /// <summary>
        /// 3D 스캐너 보정실시
        /// </summary>
        /// <returns></returns>
        private static string CreateFieldCorrection()
        {
            //현재 스캐너 보정 파일
            var srcFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            //신규로 생성할 스캐너 보정 파일
            var targetFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", $"newfile.ct5");

            // 3차원 스캐너 보정용 IRtcCorrection 객체 생성
            // 우선 Z= 0 (2D) 영역에 대한 정밀 보정을 진행한후 3D 보정이 진행되어야 한다 !
            float interval = 20;
            var correction = new Correction3DRtc(kfactor, 3, 3, interval, 5, -5, srcFile, targetFile);

            #region inputs relative error deviation : 상대적인 오차위치 값을 넣는 방법 (머신 비전 오차값을 넣는 것과 유사)
            // Z= 5mm 위치에 포커스를 하여 레이저를 출사하고 그 오차를 입력
            correction.AddRelative(0, 0, new Vector3(-20, 20, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(0, 1, new Vector3(0, 20, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(0, 2, new Vector3(20, 20, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 0, new Vector3(-20, 0, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 1, new Vector3(0, 0, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 2, new Vector3(20, 0, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 0, new Vector3(-20, -20, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 1, new Vector3(0, -20, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 2, new Vector3(20, -20, 5), new Vector3(0.01f, 0.01f, 0));

            // Z= -5mm 위치에 포커스를 하여 레이저를 출사하고 그 오차를 입력
            correction.AddRelative(0, 0, new Vector3(-20, 20, -5), new Vector3(0.01f, -0.02f, 0));
            correction.AddRelative(0, 1, new Vector3(0, 20, -5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(0, 2, new Vector3(20, 20, -5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 0, new Vector3(-20, 0, -5), new Vector3(-0.01f, 0.01f, 0));
            correction.AddRelative(1, 1, new Vector3(0, 0, -5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 2, new Vector3(20, 0, -5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 0, new Vector3(-20, -20, -5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 1, new Vector3(0, -20, -5), new Vector3(0.01f, -0.01f, 0));
            correction.AddRelative(2, 2, new Vector3(20, -20, -5), new Vector3(0.01f, 0.01f, 0));
            #endregion

            //신규 보정 파일 생성 실시
            bool success = correction.Convert();
            //var rtc = ...
            // 보정 파일을 테이블 1번으로 로딩
            //success &= rtc.CtlLoadCorrectionFile(CorrectionTableIndex.Table1, targetFile);
            // 테이블1 번을 1번 스캐너(Primary Head)에 지정
            //success &= rtc.CtlSelectCorrection(CorrectionTableIndex.Table1);
            return correction.ResultMessage;
        }

        private static void CreateFieldCorrectionWithWinForms()
        {
            //현재 스캐너 보정 파일
            var srcFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            //신규로 생성할 스캐너 보정 파일
            var targetFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", $"newfile.ct5");

            // 3차원 스캐너 보정용 IRtcCorrection 객체 생성
            // 우선 Z= 0 (2D) 영역에 대한 정밀 보정을 진행한후 3D 보정이 진행되어야 한다 !
            float interval = 20;
            var correction = new Correction3DRtc(kfactor, 3, 3, interval, 5, -5, srcFile, targetFile);

            #region inputs relative error deviation : 상대적인 오차위치 값을 넣는 방법 (머신 비전 오차값을 넣는 것과 유사)
            // Z= 5mm 위치에 포커스를 하여 레이저를 출사하고 그 오차를 입력
            correction.AddRelative(0, 0, new Vector3(-20, 20, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(0, 1, new Vector3(0, 20, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(0, 2, new Vector3(20, 20, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 0, new Vector3(-20, 0, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 1, new Vector3(0, 0, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 2, new Vector3(20, 0, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 0, new Vector3(-20, -20, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 1, new Vector3(0, -20, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 2, new Vector3(20, -20, 5), new Vector3(0.01f, 0.01f, 0));

            // Z= -5mm 위치에 포커스를 하여 레이저를 출사하고 그 오차를 입력
            correction.AddRelative(0, 0, new Vector3(-20, 20, -5), new Vector3(0.01f, -0.02f, 0));
            correction.AddRelative(0, 1, new Vector3(0, 20, -5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(0, 2, new Vector3(20, 20, -5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 0, new Vector3(-20, 0, -5), new Vector3(-0.01f, 0.01f, 0));
            correction.AddRelative(1, 1, new Vector3(0, 0, -5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 2, new Vector3(20, 0, -5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 0, new Vector3(-20, -20, -5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 1, new Vector3(0, -20, -5), new Vector3(0.01f, -0.01f, 0));
            correction.AddRelative(2, 2, new Vector3(20, -20, -5), new Vector3(0.01f, 0.01f, 0));
            #endregion

            var form = new Correction3DRtcForm(rtc, correction);
            // or
            //var form = new Correction3DRtcForm(correction);
            //form.OnApply += Form3D_OnApply;
            form.ShowDialog();
        }

        /// <summary>
        /// 3D 스캐너 보정실시
        /// </summary>
        /// <returns></returns>
        private static string CreateFieldCorrection2()
        {
            //현재 스캐너 보정 파일
            var srcFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ctb");
            //신규로 생성할 스캐너 보정 파일
            var targetFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", $"newfile.ctb");

            // 3차원 스캐너 보정용 IRtcCorrection 객체 생성
            // 우선 Z= 0 (2D) 영역에 대한 정밀 보정을 진행한후 3D 보정이 진행되어야 한다 !
            float interval = 20;
            var correction = new Correction3DRtc(kfactor, 3, 3, interval, 5, -5, srcFile, targetFile);

            #region inputs relative error deviation : 상대적인 오차위치 값을 넣는 방법 (머신 비전 오차값을 넣는 것과 유사)
            // Z= 5mm 위치에 포커스를 하여 레이저를 출사하고 그 오차를 입력
            correction.AddRelative(0, 0, new Vector3(-20, 20, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(0, 1, new Vector3(0, 20, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(0, 2, new Vector3(20, 20, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 0, new Vector3(-20, 0, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 1, new Vector3(0, 0, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 2, new Vector3(20, 0, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 0, new Vector3(-20, -20, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 1, new Vector3(0, -20, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 2, new Vector3(20, -20, 5), new Vector3(0.01f, 0.01f, 0));

            // Z= -5mm 위치에 포커스를 하여 레이저를 출사하고 그 오차를 입력
            correction.AddRelative(0, 0, new Vector3(-20, 20, -5), new Vector3(0.01f, -0.02f, 0));
            correction.AddRelative(0, 1, new Vector3(0, 20, -5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(0, 2, new Vector3(20, 20, -5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 0, new Vector3(-20, 0, -5), new Vector3(-0.01f, 0.01f, 0));
            correction.AddRelative(1, 1, new Vector3(0, 0, -5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 2, new Vector3(20, 0, -5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 0, new Vector3(-20, -20, -5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 1, new Vector3(0, -20, -5), new Vector3(0.01f, -0.01f, 0));
            correction.AddRelative(2, 2, new Vector3(20, -20, -5), new Vector3(0.01f, 0.01f, 0));
            #endregion

            //신규 보정 파일 생성 실시
            bool success = correction.Convert();
            //var rtc = ...
            // 보정 파일을 테이블 1번으로 로딩
            //success &= rtc.CtlLoadCorrectionFile(CorrectionTableIndex.Table1, targetFile);
            // 테이블1 번을 1번 스캐너(Primary Head)에 지정
            //success &= rtc.CtlSelectCorrection(CorrectionTableIndex.Table1);
            return correction.ResultMessage;
        }

        private static void CreateFieldCorrectionWithWinForms2()
        {
            //현재 스캐너 보정 파일
            var srcFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ctb");
            //신규로 생성할 스캐너 보정 파일
            var targetFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", $"newfile.ctb");

            // 3차원 스캐너 보정용 IRtcCorrection 객체 생성
            // 우선 Z= 0 (2D) 영역에 대한 정밀 보정을 진행한후 3D 보정이 진행되어야 한다 !
            float interval = 20;
            var correction = new Correction3DRtc(kfactor, 3, 3, interval, 5, -5, srcFile, targetFile);

            #region inputs relative error deviation : 상대적인 오차위치 값을 넣는 방법 (머신 비전 오차값을 넣는 것과 유사)
            // Z= 5mm 위치에 포커스를 하여 레이저를 출사하고 그 오차를 입력
            correction.AddRelative(0, 0, new Vector3(-20, 20, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(0, 1, new Vector3(0, 20, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(0, 2, new Vector3(20, 20, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 0, new Vector3(-20, 0, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 1, new Vector3(0, 0, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 2, new Vector3(20, 0, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 0, new Vector3(-20, -20, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 1, new Vector3(0, -20, 5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 2, new Vector3(20, -20, 5), new Vector3(0.01f, 0.01f, 0));

            // Z= -5mm 위치에 포커스를 하여 레이저를 출사하고 그 오차를 입력
            correction.AddRelative(0, 0, new Vector3(-20, 20, -5), new Vector3(0.01f, -0.02f, 0));
            correction.AddRelative(0, 1, new Vector3(0, 20, -5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(0, 2, new Vector3(20, 20, -5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 0, new Vector3(-20, 0, -5), new Vector3(-0.01f, 0.01f, 0));
            correction.AddRelative(1, 1, new Vector3(0, 0, -5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 2, new Vector3(20, 0, -5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 0, new Vector3(-20, -20, -5), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 1, new Vector3(0, -20, -5), new Vector3(0.01f, -0.01f, 0));
            correction.AddRelative(2, 2, new Vector3(20, -20, -5), new Vector3(0.01f, 0.01f, 0));
            #endregion

            var form = new Correction3DRtcForm(rtc, correction);
            //or
            //var form = new Correction3DRtcForm(correction);
            //form.OnApply += Form3D_OnApply;
            form.ShowDialog();
        }
        //private static void Form3D_OnApply(object sender, EventArgs e)
        //{
        //    var form = sender as Correction3DRtcForm;
        //    var index = form.Index;
        //    string ctFileName = form.RtcCorrection.TargetCorrectionFile;
        //    if (!File.Exists(ctFileName))
        //    {
        //        Logger.Log(Logger.Type.Error, $"try to change correction3d file but not exist : {ctFileName}");
        //        return;
        //    }
        //    if (DialogResult.Yes != MessageBox.Show($"Do you really want to apply new correction3d file {ctFileName} ?", "Warning !", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        //        return;

        //    bool success = true;
        //    //해당 보정파일을 RTC 제어기 메모리 안으로 로드후 선택
        //    success &= rtc.CtlLoadCorrectionFile(CorrectionTableIndex.Table1, ctFileName);
        //    success &= rtc.CtlSelectCorrection(CorrectionTableIndex.Table1, CorrectionTableIndex.Table1);
        //    if (success)
        //    {
        //        // 권장) ctFileName 파일 정보를 외부 설정 파일에 저장
        //        Logger.Log(Logger.Type.Warn, $"correction3d file has changed to {ctFileName}");
        //        MessageBox.Show($"Success to load and selected field correction3d file to {ctFileName}", "Scanner Field Correction 3D File", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    else
        //    {
        //        MessageBox.Show($"Fail to load and selected field correction3d file to {ctFileName}", "Scanner Field Correction 3D File", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }
        //}
    }
}
