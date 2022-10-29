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
 * Motf with angular (회전물체를 대상으로 MOTF 가공)
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
    /// <summary>
    /// 스테이지 회전에 대한 MOTF(marking on the fly) 예제
    /// 엔코더0 입력핀으로 회전축의 엔코더가 입력됨 (by input external encoder0 into RTC controller)
    /// </summary>
    class Program4
    {
        [STAThread]
        static void Main4()
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

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("Testcase for spirallab.sirius. powered by hcchoi@spirallab.co.kr (http://spirallab.co.kr)");
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("'S' : Enable Simulate Encoder");
                Console.WriteLine("'D' : Disable Simulate Encoder");
                Console.WriteLine("'N' : MOTF With Follow Only");
                Console.WriteLine("'C' : MOTF With Circle And Wait Encoder");
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
                        // 가상 엔코더 속도 설정 
                        float angularVelocity = 10; // 10도/초 = 각속도
                        rtcMOTF.CtlMotfEncoderAngularSpeed(angularVelocity);
                        break;
                    case ConsoleKey.D:
                        rtcMOTF.CtlMotfEncoderAngularSpeed(0);
                        break;
                    case ConsoleKey.N:
                        MotfWithFollowOnly(laser, rtc);
                        break;
                    case ConsoleKey.C:
                        MotfWithCircleAndWaitEncoder(laser, rtc);
                        break;
                }
                Console.WriteLine($"Processing time = {timer.ElapsedMilliseconds / 1000.0:F3} s");
            } while (true);

            rtc.Dispose();
            laser.Dispose();
        }
        /// <summary>
        /// scanner continous following origin (0,0) location during list executing
        /// 스캐너가 0,0 을 지속적으로 포인팅 한다 (스테이지를 손으로 돌려보면서 테스트)
        /// </summary>
        /// <param name="laser"></param>
        /// <param name="rtc"></param>
        /// <param name="externalStart"></param>
        private static bool MotfWithFollowOnly(ILaser laser, IRtc rtc, bool externalStart = false)
        {
            var rtcMotf = rtc as IRtcMOTF;
            var rtcExtension = rtc as IRtcExtension;
            Debug.Assert(rtcMotf != null);
            Debug.Assert(rtcExtension != null);

            bool success = true;
            // start list buffer
            // 리스트 시작 
            success &= rtc.ListBegin(laser, ListType.Single);

            // ListMotfAngularBegin 부터 ListMOTFEnd 사이의 모든 list 명령어는 엔코더 증감값에 의해 회전적용됩니다            
            var rotateCenter = new Vector2(-50, 0);
            success &= rtcMotf.ListMotfAngularBegin(rotateCenter);

            // goes to origin
            // 0,0 으로 점프
            // 점프 위치는 rotateCenter 을 기준으로 상대적 위치인 (50, 0)로 연산 처리됨
            success &= rtc.ListJump(new Vector2(0, 0));

            // laser on!
            // for safety reason
            //success &= rtc.ListLaserOn();

            // wait 60 secs
            // 60 초동안 대기
            success &= rtc.ListWait(1000 * 60);

            // laser off !
            //success &= rtc.ListLaserOff();

            // motf end (from now ... stopped external x/y encoder iput values)
            // MOTF 중지
            success &= rtcMotf.ListMotfEnd(Vector2.Zero);
            success &= rtc.ListEnd();
            if (externalStart)
            {
                // enable /START trigger at LASER connector on RTC card
                // RTC 15핀 커넥터에 있는 /START 을 리스트 시작 트리거로 사용합니다.
                var extCtrl = Rtc5ExternalControlMode.Empty;
                extCtrl.Add(Rtc5ExternalControlMode.Bit.ExternalStart);
                extCtrl.Add(Rtc5ExternalControlMode.Bit.ExternalStartAgain);
                extCtrl.Add(Rtc5ExternalControlMode.Bit.EncoderReset);
                rtcExtension.CtlExternalControl(extCtrl);
            }
            else
            {
                // execute at once
                // 외부 트리거(/START)가 아닌 직접 execute 호출하여 실행
                rtcExtension.CtlExternalControl(Rtc5ExternalControlMode.Empty);
                if (success)
                    success &= rtc.ListExecute();
            }
            return success;
        }
        private static bool MotfWithCircleAndWaitEncoder(ILaser laser, IRtc rtc, bool externalStart = false)
        {
            var rtcMotf = rtc as IRtcMOTF;
            var rtcExtension = rtc as IRtcExtension;
            Debug.Assert(rtcMotf != null);
            Debug.Assert(rtcExtension != null);

            bool success = true;
            // start list buffer
            // 리스트 시작 
            success &= rtc.ListBegin(laser, ListType.Single);
            // draw line
            // 직선을 그립니다. (엔코더 입력과 무관합니다)
            success &= rtc.ListJump(new Vector2(0, 0));
            success &= rtc.ListMark(new Vector2(0, 10));

            // motf begin
            // ListMotfAngularBegin 부터 ListMOTFEnd 사이의 모든 list 명령어는 엔코더 증감값이 회전 적용됩니다
            // 실제 물리적인 회전 중심 (스캐너 중심에서 회전중심까지의 dx, dy 거리를 입력)
            var rotateCenter = new Vector2(-50, 0);

            /* global coordinate system
             * 
             *                                 |                                    
             *                                 |                                    
             *                                 |                                    
             *                                 |                                    
             *                                 |                                    
             *                                 |                                    
             *                                 |                                    
             *                                 |                                    
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

            success &= rtcMotf.ListMotfAngularBegin(rotateCenter);

            /* new scanner coordinate system
             * 
             *                                 |                                    
             *                                 |                                    
             *                                 |                                    
             *                                 |                                    
             *                                 |                                    
             *                                 |                                    
             *                                 |                                    
             *                                 |                                    
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

            // wait until condition has matched
            // 엔코더 값이 10도 가 넘을때(Over) 까지 리스트 명령들이 모두 대기됨
            success &= rtcMotf.ListMotfAngularWait(0, EncoderWaitCondition.Over);

            // draw circle
            // 엔코더 X 값이 위 조건을 만족한 이후 원 을 그린다
            // 반지름이 10 인 원을 0 도 위치에 가공
            success &= rtc.ListJump(-rotateCenter + new Vector2(10, 0));
            success &= rtc.ListArc(-rotateCenter, 360.0f);

            // wait until condition has matched
            // 엔코더 값이 10도 가 넘을때(Over) 까지 리스트 명령들이 모두 대기됨
            success &= rtcMotf.ListMotfAngularWait(180, EncoderWaitCondition.Over);

            // draw circle
            // 엔코더 X 값이 위 조건을 만족한 이후 원 을 그린다
            // 반지름이 10 인 원을 180 도 위치에 가공
            success &= rtc.ListJump(rotateCenter + rotateCenter + new Vector2(10, 0));
            success &= rtc.ListArc(rotateCenter + rotateCenter, 360.0f);

            // goes to origin location
            // motf end
            // MOTF 중지및 0,0 위치(스캐너 중심 위치)로 jump 실시
            success &= rtcMotf.ListMotfEnd(Vector2.Zero);
            success &= rtc.ListEnd();

            if (externalStart)
            {
                // enable / START trigger at LASER connector on RTC card
                // RTC 15핀 커넥터에 있는 /START 을 리스트 시작 트리거로 사용합니다.
                var extCtrl = Rtc5ExternalControlMode.Empty;
                extCtrl.Add(Rtc5ExternalControlMode.Bit.ExternalStart);
                extCtrl.Add(Rtc5ExternalControlMode.Bit.ExternalStartAgain);
                extCtrl.Add(Rtc5ExternalControlMode.Bit.EncoderReset);
                rtcExtension.CtlExternalControl(extCtrl);
            }
            else
            {
                // execute at once
                // 외부 트리거(/START) 미사용
                rtcExtension.CtlExternalControl(Rtc5ExternalControlMode.Empty);
                if (success)
                    success &= rtc.ListExecute();
            }
            return success;
        }
    }
}
