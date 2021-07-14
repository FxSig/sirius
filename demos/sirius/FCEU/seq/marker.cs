/*
 *                                                             ,--,      ,--,                              
 *              ,-.----.                                     ,---.'|   ,---.'|                              
 *    .--.--.   \    /  \     ,---,,-.----.      ,---,       |   | :   |   | :      ,---,           ,---,.  
 *   /  /    '. |   :    \ ,`--.' |\    /  \    '  .' \      :   : |   :   : |     '  .' \        ,'  .'  \ 
 *  |  :  /`. / |   |  .\ :|   :  :;   :    \  /  ;    '.    |   ' :   |   ' :    /  ;    '.    ,---.' .' | 
 *  ;  |  |--`  .   :  |: |:   |  '|   | .\ : :  :       \   ;   ; '   ;   ; '   :  :       \   |   |  |: | 
 *  |  :  ;_    |   |   \ :|   :  |.   : |: | :  |   /\   \  '   | |__ '   | |__ :  |   /\   \  :   :  :  / 
 *   \  \    `. |   : .   /'   '  ;|   |  \ : |  :  ' ;.   : |   | :.'||   | :.'||  :  ' ;.   : :   |    ;  
 *    `----.   \;   | |`-' |   |  ||   : .  / |  |  ;/  \   \'   :    ;'   :    ;|  |  ;/  \   \|   :     \ 
 *    __ \  \  ||   | ;    '   :  ;;   | |  \ '  :  | \  \ ,'|   |  ./ |   |  ./ '  :  | \  \ ,'|   |   . | 
 *   /  /`--'  /:   ' |    |   |  '|   | ;\  \|  |  '  '--'  ;   : ;   ;   : ;   |  |  '  '--'  '   :  '; | 
 *  '--'.     / :   : :    '   :  |:   ' | \.'|  :  :        |   ,/    |   ,/    |  :  :        |   |  | ;  
 *    `--'---'  |   | :    ;   |.' :   : :-'  |  | ,'        '---'     '---'     |  | ,'        |   :   /   
 *             `---'.|    '---'   |   |.'    `--''                              `--''          |   | ,'    
 *                `---`            `---'                                                        `----'   
 * Copyright(C) 2020 hong chan, choi. labspiral@gmail.com
 * Copyright (C) 2010-2020 SpiralLab. All rights reserved.
 * marker with x,y, angle offsets
 * Description : 
 * Author : hong chan, choi / labspiral@gmail.com (http://spirallab.co.kr)
 * 
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// 마커 객체 (비전 Defect 용)
    /// </summary>
    public class MarkerFCEU : IMarker
    {
        /// <summary>
        /// 진행률 이벤트 핸들러
        /// </summary>
        public event MarkerEventHandler OnProgress;
        /// <summary>
        /// 완료 이벤트 핸들러
        /// </summary>
        public event MarkerEventHandler OnFinished;
        /// <summary>
        /// 식별 번호
        /// </summary>
        public uint Index { get; set; }
        /// <summary>
        /// 이름
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 가공 준비 상태
        /// </summary>
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
        /// <summary>
        /// 출사중 여부
        /// </summary>
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
        /// <summary>
        /// 에러 여부
        /// </summary>
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
        /// <summary>
        /// 가공 완료 여부
        /// </summary>
        public virtual bool IsFinished { get; set; }
        /// <summary>
        /// 마커 시작시 전달 인자 (Ready 에 의해 업데이트 되고, Start 시 내부적으로 사용됨)
        /// </summary>
        public IMarkerArg MarkerArg { get; private set; }
        public double ScannerRotateAngle { get; set; }
        public IDocument Document { get { return this.clonedDoc; } }
        /// <summary>
        /// 사용자 정의 데이타
        /// </summary>
        public object Tag { get; set; }

        protected IDocument clonedDoc;
        protected Thread thread;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="index"></param>
        public MarkerFCEU(uint index)
        {
            this.Index = index;
            this.MarkerArg = new MarkerArgDefault();
            this.ScannerRotateAngle = 0;
        }
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="index"></param>
        public MarkerFCEU(uint index, string name)
            : this(index)
        {
            this.Name = name;
        }
        /// <summary>
        /// 마커는 내부 쓰레드에 의해 가공 데이타를 처리하게 되는데, 이때 가공 데이타(IDocument)에 
        /// 크로스 쓰레드 상태가 될수있으므로, 준비(Prepare)시에는 가공 데이타를 모두 복제(Clone) 하여 가공시
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
            //character set 모두 삭제
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
                            success &= text.RegisterCharacterSetIntoRtc(rtc);
                    }
                }
            }
            if (!success)
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: fail to register character into rtc");

            this.MarkerArg.Progress = 0;
            var receivers = this.OnProgress?.GetInvocationList();
            if (null != receivers)
                foreach (MarkerEventHandler receiver in receivers)
                    receiver.Invoke(this, this.MarkerArg);
            return true;
        }
        /// <summary>
        /// 가공 시작
        /// </summary>
        /// <returns></returns>
        public virtual bool Start()
        {
            if (null == this.MarkerArg || null == this.MarkerArg.Rtc || null == this.MarkerArg.Laser)
                return false;
            var rtc = this.MarkerArg.Rtc;
            var laser = this.MarkerArg.Laser;

            if (rtc.CtlGetStatus(RtcStatus.Busy))
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: busy now !");
                return false;
            }
            if (!rtc.CtlGetStatus(RtcStatus.PowerOK))
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: rtc scanner supply power is not ok !");
                return false;
            }
            if (!rtc.CtlGetStatus(RtcStatus.PositionAckOK))
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: rtc scanner position ack is not ok !");
                return false;
            }
            if (!rtc.CtlGetStatus(RtcStatus.NoError))
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: rtc has a internal problem !");
                return false;
            }
            if (laser.IsError)
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: laser source has a error status !");
                return false;
            }
            if (null != this.thread && this.thread.IsAlive)
            {
                return false;
            }
            if (this.MarkerArg.Offsets.Count <= 0)
            {
                this.MarkerArg.Offsets.Add(Offset.Zero);
                Logger.Log(Logger.Type.Warn, $"marker [{this.Index}] {this.Name}: no offset information ...");
            }
            if (null == this.clonedDoc || 0 == this.clonedDoc.Layers.Count)
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: document doesn't has any layers");
                return false;
            }

            this.MarkerArg.Progress = 0;
            var receivers = this.OnProgress?.GetInvocationList();
            if (null != receivers)
                foreach (MarkerEventHandler receiver in receivers)
                    receiver.Invoke(this, this.MarkerArg);

            this.IsFinished = false;
            this.thread = new Thread(this.WorkerThread);
            this.thread.Name = $"Marker: {this.Name}";
            this.thread.Priority = ThreadPriority.AboveNormal;
            this.thread.Start();
            return true;
        }
        /// <summary>
        /// 가공 강제 정지
        /// </summary>
        /// <returns></returns>
        public virtual bool Stop()
        {
            if (null == this.MarkerArg)
                return false;

            Logger.Log(Logger.Type.Warn, $"marker [{this.Index}] {this.Name}: trying to stop ...");
            this.MarkerArg.Rtc?.CtlAbort();
            this.MarkerArg.Rtc?.CtlLaserOff();
            if (this.thread != null)
            {
                if (!this.thread.Join(2 * 1000))
                {
                    Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: fail to join with thread");
                }
                this.thread = null;
                return true;
            }
            return false;
        }
        /// <summary>
        /// 리셋
        /// </summary>
        /// <returns></returns>
        public virtual bool Reset()
        {
            if (null == this.MarkerArg)
                return false;
            this.MarkerArg.Rtc?.CtlReset();
            this.MarkerArg.Laser?.CtlReset();
            return true;
        }
        #region 쓰레드 작업
        protected virtual void WorkerThread()
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
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: aborted or fail to mark");
            MarkerArg.EndTime = DateTime.Now;

            if (this.IsFinished)
            {
                var timeSpan = MarkerArg.EndTime - MarkerArg.StartTime;
                var receivers = this.OnFinished?.GetInvocationList();
                if (null != receivers)
                    foreach (MarkerEventHandler receiver in receivers)
                        receiver.Invoke(this, this.MarkerArg);

                Logger.Log(Logger.Type.Info, $"marker [{this.Index}] {this.Name}: job finished. time= {timeSpan.TotalSeconds:F3}s");
            }
        }
        protected virtual bool PreWork()
        {

            return true;
        }
        protected virtual bool MainWork()
        {
            bool success = true;
            var rtc = this.MarkerArg.Rtc;
            var rtcAlc = rtc as IRtcAutoLaserControl;
            var laser = this.MarkerArg.Laser;
            var offsets = this.MarkerArg.Offsets;
            var scannerRotateAngle = this.ScannerRotateAngle;
            int totalCounts = offsets.Count * this.clonedDoc.Layers.Count;
            for (int i = 0; i < offsets.Count; i++)
            {
                var xyt = offsets[i];
                rtc.MatrixStack.Push(scannerRotateAngle); // 3. 스캐너의 기구적 회전량
                rtc.MatrixStack.Push(xyt.X + clonedDoc.RotateOffset.X, xyt.Y + clonedDoc.RotateOffset.Y); // 2. 오프셋 이동량
                rtc.MatrixStack.Push(xyt.Angle + clonedDoc.RotateOffset.Angle);  // 1. 오프셋 회전량
                for (int j = 0; j < this.clonedDoc.Layers.Count; j++)
                {
                    var layer = this.clonedDoc.Layers[j];
                    if (!layer.IsMarkerable)
                        continue;
                    switch (layer.MotionType)
                    {
                        case MotionType.ScannerOnly:
                        case MotionType.StageOnly:
                        case MotionType.StageAndScanner:

                            if (!success) break;

                            if (null != rtcAlc)
                            {
                                rtcAlc.AutoLaserControlByPositionFileName = layer.AlcPositionFileName;
                                rtcAlc.AutoLaserControlByPositionTableNo = layer.AlcPositionTableNo;
                                switch (layer.AlcSignal)
                                {
                                    case AutoLaserControlSignal.ExtDO16:
                                    case AutoLaserControlSignal.ExtDO8Bit:
                                        success &= rtcAlc.CtlAutoLaserControl<uint>(layer.AlcSignal, layer.AlcMode, (uint)layer.AlcPercentage100, (uint)layer.AlcMinValue, (uint)layer.AlcMaxValue);
                                        break;
                                    default:
                                        success &= rtcAlc.CtlAutoLaserControl<float>(layer.AlcSignal, layer.AlcMode, (uint)layer.AlcPercentage100, (uint)layer.AlcMinValue, (uint)layer.AlcMaxValue);
                                        break;
                                }
                            }
                            success &= rtc.ListBegin(laser);
                            foreach (var entity in layer)
                            {
                                var markerable = entity as IMarkerable;
                                if (null != markerable)
                                    success &= markerable.Mark(this.MarkerArg);
                                if (!success)
                                    break;

                                float progress = ((float)i / (float)offsets.Count * (float)j / (float)this.clonedDoc.Layers.Count * 100.0f);
                                this.MarkerArg.Progress = progress;
                                var receivers = this.OnProgress?.GetInvocationList();
                                if (null != receivers)
                                    foreach (MarkerEventHandler receiver in receivers)
                                        receiver.Invoke(this, this.MarkerArg);

                            }
                            if (success)
                            {
                                success &= rtc.ListEnd();
                                success &= rtc.ListExecute(true);
                            }
                            break;
                    }//end of switch
                    if (!success) break;
                }
                rtc.MatrixStack.Pop();
                rtc.MatrixStack.Pop();
                rtc.MatrixStack.Pop();
                if (!success)
                    break;
            }
            return success;
        }
        protected virtual bool PostWork()
        {
            this.IsFinished = true;
            this.MarkerArg.Progress = 100;
            var receivers = this.OnProgress?.GetInvocationList();
            if (null != receivers)
                foreach (MarkerEventHandler receiver in receivers)
                    receiver.Invoke(this, this.MarkerArg);
            return true;
        }
        #endregion
    }
}
