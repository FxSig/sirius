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
            //IRtc rtc = new RtcVirtual(0); ///가상 rtc 제어기 생성
            IRtc rtc = new RtcVirtual(0, "output.txt"); ///가상 rtc 제어기 생성
            //IRtc rtc = new Rtc5(0); ///rtc 5 제어기 생성
            float fov = 60.0f;    /// scanner field of view : 60mm            
            float kfactor = (float)Math.Pow(2, 20) / fov; /// k factor (bits/mm) = 2^20 / fov
            rtc.Initialize(kfactor, LaserMode.Yag1, "cor_1to1.ct5");    /// 스캐너 보정 파일 지정 : correction file
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
