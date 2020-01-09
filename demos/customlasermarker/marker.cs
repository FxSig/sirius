using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpiralLab.Sirius;

namespace SpiralLab.Sirius
{
    /// <summary>
    /// 사용자 정의 커스텀 마커
    /// </summary>
    public class YourCustomMarker: IMarker
    {
        /// <summary>
        /// 진행 이벤트 핸들러
        /// </summary>
        public event MarkerProgressEventHandler OnProgress;
        /// <summary>
        /// 가공 완료 이벤트 핸들러
        /// </summary>
        public event MarkerFinishedEventHandler OnFinished;
        /// <summary>
        /// 식별 번호
        /// </summary>
        public uint Index { get; set; }
        /// <summary>
        /// 이름
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 가공에 걸린 시간
        /// 시간 / 오프셋 개수 = 오프셋 하나당 가공에 걸린 시간
        /// </summary>
        public TimeSpan ElaspedTime { get; set; }
        /// <summary>
        /// 지정된 Rtc
        /// </summary>
        public IRtc Rtc { get; set; }
        /// <summary>
        /// 지정된 Laser
        /// </summary>
        public ILaser Laser { get; set; }
        /// <summary>
        /// 지정된 Motion (현재 미사용)
        /// </summary>
        public IMotion Motion { get; set; }

        /// <summary>
        /// 가공 준비 완료 여부
        /// </summary>
        public bool IsReady
        {
            get
            {
                if (null == this.clonedDoc)
                    return false;
                if (0 == this.clonedDoc.Layers.Count)
                    return false;
                if (!this.Rtc.CtlGetStatus(RtcStatus.NoError))
                    return false;
                if (this.Laser.IsError)
                    return false;
                if (this.IsBusy)
                    return false;
                return true;
            }
        }
        /// <summary>
        /// 가공중 여부
        /// </summary>
        public bool IsBusy
        {
            get {
                return 
                    Rtc.CtlGetStatus(RtcStatus.Busy) ||
                    Laser.IsBusy ||
                    (this.thread != null) ? this.thread.IsAlive : false;
            }
        }
        /// <summary>
        /// 에러 발생 여부
        /// </summary>
        public bool IsError
        {
            get
            {
                return 
                    !Rtc.CtlGetStatus(RtcStatus.NoError) || 
                    Laser.IsError;
            }
        }
        /// <summary>
        /// 가공 완료 여부
        /// </summary>
        public bool IsFinished { get; set; }

        /// <summary>
        /// 복제된 문서 객체
        /// </summary>
        private IDocument clonedDoc;

        /// <summary>
        /// x,y,angle 에 대한 오프셋 배열 정보
        /// </summary>
        public List<Offset> Offsets
        {
            get { return this.offsets; }
            set { this.offsets = value; }
        }
        private List<Offset> offsets;
        /// <summary>
        /// 장착된 스캐너가 회전되어 있는 경우 해당 최종 마킹시에는 해당 각도 만큼 회전되어 가공해야 하므로 이를 설정
        /// 예 : 90, 180, 270 , ...
        /// </summary>
        public float ScannerRotateAngle { get; set; }
        /// <summary>
        /// 마커의 상태나 사용자 인터페이스가 연결된 윈폼
        /// </summary>
        public Form Form { get; set; }
        /// <summary>
        /// 부가 정보
        /// </summary>
        public object Tag { get; set; }

        private Stopwatch timer;
        private Thread thread;

        public YourCustomMarker(uint index)
        {
            this.Index = index;
            this.ElaspedTime = TimeSpan.Zero;
            this.offsets = new List<Offset>();
            this.Form = new YourMarkerForm(this); /// 사용자가 직접 디자인한 폼을 만들어 삽입
        }

        /// <summary>
        /// 마커는 내부 쓰레드에 의해 가공 데이타를 처리하게 되는데, 이때 가공 데이타(IDocument)에 
        /// 크로스 쓰레드 상태가 될수 있으므로, 준비(Prepare)시에는 가공 데이타를 모두 복제(Clone) 하여 가공시
        /// 데이타에 대한 쓰레드 안전 접근을 처리하게 된다. 또한 가공중 뷰에 의해 원본 데이타가 조작, 수정되더라도 
        /// 준비(Ready) 즉 신규 데이타를 다운로드하지 않으면 아무런 영향이 없게 된다.
        /// </summary>
        /// <param name="doc">가공 데이타가 있는 문서 객체</param>
        /// <returns></returns>
        public bool Ready(IDocument doc, IRtc rtc, ILaser laser, IMotion motion=null)
        {
            if (doc == null || rtc == null || laser == null)
                return false;

            this.Rtc = rtc;
            this.Laser = laser;
            this.Motion = motion;
            ///모든 문서 데이타 복제 (마커의 내부 가공 쓰레드가 직접 접근하면 cross-thread 상태가 되므로 복제 실시)
            this.clonedDoc = (IDocument)doc.Clone();
            Debug.Assert(clonedDoc != null);
            this.OnProgress?.Invoke(this, 0);
            return true;
        }

        public bool Start(object args=null)
        {
            if (Rtc.CtlGetStatus(RtcStatus.Busy))
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}]: busy now !");
                return false;
            }
            if (!Rtc.CtlGetStatus(RtcStatus.PowerOK))
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}]: rtc scanner supply power is not ok !");
                return false;
            }
            if (!Rtc.CtlGetStatus(RtcStatus.PositionAckOK))
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}]: rtc scanner position ack is not ok !");
                return false;
            }
            if (!Rtc.CtlGetStatus(RtcStatus.NoError))
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}]: rtc has a internal problem !");
                return false;
            }
            if (Laser.IsError)
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}]: laser source has a error status !");
                return false;
            }
            if (null != this.thread && this.thread.IsAlive)
            {
                return false;
            }
            if (this.offsets.Count <= 0)
            {
                this.offsets.Add(Offset.Zero);
                Logger.Log(Logger.Type.Warn, $"marker [{this.Index}]: no offset information ...");
            }
            if (null == this.clonedDoc || 0 == this.clonedDoc.Layers.Count)
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}]: document doesn't has any layers");
                return false;
            }

            timer = Stopwatch.StartNew();
            this.OnProgress?.Invoke(this, 0);
            this.IsFinished = false;
            this.thread = new Thread(this.WorkerThread);
            this.thread.Name = $"Marker: {this.Name}";
            this.thread.Priority = ThreadPriority.AboveNormal;
            this.thread.Start();
            return true;
        }

        public bool Stop()
        {
            Logger.Log(Logger.Type.Warn, $"marker [{this.Index}]: trying to stop ...");
            Rtc.CtlAbort();
            Rtc.CtlLaserOff();
            Motion?.StopAll();
            if (this.thread != null)
            {
                if (!this.thread.Join(2*1000))
                {
                    Logger.Log(Logger.Type.Error, $"marker [{this.Index}]: fail to join the thread");
                }
                this.thread = null;
                return true;
            }
            return false;
        }

        public bool Reset()
        {
            ///RTC및 레이저 소스의 에러 해제 시도
            this.Rtc.CtlReset();
            this.Laser?.CtlReset();
            this.Motion?.ResetAll();
            return true;
        }

        #region 쓰레드 작업
        private void WorkerThread()
        {
            var matrixStack = (MatrixStack)this.Rtc.MatrixStack.Clone();
            this.Rtc.MatrixStack.LoadIdentity();
            bool success =
                PreWork() &&
                MainWork() &&
                PostWork();
            this.Rtc.MatrixStack = matrixStack;
            if (!success)
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}]: aborted or fail to mark");
            }
        }
        private bool PreWork()
        {
            /// 가공을 위한 RTC 버퍼 초기화
            Rtc.ListBegin(this.Laser);
            return true;
        }
        private bool MainWork()
        {
            bool success = true;
            float progress = 0;
            ///지정된 오프셋 개수 만큼 가공
            for (int i = 0; i < this.offsets.Count; i++)
            {
                var xyt = offsets[i];
                /// 문서에 설정된 Dimension 정보 처리 : 회전중심, 회전량, 원점 중심 등
                var matrix =
                    Matrix3x2.CreateRotation((float)(this.ScannerRotateAngle * Math.PI / 180.0)) *   ///7. 스캐너 회전량 적용
                    Matrix3x2.CreateTranslation(xyt.X, xyt.Y) * /// 6. 오프셋 이동량
                    Matrix3x2.CreateRotation((float)(xyt.Angle * Math.PI / 180.0)) *  /// 5. 오프셋 회전량
                    Matrix3x2.CreateTranslation(Vector2.Negate(clonedDoc.Dimension.Center)) * ///4. 문서의 원점 위치를 이동
                    Matrix3x2.CreateTranslation(clonedDoc.RotateOffset.X, clonedDoc.RotateOffset.X) * ///3. 회전 중심 위치 원복
                    Matrix3x2.CreateRotation((float)(clonedDoc.RotateOffset.Angle * Math.PI / 180.0)) *  ///2. 문서에 설정된 회전량 적용
                    Matrix3x2.CreateTranslation(-clonedDoc.RotateOffset.X, -clonedDoc.RotateOffset.X);  ///1. 회전을 위해 회점 중심을 원점으로 이동
                this.Rtc.MatrixStack.Push(matrix);
                ///문서의 레이어 순회
                foreach (var layer in this.clonedDoc.Layers)   
                {
                    /// 레이어의 가공 여부 플레그 확인
                    if (layer.IsMarkerable)
                    {
                        ///레이어에 있는 모든 개체 순회
                        foreach (var entity in layer)   
                        {
                            var markerable = entity as IMarkerable;
                            /// 개체가 레이저 가공이 가능한지 여부 판단
                            if (null != markerable)
                                success &= markerable.Mark(this.Rtc, this.Laser);
                            if (!success)
                                break;
                            /// 진행률 이벤트 (progress : 가 0~100 의 범위로 계산되도록 개선 필요)
                            this.OnProgress?.Invoke(this, progress++);
                        }
                    }
                    if (!success)
                        break;
                }
                ///위에서 Push 된 행렬스택에서 Pop 하여 초기 행렬스택 상태가 되도록으로 처리
                Rtc.MatrixStack.Pop();
                if (!success)
                    break;
            }
            return success;
        }
        private bool PostWork()
        {
            Rtc.ListEnd();
            if (!Rtc.CtlGetStatus(RtcStatus.Aborted))
                Rtc.ListExecute(true);
            /// 가공완료
            timer.Stop();
            this.ElaspedTime = timer.Elapsed;
            this.IsFinished = true;
            this.OnProgress?.Invoke(this, 100);
            this.OnFinished?.Invoke(this, this.ElaspedTime);
            Logger.Log(Logger.Type.Info, $"marker [{this.Index}]: job finished. time= {this.ElaspedTime.TotalSeconds:F3}s");
            return true;
        }
        #endregion
    }
}
