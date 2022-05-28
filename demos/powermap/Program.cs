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
 * Author : hong chan, choi / labspiral@gmail.com(http://spirallab.co.kr)
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
        static void Main(string[] args)
        {
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

            #region initialize PowerMeter and PowerMap
            var powerMeter = new PowerMeterVirtual(0, "Virtual PM");
            //var powerMeter = new PowerMeterOphirUsbI(0, "USBI", "SERIALNO");
            //var powerMeter = new PowerMeterThorLabsPM100Usb(0, "PM100USB", "SERIALNO");
            powerMeter.Initialize();

            //var powerMap = new PowerMapVirtual(0, "Virtual MAP");
            //var mapFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "map", "test.map");
            IPowerMap powerMap = new PowerMapDefault();
            #endregion

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("Testcase for spirallab.sirius. powered by labspiral@gmail.com (http://spirallab.co.kr)");
                Console.WriteLine($"----------------------------------------------------------------------------------------");
                Console.WriteLine("'P' : start laser on and create power mapping table with vary output power");
                Console.WriteLine("'C' : laser on with compensated laser output power");
                Console.WriteLine("'S' : save power map");
                Console.WriteLine("'O' : open power map");
                Console.WriteLine("'Q' : quit");
                Console.Write("Select your target : ");
                key = Console.ReadKey(false);
                Console.WriteLine($"{Environment.NewLine}");
                if (key.Key == ConsoleKey.Q)
                    break;
                switch (key.Key)
                {
                    case ConsoleKey.P:
                        StartPowerMap(laser, rtc, powerMeter, powerMap);
                        break;
                    case ConsoleKey.C:
                        TestCompensatedOutputPower(laser, rtc, powerMeter, powerMap, 5);
                        break;
                    case ConsoleKey.S:
                        SavePowerMap(powerMap);
                        break;
                    case ConsoleKey.O:
                        OpenPowerMap(ref powerMap);
                        break;
                }
            } while (true);

            powerMeter.Dispose();
            laser.Dispose();
            rtc.Dispose();
        }

        private static bool StartPowerMap(ILaser laser, IRtc rtc, IPowerMeter powerMeter, IPowerMap powerMap)
        {
            var powerControl = laser as IPowerControl;
            if (null == powerControl)
                return false;

            powerMeter.CtlStop();
            powerMeter.CtlClear();
            powerMap.Clear();

            bool success = true;
            var maxWatt = laser.MaxPowerWatt;
            float increaseWatt = maxWatt * 0.1f;
            float currentWatt = increaseWatt;
            powerMap.XName = "Watt";
            powerMap.XGap = increaseWatt;

            while (currentWatt <= maxWatt)
            {
                success &= powerControl.CtlPower(currentWatt);
                Console.WriteLine($"LASER TARGET POWER= {currentWatt}");
                if (DialogResult.Yes != MessageBox.Show($"Do you really want to laser on with power= {currentWatt}W ?", "WARNING!!! LASER", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    break;
                }
                //laser on (WARNING !!!)
                success &= rtc.CtlLaserOn();
                Console.WriteLine("WARNING !!! LASER IS ON ...");
                //for preheating 
                Thread.Sleep(5 * 1000);
                //start measurement
                success &= powerMeter.CtlStart();
                //during 5 secs
                Thread.Sleep(5 * 1000);
                //stop measurement
                success &= powerMeter.CtlStop();
                //laser off
                success &= rtc.CtlLaserOff();
                // average power (watt)
                var avgWatt = powerMeter.Data.Average();
                Console.WriteLine($"LASER AVG POWER= {avgWatt}");
                Console.WriteLine($"LASER IS OFF");
                success &= powerMap.Update("TEST", currentWatt, avgWatt);

                powerMeter.CtlClear();
                currentWatt += increaseWatt;
                if (!success)
                    break;
            }

            return success;
        }
        private static bool TestCompensatedOutputPower(ILaser laser, IRtc rtc, IPowerMeter powerMeter, IPowerMap powerMap, float targetWatt)
        {
            var powerControl = laser as IPowerControl;
            if (null == powerControl)
                return false;

            if (!powerMap.Data.ContainsKey("TEST"))
                return false;
            if (targetWatt > laser.MaxPowerWatt)
                return false;

            powerMeter.CtlStop();
            powerMeter.CtlClear();
            bool success = true;
            success &= powerMap.Lookup("TEST", targetWatt, out var xWatt);
            success &= powerControl.CtlPower(xWatt);
            Console.WriteLine($"LASER COMPENSATED POWER= {xWatt}");
            if (DialogResult.Yes != MessageBox.Show($"Do you really want to laser on with power= {targetWatt}->{xWatt}W ?", "WARNING!!! LASER", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                return false;
            }
            success &= rtc.CtlLaserOn();
            Console.WriteLine("WARNING !!! LASER IS ON ...");
            //for preheating 
            Thread.Sleep(5 * 1000);
            //start measurement
            success &= powerMeter.CtlStart();
            //during 5 secs
            Thread.Sleep(5 * 1000);
            //stop measurement
            success &= powerMeter.CtlStop();
            //laser off
            success &= rtc.CtlLaserOff();
            // average power (watt)
            var avgWatt = powerMeter.Data.Average();
            Console.WriteLine($"LASER AVG POWER= {avgWatt}");
            Console.WriteLine($"LASER IS OFF");

            powerMeter.CtlClear();
            var deviationWatt = Math.Abs(targetWatt - avgWatt);
            //+-0.2W 이내 출력 오차를 성공으로 판단
            success &= deviationWatt < 0.2f;
            Console.WriteLine($"LASER POWER DEVIATION = {targetWatt - xWatt}W");
            return success;
        }

        private static bool SavePowerMap(IPowerMap powerMap)
        {
            var mapFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "map", "powermap.map");
            return PowerMapSerializer.Save(powerMap, mapFile);

        }
        private static bool OpenPowerMap(ref IPowerMap powerMap)
        {
            var mapFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "map", "powermap.map");
            var pm = PowerMapSerializer.Open(mapFile);
            if (null != pm)
            {
                powerMap = pm;
                return true;
            }
            return false;
        }
    }
}
