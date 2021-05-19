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
 * Author : hong chan, choi / labspiral @gmail.com(http://spirallab.co.kr)
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
            //var rtc = new RtcVirtual(0); //create Rtc for dummy
            var rtc = new Rtc5(0); //create Rtc5 controller
            //var rtc = new Rtc6(0); //create Rtc6 controller
            //var rtc = new Rtc6Ethernet(0, "192.168.0.100", "255.255.255.0"); //실험적인 상태 (Scanlab Rtc6 Ethernet 제어기)
            //var rtc = new Rtc6SyncAxis(0, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configuration", "syncAXISConfig.xml")); //실험적인 상태 (Scanlab XLSCAN 솔류션)

            float fov = 60.0f;    // scanner field of view : 60mm                                
            float kfactor = (float)Math.Pow(2, 20) / fov; // k factor (bits/mm) = 2^20 / fov
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);    //default correction file
            rtc.CtlFrequency(50 * 1000, 2); //laser frequency : 50KHz, pulse width : 2usec
            rtc.CtlSpeed(100, 100); // default jump and mark speed : 100mm/s
            rtc.CtlDelay(10, 100, 200, 200, 0); //scanner and laser delays
            #endregion

            #region initialize Laser (virtual)
            ILaser laser = new LaserVirtual(0, "virtual", 20);
            #endregion

            #region create entities
            // 신규 문서 생성
            var doc = new DocumentDefault("Unnamed");
            // 레이어 생성후 문서에 추가
            var layer = new Layer("default");          
            //첫번째 그룹 객체 생성
            var group1 = new Group();
            group1.Add(
               new PenDefault()    // 그룹내에 펜 개체 생성하여 추가
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
            // 그룹내에 선 개체 생성하여 추가
            group1.Add(new Line(0, 0, 10, 20));
            // 그룹내에 원 개체 생성하여 추가
            group1.Add(new Circle(0, 0, 10));
            // 그룹내에 나선 개체 생성하여 추가
            group1.Add(new Spiral(-20.0f, 0.0f, 0.5f, 2.0f, 5, true));
            // 그룹의 반복 회수 설정 (10회 가공)
            group1.Repeat = 10;   

            // 두번째 그룹 객체 생성
            var group2 = new Group();
            group2.Add(
               new PenDefault()    // 그룹내에 펜 개체 생성하여 추가
               {
                   Frequency = 50 * 1000,
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
            group1.Add(new Line(0, 0, 5, 10));
            group1.Add(new Circle(0, 0, 50));
            group1.Add(new Spiral(-10.0f, 0.0f, 0.5f, 2.0f, 10, true));
            group1.Repeat = 20;    // 20 회 가공
            layer.Add(group2);

            //레이어의 모든 개채들 내부 데이타 계산및 갱신
            layer.Regen();
            // 문서에 레이어 추가
            doc.Layers.Add(layer);

            // 해당 문서 데이타를 지정된 파일에 저장
            DocumentSerializer.Save(doc, "test.sirius");
            #endregion

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Testcase for spirallab.sirius. powered by labspiral@gmail.com(http://spirallab.co.kr)");
                Console.WriteLine("");
                Console.WriteLine("'D' : draw group entities with pen");
                Console.WriteLine("'Q' : quit");
                Console.WriteLine("");
                Console.Write("select your target : ");
                key = Console.ReadKey(false);
                if (key.Key == ConsoleKey.Q)
                    break;
                Console.WriteLine("");
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
        /// 레이어 안에 있는 모든 객체들을 마킹하기
        /// </summary>
        /// <param name="rtc"></param>
        /// <param name="doc"></param>
        private static bool DrawForFieldCorrection(ILaser laser, IRtc rtc, IDocument doc)
        {
            bool success = true;
            var markerArg = new MarkerArgDefault()
            {
                Document = doc,
                Rtc = rtc,
                Laser = laser,
            };
            rtc.ListBegin(laser);
            foreach (var layer in doc.Layers)
            {
                foreach (var entity in layer)
                {
                    var markerable = entity as IMarkerable;
                    if (null != markerable)
                        success &= markerable.Mark(markerArg);
                }
            }
            if (success)
                rtc.ListEnd();
            return success;
        }
    }
}
