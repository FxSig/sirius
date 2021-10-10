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
 * IRtc 인터페이스와 함께 행렬 사용
 * 
 * IRtc 인터페이스 및 행렬 사용법
 * 행렬을 사용하여 회전하면서 직선, 사각형의 가공을 실시한다.
 * Author : hong chan, choi / labspiral@gmail.com(http://spirallab.co.kr)
 * 
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;

namespace SpiralLab.Sirius
{
    class Program
    {
        static void Main(string[] args)
        {
            SpiralLab.Core.Initialize();

            #region initialize RTC 
            //var rtc = new RtcVirtual(0); //create Rtc for dummy
            var rtc = new Rtc5(0); //create Rtc5 controller
            //var rtc = new Rtc6(0); //create Rtc6 controller
            //var rtc = new Rtc6Ethernet(0, "192.168.0.100", "255.255.255.0"); //실험적인 상태 (Scanlab Rtc6 Ethernet 제어기)
            //var rtc = new Rtc6SyncAxis(0, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "syncAxis", "syncAXISConfig.xml")); //실험적인 상태 (Scanlab XLSCAN 솔류션)

            float fov = 60.0f;    // scanner field of view : 60mm            
            float kfactor = (float)Math.Pow(2, 20) / fov; // k factor (bits/mm) = 2^20 / fov
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);    // 스캐너 보정 파일 지정 : correction file
            //signal level : if active low
            //var ctrlSignal = new Rtc5LaserControlSignal();
            //ctrlSignal.Add(Rtc5LaserControlSignal.Bit.Laser12SignalLevelLow);
            //ctrlSignal.Add(Rtc5LaserControlSignal.Bit.LaserOnSignalLevelLow);
            //rtc.CtlLaserSignalLevel(ctrlSignal);
            rtc.CtlFrequency(50 * 1000, 2); // laser frequency : 50KHz, pulse width : 2usec
            rtc.CtlSpeed(100, 100); // default jump and mark speed : 100mm/s
            rtc.CtlDelay(10, 100, 200, 200, 0); // scanner and laser delays
            #endregion

            #region initialize Laser (virtual)
            var laser = new LaserVirtual(0, "virtual", 20);  // virtual laser source with max 20W power (최대 출력 20W 의 가상 레이저 소스 생성)
            //var laser = new IPGYLP(0, "IPG YLP", 1, 20);
            //var laser = new JPTTypeE(0, "JPT Type E", 1, 20);
            //var laser = new SPIG4(0, "SPI G3/4", 1, 20);
            //var laser = new PhotonicsIndustryDX(0, "PI", 1, 20);
            //var laser = new AdvancedOptoWaveFotia(0, "Fotia", 1, 20);
            //var laser = new CoherentAviaLX(0, "Avia LX", 1, 20);
            laser.Rtc = rtc;
            laser.Initialize();
            laser.CtlPower(2);
            #endregion

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("Testcase for spirallab.sirius. powered by labspiral@gmail.com (http://spirallab.co.kr)");
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
                    case ConsoleKey.R:  // 회전하는 사각형 모양 가공 (가로 10, 세로 10 크기, 0 ~360 각도의 회전 형상)
                        DrawRectangle(laser, rtc, 10, 10, 0, 360);
                        break;
                    case ConsoleKey.L:  //회전하는 직선 모양 가공
                        DrawLinesWithRotate(laser, rtc, 0, 360);
                        break;
                    case ConsoleKey.F:
                        SpiralLab.Sirius.LaserForm laerForm = new SpiralLab.Sirius.LaserForm();
                        laerForm.Laser = laser;
                        laerForm.ShowDialog();
                        break;
                }
                Console.WriteLine($"Processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s");
            } while (true);

            rtc.Dispose();
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
                //회전 각도를 행렬 스택에 push
                rtc.MatrixStack.Push(angle);
                success &= rtc.ListJump(new Vector2((float)-width / 2, (float)height / 2));
                success &= rtc.ListMark(new Vector2((float)width / 2, (float)height / 2));
                success &= rtc.ListMark(new Vector2((float)width / 2, (float)-height / 2));
                success &= rtc.ListMark(new Vector2((float)-width / 2, (float)-height / 2));
                success &= rtc.ListMark(new Vector2((float)-width / 2, (float)height / 2));
                //이전에 push 된 행렬값을 pop 하여 삭제
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
            rtc.MatrixStack.Push(2, 4); // dx =2, dy=4 만큼 이동
            for (double angle = angleStart; angle <= angleEnd; angle += 1)
            {
                rtc.MatrixStack.Push(angle);
                success &= rtc.ListJump(new Vector2(-10, 0));
                success &= rtc.ListMark(new Vector2(10, 0));
                rtc.MatrixStack.Pop();
            }
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
