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
 * RTC 의 기능을 변경및 개조를 위해 새로운 사용자 정의 RTC 를 생성하고 이를 응용하는 방법
 * 
 * Author : hong chan, choi / sepwind @gmail.com(https://sepwind.blogspot.com)
 * 
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpiralLab.Sirius
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            SpiralLab.Core.Initialize();

            #region initialize RTC 
            var rtc = new RtcCustom(0); ///your custom rtc controller
            float fov = 60.0f;    /// scanner field of view : 60mm                                
            float kfactor = (float)Math.Pow(2, 20) / fov; /// k factor (bits/mm) = 2^20 / fov
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);    ///default correction file
            rtc.CtlFrequency(50 * 1000, 2); ///laser frequency : 50KHz, pulse width : 2usec
            rtc.CtlSpeed(100, 100); /// default jump and mark speed : 100mm/s
            rtc.CtlDelay(10, 100, 200, 200, 0); ///scanner and laser delays
            #endregion

            #region initialize Laser source
            ILaser laser = new LaserVirtual(0, "virtual laser", 10.0f);
            laser.Initialize();
            var pen = new Pen
            {
                Power = 5.0f,
            };
            laser.CtlPower(rtc, pen);
            #endregion          

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Testcase for spirallab.sirius. powered by labspiral@gmail.com (https://sepwind.blogspot.com)");
                Console.WriteLine("");
                Console.WriteLine("'C' : draw circle");
                Console.WriteLine("'R' : draw circle");
                Console.WriteLine("'L' : pop up your custom rtc form");
                Console.WriteLine("'Q' : quit");
                Console.WriteLine("");
                Console.Write("select your target : ");
                key = Console.ReadKey(false);
                if (key.Key == ConsoleKey.Q)
                    break;
                switch (key.Key)
                {
                    case ConsoleKey.C:
                        Console.WriteLine("\r\nWARNING !!! LASER IS BUSY ...");
                        DrawCircle(laser, rtc, 10);
                        break;
                    case ConsoleKey.R:
                        Console.WriteLine("\r\nWARNING !!! LASER IS BUSY ...");
                        DrawRectangle(laser, rtc, 10, 10);
                        break;
                    case ConsoleKey.L:
                        Console.WriteLine("\r\nLASER FORM");
                        rtc.Form.ShowDialog();

                        break;
                }

            } while (true);

            rtc.Dispose();
        }

        private static void DrawCircle(ILaser laser, IRtc rtc, double radius)
        {
            rtc.ListBegin(laser);
            rtc.ListJump(new Vector2((float)radius, 0));
            rtc.ListArc(new Vector2(0, 0), 360.0f);
            rtc.ListEnd();
            rtc.ListExecute(true);
        }

        private static void DrawRectangle(ILaser laser, IRtc rtc, double width, double height)
        {
            rtc.ListBegin(laser);
            rtc.ListJump(new Vector2((float)-width / 2, (float)height / 2));
            rtc.ListMark(new Vector2((float)width / 2, (float)height / 2));
            rtc.ListMark(new Vector2((float)width / 2, (float)-height / 2));
            rtc.ListMark(new Vector2((float)-width / 2, (float)-height / 2));
            rtc.ListMark(new Vector2((float)-width / 2, (float)height / 2));
            rtc.ListEnd();
            rtc.ListExecute(true);
        }


    }
}
