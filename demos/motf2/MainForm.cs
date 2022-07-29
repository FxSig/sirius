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
        public MainForm()
        {
            InitializeComponent();

            //라이브러리 초기화
            SpiralLab.Core.Initialize();
            //문서 생성
            var doc = new DocumentDefault();
            siriusEditorForm1.Document = doc;
            // 내부 데이타(IDocument) 가 변경될경우 이를 이벤트 통지를 받는 핸들러 등록
            siriusEditorForm1.OnDocumentSourceChanged += SiriusEditorForm1_OnDocumentSourceChanged;

            #region RTC 초기화
            //var rtc = new RtcVirtual(0); //create Rtc for dummy
            var rtc = new Rtc5(0); //create Rtc5 controller
            //var rtc = new Rtc6(0); //create Rtc6 controller
            //var rtc = new Rtc6Ethernet(0, "192.168.0.100", "255.255.255.0"); //실험적인 상태 (Scanlab Rtc6 Ethernet 제어기)
            //var rtc = new Rtc6SyncAxis(0, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configuration", "syncAXISConfig.xml")); //실험적인 상태 (Scanlab XLSCAN 솔류션)

            float fov = 60.0f;    // scanner field of view : 60mm            
            float kfactor = (float)Math.Pow(2, 20) / fov; // k factor (bits/mm) = 2^20 / fov
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);    // 스캐너 보정 파일 지정 : correction file
            rtc.CtlFrequency(50 * 1000, 2); // laser frequency : 50KHz, pulse width : 2usec
            rtc.CtlSpeed(100, 100); // default jump and mark speed : 100mm/s
            rtc.CtlDelay(10, 100, 200, 200, 0); // scanner and laser delays
            
            var rtcMOTF = rtc as IRtcMOTF;
            //엔코더 스케일 설정 (단축 MOTF)
            rtcMOTF.EncXCountsPerMm = 2000;  //단위 mm 이동시 발생되는 엔코더 펄스의 수
            rtcMOTF.EncYCountsPerMm = 0;

            #endregion
            this.siriusEditorForm1.Rtc = rtc;

            #region 레이저 소스 초기화
            var laser = new LaserVirtual(0, "virtual", 20.0f);
            laser.Rtc = rtc;
            laser.Initialize();
            laser.CtlPower(10);
            #endregion

            this.siriusEditorForm1.Laser = laser;

            #region 마커 지정
            var marker = new MotfMarker(0);
            #endregion
            this.siriusEditorForm1.Marker =  marker;

            timer1.Enabled = true;
        }

        private void SiriusEditorForm1_OnDocumentSourceChanged(object sender, IDocument doc)
        {
            siriusEditorForm1.Document = doc;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //타이머가 지속적으로 현재 엔코더 펄스개수 와 실제 물리적인 이동량(mm) 를 얻어온다
            var rtc = this.siriusEditorForm1.Rtc;
            var rtcMOTF = rtc as IRtcMOTF;
            rtcMOTF.CtlMotfGetEncoder(out int encX, out int encY, out float encXmm, out float encYmm);

            lblEncXCnt.Text = $"{encX} cnt";
            lblEncYCnt.Text = $"{encY} cnt";
            lblEncXmm.Text = $"{encXmm:F3} mm";
            lblEncYmm.Text = $"{encYmm:F3} mm";
        }
        private void mnuEncReset_Click(object sender, EventArgs e)
        {
            //현재 위치에서 엔코더 초기화 (리셋)
            var rtc = this.siriusEditorForm1.Rtc;
            var rtcMOTF = rtc as IRtcMOTF;
            rtcMOTF.CtlMotfEncoderReset();
        }

        private void lblMarker_Click(object sender, EventArgs e)
        {
            //마커 대화상자 창을 띄운다
            var marker = this.siriusEditorForm1.Marker;
            var markerArg = new MarkerArgDefault()
            {
                Document = this.siriusEditorForm1.Document,
                Rtc = this.siriusEditorForm1.Rtc,
                Laser = this.siriusEditorForm1.Laser,
            };
            marker.Ready(markerArg);
            var form = new MotfMarkerForm(marker);
            form.ShowDialog(this);
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
        }
    }
}
