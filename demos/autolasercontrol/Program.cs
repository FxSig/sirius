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
 * IRtc 인터페이스를 직접 사용하는 방법
 * 자동 레이저 제어 기법을 사용한다
 * 위치 의존적, 속도 의존적, 벡터 정의 기반의 자동 레이저 출력 제어 기법
 * Author : hong chan, choi / hcchoi@spirallab.co.kr (http://spirallab.co.kr)
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SpiralLab.Core.Initialize();

            #region initialize RTC 
            //create Rtc for dummy (가상 RTC 카드)
            //var rtc = new RtcVirtual(0); 

            //create Rtc5 controller
            var rtc = new Rtc5(0);
            //rtc.InitLaser12SignalLevel = RtcSignalLevel.ActiveHigh;
            //rtc.InitLaserOnSignalLevel = RtcSignalLevel.ActiveHigh;

            //create Rtc6 controller
            //var rtc = new Rtc6(0); 
            //rtc.InitLaser12SignalLevel = RtcSignalLevel.ActiveHigh;
            //rtc.InitLaserOnSignalLevel = RtcSignalLevel.ActiveHigh;

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
                Console.WriteLine("Testcase for spirallab.sirius. powered by hcchoi@spirallab.co.kr (http://spirallab.co.kr)");
                Console.WriteLine("");
                Console.WriteLine("'S' : get status");
                Console.WriteLine("'A' : 지령 속도(Set Velocity) 기반 레이저 신호 (아나로그) 제어");
                Console.WriteLine("'B' : 위치 의존적(Position Dependent) + 지령 속도(Set Velocity) 기반 레이저 신호 (아나로그 ~10V) 제어");
                Console.WriteLine("'C' : 실제 속도(Actual Velocity) 기반 레이저 신호 (주파수 변조) 제어");
                Console.WriteLine("'D' : 벡터 위치(Vector Defined) 기반 레이저 신호(아나로그) 제어");
                Console.WriteLine("'O' : 벡터 위치(Vector Defined) 기반 레이저 신호(아나로그) 제어 및 계측 후 그래프로 출력");
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
                    case ConsoleKey.O:
                        Console.WriteLine("\r\nWARNING !!! LASER IS BUSY ...");
                        DrawLine5(laser, rtc, -10, -10, 10, 10);
                        break;
                }
                Console.WriteLine($"Processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s");
            } while (true);

            rtc.Dispose();
            laser.Dispose();
        }

        /// <summary>
        /// 선 그리기 = 지령 속도 기반 레이저 신호 (아나로그 ~10V) 제어
        /// 가공 출력 조건 = 5V (최소 4V, 최대 6V)
        /// </summary>
        private static bool DrawLine1(ILaser laser, IRtc rtc, float x1, float y1, float x2, float y2)
        {
            var alc = rtc as IRtcAutoLaserControl;
            if (null == alc)
                return false;
            bool success = true;
            // 위치 의존 ALC 파일 포맷은 RTC 메뉴얼 참고
            alc.CtlAutoLaserControlByPositionTable(null);
            success &= alc.CtlAutoLaserControl<float>(AutoLaserControlSignal.Analog1, AutoLaserControlMode.SetVelocity, 5, 4, 6);
            success &= rtc.ListBegin(laser);
            success &= rtc.ListJump(new Vector2(x1, y1));
            success &= rtc.ListMark(new Vector2(x2, y2));
            if (success)
            {
                success &= rtc.ListEnd();
                success &= rtc.ListExecute(true);
            }
            return success;
        }

        /// <summary>
        /// 선 그리기 = 위치 의존적 + 지령 속도 기반 레이저 신호 (아나로그 ~10V) 제어
        /// 가공 출력 조건 = 5V (최소 4V, 최대 6V)
        /// </summary>
        private static bool DrawLine2(ILaser laser, IRtc rtc, float x1, float y1, float x2, float y2)
        {
            var alc = rtc as IRtcAutoLaserControl;
            if (null == alc)
                return false;
            bool success = true;
            // 위치 의존(스캐너 중심으로 부터 거리에 따른) ALC 활성화
            float fov = 60.0f;
            //tuple with distance(mm), scale(0~4)
            var kvList = new KeyValuePair<float, float>[3];
            kvList[0] = new KeyValuePair<float, float>(5, 1);
            kvList[1] = new KeyValuePair<float, float>(10, 1.5f);
            kvList[2] = new KeyValuePair<float, float>(fov/2, 2);
            success &= alc.CtlAutoLaserControlByPositionTable(kvList);
            success &= alc.CtlAutoLaserControl<float>(AutoLaserControlSignal.Analog1, AutoLaserControlMode.SetVelocity, 5, 4, 6);
            success &= rtc.ListBegin(laser);
            success &= rtc.ListJump(new Vector2(x1, y1));
            success &= rtc.ListMark(new Vector2(x2, y2));
            if (success)
            {
                success &= rtc.ListEnd();
                success &= rtc.ListExecute(true);
            }
            return success;
        }

        /// <summary>
        /// 선 그리기 = 실제 속도 기반 레이저 신호 (주파수 변조) 제어
        /// iDRIVE 기반 스캐너 필요
        /// 가공 출력 조건 = 50KHz (최소 40KHz, 최대 60KHz)
        /// </summary>
        private static bool DrawLine3(ILaser laser, IRtc rtc, float x1, float y1, float x2, float y2)
        {
            var alc = rtc as IRtcAutoLaserControl;
            if (null == alc)
                return false;
            bool success = true;
            // 위치 의존 ALC 비활성화
            success &= alc.CtlAutoLaserControlByPositionTable(null);
            //target frequency : 100KHz
            //lower cut off frequency : 50KHz
            //upper cut off frequency : 120KHz
            success &= alc.CtlAutoLaserControl<float>(AutoLaserControlSignal.Frequency, AutoLaserControlMode.ActualVelocity, 50 * 1000, 40*1000, 60 * 1000);
            success &= rtc.ListBegin(laser);
            success &= rtc.ListJump(new Vector2(x1, y1));
            success &= rtc.ListMark(new Vector2(x2, y2));
            if (success)
            {
                success &= rtc.ListEnd();
                success &= rtc.ListExecute(true);
            }
            return success;
        }

        /// <summary>
        /// 선 그리기 = 벡터 위치 기반 레이저 신호(아나로그) 제어
        /// </summary>
        private static bool DrawLine4(ILaser laser, IRtc rtc, float x1, float y1, float x2, float y2)
        {
            var alc = rtc as IRtcAutoLaserControl;
            if (null == alc)
                return false;
            bool success = true;            
            success &= rtc.ListBegin(laser);
            success &= alc.ListAlcByVectorBegin<float>(AutoLaserControlSignal.Analog1, 5F); // 아나로그 출력 초기값 : 5V
            success &= rtc.ListJump(new Vector2(x1, y1), 0.5F); //시작 위치로 점프 5V * 0.5 = 2.5V 로 시작
            success &= rtc.ListMark(new Vector2(x2, y2), 1.5F); //끝 위치로 마크 5V * 1.5 = 7.5V 로 끝
            success &= alc.ListAlcByVectorEnd();
            if (success)
            {
                success &= rtc.ListEnd();
                success &= rtc.ListExecute(true);
            }
            return success;
        }

        /// <summary>
        /// 선 그리기 = 벡터 위치 기반 레이저 신호(아나로그) 제어 
        /// 이후 계측 데이타 그래프로 출력(Plot)
        /// </summary>
        /// <param name="laser"></param>
        /// <param name="rtc"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        private static bool DrawLine5(ILaser laser, IRtc rtc, float x1, float y1, float x2, float y2)
        {
            var rtcAlc = rtc as IRtcAutoLaserControl;
            var rtcMeasurement = rtc as IRtcMeasurement;
            if (null == rtcAlc || null == rtcMeasurement)
                return false;
            bool success = true;

            //RTC5,6 는 최대 4개의 채널
            var channels = new MeasurementChannel[4]
            {
                 MeasurementChannel.SampleX, //X commanded
                 MeasurementChannel.SampleY, //Y commanded
                 MeasurementChannel.LaserOn, //Gate signal 0/1
                 MeasurementChannel.ExtAO1, //Analog1
            };
            float hz = 10 * 1000; //10KHz (샘플링 주기 : 100usec)

            success &= rtc.ListBegin(laser);
            success &= rtcMeasurement.ListMeasurementBegin(hz, channels); //1Khz, 4개 채널
            success &= rtcAlc.ListAlcByVectorBegin<float>(AutoLaserControlSignal.Analog1, 5F); // 아나로그 출력 초기값 : 5V
            success &= rtc.ListJump(new Vector2(x1, y1), 0.5F); //시작 위치로 점프 5V * 0.5 = 2.5V 로 시작
            success &= rtc.ListMark(new Vector2(x2, y2), 1.5F); //끝 위치로 마크 5V * 1.5 = 7.5V 로 끝
            success &= rtcAlc.ListAlcByVectorEnd();
            if (success)
            {
                success &= rtcMeasurement.ListMeasurementEnd();
                success &= rtc.ListEnd();
                success &= rtc.ListExecute(true);
                var measurementFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plot", "measurement_alc.txt");
                success &= MeasurementHelper.Save(measurementFile, rtcMeasurement, hz, channels, false);
                if (success)
                    MeasurementHelper.Plot(measurementFile);
            }
            return success;
        }
    }
}
