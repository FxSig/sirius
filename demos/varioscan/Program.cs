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
 * 3D 가공용 (varioscan/excelliSHIFT) IRtc3D 인터페이스를 사용한다
 * Author : hong chan, choi / sepwind @gmail.com(https://sepwind.blogspot.com)
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
            //var rtc = new RtcVirtual(0); ///create Rtc for dummy
            //var rtc = new Rtc5(0); ///create Rtc5 controller
            //var rtc = new Rtc6(0); ///create Rtc6 controller
            //var rtc = new Rtc6Ethernet(0, "192.168.0.200"); ///create Rtc6 ethernet controller
            var rtc = new Rtc53D(0); ///create Rtc5 + 3D option controller
            //var rtc = new Rtc63D(0); ///create Rtc5 + 3D option controller
            //var rtc = new Rtc5DualHead(0); ///create Rtc5 + Dual head option controller
            //var rtc = new Rtc5MOTF(0); ///create Rtc5 + MOTF option controller
            //var rtc = new Rtc6MOTF(0); ///create Rtc6 + MOTF option controller
            //var rtc = new Rtc6SyncAxis(0, "syncAXISConfig.xml"); ///create Rtc6 + XL-SCAN (ACS+SYNCAXIS) option controller

            float fov = 60.0f;    /// scanner field of view : 60mm            
            float kfactor = (float)Math.Pow(2, 20) / fov; /// k factor (bits/mm) = 2^20 / fov
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);    /// 스캐너 보정 파일 지정 : correction file
            rtc.CtlFrequency(50 * 1000, 2); /// laser frequency : 50KHz, pulse width : 2usec
            rtc.CtlSpeed(100, 100); /// default jump and mark speed : 100mm/s
            rtc.CtlDelay(10, 100, 200, 200, 0); /// scanner and laser delays

            var rtc3D = rtc as IRtc3D;
            #endregion

            #region initialize Laser (virtial)
            ILaser laser = new LaserVirtual(0, "virtual", 20);
            #endregion

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Testcase for spirallab.sirius. powered by labspiral@gmail.com (https://sepwind.blogspot.com)");
                Console.WriteLine("옵셋(Offset)은 스캐너의 초점이 해당 Z 위치로 이동되며, 이때 위치 보정을 위한 3D correction 파일이 적용된다.");
                Console.WriteLine("디포커스(Defocus)은 스캐너의 초점이 해당 Z 위치로 이동되나, 이때 위치 보정을 위한 3D correction 파일은 미 사용된다.");
                Console.WriteLine("");
                Console.WriteLine("'A' : reset z offset");
                Console.WriteLine("'B' : z offset to 1mm");
                Console.WriteLine("'C' : z offset to -1mm");
                Console.WriteLine("'D' : reset z defocus");
                Console.WriteLine("'E' : z decocus to 1mm");
                Console.WriteLine("'F' : z decocus to -1mm");
                Console.WriteLine("'S' : show dialog");
                Console.WriteLine("'Q' : quit");
                Console.WriteLine("");
                Console.Write("select your target : ");
                key = Console.ReadKey(false);
                if (key.Key == ConsoleKey.Q)
                    break;
                Console.WriteLine("\r\nWARNING !!! LASER IS BUSY ...");
                var timer = Stopwatch.StartNew();
                switch (key.Key)
                {
                    case ConsoleKey.A:
                        /// z 축의 오프셋 값을 0으로 설정
                        rtc3D.CtlZOffset(0);
                        break;
                    case ConsoleKey.B:
                        /// z 축의 오프셋 값을 1으로 설정
                        rtc3D.CtlZOffset(1.0f);
                        break;
                    case ConsoleKey.C:
                        /// z 축의 오프셋 값을 -1으로 설정
                        rtc3D.CtlZOffset(-1.0f);
                        break;
                    case ConsoleKey.D:
                        /// z 축의 defocus 값을 0으로 설정
                        rtc3D.CtlZDefocus(0);
                        break;
                    case ConsoleKey.E:
                        /// z 축의 defocus 값을 1으로 설정
                        rtc3D.CtlZDefocus(1.0f);
                        break;
                    case ConsoleKey.F:
                        /// z 축의 defocus 값을 -1으로 설정
                        rtc3D.CtlZDefocus(-1.0f);
                        break;
                    case ConsoleKey.S:
                        /// IRtc3D 의 폼 대화상자를 출력
                        rtc.Form.Show();
                        break;
                    case ConsoleKey.Q:
                        DrawCircleWith3D(laser, rtc, rtc3D);
                        break;
                }

                Console.WriteLine($"processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s");
            } while (true);

            rtc.Dispose();
        }
        private static void DrawCircleWith3D(ILaser laser, IRtc rtc, IRtc3D rtc3D)
        {
            rtc.ListBegin(laser);
            //draw circle in z = +1 mm
            rtc3D.ListJump3D(new Vector3((float)10, 0, 1)); 
            rtc3D.ListArc3D(new Vector3(0, 0, 1), 360.0f);
            rtc.ListEnd();
            rtc.ListExecute(); 
        }

    }
}
