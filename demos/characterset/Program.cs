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
 * Author : hong chan, choi / labspiral @gmail.com(http://spirallab.co.kr)
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
                Console.WriteLine("Testcase for spirallab.sirius. powered by labspiral@gmail.com (http://spirallab.co.kr)");
                Console.WriteLine("");
                Console.WriteLine("'C' : create character set");
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
                        rtcCharSet.CtlSerialReset(100, 5);
                        break;
                }
                Console.WriteLine($"Processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s");
            } while (true);

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

            //폰트 등록
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
            rtc.ListMark(new Vector2(0 + 5 + 0, 10 + 0 - 5) );
            rtc.ListMark(new Vector2(0 + 5 + 0 - 5, 10 + 0 - 5 + 0));
            rtc.ListMark(new Vector2(0 + 5 + 0 - 5 + 0, 10 + 0 - 5 + 0 - 5) );
            rtc.ListMark(new Vector2(0 + 5 + 0 - 5 + 0 + 5, 10 + 0 - 5 + 0 - 5 + 0));
            rtc.ListJump(new Vector2(0 + 5 + 0 - 5 + 0 + 5 + 5, 10 + 0 - 5 + 0 - 5 + 0 + 0));
            rtcCharSet.CtlCharacterEnd();

            rtcCharSet.CtlCharacterBegin('3');
            rtc.ListJump(new Vector2(0, 10 ));
            rtc.ListMark(new Vector2(0 + 5, 10 + 0 ));
            rtc.ListMark(new Vector2(0 + 5 + 0, 10 + 0 - 5 ));
            rtc.ListMark(new Vector2(0 + 5 + 0 - 5, 10 + 0 - 5 + 0 ));
            rtc.ListJump(new Vector2(0 + 5 + 0 - 5 + 5, 10 + 0 - 5 + 0 + 0));
            rtc.ListMark(new Vector2(0 + 5 + 0 - 5 + 5 + 0, 10 + 0 - 5 + 0 + 0 - 5 ));
            rtc.ListMark(new Vector2(0 + 5 + 0 - 5 + 5 + 0 - 5, 10 + 0 - 5 + 0 + 0 - 5 + 0));
            rtc.ListJump(new Vector2(0 + 5 + 0 - 5 + 5 + 0 - 5 + 10, 10 + 0 - 5 + 0 + 0 - 5 + 0 + 0));
            rtcCharSet.CtlCharacterEnd();

            rtcCharSet.CtlCharacterBegin('4');
            rtc.ListJump(new Vector2(0, 10 ));
            rtc.ListMark(new Vector2(0 + 0, 10 -5 ));
            rtc.ListMark(new Vector2(0 + 0 + 5, 10 - 5 + 0));
            rtc.ListJump(new Vector2(0 + 0 + 5 + 0, 10 - 5 + 0 + 5));
            rtc.ListMark(new Vector2(0 + 0 + 5 + 0 + 0, 10 - 5 + 0 + 5 - 10 ));
            rtc.ListJump(new Vector2(0 + 0 + 5 + 0 + 0 + 5, 10 - 5 + 0 + 5 - 10 + 0));
            rtcCharSet.CtlCharacterEnd();

            rtcCharSet.CtlCharacterBegin('5');
            rtc.ListJump(new Vector2(5, 10));
            rtc.ListMark(new Vector2(5 -5, 10 + 0 ));
            rtc.ListMark(new Vector2(5 - 5 + 0, 10 + 0 - 5 ));
            rtc.ListMark(new Vector2(5 - 5 + 0 + 5, 10 + 0 - 5 + 0 ));
            rtc.ListMark(new Vector2(5 - 5 + 0 + 5 + 0, 10 + 0 - 5 + 0 - 5 ));
            rtc.ListMark(new Vector2(5 - 5 + 0 + 5 + 0 - 5, 10 + 0 - 5 + 0 - 5 + 0 ));
            rtc.ListJump(new Vector2(5 - 5 + 0 + 5 + 0 - 5 + 10, 10 + 0 - 5 + 0 - 5 + 0 + 0));
            rtcCharSet.CtlCharacterEnd();

            rtcCharSet.CtlCharacterBegin('6');
            rtc.ListJump(new Vector2(5, 10 ));
            rtc.ListMark(new Vector2(5 -5, 10 + 0 ));
            rtc.ListMark(new Vector2(5 - 5 + 0, 10 + 0 - 5 ));
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
            rtc.ListMark(new Vector2( 0 + 5 + 0 + 0, 10 + 0 - 5 - 5 ));
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
            rtc.ListMark(new Vector2(5 - 5, 10 + 0 ));
            rtc.ListMark(new Vector2(5 - 5 + 0, 10 + 0 - 5 ));
            rtc.ListMark(new Vector2(5 - 5 + 0 + 5, 10 + 0 - 5 + 0));
            rtc.ListMark(new Vector2(5 - 5 + 0 + 5 + 0, 10 + 0 - 5 + 0 + 5));
            rtc.ListJump(new Vector2(5 - 5 + 0 + 5 + 0 + 0, 10 + 0 - 5 + 0 + 5 - 5 ));
            rtc.ListMark(new Vector2(5 - 5 + 0 + 5 + 0 + 0 + 0, 10 + 0 - 5 + 0 + 5 - 5 - 5 ));
            rtc.ListMark(new Vector2(5 - 5 + 0 + 5 + 0 + 0 + 0 - 5, 10 + 0 - 5 + 0 + 5 - 5 - 5 + 0));
            rtc.ListJump(new Vector2(5 - 5 + 0 + 5 + 0 + 0 + 0 - 5 + 10, 10 + 0 - 5 + 0 + 5 - 5 - 5 + 0 + 0));
            rtcCharSet.CtlCharacterEnd();

            rtcCharSet.CtlCharacterBegin(';');
            rtc.ListJump(new Vector2(0, 2 ));
            rtc.ListMark(new Vector2(0 + 2, 2 + 0 ));
            rtc.ListMark(new Vector2(0 + 2 + 0, 2 + 0 + 2 ));
            rtc.ListMark(new Vector2(0 + 2 + 0 - 2, 2 + 0 + 2 + 0 ));
            rtc.ListMark(new Vector2(0 + 2 + 0 - 2 + 0, 2 + 0 + 2 + 0 - 2 ));
            rtc.ListJump(new Vector2(0 + 2 + 0 - 2 + 0 + 0, 2 + 0 + 2 + 0 - 2 + 4 ));
            rtc.ListMark(new Vector2(0 + 2 + 0 - 2 + 0 + 0 + 2, 2 + 0 + 2 + 0 - 2 + 4 + 0 ));
            rtc.ListMark(new Vector2(0 + 2 + 0 - 2 + 0 + 0 + 2 + 0, 2 + 0 + 2 + 0 - 2 + 4 + 0 + 2 ));
            rtc.ListMark(new Vector2(0 + 2 + 0 - 2 + 0 + 0 + 2 + 0 - 2, 2 + 0 + 2 + 0 - 2 + 4 + 0 + 2+ 0));
            rtc.ListMark(new Vector2(0 + 2 + 0 - 2 + 0 + 0 + 2 + 0 - 2 + 0, 2 + 0 + 2 + 0 - 2 + 4 + 0 + 2 + 0 - 2 ));
            rtc.ListJump(new Vector2(0 + 2 + 0 - 2 + 0 + 0 + 2 + 0 - 2 + 0 + 5, 2 + 0 + 2 + 0 - 2 + 4 + 0 + 2 + 0 - 2 - 6));
            rtcCharSet.CtlCharacterEnd();

            rtcCharSet.CtlCharacterBegin(' ');
            rtc.ListJump(new Vector2(5, 0));
            rtcCharSet.CtlCharacterEnd();

            rtcCharSet.CtlCharacterBegin('-');
            rtc.ListJump(new Vector2(0, 5 ));
            rtc.ListMark(new Vector2(0 + 5, 5 + 0 ));
            rtc.ListJump(new Vector2(0 + 5 + 5, 5 + 0 - 5));
            rtcCharSet.CtlCharacterEnd();
        }

        /// <summary>
        /// 내부 리스트 메모리에 등록된 폰트를 이용한 텍스트 마킹
        /// </summary>
        /// <param name="laser"></param>
        /// <param name="rtc"></param>
        private static void MarkToText(ILaser laser, IRtc rtc)
        {
            if (rtc.CtlGetStatus(RtcStatus.Busy))
                return;

            var rtcCharSet = rtc as IRtcCharacterSet;
            rtc.ListBegin(laser, ListType.Single);
            rtc.ListJump(new Vector2(-10, 0));
            rtcCharSet.ListText("123 456");
            rtc.ListEnd();
            rtc.ListExecute();
        }

        /// <summary>
        /// 내부 리스트 메모리에 등록된 폰트를 이용한 날짜 마킹
        /// </summary>
        /// <param name="laser"></param>
        /// <param name="rtc"></param>
        private static void MarkToDate(ILaser laser, IRtc rtc)
        {
            if (rtc.CtlGetStatus(RtcStatus.Busy))
                return;

            var rtcCharSet = rtc as IRtcCharacterSet;
            rtc.ListBegin(laser, ListType.Single);
            rtc.ListJump(new Vector2(-10, 0));
            rtcCharSet.ListDate(DateFormat.MonthDigit, true);
            rtc.ListJump(new Vector2(10, 0));
            rtcCharSet.ListDate(DateFormat.Day, true);
            rtc.ListEnd();
            rtc.ListExecute();
        }
        /// <summary>
        /// 내부 리스트 메모리에 등록된 폰트를 이용한 시간 마킹
        /// </summary>
        /// <param name="laser"></param>
        /// <param name="rtc"></param>
        private static void MarkToTime(ILaser laser, IRtc rtc)
        {
            if (rtc.CtlGetStatus(RtcStatus.Busy))
                return;

            var rtcCharSet = rtc as IRtcCharacterSet;
            rtc.ListBegin(laser, ListType.Single);
            rtc.ListJump(new Vector2(-10, 0));
            rtcCharSet.ListTime(TimeFormat.Hours24, true);
            rtc.ListJump(new Vector2(10, 0));
            rtcCharSet.ListTime(TimeFormat.Minutes, true);
            rtc.ListJump(new Vector2(30, 0));
            rtcCharSet.ListTime(TimeFormat.Seconds, true);
            rtc.ListEnd();
            rtc.ListExecute();
        }
        /// <summary>
        /// 내부 리스트 메모리에 등록된 폰트를 이용한 시리얼 번호
        /// </summary>
        /// <param name="laser"></param>
        /// <param name="rtc"></param>
        private static void MarkToSerial(ILaser laser, IRtc rtc)
        {
            if (rtc.CtlGetStatus(RtcStatus.Busy))
                return;

            var rtcCharSet = rtc as IRtcCharacterSet;
            //초기값: 1000, 증가값: 1
            rtcCharSet.CtlSerialReset(1000, 1);

            rtc.ListBegin(laser, ListType.Single);
            rtc.ListJump(new Vector2(-10,-20));
            rtcCharSet.ListSerial(4, SerialFormat.LeadingWithZero);
            rtc.ListJump(new Vector2(-10, 0));
            rtcCharSet.ListSerial(4, SerialFormat.LeadingWithZero);
            rtc.ListJump(new Vector2(-10, 20));
            rtcCharSet.ListSerial(4, SerialFormat.LeadingWithZero);
            rtc.ListEnd();
            rtc.ListExecute();
        }
    }
}
