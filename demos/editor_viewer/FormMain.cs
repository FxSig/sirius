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
using SpiralLab.Sirius;

namespace SpiralLab.Sirius
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            this.FormClosing += MainForm_FormClosing;

            SpiralLab.Core.Initialize();

            // 문서 생성
            var doc = new DocumentDefault();

            //에디터에 문서 지정시 레이어가 없으면 자동 생성됨
            siriusEditorForm1.Document = doc;
            siriusViewerForm1.Document = doc;

            if (!siriusEditorForm1.EnablePens)
            {
                // 기본 펜 생성후 문서에 추가
                var pen = new PenDefault();
                doc.Action.ActEntityAdd(pen);
            }

            // 소스 문서(IDocument) 가 변경될경우 다른 멀티 뷰에 이를 통지하는 이벤트 핸들러 등록
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

            this.siriusEditorForm1.Rtc = rtc;

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
            // set basic power output to 2W
            laser.CtlPower(2);
            #endregion

            this.siriusEditorForm1.Laser = laser;

            #region 마커 지정
            var marker = new MarkerDefault(0);
            marker.MarkerArg.ViewTargets.Add(siriusEditorForm1.View);
            marker.MarkerArg.ViewTargets.Add(siriusViewerForm1.View);
            #endregion

            this.siriusEditorForm1.Laser = laser;
            this.siriusEditorForm1.Marker = marker;

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

            this.siriusEditorForm1.RtcExtension1Input = rtcExt1DInput;
            this.siriusEditorForm1.RtcExtension1Output = rtcExt1DOutput;
            this.siriusEditorForm1.RtcExtension2Output = rtcExt2DOutput;
            this.siriusEditorForm1.RtcPin2Input = rtcPin2DInput;
            this.siriusEditorForm1.RtcPin2Output = rtcPin2DOutput;
            #endregion


            #region PowerMeter
            // 파워메터
            //var pm = new PowerMeterVirtual(0, "Virtual");
            var powerMeter = new PowerMeterOphir(0, "OphirJuno", "3040875");
            //var powerMeter = new PowerMeterCoherentPowerMax(0, "CoherentPM", 1);
            //var powerMeter = new PowerMeterThorLabsPMSeries(0, "PM100USB", "SERIALNO");
            powerMeter.Initialize();
            this.siriusEditorForm1.PowerMeter = powerMeter;
            #endregion
        }

        private void SiriusEditorForm1_OnDocumentSourceChanged(object sender, IDocument doc)
        {
            // 변경된 문서 소스를 상대에게 통지하여 업데이트
            siriusEditorForm1.Document = doc;
            siriusViewerForm1.Document = doc;
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            siriusEditorForm1.Marker?.Stop();

            siriusEditorForm1.PowerMeter?.Dispose();
            siriusEditorForm1.PowerMeter = null;

            siriusEditorForm1.Laser?.Dispose();
            siriusEditorForm1.Laser = null;

            siriusEditorForm1.RtcExtension1Input?.Dispose();
            siriusEditorForm1.RtcExtension1Output?.Dispose();
            siriusEditorForm1.RtcExtension2Output?.Dispose();
            siriusEditorForm1.RtcPin2Input?.Dispose();
            siriusEditorForm1.RtcPin2Output?.Dispose();

            siriusEditorForm1.Rtc?.Dispose();
            siriusEditorForm1.Rtc = null;
        }
    }
}
