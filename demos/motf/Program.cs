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
 * RTC5 + MOTF 카드를 초기화 하고 엔코더 리셋, MOTF 마킹을 한다
 * Author : hong chan, choi / labspiral@gmail.com(http://spirallab.co.kr)
 * 
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;

namespace SpiralLab.Sirius
{
    /// <summary>
    /// MOTF 예제
    /// </summary>
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

            float fov = 60.0f;    // scanner field of view : 60mm            
            float kfactor = (float)Math.Pow(2, 20) / fov; // k factor (bits/mm) = 2^20 / fov
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);    // 스캐너 보정 파일 지정 : correction file
            rtc.CtlFrequency(50 * 1000, 2); // laser frequency : 50KHz, pulse width : 2usec
            rtc.CtlSpeed(100, 100); // default jump and mark speed : 100mm/s
            rtc.CtlDelay(10, 100, 200, 200, 0); // scanner and laser delays

            var rtcMOTF = rtc as IRtcMOTF;
            //엔코더 스케일 설정
            rtcMOTF.EncXCountsPerMm = 2000;
            rtcMOTF.EncYCountsPerMm = 2000;

            #endregion

            #region initialize Laser (virtual)
            var laser = new LaserVirtual(0, "virtual", 20);  // virtual laser source with max 20W power (최대 출력 20W 의 가상 레이저 소스 생성)
            //var laser = new IPGYLP(0, "IPG YLP", 1, 20);
            //var laser = new JPTTypeE(0, "JPT Type E", 1, 20);
            //var laser = new SPIG4(0, "SPI G3/4", 1, 20);
            //var laser = new PhotonicsIndustryDX(0, "PI", 1, 20);
            //var laser = new AdvancedOptoWaveFotia(0, "Fotia", 1, 20);
            //var laser = new CoherentAviaLX(0, "Avia LX", 1, 20);
            laser.Rtc = rtc;
            laser.Initialize();
            laser.CtlPower(2);
            #endregion

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("Testcase for spirallab.sirius. powered by labspiral@gmail.com (http://spirallab.co.kr)");
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("'R' : encoder reset");
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
                    case ConsoleKey.R:
                        // X,Y 엔코더값을 0,0 으로 리셋합니다.
                        rtcMOTF.CtlEncoderReset();
                        // 가상 엔코더를 100mm/s 으로 동작시킵니다.
                        //rtcMOTF.CtlEncoderSpeed(100, 100);
                        break;
                    case ConsoleKey.N:
                        MotfWithFollowOnly(laser, rtc, false);
                        break;
                    case ConsoleKey.C:
                        MotfWithCircleAndWaitEncoder(laser, rtc, false);
                        break;
                }
                Console.WriteLine($"Processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s");
            } while (true);

            rtc.Dispose();
        }
        /// <summary>
        /// 스캐너가 0,0 을 지속적으로 포인팅 한다 (스테이지나 컨베이어를 손으로 밀어보면서 테스트)
        /// </summary>
        /// <param name="laser"></param>
        /// <param name="rtc"></param>
        /// <param name="externalStart"></param>
        private static bool MotfWithFollowOnly(ILaser laser, IRtc rtc, bool externalStart)
        {
            var rtcMotf = rtc as IRtcMOTF;
            var rtcExtension = rtc as IRtcExtension;
            bool success = true;
            //리스트 시작 
            success &= rtc.ListBegin(laser, ListType.Single);
            // ListMOTFBegin 부터 ListMOTFEnd 사이의 모든 list 명령어는 엔코더증감값이 적용됩니다
            success &= rtcMotf.ListMOTFBegin();
            // 0,0 으로 점프
            success &= rtc.ListJump(new Vector2(0, 0));
            //10 초동안 대기
            success &= rtc.ListWait(1000 * 10);
            // MOTF 중시
            success &= rtcMotf.ListMOTFEnd(Vector2.Zero);
            success &= rtc.ListEnd();
            if (externalStart)
            {
                // RTC 15핀 커넥터에 있는 /START 을 리스트 시작 트리거로 사용합니다.
                var extCtrl = new Rtc5ExternalControlMode();
                extCtrl.Add(Rtc5ExternalControlMode.Bit.ExternalStart);
                extCtrl.Add(Rtc5ExternalControlMode.Bit.ExternalStartAgain);
                rtcExtension.CtlExternalControl(extCtrl);
            }
            else
            {
                //외부 트리거(/START)가 아닌 직접 execute 호출하여 실행
                rtcExtension.CtlExternalControl(Rtc5ExternalControlMode.Empty);
                if (success)
                    success &= rtc.ListExecute();
            }
            return success;
        }
        private static bool MotfWithCircleAndWaitEncoder(ILaser laser, IRtc rtc, bool externalStart)
        {
            var rtcMotf = rtc as IRtcMOTF;
            var rtcExtension = rtc as IRtcExtension;
            bool success = true;
            // 리스트 시작 
            success &= rtc.ListBegin(laser, ListType.Single);
            // 직선을 그립니다. (엔코더 입력과 무관합니다)
            success &= rtc.ListJump(new Vector2(0, 0));
            success &= rtc.ListMark(new Vector2(0, 10));
            // ListMOTFBegin 부터 ListMOTFEnd 사이의 모든 list 명령어는 엔코더증감값이 적용됩니다
            success &= rtcMotf.ListMOTFBegin();
            // 엔코더 X 값이 10mm 가 넘을때(Over) 까지 리스트 명령들이 모두 대기됨
            success &= rtcMotf.ListMOTFWait(RtcEncoder.EncX, 10, EncoderWaitCondition.Over);
            // 엔코더 X 값이 위 조건을 만족한 이후 원 을 그린다
            success &= rtc.ListJump(new Vector2((float)10, 0));
            success &= rtc.ListArc(new Vector2(0, 0), 360.0f);
            // MOTF 중지및 0,0 위치(스캐너 중심 위치)로 jump 실시
            success &= rtcMotf.ListMOTFEnd(Vector2.Zero);
            success &= rtc.ListEnd();
            if (externalStart)
            {
                // RTC 15핀 커넥터에 있는 /START 을 리스트 시작 트리거로 사용합니다.
                var extCtrl = new Rtc5ExternalControlMode();
                extCtrl.Add(Rtc5ExternalControlMode.Bit.ExternalStart);
                extCtrl.Add(Rtc5ExternalControlMode.Bit.ExternalStartAgain);
                rtcExtension.CtlExternalControl(extCtrl);
            }
            else
            {
                // 외부 트리거(/START) 미사용
                rtcExtension.CtlExternalControl(Rtc5ExternalControlMode.Empty);
                if (success)
                    success &= rtc.ListExecute();
            }
            return success;
        }
    }
}
