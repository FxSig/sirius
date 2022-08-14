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
 * 
 * Copyright (C) 2010-2020 SpiralLab. All rights reserved.
 * marker with x,y, angle offsets
 * Description : 
 * Author : hong chan, choi / hcchoi@spirallab.co.kr (http://spirallab.co.kr)
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
    /// user-defined marker concrete class 
    /// 마커 객체 (사용자 구현 버전)
    /// </summary>
    public class YourCustomMarker : IMarker
    {
        /// <summary>
        /// 가공 시작 이벤트핸들러
        /// </summary>
        public event MarkerEventHandler OnStarted;
        /// <summary>
        /// 진행률 이벤트 핸들러
        /// IMarkerArg 인자의 Progress 값이 0~100 범위에서 증가
        /// </summary>
        public event MarkerEventHandler OnProgress;
        /// <summary>
        /// 완료 이벤트 핸들러
        /// IMarkerArg 인자에 세부정보 참고 
        /// </summary>
        public event MarkerEventHandler OnFinished;
        /// <summary>
        /// 계측 이벤트 핸들러
        /// 가공완료후 개체내에 MeasurementBegin/End 가 존재할 경우 
        /// 계측된 데이타를 전달하기위해 이벤트 핸들러 호출됨
        /// MeasurementBegin 엔티티의 Save 를 통해 계측 데이타 저장하거나 GetMeasuredData 함수를 이용한 개별 데이타 직접 취득 가능
        /// 실행경로\plot\measurement-마커이름-날짜.txt 파일이름으로 계측 데이타가 생성된후 자동으로 그래프로 플롯(plot) 출력됨
        /// </summary>
        public event MeasureEventHandler OnMeasurement;

        /// <summary>
        /// 식별 번호
        /// </summary>
        public int Index { get; set; }
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

        /// <summary>
        /// 스캐너가 회전되어 장착되어 있는 경우 설정. 기본값 (0)
        /// 지정된 각도만큼 내부에서 회전 처리됨
        /// </summary>
        public double ScannerRotateAngle { get; set; }
        /// <summary>
        /// 복제된 문서 객체
        /// </summary>
        public IDocument Document { get { return this.clonedDoc; } }
        public uint MarkCounts { get; set; }
        public bool IsEnablePens { get; set; }
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
        public YourCustomMarker(int index)
        {
            this.Index = index;
            this.MarkerArg = new MarkerArgDefault();
            this.ScannerRotateAngle = 0;
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
            this.MarkerArg.Progress = 0;
            this.OnProgress?.Invoke(this, this.MarkerArg);
            return true;
        }
        /// <summary>
        /// 복제된 문서 데이타를 초기화 (다시 Ready 를 호출하여 문서 복제 필요)
        /// </summary>
        /// <returns></returns>
        public virtual bool Clear()
        {
            if (this.IsBusy)
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: trying to clear but busy");
                return false;
            }
            this.clonedDoc.New();
            Logger.Log(Logger.Type.Info, $"marker [{this.Index}] {this.Name}: cleared");
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
            var motorZ = this.MarkerArg.MotorZ;

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

            this.MarkerArg.Progress = 0;
            this.OnProgress?.Invoke(this, this.MarkerArg);
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

        public virtual bool IsTargetLayer(Layer layer)
        {
            //targets all
            return true;
        }
        public virtual bool IsTargetEntity(IEntity entity)
        {
            //targets all
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
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}]: aborted or fail to mark");
            MarkerArg.EndTime = DateTime.Now;

            if (this.IsFinished)
            {
                var timeSpan = MarkerArg.EndTime - MarkerArg.StartTime;
                this.OnFinished?.Invoke(this, this.MarkerArg);
                this.MarkCounts++;
                Logger.Log(Logger.Type.Info, $"marker [{this.Index}]: job finished. time= {timeSpan.TotalSeconds:F3}s");
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
            var laser = this.MarkerArg.Laser;
            var motorZ = this.MarkerArg.MotorZ;
            var offsets = this.MarkerArg.Offsets;
            var scannerRotateAngle =  this.ScannerRotateAngle;
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
                    if (layer.IsMarkerable)
                    {
                        if (!success)
                            break;
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
                            this.OnProgress?.Invoke(this, this.MarkerArg);
                        }
                        if (success)
                        {
                            success &= rtc.ListEnd();
                            success &= rtc.ListExecute(true);
                        }
                    }
                    if (!success) 
                        break;
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
            this.OnProgress?.Invoke(this, this.MarkerArg);
            return true;
        }
        #endregion


        #region 이벤트 호출 (상속 구현한 자식 클래스에서 호출시 사용)
        /// <summary>
        /// 가공 시작
        /// </summary>
        public virtual void NotifyStarted()
        {
            var receivers = this.OnStarted?.GetInvocationList();
            if (null != receivers)
                foreach (MarkerEventHandler receiver in receivers)
                    receiver.BeginInvoke(this, this.MarkerArg, null, null);
        }
        /// <summary>
        /// 가공 진행률
        /// (MarkerArg 의 Progress 값이 0~100 사이)
        /// </summary>
        public virtual void NotifyProgressing()
        {
            var receivers = this.OnProgress?.GetInvocationList();
            if (null != receivers)
                foreach (MarkerEventHandler receiver in receivers)
                    receiver.BeginInvoke(this, this.MarkerArg, null, null);
        }

        protected MeasurementBegin entityMeasurementBegin = null;
        protected MeasurementEnd entityMeasurementEnd = null;
        /// <summary>
        /// 계측 발생시 
        /// </summary>
        /// <param name="rtcMeasurement"></param>
        /// <param name="measurementBegin"></param>
        public virtual void NotifyMeasuring(IRtcMeasurement rtcMeasurement, MeasurementBegin measurementBegin)
        {
            if (null != this.MarkerArg && this.MarkerArg.IsMeasurementToPolt && null != this.entityMeasurementBegin)
            {
                string plotFileFullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plot", $"measurement-{this.Name}-{DateTime.Now.ToString("MM-dd-hh-mm-ss")}.txt");
                MeasurementHelper.Save(plotFileFullPath, measurementBegin, rtcMeasurement as IRtc);
                MeasurementHelper.Plot(plotFileFullPath);
            }
            var receivers = this.OnMeasurement?.GetInvocationList();
            if (null != receivers)
                foreach (MeasureEventHandler receiver in receivers)
                    receiver.Invoke(this, rtcMeasurement, measurementBegin);
        }
        /// <summary>
        /// 가공 완료시
        /// </summary>
        public virtual void NotifyFinished()
        {
            var receivers = this.OnFinished?.GetInvocationList();
            if (null != receivers)
                foreach (MarkerEventHandler receiver in receivers)
                    receiver.Invoke(this, this.MarkerArg);
        }
        #endregion
    }
}
