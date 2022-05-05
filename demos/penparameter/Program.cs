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
 * 레이저및 스캐너의 가공 파라메터를 일컬어 통상 "펜(Pen)" 파라메터라 하며, 이 펜 개체(Entity)를 사용해 다양한 가공 조건 (속도및 지연값등)을 설정한다.
 * 가공 정보를 가지고 있는 펜 개체(IPen Entity) 생성후 가공 좌표와 같이 레이어에 추가하여 사용하는 방법을 설명한다
 * Author : hong chan, choi / labspiral@gmail.com(http://spirallab.co.kr)
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
            //create Rtc for dummy (가상 RTC 카드)
            //var rtc = new RtcVirtual(0); 
            //create Rtc5 controller
            var rtc = new Rtc5(0);
            //create Rtc6 controller
            //var rtc = new Rtc6(0); 
            //Rtc6 Ethernet
            //var rtc = new Rtc6Ethernet(0, "192.168.0.100", "255.255.255.0"); 

            // theoretically size of scanner field of view (이론적인 FOV 크기) : 60mm
            float fov = 60.0f;
            // k factor (bits/mm) = 2^20 / fov
            float kfactor = (float)Math.Pow(2, 20) / fov;
            // full path of correction file
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            // initialize rtc controller
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);
            // basic frequency and pulse width
            // laser frequency : 50KHz, pulse width : 2usec (주파수 50KHz, 펄스폭 2usec)
            rtc.CtlFrequency(50 * 1000, 2);
            // basic sped
            // jump and mark speed : 500mm/s (점프, 마크 속도 500mm/s)
            rtc.CtlSpeed(500, 500);
            // basic delays
            // scanner and laser delays (스캐너/레이저 지연값 설정)
            rtc.CtlDelay(10, 100, 200, 200, 0);
            #endregion

            #region initialize Laser (virtual)
            // virtual laser source with max 20W power (최대 출력 20W 의 가상 레이저 소스 생성)
            var laser = new LaserVirtual(0, "virtual", 20);
            //var laser = new IPGYLPTypeD(0, "IPG YLP D", 1, 20);
            //var laser = new IPGYLPTypeE(0, "IPG YLP E", 1, 20);
            //var laser = new IPGYLPN(0, "IPG YLP N", 1, 100);
            //var laser = new JPTTypeE(0, "JPT Type E", 1, 20);
            //var laser = new SPIG4(0, "SPI G3/4", 1, 20);
            //var laser = new PhotonicsIndustryDX(0, "PI", 1, 20);
            //var laser = new AdvancedOptoWaveFotia(0, "Fotia", 1, 20);
            //var laser = new CoherentAviaLX(0, "Avia LX", 1, 20);
            //var laser = new CoherentDiamondJSeries(0, "Diamond J Series", "10.0.0.1", 200.0f);
            //var laser = new SpectraPhysicsTalon(0, "Talon", 1, 30);

            // assign RTC instance at laser 
            laser.Rtc = rtc;
            // initialize laser source
            laser.Initialize();
            // set basic power output to 2W
            laser.CtlPower(2);
            #endregion

            #region create entities
            // create document
            // 문서 생성
            var doc = new DocumentDefault("Unnamed");
            // create layer 
            // 레이어 생성
            var layer = new Layer("default");
            // create pen entity for laser parameter
            // 펜 개체(Entity) 생성       
            var pen = new PenDefault()
            {
                Frequency = 100 * 1000, //주파수 Hz
                PulseWidth = 2, //펄스폭 usec
                LaserOnDelay = 0, // 레이저 시작 지연 usec
                LaserOffDelay = 0, // 레이저 끝 지연 usec
                ScannerJumpDelay = 100, // 스캐너 점프 지연 usec
                ScannerMarkDelay = 200, // 스캐너 마크 지연 usec
                ScannerPolygonDelay = 0, // 스캐너 폴리곤 지연 usec
                JumpSpeed = 500, // 스캐너 점프 속도 mm/s
                MarkSpeed = 500, // 스캐너 마크 속도 mm/s
            };
            // add pen entity into layer
            // 펜 개체 레이어에 추가
            layer.Add(pen);
            // add line entity into layer
            // 선 개체 레이어에 추가
            layer.Add(new Line(0, 0, 10, 20));
            // add circle entity into layer
            // 원 개체 레이어에 추가
            layer.Add(new Circle(0, 0, 10));
            // add spiral entity into layer
            // 나선 개체 레이어에 추가
            layer.Add(new Spiral(-20.0f, 0.0f, 0.5f, 2.0f, 5, true));
            // regenerate entities (refresh and re-calculate internal data)
            //레이어의 모든 개채들 내부 데이타 계산및 갱신
            layer.Regen();
            // add layer into document
            // 문서에 레이어 추가
            doc.Layers.Add(layer);

            // save document
            // 문서를 지정된 파일에 저장
            var filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.sirius");
            DocumentSerializer.Save(doc, filename);
            #endregion

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("Testcase for spirallab.sirius. powered by labspiral@gmail.com (http://spirallab.co.kr)");
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("'D' : draw entities by pen");
                Console.WriteLine("'Q' : quit");
                Console.WriteLine($"{Environment.NewLine}");
                Console.Write("select your target : ");
                key = Console.ReadKey(false);
                if (key.Key == ConsoleKey.Q)
                    break;
                Console.WriteLine($"{Environment.NewLine}");
                switch (key.Key)
                {
                    case ConsoleKey.D:
                        Console.WriteLine("WARNING !!! LASER IS BUSY ...");
                        var timer = Stopwatch.StartNew();
                        DrawForFieldCorrection(laser, rtc, doc);
                        Console.WriteLine($"processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s");
                        break;
                }

            } while (true);

            rtc.CtlAbort();
            rtc.Dispose();
        }
        /// <summary>
        /// mark 3x3 spirals 
        /// 레이어 안에 있는 모든 객체들을 마킹하기 (3x3 의 나선 객체가 마킹됨)
        /// </summary>
        /// <param name="rtc"></param>
        /// <param name="doc"></param>
        private static bool DrawForFieldCorrection(ILaser laser, IRtc rtc, IDocument doc)
        {
            bool success = true;
            // create mark argument
            // 가공 정보 전달용 인자 생성
            var markerArg = new MarkerArgDefault()
            {
                Document = doc,
                Rtc = rtc,
                Laser = laser,
            };
            success &= rtc.ListBegin(laser);
            // iterate layers
            // 레이어 순회
            foreach (var layer in doc.Layers)
            {
                success &= layer.Mark(markerArg);
                // or 
                //레이어 내의 개체(Entity)들을 순회
                //foreach (var entity in layer)
                //{
                //    //레이저 가공이 가능한 개체(markerable)인지를 판단
                //    if (entity as IMarkerable markerable)
                //    {
                //        // mark entity
                //        // 해당 개체(Entity) 가공 
                //        success &= markerable.Mark(markerArg);    
                //    }
                //    if (!success)
                //        break;
                //}
                if (!success)
                    break;
            }
            if (success)
            {
                success &= rtc.ListEnd();
                success &= rtc.ListExecute(true);
            }
            return success;
        }        
    }
}
