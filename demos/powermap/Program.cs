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
 * IPowerMeter/IPowerMap 인터페이스를 직접 사용하는 방법 (How to use IPowerMeter and IPowerMap interface)
 * 파워메터 를 초기화하고 에너지를 계측하고 이를 매핑 테이블로 만들어 레이저 출력을 보상하는 기능을 활용
 * Author : hong chan, choi / hcchoi@spirallab.co.kr (http://spirallab.co.kr)
 * 
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    internal class Program
    {

        static string mapFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "powermap", "powermap.map");

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

            #region initialize PowerMeter and PowerMap
            //var powerMeter = new PowerMeterVirtual(0, "Virtual PM");
            var powerMeter = new PowerMeterOphir(0, "Ophir", "3040875");
            //var powerMeter = new PowerMeterCoherentPowerMax(0, "CoherentPM", 1);
            //var powerMeter = new PowerMeterThorLabsPMSeries(0, "PM100USB", "SERIALNO");
            powerMeter.Initialize();
            
            IPowerMap powerMap = new PowerMapDefault();
            //var mapFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "powermap", "test.pmap");
            //PowerMapSerializer.Open(powerMap, mapFile);
            powerMap.OnStartedMapping += PowerMap_OnStartedMapping;
            powerMap.OnProgress += PowerMap_OnProgress;
            powerMap.OnFailed += PowerMap_OnFailed;
            powerMap.OnFinishedMapping += PowerMap_OnFinishedMapping;
            powerMap.OnStartedVerify += PowerMap_OnStartedVerify;
            powerMap.OnFinishedVerify += PowerMap_OnFinishedVerify;
            #endregion

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("Testcase for spirallab.sirius. powered by hcchoi@spirallab.co.kr (http://spirallab.co.kr)");
                Console.WriteLine($"----------------------------------------------------------------------------------------");
                Console.WriteLine("'P' : start power mapping");
                Console.WriteLine("'C' : start power verication");
                Console.WriteLine("'S' : save power map");
                Console.WriteLine("'O' : open power map");
                Console.WriteLine("'F1' : powermeter winforms");
                Console.WriteLine("'F2' : powermap winforms");
                Console.WriteLine("'Q' : quit");
                Console.Write("Select your target : ");
                key = Console.ReadKey(false);
                Console.WriteLine($"{Environment.NewLine}");
                if (key.Key == ConsoleKey.Q)
                    break;
                switch (key.Key)
                {
                    case ConsoleKey.P:
                        StartPowerMap(powerMap, laser, rtc, powerMeter);
                        break;
                    case ConsoleKey.X:
                        StopPowerMap(powerMap);
                        break;
                    case ConsoleKey.C:
                        StartPowerVeification(powerMap, laser, rtc, powerMeter , 5);
                        break;
                    case ConsoleKey.S:
                        SavePowerMap(powerMap);
                        break;
                    case ConsoleKey.O:
                        OpenPowerMap(powerMap);
                        break;
                    case ConsoleKey.F1:
                        {
                            var form = new PowerMeterForm(powerMeter, laser);
                            form.ShowDialog();
                        }
                        break;
                    case ConsoleKey.F2:
                        {
                            var form = new PowerMapForm(null, powerMap, rtc, laser, powerMeter);
                            form.ShowDialog();
                        }
                        break;
                }
            } while (true);

            powerMap.OnStartedMapping -= PowerMap_OnStartedMapping;
            powerMap.OnProgress -= PowerMap_OnProgress;
            powerMap.OnFailed -= PowerMap_OnFailed;
            powerMap.OnFinishedMapping -= PowerMap_OnFinishedMapping;
            powerMap.OnStartedVerify -= PowerMap_OnStartedVerify;
            powerMap.OnFinishedVerify -= PowerMap_OnFinishedVerify;
            powerMeter.Dispose();
            laser.Dispose();
            rtc.Dispose();
        }
    
        private static void PowerMap_OnStartedMapping(IPowerMap sender, IPowerMapStartArg arg)
        {
            Console.WriteLine("Power mapping has started");
        }
        private static void PowerMap_OnProgress(object sender, EventArgs e)
        {
            Console.WriteLine("Power mapping working next step ...");
        }
        private static void PowerMap_OnFailed(object sender, EventArgs e)
        {
            Console.WriteLine("Power mapping/verfication has failed");
        }
        private static void PowerMap_OnFinishedMapping(object sender, EventArgs e)
        {
            Console.WriteLine("Power mapping has finished");
        }
        private static void PowerMap_OnStartedVerify(object sender, IPowerMapVerifyArg arg)
        {
            Console.WriteLine("Power verification has started");
        }
        private static void PowerMap_OnFinishedVerify(object sender, EventArgs e)
        {
            Console.WriteLine("Power verification has finished");
        }

        private static bool StartPowerMap(IPowerMap powerMap, ILaser laser, IRtc rtc, IPowerMeter powerMeter)
        {
            // 100 khz
            // 10 steps
            var arg = new PowerMapStartDefaultArg()
            { 
                Categories = new string[] { "100000" } ,
                Steps = 10,
                PowerMeter = powerMeter,
                Laser  = laser,
                Rtc = rtc,
                HoldTimeMsec = 5000,
                ThresholdWatt = 5,
            };

            return powerMap.CtlStart(arg);
        }
        private static bool StopPowerMap(IPowerMap powerMap)
        {
            return powerMap.CtlStop();
        }
        private static bool StartPowerVeification(IPowerMap powerMap, ILaser laser, IRtc rtc, IPowerMeter powerMeter, float targetWatt)
        {

            var arg = new PowerMapVerifyDefaultArg()
            {
                CategoryAndTargetWatts = new (string category, float watt)[] { ("100000", targetWatt) },
                PowerMeter = powerMeter,
                Laser = laser,
                Rtc = rtc,
                HoldTimeMsec = 5000,
                ThresholdWatt = 0.5f,
            };

            return powerMap.CtlVerify(arg);
        }

        private static bool SavePowerMap(IPowerMap powerMap)
        {
            return PowerMapSerializer.Save(powerMap, mapFile);
        }
        private static bool OpenPowerMap(IPowerMap powerMap)
        {
            return PowerMapSerializer.Open(powerMap, mapFile);
        }
    }
}
