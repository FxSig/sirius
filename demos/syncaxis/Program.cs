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
 * SyncAxis 를 이용한 MOTF
 * ExcelliSCAN + ACS Controller 조합의 고정밀 가공기법
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
            //var rtc = new Rtc5(0); //create Rtc5 controller
            //var rtc = new Rtc6(0); //create Rtc6 controller
            //var rtc = new Rtc6Ethernet(0, "192.168.0.100", "255.255.255.0"); //Scanlab Rtc6 Ethernet 제어기
            var rtc = new Rtc6SyncAxis(); // Scanlab XLSCAN 솔류션
            string configXmlFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "syncaxis", "syncAXISConfig.xml");
            rtc.Initialize(configXmlFileName);
            rtc.CtlFrequency(50 * 1000, 2); // laser frequency : 50KHz, pulse width : 2usec
            rtc.CtlSpeed(100, 100); // default jump and mark speed : 100mm/s
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
                Console.WriteLine("Testcase for spirallab.sirius. powered by labspiral@gmail.com (http://spirallab.co.kr)");
                Console.WriteLine("");
                Console.WriteLine("'S' : simulation mode enabled");
                Console.WriteLine("'H' : hardware mode enabled");
                Console.WriteLine("'C' : job characteristic");
                Console.WriteLine("'F1' : draw rectangle 2D with scanner only");
                Console.WriteLine("'F2' : draw rectangle 2D with stage only");
                Console.WriteLine("'F3' : draw rectangle 2D with scanner and stage");
                Console.WriteLine("'F10' : get status");
                Console.WriteLine("'F11' : reset");
                Console.WriteLine("'F12' : abort");
                Console.WriteLine("'M' : move stage x and y");
                Console.WriteLine("'Q' : quit");
                Console.WriteLine("");
                Console.Write("select your target : ");
                key = Console.ReadKey(false);
                if (key.Key == ConsoleKey.Q)
                    break;
                Console.WriteLine("");
                switch (key.Key)
                {
                    case ConsoleKey.S:
                        rtc.CtlSimulationMode(true);
                        break;
                    case ConsoleKey.H:
                        rtc.CtlSimulationMode(false);
                        break;
                    case ConsoleKey.F1:
                        Draw(rtc, laser, MotionType.ScannerOnly);
                        break;
                    case ConsoleKey.F2:
                        Draw(rtc, laser, MotionType.StageOnly);
                        break;
                    case ConsoleKey.F3:
                        //band width 변경 가능
                        //rtc.CtlBandWidth(2.0f); 
                        //멀티헤드 사용시 개별 헤드별 오프셋 처리 가능
                        //rtc.CtlHeadOffset(ScanDevice.ScanDevice1, new Vector2(0.1f, 0.2f), 5);
                        //rtc.CtlHeadOffset(ScanDevice.ScanDevice2, new Vector2(-0.1f, -0.2f), -5);
                        Draw(rtc, laser, MotionType.StageAndScanner);
                        break;
                    case ConsoleKey.F10:
                        if (rtc.CtlGetStatus(RtcStatus.Busy))
                            Console.WriteLine("rtc is busy now ...");
                        else
                            Console.WriteLine("rtc is not busy ...");

                        rtc.CtlGetDynamicsConfig(out var dynamics);
                        Console.WriteLine(dynamics.ToString());
                        rtc.CtlGetTrajectory(out var trajectory);
                        Console.WriteLine(trajectory.ToString());
                        {
                            rtc.CtlGetScannerPosition(ScanDevice.ScanDevice1, out float x, out float y);
                            Console.WriteLine($"scanner x= {x:F3}, y= {y:F3}");
                        }
                        {
                            rtc.CtlGetStagePosition(out float x, out float y);
                            Console.WriteLine($"stage x= {x:F3}, y= {y:F3}");
                        }

                        Console.WriteLine($"band width= {rtc.BandWidth}");
                        Console.WriteLine($"simulation mode= {rtc.IsSimulationMode} with {rtc.SimulationFileName}");

                        break;
                    case ConsoleKey.F11:
                        rtc.CtlReset();
                        break;
                    case ConsoleKey.F12:
                        rtc.CtlAbort();
                        break;
                    case ConsoleKey.C:
                        PrintJobCharacteristic(rtc);
                        break;
                    case ConsoleKey.M:
                        rtc.StageMoveSpeed = 10;
                        rtc.StageMoveTimeOut = 5;
                        rtc.CtlSelectStage(Stage.Stage1);
                        rtc.CtlSetStagePosition(10, 10);
                        break;
                }
            } while (true);
        }

        static void Draw(IRtc rtc, ILaser laser, MotionType motionType)
        {
            bool success = true;
            var rtcSyncAxis = rtc as IRtcSyncAxis;
            success &= rtcSyncAxis.ListBegin(laser, motionType);
            success &= rtc.ListJump(new Vector2(50, 50));
            success &= rtc.ListMark(new Vector2(100, 50));
            success &= rtc.ListMark(new Vector2(100, 100));
            success &= rtc.ListMark(new Vector2(50, 100));
            success &= rtc.ListMark(new Vector2(50, 50));
            if (success)
            {
                success &= rtc.ListEnd();
                success &= rtc.ListExecute(false);
            }
        }

        static void PrintJobCharacteristic(Rtc6SyncAxis rtc)
        {            
            Console.WriteLine($"{rtc.Job.ToString()}");
        }
    }
}
