﻿/*
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
 * 스캐너 필드 왜곡 보정을 윈폼 없이 직접 데이타를 입력하여 시도한다
 * Author : hong chan, choi / hcchoi@spirallab.co.kr (http://spirallab.co.kr)
 * 
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    class Program1
    {
        // 이론적인 FOV 크기 입력 필요
        static float fov = 60.0f;
        static IRtc rtc = null;
        static float kfactor20bits = (float)Math.Pow(2, 20) / fov;
        static float kfactor16bits = (float)Math.Pow(2, 16) / fov;

        [STAThread]
        static void Main1(string[] args)
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
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            rtc.Initialize(kfactor16bits, LaserMode.Yag5, correctionFile);    // correction file (스캐너 보정 파일)
            #endregion


            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Testcase for spirallab.sirius. powered by hcchoi@spirallab.co.kr (http://spirallab.co.kr)");
                Console.WriteLine("");
                Console.WriteLine("'C' : create new field correction for 2D (square)");
                Console.WriteLine("'D' : create new field correction for 2D (rectangle)");
                Console.WriteLine("'F' : create new field correction for 2D with WinForms");
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
                        Console.WriteLine(result);
                        break;
                    case ConsoleKey.D:
                        string result2 = CreateFieldCorrection2();
                        Console.WriteLine(result2);
                        break;
                    case ConsoleKey.F:
                        CreateFieldCorrectionWithWinForms();                        
                        break;
                }
            } while (true);
        }
      
        /// <summary>
        /// 2D 스캐너 보정 실시 (정사각형 영역)
        /// </summary>
        /// <returns></returns>
        private static string CreateFieldCorrection()
        {
            //현재 스캐너 보정 파일
            var srcFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            //신규로 생성할 스캐너 보정 파일
            var targetFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", $"newfile.ct5");

            // 3x3 (9개) 위치에 대한 보정 테이블 입력 용
            var correction = new Correction2DRtc(kfactor20bits, 3, 3, 20, 20, srcFile, targetFile);

            // 좌 상단 부터 오른쪽 방향으로 !
            //
            // 0,0  0,1  0,2 ...
            // 1,0  1,1  1,2 ...
            // 2,0  2,1  2,2 ...
            // 3,0  3,1  3,2 ...
            // ...

            #region inputs relative error deviation : 상대적인 오차위치 값을 넣는 방법 (머신 비전 오차값을 넣는 것과 유사)
            // 해당 X,Y 위치에서의 오차값 dx, dy 입력
            correction.AddRelative(0, 0, new Vector2(-20, 20), new Vector2(0.01f, 0.01f));
            correction.AddRelative(0, 1, new Vector2(0, 20), new Vector2(0.01f, 0.01f));
            correction.AddRelative(0, 2, new Vector2(20, 20), new Vector2(0.01f, 0.01f));
            correction.AddRelative(1, 0, new Vector2(-20, 0), new Vector2(0.01f, 0.01f));
            correction.AddRelative(1, 1, new Vector2(0, 0), new Vector2(0.01f, 0.01f));
            correction.AddRelative(1, 2, new Vector2(20, 0), new Vector2(0.01f, 0.01f));
            correction.AddRelative(2, 0, new Vector2(-20, -20), new Vector2(0.01f, 0.01f));
            correction.AddRelative(2, 1, new Vector2(0, -20), new Vector2(0.01f, 0.01f));
            correction.AddRelative(2, 2, new Vector2(20, -20), new Vector2(0.01f, 0.01f));
            #endregion

            #region inputs absolute position values : 오차값의 절대적인 위치 정보를 넣는 방법
            // 해당 X,Y 위치에서의 오차가 반영된 실제 X,Y 위치 입력 
            //correction.AddAbsolute(0, 0, new Vector2(-20, 20), new Vector2(-20.01f, 20.01f));
            //correction.AddAbsolute(0, 1, new Vector2(0, 20), new Vector2(0.01f, 20.01f));
            //correction.AddAbsolute(0, 2, new Vector2(20, 20), new Vector2(20.01f, 20.01f));
            //correction.AddAbsolute(1, 0, new Vector2(-20, 0), new Vector2(-20.01f, 0.01f));
            //correction.AddAbsolute(1, 1, new Vector2(0, 0), new Vector2(0.01f, 0.01f));
            //correction.AddAbsolute(1, 2, new Vector2(20, 0), new Vector2(20.01f, 0.01f));
            //correction.AddAbsolute(2, 0, new Vector2(-20, -20), new Vector2(-20.01f, -20.01f));
            //correction.AddAbsolute(2, 1, new Vector2(0, -20), new Vector2(0.01f, -20.01f));
            //correction.AddAbsolute(2, 2, new Vector2(20, -20), new Vector2(20.01f, -20.01f));
            #endregion

            //신규 보정 파일 생성 실시
            bool success = correction.Convert();
            //var rtc = ...;
            // 보정 파일을 테이블 1번으로 로딩
            //success &= rtc.CtlLoadCorrectionFile(CorrectionTableIndex.Table1, targetFile);
            // 테이블1 번을 1번 스캐너(Primary Head)에 지정
            //success &= rtc.CtlSelectCorrection(CorrectionTableIndex.Table1);
            return correction.ResultMessage;
        }

        /// <summary>
        /// 2D 스캐너 보정 실시 (직사각형 영역)
        /// </summary>
        /// <returns></returns>
        private static string CreateFieldCorrection2()
        {
            //현재 스캐너 보정 파일
            var srcFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ctb");
            //신규로 생성할 스캐너 보정 파일
            var targetFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", $"newfile.ctb");

            // 3x5 (15개) 위치에 대한 보정 테이블 입력 용 / 간격 10 mm
            var correction = new Correction2DRtc(kfactor16bits, 3, 5, 10, 10, srcFile, targetFile);

            // 좌 상단 부터 오른쪽 방향으로 !
            //
            // 0,0  0,1  0,2 ...
            // 1,0  1,1  1,2 ...
            // 2,0  2,1  2,2 ...
            // 3,0  3,1  3,2 ...
            // ...

            #region inputs relative error deviation : 상대적인 오차위치 값을 넣는 방법 (머신 비전 오차값을 넣는 것과 유사)
            // 해당 X,Y 위치에서의 오차값 dx, dy 입력
            correction.AddRelative(0, 0, new Vector2(-20, 10), new Vector2(0.01f, 0.01f));
            correction.AddRelative(0, 1, new Vector2(-10, 10), new Vector2(0.01f, 0.01f));
            correction.AddRelative(0, 2, new Vector2(0, 10), new Vector2(0.01f, 0.01f));
            correction.AddRelative(0, 3, new Vector2(10, 10), new Vector2(0.01f, 0.01f));
            correction.AddRelative(0, 4, new Vector2(20, 10), new Vector2(0.01f, 0.01f));

            correction.AddRelative(1, 0, new Vector2(-20, 0), new Vector2(0.01f, 0.01f));
            correction.AddRelative(1, 1, new Vector2(-10, 0), new Vector2(0.01f, 0.01f));
            correction.AddRelative(1, 2, new Vector2(0, 0), new Vector2(0.01f, 0.01f));
            correction.AddRelative(1, 3, new Vector2(10, 0), new Vector2(0.01f, 0.01f));
            correction.AddRelative(1, 4, new Vector2(20, 0), new Vector2(0.01f, 0.01f));

            correction.AddRelative(2, 0, new Vector2(-20, -10), new Vector2(0.01f, 0.01f));
            correction.AddRelative(2, 1, new Vector2(-10, -10), new Vector2(0.01f, 0.01f));
            correction.AddRelative(2, 2, new Vector2(0, -10), new Vector2(0.01f, 0.01f));
            correction.AddRelative(2, 3, new Vector2(10, -10), new Vector2(0.01f, 0.01f));
            correction.AddRelative(2, 4, new Vector2(20, -10), new Vector2(0.01f, 0.01f));
            #endregion

            //신규 보정 파일 생성 실시
            bool success = correction.Convert();
            //var rtc = ...;
            // 보정 파일을 테이블 1번으로 로딩
            //success &= rtc.CtlLoadCorrectionFile(CorrectionTableIndex.Table1, targetFile);
            // 테이블1 번을 1번 스캐너(Primary Head)에 지정
            //success &= rtc.CtlSelectCorrection(CorrectionTableIndex.Table1);
            return correction.ResultMessage;
        }

        private static void CreateFieldCorrectionWithWinForms()
        {
            //현재 스캐너 보정 파일
            var srcFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ctb");
            //신규로 생성할 스캐너 보정 파일
            var targetFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", $"newfile.ctb");

            // 3x3 (9개) 위치에 대한 보정 테이블 입력 용
            var correction = new Correction2DRtc(kfactor16bits, 3, 3, 20, 20, srcFile, targetFile);

            // 좌 상단 부터 오른쪽 방향으로 !
            //
            // 0,0  0,1  0,2 ...
            // 1,0  1,1  1,2 ...
            // 2,0  2,1  2,2 ...
            // 3,0  3,1  3,2 ...
            // ...

            #region inputs relative error deviation : 상대적인 오차위치 값을 넣는 방법 (머신 비전 오차값을 넣는 것과 유사)
            correction.AddRelative(0, 0, new Vector2(-20, 20), new Vector2(0.01f, 0.01f));
            correction.AddRelative(0, 1, new Vector2(0, 20), new Vector2(0.002f, 0.001f));
            correction.AddRelative(0, 2, new Vector2(20, 20), new Vector2(-0.0051f, 0.01f));
            correction.AddRelative(1, 0, new Vector2(-20, 0), new Vector2(0.01f, 0.01f));
            correction.AddRelative(1, 1, new Vector2(0, 0), new Vector2(0.0f, 0.0f));
            correction.AddRelative(1, 2, new Vector2(20, 0), new Vector2(-0.01f, 0.01f));
            correction.AddRelative(2, 0, new Vector2(-20, -20), new Vector2(0.01f, 0.002f));
            correction.AddRelative(2, 1, new Vector2(0, -20), new Vector2(0.005f, -0.003f));
            correction.AddRelative(2, 2, new Vector2(20, -20), new Vector2(0.002f, -0.008f));
            #endregion

            var form = new Correction2DRtcForm(rtc, correction);
            // or
            //var form = new Correction2DRtcForm(correction);
            //form.OnApply += Form_OnApply;
            form.ShowDialog();
        }

        //private static void Form_OnApply(object sender, EventArgs e)
        //{
        //    var form = sender as Correction2DRtcForm;
        //    var index = form.Index;
        //    string ctFileName = form.RtcCorrection.TargetCorrectionFile;
        //    if (!File.Exists(ctFileName))
        //    {
        //        Logger.Log(Logger.Type.Error, $"try to change correction file but not exist : {ctFileName}");
        //        return;
        //    }
        //    if (DialogResult.Yes != MessageBox.Show($"Do you really want to apply new correction file {ctFileName} ?", "Warning !", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        //        return;

        //    bool success = true;
        //    //해당 보정파일을 RTC 제어기 메모리 안으로 로드후 선택
        //    success &= rtc.CtlLoadCorrectionFile(CorrectionTableIndex.Table1, ctFileName);
        //    success &= rtc.CtlSelectCorrection(CorrectionTableIndex.Table1);
        //    if (success)
        //    {
        //        // 권장) ctFileName 파일 정보를 외부 설정 파일에 저장
        //        Logger.Log(Logger.Type.Warn, $"correction file has changed to {ctFileName}");
        //        MessageBox.Show($"Success to load and selected field correction file to {ctFileName}", "Scanner Field Correction File", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    else
        //    {
        //        MessageBox.Show($"Fail to load and selected field correction file to {ctFileName}", "Scanner Field Correction File", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }
        //}
    }
}
