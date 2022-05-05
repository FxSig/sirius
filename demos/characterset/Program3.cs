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
 * IRtcCharacterSet 인터페이스를 사용하는 방법
 * RTC5 카드를 초기화 하고 리스트 버퍼3에 등록한 문자를 이용해 
 * 시리얼 번호, 날짜, 시간등을 마킹한다
 * 리스트 버퍼가 실행될때마다 시리얼 번호, 날짜, 시간이 RTC 내부에서 자동 처리된다.
 * RTC 인터페이스가 아닌 엔티티(SiriusText)에서 제공하는 Mark 함수를 이용한 자동마킹
 * Author : hong chan, choi / labspiral@gmail.com(http://spirallab.co.kr)
 * 
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;

namespace SpiralLab.Sirius
{
    class Program3
    {
        static TextSerial serial = new TextSerial();
        static TextDate date = new TextDate();
        static TextTime time = new TextTime();

        static void Main3(string[] args)
        {
            SpiralLab.Core.Initialize();

            #region initialize RTC 
            //var rtc = new RtcVirtual(0); //create Rtc for dummy
            var rtc = new Rtc5(0); //create Rtc5 controller
            //var rtc = new Rtc6(0); //create Rtc6 controller
            //var rtc = new Rtc6Ethernet(0, "192.168.0.100", "255.255.255.0"); //Scanlab Rtc6 Ethernet 제어기

            float fov = 60.0f;    // scanner field of view : 60mm            
            float kfactor = (float)Math.Pow(2, 20) / fov; // k factor (bits/mm) = 2^20 / fov
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);    // 스캐너 보정 파일 지정 : correction file
            rtc.CtlFrequency(50 * 1000, 2); // laser frequency : 50KHz, pulse width : 2usec
            rtc.CtlSpeed(100, 100); // default jump and mark speed : 100mm/s
            rtc.CtlDelay(10, 100, 200, 200, 0); // scanner and laser delays
            #endregion

            #region initialize Laser (virtual)
            var laser = new LaserVirtual(0, "virtual", 20);  // virtual laser source with max 20W power (최대 출력 20W 의 가상 레이저 소스 생성)
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
            laser.Rtc = rtc;
            laser.Initialize();
            laser.CtlPower(2);
            #endregion

            var rtcCharSet = rtc as IRtcCharacterSet;
            var rtcSerialNo = rtc as IRtcSerialNo;

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Testcase for spirallab.sirius. powered by labspiral@gmail.com (http://spirallab.co.kr)");
                Console.WriteLine("");
                Console.WriteLine("'D' : mark to date");
                Console.WriteLine("'I' : mark to time");
                Console.WriteLine("'S' : mark to serial number");
                Console.WriteLine("'R' : reset serial number");
                Console.WriteLine("'Q' : quit");
                Console.WriteLine("");
                Console.Write("select your target : ");
                key = Console.ReadKey(false);
                if (key.Key == ConsoleKey.Q)
                    break;
                Console.WriteLine("");                
                var timer = Stopwatch.StartNew();
                switch (key.Key)
                {                  
                    case ConsoleKey.D:
                        RtcCharacterSetHelper.Clear(rtc);
                        MarkToDate(laser, rtc);
                        break;
                    case ConsoleKey.I:
                        RtcCharacterSetHelper.Clear(rtc);
                        MarkToTime(laser, rtc);
                        break;
                    case ConsoleKey.S:
                        RtcCharacterSetHelper.Clear(rtc);
                        MarkToSerial(laser, rtc);
                        break;
                    case ConsoleKey.R:
                        RtcCharacterSetHelper.Clear(rtc);
                        rtcSerialNo.CtlSerialReset(1000, 1);
                        break;
                }
                Console.WriteLine($"Processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s");
            } while (true);

            rtc.Dispose();
        }        

        private static bool MarkToDate(ILaser laser, IRtc rtc)
        {
            if (rtc.CtlGetStatus(RtcStatus.Busy))
                return false;

            date.FontName = "Daum_Regular.ttf";
            date.Width = 2.0f;
            date.CapHeight = 3.0f;
            date.Align = Alignment.LeftMiddle;
            date.Location = Vector2.Zero;
            date.DateFormat = DateFormat.Day;
            date.IsLeadingWithZero = true;
            date.Angle = 90;
            date.Location = new Vector2(0, 10);
            //data 생성
            date.Regen();
            date.RegisterCharacterSetIntoRtc(rtc);

            bool success = true;
            var rtcCharSet = rtc as IRtcCharacterSet;
            success &= rtc.ListBegin(laser, ListType.Single);
            success &= rtc.ListJump(Vector2.Zero);
            var markerArg = new MarkerArgDefault()
            {
                Rtc = rtc,
                Laser = laser,
            };
            success &= date.Mark(markerArg);
            success &= rtc.ListJump(new Vector2(10, 0));
            if (success)
            {
                success &= rtc.ListEnd();
                success &= rtc.ListExecute(false);
            }
            return success;
        }

        private static bool MarkToTime(ILaser laser, IRtc rtc)
        {
            if (rtc.CtlGetStatus(RtcStatus.Busy))
                return false;

            time.FontName = "Daum_Regular.ttf";
            time.Width = 2.0f;
            time.CapHeight = 3.0f;
            time.Align = Alignment.LeftMiddle;
            time.Location = Vector2.Zero;
            time.TimeFormat = TimeFormat.Minutes;
            time.IsLeadingWithZero = true;
            time.Angle = 180;
            time.Location = new Vector2(0, 10);
            //data 생성
            time.Regen();
            time.RegisterCharacterSetIntoRtc(rtc);

            bool success = true;
            var rtcCharSet = rtc as IRtcCharacterSet;
            success &= rtc.ListBegin(laser, ListType.Single);
            success &= rtc.ListJump(Vector2.Zero);
            var markerArg = new MarkerArgDefault()
            {
                Rtc = rtc,
                Laser = laser,
            };
            success &= time.Mark(markerArg);
            if (success)
            {
                success &= rtc.ListEnd();
                success &= rtc.ListExecute(false);
            }
            return success;
        }

        private static bool MarkToSerial(ILaser laser, IRtc rtc)
        {
            if (rtc.CtlGetStatus(RtcStatus.Busy))
                return false;

            serial.FontName = "Daum_Regular.ttf";
            serial.Width = 2.0f;
            serial.CapHeight = 3.0f;
            serial.Align = Alignment.LeftMiddle;
            serial.Location = Vector2.Zero;
            serial.NumOfDigits = 4;
            serial.SerialFormat = SerialFormat.LeadingWithZero;
            serial.Angle = -90;
            serial.Location = new Vector2(0, 10);
            //data 생성
            serial.Regen();
            serial.RegisterCharacterSetIntoRtc(rtc);

            bool success = true;
            var rtcCharSet = rtc as IRtcCharacterSet;
            success &= rtc.ListBegin(laser, ListType.Single);
            success &= rtc.ListJump(Vector2.Zero);
            var markerArg = new MarkerArgDefault()
            {
                Rtc = rtc,
                Laser = laser,
            };
            success &= serial.Mark(markerArg);
            if (success)
            {
                success &= rtc.ListEnd();
                success &= rtc.ListExecute(false);
            }
            return success;
        }
    }
}
