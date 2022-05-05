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
 * RTC5 카드를 초기화 하고 리스트 버퍼3에 문자를 등록하고 이를 사용해 시간, 날짜, 시리얼 번호를 마킹한다
 * MOTF 와 연동하여 텍스트 마킹 내용이 RTC에 의해 처리되는 외부 제어 방식
 * Author : hong chan, choi / labspiral@gmail.com(http://spirallab.co.kr)
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

            var rtcCharSet = rtc as IRtcCharacterSet;
            var rtcSerialNo = rtc as IRtcSerialNo;
            Debug.Assert(rtcCharSet != null);
            Debug.Assert(rtcSerialNo != null);

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Testcase for spirallab.sirius. powered by labspiral@gmail.com (http://spirallab.co.kr)");
                Console.WriteLine("");
                Console.WriteLine("'C' : create character set");
                Console.WriteLine("'B' : delete character set");
                Console.WriteLine("'T' : mark to text");
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
                    case ConsoleKey.C:  // 문자집합 생성
                        CreateCharacterSet(laser, rtc);
                        break;
                    case ConsoleKey.B:
                        DeleteCharacterSet(rtc);
                        break;
                    case ConsoleKey.T:
                        MarkToText(laser, rtc);
                        break;
                    case ConsoleKey.D:
                        MarkToDate(laser, rtc);
                        break;
                    case ConsoleKey.I:
                        MarkToTime(laser, rtc);
                        break;
                    case ConsoleKey.S:
                        MarkToSerial(laser, rtc);
                        break;
                    case ConsoleKey.R:
                        rtcSerialNo.CtlSerialReset(100, 5);
                        break;
                }
                Console.WriteLine($"Processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s");
            } while (true);

            laser.Dispose();
            rtc.Dispose();
        }
        
        /// <summary>
        /// 폰트 RTC 내부 리스트 메모리에 등록
        /// </summary>
        /// <param name="laser"></param>
        /// <param name="rtc"></param>
        private static void CreateCharacterSet(ILaser laser, IRtc rtc)
        {
            var rtcCharSet = rtc as IRtcCharacterSet;
            Debug.Assert( rtcCharSet != null );

            //폰트 등록
            // 총 4개의 character set 등록 가능
            // 기본값  0 번 character set 으로 등록됨
            // example font : 0~9(숫자) ;(콜론) -(대쉬)
            rtcCharSet.CtlCharacterBegin('0');
            rtc.ListJump(new Vector2(5, 10));
            rtc.ListMark(new Vector2(5 + 0, 10 - 5));
            rtc.ListMark(new Vector2(5 + 0 + 0, 10 - 5 - 5));
            rtc.ListMark(new Vector2(5 + 0 + 0 - 5, 10 - 5 - 5));
            rtc.ListMark(new Vector2(5 + 0 + 0 - 5, 10 - 5 - 5 + 5));
            rtc.ListMark(new Vector2(5 + 0 + 0 - 5 + 0, 10 - 5 - 5 + 5 + 5));
            rtc.ListMark(new Vector2(5 + 0 + 0 - 5 + 0 + 5, 10 - 5 - 5 + 5 + 5 + 0));
            rtc.ListJump(new Vector2(5 + 0 + 0 - 5 + 0 + 5 + 5, 10 - 5 - 5 + 5 + 5 + 0 - 10));
            rtcCharSet.CtlCharacterEnd();

            rtcCharSet.CtlCharacterBegin('1');
            rtc.ListJump(new Vector2(5, 0));
            rtc.ListMark(new Vector2(5 + 0, 0 + 10));
            rtc.ListJump(new Vector2(5 + 0 + 5, 0 + 10 - 10));
            rtcCharSet.CtlCharacterEnd();

            rtcCharSet.CtlCharacterBegin('2');
            rtc.ListJump(new Vector2(0, 10 ));
            rtc.ListMark(new Vector2(0 + 5, 10+ 0) );
            rtc.ListMark(new Vector2(0 + 5 + 0, 10 + 0 - 5 ));
            rtc.ListMark(new Vector2(0 + 5 + 0 - 5, 10 + 0 - 5 + 0));
            rtc.ListMark(new Vector2(0 + 5 + 0 - 5 + 0, 10 + 0 - 5 + 0 - 5 ));
            rtc.ListMark(new Vector2(0 + 5 + 0 - 5 + 0 + 5, 10 + 0 - 5 + 0 - 5 + 0));
            rtc.ListJump(new Vector2(0 + 5 + 0 - 5 + 0 + 5 + 5, 10 + 0 - 5 + 0 - 5 + 0 + 0));
            rtcCharSet.CtlCharacterEnd();

            rtcCharSet.CtlCharacterBegin('3');
            rtc.ListJump(new Vector2(0, 10 ));
            rtc.ListMark(new Vector2(0 + 5, 10 + 0 ));
            rtc.ListMark(new Vector2(0 + 5 + 0, 10 + 0 - 5) );
            rtc.ListMark(new Vector2(0 + 5 + 0 - 5, 10 + 0 - 5 + 0 ));
            rtc.ListJump(new Vector2(0 + 5 + 0 - 5 + 5, 10 + 0 - 5 + 0 + 0));
            rtc.ListMark(new Vector2(0 + 5 + 0 - 5 + 5 + 0, 10 + 0 - 5 + 0 + 0 - 5 ));
            rtc.ListMark(new Vector2(0 + 5 + 0 - 5 + 5 + 0 - 5, 10 + 0 - 5 + 0 + 0 - 5 + 0));
            rtc.ListJump(new Vector2(0 + 5 + 0 - 5 + 5 + 0 - 5 + 10, 10 + 0 - 5 + 0 + 0 - 5 + 0 + 0));
            rtcCharSet.CtlCharacterEnd();

            rtcCharSet.CtlCharacterBegin('4');
            rtc.ListJump(new Vector2(0, 10) );
            rtc.ListMark(new Vector2(0 + 0, 10 -5) );
            rtc.ListMark(new Vector2(0 + 0 + 5, 10 - 5 + 0));
            rtc.ListJump(new Vector2(0 + 0 + 5 + 0, 10 - 5 + 0 + 5));
            rtc.ListMark(new Vector2(0 + 0 + 5 + 0 + 0, 10 - 5 + 0 + 5 - 10 ));
            rtc.ListJump(new Vector2(0 + 0 + 5 + 0 + 0 + 5, 10 - 5 + 0 + 5 - 10 + 0));
            rtcCharSet.CtlCharacterEnd();

            rtcCharSet.CtlCharacterBegin('5');
            rtc.ListJump(new Vector2(5, 10));
            rtc.ListMark(new Vector2(5 -5, 10 + 0 ));
            rtc.ListMark(new Vector2(5 - 5 + 0, 10 + 0 - 5 ));
            rtc.ListMark(new Vector2(5 - 5 + 0 + 5, 10 + 0 - 5 + 0) );
            rtc.ListMark(new Vector2(5 - 5 + 0 + 5 + 0, 10 + 0 - 5 + 0 - 5 ));
            rtc.ListMark(new Vector2(5 - 5 + 0 + 5 + 0 - 5, 10 + 0 - 5 + 0 - 5 + 0 ));
            rtc.ListJump(new Vector2(5 - 5 + 0 + 5 + 0 - 5 + 10, 10 + 0 - 5 + 0 - 5 + 0 + 0));
            rtcCharSet.CtlCharacterEnd();

            rtcCharSet.CtlCharacterBegin('6');
            rtc.ListJump(new Vector2(5, 10 ));
            rtc.ListMark(new Vector2(5 -5, 10 + 0) );
            rtc.ListMark(new Vector2(5 - 5 + 0, 10 + 0 - 5) );
            rtc.ListMark(new Vector2(5 - 5 + 0 + 0, 10 + 0 - 5 - 5 ));
            rtc.ListMark(new Vector2(5 - 5 + 0 + 0 + 5, 10 + 0 - 5 - 5 + 0));
            rtc.ListMark(new Vector2(5 - 5 + 0 + 0 + 5 + 0, 10 + 0 - 5 - 5 + 0 + 5));
            rtc.ListMark(new Vector2(5 - 5 + 0 + 0 + 5 + 0 - 5, 10 + 0 - 5 - 5 + 0 + 5 + 0));
            rtc.ListJump(new Vector2(5 - 5 + 0 + 0 + 5 + 0 - 5 + 10, 10 + 0 - 5 - 5 + 0 + 5 + 0 - 5));
            rtcCharSet.CtlCharacterEnd();

            rtcCharSet.CtlCharacterBegin('7');
            rtc.ListJump(new Vector2( 0, 10 ));
            rtc.ListMark(new Vector2( 0 + 5, 10 + 0 ));
            rtc.ListMark(new Vector2( 0 + 5 + 0, 10 + 0 - 5 ));
            rtc.ListMark(new Vector2( 0 + 5 + 0 + 0, 10 + 0 - 5 - 5) );
            rtc.ListJump(new Vector2(0 + 5 + 0 + 0 + 5, 10 + 0 - 5 - 5 + 0));
            rtcCharSet.CtlCharacterEnd();

            rtcCharSet.CtlCharacterBegin('8');
            rtc.ListJump(new Vector2(5, 10 ));
            rtc.ListMark(new Vector2(5 -5, 10 + 0 ));
            rtc.ListMark(new Vector2(5 - 5 + 0, 10 + 0 - 5 ));
            rtc.ListMark(new Vector2(5 - 5 + 0 + 5, 10 + 0 - 5 + 0));
            rtc.ListMark(new Vector2(5 - 5 + 0 + 5 + 0, 10 + 0 - 5 + 0 + 5));
            rtc.ListJump(new Vector2(5 - 5 + 0 + 5 + 0 + 0, 10 + 0 - 5 + 0 + 5 - 5 ));
            rtc.ListMark(new Vector2(5 - 5 + 0 + 5 + 0 + 0 + 0, 10 + 0 - 5 + 0 + 5 - 5 - 5 ));
            rtc.ListMark(new Vector2(5 - 5 + 0 + 5 + 0 + 0 + 0 - 5, 10 + 0 - 5 + 0 + 5 - 5 - 5 + 0 ));
            rtc.ListMark(new Vector2(5 - 5 + 0 + 5 + 0 + 0 + 0 - 5 + 0, 10 + 0 - 5 + 0 + 5 - 5 - 5 + 0 + 5 ));
            rtc.ListJump(new Vector2(5 - 5 + 0 + 5 + 0 + 0 + 0 - 5 + 0 + 10, 10 + 0 - 5 + 0 + 5 - 5 - 5 + 0 + 5 - 5 ));
            rtcCharSet.CtlCharacterEnd();

            rtcCharSet.CtlCharacterBegin('9');
            rtc.ListJump(new Vector2(5, 10 ));
            rtc.ListMark(new Vector2(5 - 5, 10 + 0) );
            rtc.ListMark(new Vector2(5 - 5 + 0, 10 + 0 - 5 ));
            rtc.ListMark(new Vector2(5 - 5 + 0 + 5, 10 + 0 - 5 + 0));
            rtc.ListMark(new Vector2(5 - 5 + 0 + 5 + 0, 10 + 0 - 5 + 0 + 5));
            rtc.ListJump(new Vector2(5 - 5 + 0 + 5 + 0 + 0, 10 + 0 - 5 + 0 + 5 - 5 ));
            rtc.ListMark(new Vector2(5 - 5 + 0 + 5 + 0 + 0 + 0, 10 + 0 - 5 + 0 + 5 - 5 - 5 ));
            rtc.ListMark(new Vector2(5 - 5 + 0 + 5 + 0 + 0 + 0 - 5, 10 + 0 - 5 + 0 + 5 - 5 - 5 + 0));
            rtc.ListJump(new Vector2(5 - 5 + 0 + 5 + 0 + 0 + 0 - 5 + 10, 10 + 0 - 5 + 0 + 5 - 5 - 5 + 0 + 0));
            rtcCharSet.CtlCharacterEnd();

            rtcCharSet.CtlCharacterBegin(';');
            rtc.ListJump(new Vector2(0, 2) );
            rtc.ListMark(new Vector2(0 + 2, 2 + 0) );
            rtc.ListMark(new Vector2(0 + 2 + 0, 2 + 0 + 2) );
            rtc.ListMark(new Vector2(0 + 2 + 0 - 2, 2 + 0 + 2 + 0) );
            rtc.ListMark(new Vector2(0 + 2 + 0 - 2 + 0, 2 + 0 + 2 + 0 - 2 ));
            rtc.ListJump(new Vector2(0 + 2 + 0 - 2 + 0 + 0, 2 + 0 + 2 + 0 - 2 + 4 ));
            rtc.ListMark(new Vector2(0 + 2 + 0 - 2 + 0 + 0 + 2, 2 + 0 + 2 + 0 - 2 + 4 + 0 ));
            rtc.ListMark(new Vector2(0 + 2 + 0 - 2 + 0 + 0 + 2 + 0, 2 + 0 + 2 + 0 - 2 + 4 + 0 + 2 ));
            rtc.ListMark(new Vector2(0 + 2 + 0 - 2 + 0 + 0 + 2 + 0 - 2, 2 + 0 + 2 + 0 - 2 + 4 + 0 + 2+ 0));
            rtc.ListMark(new Vector2(0 + 2 + 0 - 2 + 0 + 0 + 2 + 0 - 2 + 0, 2 + 0 + 2 + 0 - 2 + 4 + 0 + 2 + 0 - 2) );
            rtc.ListJump(new Vector2(0 + 2 + 0 - 2 + 0 + 0 + 2 + 0 - 2 + 0 + 5, 2 + 0 + 2 + 0 - 2 + 4 + 0 + 2 + 0 - 2 - 6));
            rtcCharSet.CtlCharacterEnd();

            rtcCharSet.CtlCharacterBegin(' ');
            rtc.ListJump(new Vector2(5, 0));
            rtcCharSet.CtlCharacterEnd();

            rtcCharSet.CtlCharacterBegin('-');
            rtc.ListJump(new Vector2(0, 5) );
            rtc.ListMark(new Vector2(0 + 5, 5 + 0 ));
            rtc.ListJump(new Vector2(0 + 5 + 5, 5 + 0 - 5));
            rtcCharSet.CtlCharacterEnd();
        }

        /// <summary>
        /// 폰트 RTC 내부 리스트 메모리에 등록된 폰트 집합 삭제
        /// </summary>
        /// <param name="rtc"></param>
        private static void DeleteCharacterSet(IRtc rtc)
        {
            var rtcCharSet = rtc as IRtcCharacterSet;
            Debug.Assert(rtcCharSet != null);

            rtcCharSet.CtlCharacterSetClear();
            Debug.Assert(false == rtcCharSet.CtlCharacterSetIsExist('0'));
        }

        /// <summary>
        /// 내부 리스트 메모리에 등록된 폰트를 이용한 텍스트 마킹
        /// </summary>
        /// <param name="laser"></param>
        /// <param name="rtc"></param>
            private static bool MarkToText(ILaser laser, IRtc rtc)
        {
            if (rtc.CtlGetStatus(RtcStatus.Busy))
                return false;
            bool success = true;
            var rtcCharSet = rtc as IRtcCharacterSet;
            Debug.Assert(rtcCharSet != null);

            success &= rtc.ListBegin(laser, ListType.Single);
            success &= rtc.ListJump(new Vector2(-10, 0));
            success &= rtcCharSet.ListText("123 456");
            if (success)
            {
                success &= rtc.ListEnd();
                success &= rtc.ListExecute();
            }
            return success;
        }

        /// <summary>
        /// 내부 리스트 메모리에 등록된 폰트를 이용한 날짜 마킹
        /// </summary>
        /// <param name="laser"></param>
        /// <param name="rtc"></param>
        private static bool MarkToDate(ILaser laser, IRtc rtc)
        {
            if (rtc.CtlGetStatus(RtcStatus.Busy))
                return false;
            bool success = true;
            var rtcCharSet = rtc as IRtcCharacterSet;
            Debug.Assert(rtcCharSet != null);

            success &= rtc.ListBegin(laser, ListType.Single);
            success &= rtc.ListJump(new Vector2(-10, 0));
            success &= rtcCharSet.ListDate(DateFormat.MonthDigit, true);
            success &= rtc.ListJump(new Vector2(10, 0));
            success &= rtcCharSet.ListDate(DateFormat.Day, true);
            if (success)
            {
                success &= rtc.ListEnd();
                success &= rtc.ListExecute();
            }
            return success;
        }
        /// <summary>
        /// 내부 리스트 메모리에 등록된 폰트를 이용한 시간 마킹
        /// </summary>
        /// <param name="laser"></param>
        /// <param name="rtc"></param>
        private static bool MarkToTime(ILaser laser, IRtc rtc)
        {
            if (rtc.CtlGetStatus(RtcStatus.Busy))
                return false;
            bool success = true;
            var rtcCharSet = rtc as IRtcCharacterSet;
            Debug.Assert(rtcCharSet != null);

            success &= rtc.ListBegin(laser, ListType.Single);
            success &= rtc.ListJump(new Vector2(-10, 0));
            success &= rtcCharSet.ListTime(TimeFormat.Hours24, true);
            success &= rtc.ListJump(new Vector2(10, 0));
            success &= rtcCharSet.ListTime(TimeFormat.Minutes, true);
            success &= rtc.ListJump(new Vector2(30, 0));
            success &= rtcCharSet.ListTime(TimeFormat.Seconds, true);
            if (success)
            {
                success &= rtc.ListEnd();
                success &= rtc.ListExecute();
            }
            return success;
        }
        /// <summary>
        /// 내부 리스트 메모리에 등록된 폰트를 이용한 시리얼 번호
        /// </summary>
        /// <param name="laser"></param>
        /// <param name="rtc"></param>
        private static bool MarkToSerial(ILaser laser, IRtc rtc)
        {
            if (rtc.CtlGetStatus(RtcStatus.Busy))
                return false;
            bool success = true;
            var rtcCharSet = rtc as IRtcCharacterSet;
            var rtcSerialNo = rtc as IRtcSerialNo;
            Debug.Assert(rtcCharSet != null);
            Debug.Assert(rtcSerialNo != null);

            //초기값: 1000, 증가값: 1
            rtcSerialNo.CtlSerialReset(1000, 1);
            success &= rtc.ListBegin(laser, ListType.Single);
            success &= rtc.ListJump(new Vector2(-10,-20));
            success &= rtcSerialNo.ListSerial(4, SerialFormat.LeadingWithZero);
            success &= rtc.ListJump(new Vector2(-10, 0));
            success &= rtcSerialNo.ListSerial(4, SerialFormat.LeadingWithZero);
            success &= rtc.ListJump(new Vector2(-10, 20));
            success &= rtcSerialNo.ListSerial(4, SerialFormat.LeadingWithZero);
            if (success)
            {
                success &= rtc.ListEnd();
                success &= rtc.ListExecute();
            }
            return success;
        }
    }
}
