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
 * 
 * IRtc 인터페이스를 직접 사용하는 방법
 * RTC5 카드를 초기화 하고 원, 사각형, 도트 원 을 그린다
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
            var rtc = new Rtc5(0); //create Rtc5 controller
            //var rtc = new Rtc6(0); //create Rtc6 controller
            //var rtc = new Rtc6Ethernet(0, "192.168.0.100", "255.255.255.0"); //실험적인 상태 (Scanlab Rtc6 Ethernet 제어기)
            //var rtc = new Rtc6SyncAxis(0, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configuration", "syncAXISConfig.xml")); //실험적인 상태 (Scanlab XLSCAN 솔류션)

            float fov = 60.0f;    // scanner field of view : 60mm            
            float kfactor = (float)Math.Pow(2, 20) / fov; // k factor (bits/mm) = 2^20 / fov
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);    // 스캐너 보정 파일 지정 : correction file
            rtc.CtlFrequency(50 * 1000, 2); // laser frequency : 50KHz, pulse width : 2usec
            rtc.CtlSpeed(100, 100); // default jump and mark speed : 100mm/s
            rtc.CtlDelay(10, 100, 200, 200, 0); // scanner and laser delays
            #endregion

            #region initialize Laser (virtual)
            ILaser laser = new LaserVirtual(0, "virtual", 20);  // 최대 출력 20W 의 가상 레이저 소스 생성
            #endregion

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Testcase for spirallab.sirius. powered by labspiral@gmail.com (http://spirallab.co.kr)");
                Console.WriteLine("");
                Console.WriteLine("'S' : get status");
                Console.WriteLine("'L' : draw line");
                Console.WriteLine("'C' : draw circle");
                Console.WriteLine("'R' : draw rectangle");
                Console.WriteLine("'D' : draw circle with dots");
                Console.WriteLine("'P' : draw square area with pixel operation");
                Console.WriteLine("'Q' : quit");
                Console.WriteLine("");
                Console.Write("select your target : ");
                key = Console.ReadKey(false);
                Console.WriteLine("");
                if (key.Key == ConsoleKey.Q)
                    break;
                var timer = Stopwatch.StartNew();
                switch (key.Key)
                {
                    case ConsoleKey.S:  //RTC의 상태 확인
                        if (rtc.CtlGetStatus(RtcStatus.Busy))
                            Console.WriteLine($"\r\nRtc is busy!");
                        else if (!rtc.CtlGetStatus(RtcStatus.PowerOK))
                            Console.WriteLine($"\r\nScanner power is not ok");
                        else if (!rtc.CtlGetStatus(RtcStatus.PositionAckOK))
                            Console.WriteLine($"\r\nScanner position is not acked");
                        else if (!rtc.CtlGetStatus(RtcStatus.NoError))
                            Console.WriteLine($"\r\nRtc status has an error");
                        else
                            Console.WriteLine($"\r\nIt's okay");
                        break;
                    case ConsoleKey.L:  // 선 모양 가공
                        Console.WriteLine("\r\nWARNING !!! LASER IS BUSY ...");
                        DrawLine(laser, rtc, -10,-10, 10, 10);
                        break;
                    case ConsoleKey.C:  // 원 모양 가공
                        Console.WriteLine("\r\nWARNING !!! LASER IS BUSY ...");
                        DrawCircle(laser, rtc, 10);
                        break;
                    case ConsoleKey.R:  // 사각형 모양 가공
                        Console.WriteLine("\r\nWARNING !!! LASER IS BUSY ...");
                        DrawRectangle(laser, rtc, 10, 10);
                        break;
                    case ConsoleKey.D:  //점으로 이루어진 원 모양 가공
                        Console.WriteLine("\r\nWARNING !!! LASER IS BUSY ...");
                        DrawCircleWithDots(laser, rtc, 10, 1.0f);
                        break;
                    case ConsoleKey.P:  // 픽셀 이루어진 사각 모양 가공
                        Console.WriteLine("\r\nWARNING !!! LASER IS BUSY ...");
                        DrawSquareAreaWithPixels(laser, rtc, 10, 0.2f);
                        break;
                }
                Console.WriteLine($"Processing time = {timer.ElapsedMilliseconds/1000.0:F3}s");     
            } while (true);

            rtc.Dispose();
        }        
        /// <summary>
        /// draw circle
        /// </summary>
        /// <param name="rtc"></param>
        /// <param name="radius"></param>
        private static void DrawCircle(ILaser laser, IRtc rtc, float radius)
        {
            rtc.ListBegin(laser);
            rtc.ListJump(new Vector2(radius, 0));
            rtc.ListArc(new Vector2(0, 0), 360.0f);
            rtc.ListEnd();
            rtc.ListExecute(true);
        }

        /// <summary>
        /// draw rectangle
        /// </summary>
        /// <param name="rtc"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        private static void DrawLine(ILaser laser, IRtc rtc, float x1, float y1, float x2, float y2)
        {
            rtc.ListBegin(laser);
            rtc.ListJump(new Vector2(x1, y1));
            rtc.ListMark(new Vector2(x2, y2));
            rtc.ListEnd();
            rtc.ListExecute(true);
        }
        /// <summary>
        /// draw rectangle
        /// </summary>
        /// <param name="rtc"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private static void DrawRectangle(ILaser laser, IRtc rtc, float width, float height)
        {
            rtc.ListBegin(laser);
            rtc.ListJump(new Vector2(-width / 2, height / 2));
            rtc.ListMark(new Vector2(width / 2, height / 2));
            rtc.ListMark(new Vector2(width / 2, -height / 2));
            rtc.ListMark(new Vector2(-width / 2, -height / 2));
            rtc.ListMark(new Vector2(-width / 2, height / 2));
            rtc.ListEnd();
            rtc.ListExecute(true);
        }
        /// <summary>
        /// draw cicle with dots
        /// </summary>
        /// <param name="rtc"></param>
        /// <param name="radius"></param>
        /// <param name="durationMsec"></param>
        private static void DrawCircleWithDots(ILaser laser, IRtc rtc, float radius, float durationMsec)
        {
            rtc.ListBegin(laser);
            for (float angle =0; angle<360; angle+=1)
            {
                double x = radius * Math.Sin(angle * Math.PI / 180.0);
                double y = radius * Math.Cos(angle * Math.PI / 180.0);
                rtc.ListJump(new Vector2((float)x, (float)y));
                //지정된 짧은 시간동안 레이저 출사
                rtc.ListLaserOn(durationMsec);                
            }            
            rtc.ListEnd();
            rtc.ListExecute(true);
        }
        /// <summary>
        /// draw square area with pixels
        /// </summary>
        /// <param name=""></param>
        private static void DrawSquareAreaWithPixels(ILaser laser, IRtc rtc, float size, float gap)
        {
            // pixel operation 은 IRtcExtension 인터페이스에서 제공
            var rtcExt = rtc as IRtcExtension;
            if (null == rtcExt)
                return;

            int counts = (int)(size / gap);
            rtc.ListBegin(laser);
            for (int i=0; i< counts; i++)
            {
                //줄의 시작위치로 점프
                rtc.ListJump(new Vector2(0, i * gap));
                // pixel의 최대 주기시간 (200us), 출력 채널(analog 1), 가로세로 간격 (gap), 총 pixel 개수
                rtcExt.ListPixelLine(200, new Vector2(gap, 0), (uint)counts, ExtensionChannel.ExtAO1);
                for (int j = 0; j < counts; j++)
                    rtcExt.ListPixel(20, 5); // 20usec, 5V
            }
            rtc.ListEnd();
            rtc.ListExecute(true);
        }
    }
}
