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
 * 3D with varioscan/excelliSHIFT (3D 가공을 하기 위한 제어 기법)
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
    class Program
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
            //var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ctb");   //"D3_1128.ctb");
            // RTC5/6: full path of correction file
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5"); //"D3_1128.ct5");
            // initialize RTC controller
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);
            // laser frequency : 50KHz, pulse width : 2usec (주파수 50KHz, 펄스폭 2usec)
            rtc.CtlFrequency(50 * 1000, 2);
            // scanner jump and mark speed : 500mm/s (점프, 마크 속도 500mm/s)
            rtc.CtlSpeed(500, 500);
            // laser and scanner delays (레이저/스캐너 지연값 설정)
            rtc.CtlDelay(10, 100, 200, 200, 0);
            // RTC controller has 3D option 
            Debug.Assert(rtc.Is3D);
            #endregion

            var rtc3D = rtc as IRtc3D;
            Debug.Assert(null != rtc3D);

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

            // assign RTC controller at laser 
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
                Console.WriteLine($"----------------------------------------------------------------------------------------");
                Console.WriteLine("'S' : get status");
                Console.WriteLine("'O' : z offset");
                Console.WriteLine("'D' : z defocus");
                Console.WriteLine("'R' : reset status and z offset/defocus");
                Console.WriteLine("'F1' : laser on during 5 secs");
                Console.WriteLine("'F2' : mark square at specific z height");
                Console.WriteLine("'F3' : mark helix figure");
                Console.WriteLine("'Q' : quit");
                Console.Write("Select your target : ");
                key = Console.ReadKey(false);
                Console.WriteLine($"{Environment.NewLine}");
                if (key.Key == ConsoleKey.Q)
                    break;

                bool success = true;
                switch (key.Key)
                {
                    case ConsoleKey.S:  //RTC's status (상태 확인)
                        if (rtc.CtlGetStatus(RtcStatus.Busy))
                            Console.WriteLine($"Rtc is busy!");
                        if (!rtc.CtlGetStatus(RtcStatus.PowerOK))
                            Console.WriteLine($"Scanner power is not ok");
                        if (!rtc.CtlGetStatus(RtcStatus.PositionAckOK))
                            Console.WriteLine($"Scanner position is not acked");
                        if (!rtc.CtlGetStatus(RtcStatus.NoError))
                            Console.WriteLine($"Rtc status has an error");
                        break;
                    case ConsoleKey.O:
                        Console.Write("Z offset (mm) = ");
                        float zOffset = float.Parse(Console.ReadLine());
                        rtc3D.CtlZOffset(zOffset);
                        break;
                    case ConsoleKey.D:
                        Console.Write("Z defocus (mm) = ");
                        float zDefocus = float.Parse(Console.ReadLine());
                        rtc3D.CtlZDefocus(zDefocus);
                        break;
                    case ConsoleKey.R:
                        rtc3D.CtlZOffset(0);
                        rtc3D.CtlZDefocus(0);
                        rtc.CtlReset();
                        break;
                    case ConsoleKey.A:
                        rtc.CtlAbort();
                        break;
                    case ConsoleKey.F1:
                        success &= rtc.ListBegin(laser);
                        success &= rtc.ListLaserOn(5 * 1000);
                        success &= rtc.ListEnd();
                        if (success)
                            success &= rtc.ListExecute(false); //async
                        break;
                    case ConsoleKey.F2:
                        Console.Write("Z height (mm) = ");
                        float zHeight = float.Parse(Console.ReadLine());
                        float halfSquareSize = 10;
                        success &= rtc.ListBegin(laser);
                        success &= rtc3D.ListJump3D(-halfSquareSize, halfSquareSize, zHeight);
                        success &= rtc3D.ListMark3D(halfSquareSize, halfSquareSize, zHeight);
                        success &= rtc3D.ListJump3D(halfSquareSize, -halfSquareSize, zHeight);
                        success &= rtc3D.ListJump3D(-halfSquareSize, -halfSquareSize, zHeight);
                        success &= rtc3D.ListJump3D(-halfSquareSize, halfSquareSize, zHeight);
                        success &= rtc3D.ListJump3D(0, 0, 0);
                        success &= rtc.ListEnd();
                        if (success)
                            success &= rtc.ListExecute(false); //async
                        break;
                    case ConsoleKey.F3:
                        // helix height per revolution (mm) 
                        float helixHeightStep = 1.0f;
                        // helix revolution
                        int heliRevolution = 2;
                        // helix radius (mm)
                        float helixRadius = 10;

                        success &= rtc.ListBegin(laser);
                        for (int i = 0; i < heliRevolution; i++)
                        {
                            success &= rtc3D.ListJump3D(helixRadius, 0, i * helixHeightStep);
                            for (float angle = 10; angle < 360; angle += 10)
                            {
                                double x = helixRadius * Math.Sin(angle * Math.PI / 180.0);
                                double y = helixRadius * Math.Cos(angle * Math.PI / 180.0);
                                success &= rtc3D.ListMark3D((float)x, (float)y, i * helixHeightStep / (angle / 360.0f));
                                if (!success)
                                    break;
                            }
                            if (!success)
                                break;
                        }
                        success &= rtc.ListEnd();
                        success &= rtc3D.ListJump3D(0, 0, 0);
                        if (success)
                            success &= rtc.ListExecute(false); //async
                        break;
                }
            } while (true);

            Console.WriteLine("Terminating ...");
            if (rtc.CtlGetStatus(RtcStatus.Busy))
            {
                // abort marking operation
                rtc.CtlAbort();
                // wait until busy has finished
                rtc.CtlBusyWait();
            }
            rtc.Dispose();
            laser.Dispose();
        }

    }
}
