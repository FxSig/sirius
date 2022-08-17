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
 * Document 문서 (가공 데이타) 저장, 열기
 * 
 * 문서(document) 는 레이어, 블럭 , 환경 설정 및 가공에 필요한 다양한 객체(Entity : 선, 호, 원, 폴리라인, 레이저 파라메터 등) 정보를 가지고 있다.
 * 이 가공 객체(Entity)를 생성하고, 저장이 가능하며 또한 레이저 가공을 시도한다.
 * Author : hong chan, choi / hcchoi@spirallab.co.kr (http://spirallab.co.kr)
 * 
 */


using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //initializing spirallab.sirius library engine (시리우스 라이브러리 초기화)
            SpiralLab.Core.Initialize();

            Console.WriteLine($"{Environment.NewLine}");
            Console.WriteLine("Testcase for spirallab.sirius. powered by hcchoi@spirallab.co.kr (http://spirallab.co.kr)");
            Console.WriteLine($"{Environment.NewLine}");

            #region create entities 
            // 신규 문서 (Create document) 생성
            var doc1 = new DocumentDefault("Unnamed");
            // 레이어(Layer) 생성
            var layer = new Layer("default");
            //레이어에 선 형상 개체(Line entity) 생성및 추가
            layer.Add(new Line(0, 10, 20,20));
            //레이어에 원 형상 개체(Circle entity) 생성및 추가
            layer.Add(new Circle(0, 0, 10));
            //레이어에 나선 형상 개체(Spiral entity) 생성및 추가
            layer.Add(new Spiral(-20.0f, 0.0f, 0.5f, 2.0f, 5, true));
            // 레이어를 문서에 추가
            doc1.Layers.Add(layer);
            #endregion

            Console.WriteLine("press any key to save ...");
            Console.ReadKey(false);
            var filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "default.sirius");

            // 문서(Save document) 저장하기
            DocumentSerializer.Save(doc1, filename);

            Console.WriteLine("press any key to open ...");
            Console.ReadKey(false);
            // 문서(Open document) 불러오기
            var doc2 = DocumentSerializer.OpenSirius(filename);

            Console.WriteLine("press any key to rtc initialize ...");
            Console.ReadKey(false);

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

            Console.WriteLine("press any key to laser processing ...WARNING !!!  LASER EMISSION");
            Console.ReadKey(false);
            MarkDocument(laser, rtc, doc2);

            Console.WriteLine("press any key to terminate program");
            Console.ReadKey(false);
        }

        /// <summary>
        /// 지정된 문서(Document)를 지정된 RTC 제어기로 가공하기
        /// </summary>
        /// <param name="laser"></param>
        /// <param name="rtc"></param>
        /// <param name="doc"></param>
        static void MarkDocument(ILaser laser, IRtc rtc, IDocument doc)
        {
            var timer = Stopwatch.StartNew();
            // create mark argument
            // 가공 정보 전달용 인자 생성
            var markerArg = new MarkerArgDefault()
            {
                Document = doc,
                Rtc = rtc,
                Laser = laser,
            };

            bool success = true;
            success &= rtc.ListBegin(laser);
            // iterate layers
            // 레이어를 순회
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
            Console.WriteLine($"Processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s");
        }
    }
}
