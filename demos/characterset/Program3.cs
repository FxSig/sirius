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
 * Author : hong chan, choi / sepwind @gmail.com(https://sepwind.blogspot.com)
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
            ILaser laser = new LaserVirtual(0, "virtual", 20);  // 최대 출력 20W 의 가상 레이저 소스 생성
            #endregion

            var rtcCharSet = rtc as IRtcCharacterSet;

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Testcase for spirallab.sirius. powered by labspiral@gmail.com (https://sepwind.blogspot.com)");
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
                        rtcCharSet.CtlSerialReset(1000, 1);
                        break;
                }
                Console.WriteLine($"Processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s");
            } while (true);

            rtc.Dispose();
        }
        

        private static void MarkToDate(ILaser laser, IRtc rtc)
        {
            if (rtc.CtlGetStatus(RtcStatus.Busy))
                return;

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
            
            var rtcCharSet = rtc as IRtcCharacterSet;
            rtc.ListBegin(laser, ListType.Single);
            rtc.ListJump(0, 0);

            var markerArg = new MarkerArgDefault()
            {
                Rtc = rtc,
                Laser = laser,
            };

            date.Mark(markerArg);
            rtc.ListJump(10, 0);
            rtc.ListEnd();
            rtc.ListExecute(false);
        }

        private static void MarkToTime(ILaser laser, IRtc rtc)
        {
            if (rtc.CtlGetStatus(RtcStatus.Busy))
                return;

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

            var rtcCharSet = rtc as IRtcCharacterSet;
            rtc.ListBegin(laser, ListType.Single);
            rtc.ListJump(0, 0);
            var markerArg = new MarkerArgDefault()
            {
                Rtc = rtc,
                Laser = laser,
            };
            time.Mark(markerArg);
            rtc.ListEnd();
            rtc.ListExecute(false);
        }

        private static void MarkToSerial(ILaser laser, IRtc rtc)
        {
            if (rtc.CtlGetStatus(RtcStatus.Busy))
                return;

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

            var rtcCharSet = rtc as IRtcCharacterSet;
            rtc.ListBegin(laser, ListType.Single);
            rtc.ListJump(0, 0);
            var markerArg = new MarkerArgDefault()
            {
                Rtc = rtc,
                Laser = laser,
            };
            serial.Mark(markerArg);
            rtc.ListEnd();
            rtc.ListExecute(false);
        }

    }
}
