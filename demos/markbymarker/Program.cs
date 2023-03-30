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
 * Using 3x3 matrix stack 
 * 행렬을 사용하여 회전하면서 직선, 사각형의 가공을 실시한다.
 * Author : hong chan, choi / hcchoi@spirallab.co.kr (http://spirallab.co.kr)
 * 
 */

using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // initialize sirius library
            SpiralLab.Core.Initialize();

            #region initialize RTC 
            // RTC 제어기의 동작 로그를 기록할 파일을 지정
            var rtcOutputFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rtcOutput.txt");
            // create Rtc for dummy (가상 RTC 카드)
            //var rtc = new RtcVirtual(0); 
            // create Rtc5 controller
            var rtc = new Rtc5(0, rtcOutputFile);
            //rtc.InitLaser12SignalLevel = RtcSignalLevel.ActiveHigh;
            //rtc.InitLaserOnSignalLevel = RtcSignalLevel.ActiveHigh;
            // create Rtc6 controller
            //var rtc = new Rtc6(0); 
            // create Rtc6 Ethernet controller
            //var rtc = new Rtc6Ethernet(0, "192.168.0.100", "255.255.255.0"); 

            // theoretically size of scanner field of view (이론적인 FOV 크기) : 60mm
            float fov = 60.0f;
            // RTC4: k factor (bits/mm) = 2^16 / fov
            //float kfactor = (float)Math.Pow(2, 16) / fov;
            // RTC5/6: k factor (bits/mm) = 2^20 / fov
            float kfactor = (float)Math.Pow(2, 20) / fov;
            // RTC4: full path of correction file
            //var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ctb");
            // RTC5/6: full path of correction file
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            // initialize rtc controller
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);
            // laser frequency: 50KHz, pulse width : 2usec(주파수 50KHz, 펄스폭 2usec)
            rtc.CtlFrequency(50 * 1000, 2);
            // scanner jump and mark speed : 500mm/s (점프, 마크 속도 500mm/s)
            rtc.CtlSpeed(500, 500);
            // laser and scanner delays (레이저/스캐너 지연값 설정)
            rtc.CtlDelay(10, 100, 200, 200, 0);
            #endregion

            #region initialize Laser (virtual)
            // virtual laser source with max 20W power (최대 출력 20W 의 가상 레이저 소스 생성)
            var laser = new LaserVirtual(0, "virtual", 20);
            laser.PowerControlMethod = PowerControlMethod.Unknown;
            //var laser = new IPGYLPTypeD(0, "IPG YLP D", 1, 20);
            //var laser = new IPGYLPTypeE(0, "IPG YLP E", 1, 20);
            //var laser = new IPGYLPN(0, "IPG YLP N", 1, 100);
            //var laser = new JPTTypeE(0, "JPT Type E", 1, 20);
            //var laser = new SPIG4(0, "SPI G3/4", 1, 20);
            //var laser = new PhotonicsIndustryDX(0, "DX", 1, 20);
            //var laser = new PhotonicsIndustryRGHAIO(0, "RGHAIO", 1, 20);
            //var laser = new AdvancedOptoWaveFotia(0, "Fotia", 1, 20);
            //var laser = new AdvancedOptoWaveAOPico(0, "AOPico", 1, 20);
            //var laser = new CoherentAviaLX(0, "Avia LX", 1, 20);
            //var laser = new CoherentDiamondJSeries(0, "Diamond JSeries", "10.0.0.1", 200.0f);
            //var laser = new CoherentDiamondCSeries(0, "Diamond CSeries", 1, 100.0f);
            //var laser = new SpectraPhysicsHippo(0, "Hippo", 1, 30);
            //var laser = new SpectraPhysicsTalon(0, "Talon", 1, 30);

            // assign RTC instance at laser 
            laser.Rtc = rtc;
            // initialize laser source
            laser.Initialize();
            // set basic power output to 2W
            laser.CtlPower(2);
            #endregion

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("Testcase for spirallab.sirius. powered by hcchoi@spirallab.co.kr (http://spirallab.co.kr)");
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("'R' : draw rectangle with rotate");
                Console.WriteLine("'L' : draw lines with rotate");
                Console.WriteLine("'F' : pop up laser source form");
                Console.WriteLine("'Q' : quit");
                Console.WriteLine("");
                Console.Write("select your target : ");
                key = Console.ReadKey(false);
                if (key.Key == ConsoleKey.Q)
                    break;
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("WARNING !!! LASER IS BUSY ...");
                Console.WriteLine($"{Environment.NewLine}");
                var timer = Stopwatch.StartNew();
                switch (key.Key)
                {
                    case ConsoleKey.R:  
                        // 회전하는 사각형 모양 가공
                        // 가로 10, 세로 10 크기, 0 ~360 각도의 회전 형상
                        DrawRectangle(laser, rtc, 10, 10, 0, 360);
                        break;
                    case ConsoleKey.L:  
                        // 회전하는 직선 모양 가공
                        DrawLinesWithRotate(laser, rtc, 0, 360);
                        break;
                    case ConsoleKey.F:
                        // popup winforms for control laser source
                        // 레이저 소스 제어용 윈폼 팝업
                        SpiralLab.Sirius.Laser.LaserForm laserForm = new SpiralLab.Sirius.Laser.LaserForm(laser);
                        laserForm.ShowDialog();
                        break;
                }
                Console.WriteLine($"Processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s");
            } while (true);

            rtc.CtlAbort();
            rtc.Dispose();
            laser.Dispose();
        }
       
        /// <summary>
        /// 지정된 크기의 직사각형 그리기
        /// </summary>
        /// <param name="rtc"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private static bool DrawRectangle(ILaser laser, IRtc rtc, double width, double height, double angleStart, double angleEnd)
        {
            bool success = true;
            success &= rtc.ListBegin(laser);
            for (double angle = angleStart; angle <= angleEnd; angle += 1)
            {
                // push rotate matrix into stack
                // 회전 행렬 스택에 삽입
                rtc.MatrixStack.Push(angle);
                success &= rtc.ListJump(new Vector2((float)-width / 2, (float)height / 2));
                success &= rtc.ListMark(new Vector2((float)width / 2, (float)height / 2));
                success &= rtc.ListMark(new Vector2((float)width / 2, (float)-height / 2));
                success &= rtc.ListMark(new Vector2((float)-width / 2, (float)-height / 2));
                success &= rtc.ListMark(new Vector2((float)-width / 2, (float)height / 2));
                // pop rotate matrix from stack
                //회전 행렬 스택에서 제거
                rtc.MatrixStack.Pop();
            }
            if (success)
            {
                success &= rtc.ListEnd();
                success &= rtc.ListExecute(true);
            }
            return success;
        }       
        /// <summary>
        /// 행렬을 이용해 직선을 그릴때 1도마다 직선을 회전시켜 그리기
        /// </summary>
        /// <param name="rtc"></param>
        /// <param name="angleStart"></param>
        /// <param name="angleEnd"></param>
        private static bool DrawLinesWithRotate(ILaser laser, IRtc rtc, double angleStart, double angleEnd)
        {
            bool success = true;
            success &= rtc.ListBegin(laser);
            // push transit matrix into stack
            // 이동 행렬 스택에 삽입
            // dx= 2mm, dy= 4mm 만큼 이동
            rtc.MatrixStack.Push(2, 4);
            for (double angle = angleStart; angle <= angleEnd; angle += 1)
            {
                // push rotate matrix into stack
                // 회전 행렬 스택에 삽입
                rtc.MatrixStack.Push(angle);
                success &= rtc.ListJump(new Vector2(-10, 0));
                success &= rtc.ListMark(new Vector2(10, 0));
                // pop rotate matrix from stack
                // 회전 행렬 스택에서 제거
                rtc.MatrixStack.Pop();
            }
            // pop transit matrix from stack
            // 이동 행렬 스택에서 제거
            rtc.MatrixStack.Pop();

            if (success)
            {
                success &= rtc.ListEnd();
                success &= rtc.ListExecute(true);
            }
            return success;
        }
    }
}
