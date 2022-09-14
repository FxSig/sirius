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
 * SyncAxis 를 이용한 MOTF
 * SyncAxis (aka. XL-SCAN) : RTC6 + ExcelliSCAN + ACS Controller 조합의 고정밀 가공기법
 *
 * 1. please copy dll files into working directory (absolute path of  ~\bin\)
 * 
 * copy C:\Program Files (x86)\ACS Motion Control\SPiiPlus Runtime Kit\Redist\x64 to ~\bin\
 * copy syncAxis-1.6.0\RTC6\ProgramFiles to  ~\bin\
 * copy syncAxis-1.6.0\syncAXIS_control\bin64\dll to ~\bin\
 * copy syncAxis-1.6.0\syncAXIS_control\bin64\Wrapper\C# to ~\bin\
 * 
 * 2. xml configuration file
 *  general configuration
 *   - base directory path : absolute path of ~\bin\
 *   - log file path : [BaseDirectoryPath]/Logs/syncAxisLog.txt
 *   - sim output file directory : [BaseDirectoryPath]/Logs/
 *  RTC configuration
 *   - program file path : [BaseDirectoryPath]
 * 
 * Author : hong chan, choi / hcchoi@spirallab.co.kr (http://spirallab.co.kr)
 * 
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SpiralLab.Core.Initialize();

            #region initialize RTC 
            string configXmlFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "syncaxis", "syncAXISConfig.xml");
            var rtc = new Rtc6SyncAxis(); // Scanlab XLSCAN 솔류션
            bool success = true;
            success &= rtc.Initialize(configXmlFileName);
            // basic frequency and pulse width
            // laser frequency : 50KHz, pulse width : 2usec (주파수 50KHz, 펄스폭 2usec)
            success &= rtc.CtlFrequency(50 * 1000, 2);
            // jump and mark speed : 50mm/s (점프, 마크 속도 50mm/s)
            success &= rtc.CtlSpeed(50, 50);
            Debug.Assert(success);
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
            success &= laser.Initialize();
            // set basic power output to 2W
            success &= laser.CtlPower(2);
            Debug.Assert(success);
            #endregion

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Testcase for spirallab.sirius. powered by hcchoi@spirallab.co.kr (http://spirallab.co.kr)");
                Console.WriteLine("");
                Console.WriteLine("'S' : simulation mode enabled");
                Console.WriteLine("'H' : hardware mode enabled");
                Console.WriteLine("'V' : syncaxis viewer with simulation result");
                Console.WriteLine("'F1' : draw square 2D with scanner only");
                Console.WriteLine("'F2' : draw square 2D with stage only");
                Console.WriteLine("'F3' : draw square 2D with scanner and stage");
                Console.WriteLine("'F4' : draw circle 2D with scanner only");
                Console.WriteLine("'F5' : draw circle 2D with stage only");
                Console.WriteLine("'F6' : draw circle 2D with scanner and stage");
                Console.WriteLine("'F7' : draw for optimize laser delays");
                Console.WriteLine("'F8' : draw for optimize system delays");
                Console.WriteLine("'F10' : get status");
                Console.WriteLine("'F11' : reset");
                Console.WriteLine("'F12' : abort");
                Console.WriteLine("'C' : job characteristic");
                Console.WriteLine("'M' : move stage x and y");
                Console.WriteLine("'Q' : quit");
                Console.WriteLine("");
                Console.Write("select your target : ");
                key = Console.ReadKey(false);
                if (key.Key == ConsoleKey.Q)
                    break;
                Console.WriteLine("");
                switch (key.Key)
                {
                    case ConsoleKey.S:
                        rtc.IsSimulationMode = true;
                        break;
                    case ConsoleKey.H:
                        rtc.IsSimulationMode = false;
                        break;
                    case ConsoleKey.V:
                        SyncAxisViewer(rtc);
                        break;
                    case ConsoleKey.F1:
                        DrawSquare(rtc, laser, MotionType.ScannerOnly);
                        break;
                    case ConsoleKey.F2:
                        DrawSquare(rtc, laser, MotionType.StageOnly);
                        break;
                    case ConsoleKey.F3:
                        //band width 변경 가능
                        //rtc.BandWidth = 2.0f;
                        //멀티헤드 사용시 개별 헤드별 오프셋 처리 가능
                        //rtc.Head1Offset = new Vector3(0.1f, 0.2f, 5);
                        //rtc.Head2Offset = new Vector3(-0.1f, -0.2f, -5);
                        DrawSquare(rtc, laser, MotionType.StageAndScanner);
                        break;
                    case ConsoleKey.F4:
                        DrawCircle(rtc, laser, MotionType.ScannerOnly);
                        break;
                    case ConsoleKey.F5:
                        DrawCircle(rtc, laser, MotionType.StageOnly);
                        break;
                    case ConsoleKey.F6:
                        //band width 변경 가능
                        //rtc.BandWidth = 2.0f;
                        //멀티헤드 사용시 개별 헤드별 오프셋 처리 가능
                        //rtc.Head1Offset = new Vector3(0.1f, 0.2f, 5);
                        //rtc.Head2Offset = new Vector3(-0.1f, -0.2f, -5);
                        DrawCircle(rtc, laser, MotionType.StageAndScanner);
                        break;
                    case ConsoleKey.F7:
                        DrawOptimizeLaserDelay(rtc, laser);
                        break;
                    case ConsoleKey.F8:
                        DrawOptimizeSystemDelay(rtc, laser);
                        break;
                    case ConsoleKey.F10:
                        if (rtc.CtlGetStatus(RtcStatus.Busy))
                            Console.WriteLine("rtc is busy now ...");
                        else
                            Console.WriteLine("rtc is not busy ...");

                        rtc.CtlGetInternalErrMsg(out var errors);
                        foreach( var kv in errors)
                            Console.WriteLine($"syncaxis error: [{kv.Item1}]: {kv.Item2}");
                        break;
                    case ConsoleKey.F11:
                        rtc.CtlReset();
                        break;
                    case ConsoleKey.F12:
                        rtc.CtlAbort();
                        break;
                    case ConsoleKey.C:
                        PrintJobCharacteristic(rtc);
                        break;
                    case ConsoleKey.M:
                        //스테이지및 스캐너 보정 테이블 선택
                        rtc.CtlSelectStage(Stage.Stage1, CorrectionTableIndex.Table1);
                        rtc.StageMoveSpeed = 10;
                        rtc.StageMoveTimeOut = 5;
                        rtc.CtlSetStagePosition(10, 10);
                        break;
                }
            } while (true);

            rtc.CtlAbort();
            laser.Dispose();
            rtc.Dispose();
        }

        static bool DrawSquare(IRtc rtc, ILaser laser, MotionType motionType, float size=40)
        {
            bool success = true;
            var rtcSyncAxis = rtc as IRtcSyncAxis;
            Debug.Assert( rtcSyncAxis != null );

            success &= rtcSyncAxis.ListBegin(laser, motionType);
            //success &= rtc.ListFrequency(50 * 1000, 2);
            //success &= rtc.ListSpeed(100, 100);
            success &= rtc.ListJump(new Vector2(-size/2.0f, size / 2.0f));
            success &= rtc.ListMark(new Vector2(size / 2.0f, size / 2.0f));
            success &= rtc.ListMark(new Vector2(size / 2.0f, -size / 2.0f));
            success &= rtc.ListMark(new Vector2(-size / 2.0f, -size / 2.0f));
            success &= rtc.ListMark(new Vector2(-size / 2.0f, size / 2.0f));
            if (success)
            {
                success &= rtc.ListJump(Vector2.Zero);
                success &= rtc.ListEnd();
                success &= rtc.ListExecute(false);
            }
            return success;
        }

        static bool DrawCircle(IRtc rtc, ILaser laser, MotionType motionType, float radius = 20)
        {
            bool success = true;
            var rtcSyncAxis = rtc as IRtcSyncAxis;
            Debug.Assert(rtcSyncAxis != null);

            success &= rtcSyncAxis.ListBegin(laser, motionType);
            //success &= rtc.ListFrequency(50 * 1000, 2);
            //success &= rtc.ListSpeed(100, 100);
            success &= rtc.ListJump(new Vector2(radius, 0));
            success &= rtc.ListArc(Vector2.Zero, 360.0f);
            success &= rtc.ListJump(Vector2.Zero);
            if (success)
            {
                success &= rtc.ListJump(Vector2.Zero);
                success &= rtc.ListEnd();
                success &= rtc.ListExecute(false);
            }
            return success;
        }

        /// <summary>
        /// Optimization – Finding Laser Delays that Suit Your Application
        ///  • LaserSwitchOffsetTime – Time shift for the laser signals output)(1)
        ///  • LaserPreTriggerTime – Time to trigger the laser signal in advance, if a mark segment is executed(1)
        /// For this, CHECK_LASERDELAYS is executed.
        /// CHECK_LASERDELAYS contains an 11 × 11 raster of small marking objects, see Figure 7, page 33. 
        /// The laser delays are automatically changed between the marking objects:
        ///  • LaserSwitchOffsetTime is increased in positive x direction
        ///  • LaserPreTriggerTime is increased in positive y direction
        /// Recommended values to start with (in step 4, page 34) are:
        ///  • LaserSwitchOffsetTime: –40 μs ~ +10 μs
        ///  • LaserPreTriggerTime: –10 μs ~ 10 μs
        /// </summary>
        /// <param name="rtc"></param>
        /// <param name="laser"></param>
        /// <param name="VScanner">200mm/s</param>
        /// <param name="NumberOfGridPositions">11x11</param>
        /// <param name="GridFactor">2.5mm interval</param>
        /// <returns></returns>
        static bool DrawOptimizeLaserDelay(IRtc rtc, ILaser laser, float VScanner = 200.0f, int NumberOfGridPositions = 11, float GridFactor = 2.5f)
        {
            bool success = true;
            var rtcSyncAxis = rtc as IRtcSyncAxis;
            Debug.Assert(rtcSyncAxis != null);

            const float Size = 1;
            const float Gap = 0.1F;
            double StartSwitchOffset = -40;
            double IncrementSwitchOffset = 5;
            double StartPreTrigger = -10;
            double IncrementPreTrigger = 2;

            var oldTrajectory = rtcSyncAxis.Trajectory;
            var newTrajectory = rtcSyncAxis.Trajectory;
            newTrajectory.Mark.JumpSpeed = VScanner;
            newTrajectory.Mark.MarkSpeed = VScanner;

            //left bottom
            Vector2 offsetInitial = new Vector2(
                -(NumberOfGridPositions - 1) / 2 * GridFactor * Size, 
                -(NumberOfGridPositions - 1) / 2 * GridFactor * Size);
            Vector2 offset = offsetInitial;

            for (int x = 0; x < NumberOfGridPositions; ++x)
            {
                newTrajectory.Mark.LaserSwitchOffsetTime = (x * IncrementSwitchOffset + StartSwitchOffset);
                offset = new Vector2( GridFactor * Size * x + offsetInitial.X, offsetInitial.Y);

                for (int y = 0; y < NumberOfGridPositions; ++y)
                {
                    newTrajectory.Mark.LaserPreTriggerTime = (y * IncrementPreTrigger + StartPreTrigger);
                    success &= rtcSyncAxis.CtlSetTrajectory(newTrajectory);

                    success &= rtcSyncAxis.ListBegin(laser, MotionType.ScannerOnly);
                    offset = new Vector2(offset.X, GridFactor * Size * y + offsetInitial.Y);
                    rtc.MatrixStack.Push(offset);
                    success &= rtc.ListJump(-Size / 2, -Size);
                    success &= rtc.ListMark(-Gap / 2, -Size);
                    success &= rtc.ListMark(Size / 2, -Size);
                    success &= rtc.ListJump(0, -Size);
                    success &= rtc.ListMark(0, Size);
                    success &= rtc.ListJump(Size / 2, Size);
                    success &= rtc.ListMark(Gap / 2, Size);
                    success &= rtc.ListJump(-Gap / 2, Size);
                    success &= rtc.ListMark(-Size / 2, Size);
                    success &= rtc.ListJump(-Size / 2 - 0.001f, Size);
                    rtc.MatrixStack.Pop();
                    if (!success)
                        break;
                    else
                    {
                        success &= rtc.ListJump(Vector2.Zero);
                        success &= rtc.ListEnd();
                        success &= rtc.ListExecute(true);
                    }
                }
            }
            rtcSyncAxis.CtlSetTrajectory(oldTrajectory);
            return success;
        }

        static bool DrawOptimizeSystemDelay(IRtc rtc, ILaser laser, float VStage=100, float RStage=200)
        {
            bool success = true;
            var rtcSyncAxis = rtc as IRtcSyncAxis;
            Debug.Assert(rtcSyncAxis != null);

            float v_aLimit = (float)Math.Sqrt(RStage / 2.0 * 0.42 * VStage * 10);
            VStage = (VStage < v_aLimit ? VStage : v_aLimit);
            float jumpSpeed = 4 * VStage;
            float markSpeed = jumpSpeed;
            float lineLength = 3;

            var oldTrajectory = rtcSyncAxis.Trajectory;
            var newTrajectory = rtcSyncAxis.Trajectory;
            newTrajectory.Mark.JumpSpeed = jumpSpeed;
            newTrajectory.Mark.MarkSpeed = markSpeed;

            success &= rtcSyncAxis.CtlSetTrajectory(newTrajectory);
            float offsetsize = 5 * lineLength;

            for (int i = 0; i < 4; ++i)
            {
                success &= rtcSyncAxis.ListBegin(laser, MotionType.StageAndScanner);
                rtc.MatrixStack.Push(0, -offsetsize, i*90);
                //arrow
                success &= rtc.ListJump(-5, 0 + 2 * lineLength);
                success &= rtc.ListMark(5, 0 + 2 * lineLength);
                success &= rtc.ListJump(3, 2 + 2 * lineLength);
                success &= rtc.ListMark(5, 0 + 2 * lineLength);
                success &= rtc.ListMark(3, -2 + 2 * lineLength);
                success &= rtc.ListJump(0, 0 + 2 * lineLength);
                rtc.MatrixStack.Pop();
                if (!success)
                    break;
                else
                {
                    success &= rtc.ListJump(Vector2.Zero);
                    success &= rtc.ListEnd();
                    success &= rtc.ListExecute(true);
                }
            }


            for (int i = 0; i < 4; ++i)
            {
                success &= rtcSyncAxis.ListBegin(laser, MotionType.StageAndScanner);
                rtc.MatrixStack.Push(0, -offsetsize, i * 90);
                //line block
                success &= rtc.ListSpeed(jumpSpeed, markSpeed);
                success &= rtc.ListJump(-RStage, 0);



                rtc.MatrixStack.Pop();
                if (!success)
                    break;
                else
                {
                    success &= rtc.ListJump(Vector2.Zero);
                    success &= rtc.ListEnd();
                    success &= rtc.ListExecute(true);
                }
            }

            rtcSyncAxis.CtlSetTrajectory(oldTrajectory);
            return success;
        }

        static void SyncAxisViewer(IRtcSyncAxis rtcSyncAxis)
        {
            var exeFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "syncaxis", "Tools", "syncAXIS_Viewer", "syncAXIS_Viewer.exe");
            string simulatedFileName = Path.Combine(exeFileName, rtcSyncAxis.SimulationFileName);
            if (File.Exists(simulatedFileName))
            {
                Task.Run(() =>
                {
                    // Notice
                    // syncAxisViewer v1.6 의 버그로 인해 아래와 같이 외부에서 파일을 인자로 하여 뷰어 프로세스를 생성하면 일부 데이타 누락이 발생되기도 함
                    // 이때는 재차 open 을 하면 해결되며, SCANLAB 에 버그 리포트 된 사항임
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.WorkingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "syncaxis", "Tools", "syncAXIS_Viewer");
                    startInfo.CreateNoWindow = false;
                    startInfo.UseShellExecute = false;
                    startInfo.FileName = Config.ConfigSyncAxisViewerFileName;
                    startInfo.WindowStyle = ProcessWindowStyle.Normal;
                    startInfo.Arguments = "-a";//string.Empty;
                    if (!string.IsNullOrEmpty(simulatedFileName))
                        startInfo.Arguments = Path.Combine(Config.ConfigSyncAxisSimulateFilePath, simulatedFileName);
                    try
                    {
                        using (var proc = Process.Start(startInfo))
                        {
                            proc.WaitForExit();
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(Logger.Type.Error, ex.Message);
                    }
                });
            }
        }

        static void PrintJobCharacteristic(Rtc6SyncAxis rtc)
        {            
            Console.WriteLine($"{rtc.Job.ToString()}");

            int counts = rtc.JobHistory.Length;
            if (counts > 0)
            {
                var lastJob = rtc.JobHistory[counts - 1];
                Console.WriteLine($"Scanner Utilization: {lastJob.UtilizedScanner}");
                Console.WriteLine($"Scanner Position Max: {lastJob.Characteristic.Scanner.ScanPosMax} mm");
                Console.WriteLine($"Scanner Velocity Max: {lastJob.Characteristic.Scanner.ScanVelMax} mm/s");
                Console.WriteLine($"Scanner Accelation Max: {lastJob.Characteristic.Scanner.ScanAccMax} (mm/s²)");
                Console.WriteLine("--------------------------------------------------------");

                for (int i=0; i< rtc.StageCounts; i++)
                    Console.WriteLine($"Stage{i+1} Utilization: {lastJob.UtilizedStages[i]}");
                Console.WriteLine($"Stage Position Nax: {lastJob.Characteristic.Stage.StagePosMax} mm");
                Console.WriteLine($"Stage Velocity Max: {lastJob.Characteristic.Stage.StageVelMax} mm/s");
                Console.WriteLine($"Stage Accelation Max: {lastJob.Characteristic.Stage.StageAccMax} (mm/s²)");
                Console.WriteLine($"Stage Jerk Max: {lastJob.Characteristic.Stage.StageJerkMax} (mm/s³)");
            }
        }
    }
}
