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
 * IRtc 인터페이스를 직접 사용하는 방법
 * 자동 레이저 제어 기법을 사용한다
 * 위치 의존적, 속도 의존적, 벡터 정의 기반의 레이저 신호 변조 기법
 * Author : hong chan, choi / sepwind @gmail.com(http://spirallab.co.kr)
 * 
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;

namespace SpiralLab.Sirius
{

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
            //var rtc = new Rtc6SyncAxis(0, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configuration", "syncAXISConfig.xml")); //실험적인 상태 (Scanlab XLSCAN 솔류션)

            float fov = 60.0f;    // scanner field of view : 60mm            
            float kfactor = (float)Math.Pow(2, 20) / fov; // k factor (bits/mm) = 2^20 / fov
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);    // 스캐너 보정 파일 지정 : correction file
            rtc.CtlFrequency(50 * 1000, 2); // laser frequency : 50KHz, pulse width : 2usec
            rtc.CtlSpeed(100, 100); // default jump and mark speed : 100mm/s
            rtc.CtlDelay(10, 100, 200, 200, 0); // scanner and laser delays
            #endregion

            #region initialize Laser (virtual)
            ILaser laser = new LaserVirtual(0, "virtual", 20);  // 최대 출력 20W 의 가상 레이저 소스 생성
            #endregion

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Testcase for spirallab.sirius. powered by labspiral@gmail.com (http://spirallab.co.kr)");
                Console.WriteLine("");
                Console.WriteLine("'S' : get status");
                Console.WriteLine("'A' : 지령 속도(Set Velocity) 기반 레이저 신호 (아나로그) 제어");
                Console.WriteLine("'B' : 위치 의존적(Position Dependent) + 지령 속도(Set Velocity) 기반 레이저 신호 (아나로그 ~10V) 제어");
                Console.WriteLine("'C' : 실제 속도(Actual Velocity) 기반 레이저 신호 (주파수 변조) 제어");
                Console.WriteLine("'D' : 벡터 위치(Vector Defined) 기반 레이저 신호(아나로그) 제어");
                Console.WriteLine("'Q' : quit");
                Console.WriteLine("");
                Console.Write("select your target : ");
                key = Console.ReadKey(false);
                Console.WriteLine("");
                if (key.Key == ConsoleKey.Q)
                    break;
                var timer = Stopwatch.StartNew();
                switch (key.Key)
                {
                    case ConsoleKey.S:  //RTC의 상태 확인
                        if (rtc.CtlGetStatus(RtcStatus.Busy))
                            Console.WriteLine($"\r\nRtc is busy!");
                        else if (!rtc.CtlGetStatus(RtcStatus.PowerOK))
                            Console.WriteLine($"\r\nScanner power is not ok");
                        else if (!rtc.CtlGetStatus(RtcStatus.PositionAckOK))
                            Console.WriteLine($"\r\nScanner position is not acked");
                        else if (!rtc.CtlGetStatus(RtcStatus.NoError))
                            Console.WriteLine($"\r\nRtc status has an error");
                        else
                            Console.WriteLine($"\r\nIt's okay");
                        break;
                    case ConsoleKey.A:  
                        Console.WriteLine("\r\nWARNING !!! LASER IS BUSY ...");
                        DrawLine1(laser, rtc, -10, -10, 10, 10);
                        break;
                    case ConsoleKey.B:
                        Console.WriteLine("\r\nWARNING !!! LASER IS BUSY ...");
                        DrawLine2(laser, rtc, -10, -10, 10, 10);
                        break;
                    case ConsoleKey.C:
                        Console.WriteLine("\r\nWARNING !!! LASER IS BUSY ...");
                        DrawLine3(laser, rtc, -10, -10, 10, 10);
                        break;
                    case ConsoleKey.D:
                        Console.WriteLine("\r\nWARNING !!! LASER IS BUSY ...");
                        DrawLine4(laser, rtc, -10, -10, 10, 10);
                        break;
                }
                Console.WriteLine($"Processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s");
            } while (true);

            rtc.Dispose();
        }

        /// <summary>
        /// 선 그리기 = 지령 속도 기반 레이저 신호 (아나로그 ~10V) 제어
        /// </summary>
        /// <param name="rtc"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        private static void DrawLine1(ILaser laser, IRtc rtc, float x1, float y1, float x2, float y2)
        {
            var alc = rtc as IRtcAutoLaserControl;
            if (null == alc)
                return;
            alc.AutoLaserControlByPositionFileName = string.Empty;
            alc.CtlAutoLaserControl<float>(AutoLaserControlSignal.Analog1, AutoLaserControlMode.SetVelocity, 10, 0, 10);

            rtc.ListBegin(laser);
            rtc.ListJump(new Vector2(x1, y1));
            rtc.ListMark(new Vector2(x2, y2));
            rtc.ListEnd();
            rtc.ListExecute(true);
        }

        /// <summary>
        /// 선 그리기 = 위치 의존적 + 지령 속도 기반 레이저 신호 (아나로그 ~10V) 제어
        /// </summary>
        /// <param name="rtc"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        private static void DrawLine2(ILaser laser, IRtc rtc, float x1, float y1, float x2, float y2)
        {
            var alc = rtc as IRtcAutoLaserControl;
            if (null == alc)
                return;
            alc.AutoLaserControlByPositionFileName = "your power scale file.txt";
            alc.AutoLaserControlByPositionTableNo = 0;
            alc.CtlAutoLaserControl<float>(AutoLaserControlSignal.Analog1, AutoLaserControlMode.SetVelocity, 10, 0, 10);

            rtc.ListBegin(laser);
            rtc.ListJump(new Vector2(x1, y1));
            rtc.ListMark(new Vector2(x2, y2));
            rtc.ListEnd();
            rtc.ListExecute(true);
        }

        /// <summary>
        /// 선 그리기 = 실제 속도 기반 레이저 신호 (주파수 변조) 제어
        /// intelliDRIVE 기반 스캐너 필요
        /// </summary>
        /// <param name="rtc"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        private static void DrawLine3(ILaser laser, IRtc rtc, float x1, float y1, float x2, float y2)
        {
            var alc = rtc as IRtcAutoLaserControl;
            if (null == alc)
                return;
            alc.AutoLaserControlByPositionFileName = string.Empty;
            //target frequency : 100KHz
            //lower cut off frequency : 50KHz
            //upper cut off frequency : 120KHz
            alc.CtlAutoLaserControl<float>(AutoLaserControlSignal.Frequency, AutoLaserControlMode.ActualVelocity, 100 * 1000,  50*1000, 120 * 1000);
            rtc.ListBegin(laser);
            rtc.ListJump(new Vector2(x1, y1));
            rtc.ListMark(new Vector2(x2, y2));
            rtc.ListEnd();
            rtc.ListExecute(true);
        }

        /// <summary>
        /// 선 그리기 = 벡터 위치 기반 레이저 신호(아나로그) 제어
        /// intelliDRIVE 기반 스캐너 필요
        /// </summary>
        /// <param name="rtc"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        private static void DrawLine4(ILaser laser, IRtc rtc, float x1, float y1, float x2, float y2)
        {
            var alc = rtc as IRtcAutoLaserControl;
            if (null == alc)
                return;
            alc.AutoLaserControlByPositionFileName = string.Empty;
            alc.CtlAutoLaserControl<float>(AutoLaserControlSignal.Frequency, AutoLaserControlMode.ActualVelocity, 100 * 1000, 50 * 1000, 120 * 1000);
            rtc.ListBegin(laser);
            alc.ListAlcByVectorBegin<float>(AutoLaserControlSignal.Analog1, 10F); // 아나로그 신호로 시작 (기준값 : 10V)
            rtc.ListJump(new Vector2(x1, y1), 0.5F); //시작 위치로 점프 10V * 0.5 = 5V 로 시작
            rtc.ListMark(new Vector2(x2, y2), 1.0F); //끝 위치로 마크 10V * 1.0 = 10V 로 끝
            alc.ListAlcByVectorEnd();
            rtc.ListEnd();
            rtc.ListExecute(true);
        }

    }
}
