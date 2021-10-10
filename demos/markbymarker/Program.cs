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
 *  또한 가공을 위해 소스 문서(Document)를 복제(Clone)하고 내부 처리 쓰레드에서 이 복제본을 가지고 가공이 시작된다. 
 *  가공데이타를 복제하고 시작 방식이기 때문에 엔티티의 화면(View) 편집과는 영향이 없다
 *  
 * Author : hong chan, choi / labspiral@gmail.com(http://spirallab.co.kr)
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
            //var rtc = new RtcVirtual(0); //create Rtc for dummy
            var rtc = new Rtc5(0); //create Rtc5 controller
            //var rtc = new Rtc6(0); //create Rtc6 controller
            //var rtc = new Rtc6Ethernet(0, "192.168.0.100", "255.255.255.0"); //실험적인 상태 (Scanlab Rtc6 Ethernet 제어기)
            //var rtc = new Rtc6SyncAxis(0, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "syncAxis", "syncAXISConfig.xml")); //실험적인 상태 (Scanlab XLSCAN 솔류션)

            float fov = 60.0f;    // scanner field of view : 60mm                                
            float kfactor = (float)Math.Pow(2, 20) / fov; // k factor (bits/mm) = 2^20 / fov
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);    //default correction file
            rtc.CtlFrequency(50 * 1000, 2); //laser frequency : 50KHz, pulse width : 2usec
            rtc.CtlSpeed(100, 100); // default jump and mark speed : 100mm/s
            rtc.CtlDelay(10, 100, 200, 200, 0); //scanner and laser delays
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

            #region create entity at 0,0 location
            var doc = new DocumentDefault("3x3 scanner field correction");
            // 레이어 생성
            var layer = new Layer("default");
            // 나선 개체 추가
            layer.Add(new Spiral(0.0f, 0.0f, 0.5f, 2.0f, 5, true));
            // 레이어의 모든 개채들 내부 데이타 계산및 갱신
            layer.Regen();
            // 문서에 레이어 추가
            doc.Layers.Add(layer);
            // 문서 저장
            DocumentSerializer.Save(doc, "test.sirius");
            #endregion

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("Testcase for spirallab.sirius. powered by labspiral@gmail.com (http://spirallab.co.kr)");
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("'M' : draw entities by marker");
                Console.WriteLine("'O' : draw entities by marker with offsets");
                Console.WriteLine("'Q' : quit");
                Console.WriteLine($"{Environment.NewLine}");
                Console.Write("select your target : ");
                key = Console.ReadKey(false);
                if (key.Key == ConsoleKey.Q)
                    break;
                Console.WriteLine($"{Environment.NewLine}");
                switch (key.Key)
                {
                    case ConsoleKey.M:
                        Console.WriteLine("WARNING !!! LASER IS BUSY ...");
                        DrawByMarker(doc, rtc, laser);
                        break;
                    case ConsoleKey.O:
                        Console.WriteLine("WARNING !!! LASER IS BUSY ...");
                        DrawByMarkerWithOffset(doc, rtc, laser);
                        break;
                }

            } while (true);

            rtc.Dispose();
        }
        private static bool DrawByMarker(IDocument doc, IRtc rtc, ILaser laser)
        {
            var marker = new MarkerDefault(0);            
            marker.Name = "marker #1";
            //가공 완료 이벤트 핸들러 등록
            marker.OnFinished += Marker_OnFinished;

            var markerArg = new MarkerArgDefault()
            {
                Document = doc,
                Rtc = rtc,
                Laser = laser,
            };
            // 하나의 오프셋 정보(0,0 및 회전각도 0) 를 추가한다.
            markerArg.Offsets.Add(Offset.Zero);
            bool success = true;
            // 마커에 가공 문서(doc)및 rtc, laser 정보를 전달하고 가공 준비를 실시한다.
            success &= marker.Ready(markerArg);
            // 가공을 시작한다. 
            success &= marker.Start();
            return success;
        }
        private static bool DrawByMarkerWithOffset(IDocument doc, IRtc rtc, ILaser laser)
        {
            var marker = new MarkerDefault(0);
            marker.Name = "marker #2";
            //가공 완료 이벤트 핸들러 등록
            marker.OnFinished += Marker_OnFinished;

            var markerArg = new MarkerArgDefault()
            {
                Document = doc,
                Rtc = rtc,
                Laser = laser,
            };
            // 9개의 오프셋 정보를 추가한다
            markerArg.Offsets.Clear();
            markerArg.Offsets.Add(new Offset(-20.0f, 20.0f, -90f));
            markerArg.Offsets.Add(new Offset(0.0f, 20.0f, 0.0f));
            markerArg.Offsets.Add(new Offset(20.0f, 20.0f, 90.0f));
            markerArg.Offsets.Add(new Offset(-20.0f, 0.0f, -180.0f));
            markerArg.Offsets.Add(new Offset(0.0f, 0.0f, 0.0f));
            markerArg.Offsets.Add(new Offset(20.0f, 0.0f, 180.0f));
            markerArg.Offsets.Add(new Offset(-20.0f, -20.0f, -270.0f));
            markerArg.Offsets.Add(new Offset(0.0f, -20.0f, 0.0f));
            markerArg.Offsets.Add(new Offset(20.0f, -20.0f, 270.0f));
            bool success = true;
            // 마커에 가공 문서(doc)및 rtc, laser 정보를 전달하고 가공 준비를 실시한다.
            success &= marker.Ready(markerArg);
            // 가공을 시작한다. 
            success &= marker.Start();
            return success;
        }
        private static void Marker_OnFinished(IMarker sender, IMarkerArg arg)
        {
            var span = arg.EndTime - arg.StartTime;
            Console.WriteLine($"{sender.Name} finished : {span.ToString()} sec");
        }
    }
}
