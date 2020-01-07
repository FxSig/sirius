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
            var rtc = new RtcVirtual(0); ///create Rtc for dummy
            //var rtc = new Rtc5(0); ///create Rtc5 controller
            //var rtc = new Rtc6(0); ///create Rtc6 controller
            //var rtc = new Rtc6Ethernet(0, "192.168.0.200"); ///create Rtc6 ethernet controller
            //var rtc = new Rtc53D(0); ///create Rtc5 + 3D option controller
            //var rtc = new Rtc63D(0); ///create Rtc5 + 3D option controller
            //var rtc = new Rtc5DualHead(0); ///create Rtc5 + Dual head option controller
            //var rtc = new Rtc5MOTF(0); ///create Rtc5 + MOTF option controller
            //var rtc = new Rtc6MOTF(0); ///create Rtc6 + MOTF option controller
            //var rtc = new Rtc6SyncAxis(0); 
            //var rtc = new Rtc6SyncAxis(0, "syncAXISConfig.xml"); ///create Rtc6 + XL-SCAN (ACS+SYNCAXIS) option controller

            float fov = 60.0f;    /// scanner field of view : 60mm            
            float kfactor = (float)Math.Pow(2, 20) / fov; /// k factor (bits/mm) = 2^20 / fov
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);    /// 스캐너 보정 파일 지정 : correction file
            rtc.CtlFrequency(50 * 1000, 2); /// laser frequency : 50KHz, pulse width : 2usec
            rtc.CtlSpeed(100, 100); /// default jump and mark speed : 100mm/s
            rtc.CtlDelay(10, 100, 200, 200, 0); /// scanner and laser delays
            #endregion

            #region initialize Laser (virtial)
            ILaser laser = new LaserVirtual(0, "virtual", 20);
            #endregion

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Testcase for spirallab.sirius. powered by labspiral@gmail.com (https://sepwind.blogspot.com)");
                Console.WriteLine("");
                Console.WriteLine("'R' : draw rectangle with rotate");
                Console.WriteLine("'L' : draw lines with rotate");
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
                    case ConsoleKey.R:
                        DrawRectangle(laser, rtc, 10, 10, 0, 360);
                        break;
                    case ConsoleKey.L:
                        DrawLinesWithRotate(laser, rtc, 0, 360);
                        break;
                }
                rtc.ListExecute(true);
                Console.WriteLine($"processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s");
            } while (true);

            rtc.Dispose();
        }
       
        /// <summary>
        /// 지정된 크기의 직사각형 그리기
        /// </summary>
        /// <param name="rtc"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private static void DrawRectangle(ILaser laser, IRtc rtc, double width, double height, double angleStart, double angleEnd)
        {
            rtc.ListBegin(laser);
            for (double angle = angleStart; angle <= angleEnd; angle += 1)
            {
                rtc.MatrixStack.Push(angle);
                rtc.ListJump(new Vector2((float)-width / 2, (float)height / 2));
                rtc.ListMark(new Vector2((float)width / 2, (float)height / 2));
                rtc.ListMark(new Vector2((float)width / 2, (float)-height / 2));
                rtc.ListMark(new Vector2((float)-width / 2, (float)-height / 2));
                rtc.ListMark(new Vector2((float)-width / 2, (float)height / 2));
                rtc.MatrixStack.Pop();
            }

            rtc.ListEnd();
        }       
        /// <summary>
        /// 행렬을 이용해 직선을 그릴때 1도마다 직선을 회전시켜 그리기
        /// </summary>
        /// <param name="rtc"></param>
        /// <param name="angleStart"></param>
        /// <param name="angleEnd"></param>
        private static void DrawLinesWithRotate(ILaser laser, IRtc rtc, double angleStart, double angleEnd)
        {
            rtc.ListBegin(laser);
            for (double angle = angleStart; angle <= angleEnd; angle += 1)
            {
                rtc.MatrixStack.Push(angle);
                rtc.ListJump(new Vector2(-10, 0));
                rtc.ListMark(new Vector2(10, 0));
                rtc.MatrixStack.Pop();
            }
            rtc.ListEnd();
        }
    }
}
