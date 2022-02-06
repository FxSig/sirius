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

        public MainForm()
        {
            InitializeComponent();

            SpiralLab.Core.Initialize();
            
            // 신규 문서 생성
            var doc = new DocumentDefault();
            siriusEditorForm1.Document = doc;
            if (!siriusEditorForm1.EnablePens)
            {
                // 기본 펜 생성후 문서에 추가
                var pen = new PenDefault();
                doc.Action.ActEntityAdd(pen);
            }
            // 내부 데이타(IDocument) 가 변경될경우 이를 이벤트 통지를 받는 핸들러 등록
            siriusEditorForm1.OnDocumentSourceChanged += SiriusEditorForm1_OnDocumentSourceChanged;

            this.Load += MainForm_Load;
            this.FormClosing += MainForm_FormClosing;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var xmlConfigFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "syncAxis", "syncAXISConfig.xml");
            if (!File.Exists(xmlConfigFileName))
            {
                MessageBox.Show($"XML configuration file is not founded : {xmlConfigFileName}");
                return;
            }

            #region SyncAxis 초기화
            bool success = true;
            var rtc = new Rtc6SyncAxis(0, xmlConfigFileName);
            rtc.Name = "SyncAxis";
            float fov = 60.0f;    // scanner field of view : 60mm            
            float kfactor = (float)Math.Pow(2, 20) / fov; // k factor (bits/mm) = 2^20 / fov
            //var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            success &= rtc.Initialize(kfactor, LaserMode.None, string.Empty);
            success &= rtc.CtlFrequency(50 * 1000, 2); // laser frequency : 50KHz, pulse width : 2usec
            success &= rtc.CtlSpeed(100, 100); // default scanner jump and mark speed : 100mm/s

            //스테이지 이동시 기본 값 설정
            rtc.StageMoveSpeed = 10;
            rtc.StageMoveTimeOut = 5;
            #endregion

            #region 레이저 소스 초기화
            var laser = new LaserVirtual(0, "virtual", 20);  // virtual laser source with max 20W power (최대 출력 20W 의 가상 레이저 소스 생성)
            //var laser = new IPGYLP(0, "IPG YLP", 1, 20);
            //var laser = new JPTTypeE(0, "JPT Type E", 1, 20);
            //var laser = new SPIG4(0, "SPI G3/4", 1, 20);
            //var laser = new PhotonicsIndustryDX(0, "PI", 1, 20);
            //var laser = new AdvancedOptoWaveFotia(0, "Fotia", 1, 20);
            //var laser = new CoherentAviaLX(0, "Avia LX", 1, 20);
            laser.Rtc = rtc;
            laser.Initialize();
            laser.CtlPower(2);
            #endregion

            #region 마커 지정
            var marker = new MarkerDefault(0, " SyncAxis Marker ");
            #endregion

            #region RTC 확장 IO 
            //this.RtcExt1DInput = new RtcDInput(rtc, 0, "DIN RTC EXT1");
            //this.RtcExt1DInput.Initialize();
            this.RtcExt1DOutput = new RtcDOutputExt1(rtc, 0, "DOUT RTC EXT1");
            this.RtcExt1DOutput.Initialize();
            //this.RtcExt2DOutput = new RtcDOutputExt2(rtc, 0, "DIN RTC EXT2");
            //this.RtcExt2DOutput.Initialize();
            //this.siriusEditorForm1.RtcExtension1Input = this.RtcExt1DInput;
            this.siriusEditorForm1.RtcExtension1Output = this.RtcExt1DOutput;
            //this.siriusEditorForm1.RtcExtension2Output = this.RtcExt2DOutput;
            #endregion

            this.Rtc = rtc;
            this.siriusEditorForm1.Rtc = rtc;
            this.siriusEditorForm1.Laser = laser;
            this.siriusEditorForm1.Marker = marker;
        }

        private void SiriusEditorForm1_OnDocumentSourceChanged(object sender, IDocument doc)
        {
            siriusEditorForm1.Document = doc;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.siriusEditorForm1.Rtc = null;
            //this.siriusEditorForm1.Laser = null;
            //this.siriusEditorForm1.Marker = null;

            Rtc?.Dispose();
            //RtcExt1DInput?.Dispose();
            RtcExt1DOutput?.Dispose();
            //RtcExt2DOutput?.Dispose();

        }
    }
}
