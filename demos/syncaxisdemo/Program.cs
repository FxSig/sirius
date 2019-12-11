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
            var rtc = new Rtc6SyncAxis(0, "syncAXISConfig.xml"); ///rtc6 with XL SCAN
            rtc.Initialize(0.0f, LaserMode.Yag1, string.Empty); 
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
                Console.WriteLine("'C' : draw circle (scanner only)");
                Console.WriteLine("'R' : draw rectangle (stage only)");
                Console.WriteLine("'L' : draw circle with lines (stage + scanner)");
                Console.WriteLine("'Q' : quit");
                Console.WriteLine("");
                Console.Write("select your target : ");
                key = Console.ReadKey(false);
                if (key.Key == ConsoleKey.Q)
                    break;
                Console.WriteLine("\r\nWARNING !!! LASER IS BUSY ...");
                var timer = new Stopwatch();
                switch (key.Key)
                {
                    case ConsoleKey.C:
                        DrawCircle(laser, rtc, 10);
                        break;
                    case ConsoleKey.R:
                        DrawRectangle(laser, rtc, 10, 10);
                        break;
                    case ConsoleKey.L:
                        DrawCircleWithLines(laser, rtc, 10);
                        break;
                }

                Console.WriteLine($"processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s");
            } while (true);

            rtc.Dispose();
        }
        /// <summary>
        /// 지정된 반지름을 갖는 원 그리기
        /// </summary>
        /// <param name="rtc"></param>
        /// <param name="radius"></param>
        private static void DrawCircle(ILaser laser, IRtc rtc, double radius)
        {
            rtc.ListBegin(laser, MotionType.ScannerOnly);
            rtc.ListJump(new Vector2((float)radius, 0));
            rtc.ListArc(new Vector2(0, 0), 360.0f);
            rtc.ListEnd();
            rtc.ListExecute(true);
        }
        /// <summary>
        /// 지정된 크기의 직사각형 그리기
        /// </summary>
        /// <param name="rtc"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private static void DrawRectangle(ILaser laser, IRtc rtc, double width, double height)
        {
            rtc.MatrixStack.Push(width * 1.5f, height * 1.5f);///transit safety area
            rtc.ListBegin(laser, MotionType.StageOnly);
            rtc.ListJump(new Vector2((float)-width / 2, (float)height / 2));
            rtc.ListMark(new Vector2((float)width / 2, (float)height / 2));
            rtc.ListMark(new Vector2((float)width / 2, (float)-height / 2));
            rtc.ListMark(new Vector2((float)-width / 2, (float)-height / 2));
            rtc.ListMark(new Vector2((float)-width / 2, (float)height / 2));
            rtc.ListEnd();
            rtc.ListExecute(true);
            rtc.MatrixStack.Pop();
        }
        /// <summary>
        /// 직선으로 원 그리기
        /// </summary>
        /// <param name="rtc"></param>
        /// <param name="radius"></param>
        /// <param name="durationMsec"></param>
        private static void DrawCircleWithLines(ILaser laser, IRtc rtc, float radius)
        {
            rtc.MatrixStack.Push(radius * 2f, radius * 2f);///transit safety area
            rtc.ListBegin(laser, MotionType.StageAndScanner);
            double x = radius * Math.Sin(0 * Math.PI / 180.0);
            double y = radius * Math.Cos(0 * Math.PI / 180.0);
            rtc.ListJump(new Vector2((float)x, (float)y));

            for (float angle = 10; angle < 360; angle += 10)
            {
                x = radius * Math.Sin(angle * Math.PI / 180.0);
                y = radius * Math.Cos(angle * Math.PI / 180.0);
                rtc.ListMark(new Vector2((float)x, (float)y));
            }
            x = radius * Math.Sin(0 * Math.PI / 180.0);
            y = radius * Math.Cos(0 * Math.PI / 180.0);
            rtc.ListMark(new Vector2((float)x, (float)y));

            rtc.ListEnd();
            rtc.ListExecute(true);
            rtc.MatrixStack.Pop();
        }
    }
}
