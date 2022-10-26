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
 * MOTF 파일을 열어 가공 
 * 
 * 파일경로 : bin\recipes\motf_angular.sirius
 * 엔코더 리셋, 엔코더 시뮬레이션을 테스트 한다
 * 엔코더 대기 위치를 설정하고 가공 데이타를 동적 생성하여 가공한다
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
                //엔코더 스케일 설정 
                //한 바퀴 회전시 발생되는 엔코더 펄스의 수
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
            laser.CtlPower(2);
            #endregion

            this.siriusEditorForm1.Laser = laser;

            #region 마커 지정
            var marker = new MarkerDefault(0);
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
            //타이머가 지속적으로 현재 엔코더 펄스개수(cnt), 위치(mm)를 얻어온다
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
            this.siriusEditorForm1.Marker.Stop();
            this.siriusEditorForm1.Laser?.Dispose();
            this.siriusEditorForm1.Rtc?.Dispose();
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            // 문서 생성
            var doc = new DocumentDefault();

            // create layer
            // 레이어 생성
            var layer = new Layer("Default");

            // motf angular 시작 개체 추가
            var motfAngularBegin = new MotfAngularBegin()
            {
                IsEncoderReset = true,
                Center = RotateCenter,
            };
            layer.Add(motfAngularBegin);


            // 45, 135, 225, 270 위치에 4개의 폴리라인 가공
            for (float angle = 45; angle < 360; angle += 90)
            {
                // motf angular 대기 각도 개체 추가
                var motfAngularWait = new MotfAngularWait()
                {
                    WaitAngle = angle,
                    Condition = EncoderWaitCondition.Over,
                };
                layer.Add(motfAngularWait);

                // 사각형 모양(임의의 폐곡선)의 폴리라인 개체 추가
                var lwPolyline = new LwPolyline();
                lwPolyline.Color2 = System.Drawing.Color.White;
                lwPolyline.Add(new LwPolyLineVertex(-5f, 5f));
                lwPolyline.Add(new LwPolyLineVertex(5f, 5f));
                lwPolyline.Add(new LwPolyLineVertex(5f, -5f));
                lwPolyline.Add(new LwPolyLineVertex(-5f, -5f));
                lwPolyline.IsClosed = true;
                // 내부 해치 생성
                //lwPolyline.IsHatchable = true;
                //lwPolyline.HatchMode = HatchMode.CrossLine;
                //lwPolyline.HatchInterval = 0.2f;
                //lwPolyline.HatchExclude = 0;
                //lwPolyline.HatchAngle = 0;
                //lwPolyline.HatchAngle2 = 90;
                // transit rotate center to scanner center distance
                // 회전 중심으로 부터 스캐너 중심위치 거리로 이동
                lwPolyline.Transit(-RotateCenter);
                // rotate figure by rotate center (CW direction =  encoder +)
                // 회전 중심 기준으로 회전 (물체의 시계방향 회전이 엔코더 증가 방향)
                lwPolyline.Rotate(-angle, Vector2.Zero);

                // 폴리라인 개체를 레이어에 추가
                layer.Add(lwPolyline);
            }

            // motf angular 끝 개체 추가
            var motfAngularEnd = new MotfEnd()
            {
                  Location = Vector2.Zero,
            };
            layer.Add(motfAngularEnd);

            // 내부 개체들의 데이타 재생성 
            layer.Regen();

            // 펜 파라메터 설정 (가공 조건 설정)
            var pen = doc.Pens.ColorOf(System.Drawing.Color.White) as PenDefault;
            pen.Frequency = 50 * 1000;
            pen.PulseWidth = 2;
            pen.JumpSpeed = 1000;
            pen.MarkSpeed = 1000;
            pen.LaserOnDelay = 0;
            pen.LaserOffDelay = 0;
            pen.ScannerJumpDelay = 200;
            pen.ScannerMarkDelay = 50;
            pen.ScannerPolygonDelay = 10;
            pen.LaserQSwitchDelay = 0;

            // 레이어를 문서에 추가
            doc.Layers.Add(layer);

            // 이 레이어를 활성 레이어로 지정
            doc.Layers.Active = layer;

            // 편집기에 문서 지정
            siriusEditorForm1.Document = doc;
        }
    }
}
