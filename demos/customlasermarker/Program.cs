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
 * customized 된 마커(Marker)를 사용자가 직접 구현한다
 * 
 * Author : hong chan, choi / hcchoi@spirallab.co.kr (http://spirallab.co.kr)
 * 
 */

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Windows.Forms;
using SpiralLab.Sirius;

namespace SpiralLab.Sirius
{
    class Program
    {

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //initializing spirallab.sirius library engine (시리우스 라이브러리 초기화)
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

            #region motor z axis
            IMotor motorZ = new MotorZ();
            motorZ.Initialize();
            motorZ.CtlHomeSearch();
            #endregion

            #region prepare your marker
            // user defined custom marker instance
            // 사용자 정의 마커 생성
            var marker = new YourCustomMarker(0);
            marker.Name = "custom marker";
            // assign event handler at marker when finished 
            // 가공 완료 이벤트 핸들러 등록
            marker.OnFinished += Marker_OnFinished;
            #endregion

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("Testcase for spirallab.sirius. powered by hcchoi@spirallab.co.kr (http://spirallab.co.kr)");
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("'M' : mark by your custom marker");
                Console.WriteLine("'F' : laser form");
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
                        MarkByMarker(rtc, laser, marker, motorZ);
                        break;
                    case ConsoleKey.F:
                        SpiralLab.Sirius.Laser.LaserForm laerForm = new SpiralLab.Sirius.Laser.LaserForm(laser);
                        laerForm.ShowDialog();
                        break;
                }

            } while (true);

            rtc.Dispose();
        }

        private static bool MarkByMarker(IRtc rtc, ILaser laser, IMarker marker, IMotor motor)
        {
            #region load from sirius file
            var dlg = new OpenFileDialog();
            dlg.Filter = "sirius data files (*.sirius)|*.sirius|dxf cad files (*.dxf)|*.dxf|All Files (*.*)|*.*";
            dlg.Title = "Open to data file";
            DialogResult result = dlg.ShowDialog();
            if (result != DialogResult.OK)
                return false;
            string ext = Path.GetExtension(dlg.FileName);
            IDocument doc = null;
            if (0 == string.Compare(ext, ".dxf", true))
                doc = DocumentSerializer.OpenDxf(dlg.FileName);
            else if (0 == string.Compare(ext, ".sirius", true))
                doc = DocumentSerializer.OpenSirius(dlg.FileName);
            #endregion

            Debug.Assert(null != doc);
            Debug.Assert(doc.Layers.Count > 0);

            // override z position value at first layer if needed
            // 레이어에 설정된 Z 가공 위치를 변경한다 (필요하면)
            doc.Layers[0].ZPosition = 10;
            doc.Layers[0].ZPositionVel = 10;
            doc.Layers[0].IsZEnabled = true;

            // prepare marking argument
            // 마커 가공 준비
            marker.Ready( new MarkerArgDefault()
            {
                Document = doc,
                Rtc = rtc,
                Laser = laser,
                MotorZ = motor,
            });

            // no addition offset but center (origin location)
            // 하나의 오프셋(원점) 추가
            marker.MarkerArg.Offsets.Clear();
            marker.MarkerArg.Offsets.Add(Offset.Zero);
            // start to mark
            // 가공 시작
            return marker.Start();
        }
        private static void Marker_OnFinished(IMarker sender, IMarkerArg arg)
        {
            var span = arg.EndTime - arg.StartTime;
            Console.WriteLine($"{sender.Name} finished : {span.ToString()} sec");
        }
    }
}
