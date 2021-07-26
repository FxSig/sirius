using SpiralLab.Sirius;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace editor_wpf
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            SpiralLab.Core.Initialize();
            var siriusEditorForm = (siriusEditor.Child as SiriusEditorForm);

            var doc = new DocumentDefault();
            siriusEditorForm.Document = doc;

            #region RTC 초기화
            //var rtc = new RtcVirtual(0); //create Rtc for dummy
            var rtc = new Rtc5(0); //create Rtc5 controller
            //var rtc = new Rtc6(0); //create Rtc6 controller
            //var rtc = new Rtc6Ethernet(0, "192.168.0.100", "255.255.255.0"); //실험적인 상태 (Scanlab Rtc6 Ethernet 제어기)
            //var rtc = new Rtc6SyncAxis(0, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "syncAxis", "syncAXISConfig.xml")); //실험적인 상태 (Scanlab XLSCAN 솔류션)

            float fov = 60.0f;    ///scanner field of view : 60mm            
            float kfactor = (float)Math.Pow(2, 20) / fov; // k factor (bits/mm) = 2^20 / fov
            var correctionFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);    // 스캐너 보정 파일 지정 : correction file
            rtc.CtlFrequency(50 * 1000, 2); // laser frequency : 50KHz, pulse width : 2usec
            rtc.CtlSpeed(100, 100); // default jump and mark speed : 100mm/s
            rtc.CtlDelay(10, 100, 200, 200, 0); // scanner and laser delays
            #endregion
            siriusEditorForm.Rtc = rtc;

            #region 레이저 소스 초기화
            ILaser laser = new LaserVirtual(0, "virtual", 20.0f);
            laser.Rtc = rtc;
            laser.Initialize();
            laser.CtlPower(10);
            #endregion

            siriusEditorForm.Laser = laser;

            #region 마커 지정
            var marker = new MarkerDefault(0);
            #endregion
            siriusEditorForm.Marker = marker;
        }

        private void SiriusOnDocumentSourceChanged(object sender, IDocument doc)
        {
            var editorForm = sender as SiriusEditorForm;
            editorForm.Document = doc;
        }
    }
}
