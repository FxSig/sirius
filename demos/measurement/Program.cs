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
 * IRtcMeasurement 인터페이스를 사용해 스캐너의 모션 프로파일및 제어 신호를 계측한다
 * 계측 결과를 그래프를 이용해 출력(Plot) 한다
 * Author : hong chan, choi / labspiral@gmail.com(http://spirallab.co.kr)
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
    class Program
    {
        static Stopwatch timer;

        [STAThread]
        static void Main(string[] args)
        {
            SpiralLab.Core.Initialize();    //initializing spirallab.sirius library engine (시리우스 라이브러리 초기화)

            #region initialize RTC 
            //var rtc = new RtcVirtual(0); //create Rtc for dummy (가상 RTC 카드)
            var rtc = new Rtc5(0); //create Rtc5 controller
            //var rtc = new Rtc6(0); //create Rtc6 controller
            //var rtc = new Rtc6Ethernet(0, "192.168.0.100", "255.255.255.0"); //Rtc6 Ethernet
            //var rtc = new Rtc6SyncAxis(0, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "syncAxis", "syncAXISConfig.xml")); //Scanlab XLSCAN 

            float fov = 60.0f;    // scanner field of view : 60mm            
            float kfactor = (float)Math.Pow(2, 20) / fov; // k factor (bits/mm) = 2^20 / fov
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);    // correction file (스캐너 보정 파일)           
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
                Console.WriteLine($"----------------------------------------------------------------------------------------");
                Console.WriteLine("'C' : draw circle with measurement");
                Console.WriteLine("'R' : draw rectangle with measurement");
                Console.WriteLine("'O' : open and plot measurement file");
                Console.WriteLine("'Q' : quit");
                Console.Write("Select your target : ");
                key = Console.ReadKey(false);
                Console.WriteLine($"{Environment.NewLine}");
                if (key.Key == ConsoleKey.Q)
                    break;
                switch (key.Key)
                {                    
                    case ConsoleKey.C:  // draw circle (원 모양 가공)
                        DrawCircle(laser, rtc, 10);
                        break;
                    case ConsoleKey.R:  // draw rectangle (사각형 모양 가공)
                        DrawRectangle(laser, rtc, 10, 10);
                        break;
                    case ConsoleKey.O:  // open file
                        var dlg = new OpenFileDialog();
                        dlg.Filter = "measurement data files (*.txt)|*.txt|All Files (*.*)|*.*";
                        dlg.Title = "Open Measurement File";
                        dlg.InitialDirectory = Config.ConfigPlotFilePath;
                        DialogResult result = dlg.ShowDialog();
                        if (result != DialogResult.OK)
                            return;
                        MeasurementHelper.Plot(dlg.FileName);
                        break;
                }
            } while (true);

            if (rtc.CtlGetStatus(RtcStatus.Busy))
            {
                rtc.CtlAbort();
                rtc.CtlBusyWait();
            }
            rtc.Dispose();
        }

        /// <summary>
        /// draw circle
        /// </summary>
        /// <param name="laser"></param>
        /// <param name="rtc"></param>
        /// <param name="radius"></param>
        private static bool DrawCircle(ILaser laser, IRtc rtc, float radius)
        {
            if (rtc.CtlGetStatus(RtcStatus.Busy))
                return false;
            var rtcMeasurement = rtc as IRtcMeasurement;
            Debug.Assert(rtcMeasurement != null);
            Console.WriteLine("WARNING !!! LASER IS BUSY ... Draw Circle");
            timer = Stopwatch.StartNew();
            var channels = new MeasurementChannel[4]
            {
                 MeasurementChannel.SampleX, //X
                 MeasurementChannel.SampleY, //Y
                 MeasurementChannel.LaserOn, //Gate 0/1
                 MeasurementChannel.OutputPeriod, //KHz
            };
            float hz = 10*1000; //10KHz (샘플링 주기 : 100usec)
            bool success = true;
            success &= rtc.ListBegin(laser);
            success &= rtcMeasurement.ListMeasurementBegin(hz, channels); //1Khz, 4개 채널
            success &= rtc.ListFrequency(50 * 1000, 2); // 주파수 50KHz, 펄스폭 2usec 
            success &= rtc.ListSpeed(500, 500); // 점프, 마크 속도 500mm/s
            success &= rtc.ListDelay(10, 100, 200, 200, 0); // 스캐너/레이저 지연값
            success &= rtc.ListJump(new Vector2(radius, 0)); //원 시작 위치
            success &= rtc.ListArc(new Vector2(0, 0), 360.0f); // 360도 원 그리기
            success &= rtcMeasurement.ListMeasurementEnd();
            success &= rtc.ListEnd();
            if (success)
                success &= rtc.ListExecute(true);
            
            var measurementFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plot", "measurement_circle.txt");
            success &= MeasurementHelper.Save(measurementFile, rtcMeasurement, hz, channels, false);
            if (success)
                Console.WriteLine($"Processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s. plot to file = {measurementFile}");

            return success;
        }
        
        /// <summary>
        /// draw rectangle
        /// </summary>
        /// <param name="laser"></param>
        /// <param name="rtc"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private static bool DrawRectangle(ILaser laser, IRtc rtc, float width, float height)
        {
            if (rtc.CtlGetStatus(RtcStatus.Busy))
                return false;
            var rtcMeasurement = rtc as IRtcMeasurement;
            Debug.Assert(rtcMeasurement != null);
            Console.WriteLine("WARNING !!! LASER IS BUSY ... Draw Rectangle");
            timer = Stopwatch.StartNew();
            MeasurementChannel[] channels = new MeasurementChannel[4]
            {
                 MeasurementChannel.SampleX, //X
                 MeasurementChannel.SampleY, //Y
                 MeasurementChannel.LaserOn, //Gate
                 MeasurementChannel.OutputPeriod, //KHz
            };
            float hz = 10000; //10 KHz
            bool success = true;
            success &= rtc.ListBegin(laser);
            success &= rtcMeasurement.ListMeasurementBegin(hz, channels); //10 Khz, 4개 채널
            success &= rtc.ListFrequency(50 * 1000, 2); // 주파수 50KHz, 펄스폭 2usec 
            success &= rtc.ListSpeed(500, 500); // 점프, 마크 속도 500mm/s
            success &= rtc.ListDelay(10, 100, 200, 200, 0); // 스캐너/레이저 지연값
            success &= rtc.ListJump(new Vector2(-width / 2, height / 2));
            success &= rtc.ListMark(new Vector2(width / 2, height / 2));
            success &= rtc.ListMark(new Vector2(width / 2, -height / 2));
            success &= rtc.ListMark(new Vector2(-width / 2, -height / 2));
            success &= rtc.ListMark(new Vector2(-width / 2, height / 2));
            success &= rtcMeasurement.ListMeasurementEnd();
            success &= rtc.ListEnd();
            if (success)
                success &= rtc.ListExecute(true);
            var measurementFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plot", "measurement_rectangle.txt");
            success &= MeasurementHelper.Save(measurementFile, rtcMeasurement, hz, channels, false);
            if (success)
                Console.WriteLine($"Processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s. plot to file = {measurementFile}");

            return success;
        }
       
    }
}
