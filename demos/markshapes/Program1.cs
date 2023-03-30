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
 * Initialize sirius library and mark simple shapes
 * 시리우스 라이브러리를 초기화 하고 간단한 도형을 마킹하는 예제
 *  
 * Author : hong chan, choi / hcchoi@spirallab.co.kr (http://spirallab.co.kr)
 * 
 */


using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using SpiralLab.Sirius;

namespace SpiralLab.Sirius
{
    class Program1
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // initialize sirius library
            SpiralLab.Core.Initialize();

            #region initialize RTC 
            // create Rtc for dummy (가상 RTC 카드)
            //var rtc = new RtcVirtual(0); 
            // create Rtc5 controller
            var rtc = new Rtc5(0);
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
            // initialize RTC controller with laser mode YAG1
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);
            // laser frequency : 50KHz, pulse width : 2usec (주파수 50KHz, 펄스폭 2usec)
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
            //var laser = new JPTCWFiber(0, "JPT CW Fiber", 150);
            //var laser = new MaxPhotonicsCWFiber(0, "Max CW Fiber", 100);
            //var laser = new SPIG4(0, "SPI G3/4", 1, 20);
            //var laser = new PhotonicsIndustryDX(0, "DX", 1, 20);
            //var laser = new PhotonicsIndustryRGHAIO(0, "RGHAIO", 1, 20);
            //var laser = new AdvancedOptoWaveAOPicoPrecision(0, "AOPicoPrecision", 1, 15);
            //var laser = new AdvancedOptoWaveFotia(0, "Fotia", 1, 20);
            //var laser = new AdvancedOptoWaveAOPico(0, "AOPico", 1, 20);
            //var laser = new CoherentAviaLX(0, "Avia LX", 1, 20);
            //var laser = new CoherentDiamondJSeries(0, "Diamond JSeries", "10.0.0.1", 200.0f);
            //var laser = new CoherentDiamondCSeries(0, "Diamond CSeries", 1, 100.0f);
            //var laser = new SpectraPhysicsHippo(0, "Hippo", 1, 30);
            //var laser = new SpectraPhysicsTalon(0, "Talon", 1, 30);

            // assign RTC controller at laser 
            laser.Rtc = rtc;
            // initialize laser source
            laser.Initialize();
            // set power output to 2W as default
            laser.CtlPower(2);
            #endregion

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("Testcase for spirallab.sirius. powered by hcchoi@spirallab.co.kr (http://spirallab.co.kr)");
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("'F1' : draw circle");
                Console.WriteLine("'F2' : draw rectangle");
                Console.WriteLine("'Q' : quit");
                Console.WriteLine($"{Environment.NewLine}");
                Console.Write("select your target : ");
                key = Console.ReadKey(false);
                if (key.Key == ConsoleKey.Q)
                    break;
                Console.WriteLine($"{Environment.NewLine}");
                switch (key.Key)
                {
                    case ConsoleKey.F1:
                        Console.WriteLine("WARNING !!! LASER IS BUSY ...");
                        DrawCircles(rtc, laser);
                        break;
                    case ConsoleKey.F2:
                        Console.WriteLine("WARNING !!! LASER IS BUSY ...");
                        DrawRectangles(rtc, laser);
                        break;
                }
            } while (true);

            rtc.Dispose();
            laser.Dispose();
        }

        private static bool DrawRectangles(IRtc rtc, ILaser laser, float width = 20, float height = 20)
        {
            bool success = true;
            // list begin with single buffered list
            // max list commands limitation at single buffered list
            // RTC4: 8000 counts
            // RTC5: approx. 2^20 counts
            // RTC6: approx. 2^23 counts
            success &= rtc.ListBegin(laser, ListType.Single);
            success &= rtc.ListJump(-width / 2, height / 2);
            success &= rtc.ListMark(width / 2, height / 2);
            success &= rtc.ListMark(width / 2, height / 2);
            success &= rtc.ListMark(width / 2, -height / 2);
            success &= rtc.ListMark(-width / 2, -height / 2);
            success &= rtc.ListMark(-width / 2, height / 2);
            success &= rtc.ListEnd();
            if (success)
                success &= rtc.ListExecute(true);
            return success;
        }

        private static bool DrawCircles(IRtc rtc, ILaser laser, float radius = 10)
        {
            bool success = true;
            // list begin with single buffered list
            // max list commands limitation at single buffered list
            // RTC4: 8000 counts
            // RTC5: approx. 2^20 counts
            // RTC6: approx. 2^23 counts
            success &= rtc.ListBegin(laser, ListType.Single);
            success &= rtc.ListJump(-radius, 0);
            success &= rtc.ListArc(0, 0, 360);
            success &= rtc.ListEnd();
            if (success)
                success &= rtc.ListExecute(true);
            return success;
        }
    }
}
