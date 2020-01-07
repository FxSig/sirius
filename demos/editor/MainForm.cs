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

            SpiralLab.Core.Initialize();

            var doc = new DocumentDefault();
            siriusEditorForm1.Document = doc;
            /// 내부 데이타(IDocument) 가 변경될경우 이를 이벤트 통지를 받는 핸들러 등록
            siriusEditorForm1.OnDocumentSourceChanged += SiriusEditorForm1_OnDocumentSourceChanged;

            #region RTC 초기화
            var rtc = new RtcVirtual(0); ///create Rtc for dummy
            //var rtc = new Rtc5(0); ///create Rtc5 controller
            //var rtc = new Rtc6(0); ///create Rtc6 controller
            //var rtc = new Rtc6Ethernet(0, "192.168.0.200"); ///create Rtc6 ethernet controller
            //var rtc = new Rtc53D(0); ///create Rtc5 + 3D option controller
            //var rtc = new Rtc63D(0); ///create Rtc5 + 3D option controller
            //var rtc = new Rtc5DualHead(0); ///create Rtc5 + Dual head option controller
            //var rtc = new Rtc5MOTF(0); ///create Rtc5 + MOTF option controller
            //var rtc = new Rtc6MOTF(0); ///create Rtc6 + MOTF option controller
            //var rtc = new Rtc6SyncAxis(0); 
            //var rtc = new Rtc6SyncAxis(0, "syncAXISConfig.xml"); ///create Rtc6 + XL-SCAN (ACS+SYNCAXIS) option controller

            float fov = 60.0f;    /// scanner field of view : 60mm            
            float kfactor = (float)Math.Pow(2, 20) / fov; /// k factor (bits/mm) = 2^20 / fov
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);    /// 스캐너 보정 파일 지정 : correction file
            rtc.CtlFrequency(50 * 1000, 2); /// laser frequency : 50KHz, pulse width : 2usec
            rtc.CtlSpeed(100, 100); /// default jump and mark speed : 100mm/s
            rtc.CtlDelay(10, 100, 200, 200, 0); /// scanner and laser delays
            #endregion
            this.siriusEditorForm1.Rtc = rtc;

            #region 레이저 소스 초기화
            ILaser laser = new LaserVirtual(0, "virtual", 20.0f);
            laser.Initialize();
            var pen = new Pen()
            {
                Power = 10.0f,
            };
            laser.CtlPower(rtc, pen);
            #endregion

            this.siriusEditorForm1.Laser = laser;

            #region 마커 지정
            var marker = new MarkerDefault(0);
            #endregion
            this.siriusEditorForm1.Marker =  marker;
        }

        private void SiriusEditorForm1_OnDocumentSourceChanged(object sender, IDocument doc)
        {
            siriusEditorForm1.Document = doc;
        }
    }
}
