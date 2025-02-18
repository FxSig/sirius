﻿/*
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
 * IRtc + IRtc2ndHead 인터페이스를 직접 사용하는 방법
 * RTC5 + 2nd Head 카드를 초기화 하고 각 헤드별 오프셋을 처리한다
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
    /// 2nd Head 예제
    /// Primary/Secondary Heads 
    /// </summary>
    class Program2
    {
        [STAThread]
        static void Main2()
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

            // RTC 카드 옵션에 2nd head 가 활성화 되어 있어야 한다
            Debug.Assert(rtc.Is2ndHead);

            //이미 table1 에 로드 및 선택 완료 (initialize 의 인자에서 처리됨)
            //var correctionFile1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            //rtc.CtlLoadCorrectionFile(CorrectionTableIndex.Table1, correctionFile1);
            var correctionFile2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            rtc.CtlLoadCorrectionFile(CorrectionTableIndex.Table2, correctionFile2);
            rtc.CtlSelectCorrection(CorrectionTableIndex.Table1, CorrectionTableIndex.Table2);

            // 헤드간 거리값
            // distance bewteen primary/secondary heads
            // 중첩 되는 구간은 10mm 로 처리
            // overlap length is 10mm
            rtc.BaseDistanceToSecondaryHead = new Vector2(fov-10, 0);
            
            // 헤드 기준 오프셋 초기화
            // reset each head's base offset
            rtc.PrimaryHeadBaseOffset = Vector3.Zero;
            rtc.SecondaryHeadBaseOffset = Vector3.Zero;

            // 헤드 사용자 오프셋 초기화
            // reset each head's user offset
            rtc.PrimaryHeadUserOffset = Vector3.Zero;
            rtc.SecondaryHeadUserOffset = Vector3.Zero;

            // global offset (최종 오프셋) =  base(기준) offset + user (사용자) offset
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

            var rtc2ndHead = rtc as IRtc2ndHead;

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("Testcase for spirallab.sirius. powered by hcchoi@spirallab.co.kr (http://spirallab.co.kr)");
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("'D' : draw circle without offset");
                Console.WriteLine("'D' : draw circle with base offset");
                Console.WriteLine("'E' : draw circle with user offset");
                Console.WriteLine("'F' : quit");
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
                        rtc2ndHead.PrimaryHeadBaseOffset = Vector3.Zero;
                        rtc2ndHead.SecondaryHeadBaseOffset = Vector3.Zero;
                        rtc2ndHead.PrimaryHeadUserOffset = Vector3.Zero;
                        rtc2ndHead.SecondaryHeadUserOffset = Vector3.Zero;
                        DrawCircle(laser, rtc);
                        break;
                    case ConsoleKey.E:
                        // base offset : 두개의 헤드를 하드웨어(기구적으로) 간에 평행하도록 개별 헤드의 위치를 보정
                        // 헤드에 적용되는 최종 global offset = base + user  
                        rtc2ndHead.PrimaryHeadBaseOffset = new Vector3(10, 0, 0.1f);
                        rtc2ndHead.SecondaryHeadBaseOffset = new Vector3(-10, 0, 0.1f);
                        DrawCircle(laser, rtc);
                        break;
                    case ConsoleKey.F:
                        // user offset : 자재(혹은 레시피)별로 사용자가 헤드의 중심 위치를 이동하고자 할 경우
                        // 헤드에 적용되는 최종 global offset = base + user 
                        rtc2ndHead.PrimaryHeadUserOffset = new Vector3(-5, 0, 0);
                        rtc2ndHead.SecondaryHeadUserOffset = new Vector3(5, 0, 0);
                        DrawCircle(laser, rtc);
                        break;
                }
                Console.WriteLine($"Processing time = {timer.ElapsedMilliseconds / 1000.0:F3} s");
            } while (true);

            rtc.CtlAbort();
            rtc.Dispose();
            laser.Dispose();
        }
        private static bool DrawCircle(ILaser laser, IRtc rtc)
        {
            bool success = true;

            // start list
            // 리스트 시작
            success &= rtc.ListBegin(laser);
            // translate and rotate at each head by list command if needed
            // 리스트 명령으로 오프셋 및 회전 처리 방법
            //success &= rtc2ndHead.ListHeadOffset(ScannerHead.Primary, new Vector2(5, 0), 0);
            //success &= rtc2ndHead.ListHeadOffset(ScannerHead.Secondary, new Vector2(-5, 0), 0);

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
            // success &= rtc2ndHead.ListHeadOffset(ScannerHead.Primary, Vector2.Zero, 0);
            // success &= rtc2ndHead.ListHeadOffset(ScannerHead.Secondary, Vector2.Zero, 0);

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
