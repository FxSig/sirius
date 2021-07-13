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
 * 스캐너 필드 보정
 * 
 * 3x3 (9개) 영역에 대한 스캐너 필드 왜곡 보정을 실시한다.
 * 나선 모양을 객체(Spiral Entity)를 생성하여 매 9개 위치에 각각 레이저를 출사한다.
 * 머신 비전등의 계측 장치로 측정된 9개의 위치 오차값을 사용한 새로운 스캐너 보정파일 생성한다.
 * Author : hong chan, choi / labspiral@gmail.com(http://spirallab.co.kr)
 * 
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    class Program
    {
        static float kfactor = (float)Math.Pow(2, 20) / 60.0f;
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SpiralLab.Core.Initialize();

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Testcase for spirallab.sirius. powered by labspiral@gmail.com (http://spirallab.co.kr)");
                Console.WriteLine("");
                Console.WriteLine("'C' : create new field correction for 2D");
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
                    case ConsoleKey.F:
                        CreateFieldCorrectionWithWinForms();                        
                        break;
                }

            } while (true);

        }
      
        /// <summary>
        /// 2D 스캐너 보정 실시
        /// </summary>
        /// <returns></returns>
        private static string CreateFieldCorrection()
        {
            //현재 스캐너 보정 파일
            var srcFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            //신규로 생성할 스캐너 보정 파일
            var targetFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", $"newfile.ct5");

            // 2차원 스캐너 보정용 IRtcCorrection 객체 생성
            // 3x3 (9개) 위치에 대한 보정 테이블 입력 용
            var correction = new RtcCorrection2D(kfactor, 3, 3, 20, srcFile, targetFile);

            #region inputs relative error deviation : 상대적인 오차위치 값을 넣는 방법 (머신 비전 오차값을 넣는 것과 유사)
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

        private static void CreateFieldCorrectionWithWinForms()
        {
            //현재 스캐너 보정 파일
            var srcFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            //신규로 생성할 스캐너 보정 파일
            var targetFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", $"newfile.ct5");

            // 2차원 스캐너 보정용 IRtcCorrection 객체 생성
            // 3x3 (9개) 위치에 대한 보정 테이블 입력 용
            var correction = new RtcCorrection2D(kfactor, 3, 3, 20, srcFile, targetFile);

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

            var form = new Correction2DForm(correction);
            form.ShowDialog();
        }
    }
}
