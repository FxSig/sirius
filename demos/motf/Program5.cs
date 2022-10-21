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
 * IRtc + IRtcMOTF 인터페이스를 직접 사용하는 방법
 * RTC5 + MOTF 카드를 초기화 하고 X 엔코더(회전) 리셋, MOTF 마킹을 한다
 * Motf with angular (회전물체를 대상으로 MOTF 가공) + 대량의 데이타를 실시간으로 만들어 가공하며 이를 더블버퍼 처리하는 예제
 * Author : hong chan, choi / hcchoi@spirallab.co.kr (http://spirallab.co.kr)
 * 
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Threading;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    /// <summary>
    /// 스테이지 회전에 대한 MOTF(marking on the fly) 예제
    /// 엔코더0 입력핀으로 회전축의 엔코더가 입력됨 (by input external encoder0 into RTC controller)
    /// 대량의 데이타를 더블버퍼 처리하는 예제
    /// </summary>
    class Program5
    {

        // rotate center position 
        // 스캐너 중심에서 회전 중심으로의 위치
        public static Vector2 RotateCenter = new Vector2(0, 0);

        [STAThread]
        static void Main5()
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
            rtc.InitLaser12SignalLevel = RtcSignalLevel.ActiveHigh;
            rtc.InitLaserOnSignalLevel = RtcSignalLevel.ActiveHigh;
            rtc.Initialize(kfactor, LaserMode.Yag5, correctionFile);
            // basic frequency and pulse width
            // laser frequency : 50KHz, pulse width : 2usec (주파수 50KHz, 펄스폭 2usec)
            rtc.CtlFrequency(50 * 1000, 2);
            // basic sped
            // jump and mark speed : 500mm/s (점프, 마크 속도 500mm/s)
            rtc.CtlSpeed(500, 500);
            // basic delays
            // scanner and laser delays (스캐너/레이저 지연값 설정)
            rtc.CtlDelay(10, 100, 200, 200, 0);

            var rtcMOTF = rtc as IRtcMOTF;
            Debug.Assert(rtcMOTF != null);
            // assign encoder scale (counts/mm)
            // 엔코더 스케일 설정 (펄스수 / 회전당)
            // 엔코더 입력 핀 : Enc0 
            rtcMOTF.EncCountsPerRevolution = 3600;
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


            System.Threading.Timer encoderTimer = new System.Threading.Timer(TimerCallback, rtc, 100, 100);

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("Testcase for spirallab.sirius. powered by hcchoi@spirallab.co.kr (http://spirallab.co.kr)");
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("'S' : Set Rotate Center");
                Console.WriteLine("'R' : Reset Rotate Center");
                Console.WriteLine("'E' : Enable Simulate Encoder");
                Console.WriteLine("'D' : Disable Simulate Encoder");
                Console.WriteLine("'F' : MOTF With Follow Only");
                Console.WriteLine("'M' : MOTF With Circle And Wait Encoder + Double Buffered");
                Console.WriteLine("'Q' : quit");
                Console.WriteLine($"{Environment.NewLine}");
                Console.Write("select your target : ");
                key = Console.ReadKey(false);
                if (key.Key == ConsoleKey.Q)
                    break;
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("WARNING !!! LASER IS BUSY ...");
                var timer = Stopwatch.StartNew();
                switch (key.Key)
                {
                    case ConsoleKey.S:
                        //set
                        rtcMOTF.CtlMotfAngularCenter(RotateCenter);
                        break;
                    case ConsoleKey.R:
                        //reset
                        rtcMOTF.CtlMotfAngularCenter(Vector2.Zero);
                        break;
                    case ConsoleKey.E:
                        // simulate encoder speed : 45 deg/s  
                        // 가상 엔코더 속도 설정  
                        const float angularVelocity = 45;
                        rtcMOTF.CtlMotfEncoderAngularSpeed(angularVelocity);
                        break;
                    case ConsoleKey.D:
                        rtcMOTF.CtlMotfEncoderAngularSpeed(0);
                        break;
                    case ConsoleKey.F:
                        MotfWithFollowOnly(laser, rtc);
                        break;
                    case ConsoleKey.M:
                        MotfWithCircleAndWaitEncoderAndDoubleBuffered(laser, rtc);
                        break;
                }
                Console.WriteLine($"Processing time= {timer.ElapsedMilliseconds / 1000.0:F3} s");
            } while (true);

            encoderTimer.Dispose();
            rtc.Dispose();
            laser.Dispose();
        }

        private static void TimerCallback(Object objecct)
        {
            var rtc = objecct as IRtc;
            var rtcMotf = rtc as IRtcMOTF;
            if (rtcMotf.CtlMotfGetAngularEncoder(out int encoder, out float angle))
            {
                Console.Title = $"Encoder= {encoder}, Angle= {angle}";
            }
        }

        /// <summary>
        /// scanner continous following specific location during list executing
        /// 스캐너는 특정 위치를 지속적으로 포인팅 한다 
        /// 지시빔 레이저를 활성화하고 스테이지를 손으로 돌려보면서 따라가는지를 테스트 하는것을 추천
        /// </summary>
        /// <param name="laser"></param>
        /// <param name="rtc"></param>
        private static bool MotfWithFollowOnly(ILaser laser, IRtc rtc)
        {
            var rtcMotf = rtc as IRtcMOTF;
            var rtcExtension = rtc as IRtcExtension;
            Debug.Assert(rtcMotf != null);
            Debug.Assert(rtcExtension != null);

            // clear over/underflow warning
            rtcMotf.CtlMotfOverflowClear();

            bool success = true;
            // start list buffer
            // 리스트 시작 
            success &= rtc.ListBegin(laser, ListType.Single);

            // jump to scanner origin location
            // 스캐너 중심 0,0 으로 점프
            success &= rtc.ListJump(new Vector2(0, 0));
            //success &= rtc.ListJump(new Vector2(10, 0));

            /* global coordinate system
             * 
             *                                 |     <- Counter Clock Wise = Angle - = Enc -
             *                                 |              .                      
             *                                 |                 .                   
             *                                 |                    .                
             *                                 |                      .              
             *                                 |                        .             
             *                                 |                         .          
             *                                 |                          .          
             *                                 |                 |--------|--------|                    
             *                                 |                 |        |        |
             *                                 |                 |        |        |       
             *                                 |                 |        |        |    
             *  ------------------------ Rotate Center --------------- Scanner ----|
             *                                 |                 |      0 , 0      |    
             *                                 |                 |        |        |      
             *                                 |                 |        |        |
             *                                 |                 |--------|--------|                    
             *                                 |                          .          
             *                                 |                         .           
             *                                 |                        .            
             *                                 |                      .             
             *                                 |                    .                
             *                                 |                 .                   
             *                                 |             . 
             *                                 |     <- Clock Wise = Angle + = Enc + 
             *                          
             */

            // angular motf begin
            // ListMotfAngularBegin 부터 ListMOTFEnd 사이의 모든 list 명령어는 엔코더 증감값에 의해 회전 적용됨            
            success &= rtcMotf.ListMotfAngularBegin(RotateCenter);

            /* new coordinate system
             * 
             *                                 |     <- Counter Clock Wise = Angle - = Enc -
             *                                 |              .                      
             *                                 |                  .                   
             *                                 |                    .                
             *                                 |                      .              
             *                                 |                        .             
             *                                 |                         .          
             *                                 |                          .          
             *                                 |                 |--------|--------|                    
             *                                 |                 |        |        |
             *                                 |                 |        |        |       
             *                                 |                 |        |        |    
             *  ---------------------------- 0 , 0 --------------|---- Scanner ----|
             *                                 |                 |  RotateCenter   |    
             *                                 |                 |        |        |      
             *                                 |                 |        |        |
             *                                 |                 |--------|--------|                    
             *                                 |                          .          
             *                                 |                         .           
             *                                 |                        .            
             *                                 |                      .             
             *                                 |                    .                
             *                                 |                 .                   
             *                                 |             . 
             *                                 |     <- Clock Wise = Angle + = Enc + 
             *                          
             */

            // wait 60 secs
            // 60 초동안 대기
            success &= rtc.ListWait(1000 * 60);
            // or
            // for safety reason
            //success &= rtc.ListLaserOn(1000* 5);

            // motf end
            // MOTF 중지
            success &= rtcMotf.ListMotfEnd(Vector2.Zero);
            success &= rtc.ListEnd();
            if (success)
                success &= rtc.ListExecute();
            return success;
        }

        /// <summary>
        /// processing angular MOTF with continous data 
        /// 회전 기반 MOTF를 활성화 하여 대량의 데이타를 실시간 생성하여 특정 각도에서 가공 처리하며, 이를 계측하고 그래프로 plot 한다
        /// </summary>
        /// <param name="laser"></param>
        /// <param name="rtc"></param>
        /// <returns></returns>
        private static bool MotfWithCircleAndWaitEncoderAndDoubleBuffered(ILaser laser, IRtc rtc)
        {
            var rtcMotf = rtc as IRtcMOTF;
            var rtcExtension = rtc as IRtcExtension;
            var rtcMeasurement = rtc as IRtcMeasurement;
            Debug.Assert(rtcMotf != null);
            Debug.Assert(rtcExtension != null);

            // clear over/underflow warning
            rtcMotf.CtlMotfOverflowClear();

            bool success = true;

            //가공 데이타를 실시간 생성하는 방식이기 때문에 문서의 펜(Pen) 정보만 사용된다
            var doc = new DocumentDefault();
            var pen = doc.Pens.ColorOf(System.Drawing.Color.White) as PenDefault;

            pen.Frequency = 50 * 1000;
            pen.PulseWidth = 2;
            pen.JumpSpeed = 1000;
            pen.MarkSpeed = 1000;
            pen.LaserOnDelay = 0;
            pen.LaserOffDelay = 0;
            pen.ScannerJumpDelay = 200;
            pen.ScannerMarkDelay = 50;
            pen.ScannerPolygonDelay = 10;
            pen.LaserQSwitchDelay = 0;

            var markArg = new MarkerArgDefault()
            {
                Rtc = rtc,
                Laser = laser,
                Document = doc,
            };

            // start list buffer
            // 리스트 명령 시작 
            success &= rtc.ListBegin(laser, ListType.Auto);

            // measurement sampling rate (Hz)
            // 계측 측정 주기 (Hz)
            float samplingHz = 1000;

            // target measurement channels
            // 계측 대상 채널 (최대 4개)
            MeasurementChannel[] ch = new MeasurementChannel[4]
            {
                MeasurementChannel.SampleX, //X commanded
                MeasurementChannel.SampleY, //Y commanded
                MeasurementChannel.LaserOn, //Gate signal 0/1
                MeasurementChannel.Enc0Counter, //Enc0
            };

            // start measurement
            // 계측 시작
            success &= rtcMeasurement.ListMeasurementBegin(samplingHz, ch);

            //jump to scanner center
            // 스캐너 중심 위치로 점프
            success &= rtc.ListJump(0, 0);

            /* global coordinate system
             * 
             *                                 |     <- Counter Clock Wise = Angle - = Enc -
             *                                 |              .                      
             *                                 |                  .                   
             *                                 |                    .                
             *                                 |                      .              
             *                                 |                        .             
             *                                 |                         .          
             *                                 |                          .           
             *                                 |                 |--------|--------|                    
             *                                 |                 |        |        |
             *                                 |                 |        |        |       
             *                                 |                 |        |        |    
             *  ------------------------ Rotate Center --------------- Scanner ----|
             *                                 |                 |      0 , 0      |    
             *                                 |                 |        |        |      
             *                                 |                 |        |        |
             *                                 |                 |--------|--------|                    
             *                                 |                          .          
             *                                 |                         .           
             *                                 |                        .            
             *                                 |                      .             
             *                                 |                    .                
             *                                 |                 .                   
             *                                 |             . 
             *                                 |     <- Clock Wise = Angle + = Enc + 
             *                          
             */

            // motf begin 
            // ListMotfAngularBegin 부터 ListMOTFEnd 사이의 모든 list 명령어는 엔코더 증감값이 회전 적용됩니다
            success &= rtcMotf.ListMotfAngularBegin(RotateCenter);

            /* new scanner coordinate system
             * 
             *                                 |     <- Counter Clock Wise = Angle - = Enc -
             *                                 |              .                      
             *                                 |                  .                   
             *                                 |                    .                
             *                                 |                      .              
             *                                 |                        .             
             *                                 |                         .          
             *                                 |                          .           
             *                                 |                 |--------|--------|                    
             *                                 |                 |        |        |
             *                                 |                 |        |        |       
             *                                 |                 |        |        |    
             *  ------------------------ Rotate Center --------------- Scanner ----|
             *                               0 , 0               |     50 , 0      |    
             *                                 |                 |        |        |      
             *                                 |                 |        |        |
             *                                 |                 |--------|--------|                    
             *                                 |                          .          
             *                                 |                         .           
             *                                 |                        .            
             *                                 |                      .             
             *                                 |                    .                
             *                                 |                 .                   
             *                                 |             . 
             *                                 |     <- Clock Wise = Angle + = Enc + 
             *                          
             */

            // limis of bound area of scanner field
            // motf 사용시 범위 오류 검출에 사용할 스캐너 필드 영역 크기 설정
            // 가공 완료후 RtcMarkingInfo 를 사용해 영역 침범 여부 확인 가능
            success &= rtcMotf.ListMotfLimits(new Vector2(-10, 10), new Vector2(-10, 10));

            // every 90 degrees (0, 90, 180, 270)
            // 각도 (0, 90, 180, 270) 마다 가공
            // material rotate by CW direction = incremented encoder direction 
            // angular motf 에서는 대상 물체의 시계 방향 회전이 엔코더 증가 방향
            for (float angle = 0; angle < 360; angle += 90)
            {
                // wait until condition has matched
                // 지정된 각도가 넘을때까지 명령 대기
                success &= rtcMotf.ListMotfAngularWait(angle, EncoderWaitCondition.Over);

                // laser on during 200ms
                success &= rtc.ListLaserOn(200);
                // or

                /*
                // create closed figure
                // 폐곡선을 생성하고 (필요하면 내부 해치를 생성후) 가공 시작
                var lwPolyline = new LwPolyline();
                lwPolyline.Add(new LwPolyLineVertex(-5f, 5f));
                lwPolyline.Add(new LwPolyLineVertex(5f, 5f));
                lwPolyline.Add(new LwPolyLineVertex(5f, -5f));
                lwPolyline.Add(new LwPolyLineVertex(-5f, -5f));
                lwPolyline.IsClosed = true;
                // enable internal hatch by cross lines
                // 내부 해치 생성
                lwPolyline.IsHatchable = true;
                lwPolyline.HatchMode = HatchMode.CrossLine;
                lwPolyline.HatchInterval = 0.2f;
                lwPolyline.HatchExclude = 0;
                lwPolyline.HatchAngle = 0;
                lwPolyline.HatchAngle2 = 90;
                lwPolyline.Regen();
                // transit rotate center to scanner center distance
                // 회전 중심으로 부터 스캐너 중심위치 거리로 이동
                lwPolyline.Transit(-RotateCenter);
                // rotate figure by rotate center (CW direction =  encoder +)
                // 회전 중심 기준으로 회전 (물체의 시계방향 회전이 엔코더 증가 방향)
                lwPolyline.Rotate(-angle, Vector2.Zero);
                // mark (가공)
                success &= lwPolyline.Mark(markArg);
                */

                if (!success)
                     break;
            }
            // goes to origin location
            // motf end
            // MOTF 중지및 0,0 위치(스캐너 중심 위치)로 jump 실시
            success &= rtcMotf.ListMotfEnd(Vector2.Zero);
            if (success)
            {
                // end measurement
                // 계측 종료
                success &= rtcMeasurement.ListMeasurementEnd();
                // end of list buffer
                // 리스트 명령 종료
                success &= rtc.ListEnd();
            }
            if (success)
                // execute list buffer until it has flushed
                // 리스트 실행 (버퍼에 있는 모든 리스트 명령이 실행될때까지 대기)
                success &= rtc.ListExecute();

            if (success)
            {
                // extract measurement data from RTC controller and save into target file
                // RTC 카드에서 계측 데이타를 가져와 파일에 저장
                string plotFileFullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plot", $"measurement-{DateTime.Now.ToString("MM-dd-hh-mm-ss")}.txt");
                MeasurementHelper.Save(plotFileFullPath, rtcMeasurement, samplingHz, ch);

                // 파일을 분석하여 그래프로 Plot 출력
                MeasurementHelper.Plot(plotFileFullPath);

                // 
                if (rtc.CtlGetStatus(RtcStatus.MotfOutOfRange))
                {
                    // exceed limis of bound area of scanner field
                    // 지정된 영역을 벋어남
                    if (rtc is Rtc5 rtc5)
                    {
                        var infoBits = rtc5.RtcMarkingInfo.Value;
                        //rtc5.RtcMarkingInfo.Contains(Rtc5MarkingInfo.Bit.MotfUnderflowInXUserDirection);
                        //rtc5.RtcMarkingInfo.Contains(Rtc5MarkingInfo.Bit.MotfUnderflowInXDirection);
                        //rtc5.RtcMarkingInfo.Contains(Rtc5MarkingInfo.Bit.MotfUnderflowInYUserDirection);
                        //rtc5.RtcMarkingInfo.Contains(Rtc5MarkingInfo.Bit.MotfUnderflowInYDirection);

                        //rtc5.RtcMarkingInfo.Contains(Rtc5MarkingInfo.Bit.MotfOverflowInXUserDirection);
                        //rtc5.RtcMarkingInfo.Contains(Rtc5MarkingInfo.Bit.MotfOverflowInXDirection);
                        //rtc5.RtcMarkingInfo.Contains(Rtc5MarkingInfo.Bit.MotfOverflowInYUserDirection);
                        //rtc5.RtcMarkingInfo.Contains(Rtc5MarkingInfo.Bit.MotfOverflowInYDirection);
                        Console.WriteLine($"Out of bound area: {infoBits}");
                    }
                    else if (rtc is Rtc6 rtc6)
                    {
                        var infoBits = rtc6.RtcMarkingInfo.Value;
                        //rtc6.RtcMarkingInfo.Contains(Rtc6MarkingInfo.Bit.MotfUnderflowInXUserDirection);
                        //rtc6.RtcMarkingInfo.Contains(Rtc6MarkingInfo.Bit.MotfUnderflowInXDirection);
                        //rtc6.RtcMarkingInfo.Contains(Rtc6MarkingInfo.Bit.MotfUnderflowInYUserDirection);
                        //rtc6.RtcMarkingInfo.Contains(Rtc6MarkingInfo.Bit.MotfUnderflowInYDirection);

                        //rtc6.RtcMarkingInfo.Contains(Rtc6MarkingInfo.Bit.MotfOverflowInXUserDirection);
                        //rtc6.RtcMarkingInfo.Contains(Rtc6MarkingInfo.Bit.MotfOverflowInXDirection);
                        //rtc6.RtcMarkingInfo.Contains(Rtc6MarkingInfo.Bit.MotfOverflowInYUserDirection);
                        //rtc6.RtcMarkingInfo.Contains(Rtc6MarkingInfo.Bit.MotfOverflowInYDirection);

                        Console.WriteLine($"Out of bound area: {infoBits}");
                    }
                }
            }
            return success;
        }
    }
}
