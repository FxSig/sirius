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

            bool success = true;
            success &= SpiralLab.Core.Initialize();

            #region initialize RTC 
            // SCANLAB XL-SCAN by syncAXIS library
            var rtc = new Rtc6SyncAxis(); 
            // initialized by xml config file
            string configXmlFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "syncaxis", "syncAXISConfig.xml");
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
                Console.WriteLine("'S' : enable simulation mode");
                Console.WriteLine("'H' : enable hardware mode");
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
                        rtc.CtlSetStagePosition(0, 0);
                        break;
                }
            } while (true);

            rtc.CtlAbort();
            laser.Dispose();
            rtc.Dispose();
        }

        /// <summary>
        /// 사각형 가공
        /// </summary>
        /// <param name="rtc"></param>
        /// <param name="laser"></param>
        /// <param name="motionType"></param>
        /// <param name="size"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 원형 가공
        /// </summary>
        /// <param name="rtc"></param>
        /// <param name="laser"></param>
        /// <param name="motionType"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
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
        /// 레이저 지연시간 최적화 (CHECK_LASERDELAYS)
        /// </summary>
        /// <param name="rtc"></param>
        /// <param name="laser"></param>
        /// <param name="vScanner">200mm/s</param>
        /// <param name="numberOfGridPositions">11x11</param>
        /// <param name="gridFactor">2.5mm interval</param>
        /// <returns></returns>
        static bool DrawOptimizeLaserDelay(IRtc rtc, ILaser laser, float vScanner=200, int numberOfGridPositions = 11, float gridFactor = 2.5f)
        {
            bool success = true;
            var rtcSyncAxis = rtc as IRtcSyncAxis;
            Debug.Assert(rtcSyncAxis != null);

            const float size = 1;
            const float gap = 0.1F;
            double startSwitchOffset = -40; //us
            double incrementSwitchOffset = 5; //us
            double startPreTrigger = -10; //us
            double incrementPreTrigger = 2; //us

            var oldMode = rtcSyncAxis.MotionMode;
            var oldTrajectory = rtcSyncAxis.Trajectory;
            var newTrajectory = rtcSyncAxis.Trajectory;
            newTrajectory.Mark.JumpSpeed = vScanner;
            newTrajectory.Mark.MarkSpeed = vScanner;

            //left bottom
            Vector2 offsetInitial = new Vector2(
                -(numberOfGridPositions - 1) / 2 * gridFactor * size, 
                -(numberOfGridPositions - 1) / 2 * gridFactor * size);
            Vector2 offset = offsetInitial;
            int gridCounter = 0;

            for (int x = 0; x < numberOfGridPositions; ++x)
            {
                newTrajectory.Mark.LaserSwitchOffsetTime = (x * incrementSwitchOffset + startSwitchOffset);
                offset = new Vector2( gridFactor * size * x + offsetInitial.X, offsetInitial.Y);

                for (int y = 0; y < numberOfGridPositions; ++y)
                {
                    newTrajectory.Mark.LaserPreTriggerTime = (y * incrementPreTrigger + startPreTrigger);
                    success &= rtcSyncAxis.CtlSetTrajectory(newTrajectory);

                    success &= rtcSyncAxis.ListBegin(laser, MotionType.ScannerOnly);
                    offset = new Vector2(offset.X, gridFactor * size * y + offsetInitial.Y);
                    /*
                     *  +
                     *  
                     *  L       ---     ---     ---      ---  
                     *  a        |       |       |        |   
                     *  s        |       |       |        |   
                     *  e       ---     ---     ---      ---  
                     *  r       ---     ---     ---      ---  
                     *  P        |       |       |        |   
                     *  r        |       |       |        |   
                     *  e       ---     ---     ---      ---  
                     *  T       ---     ---     ---      ---  
                     *  r        |       |       |        |   
                     *  i        |       |       |        |   
                     *  g       ---     ---     ---      ---  
                     *  g       ---     ---     ---      ---  
                     *  e        |       |       |        |   
                     *  r        |       |       |        |   
                     *  T       ---     ---     ---      ---  
                     *  i                              
                     *  m      
                     *  e      -  Laser Switch Offset Time  +
                     *  
                     *  -
                     */
                    rtc.MatrixStack.Push(offset);
                    success &= rtc.ListJump(-size / 2, -size);
                    success &= rtc.ListMark(-gap / 2, -size);
                    success &= rtc.ListJump(gap/ 2, -size);
                    success &= rtc.ListMark(size / 2, -size);
                    success &= rtc.ListJump(0, -size);
                    success &= rtc.ListMark(0, size);
                    success &= rtc.ListJump(size / 2, size);
                    success &= rtc.ListMark(gap / 2, size);
                    success &= rtc.ListJump(-gap / 2, size);
                    success &= rtc.ListMark(-size / 2, size);
                    success &= rtc.ListJump(-size / 2 - 0.001f, size);
                    rtc.MatrixStack.Pop();
                    if (!success)
                        break;
                    else
                    {
                        gridCounter++;
                        success &= rtc.ListJump(Vector2.Zero);
                        success &= rtc.ListEnd();
                        success &= rtc.ListExecute(true);
                    }
                }
                if (!success)
                    break;
            }
            rtcSyncAxis.CtlSetTrajectory(oldTrajectory);
            rtcSyncAxis.MotionMode = oldMode;
            return success;
        }
        /// <summary>
        /// 시스템 지연시간 최적화 (CHECK_SYSTEMDELAYS)
        /// </summary>
        /// <param name="rtc"></param>
        /// <param name="laser"></param>
        /// <param name="vStage"></param>
        /// <param name="rStage"></param>
        /// <returns></returns>
        static bool DrawOptimizeSystemDelay(IRtc rtc, ILaser laser, float vStage=100, float rStage=200)
        {
            bool success = true;
            var rtcSyncAxis = rtc as IRtcSyncAxis;
            Debug.Assert(rtcSyncAxis != null);

            float v_aLimit = (float)Math.Sqrt(rStage / 2.0 * 0.42 * vStage * 10);
            vStage = (vStage < v_aLimit ? vStage : v_aLimit);
            float jumpSpeed = 4 * vStage;
            float markSpeed = jumpSpeed;
            float lineLength = 3;

            var oldMode = rtcSyncAxis.MotionMode;
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

            const uint totalNumberOfLines = 20;
            const float increment = 1;
            for (int i = 0; i < 4; ++i)
            {
                success &= rtcSyncAxis.ListBegin(laser, MotionType.StageAndScanner);

                rtc.MatrixStack.Push(0, -offsetsize, i * 90);
                //line block
                success &= rtc.ListSpeed(vStage, markSpeed);
                success &= rtc.ListJump(-rStage, 0);
                success &= rtc.ListWait((float)(1.0e3 * newTrajectory.Mark.LaserMinOffTime + (newTrajectory.Mark.LaserPreTriggerTime > 0 ? newTrajectory.Mark.LaserPreTriggerTime : 0)));
                success &= rtc.ListJump(-rStage, lineLength);
                success &= rtc.ListWait((float)(1.0e3 * newTrajectory.Mark.LaserMinOffTime + (newTrajectory.Mark.LaserPreTriggerTime > 0 ? newTrajectory.Mark.LaserPreTriggerTime : 0)));
                success &= rtc.ListJump(-(totalNumberOfLines / 2.0f) * increment, lineLength);
                success &= rtc.ListSpeed(jumpSpeed, markSpeed);
                for (int lineNumber = 0; lineNumber <= totalNumberOfLines; lineNumber += 2)
                {
                    success &= rtc.ListJump((lineNumber - totalNumberOfLines / 2.0f) * increment, lineLength);
                    success &= rtc.ListMark((lineNumber - totalNumberOfLines / 2.0f) * increment, 0.1f);
                    if (lineNumber + 1 <= totalNumberOfLines)
                    {
                        success &= rtc.ListJump( (lineNumber + 1 - totalNumberOfLines / 2.0f) * increment, 0.1f);
                        success &= rtc.ListMark( (lineNumber + 1 - totalNumberOfLines / 2.0f) * increment, lineLength);
                    }
                }
                success &= rtc.ListSpeed(vStage, markSpeed);
                success &= rtc.ListJump(rStage, lineLength);
                success &= rtc.ListWait((float)(1.0e3 * newTrajectory.Mark.LaserMinOffTime + (newTrajectory.Mark.LaserPreTriggerTime > 0 ? newTrajectory.Mark.LaserPreTriggerTime : 0)));
                success &= rtc.ListJump(rStage, -lineLength);
                success &= rtc.ListWait((float)(1.0e3 * newTrajectory.Mark.LaserMinOffTime + (newTrajectory.Mark.LaserPreTriggerTime > 0 ? newTrajectory.Mark.LaserPreTriggerTime : 0)));
                success &= rtc.ListJump((totalNumberOfLines / 2.0f) * increment, -lineLength);
                success &= rtc.ListSpeed(jumpSpeed, markSpeed);
                for (int lineNumber = 0; lineNumber <= totalNumberOfLines; lineNumber -= 2)
                {
                    success &= rtc.ListJump((lineNumber - totalNumberOfLines / 2.0f) * increment, -lineLength);
                    success &= rtc.ListMark((lineNumber - totalNumberOfLines / 2.0f) * increment, -0.1f);
                    if (lineNumber - 1 >= 0)
                    {
                        success &= rtc.ListJump((lineNumber - 1 - totalNumberOfLines / 2.0f) * increment, -0.1f);
                        success &= rtc.ListMark((lineNumber - 1 - totalNumberOfLines / 2.0f) * increment, -lineLength);
                    }
                }
                success &= rtc.ListSpeed(vStage, markSpeed);
                success &= rtc.ListJump(-rStage, -lineLength);
                success &= rtc.ListWait((float)(1.0e3 * newTrajectory.Mark.LaserMinOffTime + (newTrajectory.Mark.LaserPreTriggerTime > 0 ? newTrajectory.Mark.LaserPreTriggerTime : 0)));
                success &= rtc.ListJump(0, 0);

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
            rtcSyncAxis.MotionMode = oldMode;
            return success;
        }
        /// <summary>
        /// 시뮬레이션 로그 결과에 대한 뷰어 실행
        /// </summary>
        /// <param name="rtcSyncAxis"></param>
        static void SyncAxisViewer(IRtcSyncAxis rtcSyncAxis)
        {
            var exeFileName = Config.ConfigSyncAxisViewerFileName;
            string simulatedFileName = Path.Combine(Config.ConfigSyncAxisSimulateFilePath, rtcSyncAxis.SimulationFileName);
            if (File.Exists(simulatedFileName))
            {
                Console.WriteLine($"syncAXIS Viewer trying to open: {simulatedFileName}");
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
        /// <summary>
        /// 마지막 가공된 작업(JOB)의 특성 출력
        /// </summary>
        /// <param name="rtc"></param>
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
                Console.WriteLine($"Stage Position Max: {lastJob.Characteristic.Stage.StagePosMax} mm");
                Console.WriteLine($"Stage Velocity Max: {lastJob.Characteristic.Stage.StageVelMax} mm/s");
                Console.WriteLine($"Stage Accelation Max: {lastJob.Characteristic.Stage.StageAccMax} (mm/s²)");
                Console.WriteLine($"Stage Jerk Max: {lastJob.Characteristic.Stage.StageJerkMax} (mm/s³)");
                Console.WriteLine("");
            }
        }
    }
}
