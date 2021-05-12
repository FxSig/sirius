using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

            SpiralLab.Core.Initialize();

            // 문서 생성
            var doc = new DocumentDefault();
            siriusEditorForm1.Document = doc;
            siriusViewerForm1.Document = doc;

            // 소스 문서(IDocument) 가 변경될경우 다른 멀티 뷰에 이를 통지하는 이벤트 핸들러 등록
            siriusEditorForm1.OnDocumentSourceChanged += SiriusEditorForm1_OnDocumentSourceChanged;

            #region RTC 초기화
            //var rtc = new RtcVirtual(0); //create Rtc for dummy
            var rtc = new Rtc5(0); //create Rtc5 controller
            //var rtc = new Rtc6(0); //create Rtc6 controller
            //var rtc = new Rtc6Ethernet(0, "192.168.0.100", "255.255.255.0"); //실험적인 상태 (Scanlab Rtc6 Ethernet 제어기)
            //var rtc = new Rtc6SyncAxis(0, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configuration", "syncAXISConfig.xml")); //실험적인 상태 (Scanlab XLSCAN 솔류션)

            float fov = 60.0f;    // scanner field of view : 60mm            
            float kfactor = (float)Math.Pow(2, 20) / fov; // k factor (bits/mm) = 2^20 / fov
            rtc.Initialize(kfactor, LaserMode.Yag1, "cor_1to1.ct5");    // 스캐너 보정 파일 지정 : correction file
            rtc.CtlFrequency(50 * 1000, 2); // laser frequency : 50KHz, pulse width : 2usec
            rtc.CtlSpeed(100, 100); // default jump and mark speed : 100mm/s
            rtc.CtlDelay(10, 100, 200, 200, 0); // scanner and laser delays
            #endregion
            
            this.siriusEditorForm1.Rtc = rtc;

            #region 레이저 소스 초기화
            ILaser laser = new LaserVirtual(0, "virtual", 20.0f);
            laser.Initialize();
            laser.CtlPower(rtc, 10);
            #endregion
            this.siriusEditorForm1.Laser = laser;

            #region 마커 지정
            var marker = new MarkerDefault(0);
            #endregion
            this.siriusEditorForm1.Laser = laser;
            this.siriusEditorForm1.Marker = marker;
        }

        private void SiriusEditorForm1_OnDocumentSourceChanged(object sender, IDocument doc)
        {
            // 변경된 문서 소스를 상대에게 통지하여 업데이트
            siriusEditorForm1.Document = doc;
            siriusViewerForm1.Document = doc;
        }
    }
}
