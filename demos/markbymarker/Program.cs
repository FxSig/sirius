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
 * 지금까지 소개한 
 * 1. 가공 데이타(Document)
 * 2. 레이저 소스(Laser)
 * 3. 벡터 가공 장치(Rtc) 
 * 를 가지고 실제 가공을 실시하는 관리 객체를 마커(Marker) 라 한다.
 * 
 * 마커는 RTC, 레이저, 데이타(IDocument)를 모아 이를 가공하는 절차를 가지고 있는 객체로
 *  상태 (IsReady, IsBusy, IsError)및 오프셋 가공( List<Offset> )을 처리할수있다.
 *  또한 가공을 위해 소스 문서(Document)를 복제(Clone)한후 가공에 사용하며, 가공시에는 내부에 쓰레드를 만든후 가공이 시작되므로
 *  다른 함수호출들이  block 되지 않도록 처리해 준다.
 *  
 * Author : hong chan, choi / sepwind @gmail.com(https://sepwind.blogspot.com)
 * 
 */


using System;
using System.Diagnostics;
using System.IO;
using SpiralLab.Sirius;

namespace SpiralLab.Sirius
{
    class Program
    {
        static void Main(string[] args)
        {
            SpiralLab.Core.Initialize();

            #region initialize RTC 
            var rtc = new RtcVirtual(0);
            //var rtc = new Rtc5(0); ///create Rtc5 controller
            //var rtc = new Rtc6(0); ///create Rtc6 controller
            //var rtc = new Rtc6Ethernet(0, "192.168.0.200"); ///create Rtc6 ethernet controller
            //var rtc = new Rtc53D(0); ///create Rtc5 + 3D option controller
            //var rtc = new Rtc63D(0); ///create Rtc5 + 3D option controller
            //var rtc = new Rtc5DualHead(0); ///create Rtc5 + Dual head option controller
            //var rtc = new Rtc5MOTF(0); ///create Rtc5 + MOTF option controller
            //var rtc = new Rtc6MOTF(0); ///create Rtc6 + MOTF option controller
            //var rtc = new Rtc6SyncAxis(0); 
            //var rtc = new Rtc6SyncAxis(0, "syncAXISConfig.xml"); ///create Rtc6 + XL-SCAN (ACS+SYNCAXIS) option controller

            float fov = 60.0f;    /// scanner field of view : 60mm                                
            float kfactor = (float)Math.Pow(2, 20) / fov; /// k factor (bits/mm) = 2^20 / fov
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);    ///default correction file
            rtc.CtlFrequency(50 * 1000, 2); ///laser frequency : 50KHz, pulse width : 2usec
            rtc.CtlSpeed(100, 100); /// default jump and mark speed : 100mm/s
            rtc.CtlDelay(10, 100, 200, 200, 0); ///scanner and laser delays
            #endregion

            #region initialize Laser source
            ILaser laser = new LaserVirtual(0, "virtual", 10.0f);
            laser.Initialize();
            var pen = new Pen
            {
                Power = 10.0f,
            };
            laser.CtlPower(rtc, pen);
            #endregion

            #region create entity at 0,0 location
            var doc = new DocumentDefault("3x3 scanner field correction");
            var layer = new Layer("default");
            doc.Layers.Add(layer);
            layer.Add(new Spiral(0.0f, 0.0f, 0.5f, 2.0f, 5, true));

            var ds = new DocumentSerializer();
            ds.Save(doc, "test.sirius");
            #endregion

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Testcase for spirallab.sirius. powered by labspiral@gmail.com (https://sepwind.blogspot.com)");
                Console.WriteLine("");
                Console.WriteLine("'M' : draw entities by marker");
                Console.WriteLine("'O' : draw entities by marker with offsets");
                Console.WriteLine("'Q' : quit");
                Console.WriteLine("");
                Console.Write("select your target : ");
                key = Console.ReadKey(false);
                if (key.Key == ConsoleKey.Q)
                    break;
                switch (key.Key)
                {
                    case ConsoleKey.M:
                        Console.WriteLine("\r\nWARNING !!! LASER IS BUSY ...");
                        DrawByMarker(doc, rtc, laser);
                        break;
                    case ConsoleKey.O:
                        Console.WriteLine("\r\nWARNING !!! LASER IS BUSY ...");
                        DrawByMarkerWithOffset(doc, rtc, laser);
                        break;
                }

            } while (true);

            rtc.Dispose();
        }

        private static void DrawByMarker(IDocument doc, IRtc rtc, ILaser laser)
        {
            var marker = new MarkerDefault(0);            
            marker.Name = "marker #1";
            ///가공 완료 이벤트 핸들러 등록
            marker.OnFinished += Marker_OnFinished;
            /// 마커에 가공 문서(doc)및 rtc, laser 정보를 전달하고 가공 준비를 실시한다.
            marker.Ready(doc, rtc, laser);
            /// 하나의 오프셋 정보(0,0및 회전각도 0) 를 추가한다.
            marker.Offsets.Clear();
            marker.Offsets.Add(Offset.Zero);
            /// 가공을 시작한다. 
            marker.Start();
        }

        private static void DrawByMarkerWithOffset(IDocument doc, IRtc rtc, ILaser laser)
        {
            var marker = new MarkerDefault(0);
            marker.Name = "marker #2";
            ///가공 완료 이벤트 핸들러 등록
            marker.OnFinished += Marker_OnFinished;
            /// 9개의 오프셋 정보를 추가한다
            marker.Offsets.Clear();
            marker.Offsets.Add(new Offset(-20.0f, 20.0f, -90f));
            marker.Offsets.Add(new Offset(0.0f, 20.0f, 0.0f));
            marker.Offsets.Add(new Offset(20.0f, 20.0f, 90.0f));
            marker.Offsets.Add(new Offset(-20.0f, 0.0f, -180.0f));
            marker.Offsets.Add(new Offset(0.0f, 0.0f, 0.0f));
            marker.Offsets.Add(new Offset(20.0f, 0.0f, 180.0f));
            marker.Offsets.Add(new Offset(-20.0f, -20.0f, -270.0f));
            marker.Offsets.Add(new Offset(0.0f, -20.0f, 0.0f));
            marker.Offsets.Add(new Offset(20.0f, -20.0f, 270.0f));
            /// 마커에 가공 문서(doc)및 rtc, laser 정보를 전달하고 가공 준비를 실시한다.
            marker.Ready(doc, rtc, laser);
            /// 가공을 시작한다. 
            marker.Start();
        }
            
        private static void Marker_OnFinished(IMarker sender, TimeSpan span)
        {
            Console.WriteLine($"{sender.Name} finished : {span.ToString()}");
        }

    }
}
