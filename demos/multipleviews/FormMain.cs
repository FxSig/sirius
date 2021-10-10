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
    public partial class FormMain : Form
    {
        FormViewer formViewer;
        FormEditor formEditor1;
        FormEditor formEditor2;
        public FormMain()
        {
            InitializeComponent();

            SpiralLab.Core.Initialize();

            //Rtc 제어기 객체 2개 생성
            var rtc1 = new RtcVirtual(0, "output1.txt");
            var rtc2 = new RtcVirtual(1, "output2.txt");
            //var rtc = new Rtc5(0); //create Rtc5 controller
            //var rtc = new Rtc6(0); //create Rtc6 controller
            //var rtc = new Rtc6Ethernet(0, "192.168.0.100", "255.255.255.0"); //실험적인 상태 (Scanlab Rtc6 Ethernet 제어기)
            //var rtc = new Rtc6SyncAxis(0, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "syncAxis", "syncAXISConfig.xml")); //실험적인 상태 (Scanlab XLSCAN 솔류션)

            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            rtc1.Initialize(1000, LaserMode.Yag1, correctionFile);
            rtc2.Initialize(1000, LaserMode.Yag1, correctionFile);

            //Laser 소스 객체 2개 생성
            var laser1 = new LaserVirtual(0, "virtual", 20);  // virtual laser source with max 20W power (최대 출력 20W 의 가상 레이저 소스 생성)
            //var laser1 = new IPGYLP(0, "IPG YLP", 1, 20);
            //var laser1 = new JPTTypeE(0, "JPT Type E", 1, 20);
            //var laser1 = new SPIG4(0, "SPI G3/4", 1, 20);
            //var laser1 = new PhotonicsIndustryDX(0, "PI", 1, 20);
            //var laser1 = new AdvancedOptoWaveFotia(0, "Fotia", 1, 20);
            //var laser1 = new CoherentAviaLX(0, "Avia LX", 1, 20);
            laser1.Rtc = rtc1;
            laser1.Initialize();
            laser1.CtlPower(2);

            var laser2 = new LaserVirtual(0, "virtual", 20);  // virtual laser source with max 20W power (최대 출력 20W 의 가상 레이저 소스 생성)
            //var laser2 = new IPGYLP(0, "IPG YLP", 2, 20);
            //var laser2 = new JPTTypeE(0, "JPT Type E", 2, 20);
            //var laser2 = new SPIG4(0, "SPI G3/4", 2, 20);
            //var laser2 = new PhotonicsIndustryDX(0, "PI", 2, 20);
            //var laser2 = new AdvancedOptoWaveFotia(0, "Fotia", 2, 20);
            //var laser2 = new CoherentAviaLX(0, "Avia LX", 2, 20);
            laser2.Rtc = rtc2;
            laser2.Initialize();
            laser2.CtlPower(2);

            //Marker 2개 생성
            var marker1 = new MarkerDefault(0);
            var marker2 = new MarkerDefault(1);

            //두개의 문서 생성
            var doc1 = new DocumentDefault();
            var doc2 = new DocumentDefault();

            //뷰어에 문서 소스 설정
            this.formViewer = new FormViewer();
            this.formViewer.Viewer1.Document = doc1;
            this.formViewer.Viewer1.AliasName = "Left Viewer";
            this.formViewer.Viewer2.Document = doc2;
            this.formViewer.Viewer2.AliasName = "Right Viewer";

            //에디터 1에 문서 소스 설정
            this.formEditor1 = new FormEditor();
            this.formEditor1.Editor.Document = doc1;
            this.formEditor1.Editor.AliasName = "Left Editor";

            //에디터 2에 문서 소스 설정
            this.formEditor2 = new FormEditor();
            this.formEditor2.Editor.Document = doc2;
            this.formEditor2.Editor.AliasName = "Right Editor";

            //소스 문서(IDocument) 가 변경될경우 다른 멀티 뷰에 이를 통지가능하도록 이벤트 핸들러 등록
            this.formEditor1.Editor.OnDocumentSourceChanged += Editor1_OnDocumentSourceChanged;
            this.formEditor2.Editor.OnDocumentSourceChanged += Editor2_OnDocumentSourceChanged;

            // 기본 펜 개체 생성
            var pen1 = new PenDefault();
            doc1.Action.ActEntityAdd(pen1);
            var pen2 = new PenDefault();
            doc2.Action.ActEntityAdd(pen2);

            // 에디터1 와 하드웨어 연결
            this.formEditor1.Editor.Rtc = rtc1;
            this.formEditor1.Editor.Laser = laser1;
            this.formEditor1.Editor.Marker = marker1;

            //에디터1 와 하드웨어 연결
            this.formEditor2.Editor.Rtc = rtc2;
            this.formEditor2.Editor.Laser = laser2;
            this.formEditor2.Editor.Marker = marker2;

            // 뷰어를 초기화면에 출력
            SwitchForm(panel3, this.formViewer);
        }

        private void Editor1_OnDocumentSourceChanged(object sender, IDocument doc)
        {
            // 변경된 문서 소스 업데이트
            this.formViewer.Viewer1.Document = doc;
            this.formEditor1.Editor.Document = doc;
        }

        private void Editor2_OnDocumentSourceChanged(object sender, IDocument doc)
        {
            // 변경된 문서 소스 업데이트
            this.formViewer.Viewer2.Document = doc;
            this.formEditor2.Editor.Document = doc;
        }

        /// <summary>
        /// 폼(Form) 전환
        /// </summary>
        /// <param name="destination">폼이 놓여질 대상 패널</param>
        /// <param name="target">화면에 출력될 폼</param>
        public void SwitchForm(Panel destination, Form target)
        {
            destination.SuspendLayout();
            foreach (Form f in destination.Controls)
            {
                f.Visible = false;
            }
            destination.Controls.Clear();

            target.SuspendLayout();
            target.TopLevel = false;
            target.FormBorderStyle = FormBorderStyle.None;
            target.Dock = DockStyle.Fill;
            target.Visible = true;
            destination.Controls.Add(target);
            target.ResumeLayout();
            destination.ResumeLayout();
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            SwitchForm(panel3, this.formViewer);
        }

        private void btnEditor1_Click(object sender, EventArgs e)
        {
            SwitchForm(panel3, this.formEditor1);
        }

        private void btnEditor2_Click(object sender, EventArgs e)
        {
            SwitchForm(panel3, this.formEditor2);
        }
    }
}
