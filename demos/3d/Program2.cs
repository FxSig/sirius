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
 * Load STL(StereoLithography) file and extract points cloud from model and convert 3D calibration 
 * STL 모델을 로드하고 모델에서 points cloud 추출하여 새로운 3D 보정 파일 생성하여 가공에 사용하기
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

    class Program2
    {

        static Stereolithography stlEntity = null;

        [STAThread]
        static void Main2()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // initialize sirius library
            SpiralLab.Core.Initialize();

            #region initialize RTC 
            // create Rtc for dummy (가상 RTC 카드)
            //var rtc = new RtcVirtual(0); 
            // create Rtc5 controller
            var rtc = new Rtc5(0);
            // create Rtc6 controller
            //var rtc = new Rtc6(0); 
            // create Rtc6 Ethernet controller
            //var rtc = new Rtc6Ethernet(0, "192.168.0.100", "255.255.255.0"); 

            // theoretically size of scanner field of view (이론적인 FOV 크기) : 60mm
            float fov = 60.0f;
            // RTC4: k factor (bits/mm) = 2^16 / fov
            //float kfactor = (float)Math.Pow(2, 16) / fov;
            // RTC5/6: k factor (bits/mm) = 2^20 / fov
            float kfactor = (float)Math.Pow(2, 20) / fov;

            // RTC4: full path of correction file
            //var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "D3_2982.ctb");
            // RTC5/6: full path of correction file
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "D3_2982.ct5"); 
            // initialize RTC controller
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);
            // laser frequency : 50KHz, pulse width : 2usec (주파수 50KHz, 펄스폭 2usec)
            rtc.CtlFrequency(50 * 1000, 2);
            // scanner jump and mark speed : 500mm/s (점프, 마크 속도 500mm/s)
            rtc.CtlSpeed(500, 500);
            // laser and scanner delays (레이저/스캐너 지연값 설정)
            rtc.CtlDelay(10, 100, 200, 200, 0);
            // RTC controller has 3D option 
            Debug.Assert(rtc.Is3D);
            #endregion

            var rtc3D = rtc as IRtc3D;
            Debug.Assert(null != rtc3D);


            #region initialize Laser (virtual)
            // virtual laser source with max 20W power (최대 출력 20W 의 가상 레이저 소스 생성)
            var laser = new LaserVirtual(0, "virtual", 20);
            laser.PowerControlMethod = PowerControlMethod.Unknown;
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

            // assign RTC controller at laser 
            laser.Rtc = rtc;
            // initialize laser source
            laser.Initialize();
            // set basic power output to 2W
            laser.CtlPower(2);
            #endregion


            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("Testcase for spirallab.sirius. powered by hcchoi@spirallab.co.kr (http://spirallab.co.kr)");
                Console.WriteLine($"----------------------------------------------------------------------------------------");
                Console.WriteLine("'R' : reset");
                Console.WriteLine("'A' : abort");
                Console.WriteLine("'O' : open stereo-lithography file");
                Console.WriteLine("'X' : reset/revert correction file");
                Console.WriteLine("'C' : convert correction file by stl's points clouds");
                Console.WriteLine("'D' : convert correction file by cylinder");
                Console.WriteLine("'E' : convert correction file by cone");
                Console.WriteLine("'F' : convert correction file by plane");
                Console.WriteLine("'F1' : mark square");
                Console.WriteLine("'F2' : mark circle");
                Console.WriteLine("'Q' : quit");
                Console.Write("Select your target : ");
                key = Console.ReadKey(false);
                Console.WriteLine($"{Environment.NewLine}");
                if (key.Key == ConsoleKey.Q)
                    break;

                bool success = true;
                switch (key.Key)
                {
                    case ConsoleKey.R:
                        rtc.CtlReset();
                        break;
                    case ConsoleKey.A:
                        rtc.CtlAbort();
                        break;
                    case ConsoleKey.O:
                        var dlg = new OpenFileDialog();
                        dlg.Filter = "stl model files (*.stl)|*.stl|All Files (*.*)|*.*";
                        dlg.Title = "Open STL Model File";
                        dlg.InitialDirectory = Config.ConfigLogoFilePath;
                        dlg.FileName = "Nefertiti_face.stl";
                        DialogResult result = dlg.ShowDialog();
                        if (result != DialogResult.OK)
                            return;
                        STLHelper stlHelper = new STLHelper(dlg.FileName);
                        stlEntity = new Stereolithography();
                        stlEntity.Faces = stlHelper.ReadFile();
                        if (stlHelper.IsError)
                            break;
                        stlEntity.Name = Path.GetFileName(dlg.FileName);
                        // adjust z positive length
                        //stlEntity.ZPositive = 0;

                        stlEntity.Regen();
                        //STL model has translated at below Z <= 0 by automatically
                        //
                        //                        Z +
                        //
                        //
                        //  -------------------- Z = 0 ---------------------
                        //                [ Loaded STL Model ]
                        //
                        //                        Z -
                        //
                        break;
                    case ConsoleKey.X:
                        switch (rtc.RtcType)
                        {
                            default:
                            case RtcType.Rtc4:
                                success &= rtc.CtlSelectCorrection(CorrectionTableIndex.Table1, CorrectionTableIndex.Table1);
                                break;
                            case RtcType.Rtc5:
                                success &= rtc.CtlSelectCorrection(CorrectionTableIndex.Table1, CorrectionTableIndex.Table1);
                                break;
                            case RtcType.Rtc6:
                                success &= rtc.CtlSelectCorrection(CorrectionTableIndex.Table1, CorrectionTableIndex.Table1);
                                break;
                        }
                        Logger.Log(Logger.Type.Info, $"3D calibration has reset as original correction table");
                        break;
                    case ConsoleKey.C:
                        if (null == stlEntity)
                            break;
                        {
                            var inputCtFileName = rtc.CorrectionFiles[(int)rtc.PrimaryHeadTable].FileName;
                            var newCtFileName = string.Empty;
                            // Extract points cloud data after transform translate, rotate and scale matrix
                            // STL 모델에 적용된 변환 행렬 (이동, 회전, 크기 변환) 연산 적용된후 Points cloud 데이타 추출됨
                            if (!stlEntity.GetPointsCloud(out Vector3[] xyz))
                            {
                                Logger.Log(Logger.Type.Error, $"fail to get points cloud from stl model");
                            }
                            else
                            {
                                Logger.Log(Logger.Type.Info, $"extracted point counts: {xyz.Length}");
                        
                                string ext = Path.GetExtension(inputCtFileName);
                                switch (ext.ToLower())
                                {
                                    case ".ctb":
                                        newCtFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction",
                                            Path.GetFileNameWithoutExtension(inputCtFileName)) + "_points_cloud.ctb";
                                        break;
                                    case ".ct5":
                                    default:
                                        newCtFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction",
                                            Path.GetFileNameWithoutExtension(inputCtFileName)) + "_points_cloud.ct5";
                                        break;
                                }
                                if (File.Exists(newCtFileName))
                                    File.Delete(newCtFileName);

                                if (Correction3DRtc.PointCloudCalibration(xyz, inputCtFileName, string.Empty, newCtFileName, out var returnCode))
                                {
                                    LoadAndSelectCorrection(rtc, newCtFileName);
                                }
                            }
                        }
                        break;
                    case ConsoleKey.D:
                        {
                            var inputCtFileName = rtc.CorrectionFiles[(int)rtc.PrimaryHeadTable].FileName;
                            var newCtFileName = string.Empty;
                            string ext = Path.GetExtension(inputCtFileName);
                            switch (ext.ToLower())
                            {
                                case ".ctb":
                                    newCtFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction",
                                        Path.GetFileNameWithoutExtension(inputCtFileName)) + "_cylinder.ctb";
                                    break;
                                case ".ct5":
                                default:
                                    newCtFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction",
                                        Path.GetFileNameWithoutExtension(inputCtFileName)) + "_cylinder.ct5";
                                    break;
                            }
                            if (File.Exists(newCtFileName))
                                File.Delete(newCtFileName);
                            if (Correction3DRtc.CylinderCalibration(Vector3.Zero, Vector3.UnitX, 10, inputCtFileName, null, newCtFileName, out var returnCode))
                            {
                                LoadAndSelectCorrection(rtc, newCtFileName);
                            }
                        }
                        break;
                    case ConsoleKey.E:
                        {
                            var inputCtFileName = rtc.CorrectionFiles[(int)rtc.PrimaryHeadTable].FileName;
                            var newCtFileName = string.Empty;
                            string ext = Path.GetExtension(inputCtFileName);
                            switch (ext.ToLower())
                            {
                                case ".ctb":
                                    newCtFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction",
                                        Path.GetFileNameWithoutExtension(inputCtFileName)) + "_cone.ctb";
                                    break;
                                case ".ct5":
                                default:
                                    newCtFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction",
                                        Path.GetFileNameWithoutExtension(inputCtFileName)) + "_cone.ct5";
                                    break;
                            }
                            if (File.Exists(newCtFileName))
                                File.Delete(newCtFileName);
                            if (Correction3DRtc.ConeCalibration(Vector3.Zero, Vector3.UnitX, 20, 45, inputCtFileName, null, newCtFileName, out var returnCode))
                            {
                                LoadAndSelectCorrection(rtc, newCtFileName);
                            }
                        }
                        break;
                    case ConsoleKey.F:
                        {
                            var inputCtFileName = rtc.CorrectionFiles[(int)rtc.PrimaryHeadTable].FileName;
                            var newCtFileName = string.Empty;
                            string ext = Path.GetExtension(inputCtFileName);
                            switch (ext.ToLower())
                            {
                                case ".ctb":
                                    newCtFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction",
                                        Path.GetFileNameWithoutExtension(inputCtFileName)) + "_plane.ctb";
                                    break;
                                case ".ct5":
                                default:
                                    newCtFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction",
                                        Path.GetFileNameWithoutExtension(inputCtFileName)) + "_plane.ct5";
                                    break;
                            }
                            if (File.Exists(newCtFileName))
                                File.Delete(newCtFileName);
                            //10 deg at x axis
                            var radian = 10 * MathHelper.DegToRad;
                            var dirX = Vector3.Transform(Vector3.UnitX, Matrix4x4.CreateRotationX(radian));
                            var dirY = Vector3.Transform(Vector3.UnitY, Matrix4x4.CreateRotationX(radian));
                            if (Correction3DRtc.PlaneCalibration(Vector3.Zero, dirX, dirY, inputCtFileName, null, newCtFileName, out var returnCode))
                            {
                                LoadAndSelectCorrection(rtc, newCtFileName);
                            }
                        }
                        break;
                    case ConsoleKey.F1:                       
                        float halfSquareSize = 10;
                        success &= rtc.ListBegin(laser);
                        success &= rtc.ListJump(-halfSquareSize, halfSquareSize);
                        success &= rtc.ListMark(halfSquareSize, halfSquareSize);
                        success &= rtc.ListMark(halfSquareSize, -halfSquareSize);
                        success &= rtc.ListMark(-halfSquareSize, -halfSquareSize);
                        success &= rtc.ListMark(-halfSquareSize, halfSquareSize);
                        success &= rtc.ListJump(0, 0, 0);
                        success &= rtc.ListEnd();
                        if (success)
                            success &= rtc.ListExecute(false); //async
                        break;
                    case ConsoleKey.F2:
                        float radius = 10;
                        success &= rtc.ListBegin(laser);
                        success &= rtc.ListJump(radius, 0);
                        success &= rtc.ListArc(0, 0, 360);
                        success &= rtc.ListJump(0, 0, 0);
                        success &= rtc.ListEnd();
                        if (success)
                            success &= rtc.ListExecute(false); //async
                        break;
                }
            } while (true);

            Console.WriteLine("Terminating ...");
            if (rtc.CtlGetStatus(RtcStatus.Busy))
            {
                // abort marking operation
                rtc.CtlAbort();
                // wait until busy has finished
                rtc.CtlBusyWait();
            }
            rtc.Dispose();
            laser.Dispose();
        }

        private static bool LoadAndSelectCorrection(IRtc rtc, string newCtFileName)
        {
            
            bool success = true;
            CorrectionTableIndex targetTable = CorrectionTableIndex.None;
            switch (rtc.RtcType)
            {
                default:
                case RtcType.Rtc4:
                    targetTable = CorrectionTableIndex.Table2;
                    success &= rtc.CtlLoadCorrectionFile(targetTable, newCtFileName);
                    success &= rtc.CtlSelectCorrection(targetTable, targetTable);
                    break;
                case RtcType.Rtc5:
                    targetTable = CorrectionTableIndex.Table4;
                    success &= rtc.CtlLoadCorrectionFile(targetTable, newCtFileName);
                    success &= rtc.CtlSelectCorrection(targetTable, targetTable);
                    break;
                case RtcType.Rtc6:
                    targetTable = CorrectionTableIndex.Table8;
                    success &= rtc.CtlLoadCorrectionFile(targetTable, newCtFileName);
                    success &= rtc.CtlSelectCorrection(targetTable, targetTable);
                    break;
            }
            //Table1에는 초기 보정 파일이 지속됨
            if (success)
                Logger.Log(Logger.Type.Info, $"new 3D calibration has applied: {newCtFileName}");
            else
                Logger.Log(Logger.Type.Error, $"fail to load/select 3D calibration: {newCtFileName}");
            return success;
        }

    }
}
