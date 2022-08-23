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
 * 그룹 개체에 개별 펜 파라메터를 설정하여 가공한다.
 *
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

            // query white pen
            // 펜 집합에서 흰색 펜 정보 변경
            var pen1 = doc.Pens.ColorOf(System.Drawing.Color.White);
            // 파라메터 값을 변경
            // configure pen parameters
            var penDefault1 = pen1 as PenDefault;
            penDefault1.Frequency = 100 * 1000; //주파수 Hz
            penDefault1.PulseWidth = 2; //펄스폭 usec
            penDefault1.LaserOnDelay = 0; // 레이저 시작 지연 usec
            penDefault1.LaserOffDelay = 0; // 레이저 끝 지연 usec
            penDefault1.ScannerJumpDelay = 100; // 스캐너 점프 지연 usec
            penDefault1.ScannerMarkDelay = 200; // 스캐너 마크 지연 usec
            penDefault1.ScannerPolygonDelay = 0; // 스캐너 폴리곤 지연 usec
            penDefault1.JumpSpeed = 500; // 스캐너 점프 속도 mm/s
            penDefault1.MarkSpeed = 500; // 스캐너 마크 속도 mm/s
            // group with white pen parameters 
            // 흰색 펜 가공 파라메터 사용
            group1.Color2 = System.Drawing.Color.White;
            // add group into layer
            layer.Add(group1);

            // create another group entity
            // 두번째 그룹 객체 생성
            var group2 = new Group();
            // add pen entity into group
            // add line entity into group
            group2.Add(new Line(0, 0, 5, 10));
            // add circle entity into group
            group2.Add(new Circle(0, 0, 50));
            // add spiral entity into group
            group2.Add(new Spiral(-10.0f, 0.0f, 0.5f, 2.0f, 10, true));
            // group repeat counts = 20 times
            // 그룹의 반복 회수 설정 (20회 가공)
            group2.Repeat = 20;
            // query white pen
            // 펜 집합에서 흰색 펜 정보 변경
            var pen2 = doc.Pens.ColorOf(System.Drawing.Color.Yellow);
            // 파라메터 값을 변경
            // configure pen parameters
            var penDefault2 = pen2 as PenDefault;
            penDefault2.Frequency = 100 * 1000; //주파수 Hz
            penDefault2.PulseWidth = 2; //펄스폭 usec
            penDefault2.LaserOnDelay = 100; // 레이저 시작 지연 usec
            penDefault2.LaserOffDelay = 200; // 레이저 끝 지연 usec
            penDefault2.ScannerJumpDelay = 100; // 스캐너 점프 지연 usec
            penDefault2.ScannerMarkDelay = 350; // 스캐너 마크 지연 usec
            penDefault2.ScannerPolygonDelay = 0; // 스캐너 폴리곤 지연 usec
            penDefault2.JumpSpeed = 1000; // 스캐너 점프 속도 mm/s
            penDefault2.MarkSpeed = 1000; // 스캐너 마크 속도 mm/s
            // group with white pen parameters 
            // 흰색 펜 가공 파라메터 사용
            group2.Color2 = System.Drawing.Color.Yellow;
            // addition dx, dy offset location
            // dx= 10, dy= 0 오프셋 위치해 추가 가공
            group2.Offsets = new Offset[1]
            {
                new Offset(10, 0),
            };
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
            laser.Dispose();
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
                IsEnablePens = true,
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
