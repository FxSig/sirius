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
    public class YourCustomMarker : IMarker
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
        public object Tag { get; set; }
        public virtual bool IsReady
        {
            get
            {
                if (null == this.clonedDoc)
                    return false;
                if (0 == this.clonedDoc.Layers.Count)
                    return false;
                if (null == this.MarkerArg)
                    return false;
                if (!this.MarkerArg.Rtc.CtlGetStatus(RtcStatus.NoError))
                    return false;
                if (this.MarkerArg.Laser.IsError)
                    return false;
                if (this.IsBusy)
                    return false;
                return true;
            }
        }
        public virtual bool IsBusy
        {
            get
            {
                bool busy = false;
                if (null != this.MarkerArg && null != this.MarkerArg.Rtc)
                    busy |= this.MarkerArg.Rtc.CtlGetStatus(RtcStatus.Busy);
                if (null != this.MarkerArg && null != this.MarkerArg.Laser)
                    busy |= this.MarkerArg.Laser.IsBusy;
                if (null != this.thread)
                    busy |= this.thread.IsAlive;
                return busy;
            }
        }
        public virtual bool IsError
        {
            get
            {
                bool error = false;
                if (null != this.MarkerArg && null != this.MarkerArg.Rtc)
                    error |= !this.MarkerArg.Rtc.CtlGetStatus(RtcStatus.NoError);
                if (null != this.MarkerArg && null != this.MarkerArg.Laser)
                    error |= this.MarkerArg.Laser.IsError;
                return error;
            }
        }
        public virtual bool IsFinished { get; set; }

        public IMarkerArg MarkerArg { get; private set; }

        protected IDocument clonedDoc;
        protected Thread thread;

        public YourCustomMarker(uint index)
        {
            this.Index = index;
        }

        /// <summary>
        /// 마커는 내부 쓰레드에 의해 가공 데이타를 처리하게 되는데, 이때 가공 데이타(IDocument)에 
        /// 크로스 쓰레드 상태가 될수 있으므로, 준비(Prepare)시에는 가공 데이타를 모두 복제(Clone) 하여 가공시
        /// 데이타에 대한 쓰레드 안전 접근을 처리하게 된다. 또한 가공중 뷰에 의해 원본 데이타가 조작, 수정되더라도 
        /// 준비(Ready) 즉 신규 데이타를 다운로드하지 않으면 아무런 영향이 없게 된다.
        /// </summary>
        /// <param name="markerArg">가공 인자</param>
        /// <returns></returns>
        public virtual bool Ready(IMarkerArg markerArg)
        {
            if (null == markerArg)
                return false;
            if (null == markerArg.Document || null == markerArg.Rtc || null == markerArg.Laser)
                return false;

            this.MarkerArg = markerArg;
            this.clonedDoc = (IDocument)this.MarkerArg.Document.Clone();
            Debug.Assert(clonedDoc != null);
            var rtc = this.MarkerArg.Rtc;

            RtcCharacterSetHelper.Clear(rtc);
            // 재등록
            bool success = true;
            for (int i = 0; i < this.clonedDoc.Layers.Count; i++)
            {
                var layer = this.clonedDoc.Layers[i];
                if (layer.IsMarkerable)
                {
                    foreach (var entity in layer)
                    {
                        var siriusText = entity as SiriusText;
                        if (null != siriusText)
                            success &= siriusText.RegisterCharacterSetIntoRtc(rtc);
                        var text = entity as Text;
                        if (null != text)
                            success &= siriusText.RegisterCharacterSetIntoRtc(rtc);
                    }
                }
            }
            if (!success)
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}]: fail to register character into rtc");

            this.OnProgress?.Invoke(this, 0);
            return true;
        }

        public bool Start()
        {
            if (null == this.MarkerArg || null == this.MarkerArg.Rtc || null == this.MarkerArg.Laser)
                return false;
            var rtc = this.MarkerArg.Rtc;
            var laser = this.MarkerArg.Laser;

            if (rtc.CtlGetStatus(RtcStatus.Busy))
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}]: busy now !");
                return false;
            }
            if (!rtc.CtlGetStatus(RtcStatus.PowerOK))
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}]: rtc scanner supply power is not ok !");
                return false;
            }
            if (!rtc.CtlGetStatus(RtcStatus.PositionAckOK))
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}]: rtc scanner position ack is not ok !");
                return false;
            }
            if (!rtc.CtlGetStatus(RtcStatus.NoError))
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}]: rtc has a internal problem !");
                return false;
            }
            if (laser.IsError)
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}]: laser source has a error status !");
                return false;
            }
            if (null != this.thread && this.thread.IsAlive)
            {
                return false;
            }
            if (this.MarkerArg.Offsets.Count <= 0)
            {
                this.MarkerArg.Offsets.Add(Offset.Zero);
                Logger.Log(Logger.Type.Warn, $"marker [{this.Index}]: no offset information ...");
            }
            if (null == this.clonedDoc || 0 == this.clonedDoc.Layers.Count)
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}]: document doesn't has any layers");
                return false;
            }

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
            if (null == this.MarkerArg)
                return false;

            Logger.Log(Logger.Type.Warn, $"marker [{this.Index}]: trying to stop ...");
            this.MarkerArg.Rtc?.CtlAbort();
            this.MarkerArg.Rtc?.CtlLaserOff();
            if (this.thread != null)
            {
                if (!this.thread.Join(2 * 1000))
                {
                    Logger.Log(Logger.Type.Error, $"marker [{this.Index}]: fail to join with thread");
                }
                this.thread = null;
                return true;
            }
            return false;
        }

        public bool Reset()
        {
            if (null == this.MarkerArg)
                return false;
            this.MarkerArg.Rtc?.CtlReset();
            this.MarkerArg.Laser?.CtlReset();
            return true;
        }

        #region 쓰레드 작업
        private void WorkerThread()
        {
            MarkerArg.StartTime = DateTime.Now;
            var rtc = this.MarkerArg.Rtc;
            var oldMatrix = (IMatrixStack)rtc.MatrixStack.Clone();
            rtc.MatrixStack.Clear();
            bool success =
                PreWork() &&
                MainWork() &&
                PostWork();
            rtc.MatrixStack = oldMatrix;
            if (!success)
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}]: aborted or fail to mark");
            MarkerArg.EndTime = DateTime.Now;

            if (this.IsFinished)
            {
                var timeSpan = MarkerArg.EndTime - MarkerArg.StartTime;
                this.OnFinished?.Invoke(this, timeSpan);
                Logger.Log(Logger.Type.Info, $"marker [{this.Index}]: job finished. time= {timeSpan.TotalSeconds:F3}s");
            }
        }
        private bool PreWork()
        {
            // 가공을 위한 RTC 버퍼 초기화
            var rtc = this.MarkerArg.Rtc;
            var laser = this.MarkerArg.Laser;
            rtc.ListBegin(laser);
            return true;
        }
        private bool MainWork()
        {
            bool success = true;
            var rtc = this.MarkerArg.Rtc;
            var laser = this.MarkerArg.Laser;
            var offsets = this.MarkerArg.Offsets;
            var scannerRotateAngle = this.MarkerArg.ScannerRotateAngle;
            int totalCounts = offsets.Count * this.clonedDoc.Layers.Count;
            float progress = 0;
            //지정된 오프셋 개수 만큼 가공
            for (int i = 0; i < offsets.Count; i++)
            {
                var xyt = offsets[i];
                // 문서에 설정된 Dimension 정보 처리 : 회전중심, 회전량, 원점 중심 등
                rtc.MatrixStack.Push(scannerRotateAngle); // 스캐너의 기구적 회전량
                rtc.MatrixStack.Push(xyt.X, xyt.Y); // 오프셋 이동량
                rtc.MatrixStack.Push(xyt.Angle);  // 오프셋 회전량
                rtc.MatrixStack.Push(clonedDoc.RotateOffset.X, clonedDoc.RotateOffset.Y, clonedDoc.RotateOffset.Angle);  // 회전을 위해 회점 중심을 원점으로 이동);

                //문서의 레이어 순회
                foreach (var layer in this.clonedDoc.Layers)   
                {
                    // 레이어의 가공 여부 플레그 확인
                    if (layer.IsMarkerable)
                    {
                        //레이어에 있는 모든 개체 순회
                        foreach (var entity in layer)   
                        {
                            var markerable = entity as IMarkerable;
                            // 개체가 레이저 가공이 가능한지 여부 판단
                            if (null != markerable)
                                success &= markerable.Mark(this.MarkerArg);
                            if (!success)
                                break;
                            // 진행률 이벤트 (progress : 가 0~100 의 범위로 계산되도록 개선 필요)
                            this.OnProgress?.Invoke(this, progress++);
                        }
                    }
                    if (!success)
                        break;
                }
                //위에서 Push 된 행렬스택에서 Pop 하여 초기 행렬스택 상태가 되도록으로 처리
                rtc.MatrixStack.Pop();
                rtc.MatrixStack.Pop();
                rtc.MatrixStack.Pop();
                rtc.MatrixStack.Pop();
                if (!success)
                    break;
            }
            return success;
        }
        private bool PostWork()
        {
            var rtc = this.MarkerArg.Rtc;
            rtc.ListEnd();
            if (!rtc.CtlGetStatus(RtcStatus.Aborted))
                rtc.ListExecute(true);
            // 가공완료
            this.IsFinished = true;
            this.OnProgress?.Invoke(this, 100);
            return true;
        }
        #endregion
    }
}
