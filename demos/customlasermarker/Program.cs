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
 * Author : hong chan, choi / labspiral@gmail.com(http://spirallab.co.kr)
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
            SpiralLab.Core.Initialize();

            #region initialize RTC 
            //var rtc = new RtcVirtual(0); //create Rtc for dummy
            var rtc = new Rtc5(0); //create Rtc5 controller
            //var rtc = new Rtc6(0); //create Rtc6 controller
            //var rtc = new Rtc6Ethernet(0, "192.168.0.100", "255.255.255.0"); //실험적인 상태 (Scanlab Rtc6 Ethernet 제어기)
            //var rtc = new Rtc6SyncAxis(0, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "syncAxis", "syncAXISConfig.xml")); //실험적인 상태 (Scanlab XLSCAN 솔류션)

            float fov = 60.0f;    /// scanner field of view : 60mm                                
            float kfactor = (float)Math.Pow(2, 20) / fov; /// k factor (bits/mm) = 2^20 / fov
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);    ///default correction file
            rtc.CtlFrequency(50 * 1000, 2); ///laser frequency : 50KHz, pulse width : 2usec
            rtc.CtlSpeed(100, 100); /// default jump and mark speed : 100mm/s
            rtc.CtlDelay(10, 100, 200, 200, 0); ///scanner and laser delays
            #endregion

            #region initialize Laser source
            //var laser = new LaserVirtual(0, "Virtual", 20);
            //var laser = new IPGYLP(0, "IPG YLP", 1, 20);
            //var laser = new JPTTypeE(0, "JPT Type E", 1, 20);
            //var laser = new SPIG4(0, "SPI G3/4", 1, 20);
            //var laser = new PhotonicsIndustryDX(0, "PI", 1, 20);
            //var laser = new AdvancedOptoWaveFotia(0, "Fotia", 1, 20);
            //var laser = new CoherentAviaLX(0, "Avia LX", 1, 20);
            var laser = new YourCustomLaser(0, "custom laser", 20.0f);
            //var laser2 = new YourCustomLaser2(0, "custom laser", 20.0f, 1);

            laser.Rtc = rtc;
            laser.Initialize();
            laser.CtlPower(10);
            #endregion

            #region motor z axis
            IMotor motorZ = new MotorZ();
            motorZ.Initialize();
            motorZ.CtlHomeSearch();
            #endregion

            #region prepare your marker
            // 사용자 정의 마커 생성
            var marker = new YourCustomMarker(0);
            marker.Name = "custom marker";
            //가공 완료 이벤트 핸들러 등록
            marker.OnFinished += Marker_OnFinished;
            #endregion

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("Testcase for spirallab.sirius. powered by labspiral@gmail.com (http://spirallab.co.kr)");
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
                        DrawByMarker(rtc, laser, marker, motorZ);
                        break;
                    case ConsoleKey.F:
                        SpiralLab.Sirius.LaserForm laerForm = new SpiralLab.Sirius.LaserForm();
                        laerForm.Laser = laser;
                        laerForm.ShowDialog();
                        break;
                }

            } while (true);

            rtc.Dispose();
        }

        private static bool DrawByMarker(IRtc rtc, ILaser laser, IMarker marker, IMotor motor)
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
            // 마커 가공 준비
            marker.Ready( new MarkerArgDefault()
            {
                Document = doc,
                Rtc = rtc,
                Laser = laser,
                MotorZ = motor,
            });
            // 하나의 오프셋 정보 추가
            marker.MarkerArg.Offsets.Clear();
            marker.MarkerArg.Offsets.Add(Offset.Zero);
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
