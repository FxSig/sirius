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
 * Author : hong chan, choi / labspiral @gmail.com(http://spirallab.co.kr)
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
            //var rtc = new Rtc6Ethernet(0, "192.168.0.100", "255.255.255.0"); //실험적인 상태 (Scanlab Rtc6 Ethernet 제어기)
            var rtc = new Rtc6SyncAxis(0, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "syncaxis", "syncAXISConfig.xml")); //실험적인 상태 (Scanlab XLSCAN 솔류션)

            rtc.Initialize(0, 0, string.Empty);
            rtc.CtlFrequency(50 * 1000, 2); // laser frequency : 50KHz, pulse width : 2usec
            rtc.CtlSpeed(100, 100); // default jump and mark speed : 100mm/s
            #endregion

            #region initialize Laser (virtual)
            ILaser laser = new LaserVirtual(0, "virtual", 20);  /// 최대 출력 20W 의 가상 레이저 소스 생성
            #endregion

            
            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Testcase for spirallab.sirius. powered by labspiral@gmail.com (http://spirallab.co.kr)");
                Console.WriteLine("");
                Console.WriteLine("'F1' : draw rectangle 2D with scanner only");
                Console.WriteLine("'F2' : draw rectangle 2D with stage only");
                Console.WriteLine("'F3' : draw rectangle 2D with scanner and stage");
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
                    case ConsoleKey.F1:
                        Draw(rtc, laser, MotionType.ScannerOnly);
                        break;
                    case ConsoleKey.F2:
                        Draw(rtc, laser, MotionType.StageOnly);
                        break;
                    case ConsoleKey.F3:
                        Draw(rtc, laser, MotionType.StageAndScanner);
                        break;
                    case ConsoleKey.M:
                        rtc.StageMoveSpeed = 10;
                        rtc.StageMoveTimeOut = 5;
                        rtc.CtlMove(MotionType.StageOnly, 10, 10);
                        break;
                }

            } while (true);
        }

        static void Draw(IRtc rtc, ILaser laser, MotionType motionType)
        {
            var rtcSyncAxis = rtc as IRtcSyncAxis;
            rtcSyncAxis.ListBegin(laser, motionType);

            rtc.ListJump(new Vector2(50, 50));
            rtc.ListMark(new Vector2(100, 50));
            rtc.ListMark(new Vector2(100, 100));
            rtc.ListMark(new Vector2(50, 100));
            rtc.ListMark(new Vector2(50, 50));

            rtc.ListEnd();
            rtc.ListExecute(true);
        }
    }
}
