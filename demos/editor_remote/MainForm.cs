﻿using System;
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
    /// <summary>
    /// LOCALHOST 에 9999 번 포트로 실행중인 TCP 서버가 필요함
    /// 통신 포맷은 ExampleFormat.txt 를 참고할것
    /// 
    /// 예)
    /// Entity;Pen1;Frequency;20000;
    /// Entity;Pen1;JumpSpeed;1000;
    /// Entity;Pen1;MarkSpeed;1000;
    /// Entity;Pen1;Power;20;
    ///
    /// Entity;Point1;Markerable;False;
    /// Entity;Point1;Location;10,0;
    /// Entity;Point1;Repeat;10;
    ///
    /// Entity;Rectangle1;Width;20;
    /// Entity;Rectangle1;Align;LeftTop;
    /// Entity;Rectangle1;Angle;90;
    /// Entity;Rectangle1;IsHatchable;true;
    /// Entity;Rectangle1;IsHatchable;false;
    /// Entity;Rectangle1;HatchInterval; 0.2;
    ///
    /// Entity;Circle1;Radius;50;
    /// Entity;Circle1;Center;5,10;
    ///
    /// Entity;QR1;Data;HELLO WORLD;
    /// Entity;QR1;ShapeType;Hatch;
    ///
    /// Entity;Stitched Image1; Rows;4;
    /// Entity;Stitched Image1; Cols;2;
    /// Entity;Stitched Image1; Width;200;
    /// Entity;Stitched Image1; Height;100;
    ///
    /// Entity;Stitched Image1; ImageIndex;0;
    /// Entity;Stitched Image1; ImageFileName;Grid0.bmp;
    /// 
    /// </summary>
    public partial class MainForm : Form
    {
        SiriusTcpClient tcpClient;

        public MainForm()
        {
            InitializeComponent();

            this.FormClosed += MainForm_FormClosed;

            SpiralLab.Core.Initialize();
            // 신규 문서 생성
            var doc = new DocumentDefault();            
            // 문서 지정
            siriusEditorForm1.Document = doc;

            // 기본 펜 생성후 문서에 추가
            var pen = new PenDefault();
            doc.Action.ActEntityAdd(pen);

            // 내부 데이타(IDocument) 가 변경될경우 이를 이벤트 통지를 받는 핸들러 등록
            siriusEditorForm1.OnDocumentSourceChanged += SiriusEditorForm1_OnDocumentSourceChanged;

            #region RTC 초기화
            //var rtc = new RtcVirtual(0); //create Rtc for dummy
            var rtc = new Rtc5(0); //create Rtc5 controller
            //var rtc = new Rtc6(0); //create Rtc6 controller
            //var rtc = new Rtc6Ethernet(0, "192.168.0.100", "255.255.255.0"); //Scanlab Rtc6 Ethernet 제어기
            //var rtc = new Rtc6SyncAxis(0, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "syncAxis", "syncAXISConfig.xml")); //Scanlab XLSCAN 솔류션

            rtc.InitLaser12SignalLevel = RtcSignalLevel.ActiveHigh;
            rtc.InitLaserOnSignalLevel = RtcSignalLevel.ActiveHigh;

            float fov = 60.0f;    ///scanner field of view : 60mm            
            float kfactor = (float)Math.Pow(2, 20) / fov; // k factor (bits/mm) = 2^20 / fov
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            rtc.Initialize(kfactor, LaserMode.Yag5, correctionFile);    // 스캐너 보정 파일 지정 : correction file
            rtc.CtlFrequency(50 * 1000, 2); // laser frequency : 50KHz, pulse width : 2usec
            rtc.CtlSpeed(100, 100); // default jump and mark speed : 100mm/s
            rtc.CtlDelay(10, 100, 200, 200, 0); // scanner and laser delays
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
            laser.Rtc = rtc;
            laser.Initialize();
            laser.CtlPower(2);
            #endregion

            this.siriusEditorForm1.Laser = laser;

            #region 마커 지정
            var marker = new MarkerDefault(0);
            #endregion
            this.siriusEditorForm1.Marker =  marker;

            this.tcpClient = new SiriusTcpClient(this.siriusEditorForm1, "localhost", 9999);
            this.tcpClient.Start();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            tcpClient?.Dispose();
        }

        private void SiriusEditorForm1_OnDocumentSourceChanged(object sender, IDocument doc)
        {
            siriusEditorForm1.Document = doc;
        }
    }
}
