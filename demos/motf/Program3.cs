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
 * IRtc 인터페이스의 확장 기능을 사용하는 방법
 * RTC5 카드를 초기화 하고 다양한 확장 기능을 테스트
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
    /// 확장기능 예제 (raster operation, read/write data)
    /// Raster operation, read/write data
    /// </summary>
    class Program3
    {
        [STAThread]
        static void Main3()
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
                Console.WriteLine("'D' : write data to extension ports");
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
                        DrawWithExtensionDataOutput(laser, rtc);
                        break;
                }

                Console.WriteLine($"Processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s");
            } while (true);

            rtc.Dispose();
            laser.Dispose();
        }

        private static bool DrawWithExtensionDataOutput(ILaser laser, IRtc rtc)
        {
            bool success = true;
            var rtcRaster = rtc as IRtcRaster;
            // start list
            // 리스트 시작
            success &= rtc.ListBegin(laser);
            // output to analog2 port
            // 아나로그2 에 5V 출력
            success &= rtc.ListWriteData<float>(ExtensionChannel.ExtAO2, 0.5f);
            // wait 1000 ms
            // 1 초 동안 대기
            success &= rtc.ListWait(1000);
            // output to analog2 port
            // 아나로그2 에 0V 출력
            success &= rtc.ListWriteData<float>(ExtensionChannel.ExtAO2, 0);
            // jump
            // 점프
            success &= rtc.ListJump(new Vector2(10, 0));
            // output to pin 2 output port
            // 레이저 출력 15핀에 있는 출력 2접점중 첫번째 비트 켜기 
            success &= rtc.ListWriteData<int>(ExtensionChannel.ExtDO2, 0x01);
            // laser on
            // 레이저 출력 시작
            success &= rtc.ListLaserOn();
            // wait 500ms
            // 0.5 초 동안 대기
            success &= rtc.ListWait(500);
            // laser off
            // 레이저 출력 중지
            success &= rtc.ListLaserOff();
            // output to pin 2 output port
            // 레이저 출력 15핀에 있는 출력 2접점중 첫번째 비트 끄기
            success &= rtc.ListWriteData<int>(ExtensionChannel.ExtDO2, 0x00);
            // jump
            // 점프
            success &= rtc.ListJump(new Vector2(-10, 0));

            // bitmap raster operation
            // 매 100us 마다 X 방향으로 0.1 mm 이동하면서 아나로그 1번 출력으로 픽셀 출력(Raster Operation)을 준비 (100개)
            success &= rtcRaster.ListPixelLine(100, new Vector2(0.1F, 0), 100, ExtensionChannel.ExtAO2);
            for (int i = 0; i < 100; i++)
                success &= rtcRaster.ListPixel(10, 0.5f); //10us 펄스 생성및 아나로그2 에 5V 출력
            
            if (success)
            {
                // end of list
                // 리스트 종료
                success &= rtc.ListEnd();
                //execute and wait until has finined
                // 나머지 데이타 가공 완료 대기
                success &= rtc.ListExecute(true);
            }
            return success;
        }
    }
}
