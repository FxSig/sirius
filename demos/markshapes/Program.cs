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
 * IRtc 인터페이스를 직접 사용하는 방법 (How to use IRtc interface)
 * RTC5 카드를 초기화 하고 원, 사각형, 도트 원 을 그린다 (Initialize RTC card and Draw Circle/Rectangle/Dotted Circle ...)
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
    class Program
    {
        static Stopwatch timer;

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //initializing spirallab.sirius library engine (시리우스 라이브러리 초기화)
            //log configuration file: working directory\logs\NLogSpiralLab.config
            //로그 파일 설정은 작업 디렉토리\logs\NLogSpiralLab.config 파일이 사용됨
            SpiralLab.Core.Initialize();

            #region initialize RTC 
            //create Rtc for dummy (가상 RTC 카드)
            //var rtc = new RtcVirtual(0); 

            //create Rtc5 controller (RTC5 카드)
            var rtc = new Rtc5(0);
            rtc.InitLaser12SignalLevel = RtcSignalLevel.ActiveHigh;
            rtc.InitLaserOnSignalLevel = RtcSignalLevel.ActiveHigh;

            //create Rtc6 controller (RTC6 카드)
            //var rtc = new Rtc6(0); 
            //rtc.InitLaser12SignalLevel = RtcSignalLevel.ActiveHigh;
            //rtc.InitLaserOnSignalLevel = RtcSignalLevel.ActiveHigh;

            //create Rtc6 Ethernet (RTC6e 카드)
            //var rtc = new Rtc6Ethernet(0, "192.168.0.100", "255.255.255.0"); 

            // theoretically size of scanner field of view (이론적인 FOV 크기) : 60mm
            float fov = 60.0f;
            // k factor (bits/mm) = 2^20 / fov (2^16 if RTC4)
            float kfactor = (float)Math.Pow(2, 20) / fov; 
            // full path of correction file
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5"); //"cor_1to1.ctb" if RTC4

            // initialize rtc controller
            rtc.Initialize(kfactor, LaserMode.Yag5, correctionFile);
            // default frequency and pulse width
            // laser frequency : 50KHz, pulse width : 2usec (주파수 50KHz, 펄스폭 2usec)
            rtc.CtlFrequency(50 * 1000, 2);
            // default speed
            // jump and mark speed : 500mm/s (점프, 마크 속도 500mm/s)
            rtc.CtlSpeed(500, 500);
            // default delays
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
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("Testcase for spirallab.sirius. powered by hcchoi@spirallab.co.kr (http://spirallab.co.kr)");
                Console.WriteLine($"----------------------------------------------------------------------------------------");
                Console.WriteLine("'S' : get status");
                Console.WriteLine("'L' : draw line");
                Console.WriteLine("'C' : draw circle");
                Console.WriteLine("'R' : draw rectangle");
                Console.WriteLine("'D' : draw circle with dots");
                Console.WriteLine("'P' : draw square area with pixel operation");
                Console.WriteLine("'H' : draw heavy and slow job with thread");
                Console.WriteLine("'A' : abort to mark and finish the thread");
                Console.WriteLine("'F' : pop up laser source form");
                Console.WriteLine("'Q' : quit");
                Console.Write("Select your target : ");
                key = Console.ReadKey(false);
                Console.WriteLine($"{Environment.NewLine}");
                if (key.Key == ConsoleKey.Q)
                    break;
                switch (key.Key)
                {
                    case ConsoleKey.S:  //RTC's status (상태 확인)
                        if (rtc.CtlGetStatus(RtcStatus.Busy))
                            Console.WriteLine($"Rtc is busy!");
                        if (!rtc.CtlGetStatus(RtcStatus.PowerOK))
                            Console.WriteLine($"Scanner power is not ok");
                        if (!rtc.CtlGetStatus(RtcStatus.PositionAckOK))
                            Console.WriteLine($"Scanner position is not acked");
                        if (!rtc.CtlGetStatus(RtcStatus.NoError))
                            Console.WriteLine($"Rtc status has an error");                        
                        break;
                    case ConsoleKey.L:  
                        // draw line (선 모양 가공)
                        DrawLine(laser, rtc, -10,-10, 10, 10);
                        break;
                    case ConsoleKey.C:  
                        // draw circle (원 모양 가공)
                        DrawCircle(laser, rtc, 10);
                        break;
                    case ConsoleKey.R:  
                        // draw rectangle (사각형 모양 가공)
                        DrawRectangle(laser, rtc, 10, 10);
                        break;
                    case ConsoleKey.D:  
                        // draw dotted circle (점으로 이루어진 원 모양 가공)
                        DrawCircleWithDots(laser, rtc, 10, 1.0f);
                        break;
                    case ConsoleKey.P:  
                        // draw filled rectangle with raster (사각 영역을 픽셀(Raster)로 채우기
                        DrawSquareAreaWithPixels(laser, rtc, 10, 0.2f);
                        break;
                    case ConsoleKey.H:  
                        // draw with heavy works
                        // 시간이 오래 걸리는 모양을 가공
                        // 별도 가공 쓰레드 생성하여 처리
                        DrawTooHeavyAndSlowJob(laser, rtc);
                        break;
                    case ConsoleKey.A:  
                        // abort operation
                        // 강제 가공 중지
                        StopMarkAndReset(laser, rtc);
                        break;
                    case ConsoleKey.F:
                        // popup winforms for control laser source
                        // 레이저 소스 제어용 윈폼 팝업
                        SpiralLab.Sirius.Laser.LaserForm laerForm = new SpiralLab.Sirius.Laser.LaserForm(laser);
                        laerForm.ShowDialog();
                        break;
                }
            } while (true);

            if (rtc.CtlGetStatus(RtcStatus.Busy))
            {
                // abort marking operation
                rtc.CtlAbort();
                // wait until busy has finished
                rtc.CtlBusyWait();
            }

            //wait for thread has finished
            Program.thread?.Join();
            Program.thread = null;

            rtc.Dispose();
            laser.Dispose();
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
            Console.WriteLine("WARNING !!! LASER IS BUSY ... Draw Circle");
            timer = Stopwatch.StartNew();
            bool success = true;
            success &= rtc.ListBegin(laser);
            success &= rtc.ListJump(new Vector2(radius, 0));
            success &= rtc.ListArc(new Vector2(0, 0), 360.0f);
            success &= rtc.ListEnd();
            if (success)
                success &= rtc.ListExecute(true);
            Console.WriteLine($"Processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s");
            return success;
        }
        /// <summary>
        /// draw line
        /// </summary>
        /// <param name="laser"></param>
        /// <param name="rtc"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        private static bool DrawLine(ILaser laser, IRtc rtc, float x1, float y1, float x2, float y2)
        {
            if (rtc.CtlGetStatus(RtcStatus.Busy))
                return false;
            Console.WriteLine("WARNING !!! LASER IS BUSY ... DrawLine");
            timer = Stopwatch.StartNew();
            bool success = true;
            success &= rtc.ListBegin(laser);
            success &= rtc.ListJump(new Vector2(x1, y1));
            success &= rtc.ListMark(new Vector2(x2, y2));
            success &= rtc.ListEnd();
            if (success)
                success &= rtc.ListExecute(true);
            Console.WriteLine($"Processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s");
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
            Console.WriteLine("WARNING !!! LASER IS BUSY ... DrawRectangle");
            timer = Stopwatch.StartNew();
            bool success = true;
            success &= rtc.ListBegin(laser);
            success &= rtc.ListJump(new Vector2(-width / 2, height / 2));
            success &= rtc.ListMark(new Vector2(width / 2, height / 2));
            success &= rtc.ListMark(new Vector2(width / 2, -height / 2));
            success &= rtc.ListMark(new Vector2(-width / 2, -height / 2));
            success &= rtc.ListMark(new Vector2(-width / 2, height / 2));
            success &= rtc.ListEnd();
            if (success)
                success &= rtc.ListExecute(true);
            Console.WriteLine($"Processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s");
            return success;
        }
        /// <summary>
        /// draw cicle with dots
        /// </summary>
        /// <param name="laser"></param>
        /// <param name="rtc"></param>
        /// <param name="radius"></param>
        /// <param name="durationMsec"></param>
        private static bool DrawCircleWithDots(ILaser laser, IRtc rtc, float radius, float durationMsec)
        {
            if (rtc.CtlGetStatus(RtcStatus.Busy))
                return false;
            Console.WriteLine("WARNING !!! LASER IS BUSY ... DrawCircleWithDots");
            timer = Stopwatch.StartNew();
            bool success = true;
            success &= rtc.ListBegin(laser);
            for (float angle = 0; angle < 360; angle += 1)
            {
                double x = radius * Math.Sin(angle * Math.PI / 180.0);
                double y = radius * Math.Cos(angle * Math.PI / 180.0);
                success &= rtc.ListJump(new Vector2((float)x, (float)y));
                //laser signal on during specific time
                //지정된 짧은 시간동안 레이저 출사
                success &= rtc.ListLaserOn(durationMsec);
                if (!success)
                    break;
            }
            success &= rtc.ListEnd();
            if (success)
                success &= rtc.ListExecute(true);
            Console.WriteLine($"Processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s");
            return success;
        }
        /// <summary>
        /// draw square area with pixels
        /// </summary>
        /// <param name="laser"></param>
        /// <param name="rtc"></param>
        /// <param name="length"></param>
        /// <param name="gap"></param>
        private static bool DrawSquareAreaWithPixels(ILaser laser, IRtc rtc, float length, float gap)
        {
            if (rtc.CtlGetStatus(RtcStatus.Busy))
                return false;
            Console.WriteLine("WARNING !!! LASER IS BUSY ... DrawSquareAreaWithPixels");
            timer = Stopwatch.StartNew();
            // pixel operation 은 IRtcRaster 인터페이스에서 제공
            var rtcRaster = rtc as IRtcRaster;
            if (null == rtcRaster)
                return false;
            // pixel counts per line
            uint counts = (uint)(length / gap);
            //every 200 usec
            float period = 200; 
            // gap = distance from pixel to pixel
            var delta = new Vector2(gap, 0); 

            bool success = true;
            success &= rtc.ListBegin(laser);
            for (int i = 0; i < counts; i++)
            {
                //jumtp to start position (줄의 시작위치로 점프)
                success &= rtc.ListJump(new Vector2(0, i * gap));
                // pixel period : 200us, intervael : gap, total pixel counts, output analog channel : analog 2
                success &= rtcRaster.ListPixelLine(period, delta, counts, ExtensionChannel.ExtAO2);
                // each pixels
                for (int j = 0; j < counts; j++)
                    success &= rtcRaster.ListPixel(50, 0.5f); // each pixel with 50usec, 5V
                if (!success)
                    break;
            }
            success &= rtc.ListEnd();
            if (success)
                success &= rtc.ListExecute(true);
            Console.WriteLine($"Processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s");
            return success;
        }

        static Thread thread;
        static ILaser laser;
        static IRtc rtc;
        private static void DrawTooHeavyAndSlowJob(ILaser laser, IRtc rtc)
        {
            if (rtc.CtlGetStatus(RtcStatus.Busy))
            {
                Console.WriteLine("Processing are working already !");
                return;
            }

            Program.laser = laser;
            Program.rtc = rtc;
            //create worker thread
            Program.thread = new Thread(DoHeavyWork); 
            Program.thread.Start();
        }
        private static void DoHeavyWork()
        {
            bool success = true;
            float width = 1;
            float height = 1;

            Console.WriteLine("WARNING !!! LASER IS BUSY... DoHeavyWork thread");
            timer = Stopwatch.StartNew();
            //auto list buffer handling (double buffered lists)
            //대량의 데이타를 처리하기 위해 auto 리스트 버퍼 모드 사용
            success &= rtc.ListBegin(laser, ListType.Auto); 
            success &= rtc.ListJump(new Vector2(-width / 2, height / 2));
            for (int i = 0; i < 1000 * 1000 * 10; i++)
            {
                //list commands = 4 * 1000*1000*10 = approx. 40M counts (약 4천 만개의 리스트 명령)
                //warning!!! laser will be activated if buffer is enough to start (주의: 리스트 버퍼에 충분한 데이타가 쌓이면 자동 가공 시작됨)
                success &= rtc.ListMark(new Vector2(width / 2, height / 2));
                success &= rtc.ListMark(new Vector2(width / 2, -height / 2));
                success &= rtc.ListMark(new Vector2(-width / 2, -height / 2));
                success &= rtc.ListMark(new Vector2(-width / 2, height / 2));
                if (!success)
                    break;
            }
            if (success)
            {
                success &= rtc.ListEnd();
                //wait until list commands has finished
                success &= rtc.ListExecute(true);
            }
            if (success)
                Console.WriteLine("Success to mark by DoHeavyWork thread");
            else
                Console.WriteLine("Fail to mark by DoHeavyWork thread !");
            Console.WriteLine($"Processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s");
        }
        private static void StopMarkAndReset(ILaser laser, IRtc rtc)
        {
            Console.WriteLine("Trying to abort ...");

            //abort to mark
            rtc.CtlAbort();

            //wait for rtc busy off
            rtc.CtlBusyWait();

            //waiting to finish if thread has running ...
            Program.thread?.Join();
            Program.thread = null;

            //reset rtc's status 
            //Debug.Assert(rtc.CtlGetStatus(RtcStatus.Aborted));
            rtc.CtlReset();
            //Debug.Assert(rtc.CtlGetStatus(RtcStatus.NoError));
        }
    }
}
