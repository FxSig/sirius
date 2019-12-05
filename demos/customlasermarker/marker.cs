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

    public class YourMarker: IMarker
    {
        public event MarkerProgressEventHandler OnProgress;
        public event MarkerFinishedEventHandler OnFinished;
        public uint Index { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// 가공에 걸린 시간
        /// 시간 / 오프셋 개수 = 오프셋 하나당 가공에 걸린 시간
        /// </summary>
        public TimeSpan ElaspedTime { get; private set; }

        public IRtc Rtc { get; private set; }
        public ILaser Laser { get; private set; }
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
                if (!this.IsBusy)
                    return false;
                return true;
            }
        }
        public bool IsBusy
        {
            get {
                return 
                    Rtc.CtlGetStatus(RtcStatus.Busy) ||
                    Laser.IsBusy ||
                    (this.thread != null) ? this.thread.IsAlive : false;
            }
        }
        public bool IsError
        {
            get
            {
                return !Rtc.CtlGetStatus(RtcStatus.NoError) || Laser.IsError;
            }
        }
        /// <summary>
        /// 가공 완료 여부
        /// </summary>
        public bool IsFinished { get; set; }
        private IDocument clonedDoc;


        /// <summary>
        /// x,y,angle 에 대한 오프셋 배열 정보
        /// </summary>
        public List<(double dx, double dy, double angle)> Offsets
        {
            get { return this.offsets; }
            set { this.offsets = value; }
        }
        private List<(double dx, double dy, double angle)> offsets;

        public Form Form { get; set; }

        private Stopwatch timer;
        private Thread thread;

        public YourMarker(uint index)
        {
            this.Index = index;
            this.ElaspedTime = TimeSpan.Zero;
            this.offsets = new List<(double dx, double dy, double angle)>();
            this.Form = null; /// 윈폼을 만들어 삽입
        }

        /// <summary>
        /// 마커는 내부 쓰레드에 의해 가공 데이타를 처리하게 되는데, 이때 가공 데이타(IDocument)에 
        /// 크로스 쓰레드 상태가 될수 있으므로, 준비(Prepare)시에는 가공 데이타를 모두 복제(Clone) 하여 가공시
        /// 데이타에 대한 쓰레드 안전 접근을 처리하게 된다. 또한 가공중 뷰에 의해 원본 데이타가 조작, 수정되더라도 
        /// 준비(Ready) 즉 신규 데이타를 다운로드하지 않으면 아무런 영향이 없게 된다.
        /// </summary>
        /// <param name="doc">가공 데이타가 있는 문서 객체</param>
        /// <returns></returns>
        public bool Ready(IDocument doc, IRtc rtc, ILaser laser)
        {
            if (doc == null || rtc == null || laser == null)
                return false;

            this.Rtc = rtc;
            this.Laser = laser;
            this.clonedDoc = (IDocument)doc.Clone();
            Debug.Assert(clonedDoc != null);
            this.OnProgress?.Invoke(this, 0);
            return true;
        }

        public bool Start()
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
                this.offsets.Add((0, 0, 0));
                Logger.Log(Logger.Type.Warn, $"marker [{this.Index}]: no offset information ...");
            }
            if (null == this.clonedDoc || 0 == this.clonedDoc.Layers.Count)
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}]: document doesn't has any layers");
                return false;
            }

            Rtc.CtlReset();
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
            this.Laser.CtlReset();
            return true;
        }

        #region 쓰레드 작업
        public void WorkerThread()
        {
            var matrixStack = (MatrixStack)this.Rtc.MatrixStack.Clone();
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
                ///오프셋 정보를 행렬로 변환하여 RTC 행렬 스택에 Push
                var xyt = offsets[i];
                Rtc.MatrixStack.Push(xyt.dx, xyt.dy, xyt.angle);

                var matrix = Matrix3x2.CreateTranslation(clonedDoc.RotateOrigin) * ///4. 원점으로 이동된 회전 위치를 다시 복원
                    Matrix3x2.CreateRotation(clonedDoc.RotateAngle) *  ///3. 문서에 설정된 회전량 적용
                    Matrix3x2.CreateTranslation(Vector2.Negate(clonedDoc.RotateOrigin)) *  ///2. 회전을 위해 회점 중심을 원점으로 이동
                    Matrix3x2.CreateTranslation(Vector2.Negate(clonedDoc.Origin));   ///1. 문서의 원점 위치를 이동
                Rtc.MatrixStack.Push(matrix);

                foreach (var layer in this.clonedDoc.Layers)    ///레이어 순회
                {
                    if (layer.IsMarkerable)
                    {
                        foreach (var entity in layer)   ///레이어에 있는 각 엔티티 순회
                        {
                            var markerable = entity as IMarkerable;
                            if (null != markerable)
                                success &= markerable.Mark(this.Rtc, this.Laser);
                            if (!success)
                                break;
                            this.OnProgress?.Invoke(this, progress++);
                        }
                    }
                    if (!success)
                        break;
                }
                ///위에서 Push 된 행렬스택에서 반드시 Pop 하여야 한다 !
                Rtc.MatrixStack.Pop();
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
