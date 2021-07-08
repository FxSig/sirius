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
 * IRtc + IRtcExtension 인터페이스를 직접 사용하는 방법
 * RTC5 + RtcExtension 카드를 초기화 하고 다양한 확장 기능을 테스트
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
    /// 확장 기능 예제
    /// </summary>
    class Program3
    {
        static void Main3(string[] args)
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
            #endregion

            #region initialize Laser (virtual)
            ILaser laser = new LaserVirtual(0, "virtual", 20);
            #endregion

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Testcase for spirallab.sirius. powered by labspiral@gmail.com (http://spirallab.co.kr)");
                Console.WriteLine("");
                Console.WriteLine("'D' : draw circle with dual head offset");
                Console.WriteLine("'Q' : quit");
                Console.WriteLine("");
                Console.Write("select your target : ");
                key = Console.ReadKey(false);
                if (key.Key == ConsoleKey.Q)
                    break;
                Console.WriteLine("");
                Console.WriteLine("WARNING !!! LASER IS BUSY ...");
                var timer = Stopwatch.StartNew();
                switch (key.Key)
                {
                    case ConsoleKey.D:
                        DrawCircle(laser, rtc);
                        break;
                }

                Console.WriteLine($"processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s");
            } while (true);

            rtc.Dispose();
        }
        private static bool DrawCircle(ILaser laser, IRtc rtc)
        {
            bool success = true;
            var rtcExt = rtc as IRtcExtension;
            //리스트 시작
            success &= rtc.ListBegin(laser);
            //아나로그1 에 5V 출력
            success &= rtc.ListWriteData<float>(ExtensionChannel.ExtAO2, 0.5f);
            //1 초 동안 대기
            success &= rtc.ListWait(1000);
            //아나로그1 에 0V 출력
            success &= rtc.ListWriteData<float>(ExtensionChannel.ExtAO2, 0);
            //점프
            success &= rtc.ListJump(new Vector2(10, 0));
            //레이저 출력 15핀에 있는 출력 2접점중 첫번째 비트 켜기 
            success &= rtc.ListWriteData<int>(ExtensionChannel.ExtDO2, 0x01);
            //레이저 출력 시작
            success &= rtc.ListLaserOn();
            //0.5 초 동안 대기
            success &= rtc.ListWait(500);
            //레이저 출력 중지
            success &= rtc.ListLaserOff();
            //레이저 출력 15핀에 있는 출력 2접점중 첫번째 비트 끄기
            success &= rtc.ListWriteData<int>(ExtensionChannel.ExtDO2, 0x00);
            //점프
            success &= rtc.ListJump(new Vector2(-10, 0));
            //매 100us 마다 X 방향으로 0.1 mm 이동하면서 아나로그 1번 출력으로 픽셀 출력(Raster Operation)을 준비 (100개)
            success &= rtcExt.ListPixelLine(100, new Vector2(0.1F, 0), 100, ExtensionChannel.ExtAO2);
            for (int i = 0; i < 100; i++)
                success &= rtcExt.ListPixel(10, 0.5f); //10us 펄스 생성및 아나로그2 에 5V 출력
            
            if (success)
            {
                //리스트 종료
                success &= rtc.ListEnd();
                //나머지 데이타 가공 완료 대기
                success &= rtc.ListExecute(true);
            }
            return success;
        }
    }
}
