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
 * 여러 객체(Entity)를을 묶어 하나의 그룹(Group) 으로 관리가 가능한다. 
 * 이는 데이타가 매우 많거나 반복(repeat)가공이 필요할 경우 유용하다.
 * Author : hong chan, choi / sepwind @gmail.com(https://sepwind.blogspot.com)
 * 
 */


using System;
using System.Diagnostics;
using System.IO;

namespace SpiralLab.Sirius
{
    class Program
    {
        static void Main(string[] args)
        {
            SpiralLab.Core.Initialize();

            #region initialize RTC 
            var rtc = new RtcVirtual(0);
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
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);    ///default correction file
            rtc.CtlFrequency(50 * 1000, 2); ///laser frequency : 50KHz, pulse width : 2usec
            rtc.CtlSpeed(100, 100); /// default jump and mark speed : 100mm/s
            rtc.CtlDelay(10, 100, 200, 200, 0); ///scanner and laser delays
            #endregion

            #region initialize Laser (virtial)
            ILaser laser = new LaserVirtual(0, "virtual", 20);
            #endregion

            #region create entities
            var doc = new DocumentDefault("Unnamed");
            var layer = new Layer("default");
            doc.Layers.Add(layer);
           
            ///첫번째 그룹 객체 생성
            var group1 = new Group();
            group1.Add(
               new Pen()
               {
                   Frequency = 100 * 1000,
                   PulseWidth = 2,
                   LaserOnDelay = 0,
                   LaserOffDelay = 0,
                   ScannerJumpDelay = 100,
                   ScannerMarkDelay = 200,
                   ScannerPolygonDelay = 0,
                   JumpSpeed = 500,
                   MarkSpeed = 500,
               }
            );
            group1.Add(new Line(0, 0, 10, 20));
            group1.Add(new Circle(0, 0, 10));
            group1.Add(new Spiral(-20.0f, 0.0f, 0.5f, 2.0f, 5, true));
            group1.RepeatCount = 10;    /// 10회 가공

            /// 두번째 그룹 객체 생성
            var group2 = new Group();
            group2.Add(
               new Pen()
               {
                   Frequency = 50 * 1000,
                   PulseWidth = 2,
                   LaserOnDelay = 0,
                   LaserOffDelay = 0,
                   ScannerJumpDelay = 100,
                   ScannerMarkDelay = 200,
                   ScannerPolygonDelay = 0,
                   JumpSpeed = 100,
                   MarkSpeed = 100,
               }
            );
            group1.Add(new Line(0, 0, 5, 10));
            group1.Add(new Circle(0, 0, 50));
            group1.Add(new Spiral(-10.0f, 0.0f, 0.5f, 2.0f, 10, true));
            group1.RepeatCount = 20;    /// 20 회 가공
            layer.Add(group2);

            var ds = new DocumentSerializer();
            ds.Save(doc, "test.sirius");
            #endregion

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Testcase for spirallab.sirius. powered by labspiral@gmail.com(https://sepwind.blogspot.com)");
                Console.WriteLine("");
                Console.WriteLine("'D' : draw group entities with pen");
                Console.WriteLine("'Q' : quit");
                Console.WriteLine("");
                Console.Write("select your target : ");
                key = Console.ReadKey(false);
                if (key.Key == ConsoleKey.Q)
                    break;
                switch (key.Key)
                {
                    case ConsoleKey.D:
                        Console.WriteLine("\r\nWARNING !!! LASER IS BUSY ...");
                        var timer = Stopwatch.StartNew();
                        if (DrawForFieldCorrection(laser, rtc, doc))
                        {
                            rtc.ListExecute(true);
                            Console.WriteLine($"processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s");
                        }
                        break;
                }

            } while (true);

            rtc.Dispose();
        }
        /// <summary>
        /// 레이어 안에 있는 모든 객체들을 마킹하기 (3x3 의 나선 객체가 마킹됨)
        /// </summary>
        /// <param name="rtc"></param>
        /// <param name="doc"></param>
        private static bool DrawForFieldCorrection(ILaser laser, IRtc rtc, IDocument doc)
        {
            bool success = true;
            rtc.ListBegin(laser);
            foreach (var layer in doc.Layers)
            {
                foreach (var entity in layer)
                {
                    var markerable = entity as IMarkerable;
                    if (null != markerable)
                        success &= markerable.Mark(rtc);
                }
            }
            if (success)
                rtc.ListEnd();
            return success;
        }
    }
}
