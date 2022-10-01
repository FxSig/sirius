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
 * 파일경로 : bin\recipes\motf.sirius
 * 엔코더 리셋, 엔코더 시뮬레이션을 테스트 한다
 * 엔코더 대기 위치를 설정하고 원 형상을 가공한다
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
            //var rtc = new Rtc6Ethernet(0, "192.168.0.100", "255.255.255.0"); //Scanlab Rtc6 Ethernet 제어기
            //var rtc = new Rtc6SyncAxis(0, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "syncAxis", "syncAXISConfig.xml")); //Scanlab XLSCAN 솔류션

            float fov = 60.0f;    // scanner field of view : 60mm            
            float kfactor = (float)Math.Pow(2, 20) / fov; // k factor (bits/mm) = 2^20 / fov
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);    // 스캐너 보정 파일 지정 : correction file
            rtc.CtlFrequency(50 * 1000, 2); // laser frequency : 50KHz, pulse width : 2usec
            rtc.CtlSpeed(100, 100); // default jump and mark speed : 100mm/s
            rtc.CtlDelay(10, 100, 200, 200, 0); // scanner and laser delays

            if (rtc is IRtcMOTF rtcMOTF)
            {
                //엔코더 스케일 설정 
                //단위 mm 이동시 발생되는 엔코더 펄스의 수
                rtcMOTF.EncXCountsPerMm = 2000;  
                rtcMOTF.EncYCountsPerMm = 2000;
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
                rtcMOTF.CtlMotfGetEncoder(out int encX, out int encY, out float encXmm, out float encYmm);
                lblEncXCnt.Text = $"{encX} cnt";
                lblEncYCnt.Text = $"{encY} cnt";
                lblEncXmm.Text = $"{encXmm:F3} mm";
                lblEncYmm.Text = $"{encYmm:F3} mm";
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
            var filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "recipes", "motf.sirius");
            var doc = DocumentSerializer.OpenSirius(filename);

            siriusEditorForm1.Document = doc;

            string message =
                @"
1. 마커 창을 실행 (F1)
2. (외부 엔코더 입력 결선이 준비되지 않은 경우) MOTF 탭으로 이동 : X 엔코더 시뮬레이션 속도 50 mm/s 설정
3. 옵션 탭으로 이동 : 스트 버퍼 모드 Single 설정
4. 오퍼레이션 탭으로 이동 : 가공 시작 (라이센스키에 MOTF 옵션 필요)
5. 엔코더 리셋 -> 100mm 위치에서 원 가공 시작 -> 200mm 위치대기 를 반복함
                ";

            MessageBox.Show(message, "How to start");

            //popup "marker" window (F1)
            //마커 창을 실행 (F1)

            //switch tab to "marking on the fly"
            // MOTF 탭으로 이동

            //input encoder simulation x speed : 50 mm/s and set to start
            // 외부 엔코더 입력 결선이 준비되지 않은 경우에 한함
            // X 엔코더 시뮬레이션 속도 입력 : 50 mm/s 후 set 실행
            // 주의) 라이센스키에 MOTF 옵션 필요

            // switch table to "option "
            // 옵션 탭으로 이동
            // list buffer handling to "single"
            // 리스트 버퍼 모드 단일 로 설정

            // switch tab to "operation"
            // 오퍼레이션 탭으로 이동

            // press "start" button
            // 가공 시작 버튼 클릭

            // marking process
            // 실행 과정
            // mark circle every 300mm interval 
            // 엔코더가 100mm 위치에 도달하면 원 형상을 가공하고 200mm 위치에 도달하면 이를 다시 반복
            // -> 매 300mm 간격으로 원 모양 가공됨
            //
            // 1. motf begin with encoder reset : MOTF 시작 및 입력 엔코더 리셋
            // 2. wait encoder x until position has over 100 mm : X 엔코더 위치가 100 mm 가 넘을때 까지 대기
            // 3. mark circle : 원 가공 형상
            // 4. motf end : MOTF 끝
            // 5. wait encoder x until position has over 200 mm : X 엔코더 위치가 200 mm 가 넘을때 까지 대기
            // 6. jump to start of list buffer to repeat : 처음 부터 반복
        }
    }
}
