/*
 *                                                            ,--,      ,--,                              
 *             ,-.----.                                     ,---.'|   ,---.'|                              
 *   .--.--.   \    /  \     ,---,,-.----.      ,---,       |   | :   |   | :      ,---,           ,---,.  
 *  /  /    '. |   :    \ ,`--.' |\    /  \    '  .' \      :   : |   :   : |     '  .' \        ,'  .'  \ 
 * |  :  /`. / |   |  .\ :|   :  :;   :    \  /  ;    '.    |   ' :   |   ' :    /  ;    '.    ,---.' .' | 
 * ;  |  |--`  .   :  |: |:   |  '|   | .\ : :  :       \   ;   ; '   ;   ; '   :  :       \   |   |  |: | 
 * |  :  ;_    |   |   \ :|   :  |.   : |: | :  |   /\   \  '   | |__ '   | |__ :  |   /\   \  :   :  :  / 
 *  \  \    `. |   : .   /'   '  ;|   |  \ : |  :  ' ;.   : |   | :.'||   | :.'||  :  ' ;.   : :   |    ;  
 *   `----.   \;   | |`-' |   |  ||   : .  / |  |  ;/  \   \'   :    ;'   :    ;|  |  ;/  \   \|   :     \ 
 *   __ \  \  ||   | ;    '   :  ;;   | |  \ '  :  | \  \ ,'|   |  ./ |   |  ./ '  :  | \  \ ,'|   |   . | 
 *  /  /`--'  /:   ' |    |   |  '|   | ;\  \|  |  '  '--'  ;   : ;   ;   : ;   |  |  '  '--'  '   :  '; | 
 * '--'.     / :   : :    '   :  |:   ' | \.'|  :  :        |   ,/    |   ,/    |  :  :        |   |  | ;  
 *   `--'---'  |   | :    ;   |.' :   : :-'  |  | ,'        '---'     '---'     |  | ,'        |   :   /   
 *             `---'.|    '---'   |   |.'    `--''                              `--''          |   | ,'    
 *               `---`            `---'                                                        `----'   
 * 
 *
 * MOTF Angular Demo2 
 * 가공 데이타를 동적으로 만들어 가공
 * 
 * Test Functions : Measurement + Simulate Encoder + Rotate Center + (Wait for Encoder
 * 테스트 기능 : 계측 (Measurement) + 엔코더 시뮬레이션 (Simulate Encoder) + 회전 중심 오프셋 (Rotate Center) + 엔코더 대기 (Wait for Encoder)
 * Author : hong chan, choi / hcchoi@spirallab.co.kr (http://spirallab.co.kr)
 * 
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    /// <summary>
    /// 회전 엔코더 기반 MOTF #2
    /// 가공 데이타를 동적으로 생성 사용
    /// </summary>
    public partial class MainFormAngular2 : Form
    {

        // rotate center position 
        // 스캐너 중심에서 회전 중심으로의 위치
        public static Vector2 RotateCenter = new Vector2(-50, 0);


        public MainFormAngular2()
        {
            InitializeComponent();

            // initialize sirius library
            // 라이브러리 초기화
            SpiralLab.Core.Initialize();

            // create document
            // 문서 생성
            var doc = new DocumentDefault();
            // assign document into sirius editor
            siriusEditorForm1.Document = doc;

            // assign event handler
            // 내부 데이타(IDocument) 가 변경될경우 이를 이벤트 통지를 받는 핸들러 등록
            siriusEditorForm1.OnDocumentSourceChanged += SiriusEditorForm1_OnDocumentSourceChanged;

            #region RTC 초기화
            //var rtc = new RtcVirtual(0); //create Rtc for dummy
            var rtc = new Rtc5(0); //create Rtc5 controller
            //var rtc = new Rtc6(0); //create Rtc6 controller
            //var rtc = new Rtc6Ethernet(0, "192.168.0.100", "255.255.255.0"); //Scanlab Rtc6 Ethernet 제어기
            //var rtc = new Rtc6SyncAxis(0, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "syncAxis", "syncAXISConfig.xml")); //Scanlab XLSCAN 솔류션

            float fov = 60.0f;    // scanner field of view : 60mm            
            float kfactor = (float)Math.Pow(2, 20) / fov; // k factor (bits/mm) = 2^20 / fov
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            rtc.Initialize(kfactor, LaserMode.Yag5, correctionFile);    // 스캐너 보정 파일 지정 : correction file
            rtc.CtlFrequency(50 * 1000, 2); // laser frequency : 50KHz, pulse width : 2usec
            rtc.CtlSpeed(100, 100); // default jump and mark speed : 100mm/s
            rtc.CtlDelay(10, 100, 200, 200, 0); // scanner and laser delays

            if (rtc is IRtcMOTF rtcMOTF)
            {
                // 엔코더 스케일 설정 
                // 한 바퀴 회전시 발생되는 엔코더 펄스의 수
                rtcMOTF.EncCountsPerRevolution = 3600;
            }
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

            // set output power to 2watt
            laser.CtlPower(2);
            #endregion

            this.siriusEditorForm1.Laser = laser;

            #region 마커 지정
            // create marker
            var marker = new MarkerDefault(0);
            #endregion

            this.siriusEditorForm1.Marker =  marker;

            timer1.Enabled = true;
        }

        private void SiriusEditorForm1_OnDocumentSourceChanged(object sender, IDocument doc)
        {
            //replace document at sirius editor
            siriusEditorForm1.Document = doc;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // query encoder counts and angle(degree)
            // 타이머가 지속적으로 현재 엔코더 펄스개수(cnt), 위치(mm)를 얻어온다
            var rtc = this.siriusEditorForm1.Rtc;
            if (rtc is IRtcMOTF rtcMOTF)
            {
                rtcMOTF.CtlMotfGetAngularEncoder(out int enc, out float angle);
                lblEncCnt.Text = $"{enc} cnt";
                lblEncAngle.Text = $"{angle:F3} °";
            }
        } 
     
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // disposing
            this.siriusEditorForm1.Marker.Stop();
            this.siriusEditorForm1.Laser?.Dispose();
            this.siriusEditorForm1.Rtc?.Dispose();
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            // create document
            // 문서 생성
            var doc = new DocumentDefault();

            // create layer 
            // 레이어 생성
            var layer = new Layer("Default");

            // create measurement begin entity
            // measurement begin 개체 생성
            var measurementBegin = new MeasurementBegin()
            {
                Channels = new MeasurementChannel[4]
                {
                    MeasurementChannel.SampleX, //X commanded
                    MeasurementChannel.SampleY, //Y commanded
                    MeasurementChannel.LaserOn, //Gate signal 0/1
                    MeasurementChannel.Enc0Counter, //Enc0
                },
                Frequency = 1000, // 계측 측정 주기 (Hz)
            };
            // add mesurement begin entity into layer
            // measurement begin 개체 레이어에 추가
            layer.Add(measurementBegin);

            // create motf angular begin entity
            // motf angular begin 개체 생성
            var motfAngularBegin = new MotfAngularBegin()
            {
                IsEncoderReset = true, // encoder reset 
                Center = RotateCenter, // rotate center location from scanner
            };

            // add motf angular begin entity into layer
            // motf angular begin 개체 레이어에 추가
            layer.Add(motfAngularBegin);

            // wait each degrees (0, 45, 90, 125, 180, 225, 270, 315) and then mark polyline 
            // 0, 45, 90, 125, 180, 225, 270, 315 각도 도달을 대기하면서 폴리라인 가공
            for (float angle = 0; angle < 360; angle += 45)
            {
                // create motf angular wait entity
                // motf angular wait 개체 생성
                var motfAngularWait = new MotfAngularWait()
                {
                    WaitAngle = angle, //wait angle (대기 각도)
                    Condition = EncoderWaitCondition.Over, //wait condition (대기 조건)
                };
                // add motf angular wait entity into layer
                // motf angular wait 개체 레이어에 추가
                layer.Add(motfAngularWait);

                // create LWPolyline entity
                // LW폴리라인 개체 생성 
                var lwPolyline = new LwPolyline();
                lwPolyline.Color2 = System.Drawing.Color.White;
                lwPolyline.Add(new LwPolyLineVertex(-5f, 5f));
                lwPolyline.Add(new LwPolyLineVertex(5f, 5f));
                lwPolyline.Add(new LwPolyLineVertex(5f, -5f));
                lwPolyline.Add(new LwPolyLineVertex(-5f, -5f));
                lwPolyline.IsClosed = true;

                // also, internal line hatch patterns 
                // 내부 라인 해치를 추가할 경우
                //lwPolyline.IsHatchable = true;
                //lwPolyline.HatchMode = HatchMode.CrossLine;
                //lwPolyline.HatchInterval = 0.2f;
                //lwPolyline.HatchExclude = 0;
                //lwPolyline.HatchAngle = 0;
                //lwPolyline.HatchAngle2 = 90;

                // rotate LWPolyline entity by rotate center 
                // 회전 중심 기준으로 회전 (물체의 시계방향 회전이 엔코더 증가 방향)
                lwPolyline.Rotate(angle, RotateCenter);

                // add LWPolyline entity into layer
                // LW폴리라인 개체를 레이어에 추가
                layer.Add(lwPolyline);
            }

            // create motf end entity
            // motf end 개체 생성
            var motfAngularEnd = new MotfEnd()
            {
                  Location = Vector2.Zero, // jump at orgin location (MOTF 종료시 원점으로 점프)
            };
            // add motf end entity into layer
            // motf end 개체 레이어에 추가
            layer.Add(motfAngularEnd);

            // create measurement end entity
            // measurement end 개체 생성
            var measurementEnd = new MeasurementEnd();
            // add mesurement end entity into layer
            // measurement end 개체 레이어에 추가
            layer.Add(measurementEnd);

            // regen entities within layer
            // 레이어 내부의 개체들의 데이타 갱신 
            layer.Regen();

            // query default(white) pen entity and configure it at document
            // 문서에서 기본펜 (흰색) 파라메터 가져와 설정
            var pen = doc.Pens.ColorOf(System.Drawing.Color.White) as PenDefault;
            pen.Frequency = 50 * 1000; //KHz
            pen.PulseWidth = 2; //usec
            pen.JumpSpeed = 1000; //mm/s
            pen.MarkSpeed = 1000; //mm/s
            pen.LaserOnDelay = 0; //usec
            pen.LaserOffDelay = 0; //usec
            pen.ScannerJumpDelay = 200; //usec
            pen.ScannerMarkDelay = 50; //usec
            pen.ScannerPolygonDelay = 10; //us
            pen.LaserQSwitchDelay = 0; //usec

            // add layer into layers
            // 레이어를 레이어 집합에 추가
            doc.Layers.Add(layer);

            // activate layer as default
            // 이 레이어를 활성 레이어로 지정
            doc.Layers.Active = layer;

            // assing document at sirius editor
            // 시리우스 편집기에 문서 객체를 지정
            siriusEditorForm1.Document = doc;
        }
    }
}
