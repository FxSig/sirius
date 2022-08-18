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
        /// 가공 실패 이벤트 핸들러
        /// </summary>
        public event MarkerEventHandler OnFailed;
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
        /// 마커 식별 번호
        /// </summary>
        public virtual int Index { get; set; }
        /// <summary>
        /// 마커 이름
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 가공 준비 상태
        /// </summary>
        public virtual bool IsReady
        {
            get
            {
                if (null == this.MarkerArg)
                    return false;
                if (this.IsBusy)
                    return false;
                if (null != this.MarkerArg.Rtc)
                {
                    if (!this.MarkerArg.Rtc.CtlGetStatus(RtcStatus.NoError))
                        return false;
                }
                if (null != this.MarkerArg.Laser)
                {
                    if (!this.MarkerArg.Laser.IsReady)
                        return false;
                    if (this.MarkerArg.Laser.IsError)
                        return false;
                }
                if (null != this.MarkerArg.MotorZ)
                {
                    if (!this.MarkerArg.MotorZ.IsReady)
                        return false;
                    if (this.MarkerArg.MotorZ.IsError)
                        return false;
                }
                return true;
            }
        }
        /// <summary>
        /// 가공중 여부
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
                return busy;
            }
        }
        /// <summary>
        /// 에러발생 여부
        /// </summary>
        public virtual bool IsError
        {
            get
            {
                bool error = false;
                if (null != this.MarkerArg && null != this.MarkerArg.Laser)
                    error |= this.MarkerArg.Laser.IsError;
                if (null != this.MarkerArg && null != this.MarkerArg.MotorZ)
                    error |= this.MarkerArg.MotorZ.IsError;
                return error;
            }
        }
        /// <summary>
        /// 마커 Ready 호출시 전달된 MarkerArg 의 복사본 
        /// </summary>
        public virtual IMarkerArg MarkerArg { get; protected set; }
        /// <summary>
        /// 스캐너가 회전되어 장착되어 있는 경우 설정. 기본값 (0)
        /// 지정된 각도만큼 내부에서 회전 행렬이 삽입되어 처리됨
        /// </summary>
        public virtual double ScannerRotateAngle { get; set; }
        /// <summary>
        /// Ready 호출시 전달된 원본 문서(Document) 의 복사본
        /// </summary>
        public virtual IDocument Document { get { return this.clonedDoc; } }
        protected IDocument clonedDoc;

        /// <summary>
        /// Ready 호출시 전달된 원본(Original) 문서(Document) 
        /// </summary>
        protected IDocument originalDoc;

        /// <summary>
        /// 펜 집한 기능 사용 유무 
        /// 편집기(SiriusEditorForm) 의 펜 집합 기능 사용유무 전달용
        /// </summary>
        public virtual bool IsEnablePens { get; set; }
        /// <summary>
        /// 누적 가공 회수
        /// </summary>
        public uint MarkCounts { get; protected set; }
        /// <summary>
        /// 사용자 정의 데이타
        /// </summary>
        public virtual object Tag { get; set; }

        protected Thread thread;
        protected bool isAborted;
        /// <summary>
        /// 가공 시작전 RTC 의 행렬 스택 복사본
        /// </summary>
        protected IMatrixStack oldMatrixStack;

        protected MeasurementBegin entityMeasurementBegin = null;
        protected MeasurementEnd entityMeasurementEnd = null;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="index">식별 번호</param>
        public YourCustomMarker(int index)
        {
            this.Index = index;
            this.Name = "Your Custom Marker";
            this.MarkerArg = new MarkerArgDefault();
            this.ScannerRotateAngle = 0;
            this.isAborted = false;            
        }
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="index">식별 번호</param>
        /// <param name="name">이름</param>
        public YourCustomMarker(int index, string name)
            : this(index)
        {
            this.Name = name;
        }
        /// <summary>
        /// 마커는 내부 쓰레드에 의해 가공 데이타를 순회하면서 (Mark 호출) 가공 처리하게 되는데, 
        /// 이때 가공 데이타(IDocument)는 크로스 쓰레드 상태에 놓이게 된다.
        /// 때문에 준비(Prepare) 함수를 호출하면 원본 문서(Document)를 모두 복제(Clone)하여 가공시 이 복사본을 이용해 가공 처리를 하여 동기화 문제를 해소한다. 
        /// 때문에 가공중 뷰(편집기 등)에 의해 원본 문서 데이타가 수정되더라도 가공 중인 데이타와는 상관이 없다.
        /// </summary>
        /// <param name="markerArg">가공 인자</param>
        /// <returns></returns>
        public virtual bool Ready(IMarkerArg markerArg)
        {
            if (null == markerArg)
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: invalid marker arg");
                return false;
            }
            if (null == markerArg.Document || null == markerArg.Rtc || null == markerArg.Laser)
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: invalid marker args");
                return false;
            }
            if (this.IsBusy)
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: busy now ! fail to ready");
                return false;
            }
            this.MarkerArg = (MarkerArgDefault)markerArg.Clone();
            this.clonedDoc = this.MarkerArg.Document;
            Debug.Assert(clonedDoc != null);
            this.originalDoc = markerArg.Document;
            /*
             * MOTF 용 폰트 다운로드 & 스크립트 컴파일 
             * 
            if (!this.RegisterFonts(this.MarkerArg.Rtc, this.clonedDoc))
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: fail to register characterset");
                return false;
            }
            if (!this.ScriptCompile(this.MarkerArg.Rtc, this.clonedDoc))
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: fail to compile scripts");
                return false;
            }
            */
            this.MarkerArg.Progress = 0;
            this.NotifyProgressing();
            Logger.Log(Logger.Type.Info, $"marker [{this.Index}] {this.Name}: document has been ready with offset: {clonedDoc.RotateOffset.ToString()}");
            return true;
        }

        /*
        /// <summary>
        /// 문자 집합 RTC 내부 버퍼(리스트3)에 다운로드
        /// (비활성화됨) 스크립트 컴파일및 실행
        /// </summary>
        /// <param name="rtc"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        protected virtual bool RegisterFonts(IRtc rtc, IDocument doc)
        {
            Debug.Assert(rtc != null);
            Debug.Assert(doc != null);
            //character set 모두 삭제
            RtcCharacterSetHelper.Clear(rtc);
            bool success = true;
            for (int i = 0; i < doc.Layers.Count; i++)
            {
                var layer = doc.Layers[i];
                if (this.IsTargetLayer(layer))
                {
                    if (layer.IsMarkerable)
                    {
                        foreach (var entity in layer)
                        {
                            success &= this.RegisterFont(rtc, entity);
                            if (!success)
                                break;
                        }
                    }
                }
            }
            return success;
        }
        /// <summary>
        /// 문서내의 모든 개체를 순회하면서 스크립트 실행
        /// </summary>
        /// <param name="rtc"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        protected virtual bool ScriptCompile(IRtc rtc, IDocument doc)
        {
            Debug.Assert(rtc != null);
            Debug.Assert(doc != null);
            bool success = true;
            for (int i = 0; i < doc.Layers.Count; i++)
            {
                var layer = doc.Layers[i];
                if (this.IsTargetLayer(layer))
                {
                    if (layer.IsMarkerable)
                    {
                        foreach (var entity in layer)
                        {
                            success &= this.ScriptCompile(entity);
                            if (!success)
                                break;
                        }
                    }
                }
            }
            return success;
        }
        /// <summary>
        /// 스크립트 인터페이스를 가진 엔티티의 스크립트 실행을 처리
        /// </summary>
        /// <param name="entity">대상 개체</param>
        /// <param name="applyIntoData">실행 결과를 대상 개체 데이타에 적용여부</param>
        /// <returns></returns>
        protected virtual bool ScriptExecute(IEntity entity, bool applyIntoData = false)
        {
            var scriptable = entity as IScriptable;
            var group = entity as Group;
            if (null != group) //group 의 경우 하부 엔티티에 대해서도 동작
            {
                foreach (var subEntity in group)
                {
                    if (!this.ScriptExecute(subEntity))
                        return false;
                }
                return true;
            }
            else if (null != scriptable)
            {
                if (!scriptable.IsScriptEnabled)
                    return true;
                //doc ready (download) 시에 이미 컴파일되어 있음
                if (scriptable.ScriptExecute(out var result, applyIntoData))
                {
                    return true;
                }
                else
                {
                    Logger.Log(Logger.Type.Error, $"fail to execute script: {entity.ToString()}");
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 문자 집합 다운로드
        /// </summary>
        /// <param name="rtc"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual bool RegisterFont(IRtc rtc, IEntity entity)
        {
            Debug.Assert(rtc != null);
            Debug.Assert(entity != null);
            bool success = true;
            var siriusText = entity as SiriusText;
            var text = entity as Text;
            var group = entity as Group;
            if (null != siriusText)
            {
                success &= siriusText.RegisterCharacterSetIntoRtc(rtc);
                return success;
            }
            else if (null != text)
            {
                success &= text.RegisterCharacterSetIntoRtc(rtc);
                return success;
            }
            else if (null != group) //group 의 경우 하부 엔티티에 대해서도 동작
            {
                foreach (var subEntity in group)
                {
                    success &= this.RegisterFont(rtc, subEntity);
                    if (!success)
                        break;
                }
                return success;
            }
            return true;
        }
        /// <summary>
        /// 개체 내에 설정된 스크립트 실행
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual bool ScriptCompile(IEntity entity)
        {
            bool success = true;
            var scriptable = entity as IScriptable;
            var group = entity as Group;
            if (null != scriptable)
            {
                if (scriptable.IsScriptEnabled)
                {
                    string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scripts", scriptable.ScriptFileName);
                    if (File.Exists(fullPath))
                    {
                        var lineOfCodes = File.ReadAllText(fullPath);
                        if (scriptable.ScriptCompile(lineOfCodes, out var compileErrorCollection))
                        {

                        }
                        else
                        {
                            foreach (var err in compileErrorCollection)
                            {
                                Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: scrip compile error: {err.ToString()}");
                            }
                            success &= false;
                        }
                    }
                    else
                    {
                        Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: script file is not founded: {fullPath}");
                        success &= false;
                    }
                }
            }
            else if (null != group) //group 의 경우 하부 엔티티에 대해서도 동작
            {
                foreach (var subEntity in group)
                {
                    success &= this.ScriptCompile(subEntity);
                    if (!success)
                        break;
                }
            }
            return success;
        }
        /// <summary>
        /// TextChangeable 인터페이스를 지원하는 개체들(문자, 바코드등)
        /// 에 대한 날짜, 시간 포맷변환, 알련번호 증가 등 처리
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="layerIndex"></param>
        /// <returns></returns>
        public virtual bool IncrementSerialNoOrDateTime(IDocument doc, int layerIndex)
        {
            Debug.Assert(doc != null);
            Form form = null;
            if (Application.OpenForms.Count > 1)
                form = Application.OpenForms[0];
            bool success = true;
            form.Invoke(new MethodInvoker(delegate ()
            {
                //for (int i = 0; i < doc.Layers.Count; i++)
                {
                    var layer = doc.Layers[layerIndex];
                    if (this.IsTargetLayer(layer))
                    {
                        if (layer.IsMarkerable)
                        {
                            foreach (var entity in layer)
                            {
                                success &= this.IncrementSerialNoOrDateTime(entity, out bool dummy);
                            }
                        }
                    }
                }
            }));

            return success;
        }
        /// <summary>
        /// 날짜, 시간 포맷변환, 알련번호 증가 등 처리
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isModified"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        protected virtual bool IncrementSerialNoOrDateTime(IEntity entity, out bool isModified, uint skip = 0)
        {
            bool success = true;
            isModified = false;
            var textChangeable = entity as ITextChangeable;
            var markerable = entity as IMarkerable;
            var group = entity as Group;
            if (null != textChangeable && null != markerable && markerable.IsMarkerable)
            {
                uint increment = textChangeable.OffsetSerialNoIncrementStep;
                success &= TextChangeableHelper.IncrementSerialNoOrDateTime(textChangeable, increment);
                if (success)
                {
                    isModified = true;
                }
            }
            else if (null != group) //group 의 경우 하부 엔티티에 대해서도 동작
            {
                bool isGroupModified = false;
                foreach (var subEntity in group)
                {
                    success &= this.IncrementSerialNoOrDateTime(subEntity, out bool isModified2);
                    isGroupModified |= isModified2;
                }
                //실제 데이타가 바뀌면 boundrect 재 계산을 위해 ?
                if (isGroupModified)
                    group.Regen();
            }

            return success;
        }
        /// <summary>
        /// 스크립트 실행후 변경된 사항을 UI(윈폼)에 반영
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="layerIndex"></param>
        /// <returns></returns>
        public virtual bool ScriptCompileFeedBack(IDocument doc, int layerIndex)
        {
            Debug.Assert(doc != null);

            Form form = null;
            if (Application.OpenForms.Count > 1)
                form = Application.OpenForms[0];
            bool success = true;
            form.Invoke(new MethodInvoker(delegate ()
            {
                var layer = doc.Layers[layerIndex];
                if (this.IsTargetLayer(layer))
                {
                    if (layer.IsMarkerable)
                    {
                        foreach (var entity in layer)
                        {
                            success &= this.ScriptCompile(entity);
                            if (success)
                            {
                                this.ScriptExecute(entity, true);
                            }
                        }
                    }
                }
            }));
            return success;
        }
        */

        /// <summary>
        /// 복제된 내부 가공 데이타를 초기화 
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
        /// 내부 함수 호출 순서 
        /// WorkerThread : PreWork -> MainWork (LayerWork * N  -> EntityWork * N) -> PostWork
        /// </summary>
        /// <returns></returns>
        public virtual bool Start()
        {
            Logger.Log(Logger.Type.Warn, $"marker [{this.Index}] {this.Name}: trying to start mark");
            if (null == this.MarkerArg || null == this.MarkerArg.Rtc || null == this.MarkerArg.Laser)
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: invalid marker arg, rtc, laser status");
                this.NotifyFailed();
                return false;
            }
            if (null == this.clonedDoc || 0 == this.clonedDoc.Layers.Count)
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: document doesn't has any layers");
                this.NotifyFailed();
                return false;
            }
            var rtc = this.MarkerArg.Rtc;
            var laser = this.MarkerArg.Laser;
            var motorZ = this.MarkerArg.MotorZ;
            this.MarkerArg.IsEnablePens = true;

            if (rtc.CtlGetStatus(RtcStatus.Busy))
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: busy now !");
                this.NotifyFailed();
                return false;
            }
            if (this.MarkerArg.IsVerifyScannerPowerFault && !rtc.CtlGetStatus(RtcStatus.PowerOK))
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: scanner supply power is not ok !");
                this.NotifyFailed();
                return false;
            }
            if (this.MarkerArg.IsVerifyScannerPowerFault && !rtc.CtlGetStatus(RtcStatus.PositionAckOK))
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: scanner position ack is not ok !");
                this.NotifyFailed();
                return false;
            }
            //if (!rtc.CtlGetStatus(RtcStatus.NoError))
            //{
            //    Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: rtc has a internal problem !");
            //    this.NotifyFailed();
            //    return false;
            //}
            if (laser.IsError)
            {
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: laser source has a error status !");
                this.NotifyFailed();
                return false;
            }
            if (null != motorZ)
            {
                if (!motorZ.IsReady)
                {
                    Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: motor z is not ready");
                    this.NotifyFailed();
                    return false;
                }
                if (motorZ.IsBusy)
                {
                    Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: motor z is busy");
                    this.NotifyFailed();
                    return false;
                }
                if (motorZ.IsError)
                {
                    Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: motor z has a alarm status");
                    this.NotifyFailed();
                    return false;
                }
            }
            this.thread?.Join(5);
            if (this.MarkerArg.Offsets.Count == 0)
            {
                this.MarkerArg.Offsets.Add(Offset.Zero);
                Logger.Log(Logger.Type.Warn, $"marker [{this.Index}] {this.Name}: no offset information ... so reset by origin");
            }
            Logger.Log(Logger.Type.Debug, $"marker [{this.Index}] {this.Name}: total offset counts = {this.MarkerArg.Offsets.Count}");

            this.MarkerArg.PenStack.Clear();
            this.isAborted = false;
            this.thread = new Thread(this.WorkerThread);
            this.thread.Name = $"Marker: {this.Name}";
            this.thread.Priority = ThreadPriority.Normal;
            this.thread.Start();
            return true;
        }

        /// <summary>
        /// External /START 사용시 이를 해제
        /// </summary>
        /// <returns></returns>
        public virtual bool ExternalStop()
        {
            if (null == this.MarkerArg)
                return false;

            bool success = false;
            if (this.MarkerArg.IsExternalStart)
            {
                var rtc = this.MarkerArg.Rtc;
                var rtcExt = rtc as IRtcExtension;
                if (null == rtcExt)
                    return false;
                if (rtc is Rtc4)
                {
                    var extMode = Rtc4ExternalControlMode.Empty;
                    //extMode.Add(Rtc4ExternalControlMode.Bit.ExternalStart);
                    //extMode.Add(Rtc4ExternalControlMode.Bit.ExternalStartAgain);
                    success = rtcExt.CtlExternalControl(extMode);
                }
                else if (rtc is Rtc5)
                {
                    var extMode = Rtc5ExternalControlMode.Empty;
                    //extMode.Add(Rtc5ExternalControlMode.Bit.ExternalStart);
                    //extMode.Add(Rtc5ExternalControlMode.Bit.ExternalStartAgain);
                    //extMode.Add(Rtc5ExternalControlMode.Bit.ExternalStop);
                    //extMode.Add(Rtc5ExternalControlMode.Bit.TrackDelay);
                    success = rtcExt.CtlExternalControl(extMode);
                }
                else if (rtc is Rtc6 || rtc is Rtc6Ethernet)
                {
                    var extMode = Rtc6ExternalControlMode.Empty;
                    //extMode.Add(Rtc6ExternalControlMode.Bit.ExternalStart);
                    //extMode.Add(Rtc6ExternalControlMode.Bit.ExternalStartAgain);
                    //extMode.Add(Rtc6ExternalControlMode.Bit.ExternalStop);
                    //extMode.Add(Rtc6ExternalControlMode.Bit.TrackDelay);
                    success = rtcExt.CtlExternalControl(extMode);
                }
                if (rtc is IRtcAutoLaserControl rtcAlc)
                {
                    rtcAlc.AutoLaserControlByPositionFileName = string.Empty;
                    //rtcAlc.AutoLaserControlByPositionTableNo = 
                    rtcAlc.CtlAutoLaserControl<uint>(AutoLaserControlSignal.Disabled, AutoLaserControlMode.Disabled, 0, 0, 0);
                }
                Logger.Log(Logger.Type.Info, $"marker [{this.Index}] {this.Name}: job finished");
                this.NotifyFinished();
            }
            return success;
        }
        /// <summary>
        /// 가공 강제 정지
        /// </summary>
        /// <returns></returns>
        public virtual bool Stop()
        {
            if (null == this.MarkerArg)
                return false;
            if (this.MarkerArg.IsExternalStart)
                return this.ExternalStop();

            bool success = true;
            this.isAborted = true;
            Logger.Log(Logger.Type.Warn, $"marker [{this.Index}] {this.Name}: trying to stop ...");
            if (null != this.MarkerArg.Rtc)
            {
                success &= this.MarkerArg.Rtc.CtlAbort();
                success &= this.MarkerArg.Rtc.CtlLaserOff();
            }
            if (null != this.MarkerArg.MotorZ)
            {
                if (this.MarkerArg.MotorZ.IsDriving)
                    success &= this.MarkerArg.MotorZ.CtlMoveStop();
            }
            if (null != this.thread)
            {
                if (this.thread.Join(100))
                    this.thread = null;
                else
                    Logger.Log(Logger.Type.Warn, $"marker [{this.Index}] {this.Name}: thread is not joined yet");
            }

            if (this.MarkerArg.IsExternalStart)
                success &= this.ExternalStop();

            if (MarkerArg.IsGuided)
            {
                this.MarkerArg.Rtc.CtlMove(0, 0);
                if (this.MarkerArg.Rtc.CtlGetStatus(RtcStatus.Aborted))
                    this.MarkerArg.Rtc.CtlReset(); 
            }
            return success;
        }
        /// <summary>
        /// 리셋 (에러 상태 해제)
        /// </summary>
        /// <returns></returns>
        public virtual bool Reset()
        {
            if (null == this.MarkerArg)
                return false;
            this.MarkerArg.Rtc?.CtlReset();
            this.MarkerArg.Laser?.CtlReset();
            this.MarkerArg.MotorZ?.CtlReset();
            return true;
        }

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
        /// <summary>
        /// 가공 실패 통지
        /// </summary>
        public virtual void NotifyFailed()
        {
            var receivers = this.OnFailed?.GetInvocationList();
            if (null != receivers)
                foreach (MarkerEventHandler receiver in receivers)
                    receiver.BeginInvoke(this, this.MarkerArg, null, null);
        }
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

        #region 가공 작업 쓰레드 
        protected virtual void WorkerThread()
        {
            this.NotifyStarted();
            this.MarkerArg.StartTime = DateTime.Now;
            var rtc = this.MarkerArg.Rtc;
            bool success = PreWork() && MainWork();
            this.MarkerArg.IsSuccess = success;
            success &= PostWork(success);
            if (success)
            {
                if (!this.MarkerArg.IsExternalStart)
                    this.NotifyFinished();
            }
            else
                this.NotifyFailed();
        }
        /// <summary>
        /// 사전 작업
        /// 순서 : PreWork -> MainWork (LayerWork * N  -> EntityWork * N) -> PostWork
        /// </summary>
        protected virtual bool PreWork()
        {
            var rtc = this.MarkerArg.Rtc;
            this.oldMatrixStack = (IMatrixStack)rtc.MatrixStack.Clone();
            this.entityMeasurementBegin = null;
            this.entityMeasurementEnd = null;
            this.MarkerArg.Progress = 0;
            this.MarkerArg.IsSuccess = false;
            this.NotifyProgressing();

            // 3D 오프셋 값이 있으면 모두 리셋
            if (rtc is IRtc3D rtc3D)
            {
                rtc3D.CtlZOffset(0);
                rtc3D.CtlZDefocus(0);
            }
            // 레이저 소스측 리스트 시작 통지
            var laser = this.MarkerArg.Laser;
            if (laser.ListBegin())
                return true;
            else
            {
                Logger.Log(Logger.Type.Debug, $"marker [{this.Index}] {this.Name}: laser listbegin has returned false");
                return false;
            }
        }
        /// <summary>
        /// 주 작업
        /// 순서 : PreWork -> MainWork (LayerWork * N  -> EntityWork * N) -> PostWork
        /// </summary>
        protected virtual bool MainWork()
        {
            bool success = true;
            var rtc = this.MarkerArg.Rtc;
            int progressTotoal = this.clonedDoc.Layers.Count;
            int progressIndex = 0;

            for (int i = 0; i < this.clonedDoc.Layers.Count; i++)
            {
                var layer = this.clonedDoc.Layers[i];
                if (this.IsTargetLayer(layer))
                {
                    success &= LayerWork(i, layer);
                    if (!success)
                        break;
                }
                progressIndex++;
                float progress = (float)progressIndex / (float)progressTotoal * 100.0f;
                this.MarkerArg.Progress = progress;
                this.NotifyProgressing();
            }

            return success;
        }
        /// <summary>
        /// 레이어 가공 (매 레이어 가공시 호출됨)
        /// </summary>
        /// <param name="j">레이어 번호</param>
        /// <param name="layer">레이어 객체</param>
        /// <returns></returns>
        protected virtual bool LayerWork(int j, Layer layer)
        {
            if (!layer.IsMarkerable)
                return true;
            bool success = true;
            var rtc = this.MarkerArg.Rtc;
            var rtcAlc = rtc as IRtcAutoLaserControl;
            var laser = this.MarkerArg.Laser;
            var motorZ = this.MarkerArg.MotorZ;
            switch (layer.MotionType)
            {
                case MotionType.ScannerOnly:
                case MotionType.StageOnly:
                case MotionType.StageAndScanner:
                    #region Z 축 모션 제어
                    if (null != motorZ && layer.IsZEnabled)
                    {
                        float cmdPosition = layer.ZPosition;
                        float cmdVelocity = layer.ZPositionVel;
                        motorZ.Update(); //현재 Z 상태 업데이트
                        if (!MathHelper.IsEqual(cmdPosition, motorZ.ActualPosition, 0.01f)) //inpos 비교
                        {
                            if (!motorZ.IsReady)
                            {
                                success &= false;
                                Logger.Log(Logger.Type.Error, $"invalid motor z axis status. it's not ready");
                            }
                            if (motorZ.IsBusy)
                            {
                                success &= false;
                                Logger.Log(Logger.Type.Error, $"invalid motor z axis status. it's busy status");
                            }
                            if (motorZ.IsError)
                            {
                                success &= false;
                                Logger.Log(Logger.Type.Error, $"invalid motor z axis status. it's alarm status");
                            }
                            if (!success)
                                break;
                            success &= motorZ.CtlMoveAbs(cmdPosition, cmdVelocity);
                            if (!success)
                                break;
                            var sw = Stopwatch.StartNew();
                            do
                            {
                                motorZ.Update(); //현재 Z 상태 업데이트
                                if (sw.ElapsedMilliseconds > 10 * 1000) //10s time out
                                {
                                    Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: fail to move motor z axis. timed out ?");
                                    success &= false;
                                    break;
                                }
                                if (motorZ.IsError)
                                {
                                    success &= false;
                                    break;
                                }
                                if (this.isAborted)
                                {
                                    success &= false; //marker has stopped
                                    break;
                                }
                                if (MathHelper.IsEqual(cmdPosition, motorZ.ActualPosition, 0.01f) && !motorZ.IsDriving) //inpos 비교
                                    break;
                                Thread.Sleep(10);
                            } while (success);
                            if (!success)
                                break;
                        }
                        if (!success)
                            Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: fail to move motor z axis");
                    }
                    #endregion
                    if (!success)
                        break;

                    #region 레이어에 설정된 ALC 설정 적용 (스케일 보정 파일및 모드 설정)
                    if (null != rtcAlc && layer.IsALC)
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
                    #endregion

                    #region 매 레이어마다 RTC 리스트 명령 실행
                    var syncAxis = rtc as IRtcSyncAxis;
                    if (null != syncAxis)
                    {
                        //success &= syncAxis.CtlSetStagePosition(0, 0);
                        //success &= syncAxis.CtlSetScannerPosition(0,0);
                        switch (layer.MotionType)
                        {
                            case MotionType.StageOnly:
                                success &= syncAxis.CtlMotionType(MotionType.StageOnly);
                                break;
                            case MotionType.ScannerOnly:
                                success &= syncAxis.CtlMotionType(MotionType.ScannerOnly);
                                break;
                            case MotionType.StageAndScanner:
                                success &= syncAxis.CtlMotionType(MotionType.StageAndScanner);
                                success &= syncAxis.CtlBandWidth(layer.BandWidth);
                                break;
                        }
                        Logger.Log(Logger.Type.Debug, $"marker [{this.Index}] {this.Name}: layer {layer.Name} has started with motion type= {layer.MotionType.ToString()}");
                        success &= syncAxis.ListBegin(laser, layer.MotionType);
                    }
                    else
                    {
                        Logger.Log(Logger.Type.Debug, $"marker [{this.Index}] {this.Name}: layer {layer.Name} has started");
                        if (this.MarkerArg.IsExternalStart)
                            success &= rtc.ListBegin(laser, ListType.Single); //외부 트리거 사용시 강제로 단일 리스트로 고정
                        else
                            success &= rtc.ListBegin(laser, this.MarkerArg.RtcListType);
                    }
                    if (!success)
                        break;
                    var offsets = this.MarkerArg.Offsets;
                    var scannerRotateAngle = this.ScannerRotateAngle;
                    for (int i = 0; i < offsets.Count; i++)
                    {
                        var xyt = offsets[i];
                        Logger.Log(Logger.Type.Debug, $"marker [{this.Index}] {this.Name}: offset [{i}] : {xyt}");
                        rtc.MatrixStack.Push(scannerRotateAngle); // 3. 스캐너의 기구적 회전량
                        rtc.MatrixStack.Push(xyt.X + clonedDoc.RotateOffset.X, xyt.Y + clonedDoc.RotateOffset.Y); // 2. 오프셋 이동량
                        rtc.MatrixStack.Push(xyt.Angle + clonedDoc.RotateOffset.Angle);  // 1. 오프셋 회전량

                        for (int l = 0; l < layer.Repeat; l++)
                        {
                            switch (this.MarkerArg.MarkTargets)
                            {
                                case MarkTargets.All:
                                case MarkTargets.Custom:
                                    for (int k = 0; k < layer.Count; k++)
                                    {
                                        var entity = layer[k];
                                        success &= this.EntityWork(i, j, k, entity);
                                        if (!success)
                                            break;
                                    }
                                    break;
                                case MarkTargets.Selected:
                                case MarkTargets.SelectedButBoundRect:
                                    if (this.MarkerArg.IsGuided)
                                    {
                                        for (int r = 0; r < this.MarkerArg.RepeatSelected; r++)
                                        {
                                            for (int k = 0; k < layer.Count; k++)
                                            {
                                                var entity = layer[k];
                                                success &= this.EntityWork(i, j, k, entity);
                                                if (!success)
                                                    break;
                                            }
                                            if (!success)
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        for (int k = 0; k < layer.Count; k++)
                                        {
                                            var entity = layer[k];
                                            success &= this.EntityWork(i, j, k, entity);
                                            if (!success)
                                                break;
                                        }
                                    }
                                    break;
                            }
                            if (!success)
                                break;
                        }
                        rtc.MatrixStack.Pop();
                        rtc.MatrixStack.Pop();
                        rtc.MatrixStack.Pop();
                        if (!success)
                            break;

                        /*
                         * 일련번호 가공시 증감 결과 대상 뷰에 반영 
                        if (null != this.originalDoc)
                        {

                            if (!this.IncrementSerialNoOrDateTime(this.originalDoc, j))
                                Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: fail to update datetime/serial no");
                            if (!this.ScriptCompileFeedBack(this.originalDoc, j))
                                Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: fail to recompile");
                            Form form = null;
                            if (Application.OpenForms.Count > 1)
                                form = Application.OpenForms[0];
                            //property grid 도 refresh 되는게 좋은듯 한데 ....ㅡ.ㅡ
                            form.BeginInvoke(new MethodInvoker(delegate ()
                            {
                                if (null != this.MarkerArg.ViewTargets)
                                {
                                    foreach (var view in this.MarkerArg.ViewTargets)
                                        view?.Render();
                                }
                            }));
                        }
                    */
                    }

                    if (success)
                    {
                        if (this.MarkerArg.IsJumpToOriginAfterFinished || null != syncAxis)
                        {
                            success &= rtc.ListJump(0, 0);
                        }
                        success &= rtc.ListEnd();
                    }
                    if (success && !this.MarkerArg.IsExternalStart)
                        success &= rtc.ListExecute(true);
                    #endregion

                    #region 레이어에 설정된 ALC 설정 적용해제
                    if (null != rtcAlc && layer.IsALC && !this.MarkerArg.IsExternalStart)
                    {
                        rtcAlc.AutoLaserControlByPositionFileName = string.Empty;
                        rtcAlc.CtlAutoLaserControl<uint>(AutoLaserControlSignal.Disabled, AutoLaserControlMode.Disabled, 0, 0, 0);
                    }
                    #endregion

                    /* syncaxis 시뮬레이션 모드 사용시 뷰어 실행
                    if (success && null != syncAxis && syncAxis.IsSimulationMode)
                    {
                        if (!File.Exists(Config.ConfigSyncAxisViewerFileName))
                        {
                            MessageBox.Show(null, "syncAxis viewer is not founded", "SyncAxis Viewer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            string simulatedFileName = Path.Combine(Config.ConfigSyncAxisSimulateFilePath, syncAxis.SimulationFileName);
                            if (File.Exists(simulatedFileName))
                            {
                                Task.Run(() =>
                                {
                                    ProcessStartInfo startInfo = new ProcessStartInfo();
                                    startInfo.WorkingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "syncaxis", "Tools", "syncAXIS_Viewer");
                                    startInfo.CreateNoWindow = false;
                                    startInfo.UseShellExecute = false;
                                    startInfo.FileName = Config.ConfigSyncAxisViewerFileName;
                                    startInfo.WindowStyle = ProcessWindowStyle.Normal;
                                    startInfo.Arguments = "-a";//string.Empty;
                                    if (!string.IsNullOrEmpty(simulatedFileName))
                                        startInfo.Arguments = Path.Combine(Config.ConfigSyncAxisSimulateFilePath, simulatedFileName);
                                    try
                                    {
                                        var proc = Process.Start(startInfo);
                                    }
                                    catch (Exception ex)
                                    {
                                        Logger.Log(Logger.Type.Error, ex.Message);
                                    }
                                });
                            }
                        }
                    }
                    */
                    Logger.Log(Logger.Type.Debug, $"marker [{this.Index}] {this.Name}: layer {layer.Name} has ended");
                    break;
            }//end of switch

            return success;
        }
        /// <summary>
        /// 특정 레이어를 가공할지를 선택하기 위한 comparer
        /// 내부 가공 쓰레드가 레이어를 순회하면서 가공대상인지 여부를 판단
        /// </summary>
        /// <param name="layer">가공 대상인지 여부를 판단해야 하는 대상 레이어</param>
        /// <returns></returns>
        public virtual bool IsTargetLayer(Layer layer)
        {
            //default : all targets 
            return true;
        }
        /// <summary>
        /// 특정 개체를 가공할지를 선택하기 위한 comparer
        /// 내부 가공 쓰레드가 레이어내의 개체들를 순회하면서 가공대상인지 여부를 판단
        /// </summary>
        /// <param name="entity">가공 대상인지 여부를 판단해야 하는 대상 개체</param>
        /// <returns></returns>
        public virtual bool IsTargetEntity(IEntity entity)
        {
            //default : all targets 
            return true;
        }
        /// <summary>
        /// 엔티티 가공 (레이어 내의 매 엔티티 마다 호출됨)
        /// </summary>
        /// <param name="i">오프셋 번호</param>
        /// <param name="j">레이어 번호</param>
        /// <param name="k">엔티티 번호</param>
        /// <param name="entity">엔티티 객체</param>
        /// <returns></returns>
        protected virtual bool EntityWork(int i, int j, int k, IEntity entity)
        {
            bool success = true;
            var markerable = entity as IMarkerable;
            if (null != markerable)
            {
                switch (this.MarkerArg.MarkTargets)
                {
                    case MarkTargets.All:
                    case MarkTargets.Custom:
                        if (!this.IsTargetEntity(entity))
                            break;
                        //스크립트 기능 사용시
                        //success &= this.ScriptExecute(entity);
                        if (!success)
                            break;
                        success &= markerable.Mark(this.MarkerArg);
                        break;
                    case MarkTargets.Selected:
                        if (entity.IsSelected || entity is IPen || entity is PenReturn)
                        {
                            //스크립트 기능 사용시
                            //success &= this.ScriptExecute(entity);
                            if (!success)
                                break;
                            success &= markerable.Mark(this.MarkerArg);
                        }
                        break;
                    case MarkTargets.SelectedButBoundRect:
                        if (entity.IsSelected || entity is IPen || entity is PenReturn)
                        {
                            //스크립트 기능 사용시
                            //success &= this.ScriptExecute(entity);
                            if (!success)
                                break;
                            if (!this.IsTargetEntity(entity))
                                break;
                            if (!entity.BoundRect.IsEmpty)
                            {
                                //entity 의 펜 적용은?
                                var drawable = entity as IDrawable;
                                if (null != drawable)
                                {
                                    success &= PenDefault.MarkPen(MarkerArg, drawable.Color2, entity);
                                    success &= entity.BoundRect.Mark(this.MarkerArg);
                                }
                            }
                        }
                        break;
                    default:
                        success &= false;
                        break;
                }
                if (entity is MeasurementBegin)
                    this.entityMeasurementBegin = entity as MeasurementBegin;
                if (entity is MeasurementEnd)
                    this.entityMeasurementEnd = entity as MeasurementEnd;
            }
            return success;
        }
        /// <summary>
        /// 마무리 작업
        /// 순서 : PreWork -> MainWork (LayerWork * N  -> EntityWork * N) -> PostWork
        /// </summary>
        /// <returns></returns>
        protected virtual bool PostWork(bool success)
        {
            var rtc = this.MarkerArg.Rtc;
            rtc.MatrixStack = this.oldMatrixStack;
            this.MarkerArg.EndTime = DateTime.Now;
            var timeSpan = this.MarkerArg.EndTime - this.MarkerArg.StartTime;
            this.MarkerArg.IsSuccess = success;

            var laser = this.MarkerArg.Laser;
            success &= laser.ListEnd();
            if (!success)
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: laser listend has returned false");

            if (success)
            {
                if (null != this.entityMeasurementBegin && null != this.entityMeasurementEnd)
                {
                    var rtcMeasure = rtc as IRtcMeasurement;
                    if (null != rtcMeasure)
                    {
                        this.NotifyMeasuring(rtcMeasure, this.entityMeasurementBegin);
                    }
                }

                if (this.MarkerArg.IsExternalStart)
                {
                    var rtcExt = rtc as IRtcExtension;
                    if (rtc is Rtc4)
                    {
                        var extMode = Rtc4ExternalControlMode.Empty;
                        extMode.Add(Rtc4ExternalControlMode.Bit.ExternalStart);
                        extMode.Add(Rtc4ExternalControlMode.Bit.ExternalStartAgain);
                        //track delay 가 없어 ext delay 항목을 에디터에서 disable 시킴
                        success &= rtcExt.CtlExternalControl(extMode);
                    }
                    else if (rtc is Rtc5)
                    {
                        var extMode = Rtc5ExternalControlMode.Empty;
                        extMode.Add(Rtc5ExternalControlMode.Bit.ExternalStart);
                        extMode.Add(Rtc5ExternalControlMode.Bit.ExternalStartAgain);
                        extMode.Add(Rtc5ExternalControlMode.Bit.ExternalStop);
                        //extMode.Add(Rtc5ExternalControlMode.Bit.EncoderReset);
                        extMode.Add(Rtc5ExternalControlMode.Bit.TrackDelay);
                        success &= rtcExt.CtlExternalControl(extMode);
                    }
                    else if (rtc is Rtc6 || rtc is Rtc6Ethernet)
                    {
                        var extMode = Rtc6ExternalControlMode.Empty;
                        extMode.Add(Rtc6ExternalControlMode.Bit.ExternalStart);
                        extMode.Add(Rtc6ExternalControlMode.Bit.ExternalStartAgain);
                        extMode.Add(Rtc6ExternalControlMode.Bit.ExternalStop);
                        //extMode.Add(Rtc6ExternalControlMode.Bit.EncoderReset);
                        extMode.Add(Rtc6ExternalControlMode.Bit.TrackDelay);
                        success &= rtcExt.CtlExternalControl(extMode);
                    }
                }
                else
                {
                    this.MarkerArg.Progress = 100;
                    this.NotifyProgressing();
                    if (this.MarkerArg.IsGuided)
                        this.MarkerArg.Rtc.CtlMove(0, 0);
                    Logger.Log(Logger.Type.Info, $"marker [{this.Index}] {this.Name}: job finished. time= {timeSpan.TotalSeconds:F3}s");
                }

                this.MarkCounts++;
            }
            else
                Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: aborted or fail to mark");

            this.entityMeasurementBegin = null;
            this.entityMeasurementEnd = null;
            return true;
        }
        #endregion
    }
}
