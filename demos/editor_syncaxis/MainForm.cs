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
 * 3. XLSCAN dongle key must be plugged into your computer 
 * 
 * Author : hong chan, choi / hcchoi@spirallab.co.kr (http://spirallab.co.kr)
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    public partial class MainForm : Form
    {
        IRtc Rtc;
        
        //syncaxis 는 확장1포트 디지털 입력 읽기를 지원하지 않는다
        //IDInput RtcExt1DInput;

        IDOutput RtcExt1DOutput;

        //syncaxis 는 확장2포트 8비트 디지털 쓰기를 지원하지 않는다
        //IDOutput RtcExt2DOutput;

        ILaser Laser;

        /// <summary>
        /// 레이저 최대 출력 (Watt)
        /// </summary>
        public float LaserMaxPowerWatt = 5.0f;
        /// <summary>
        /// 마커 인터페이스
        /// </summary>
        public IMarker Marker { get; set; }
        /// <summary>
        /// 모터 집합
        /// </summary>
        public IMotor[] Motors { get; set; }
        /// <summary>
        /// 파워메터 인터페이스
        /// </summary>
        public IPowerMeter PowerMeter { get; set; }
        /// <summary>
        /// 파워맵 인터페이스
        /// </summary>
        public IPowerMap PowerMap { get; set; }



        public MainForm()
        {
            InitializeComponent();

            // initialize core engine
            SpiralLab.Core.Initialize();
            // enable pens function
            siriusEditorForm1.EnablePens = true;

            // create document
            // 신규 문서 생성
            var doc = new DocumentDefault();
            // assign document into editor
            // 문서 지정
            siriusEditorForm1.Document = doc;

            // assign document source changed event handler
            // 내부 데이타(IDocument) 가 변경될경우 이를 이벤트 통지를 받는 핸들러 등록
            siriusEditorForm1.OnDocumentSourceChanged += SiriusEditorForm1_OnDocumentSourceChanged;

            this.Load += MainForm_Load;
            this.FormClosing += MainForm_FormClosing;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var xmlConfigFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "syncAxis", "syncAXISConfig.xml");
            //var xmlConfigFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "syncAxis", "syncAXISConfig2.xml");
            if (!File.Exists(xmlConfigFileName))
            {
                MessageBox.Show($"XML configuration file is not founded : {xmlConfigFileName}");
                return;
            }

            #region SyncAxis 초기화
            bool success = true;
            var rtc = new Rtc6SyncAxis();
            rtc.Name = "SyncAxis";
            success &= rtc.Initialize(xmlConfigFileName); // initialized by xml config file
            success &= rtc.CtlFrequency(50 * 1000, 2); // laser frequency : 50KHz, pulse width : 2usec
            success &= rtc.CtlSpeed(100, 100); // default scanner jump and mark speed : 100mm/s

            //스테이지 이동시 기본 값 설정
            rtc.StageMoveSpeed = 10;
            rtc.StageMoveTimeOut = 5;
            #endregion

            #region 레이저 소스 초기화
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
            // 10% output 
            laser.CtlPower(LaserMaxPowerWatt * 0.1f);
            #endregion

            #region 마커 지정
            var marker = new MarkerDefault(0, "SyncAxis Marker");
            marker.OnStarted += Marker_OnStarted;
            marker.OnProgress += Marker_OnProgress;
            marker.OnFinished += Marker_OnFinished;
            marker.OnFailed += Marker_OnFailed;
            #endregion

            #region RTC 확장 IO 
            //this.RtcExt1DInput = new RtcDInput(rtc, 0, "DIN RTC EXT1");
            //this.RtcExt1DInput.Initialize();
            this.RtcExt1DOutput = new RtcDOutputExt1(rtc, 0, "DOUT RTC EXT1");
            this.RtcExt1DOutput.Initialize();
            //this.RtcExt2DOutput = new RtcDOutputExt2(rtc, 0, "DIN RTC EXT2");
            //this.RtcExt2DOutput.Initialize();
            //this.siriusEditorForm1.RtcExtension1Input = this.RtcExt1DInput;
            //this.siriusEditorForm1.RtcExtension2Output = this.RtcExt2DOutput;
            #endregion

            #region PowerMeter
            // 파워메터
            var powerMeter = new PowerMeterVirtual(0, "Virtual", laser.MaxPowerWatt);
            //var powerMeter = new PowerMeterOphir(0, "OphirJuno", "3040875");
            //var powerMeter = new PowerMeterCoherentPowerMax(0, "CoherentPM", 1);
            //var powerMeter = new PowerMeterThorLabsPMSeries(0, "PM100USB", "SERIALNO");
            powerMeter.Initialize();
            #endregion

            #region Powermap
            var powerMap = new PowerMapDefault(0, "Virtual", "Watt");
            //var powerMapFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "powermap", "default.map");
            //PowerMapSerializer.Open(powerMap, powerMapFile);
            laser.PowerMap = powerMap;
            #endregion        

            this.Rtc = rtc;
            this.Laser = laser;
            this.Marker = marker;
            this.PowerMeter = powerMeter;
            this.PowerMap = powerMap;

            this.siriusEditorForm1.Rtc = rtc;
            this.siriusEditorForm1.Laser = laser;
            this.siriusEditorForm1.Marker = marker;
            this.siriusEditorForm1.RtcExtension1Output = this.RtcExt1DOutput;
            this.siriusEditorForm1.PowerMeter = powerMeter;
            this.siriusEditorForm1.PowerMap = powerMap;
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Marker?.Stop();

            Marker.OnStarted -= Marker_OnStarted;
            Marker.OnProgress -= Marker_OnProgress;
            Marker.OnFinished -= Marker_OnFinished;
            Marker.OnFailed -= Marker_OnFailed;

            PowerMeter?.Dispose();
            Laser?.Dispose();
            siriusEditorForm1.RtcExtension1Input?.Dispose();
            siriusEditorForm1.RtcExtension1Output?.Dispose();
            siriusEditorForm1.RtcExtension2Output?.Dispose();
            Rtc?.Dispose();
        }

        private void SiriusEditorForm1_OnDocumentSourceChanged(object sender, IDocument doc)
        {
            siriusEditorForm1.Document = doc;
        }

        private void Marker_OnStarted(IMarker sender, IMarkerArg markerArg)
        {
            Logger.Log(Logger.Type.Info, $"{sender.Name} marker has started");
        }
        private void Marker_OnProgress(IMarker sender, IMarkerArg markerArg)
        {
            Logger.Log(Logger.Type.Info, $"{sender.Name} marker has {markerArg.Progress} progressing...");
        }
        private void Marker_OnFailed(IMarker sender, IMarkerArg markerArg)
        {
            Logger.Log(Logger.Type.Error, $"{sender.Name} marker has failed");
        }
        private void Marker_OnFinished(IMarker sender, IMarkerArg markerArg)
        {
            var timeSpan = markerArg.EndTime - markerArg.StartTime;
            Logger.Log(Logger.Type.Info, $"{sender.Name} marker has finished. {timeSpan.TotalSeconds:F3}s");
        }

        /// <summary>
        /// 레시피 변경
        /// </summary>
        /// <param name="siriusFileName"></param>
        /// <returns></returns>
        public bool Open(string siriusFileName)
        {
            if (this.IsBusy())
            {
                Logger.Log(Logger.Type.Info, $"system is busy. fail to change target recipe");
                return false;
            }
            try
            {
                var doc = DocumentSerializer.OpenSirius(siriusFileName);
                siriusEditorForm1.Document = doc;
                //if you want to download recipe file by automatically
                bool success = this.Ready();
                if (success)
                    Logger.Log(Logger.Type.Info, $"success to change target recipe by {siriusFileName}");
                else
                    Logger.Log(Logger.Type.Error, $"system is busy. fail to change target recipe");
                return success;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 가공 데이타가 다운로드 된다
        /// </summary>
        /// <returns></returns>
        public bool Ready()
        {
            if (this.IsBusy())
                return false;
            var arg = new MarkerArgDefault()
            {
                Document = siriusEditorForm1.Document,
                Rtc = Rtc,
                Laser = Laser,
                RtcListType = ListType.Auto,
                MarkTargets = MarkTargets.All,
            };
            arg.ViewTargets.Add(siriusEditorForm1.View);
            arg.Offsets.Clear();
            return Marker.Ready(arg);
        }
        /// <summary>
        /// 가공 시작
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            if (this.IsBusy())
                return false;

            var doc = siriusEditorForm1.Document;
            Marker.MarkerArg.Offsets.Clear();
            return Marker.Start();
        }
        /// <summary>
        /// 가공 중지
        /// </summary>
        /// <returns></returns>
        public bool Abort()
        {
            bool success = true;
            success &= Rtc.CtlAbort();
            success &= Laser.CtlAbort();
            success &= Marker.Stop();
            return success;
        }
        /// <summary>
        /// 에러 리셋
        /// </summary>
        /// <returns></returns>
        public bool Reset()
        {
            bool success = true;
            success &= Rtc.CtlReset();
            success &= Laser.CtlReset();
            success &= Marker.Reset();
            return success;
        }
        /// <summary>
        /// 가공 준비 상태
        /// </summary>
        /// <returns></returns>
        public bool IsReady()
        {
            return Marker.IsReady;
        }
        /// <summary>
        /// 가공중 여부
        /// </summary>
        /// <returns></returns>
        public bool IsBusy()
        {
            bool busy = false;
            busy |= Rtc.CtlGetStatus(RtcStatus.Busy);
            busy |= Laser.IsBusy;
            busy |= Marker.IsBusy;
            return busy;
        }
        /// <summary>
        /// 에러 여부
        /// </summary>
        /// <returns></returns>
        public bool IsError()
        {
            bool error = false;
            error |= !Rtc.CtlGetStatus(RtcStatus.NoError);
            error |= Laser.IsError;
            error |= Marker.IsError;
            return error;
        }
    }
}
