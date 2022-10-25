using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpiralLab.Sirius;

namespace SpiralLab.Sirius
{
    /// <summary>
    /// 4개의 레이저 /RTC 제어기를 사용
    /// 레시피에 텍스트, 바코드 개체를 만들고
    /// 가공 데이타를 실시간으로 변환하며, 지정된 오프셋 위치에 가공한다
    /// </summary>
    public partial class Form1 : Form
    {

        /// <summary>
        /// 4개의 에디터 윈폼
        /// </summary>
        public SiriusEditorForm[] SiriusEditor { get; set; }

        /// <summary>
        /// 스캐너 필드 크기 (mm)
        /// </summary>
        public float[] ScannerFieldSize { get; set; }

        /// <summary>
        /// 스캐너 보정 파일
        /// </summary>
        public string[] ScannerCorrectionFile { get; set; }

        /// <summary>
        /// 레이저 소스 최대 출력 (Watt)
        /// </summary>
        public float[] LaserMaxPowerWatt { get; set; }

        /// <summary>
        /// RTC 카드 목록
        /// </summary>
        public IRtc[] Rtc { get; set; }

        /// <summary>
        /// 레이저 소스 목록
        /// </summary>
        public ILaser[] Laser { get; set; }

        /// <summary>
        /// 마커 목록
        /// </summary>
        public IMarker[] Marker { get; set; }

        public Form1()
        {
            InitializeComponent();


            SiriusEditor = new SpiralLab.Sirius.SiriusEditorForm[4]
            {
                siriusEditorForm1,
                siriusEditorForm2,
                siriusEditorForm3,
                siriusEditorForm4,
            };
          
            ScannerFieldSize = new float[4]
            {
                100,
                100,
                100,
                100
            };

            ScannerCorrectionFile = new string[4]
            {
                @"correction\Cor_1to1.ct5",
                @"correction\Cor_1to1.ct5",
                @"correction\Cor_1to1.ct5",
                @"correction\Cor_1to1.ct5",
            };

            LaserMaxPowerWatt = new float[4]
            {
                5,
                5,
                5,
                5
            };

            Rtc = new IRtc[4];
            Laser = new ILaser[4];
            Marker = new IMarker[4];

            this.Load += Form1_Load;
            this.FormClosed += Form1_FormClosed;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bool success = true;

            // 시리우스 라이브러리 초기화
            // 이 경로에 로그 설정 파일 필요 : logs\\NLogSpiralLab.config
            Core.Initialize();

            for (int i = 0; i < 4; i++)
            {
                // 빈 문서 객체 지정
                var doc = new DocumentDefault();
                SiriusEditor[i].Document = doc;

                #region RTC 초기화
                // RTC 카드 생성
                var rtc = new Rtc5(i);
                // K-Factor (bits/mm) = 2^20 / ScannerFieldSize
                float kfactor = (float)Math.Pow(2, 20) / ScannerFieldSize[i];
                // 스캐너 보정파일 경로
                var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ScannerCorrectionFile[i]);
                // 출력 핀 레벨 설정
                rtc.InitLaser12SignalLevel = RtcSignalLevel.ActiveHigh;
                rtc.InitLaserOnSignalLevel = RtcSignalLevel.ActiveHigh;

                // RTC 초기화
                success &= rtc.Initialize(kfactor, LaserMode.Yag5, correctionFile);
                // 기본 주파수 : 50KHz, 펄스폭: 2usec 
                success &= rtc.CtlFrequency(50 * 1000, 2);
                // 기본 점프, 마크 속도
                success &= rtc.CtlSpeed(500, 500);
                // 기본 스캐너 및 레이저 지연시간
                success &= rtc.CtlDelay(10, 100, 200, 200, 0);

                Rtc[i] = rtc;
                #endregion

                #region 레이저 소스 생성
                //가상의 레이저 소스 생성
                var laser = new MyLaser(i, "mylaser", LaserMaxPowerWatt[i]);
                // 레이저 소스와 RTC 카드 연결
                laser.Rtc = rtc;
                // 레이저 소스 초기화
                laser.Initialize();
                // 기본 출력 설정 (10%)
                laser.CtlPower(LaserMaxPowerWatt[i] * 0.1f);

                Laser[i] = laser;
                #endregion

                #region 마커 생성
                // 마커 생성
                var marker = new CustomMarker(i, $"MyMarker{i}");
                marker.OnStarted += Marker_OnStarted;
                marker.OnProgress += Marker_OnProgress;
                marker.OnFailed += Marker_OnFailed;
                marker.OnFinished += Marker_OnFinished;                

                Marker[i] = marker;
                #endregion

                // 편집기에 식별자 설정 (0,1,2,...)
                SiriusEditor[i].Index = (uint)i;
                // 에디터에 RTC 카드 연결
                SiriusEditor[i].Rtc = rtc;
                // 에디터에 레이저 소스연결
                SiriusEditor[i].Laser = laser;
                // 에디터에 마커 연결
                SiriusEditor[i].Marker = marker;
                // 스캐너 보정 선택시 사용자 정의 윈폼 처리
                SiriusEditor[i].OnCorrection2D += SiriusEditor_OnCorrection2D;
            }

            cbbIndex.SelectedIndex = 0;
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                SiriusEditor[i].OnCorrection2D -= SiriusEditor_OnCorrection2D;

                Marker[i].OnStarted -= Marker_OnStarted;
                Marker[i].OnFailed -= Marker_OnFailed;
                Marker[i].OnFinished -= Marker_OnFinished;
                //disposing
                
                Marker[i].Stop();
                Rtc[i].Dispose();
                Laser[i].Dispose();
            }
        }
        /// <summary>
        /// 가공 시작 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="markerArg"></param>
        private void Marker_OnStarted(IMarker sender, IMarkerArg markerArg)
        {
            var index = sender.Index;
            Logger.Log(Logger.Type.Info, $"[{index}]:{sender.Name} marker has started");
        }
        /// <summary>
        /// 가공 실패 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="markerArg"></param>
        private void Marker_OnFailed(IMarker sender, IMarkerArg markerArg)
        {
            var index = sender.Index;
            Logger.Log(Logger.Type.Error, $"[{index}]:{sender.Name} marker has failed");
        }
        /// <summary>
        /// 가공 진행 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="markerArg"></param>
        private void Marker_OnProgress(IMarker sender, IMarkerArg markerArg)
        {
            var index = sender.Index;
            Logger.Log(Logger.Type.Info, $"[{index}]:{sender.Name} marker has {markerArg.Progress} progressing...");
        }
        /// <summary>
        /// 가공 완료 이벤트 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="markerArg"></param>
        private void Marker_OnFinished(IMarker sender, IMarkerArg markerArg)
        {
            var index = sender.Index;
            Logger.Log(Logger.Type.Info, $"[{index}]:{sender.Name} marker has finished");
        }
        /// <summary>
        /// 레시피 변경
        /// </summary>
        /// <param name="index"></param>
        /// <param name="recipeFileName"></param>
        /// <returns></returns>
        public bool RecipeChange(int index, string recipeFileName)
        {
            if (this.IsBusy(index))
            {
                Logger.Log(Logger.Type.Info, $"[{index}]: system is busy. fail to change target recipe");
                return false;
            }
            try
            {
                var doc = DocumentSerializer.OpenSirius(recipeFileName);
                SiriusEditor[index].Document = doc;
                //if you want to download recipe file by automatically
                bool success = this.Ready(index);
                if (success)
                    Logger.Log(Logger.Type.Info, $"[{index}]: success to change target recipe by {recipeFileName}");
                else
                    Logger.Log(Logger.Type.Error, $"[{index}]: system is busy. fail to change target recipe");
                return success;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 가공 데이타가 다운로드 된다
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool Ready(int index)
        {
            if (this.IsBusy(index))
                return false;
            var arg = new MarkerArgDefault()
            {
                 Document = SiriusEditor[index].Document,
                 Rtc = Rtc[index],
                 Laser = Laser[index],
                 RtcListType = ListType.Auto,
                 MarkTargets = MarkTargets.All,
            };
            arg.ViewTargets.Add(SiriusEditor[index].View);
            arg.Offsets.Clear();
            return Marker[index].Ready(arg);
        }
        /// <summary>
        /// 가공 시작
        /// </summary>
        /// <param name="index"></param>
        /// <param name="xyts"></param>
        /// <returns></returns>
        public bool Start(int index, List<CustomMarkArg> infos)
        {
            if (this.IsBusy(index))
                return false;

            var doc = SiriusEditor[index].Document;
            // 문서에서 대상 개체 검색
            var bcdEntity = doc.Layers.NameOf(CustomMarkArg.BarcodeEntityName, out var layer1);
            var textEntity = doc.Layers.NameOf(CustomMarkArg.TextEntityName, out var layer2);
            if (null == bcdEntity || null == textEntity)
            {
                // 변경이 필요한 개체 (텍스트및 바코드) 가 레시피에 없다
                // if target entities  is not exist
                Logger.Log(Logger.Type.Warn, $"[{index}]: target entities are not exist ? {CustomMarkArg.BarcodeEntityName}, {CustomMarkArg.TextEntityName}");
                // 에러로 처리시 (if error or warn)
                //return false;
            }
            var marker = Marker[index];
            marker.MarkerArg.Offsets.Clear();
            foreach (var info in infos)
            {
                var offset = new Offset(info.X, info.Y, info.Angle);
                // 사용자 데이타를 간단히 전달 처리 (userdata as tuple)
                offset.UserData = (info.BarcodeEntityData, info.TextData); 
                marker.MarkerArg.Offsets.Add(offset);
            }
            return marker.Start();
        }
        /// <summary>
        /// 가공 중지
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool Abort(int index)
        {
            var rtc = Rtc[index];
            var laser = Laser[index];

            bool success = true;
            success &= rtc.CtlAbort();
            success &= laser.CtlAbort();
            success &= Marker[index].Stop();
            return success;
        }
        /// <summary>
        /// 에러 리셋
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool Reset(int index)
        {
            var rtc = Rtc[index];
            var laser = Laser[index];

            bool success = true;
            success &= rtc.CtlReset();
            success &= laser.CtlReset();
            success &= Marker[index].Reset();
            return success;
        }
        /// <summary>
        /// 가공 준비 상태
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsReady(int index)
        {
            return Marker[index].IsReady;
        }
        /// <summary>
        /// 가공중 여부
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsBusy(int index)
        {
            var rtc = Rtc[index];
            var laser = Laser[index];

            bool busy = false;
            busy |= rtc.CtlGetStatus(RtcStatus.Busy);
            busy |= laser.IsBusy;
            busy |= Marker[index].IsBusy;
            return busy;
        }
        /// <summary>
        /// 에러 여부
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsError(int index)
        {
            var rtc = Rtc[index];
            var laser = Laser[index];

            bool error = false;
            error |= !rtc.CtlGetStatus(RtcStatus.NoError);
            error |= laser.IsError;
            error |= Marker[index].IsError;
            return error;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            int index = cbbIndex.SelectedIndex;
            var filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "recipes", "bcd_text.sirius");
            this.RecipeChange(index, filename);
        }

        private void btnReady_Click(object sender, EventArgs e)
        {
            int index = cbbIndex.SelectedIndex;
            this.Ready(index);
        }
        private void btnTestMark_Click(object sender, EventArgs e)
        {
            int index = cbbIndex.SelectedIndex;
            if (DialogResult.Yes != MessageBox.Show($"Do you really want to start mark ?", $"[{index + 1}] Laser !!! ", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            // 4 offset with text/barcode array
            //
            //   1 (00)            2(01)
            //
            //              +
            //
            //   3 (10)            4(11)
            //

            var infos = new List<CustomMarkArg>(4);
            infos.Add(new CustomMarkArg()
            {
                X = -10,
                Y = 10,
                Angle = 0,
                BarcodeEntityData = "00",
                TextData = "00",
            });
            infos.Add(new CustomMarkArg()
            {
                X = 10,
                Y = 10,
                Angle = 0,
                BarcodeEntityData = "01",
                TextData = "01",
            });
            infos.Add(new CustomMarkArg()
            {
                X = -10,
                Y = -10,
                Angle = 0,
                BarcodeEntityData = "10",
                TextData = "10",
            });
            infos.Add(new CustomMarkArg()
            {
                X = 10,
                Y = -10,
                Angle = 0,
                BarcodeEntityData = "11",
                TextData = "11",
            });

            this.Start(index, infos);
        }


        /// <summary>
        /// 스캐너 필드 보정 화면 사용자 구현 2D
        /// </summary> 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SiriusEditor_OnCorrection2D(object sender, EventArgs e)
        {
            var editor = sender as SiriusEditorForm;
            var index = editor.Index;

            // 보정 폼 화면을 출력하기 위한 정보 준비
            int rows = 3;
            int cols = 3;
            float rowInterval = 25;
            float colInterval = 25;
            string sourceFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ScannerCorrectionFile[index]);
            string targetFile = string.Empty;
            float left = -colInterval * (float)(int)(cols / 2);
            float top = rowInterval * (float)(int)(rows / 2);
            // bits/mm
            float kfactor = (float)Math.Pow(2, 20) / ScannerFieldSize[index];

            // create correction data
            var correction2D = new Correction2DRtc(kfactor, rows, cols, rowInterval, colInterval, sourceFile, targetFile);
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    // 좌 상단 최상위 행 부터 입력
                    // 0 1 2
                    // 3 4 5
                    // 6 7 8
                    // 머신 비전 위치 측정한 dx, dy 오차값 (mm) 입력
                    correction2D.AddRelative(row, col,
                        new Vector2(left + col * colInterval, top - row * rowInterval),
                        Vector2.Zero
                        );
                }
            }
            // 보정용 윈폼에 전달 후 팝업
            var form2D = new Correction2DRtcForm(correction2D);
            form2D.Index = index;
            form2D.OnApply += Form2D_OnApply;
            form2D.OnClose += Form2D_OnClose;
            form2D.ShowDialog(this);
        }

        /// <summary>
        /// 스캐너 보정 완료후 신규 보정 파일 적용 버튼 클릭시 호출
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form2D_OnApply(object sender, EventArgs e)
        {
            var form = sender as Correction2DRtcForm;
            var index = form.Index;
            string ctFileName = form.RtcCorrection.TargetCorrectionFile;
            if (!File.Exists(ctFileName))
            {
                Logger.Log(Logger.Type.Error, $"[{index}]: try to change correction file but not exist : {ctFileName}");
                return;
            }
            if (DialogResult.Yes != MessageBox.Show($"Do you really want to apply new correction file {ctFileName} ?", "Warning !", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            var rtc = Rtc[index];
            bool success = true;
            //해당 보정파일을 RTC 제어기 메모리 안으로 로드후 선택
            success &= rtc.CtlLoadCorrectionFile(CorrectionTableIndex.Table1, ctFileName);
            success &= rtc.CtlSelectCorrection(CorrectionTableIndex.Table1);
            if (success)
            {
                // 권장) ctFileName 파일 정보를 외부 설정 파일에 저장
                ScannerCorrectionFile[index] = Path.Combine("correction", Path.GetFileName(ctFileName));
                Logger.Log(Logger.Type.Warn, $"correction file has changed to {ctFileName}");
                MessageBox.Show($"Success to load and selected field correction file to {ctFileName}", "Scanner Field Correction File", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Fail to load and selected field correction file to {ctFileName}", "Scanner Field Correction File", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        /// <summary>
        /// 스캐너 보정 폼 닫기 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form2D_OnClose(object sender, EventArgs e)
        {
            var form = sender as Correction2DRtcForm;
            var index = form.Index;
            //
        }
    }
}
