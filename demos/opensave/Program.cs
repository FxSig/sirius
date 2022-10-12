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
 * 이 가공 개체(Entity)를 생성하고, 저장이 가능하며 또한 레이저 가공을 시도한다.
 * 개체를 접근하여 가공하는 다양한 방법에 대한 예제 코드
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



            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("Testcase for spirallab.sirius. powered by hcchoi@spirallab.co.kr (http://spirallab.co.kr)");
                Console.WriteLine($"----------------------------------------------------------------------------------------");
                Console.WriteLine("'S' : get status");
                Console.WriteLine("'A' : mark in document");
                Console.WriteLine("'B' : mark layer by layer");
                Console.WriteLine("'C' : mark entity by entity");
                Console.WriteLine("'F' : pop up laser source form");
                Console.WriteLine("'Q' : quit");
                Console.Write("Select your target : ");
                key = Console.ReadKey(false);
                Console.WriteLine($"{Environment.NewLine}");
                if (key.Key == ConsoleKey.Q)
                    break;
                switch (key.Key)
                {
                    case ConsoleKey.S:  //RTC's status (상태 확인)
                        if (rtc.CtlGetStatus(RtcStatus.Busy))
                            Console.WriteLine($"Rtc is busy!");
                        if (!rtc.CtlGetStatus(RtcStatus.PowerOK))
                            Console.WriteLine($"Scanner power is not ok");
                        if (!rtc.CtlGetStatus(RtcStatus.PositionAckOK))
                            Console.WriteLine($"Scanner position is not acked");
                        if (!rtc.CtlGetStatus(RtcStatus.NoError))
                            Console.WriteLine($"Rtc status has an error");
                        break;
                    case ConsoleKey.A :
                        MarkDocument(laser, rtc, doc2);
                        break;
                    case ConsoleKey.B:
                        MarkDocument2(laser, rtc, doc2);
                        break;
                    case ConsoleKey.C:
                        MarkDocument3(laser, rtc, doc2);
                        break;
                    case ConsoleKey.F:
                        // popup winforms for control laser source
                        // 레이저 소스 제어용 윈폼 팝업
                        SpiralLab.Sirius.Laser.LaserForm laerForm = new SpiralLab.Sirius.Laser.LaserForm(laser);
                        laerForm.ShowDialog();
                        break;
                }
            } while (true);

            Console.WriteLine("Terminating ...");
            if (rtc.CtlGetStatus(RtcStatus.Busy))
            {
                // abort marking operation
                rtc.CtlAbort();
                // wait until busy has finished
                rtc.CtlBusyWait();
            }

            rtc.Dispose();
            laser.Dispose();
        }

        /// <summary>
        /// 지정된 문서(Document)를 지정된 RTC 제어기로 가공하기
        /// 레이어 직접 순회
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
                if (layer.IsMarkerable)
                {
                    success &= layer.Mark(markerArg);
                }
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


        /// <summary>
        /// 지정된 문서(Document)를 지정된 RTC 제어기로 가공하기
        /// 레이어 내의 개체를 직접 순회
        /// </summary>
        /// <param name="laser"></param>
        /// <param name="rtc"></param>
        /// <param name="doc"></param>
        static void MarkDocument2(ILaser laser, IRtc rtc, IDocument doc)
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
            // iterate layer in layers
            // 레이어를 순회
            foreach (var layer in doc.Layers)
            {
                if (layer.IsMarkerable)
                {
                    // iterate entities in layer
                    // 레이어 내의 개체(Entity)들을 순회
                    foreach (var entity in layer)
                    {
                        //레이저 가공이 가능한 개체(markerable)인지를 판단
                        if (entity is IMarkerable markerable)
                        {
                            // mark entity
                            // 해당 개체(Entity) 가공 
                            success &= markerable.Mark(markerArg);
                        }
                        if (!success)
                            break;
                    }
                }
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

        /// <summary>
        /// 지정된 문서(Document)를 지정된 RTC 제어기로 가공하기
        /// 레이어 내의 개체(Entity)의 개별 타입을 직접 확인하고 가공
        /// </summary>
        /// <param name="laser"></param>
        /// <param name="rtc"></param>
        /// <param name="doc"></param>
        static void MarkDocument3(ILaser laser, IRtc rtc, IDocument doc)
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
            // iterate layer in layers
            // 레이어를 순회
            foreach (var layer in doc.Layers)
            {
                if (layer.IsMarkerable)
                {
                    // iterate entities in layer
                    // 레이어 내의 개체(Entity)들을 순회
                    foreach (var entity in layer)
                    {
                        switch (entity.EntityType)
                        {
                            case EType.Point:
                                var point = entity as Point;
                                //point.Location 
                                //point.DwellTime
                                success &= point.Mark(markerArg);
                                break;
                            case EType.Points:
                                var points = entity as Points;
                                foreach (var vertex in points)
                                {
                                    //vertex.X
                                    //vertex.Y
                                }
                                //points.DwellTime
                                success &= points.Mark(markerArg);
                                break;
                            case EType.Line:
                                var line = entity as Line;
                                //line.Start
                                //line.End
                                success &= line.Mark(markerArg);
                                break;
                            case EType.Arc:
                                var arc = entity as Arc;
                                //arc.Radius
                                //arc.Center
                                //arc.StartAngle
                                //arc.SweepAngle
                                success &= arc.Mark(markerArg);
                                break;
                            case EType.Circle:
                                var circle = entity as Circle;
                                //circle.Center 
                                //circle.Radius
                                success &= circle.Mark(markerArg);
                                break;
                            case EType.Rectangle:
                                var rectangle = entity as Rectangle;
                                //rectangle.Width
                                //rectangle.Height
                                //rectangle.Align
                                //rectangle.Location
                                success &= rectangle.Mark(markerArg);
                                break;
                            case EType.LWPolyline:
                                var lwPolyline = entity as LwPolyline;
                                //lwPolyline.IsClosed
                                foreach (var vertex in lwPolyline)
                                {
                                    //vertex.X
                                    //vertex.Y
                                    //vertex.Bulge
                                }
                                success &= lwPolyline.Mark(markerArg);
                                break;
                            case EType.Spiral:
                                var spiral = entity as Spiral;
                                //spiral.OutterDiameter 
                                //spiral.InnerDiameter
                                //spiral.RadialPitch
                                //spiral.Revolutions
                                //spiral.Center
                                success &= spiral.Mark(markerArg);
                                break;
                            case EType.Group:
                                var group = entity as Group;
                                foreach (var subEntity in group)
                                {
                                    switch (subEntity.GetType())
                                    {
                                        default:
                                            break;
                                    }
                                }
                                success &= group.Mark(markerArg);
                                break;
                            // case EType....
                            // ...

                            default:
                                if (entity is IMarkerable markerable)
                                {
                                    // mark entity
                                    // 해당 개체(Entity) 가공 
                                    success &= markerable.Mark(markerArg);
                                }
                                break;
                        }
                        if (!success)
                            break;
                    }
                }
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
