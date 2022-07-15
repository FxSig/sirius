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
 * 여러 개체(Entity)를을 묶어 하나의 그룹(Group) 으로 관리가 가능한다. 
 * 이는 데이타가 매우 많거나 반복(repeat)가공이 필요할 경우 유용하다.
 * 또한 그룹개체(Group Entity)는 자체적으로 오프셋(Offset) 속성을 통해 자기 자신을 여러 오프셋 위치에 반복 가공이 가능하다
 * Author : hong chan, choi / hcchoi@spirallab.co.kr (http://spirallab.co.kr)
 * 
 */


using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
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
            //var laser = new PhotonicsIndustryDX(0, "DX", 1, 20);
            //var laser = new PhotonicsIndustryRGHAIO(0, "RGHAIO", 1, 20);
            //var laser = new AdvancedOptoWaveFotia(0, "Fotia", 1, 20);
            //var laser = new AdvancedOptoWaveAOPico(0, "AOPico", 1, 20);
            //var laser = new CoherentAviaLX(0, "Avia LX", 1, 20);
            //var laser = new CoherentDiamondJSeries(0, "Diamond JSeries", "10.0.0.1", 200.0f);
            //var laser = new CoherentDiamondCSeries(0, "Diamond CSeries", 1, 100.0f);
            //var laser = new SpectraPhysicsHippo(0, "Hippo", 1, 30);
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
            // 신규 문서 생성
            var doc = new DocumentDefault("Unnamed");
            // create layer
            // 레이어 생성
            var layer = new Layer("default");          
            // create group entity
            // 첫번째 그룹 객체 생성
            var group1 = new Group();
            // add pen entity into group 
            group1.Add(
               new PenDefault() // 그룹내에 펜 개체 생성하여 추가
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
            // add line entity into group
            // 그룹내에 선 개체 생성하여 추가
            group1.Add(new Line(0, 0, 10, 20));
            // add circle entity into group
            // 그룹내에 원 개체 생성하여 추가
            group1.Add(new Circle(0, 0, 10));
            // add spiral entity into group
            // 그룹내에 나선 개체 생성하여 추가
            group1.Add(new Spiral(-20.0f, 0.0f, 0.5f, 2.0f, 5, true));
            // group repeat counts = 10 times
            // 그룹의 반복 회수 설정 (10회 가공)
            group1.Repeat = 10;
            // add group into layer
            layer.Add(group1);

            // create another group entity
            // 두번째 그룹 객체 생성
            var group2 = new Group();
            // add pen entity into group
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
            // add line entity into group
            group1.Add(new Line(0, 0, 5, 10));
            // add circle entity into group
            group1.Add(new Circle(0, 0, 50));
            // add spiral entity into group
            group1.Add(new Spiral(-10.0f, 0.0f, 0.5f, 2.0f, 10, true));
            // group repeat counts = 20 times
            // 그룹의 반복 회수 설정 (20회 가공)
            group1.Repeat = 20;
            // add group into layer
            layer.Add(group2);

            // regen all entities within layers
            // 레이어의 모든 개채들 내부 데이타 계산및 갱신
            layer.Regen();
            // add layer into document
            // 문서에 레이어 추가
            doc.Layers.Add(layer);
            // save document
            // 해당 문서 데이타를 지정된 파일에 저장
            var filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.sirius");
            DocumentSerializer.Save(doc, filename);
            #endregion

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("Testcase for spirallab.sirius. powered by hcchoi@spirallab.co.kr (http://spirallab.co.kr)");
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("'D' : draw group entities with pen");
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
                        DoMarkDocument(laser, rtc, doc);
                        Console.WriteLine($"Processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s");
                        break;
                }
            } while (true);

            rtc.Dispose();
        }
        /// <summary>
        /// 레이어 안에 있는 모든 객체들을 마킹하기
        /// </summary>
        /// <param name="laser"></param>
        /// <param name="rtc"></param>
        /// <param name="doc"></param>
        private static bool DoMarkDocument(ILaser laser, IRtc rtc, IDocument doc)
        {
            var markerArg = new MarkerArgDefault()
            {
                Document = doc,
                Rtc = rtc,
                Laser = laser,
            };
            bool success = true;
            success &= rtc.ListBegin(laser);
            foreach (var layer in doc.Layers)
            {
                success &= layer.Mark(markerArg);
                // or
                //foreach (var entity in layer)
                //{
                //    if (entity as IMarkerable markerable);
                //        success &= markerable.Mark(markerArg);
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
