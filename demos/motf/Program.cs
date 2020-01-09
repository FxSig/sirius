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
 * IRtc + IRtcMOTF 인터페이스를 직접 사용하는 방법
 * RTC5 + MOTF 카드를 초기화 하고 엔코더 리셋, MOTF 마킹을 한다
 * Author : hong chan, choi / sepwind @gmail.com(https://sepwind.blogspot.com)
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
            //var rtc = new RtcVirtual(0); ///create Rtc for dummy
            //var rtc = new Rtc5(0); ///create Rtc5 controller
            //var rtc = new Rtc6(0); ///create Rtc6 controller
            //var rtc = new Rtc6Ethernet(0, "192.168.0.200"); ///create Rtc6 ethernet controller
            //var rtc = new Rtc53D(0); ///create Rtc5 + 3D option controller
            //var rtc = new Rtc63D(0); ///create Rtc5 + 3D option controller
            //var rtc = new Rtc5DualHead(0); ///create Rtc5 + Dual head option controller
            var rtc = new Rtc5MOTF(0); ///create Rtc5 + MOTF option controller
            //var rtc = new Rtc6MOTF(0); ///create Rtc6 + MOTF option controller
            //var rtc = new Rtc6SyncAxis(0, "syncAXISConfig.xml"); ///create Rtc6 + XL-SCAN (ACS+SYNCAXIS) option controller

            float fov = 60.0f;    /// scanner field of view : 60mm            
            float kfactor = (float)Math.Pow(2, 20) / fov; /// k factor (bits/mm) = 2^20 / fov
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);    /// 스캐너 보정 파일 지정 : correction file
            rtc.CtlFrequency(50 * 1000, 2); /// laser frequency : 50KHz, pulse width : 2usec
            rtc.CtlSpeed(100, 100); /// default jump and mark speed : 100mm/s
            rtc.CtlDelay(10, 100, 200, 200, 0); /// scanner and laser delays

            var rtcMOTF = rtc as IRtcMOTF;
            #endregion

            #region initialize Laser (virtial)
            ILaser laser = new LaserVirtual(0, "virtual", 20);
            #endregion

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Testcase for spirallab.sirius. powered by labspiral@gmail.com (https://sepwind.blogspot.com)");
                Console.WriteLine("");
                Console.WriteLine("'R' : encoder reset");
                Console.WriteLine("'D' : show dialog");
                Console.WriteLine("'C' : draw circle with MOTF");
                Console.WriteLine("'Q' : quit");
                Console.WriteLine("");
                Console.Write("select your target : ");
                key = Console.ReadKey(false);
                if (key.Key == ConsoleKey.Q)
                    break;
                Console.WriteLine("\r\nWARNING !!! LASER IS BUSY ...");
                var timer = Stopwatch.StartNew();
                switch (key.Key)
                {
                    case ConsoleKey.R:
                        /// X,Y 엔코더값을 0,0 으로 리셋합니다.
                        rtcMOTF.CtlEncoderReset();
                        /// X,Y 엔코더값을 0,0 으로 리셋한후, 가상의 엔코더를 100mm/s 으로 동작시킵니다.
                        //rtcMOTF.CtlEncoderReset(0,0, 100, 100);
                        break;
                    case ConsoleKey.D:
                        rtc.Form.Show();
                        break;
                    case ConsoleKey.C:
                        DrawCircleWithPosition(laser, rtc);
                        break;
                }

                Console.WriteLine($"processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s");
            } while (true);

            rtc.Dispose();
        }
        private static void DrawCircleWithPosition(ILaser laser, IRtc rtc)
        {
            var motf = rtc as IRtcMOTF;
            /// RTC 15핀 커넥터에 있는 /START , /STOP 입력핀을 시작, 중지 트리거로 사용합니다.
            ///turn on external /start
            ///turn on reset encoder when external start
            var extCtrl = new RtcExternalControlMode();
            extCtrl.Add(RtcExternalControlMode.Signal.EncoderReset);
            extCtrl.Add(RtcExternalControlMode.Signal.ExternalStart);
            extCtrl.Add(RtcExternalControlMode.Signal.ExternalStartAgain);
            motf.CtlExternalControl(extCtrl);


            rtc.ListBegin(laser);
            ///직선을 그립니다. (엔코더 입력과 무관합니다)
            rtc.ListJump(new Vector2(0, 0));
            rtc.ListMark(new Vector2(10, 0));
            /// ListMOTFBegin 부터 ListMOTFEnd 사이의 모든 list 명령어는 엔코더증감값이 적용됩니다
            motf.ListMOTFBegin();
            /// 엔코더 X 값이  10mm 가 넘을때(Over) 까지 리스트 명령을 대기
            motf.ListMOTFWait(RtcEncoder.EncX, 10, EncoderWaitCondition.Over);//wait until encoder x position over 10.0mm
            ///엔코더 X 값이 위 조건을 만족한이후 원 을 그린다
            rtc.ListJump(new Vector2((float)10, 0)); 
            rtc.ListArc(new Vector2(0, 0), 360.0f);
            /// MOTF 중지및 0,0 위치(스캐너 중심 위치)로 jump 실시
            motf.ListMOTFEnd(Vector2.Zero);

            rtc.ListEnd();
            ///외부 트리거(/START)에 의해 시작되므로 execute 호출은 하지 않는다
            ///rtc.ListExecute(); /// its not need to call because its started by external trigger
        }
      
    }
}
