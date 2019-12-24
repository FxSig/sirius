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
 * 마커는 
 * 
 * Author : hong chan, choi / sepwind @gmail.com(https://sepwind.blogspot.com)
 * 
 */


using System;
using System.Diagnostics;
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
            IRtc rtc = new RtcVirtual(0); ///가상 rtc 제어기 생성
            //IRtc rtc = new Rtc5(0); ///rtc 5 제어기 생성
            float fov = 60.0f;    /// scanner field of view : 60mm                                
            float kfactor = (float)Math.Pow(2, 20) / fov; /// k factor (bits/mm) = 2^20 / fov
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);    ///default correction file
            rtc.CtlFrequency(50 * 1000, 2); ///laser frequency : 50KHz, pulse width : 2usec
            rtc.CtlSpeed(100, 100); /// default jump and mark speed : 100mm/s
            rtc.CtlDelay(10, 100, 200, 200, 0); ///scanner and laser delays
            #endregion

            #region initialize Laser source
            ILaser laser = new YourCustomLaser(0, "custom laser", 20.0f, PowerXFactor.ByUser);
            laser.Initialize();
            var pen = new Pen
            {
                Power = 10.0f,
            };
            laser.CtlPower(rtc, pen);
            #endregion

            #region prepare your marker
            var marker = new YourCustomMarker(0);
            marker.Name = "custom marker";
            marker.OnFinished += Marker_OnFinished;
            #endregion

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Testcase for spirallab.sirius. powered by labspiral@gmail.com (https://sepwind.blogspot.com)");
                Console.WriteLine("");
                Console.WriteLine("'M' : mark by your custom marker");
                Console.WriteLine("'L' : pop up your custom laser form");
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
                        DrawByMarker(rtc, laser, marker);
                        break;
                    case ConsoleKey.L:
                        Console.WriteLine("\r\nLASER FORM");
                        PopUpLaserForm(laser);
                        break;
                }

            } while (true);

            rtc.Dispose();
        }

        private static void DrawByMarker(IRtc rtc, ILaser laser, IMarker marker)
        {
            #region load from sirius file
            var dlg = new OpenFileDialog();
            dlg.Filter = "sirius data files (*.sirius)|*.sirius|dxf cad files (*.dxf)|*.dxf|All Files (*.*)|*.*";
            dlg.Title = "Open to data file";
            DialogResult result = dlg.ShowDialog();
            if (result != DialogResult.OK)
                return;
            string ext = Path.GetExtension(dlg.FileName);
            IDocument doc = null;
            if (0 == string.Compare(ext, ".dxf", true))
                doc = DocumentSerializer.OpenDxf(dlg.FileName);
            else if (0 == string.Compare(ext, ".sirius", true))
                doc = DocumentSerializer.OpenSirius(dlg.FileName);
            #endregion

            Debug.Assert(null != doc);
            marker.Ready(doc, rtc, laser);
            marker.Offsets.Clear();
            marker.Offsets.Add(Offset.Zero);
            marker.Start();
        }

        private static void PopUpLaserForm(ILaser laser)
        {
            laser.Form.ShowDialog();
        }

        private static void Marker_OnFinished(IMarker sender, TimeSpan span)
        {
            Console.WriteLine($"{sender.Name} finished : {span.ToString()}");
        }

    }
  
}
