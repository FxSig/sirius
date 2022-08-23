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
 * IRtc + IRtcDualHead 인터페이스를 직접 사용하는 방법
 * RTC5 + DualHead 카드를 초기화 하고 각 헤드별 오프셋을 처리한다
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
    /// 듀얼 헤드 예제
    /// </summary>
    class Program2
    {
        [STAThread]
        static void Main2()
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

            //이미 table1 에 로드 및 선택 완료 (initialize 의 인자에서 처리됨)
            //var correctionFile1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            //rtc.CtlLoadCorrectionFile(CorrectionTableIndex.Table1, correctionFile1);
            var correctionFile2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            rtc.CtlLoadCorrectionFile(CorrectionTableIndex.Table2, correctionFile2);
            rtc.CtlSelectCorrection(CorrectionTableIndex.Table1, CorrectionTableIndex.Table2);
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
                Console.WriteLine("'D' : draw circle with dual head offset");
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
                    case ConsoleKey.D:
                        // translate and rotate at each head
                        //개별 헤드에 오프셋 및 회전 처리
                        var rtcDualHead = rtc as IRtcDualHead;
                        rtcDualHead.CtlHeadOffset(ScannerHead.Primary, new Vector2(5, 0), 0);
                        rtcDualHead.CtlHeadOffset(ScannerHead.Secondary, new Vector2(-5, 0), 0);
                        DrawCircle(laser, rtc);
                        // revert /reset head 
                        // 원복
                        rtcDualHead.CtlHeadOffset(ScannerHead.Primary, Vector2.Zero, 0);
                        rtcDualHead.CtlHeadOffset(ScannerHead.Secondary, Vector2.Zero, 0);
                        break;
                }
                Console.WriteLine($"Processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s");
            } while (true);

            rtc.CtlAbort();
            rtc.Dispose();
            laser.Dispose();
        }
        private static bool DrawCircle(ILaser laser, IRtc rtc)
        {
            bool success = true;
            var rtcDualHead = rtc as IRtcDualHead;
            Debug.Assert(rtcDualHead != null);

            // start list
            // 리스트 시작
            success &= rtc.ListBegin(laser);

            // translate and rotate at each head by list command if needed
            // 리스트 명령으로 오프셋 및 회전 처리 방법
            //success &= rtcDualHead.ListHeadOffset(ScannerHead.Primary, new Vector2(5, 0), 0);
            //success &= rtcDualHead.ListHeadOffset(ScannerHead.Secondary, new Vector2(-5, 0), 0);
            for (int i = 0; i < 10; i++)
            {
                // draw line
                // 직선을 그립니다
                success &= rtc.ListJump(new Vector2(0, 0));
                success &= rtc.ListMark(new Vector2(10, 0));
                // draw circle
                // 원을 그립니다
                success &= rtc.ListJump(new Vector2((float)10, 0));
                success &= rtc.ListArc(new Vector2(0, 0), 360.0f);
                if (!success)
                    break;
            }

            // translate and rotate at each head by list command if needed
            // 리스트 명령으로 오프셋 및 회전 처리 방법
            // success &= rtcDualHead.ListHeadOffset(ScannerHead.Primary, Vector2.Zero, 0);
            // success &= rtcDualHead.ListHeadOffset(ScannerHead.Secondary, Vector2.Zero, 0);
            //리스트 종료
            if (success)
            {
                success &= rtc.ListEnd();
                //나머지 데이타 가공 완료 대기
                success &= rtc.ListExecute(true);
            }
            return success;
        }
    }
}
