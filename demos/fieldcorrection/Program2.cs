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
 * 3D 영역에 대해 상하부 각각
 * 3x3 (9개) 영역에 대한 스캐너 필드 왜곡 보정을 실시한다.
 * 나선 모양을 객체(Spiral Entity)를 생성하여 매 9개 위치에 각각 레이저를 출사한다.
 * 머신 비전등의 계측 장치로 측정된 9개의 위치 * 2 (상하부) 오차값을 사용한 새로운 스캐너 보정파일 생성한다.
 * Author : hong chan, choi / labspiral@gmail.com(http://spirallab.co.kr)
 * 
 */


using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;

namespace SpiralLab.Sirius
{
    class Program2
    {
        static float kfactor = (float)Math.Pow(2, 20) / 60.0f;
        static void Main2(string[] args)
        {
            SpiralLab.Core.Initialize();

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Testcase for spirallab.sirius. powered by labspiral@gmail.com (http://spirallab.co.kr)");
                Console.WriteLine("");
                Console.WriteLine("'C' : create new field correction for 3D");
                Console.WriteLine("'F' : create new field correction for 3D with WinForms");
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
            var correction = new RtcCorrection3D(kfactor, 3, 3, interval, 5, -5, srcFile, targetFile);

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
            var correction = new RtcCorrection3D(kfactor, 3, 3, interval, 5, -5, srcFile, targetFile);

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

            var form = new Correction3DForm(correction);
            form.ShowDialog();
        }
    }
}
