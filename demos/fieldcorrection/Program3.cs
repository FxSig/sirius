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
 * Scanner Field Correction / 스캐너 필드 보정
 * 스캐너 보정 파일 (ct5) 의 헤더 정보를 조회하는 예제
 * 
 * Author : hong chan, choi / hcchoi@spirallab.co.kr (http://spirallab.co.kr)
 * 
 */


using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    class Program3
    {
        [STAThread]
        static void Main3(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SpiralLab.Core.Initialize();

            #region initialize RTC 
            //var rtc = new RtcVirtual(0); //create Rtc for dummy (가상 RTC 카드)
            var rtc = new Rtc5(0); //create Rtc5 controller
            //var rtc = new Rtc6(0); //create Rtc6 controller
            //var rtc = new Rtc6Ethernet(0, "192.168.0.100", "255.255.255.0"); //Rtc6 Ethernet
            //var rtc = new Rtc6SyncAxis(0, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "syncAxis", "syncAXISConfig.xml")); //Scanlab XLSCAN 

            float fov = 60.0f;    // scanner field of view : 60mm            
            float kfactor = (float)Math.Pow(2, 20) / fov; // k factor (bits/mm) = 2^20 / fov
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);    // correction file (스캐너 보정 파일)
            #endregion

            // 헤더 정보를 조회할 보정 파일 RTC 내부 메모리 (테이블)로 로드
            var targetCorrectionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "D2_1128.ct5");
            rtc.CtlLoadCorrectionFile(CorrectionTableIndex.Table3, targetCorrectionFile);
            var rtcExt = rtc as IRtcExtension;
            Debug.Assert(rtcExt != null);

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Testcase for spirallab.sirius. powered by hcchoi@spirallab.co.kr (http://spirallab.co.kr)");
                Console.WriteLine("");
                Console.WriteLine("'H' : correction file header information");
                Console.WriteLine("'Q' : quit");
                Console.WriteLine("");
                Console.Write("select your target : ");
                key = Console.ReadKey(false);
                if (key.Key == ConsoleKey.Q)
                    break;
                Console.WriteLine("");
                switch (key.Key)
                {
                    case ConsoleKey.H:
                        rtcExt.CtlLoadedCorrectionInfo(CorrectionTableIndex.Table3, CorrectionFileHeaderParam.CorrectionTable, out double dimension);
                        if (0 == dimension)
                            Console.WriteLine($"Dimension : 2D");
                        else
                            Console.WriteLine($"Dimension : 3D");
                        rtcExt.CtlLoadedCorrectionInfo(CorrectionTableIndex.Table3, CorrectionFileHeaderParam.KFactor, out double internalKFactor);
                        Console.WriteLine($"K-Factor : {internalKFactor}");

                        rtcExt.CtlLoadedCorrectionInfo(CorrectionTableIndex.Table3, CorrectionFileHeaderParam.FocalLengthOrWorkingDistance, out double len);
                        Console.WriteLine($"F-Length/Working Distance : {len}");

                        rtcExt.CtlLoadedCorrectionInfo(CorrectionTableIndex.Table3, CorrectionFileHeaderParam.StretchFactorX, out double stretchX);
                        rtcExt.CtlLoadedCorrectionInfo(CorrectionTableIndex.Table3, CorrectionFileHeaderParam.StretchFactorY, out double stretchY);
                        Console.WriteLine($"3D : Stretch Factor : X= {stretchX}, Y= {stretchY}");

                        rtcExt.CtlLoadedCorrectionInfo(CorrectionTableIndex.Table3, CorrectionFileHeaderParam.CoefficientA, out double coefA);
                        rtcExt.CtlLoadedCorrectionInfo(CorrectionTableIndex.Table3, CorrectionFileHeaderParam.CoefficientB, out double coefB);
                        rtcExt.CtlLoadedCorrectionInfo(CorrectionTableIndex.Table3, CorrectionFileHeaderParam.CoefficientC, out double coefC);
                        Console.WriteLine($"3D : Parabolic Equation : Zout = {coefA} + l * {coefB} + l^2 * {coefC}");

                        rtcExt.CtlLoadedCorrectionInfo(CorrectionTableIndex.Table3, CorrectionFileHeaderParam.AngleCalibration, out double angleCalibration);
                        Console.WriteLine($"Angle Calibration : {angleCalibration}");

                        rtcExt.CtlLoadedCorrectionInfo(CorrectionTableIndex.Table3, CorrectionFileHeaderParam.ImageFieldSizeLimited, out double fieldSizeLimited);
                        if (0 == fieldSizeLimited)
                            Console.WriteLine($"Image Field Size : Not clipped");
                        else
                            Console.WriteLine($"Image Field Size : Clipped");

                        Console.WriteLine(Environment.NewLine);
                        break;
                }
            } while (true);
        }
    }
}
