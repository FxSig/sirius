using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// RTC 인터페이스
        /// </summary>
        public IRtc Rtc { get; set; }
        /// <summary>
        /// 스캐너 필드 크기 (mm)
        /// </summary>
        public float ScannerFieldSize { get; set; } = 60;
        /// <summary>
        /// 스캐너 필드 보정 파일
        /// </summary>
        public string ScannerCorrectionFile { get; set; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
        /// <summary>
        /// 레이저 인터페이스
        /// </summary>
        public ILaser Laser { get; set; }
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
            FormClosing += MainForm_FormClosing;

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

            #region RTC 초기화
            //create Rtc for dummy (가상 RTC 카드)
            //var rtc = new RtcVirtual(0); 
            //create Rtc5 controller
            var rtc = new Rtc5(0);
            //create Rtc6 controller
            //var rtc = new Rtc6(0); 
            //Rtc6 Ethernet
            //var rtc = new Rtc6Ethernet(0, "192.168.0.100", "255.255.255.0"); 

            // theoretically size of scanner field of view (이론적인 FOV 크기) : 60mm
            float fov = ScannerFieldSize;
            // k factor (bits/mm)
            // 2^20 bits / fov(mm)
            float kfactor = (float)Math.Pow(2, 20) / fov;
            // initialize rtc controller
            rtc.Initialize(kfactor, LaserMode.Yag5, ScannerCorrectionFile);
            // laser frequency : 50KHz, pulse width : 2usec
            // 주파수 50KHz, 펄스폭 2usec
            rtc.CtlFrequency(50 * 1000, 2);
            // jump and mark speed : 500mm/s
            // 점프, 마크 속도 500mm/s
            rtc.CtlSpeed(500, 500);
            // scanner and laser delays
            // 스캐너/레이저 지연값 설정
            rtc.CtlDelay(10, 100, 200, 200, 0);
            
            // processing on the fly (xy용) 스케일 설정
            // enc counts / mm
            rtc.EncYCountsPerMm = 2000;
            rtc.EncYCountsPerMm = 2000;
            // processing on the fly (angular용) 스케일 설정
            // enc counts / revolution
            rtc.EncCountsPerRevolution = 3600;

            siriusEditorForm1.Rtc = rtc;
            Rtc = rtc;
            #endregion

            #region 레이저 소스 초기화
            // virtual laser source with max 20W power (최대 출력 20W 의 가상 레이저 소스 생성)
            var laser = new LaserVirtual(0, "virtual", LaserMaxPowerWatt);
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
            //var laser = new SpectraPhysicsTalon(0, "Talon", 1, 20);

            // assign RTC instance at laser 
            laser.Rtc = rtc;
            // initialize laser source
            laser.Initialize();
            // 10% output 
            laser.CtlPower(LaserMaxPowerWatt * 0.1f);
            Laser = laser;
            siriusEditorForm1.Laser = laser;
            #endregion

            #region 마커 지정
            // create default marker 
            var marker = new MarkerDefault(0);
            marker.OnStarted += Marker_OnStarted;
            marker.OnProgress += Marker_OnProgress;
            marker.OnFinished += Marker_OnFinished;
            marker.OnFailed += Marker_OnFailed;
            siriusEditorForm1.Marker =  marker;
            Marker = marker;
            #endregion

            #region RTC extension IO 
            // create RTC io 
            var rtcExt1DInput = new RtcDInputExt1(rtc, 0, "DIN RTC EXT1");
            rtcExt1DInput.Initialize();
            var rtcExt1DOutput = new RtcDOutputExt1(rtc, 0, "DOUT RTC EXT1");
            rtcExt1DOutput.Initialize();
            var rtcExt2DOutput = new RtcDOutputExt2(rtc, 0, "DIN RTC EXT2");
            rtcExt2DOutput.Initialize();

            //rtc 5,6 only
            var rtcPin2DInput = new RtcDInput2Pin(rtc, 0, "DIN RTC PIN2");
            rtcPin2DInput.Initialize();
            var rtcPin2DOutput = new RtcDOutput2Pin(rtc, 0, "DOUT RTC PIN2");
            rtcPin2DOutput.Initialize();

            siriusEditorForm1.RtcExtension1Input = rtcExt1DInput;
            siriusEditorForm1.RtcExtension1Output = rtcExt1DOutput;
            siriusEditorForm1.RtcExtension2Output = rtcExt2DOutput;
            siriusEditorForm1.RtcPin2Input = rtcPin2DInput;
            siriusEditorForm1.RtcPin2Output = rtcPin2DOutput;
            #endregion

            #region XYZ 모터
            var motorX = new MotorVirtual(0, "X");
            motorX.Initialize();
            var motorY = new MotorVirtual(1, "Y");
            motorY.Initialize();
            var motorZ = new MotorVirtual(2, "Z");
            motorZ.Initialize();
            var motorR = new MotorVirtual(3, "R");
            motorR.Initialize();

            Motors = new IMotor[]
            {
                motorX,
                motorY,
                motorZ,
                motorR,
            };
            var motors = new MotorsDefault(0, "Group", Motors);
            siriusEditorForm1.Motors = motors;

            // layer z 제어 지원
            this.siriusEditorForm1.MotorZ = motorZ;
            #endregion

            #region PowerMeter
            // 파워메터
            var powerMeter = new PowerMeterVirtual(0, "Virtual", laser.MaxPowerWatt);
            //var powerMeter = new PowerMeterOphir(0, "OphirJuno", "3040875");
            //var powerMeter = new PowerMeterCoherentPowerMax(0, "CoherentPM", 1);
            //var powerMeter = new PowerMeterThorLabsPMSeries(0, "PM100USB", "SERIALNO");
            powerMeter.Initialize();
            siriusEditorForm1.PowerMeter = powerMeter;
            PowerMeter = powerMeter;
            #endregion

            #region Powermap
            var powerMap = new PowerMapDefault(0, "Virtual", "Watt");
            //var powerMapFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "powermap", "default.map");
            //PowerMapSerializer.Open(powerMap, powerMapFile);
            siriusEditorForm1.PowerMap = powerMap;
            laser.PowerMap = powerMap;
            PowerMap = powerMap;
            #endregion            
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
            siriusEditorForm1.RtcPin2Input?.Dispose();
            siriusEditorForm1.RtcPin2Output?.Dispose();
            Rtc?.Dispose();
            if (null != Motors)
                foreach(var motor in Motors)
                    motor?.Dispose();
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
            Logger.Log(Logger.Type.Info, $"{sender.Name} marker has finished");
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
