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
 * 지금까지 소개한 
 * 1. 가공 데이타 (Document)
 * 2. 레이저 소스 (Laser)
 * 3. 벡터 가공 장치 (Rtc) 
 * 를 가지고 실제 가공을 실시하는 관리 객체를 마커(Marker) 라 한다.
 * Create marker and how to manage it
 * 
 * 마커는 RTC, 레이저, 데이타(IDocument)를 모아 이를 가공하는 절차를 가지고 있는 객체로 상태 (IsReady, IsBusy, IsError)및 오프셋 가공 (List<Offset>)을 처리할수있다.
 * 또한 가공을 위해 소스 문서(Document)를 복제(Clone)하고 내부 처리 쓰레드에서 이 복제본을 가지고 가공이 시작된다. 
 * 가공데이타를 복제하고 시작 방식이기 때문에 엔티티의 화면(View) 편집과는 영향이 없다
 *  
 * Author : hong chan, choi / hcchoi@spirallab.co.kr (http://spirallab.co.kr)
 * 
 */


using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using SpiralLab.Sirius;

namespace SpiralLab.Sirius
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // initialize sirius library
            SpiralLab.Core.Initialize();

            #region initialize RTC 
            // create Rtc for dummy (가상 RTC 카드)
            //var rtc = new RtcVirtual(0); 
            // create Rtc5 controller
            var rtc = new Rtc5(0);
            // create Rtc6 controller
            //var rtc = new Rtc6(0); 
            // create Rtc6 Ethernet controller
            //var rtc = new Rtc6Ethernet(0, "192.168.0.100", "255.255.255.0"); 

            // theoretically size of scanner field of view (이론적인 FOV 크기) : 60mm
            float fov = 60.0f;
            // RTC4: k factor (bits/mm) = 2^16 / fov
            //float kfactor = (float)Math.Pow(2, 16) / fov;
            // RTC5/6: k factor (bits/mm) = 2^20 / fov
            float kfactor = (float)Math.Pow(2, 20) / fov;

            // RTC4: full path of correction file
            //var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ctb");
            // RTC5/6: full path of correction file
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            // initialize RTC controller
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);
            // laser frequency : 50KHz, pulse width : 2usec (주파수 50KHz, 펄스폭 2usec)
            rtc.CtlFrequency(50 * 1000, 2);
            // scanner jump and mark speed : 500mm/s (점프, 마크 속도 500mm/s)
            rtc.CtlSpeed(500, 500);
            // laser and scanner delays (레이저/스캐너 지연값 설정)
            rtc.CtlDelay(10, 100, 200, 200, 0);
            #endregion

            #region initialize Laser (virtual)
            // virtual laser source with max 20W power (최대 출력 20W 의 가상 레이저 소스 생성)
            var laser = new LaserVirtual(0, "virtual", 20);
            laser.PowerControlMethod = PowerControlMethod.Unknown;
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

            // assign RTC controller at laser 
            laser.Rtc = rtc;
            // initialize laser source
            laser.Initialize();
            // set basic power output to 2W
            laser.CtlPower(2);
            #endregion

            #region create document/layer/and spiral entity 
            // create sirius document
            // 문서 생성
            var doc = new DocumentDefault("3x3 scanner field correction");
            // create layer
            // 레이어 생성
            var layer = new Layer("default");
            // create spiral entity
            // 나선 개체 레이어에 추가
            var spiral = new Spiral(0.0f, 0.0f, 0.5f, 2.0f, 5, true);
            spiral.Color2 = System.Drawing.Color.White;

            layer.Add(spiral);

            // query white pen
            // 펜 집합에서 흰색 펜 정보 변경
            var pen = doc.Pens.ColorOf(System.Drawing.Color.White);
            // 파라메터 값을 변경
            // configure pen parameters
            var penDefault = pen as PenDefault;
            penDefault.Frequency = 100 * 1000; //주파수 Hz
            penDefault.PulseWidth = 2; //펄스폭 usec
            penDefault.LaserOnDelay = 0; // 레이저 시작 지연 usec
            penDefault.LaserOffDelay = 0; // 레이저 끝 지연 usec
            penDefault.ScannerJumpDelay = 100; // 스캐너 점프 지연 usec
            penDefault.ScannerMarkDelay = 200; // 스캐너 마크 지연 usec
            penDefault.ScannerPolygonDelay = 0; // 스캐너 폴리곤 지연 usec
            penDefault.JumpSpeed = 500; // 스캐너 점프 속도 mm/s
            penDefault.MarkSpeed = 500; // 스캐너 마크 속도 mm/s

            // regenerate
            // 레이어의 모든 개채들 내부 데이타 계산및 갱신
            layer.Regen();
            // add layer into document
            // 문서에 레이어 추가
            doc.Layers.Add(layer);
            doc.Layers.Active = layer;
            // save sirius document
            // 문서 저장
            var filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.sirius");
            DocumentSerializer.Save(doc, filename);
            #endregion

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("Testcase for spirallab.sirius. powered by hcchoi@spirallab.co.kr (http://spirallab.co.kr)");
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("'M' : draw entities by marker");
                Console.WriteLine("'O' : draw entities by marker with offsets");
                Console.WriteLine("'Q' : quit");
                Console.WriteLine($"{Environment.NewLine}");
                Console.Write("select your target : ");
                key = Console.ReadKey(false);
                if (key.Key == ConsoleKey.Q)
                    break;
                Console.WriteLine($"{Environment.NewLine}");
                switch (key.Key)
                {
                    case ConsoleKey.M:
                        Console.WriteLine("WARNING !!! LASER IS BUSY ...");
                        DoMarkByMarker(doc, rtc, laser);
                        break;
                    case ConsoleKey.O:
                        Console.WriteLine("WARNING !!! LASER IS BUSY ...");
                        DrawByMarkerWithOffset(doc, rtc, laser);
                        break;
                }

            } while (true);

            rtc.Dispose();
            laser.Dispose();
        }
        private static bool DoMarkByMarker(IDocument doc, IRtc rtc, ILaser laser)
        {
            if (rtc.CtlGetStatus(RtcStatus.Busy))
                return false;
            // create marker (has a internal worker thread)
            // 마커 객체 생성 (내부 쓰레드에 의해 비동기 적으로 대량의 데이타를 가공 처리)
            var marker = new MarkerDefault(0);            
            marker.Name = "marker #1";
            // register marker finish event handler
            // 가공 완료 이벤트 핸들러 등록
            marker.OnFinished += Marker_OnFinished;

            var markerArg = new MarkerArgDefault()
            {
                Document = doc,
                Rtc = rtc,
                Laser = laser,
                IsEnablePens = true,
            };

            bool success = true;
            // prepare document for mark (cloned all data by internally)
            // 마커에 가공 문서(doc)및 rtc, laser 정보를 전달하고 가공 준비를 실시한다
            success &= marker.Ready(markerArg);
            // start worker thread
            // 가공을 시작한다
            success &= marker.Start();
            return success;
        }
        private static bool DrawByMarkerWithOffset(IDocument doc, IRtc rtc, ILaser laser)
        {
            if (rtc.CtlGetStatus(RtcStatus.Busy))
                return false;
            // create marker (has a internal worker thread)
            // 마커 객체 생성 (내부 쓰레드에 의해 비동기 적으로 대량의 데이타를 가공 처리)
            var marker = new MarkerDefault(0);
            marker.Name = "marker #2";
            // register marker finish event handler
            //가공 완료 이벤트 핸들러 등록
            marker.OnFinished += Marker_OnFinished;

            var markerArg = new MarkerArgDefault()
            {
                Document = doc,
                Rtc = rtc,
                Laser = laser,
                IsEnablePens = true,
            };
            // multiple 9 offsets 
            // 9개의 오프셋 정보를 추가한다
            markerArg.Offsets.Add(new Offset(-20.0f, 20.0f, -90f));
            markerArg.Offsets.Add(new Offset(0.0f, 20.0f, 0.0f));
            markerArg.Offsets.Add(new Offset(20.0f, 20.0f, 90.0f));
            markerArg.Offsets.Add(new Offset(-20.0f, 0.0f, -180.0f));
            markerArg.Offsets.Add(new Offset(0.0f, 0.0f, 0.0f));
            markerArg.Offsets.Add(new Offset(20.0f, 0.0f, 180.0f));
            markerArg.Offsets.Add(new Offset(-20.0f, -20.0f, -270.0f));
            markerArg.Offsets.Add(new Offset(0.0f, -20.0f, 0.0f));
            markerArg.Offsets.Add(new Offset(20.0f, -20.0f, 270.0f));
            bool success = true;
            // prepare document for mark (cloned all data by internally)
            // 마커에 가공 문서(doc)및 rtc, laser 정보를 전달하고 가공 준비를 실시한다
            success &= marker.Ready(markerArg);
            // start worker thread
            // 가공을 시작한다
            success &= marker.Start();
            return success;
        }

        /// <summary>
        /// event has called when marker has finished 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        private static void Marker_OnFinished(IMarker sender, IMarkerArg arg)
        {
            var span = arg.EndTime - arg.StartTime;
            Console.WriteLine($"{Environment.NewLine}{sender.Name} finished. {span.TotalSeconds:F3} sec");
        }
    }
}
