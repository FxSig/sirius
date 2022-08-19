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
 * Copyright (C) 2010-2020 SpiralLab. All rights reserved. 
 * CustomEditorForm
 * Description : 
 * Author : hong chan, choi / hcchoi@spirallab.co.kr (http://spirallab.co.kr)
 * 
 */

using System;
using System.Windows.Forms;
using SharpGL;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace SpiralLab.Sirius
{
    /// <summary>
    /// 커스텀 시리우스 편집기
    /// </summary>
    public partial class CustomEditorForm 
        : Form
        //: UserControl
    {
        #region 이벤트 핸들러
        /// <summary>
        /// 문서(Document) 가 변경되었을때 발생하는 이벤트
        /// </summary>
        public virtual event SiriusDocumentSourceChanged OnDocumentSourceChanged;
        /// <summary>
        /// 문서(Document)를 신규로 생성할때 발생하는 이벤트
        /// Document.Action.ActNew() 가 호출되어야 함
        /// </summary>
        public virtual event SiriusDocumentNew OnDocumentNew;
        /// <summary>
        /// 문서(Document)가 로드될때 발생하는 이벤트
        /// DocumentSerializer.OpenSirius(파일이름) 으로 불러들인후 Document 에 문서객체를 설정
        /// </summary>
        public virtual event SiriusDocumentOpen OnDocumentOpen;
        /// <summary>
        /// 문서(Document)가 저장될때 발생하는 이벤트
        /// 미리 지정된 파일이름(FileName)에 덮어쓰기 저장됨
        /// Document.Action.ActSave(파일이름) 가 호출되어야 함
        /// </summary>
        public virtual event SiriusDocumentSave OnDocumentSave;
        /// 문서(Document)가 다른이름으로 저장될때 발생하는 이벤트
        /// Document.Action.ActSave(파일이름) 가 호출되어야 함
        /// 파일이름(FileName ) 속성 변경이 호출되어야 함
        public virtual event SiriusDocumentSave OnDocumentSaveAs;
        /// <summary>
        /// 편집기에서 새로운 레이어를 생성할때 발생하는 이벤트
        /// </summary>
        public virtual event SiriusDocumentLayerNew OnDocumentLayerNew;

        /// <summary>
        /// 문서(Document)에서 Marker 팝업 창을 띄울때 발생하는 이벤트
        /// 미 등록시 자체 Marker Form 이 팝업됨
        /// </summary>
        public virtual event EventHandler OnMarker;
        /// <summary>
        /// 문서(Document)에서 Laser 팝업 창을 띄울때 발생하는 이벤트
        /// </summary>
        public virtual event EventHandler OnLaser;
        /// <summary>
        /// 문서(Document)에서 RTC 팝업 창을 띄울때 발생하는 이벤트
        /// </summary>
        public virtual event EventHandler OnRtc;
        /// <summary>
        /// 문서(Document)에서 스캐너 보정 2D 팝업 창을 띄울때 발생하는 이벤트
        /// </summary>
        public virtual event EventHandler OnCorrection2D;
        /// <summary>
        /// 문서(Document)에서 스캐너 보정 3D 팝업 창을 띄울때 발생하는 이벤트
        /// </summary>
        public virtual event EventHandler OnCorrection3D;
        /// <summary>
        /// 문서(Document)에서 계측(Measurement) 팝업 창을 띄울때 발생하는 이벤트
        /// </summary>
        public virtual  event EventHandler OnMeasurement;
        /// <summary>
        /// 문서(Document)에서 디지털 입출력 창을 띄울때 발생하는 이벤트
        /// </summary>
        public virtual event EventHandler OnIO;
        /// <summary>
        /// 모터 창을 띄울때 발생하는 이벤트
        /// </summary>
        public event EventHandler OnMotor;
        /// <summary>
        /// 문서(Document)에서 파워측정(PowerMeter)창을 띄울때 발생하는 이벤트
        /// </summary>
        public virtual event EventHandler OnPowerMeter;
        /// <summary>
        /// 문서(Document)에서 파워매핑(PowerMap) 창을 띄울때 발생하는 이벤트
        /// </summary>
        public virtual event EventHandler OnPowerMap;
        #endregion

        #region 이벤트 호출 
        protected virtual void OnDocumentSourceChanging()
        {
            var receivers = this.OnDocumentSourceChanged?.GetInvocationList();
            if (null != receivers)
                foreach (SiriusDocumentSourceChanged receiver in receivers)
                    receiver.Invoke(this, this.doc);
            Logger.Log(Logger.Type.Debug, $"sirius editor document source has changed");
        }
        protected virtual void OnDocumentNewing()
        {
            var receivers = this.OnDocumentNew?.GetInvocationList();
            if (null != receivers)
                foreach (SiriusDocumentNew receiver in receivers)
                    receiver.Invoke(this);
            Logger.Log(Logger.Type.Debug, $"sirius editor document has new");
        }
        protected virtual void OnDocumentOpening()
        {
            var receivers = this.OnDocumentOpen?.GetInvocationList();
            if (null != receivers)
                foreach (SiriusDocumentOpen receiver in receivers)
                    receiver.Invoke(this);
            Logger.Log(Logger.Type.Debug, $"sirius editor document has opening");
        }
        protected virtual void OnDocumentSaving()
        {
            var receivers = this.OnDocumentSave?.GetInvocationList();
            if (null != receivers)
                foreach (SiriusDocumentSave receiver in receivers)
                    receiver.Invoke(this);
            Logger.Log(Logger.Type.Debug, $"sirius editor document saving");
        }
        protected virtual void OnDocumentSaveAsing()
        {
            var receivers = this.OnDocumentSaveAs?.GetInvocationList();
            if (null != receivers)
                foreach (SiriusDocumentSave receiver in receivers)
                    receiver.Invoke(this);
            Logger.Log(Logger.Type.Debug, $"sirius editor document save as");
        }     
        protected virtual void OnDocumentLayerNewing()
        {
            var receivers = this.OnDocumentLayerNew?.GetInvocationList();
            if (null != receivers)
                foreach (SiriusDocumentLayerNew receiver in receivers)
                    receiver.Invoke(this);
            Logger.Log(Logger.Type.Debug, $"sirius editor document layer new");
        }
        protected virtual void OnMarkerShow()
        {
            var receivers = this.OnMarker?.GetInvocationList();
            if (null != receivers)
                foreach (EventHandler receiver in receivers)
                    receiver.Invoke(this, EventArgs.Empty);
            Logger.Log(Logger.Type.Debug, $"sirius editor marker show");
        }
        protected virtual void OnLaserShow()
        {
            var receivers = this.OnLaser?.GetInvocationList();
            if (null != receivers)
                foreach (EventHandler receiver in receivers)
                    receiver.Invoke(this, EventArgs.Empty);
            Logger.Log(Logger.Type.Debug, $"sirius editor laser show");
        }
        protected virtual void OnRtcShow()
        {
            var receivers = this.OnRtc?.GetInvocationList();
            if (null != receivers)
                foreach (EventHandler receiver in receivers)
                    receiver.Invoke(this, EventArgs.Empty);
            Logger.Log(Logger.Type.Debug, $"sirius editor rtc show");
        }
        protected virtual void OnCorrection2DShow()
        {
            var receivers = this.OnCorrection2D?.GetInvocationList();
            if (null != receivers)
                foreach (EventHandler receiver in receivers)
                    receiver.Invoke(this, EventArgs.Empty);
            Logger.Log(Logger.Type.Debug, $"sirius editor correction 2d show");
        }
        protected virtual void OnCorrection3DShow()
        {
            var receivers = this.OnCorrection3D?.GetInvocationList();
            if (null != receivers)
                foreach (EventHandler receiver in receivers)
                    receiver.Invoke(this, EventArgs.Empty);
            Logger.Log(Logger.Type.Debug, $"sirius editor correction 3d show");
        }
        protected virtual void OnMeasurementShow()
        {
            var receivers = this.OnMeasurement?.GetInvocationList();
            if (null != receivers)
                foreach (EventHandler receiver in receivers)
                    receiver.Invoke(this, EventArgs.Empty);
            Logger.Log(Logger.Type.Debug, $"sirius editor measurement show");
        }
        protected virtual void OnIOShow()
        {
            var receivers = this.OnIO?.GetInvocationList();
            if (null != receivers)
                foreach (EventHandler receiver in receivers)
                    receiver.Invoke(this, EventArgs.Empty);
            Logger.Log(Logger.Type.Debug, $"sirius editor io show");
        }
        protected virtual void OnPowerMeterShow()
        {
            var receivers = this.OnPowerMeter?.GetInvocationList();
            if (null != receivers)
                foreach (EventHandler receiver in receivers)
                    receiver.Invoke(this, EventArgs.Empty);
            Logger.Log(Logger.Type.Debug, $"sirius editor power meter show");
        }
        protected virtual void OnPowerMapShow()
        {
            var receivers = this.OnPowerMap?.GetInvocationList();
            if (null != receivers)
                foreach (EventHandler receiver in receivers)
                    receiver.Invoke(this, EventArgs.Empty);
            Logger.Log(Logger.Type.Debug, $"sirius editor power map show");
        }
        protected virtual void OnMotorShow()
        {
            var receivers = this.OnMotor?.GetInvocationList();
            if (null != receivers)
                foreach (EventHandler receiver in receivers)
                    receiver.Invoke(this, EventArgs.Empty);
            Logger.Log(Logger.Type.Debug, $"sirius editor motor show");
        }
        #endregion

        /// <summary>
        /// 식별 번호
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Sirius")]
        [DisplayName("식별번호")]
        [Description("고유 식별 번호")]
        public virtual uint Index { get; set; }

        /// <summary>
        /// 상태바에 출력되는 이름
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Sirius")]
        [DisplayName("별명")]
        [Description("상태바에 표시되는 별명")]
        public virtual string AliasName
        {
            get
            {
                return this.lblName.Text;
            }
            set
            {
                this.lblName.Text = value;
            }
        }

        /// <summary>
        /// 상태바에 출력되는 진행상태 (0~100)
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Sirius")]
        [DisplayName("진행률")]
        [Description("가공 진행률 (0~100)")]
        public virtual int Progress
        {
            get
            {
                return this.pgbProgress.Value;
            }
            set
            {
                if (!this.IsHandleCreated || this.IsDisposed)
                    return;
                this.pgbProgress.Value = value;
            }
        }

        /// <summary>
        /// 상태바에 출력되는 작업 파일이름
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Sirius")]
        [DisplayName("작업파일이름")]
        [Description("파일명")]
        public virtual string FileName
        {
            get
            {
                return this.lblFileName.Text;
            }
            set
            {
                this.lblFileName.Text = value;
            }
        }

        /// <summary>
        /// 뷰 객체
        /// </summary>
        [Browsable(false)]
        public virtual IView View
        {
            get { return this.view; }
        }
        protected IView view;
        /// <summary>
        /// 문서 컨테이너 객체
        /// </summary>
        [Browsable(false)]
        public virtual IDocument Document
        {
            get { return this.doc; }
            set
            {
                if (null == value)
                    return;
                if (value.Equals(this.doc))
                    return;
                this.doc = value;
                this.ppgEntity.SelectedObject = null;
                if (0 == this.doc.Layers.Count)  //default layer create
                    this.doc.Action.ActNew();
                List<IView> oldViews = new List<IView>();
                if (null != this.Document)
                {
                    oldViews.AddRange(this.Document.Views);
                    this.doc.Action.OnEntitySelectedChanged -= Action_OnSelectedEntityChanged;
                    this.doc.Layers.OnAddItem -= Layer_OnAddItem;
                    this.doc.Layers.OnRemoveItem -= Layer_OnRemoveItem;
                    if (null != this.view)
                    {
                        this.doc.Views.Remove(this.view);
                        oldViews.Remove(this.view);
                    }
                }

                this.doc.Action.OnEntitySelectedChanged += Action_OnSelectedEntityChanged;
                this.doc.Layers.OnAddItem += Layer_OnAddItem;
                this.doc.Layers.OnRemoveItem += Layer_OnRemoveItem;
                this.FileName = this.doc.FileName;

                this.view = new ViewDefault(doc, this.GLcontrol);
                this.doc.Views.Add(this.view);
                this.doc.Views.AddRange(oldViews);
                this.view.Render();
                this.view.OnZoomFit();
                this.RegenTreeView();
                this.OnDocumentSourceChanging();
                this.chbLock.Checked = this.doc.IsEditorLock;       
            }
        }
        protected IDocument doc;

        /// <summary>
        /// RTC 제어 객체
        /// </summary>
        [Browsable(false)]
        public virtual IRtc Rtc
        {
            get { return this.rtc; }
            set
            {
                this.rtc = value;
                if (this.rtc == null)
                    return;
                var rtc5 = this.rtc as Rtc5;
                var rtc6 = this.rtc as Rtc6;

                if (this.rtc is RtcVirtual rtcVirtual)
                {

                }
                else if (this.rtc is Rtc4 rtc4)
                {
                    //지원되지 않는 기능에 대해 객체 속성을 가린다
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "IsSkyWritingEnabled", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "SkyWritingMode", false); 
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "LaserOnShift", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "TimeLag", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "AngularLimit", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "MinMarkSpeed", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "ApproxBlendLimit", false);
                    
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "WobbelShape", false);

                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "IsALC", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "AlcPositionFileName", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "AlcPositionTableNo", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "AlcSignal", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "AlcMode", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "AlcPercentage100", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "AlcMinValue", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "AlcMaxValue", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "MotionType", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "BandWidth", false);

                    btnRaster.Enabled = true;
                    btnImage.Enabled = true;
                    mnuMOTF.Enabled = true;
                    mnuMOTFExtStartDelay.Enabled = false;
                    mnuMOTFWait.Enabled = false;

                    mnuAlc.Enabled = false;
                    mnuPoD.Enabled = false;
                    mnuVectorBeginEnd.Enabled = false;
                    mnuAlcSyncAxisBeginEnd.Enabled = false;
                    mnuCalculationDynamics.Enabled = false;

                    mnuMeasurementBeginEnd.Enabled = true;
                    ext16IfToolStripMenuItem.Enabled = false;
                    waitExt16IfToolStripMenuItem1.Enabled = false;
                    zOffsetToolStripMenuItem.Enabled = true;
                    mnuZOffset.Enabled = false;

                    mnuSiriusTime.Enabled = false;
                    mnuSiriusDate.Enabled = false;
                    mnuSiriusSerial.Enabled = false;
                    mnuTextTime.Enabled = false;
                    mnuTextDate.Enabled = false;
                    mnuTextSerial.Enabled = false;
                }
                else if (null != rtc5 || null != rtc6)
                {
                    //지원되지 않는 기능에 대해 객체 속성을 가린다
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "IsSkyWritingEnabled", true);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "SkyWritingMode", true);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "LaserOnShift", true);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "TimeLag", true);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "AngularLimit", true);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "MinMarkSpeed", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "ApproxBlendLimit", false);

                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "IsALC", true);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "AlcPositionFileName", true);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "AlcPositionTableNo", true);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "AlcSignal", true);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "AlcMode", true);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "AlcPercentage100", true);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "AlcMinValue", true);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "AlcMaxValue", true);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "MotionType", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "BandWidth", false);

                    btnRaster.Enabled = true;
                    btnImage.Enabled = true;
                    mnuMOTF.Enabled = true;

                    mnuAlc.Enabled = true;
                    mnuPoD.Enabled = true;
                    mnuVectorBeginEnd.Enabled = true;
                    mnuAlcSyncAxisBeginEnd.Enabled = false;
                    mnuCalculationDynamics.Enabled = false;

                    mnuMeasurementBeginEnd.Enabled = true;
                    ext16IfToolStripMenuItem.Enabled = true;
                    waitExt16IfToolStripMenuItem1.Enabled = true;
                    zOffsetToolStripMenuItem.Enabled = true;

                    mnuSiriusTime.Enabled = true;
                    mnuSiriusDate.Enabled = true;
                    mnuSiriusSerial.Enabled = true;
                }
                else if (this.rtc is IRtcSyncAxis rtcSyncAxis)
                {
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "LaserOnDelay", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "LaserOffDelay", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "LaserFpk", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "ScannerJumpDelay", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "ScannerMarkDelay", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "ScannerPolygonDelay", false);

                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "IsSkyWritingEnabled", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "SkyWritingMode", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "LaserOnShift", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "TimeLag", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "AngularLimit", false);

                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "IsWobbelEnabled", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "WobbelPerpendicular", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "WobbelParallel", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "WobbelFrequency", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "WobbelShape", false);

                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "MinMarkSpeed", true);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "ApproxBlendLimit", true);

                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "IsALC", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "AlcPositionFileName", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "AlcPositionTableNo", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "AlcSignal", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "AlcMode", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "AlcPercentage100", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "AlcMinValue", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "AlcMaxValue", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "MotionType", true);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "BandWidth", true);

                    btnRaster.Enabled = false;
                    btnImage.Enabled = false;
                    mnuMOTF.Enabled = false;

                    mnuAlc.Enabled = true; 
                    mnuPoD.Enabled = false;
                    mnuVectorBeginEnd.Enabled = false;
                    mnuAlcSyncAxisBeginEnd.Enabled = true;
                    mnuCalculationDynamics.Enabled = true;

                    mnuMeasurementBeginEnd.Enabled = false;
                    ext16IfToolStripMenuItem.Enabled = false;
                    waitExt16IfToolStripMenuItem1.Enabled = false;
                    zOffsetToolStripMenuItem.Enabled = false;

                    mnuSiriusTime.Enabled = false;
                    mnuSiriusDate.Enabled = false;
                    mnuSiriusSerial.Enabled = false;

                    mnuMeasurementFile.Enabled = false;
                }
            }
        }
        protected IRtc rtc;
        /// <summary>
        /// 레이저 소스 제어 객체
        /// </summary>
        [Browsable(false)]
        public virtual ILaser Laser
        {
            get { return this.laser; }
            set { this.laser = value;

                if (this.laser == null)
                    return;
                //지원되지 않는 기능에 대해 객체 속성을 가린다
                if (this.laser.IsPowerControl)
                {
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "Power", true);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "PowerMax", true);
                    SpiralLab.Sirius.PenDefault.powerMax = laser.MaxPowerWatt;
                }
                else
                {
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "Power", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.PenDefault), "PowerMax", false);
                    SpiralLab.Sirius.PenDefault.powerMax = 0;
                }
            }
        }
        protected ILaser laser;
        /// <summary>
        /// 마커 제어 객체
        /// </summary>
        [Browsable(false)]
        public virtual IMarker Marker
        {
            get { return this.marker; }
            set
            {
                if (null != this.marker)
                {
                    this.marker.OnProgress -= Marker_OnProgress;
                    this.marker.OnFinished -= Marker_OnFinished;
                    this.Progress = 0;
                }
                this.marker = value;
                if (null != this.marker)
                {
                    this.marker.OnProgress += Marker_OnProgress;
                    this.marker.OnFinished += Marker_OnFinished;
                    this.marker.IsEnablePens = true;
                }
            }
        }
        protected IMarker marker;

        /// <summary>
        /// 스캐너 Z 축 제어용 객체
        /// </summary>
        [Browsable(false)]
        public virtual IMotor MotorZ
        {
            get { return this.motorZ; }
            set {
                this.motorZ = value;
                //지원되지 않는 기능에 대해 객체 속성을 가린다
                if (null != this.motorZ)
                {
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "IsZEnabled", true);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "ZPosition", true);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "ZPositionVel", true);
                }
                else
                {
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "IsZEnabled", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "ZPosition", false);
                    UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "ZPositionVel", false);
                }
                if (null != this.motorZ || null != this.motors)
                    mnuMotor.Enabled = true;
                else
                    mnuMotor.Enabled = false;
            }
        }
        protected IMotor motorZ;

        /// <summary>
        /// 모터 집합을 제어하기 위한 객체
        /// </summary>
        [Browsable(false)]
        public virtual IMotors Motors
        {
            get { return this.motors; }
            set
            {
                this.motors = value;
                if (null != this.motorZ || null != this.motors)
                    mnuMotor.Enabled = true;
                else
                    mnuMotor.Enabled = false;
            }
        }
        protected IMotors motors;

        /// <summary>
        /// RTC Pin2 포트 2입력
        /// </summary>
        [Browsable(false)]
        public virtual IDInput RtcPin2Input { get; set; }
        /// <summary>
        /// RTC Pin2 포트 2출력
        /// </summary>
        [Browsable(false)]
        public virtual IDOutput RtcPin2Output { get; set; }
        /// <summary>
        /// RTC 확장1 포트 16입력
        /// </summary>
        [Browsable(false)]
        public virtual IDInput RtcExtension1Input { get; set; }
        /// <summary>
        /// RTC 확장1 포트 16출력
        /// </summary>
        [Browsable(false)]
        public virtual IDOutput RtcExtension1Output { get; set; }
        /// <summary>
        /// RTC 확장2 포트 8출력
        /// </summary>
        [Browsable(false)]
        public virtual IDOutput RtcExtension2Output { get; set; }
        /// <summary>
        /// 파워메터
        /// </summary>
        [Browsable(false)]
        public virtual IPowerMeter PowerMeter 
        {
            get { return this.powerMeter; }
            set
            {
                this.powerMeter = value;
                if (null != this.powerMeter)
                    mnuPowerMeter.Enabled = true;
                else
                    mnuPowerMeter.Enabled = false;
            }
        }
        protected IPowerMeter powerMeter;

        /// <summary>
        /// 파워맵
        /// </summary>
        /// 
        [Browsable(false)]
        public virtual IPowerMap PowerMap
        {
            get { return this.powerMap; }
            set
            {                
                this.powerMap = value;                
                if (null != this.powerMap)
                    mnuPowerMap.Enabled = true;
                else
                    mnuPowerMap.Enabled = false;
            }
        }
        protected IPowerMap powerMap;

        protected System.Drawing.Point currentMousePos;


        /// <summary>
        /// 생성자
        /// </summary>
        public CustomEditorForm()
        {
            InitializeComponent();

            this.GLcontrol.OpenGLInitialized += new EventHandler(this.OnInitialized);
            this.GLcontrol.Resize += new EventHandler(this.OnResized);
            this.GLcontrol.MouseDown += new MouseEventHandler(this.OnMouseDown);
            this.GLcontrol.MouseUp += new MouseEventHandler(this.OnMouseUp);
            this.GLcontrol.MouseMove += new MouseEventHandler(this.OnMouseMove);
            this.GLcontrol.MouseWheel += new MouseEventHandler(this.OnMouseWheel);
            this.GLcontrol.OpenGLDraw += new RenderEventHandler(this.OnDraw);

            this.ppgEntity.PropertySort = PropertySort.Categorized;
            this.Disposed += SiriusEditorForm_Disposed;
            this.DragDrop += new System.Windows.Forms.DragEventHandler(SiriusEditorForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(SiriusEditorForm_DragEnter);
            if (null == CustomEditorForm.logForm)
            {
                CustomEditorForm.logForm = new LogForm();
                Logger.OnLogged += Logger_OnLogged;
            }

            //지원되지 않는 기능에 대해 객체 속성을 가린다
            //기본 false, MotorZ 지정시 보여주도록
            UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "IsZEnabled", false);
            UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "ZPosition", false);
            UiHelper.PropertyBrowsable(typeof(SpiralLab.Sirius.Layer), "ZPositionVel", false);
        }
        protected virtual void SiriusEditorForm_Disposed(object sender, EventArgs e)
        {
            Document?.Action?.ActEntityLaserPathSimulateStop();
        }
        protected virtual void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.markerForm = null;
        }
        protected virtual void SiriusEditorForm_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.FileDrop);
            if (data != null)
            {
                string[] fileNames = data as string[];
                string ext = System.IO.Path.GetExtension(fileNames[0]);
                switch (ext.ToLower())
                {
                    case ".sirius":
                    case ".dxf":
                        this.OnOpen(fileNames[0]);
                        break;
                }
            }
        }
        protected virtual void SiriusEditorForm_DragEnter(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.FileDrop);
            if (data != null)
            {
                string[] fileNames = data as string[];
                string ext = System.IO.Path.GetExtension(fileNames[0]);
                switch (ext.ToLower())
                {
                    case ".sirius":
                    case ".dxf":
                        e.Effect = DragDropEffects.Copy;
                        break;
                    default:
                        e.Effect = DragDropEffects.None;
                        break;
                }
            }
        }

        internal static LogForm logForm = null;
        protected virtual void Logger_OnLogged(Logger.Type type, string message)
        {
            if (!this.IsHandleCreated || this.IsDisposed)
                return;            
            if (null == logForm || logForm.IsDisposed)
                return;
            this.Invoke(new MethodInvoker(delegate ()
            {
                try
                {
                    logForm.Log(type, message);
                }
                catch(Exception )
                {

                }
            }));
        }
        protected virtual void Marker_OnProgress(IMarker sender, IMarkerArg arg)
        {
            if (!this.IsHandleCreated || this.IsDisposed)
                return;
            var span = arg.EndTime - arg.StartTime;
            stsBottom.BeginInvoke(new MethodInvoker(delegate ()
            {
                this.Progress = (int)arg.Progress;
            }));
        }
        protected virtual void Marker_OnFinished(IMarker sender, IMarkerArg arg)
        {
            if (!this.IsHandleCreated || this.IsDisposed)
                return;
            var span = arg.EndTime - arg.StartTime;
            stsBottom.BeginInvoke(new MethodInvoker(delegate ()
            {
                this.Progress = (int)arg.Progress;
            }));
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.F1))
            {
                mnuMarker_Click(this, EventArgs.Empty);
                return true;
            }
            else if (keyData == (Keys.F2))
            {
                mnuLaser_Click(this, EventArgs.Empty);
                return true;
            }
            else if (keyData == (Keys.F3))
            {
                mnuIO_Click(this, EventArgs.Empty);
                return true;
            }
            else if (keyData == (Keys.F4))
            {
                mnuRtc_Click(this, EventArgs.Empty);
                return true;
            }
            else if (keyData == (Keys.F12))
            {
                mnuLogWindow_Click(this, EventArgs.Empty);
                return true;
            }
            else
                return base.ProcessCmdKey(ref msg, keyData);
        }
        public override void Refresh()
        {
            base.Refresh();
            this.trvEntity.Refresh();
            this.ppgEntity.Refresh();
            this.view.Render();
        }
        protected virtual void OnInitialized(object sender, EventArgs e)
        {
            this.view?.OnInitialized(sender, e);
        }
        protected virtual void OnResized(object sender, EventArgs e)
        {
            this.view?.OnResized(sender, e);
        }
        protected virtual void OnMouseDown(object sender, MouseEventArgs e)
        {
            this.view?.OnMouseDown(sender, e);
        }
        protected virtual void OnMouseUp(object sender, MouseEventArgs e)
        {
            this.view?.OnMouseUp(sender, e);
            this.GroupButtonEnableOrNot();
        }
        protected virtual void OnMouseMove(object sender, MouseEventArgs e)
        {
            this.currentMousePos = e.Location;
            if (null != this.view)
            {
                this.view.OnMouseMove(sender, e);
                this.view.Dp2Lp(e.Location, out float x, out float y);
                this.lblXPos.Text = $"X: {x:F3}";
                this.lblYPos.Text = $"Y: {y:F3}";
            }
        }
        protected virtual void OnMouseWheel(object sender, MouseEventArgs e)
        {
            this.view?.OnMouseWheel(sender, e);
        }
        protected virtual void OnDraw(object sender, RenderEventArgs args)
        {
            if (!this.IsHandleCreated || this.IsDisposed)
                return;
            if (null == this.view)
                return;
            this.Invoke(new MethodInvoker(delegate ()
            {
                long msec = this.view.OnDraw();
                lblRenderTime.Text = $"Render: {msec} ms";
                if (this.view.SelectedBoundRect.IsEmpty)
                {
                    lblBound.Text = $" Left/Top, Right/Bottom ";
                    lblCenter.Text = $" Center ";
                    lblWH.Text = $" Width, Height ";
                }
                else
                {
                    lblBound.Text = this.view.SelectedBoundRect.ToString();
                    lblCenter.Text = $" {this.view.SelectedBoundRect.Center.X:F3}, {this.view.SelectedBoundRect.Center.Y:F3} ";
                    lblWH.Text = $" {this.view.SelectedBoundRect.Width:F3}, {this.view.SelectedBoundRect.Height:F3} ";
                }
            }));
        }
        protected virtual void RegenTreeView()
        {
            trvEntity.BeginUpdate();
            trvEntity.Nodes.Clear();
            int i = 0;
            foreach(var layer in this.Document.Layers)
            {
                this.Layer_OnAddItem(this.Document.Layers, i++, layer);
                int j = 0;
                foreach (var entity in layer)
                {
                    this.Entity_OnAddItem(layer, j++, entity);
                }
            }
            trvEntity.EndUpdate();
        }
        protected virtual void Layer_OnAddItem(ObservableList<Layer> sender, int index, Layer l)
        {
            if (trvEntity.IsDisposed)
                return;
            l.OnAddItem += Entity_OnAddItem;
            l.OnRemoveItem += Entity_OnRemoveItem;
            l.Node.Tag = l;

            var layers = sender as Layers;
            if (layers.Count == index)
                trvEntity.Nodes.Add(l.Node);
            else
                trvEntity.Nodes.Insert(index, l.Node);

            l.Index = index;
            trvEntity.Nodes[trvEntity.Nodes.Count - 1].EnsureVisible();
        }
        protected virtual void Layer_OnRemoveItem(ObservableList<Layer> sender, int index, Layer l)
        {
            if (trvEntity.IsDisposed)
                return;
            trvEntity.BeginUpdate();
            l.OnAddItem -= Entity_OnAddItem;
            l.OnRemoveItem -= Entity_OnRemoveItem;
            trvEntity.Nodes.Remove(l.Node);
            trvEntity.EndUpdate();
        }
        protected virtual void Entity_OnAddItem(ObservableList<IEntity> sender, int index, IEntity e)
        {
            if (trvEntity.IsDisposed)
                return;
            
            e.Node.Tag = e;
            var layer = sender as Layer;
            e.Node.Text = $"{e.ToString()}";
            if (layer.Node.Nodes.Count == index)
                layer.Node.Nodes.Add(e.Node);
            else
            {
                layer.Node.Nodes.Insert(index, e.Node);
            }
            e.Owner = layer;
            e.Index = index;
            trvEntity.Nodes[trvEntity.Nodes.Count - 1].EnsureVisible();
        }
        protected virtual void Entity_OnRemoveItem(ObservableList<IEntity> sender, int index, IEntity e)
        {
            if (trvEntity.IsDisposed)
                return;
            trvEntity.BeginUpdate();
            var layer = sender as Layer;
            layer.Node.Nodes.Remove(e.Node);
            trvEntity.EndUpdate();
        }
        /// <summary>
        /// 마우스 선택, 트리뷰 선택 등의 이벤트 발생시 내부 action 에 의해 콜백됨
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="list"></param>
        protected virtual void Action_OnSelectedEntityChanged(IDocument doc, List<IEntity> list)
        {
            if ( trvEntity.IsDisposed)
                return;
            trvEntity.Invoke(new MethodInvoker(delegate ()
            {
                var nodes = new List<TreeNode>(list.Count);
                trvEntity.BeginUpdate();
                foreach (var e in list)
                {
                    nodes.Add(e.Node);
                }
                if (nodes.Count > 0)
                    trvEntity.SelectedNodes = nodes;
                else
                    trvEntity.SelectedNodes = null;
                if (nodes.Count > 0)
                    nodes[nodes.Count - 1].EnsureVisible();
                trvEntity.EndUpdate();
                trvEntity.Refresh();

                lblEntityCount.Text = $"Selected: {list.Count.ToString()}";
                if (0 == list.Count)
                    this.ppgEntity.SelectedObjects = null;
                else
                    this.ppgEntity.SelectedObjects = list.ToArray();
            }));

        }
        /// <summary>
        /// 트리뷰에서 엔티티 선택시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void trvEntity_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (trvEntity.IsDisposed)
                return;
            trvEntity.Invoke(new MethodInvoker(delegate ()
            {
                var list = new List<IEntity>(trvEntity.SelectedNodes.Count);
                foreach (var layer in Document.Layers)
                {
                    if (trvEntity.SelectedNodes.Contains(layer.Node)) // O(N*N)
                        list.Add(layer);
                    foreach (var entity in layer)
                        if (trvEntity.SelectedNodes.Contains(entity.Node)) // O(N*N)
                            list.Add(entity);
                }

                this.Document.Action.ActEntitySelect(list);
                this.GroupButtonEnableOrNot();
            }));
        }
        protected virtual void trvEntity_DragEnter(object sender, DragEventArgs e)
        {
            bool layer = false;
            foreach (var entity in Document.Action.SelectedEntity)
            {
                if (entity is Layer)
                {
                    layer = true;
                    break;
                }
            }
            if (layer)
                e.Effect = DragDropEffects.None;
            else
                e.Effect = e.AllowedEffect;
        }
        protected virtual void trvEntity_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                DoDragDrop(e.Item, DragDropEffects.Move);
        }
        protected virtual void trvEntity_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(TreeNode)))
                return;

            var pos = trvEntity.PointToClient(new System.Drawing.Point(e.X, e.Y));
            TreeNode targetNode = trvEntity.GetNodeAt(pos);
            if (trvEntity.SelectedNodes.Count == 1)
                if (trvEntity.SelectedNodes[0].Equals(targetNode))
                    return;

            trvEntity.BeginUpdate();
            var targetEntity = (IEntity)targetNode.Tag;
            Layer targetLayer = null;

            if (targetEntity is Layer)
            {
                targetLayer = targetEntity as Layer;

                if (trvEntity.SelectedNodes.Count == 1)
                {
                    if (trvEntity.SelectedNodes[0].Tag is Layer)
                    {
                        Document.Action.ActLayerDragMove(trvEntity.SelectedNodes[0].Tag as Layer, targetLayer, targetNode.Index);
                        trvEntity.EndUpdate();
                        return;
                    }
                }
                Document.Action.ActEntityDragMove(this.Document.Action.SelectedEntity, targetLayer, 0);
            }
            else
            {
                targetLayer = targetEntity.Owner as Layer;
                Document.Action.ActEntityDragMove(this.Document.Action.SelectedEntity, targetLayer, targetNode.Index);
            }
            trvEntity.EndUpdate();
        }
        protected virtual void trvEntity_DragOver(object sender, DragEventArgs e)
        {
            var p = trvEntity.PointToClient(new System.Drawing.Point(e.X, e.Y));
            TreeNode targetNode = trvEntity.GetNodeAt(p);
            if (targetNode == null)
                e.Effect = DragDropEffects.None;
            else
                e.Effect = DragDropEffects.Move;
        }
        protected virtual void ppgEntity_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            string propertyName = e.ChangedItem.PropertyDescriptor.Name;
            var oldValue = e.OldValue;
            var newValue = e.ChangedItem.Value;
            Document.Action.ActEntityPropertyChanged(Document.Action.SelectedEntity, propertyName, oldValue, newValue);
        }

        #region 툴바 버튼
        protected virtual void btnNew_Click(object sender, EventArgs e)
        {
            if (null != this.OnDocumentNew)
            {
                this.OnDocumentNewing();
            }
            else
            {
                if (DialogResult.Yes != MessageBox.Show($"Do you really want to clear the document ?", "Document New", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    return;
                this.OnNew();
            }
        }
        /// <summary>
        /// 문서를 신규로 생성시 호출되는 내부 함수
        /// OnDocumentNew 이벤트 핸들러 사용시 사용자의 이벤트 함수내에서 특정 작업후
        /// 반드시 OnNew를 호출해 주어야 한다
        /// </summary>
        public virtual void OnNew()
        {
            trvEntity.BeginUpdate();
            trvEntity.Nodes.Clear();
            this.FileName = string.Empty;
            this.Document.Action.ActNew();
            trvEntity.EndUpdate();
            chbLock.Checked = this.Document.IsEditorLock;
        }
        protected virtual void btnOpen_Click(object sender, EventArgs e)
        {
            if (null != this.OnDocumentOpen)
            {
                this.OnDocumentOpening();
            }
            else
            {
                var dlg = new OpenFileDialog();
                dlg.Title = "Open File";
                dlg.Filter = "Supported files (*.sirius, *.dxf)|*.sirius;*.dxf|sirius data files (*.sirius)|*.sirius|dxf cad files (*.dxf)|*.dxf|All Files (*.*)|*.*";
                dlg.FileName = string.Empty;
                dlg.Multiselect = false;
                DialogResult result = dlg.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    ppgEntity.SelectedObject = null;
                    this.OnOpen(dlg.FileName);
                }
            }
        }
        /// <summary>
        /// OnDocumentOpen 이벤트 핸들러 사용시 사용자의 이벤트 함수내에서 특정 작업후
        /// 반드시 OnOpen 호출해 주어야 한다
        /// </summary>
        /// <param name="fileName">파일 이름</param>
        /// <returns></returns>
        public virtual bool OnOpen(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            if (0 == string.Compare(ext, ".dxf", true))
            {
                trvEntity.BeginUpdate();
                var doc = DocumentSerializer.OpenDxf(fileName);
                trvEntity.EndUpdate();
                if (null == doc)
                {
                    MessageBox.Show($"Fail to open : {fileName}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                this.Document = doc;
                this.FileName = fileName;
                return true;
            }
            else if (0 == string.Compare(ext, ".sirius", true))
            {
                trvEntity.BeginUpdate();
                var doc = DocumentSerializer.OpenSirius(fileName);
                trvEntity.EndUpdate();
                if (null == doc)
                {
                    return false;
                }
                this.Document = doc;
                this.FileName = fileName;
                return true;
            }
            MessageBox.Show($"Unsupported file extension: {fileName}", "File Type Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        protected virtual void btnImport_Click(object sender, EventArgs e)
        {
            var form = new SiriusPreviewForm();
            DialogResult result = form.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                if (null != form.Entity)
                {
                    ppgEntity.SelectedObject = null;
                    this.Document.Action.ActEntityAdd(form.Entity);
                }
            }
        }
        protected virtual void btnSave_Click(object sender, EventArgs e)
        {
            if (null != this.OnDocumentSave)
            {
                this.OnDocumentSaving();
            }
            else
            {
                if (string.IsNullOrEmpty(this.FileName))
                {
                    this.btnSaveAs_Click(null, EventArgs.Empty);
                }
                else if (true == this.OnSave(this.FileName))
                {
                    MessageBox.Show($"Success to save : {this.FileName}", "Document Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Fail to save : {this.FileName}", "Document Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// OnDocumentSave 이벤트 핸들러 사용시 사용자의 이벤트 함수내에서 특정 작업후
        /// 반드시 OnSave 호출해 주어야 한다
        /// </summary>
        /// <param name="fileName">파일 이름</param>
        /// <returns></returns>
        public virtual bool OnSave(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                this.btnSaveAs_Click(null, EventArgs.Empty);
                return true;
            }
            else
            {
                this.FileName = fileName;
                return this.Document.Action.ActSave(fileName);
            }
        }
        protected virtual void btnSaveAs_Click(object sender, EventArgs e)
        {
            if (null != this.OnDocumentSaveAs)
            {
                this.OnDocumentSaveAsing();
            }
            else
            {
                var dlg = new SaveFileDialog();
                dlg.Filter = "Sirius data files (*.sirius)|*.sirius|All Files (*.*)|*.*";
                dlg.Title = "Save As ...";
                dlg.FileName = string.Empty;
                DialogResult result = dlg.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    if (true == this.OnSaveAs(dlg.FileName))
                    {
                        MessageBox.Show($"Success to save : {this.FileName}", "Document Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Fail to save : {this.FileName}", "Document Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        /// <summary>
        /// OnDocumentSaveAs 이벤트 핸들러 사용시 사용자의 이벤트 함수내에서 특정 작업후
        /// 반드시 OnSaveAs 호출해 주어야 한다
        /// </summary>
        /// <param name="fileName">파일 이름</param>
        /// <returns></returns>
        public virtual bool OnSaveAs(string fileName)
        {
            this.Document.Action.ActSave(fileName);
            this.FileName = fileName;
            return true;
        }
        /// <summary>
        /// OnDocumentLayerNew 이벤트 사용시 사용자가 원하는 레이어(Layer) 생성후 OnLayerNew 함수를 통해 생성된 레이어 개체를 전달해 주어야 한다
        /// </summary>
        /// <param name="layer">Layer 상속 구현 객체</param>
        /// <returns></returns>
        public virtual bool OnLayerNew(Layer layer)
        {
            if (null == layer)
                return false;
            return this.Document.Action.ActEntityAdd(layer);
        }
        protected virtual void btnLayer_Click(object sender, EventArgs e)
        {
            if (null != OnDocumentLayerNew)
            {
                OnDocumentLayerNewing();
            }
            else
            {
                var layer = new Layer($"NoName{this.Document.Action.NewLayerIndex++}");
                var form = new PropertyForm(layer);
                if (DialogResult.OK != form.ShowDialog(this))
                    return;
                this.OnLayerNew(layer);
            }
        }

        protected virtual void btnUndo_Click(object sender, EventArgs e)
        {
            trvEntity.BeginUpdate();
            Document.Action.ActUndo();
            trvEntity.EndUpdate();
        }
        protected virtual void btnReDo_Click(object sender, EventArgs e)
        {
            trvEntity.BeginUpdate();
            Document.Action.ActRedo();
            trvEntity.EndUpdate();
        }
        protected virtual void btnZoomOut_Click(object sender, EventArgs e)
        {
            if (null == Document.Action.SelectedEntity || 0 == Document.Action.SelectedEntity.Count)
                this.view?.OnZoomOut(new System.Drawing.Point(GLcontrol.Width / 2, GLcontrol.Height / 2));
            else
            {
                var br = new BoundRect();
                foreach (var entity in Document.Action.SelectedEntity)
                    br.Union(entity.BoundRect);
                this.view?.OnZoomOut(br);
            }
        }
        protected virtual void btnZoomIn_Click(object sender, EventArgs e)
        {
            if (null == Document.Action.SelectedEntity || 0 == Document.Action.SelectedEntity.Count)
                this.view?.OnZoomIn(new System.Drawing.Point(GLcontrol.Width / 2, GLcontrol.Height / 2));
            else
            {
                var br = new BoundRect();
                foreach (var entity in Document.Action.SelectedEntity)
                    br.Union(entity.BoundRect);
                this.view?.OnZoomIn(br);
            }
        }
        protected virtual void btnZoomFit_Click(object sender, EventArgs e)
        {
            var br = new BoundRect();
            if (null != Document.Action.SelectedEntity)
                foreach (var entity in Document.Action.SelectedEntity)
                    br.Union(entity.BoundRect);
            if (!br.IsEmpty)
                this.view?.OnZoomFit(br);
            else
                this.view?.OnZoomFit();
        }
        protected virtual void btnPan_Click(object sender, EventArgs e)
        {
            this.view?.OnPan(chbPan.Checked);
        }
        protected virtual void btnPoint_Click(object sender, EventArgs e)
        {
            var point = new SpiralLab.Sirius.Point(0,0);
            var form = new PropertyForm(point);
            if (DialogResult.OK != form.ShowDialog(this))
                return;

            this.Document.Action.ActEntityAdd(point);
        }
        protected virtual void btnPoints_Click(object sender, EventArgs e)
        {
            var points = new SpiralLab.Sirius.Points();
            Random rand = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < 100; i++)
            {
                points.Add(new Vertex(rand.Next(100) - 50, rand.Next(100) - 50));
            }
            var form = new PropertyForm(points);
            if (DialogResult.OK != form.ShowDialog(this))
                return;

            this.Document.Action.ActEntityAdd(points);
        }
        protected virtual void btnRaster_Click(object sender, EventArgs e)
        {
            var raster = new Raster();
            var form = new PropertyForm(raster);
            if (DialogResult.OK != form.ShowDialog(this))
                return;

            this.Document.Action.ActEntityAdd(raster);
        }
        protected virtual void btnLine_Click(object sender, EventArgs e)
        {
            var line = new Line(0, 0, 10, 10);
            var form = new PropertyForm(line);
            if (DialogResult.OK != form.ShowDialog(this))
                return;

            this.Document.Action.ActEntityAdd(line);
        }
        protected virtual void btnArc_Click(object sender, EventArgs e)
        {
            var arc = new Arc(0, 0, 10, 0, 90);
            var form = new PropertyForm(arc);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(arc);
        }
        protected virtual void btnCircle_Click(object sender, EventArgs e)
        {
            var circle = new Circle(0, 0, 20);
            var form = new PropertyForm(circle);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(circle);
        }
        protected virtual void btnTrepan_Click(object sender, EventArgs e)
        {
            var trepan = new Trepan(0, 0, 10, 2);
            var form = new PropertyForm(trepan);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(trepan);
        }
        protected virtual void btnTriangle_Click(object sender, EventArgs e)
        {
            var triangle = new Triangle(0, 0, 5, 5);
            var form = new PropertyForm(triangle);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(triangle);
        }
        protected virtual void btnRectangle_Click(object sender, EventArgs e)
        {
            var rectangle = new Rectangle(0, 0, 20, 20);
            var form = new PropertyForm(rectangle);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(rectangle);
        }
        protected virtual void btnLWPolyline_Click(object sender, EventArgs e)
        {
            var poly = new LwPolyline();
            poly.Add(new LwPolyLineVertex(55.3005f, 125.1903f, 0));
            poly.Add(new LwPolyLineVertex(80.5351f, 161.2085f, 0));
            poly.Add(new LwPolyLineVertex(129.8027f, 148.6021f, -1.3108f));
            poly.Add(new LwPolyLineVertex(107.5722f, 109.5824f, 0.8155f));
            poly.Add(new LwPolyLineVertex(77.5310f, 89.7724f, 0));
            poly.IsClosed = true;
            var form = new PropertyForm(poly);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(poly);
        }
        protected virtual void mnuMOTFBeginEnd_Click(object sender, EventArgs e)
        {
            {
                var motfBegin = new MotfBegin();
                var form = new PropertyForm(motfBegin);
                if (DialogResult.OK != form.ShowDialog(this))
                    return;
                this.Document.Action.ActEntityAdd(motfBegin);
                var motfEnd = new MotfEnd();
                this.Document.Action.ActEntityAdd(motfEnd);
            }
        }
        protected virtual void mnuOTFExtStartDelay_Click(object sender, EventArgs e)
        {
            var motf = new MotfExternalStartDelay();
            var form = new PropertyForm(motf);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(motf);
        }
        protected virtual void mnuMOTFWait_Click(object sender, EventArgs e)
        {
            var motf = new MotfWait();
            var form = new PropertyForm(motf);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(motf);
        }
        protected virtual void mnuMOTFCall_Click(object sender, EventArgs e)
        {
            var motf = new MotfRepeat();
            var form = new PropertyForm(motf);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(motf);
        }
        protected virtual void btnTimer_Click(object sender, EventArgs e)
        {
            var timer = new SpiralLab.Sirius.Timer();
            var form = new PropertyForm(timer);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(timer);
        }
        protected virtual void mnuVectorBeginEnd_Click(object sender, EventArgs e)
        {
            {
                var vec = new VectorBegin();
                var form = new PropertyForm(vec);
                if (DialogResult.OK != form.ShowDialog(this))
                    return;
                this.Document.Action.ActEntityAdd(vec);
            }
            {
                var vec = new VectorEnd();
                //var form = new PropertyForm(vec);
                //if (DialogResult.OK != form.ShowDialog(this))
                //    return;
                this.Document.Action.ActEntityAdd(vec);
            }
        }
        protected virtual void mnuPoDSyncAxisBeginEnd_Click(object sender, EventArgs e)
        {
            {
                var vec = new AlcSyncAxisBegin();
                var form = new PropertyForm(vec);
                if (DialogResult.OK != form.ShowDialog(this))
                    return;
                this.Document.Action.ActEntityAdd(vec);
            }
            {
                var vec = new AlcSyncAxisEnd();
                //var form = new PropertyForm(vec);
                //if (DialogResult.OK != form.ShowDialog(this))
                //    return;
                this.Document.Action.ActEntityAdd(vec);
            }
        }
        protected virtual void mnuPoDBeginEnd_Click(object sender, EventArgs e)
        {
            var vec = new AlcBegin();
            var form = new PropertyForm(vec);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(vec);
        }
        protected virtual void btnSpiral_Click(object sender, EventArgs e)
        {
            var spiral = new Spiral(2, 10, 10, true);
            var form = new PropertyForm(spiral);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(spiral);
        }
        protected virtual void btnSiriusText_Click(object sender, EventArgs e)
        {
            var text = new SiriusText("HELLO");
            var form = new PropertyForm(text);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(text);
        }
        protected virtual void mnuTime_Click(object sender, EventArgs e)
        {
            var text = new SiriusTextTime();
            var form = new PropertyForm(text);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(text);
        }
        protected virtual void mnuDate_Click(object sender, EventArgs e)
        {
            var text = new SiriusTextDate();
            var form = new PropertyForm(text);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(text);
        }
        protected virtual void mnuSerial_Click(object sender, EventArgs e)
        {
            var text = new SiriusTextSerial();
            var form = new PropertyForm(text);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(text);
        }
        protected virtual void mnuSirius_Click(object sender, EventArgs e)
        {
            var text = new SiriusText("HELLO");
            var form = new PropertyForm(text);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(text);
        }
        protected virtual void btnRotateCW_Click(object sender, EventArgs e)
        {
            var br = new BoundRect();
            foreach (var entity in this.Document.Action.SelectedEntity)
                br.Union(entity.BoundRect);
            if (br.IsEmpty)
                return;

            this.Document.Action.ActEntityRotate(this.Document.Action.SelectedEntity, -90, br.Center.X, br.Center.Y);
        }
        protected virtual void btnRotateCCW_Click(object sender, EventArgs e)
        {
            var br = new BoundRect();
            foreach (var entity in this.Document.Action.SelectedEntity)
                br.Union(entity.BoundRect);
            if (br.IsEmpty)
                return;

            this.Document.Action.ActEntityRotate(this.Document.Action.SelectedEntity, 90, br.Center.X, br.Center.Y);
        }
        protected virtual void btnRotateCustom_Click(object sender, EventArgs e)
        {
            var br = new BoundRect();
            foreach (var entity in this.Document.Action.SelectedEntity)
                br.Union(entity.BoundRect);
            if (br.IsEmpty)
                return;

            var form = new RotateForm(this.Document, br.Center.X, br.Center.Y);
            form.ShowDialog(this);
        }
        protected virtual void mnuOrigin_Click(object sender, EventArgs e)
        {
            this.Document.Action.ActEntityAlign(this.Document.Action.SelectedEntity, EntityAlign.Origin);
        }
        protected virtual void mnuLeft_Click(object sender, EventArgs e)
        {
            this.Document.Action.ActEntityAlign(this.Document.Action.SelectedEntity, EntityAlign.Left);
        }
        protected virtual void mnuRight_Click(object sender, EventArgs e)
        {
            this.Document.Action.ActEntityAlign(this.Document.Action.SelectedEntity, EntityAlign.Right);
        }
        protected virtual void mnuTop_Click(object sender, EventArgs e)
        {
            this.Document.Action.ActEntityAlign(this.Document.Action.SelectedEntity, EntityAlign.Top);
        }
        protected virtual void mnuBottom_Click(object sender, EventArgs e)
        {
            this.Document.Action.ActEntityAlign(this.Document.Action.SelectedEntity, EntityAlign.Bottom);
        }
        protected virtual void mnuBottomTop_Click(object sender, EventArgs e)
        {
            this.Document.Action.ActEntitySort(this.Document.Action.SelectedEntity, this.Document.Layers.Active, EntitySort.BottomToTop);
        }
        protected virtual void mnuTopBottom_Click(object sender, EventArgs e)
        {
            this.Document.Action.ActEntitySort(this.Document.Action.SelectedEntity, this.Document.Layers.Active, EntitySort.TopToBottom);
        }
        protected virtual void mnuLeftRight_Click(object sender, EventArgs e)
        {
            this.Document.Action.ActEntitySort(this.Document.Action.SelectedEntity, this.Document.Layers.Active, EntitySort.LeftToRight);
        }
        protected virtual void mnuRightLeft_Click(object sender, EventArgs e)
        {
            this.Document.Action.ActEntitySort(this.Document.Action.SelectedEntity, this.Document.Layers.Active, EntitySort.RightToLeft);
        }
        protected virtual void btnDocumentInfo_Click(object sender, EventArgs e)
        {
            var form = new PropertyForm(this.Document);
            form.PropertyGrid.PropertyValueChanged += Document_PropertyValueChanged;
            try
            {
                if (DialogResult.OK != form.ShowDialog(this))
                    return;
            }
            finally
            {
                form.PropertyGrid.PropertyValueChanged -= Document_PropertyValueChanged;
            }
        }
        protected virtual void Document_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            string propertyName = e.ChangedItem.PropertyDescriptor.Name;
            var oldValue = e.OldValue;
            var newValue = e.ChangedItem.Value;
            Document.Action.ActDocumentPropertyChanged(Document, propertyName, oldValue, newValue);
        }
        protected virtual void btnExplode_Click(object sender, EventArgs e)
        {
            trvEntity.BeginUpdate();
            this.Document.Action.ActEntityExplode(this.Document.Action.SelectedEntity);
            trvEntity.EndUpdate();
        }
        protected virtual void btnHatch_Click(object sender, EventArgs e)
        {
            if (null == this.Document.Action.SelectedEntity || 0 == this.Document.Action.SelectedEntity.Count)
            {
                MessageBox.Show($"Please select target entity at first", "Hatch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var form = new HatchForm();
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityHatch(this.Document.Action.SelectedEntity, form.Mode, form.ZigZag, form.Angle, form.Angle2, form.Interval, form.Exclude, form.Shift);
        }
        protected virtual void btnDelete_Click(object sender, EventArgs e)
        {
            trvEntity.BeginUpdate();
            this.Document.Action.ActEntityDelete(this.Document.Action.SelectedEntity);
            trvEntity.EndUpdate();
        }
        protected virtual void btnCopy_Click(object sender, EventArgs e)
        {
            Document.Action.ActEntityCopy(Document.Action.SelectedEntity);
        }
        protected virtual void btnCut_Click(object sender, EventArgs e)
        {
            Document.Action.ActEntityCut(Document.Action.SelectedEntity);
        }
        protected virtual void btnPaste_Click(object sender, EventArgs e)
        {
            if (Action.ClipBoard.Count <= 0)
            {
                MessageBox.Show($"No Data in ClipBoard", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.view.Dp2Lp(this.currentMousePos, out float x, out float y);
            Document.Action.ActEntityPaste( Document.Layers.Active, x, y);
        }
        protected virtual void btnPasteArray_Click(object sender, EventArgs e)
        {
            if (Action.ClipBoard.Count <=0)
            {
                MessageBox.Show($"No Data in ClipBoard", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var form = new PasteForm();
            form.Clipboard = Action.ClipBoard;
            DialogResult result = form.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                //this.view.Dp2Lp(this.CurrentMousePos, out float x, out float y);
                trvEntity.BeginUpdate();
                this.Document.Action.ActEntityPasteArray(Document.Layers.Active, form.Result);
                trvEntity.EndUpdate();
            }
        }
        protected virtual void btnPasteClone_Click(object sender, EventArgs e)
        {
            if (Action.ClipBoard.Count <= 0)
            {
                MessageBox.Show($"No Data in ClipBoard", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Document.Action.ActEntityPasteClone(Document.Layers.Active);
        }
        protected virtual void btnBarcode1D_Click(object sender, EventArgs e)
        {
            var bcd = new Barcode1D("123456789");
            var form = new PropertyForm(bcd);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(bcd);
        }
        protected virtual void mnuDataMatrix_Click(object sender, EventArgs e)
        {
            var text = new BarcodeDataMatrix2("SIRIUS");
            var form = new PropertyForm(text);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(text);
        }
        protected virtual void mnuQRCode_Click(object sender, EventArgs e)
        {
            var text = new BarcodeQR2("SIRIUS");
            var form = new PropertyForm(text);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(text);
        }
        protected virtual void btnHPGL_Click(object sender, EventArgs e)
        {
            btnImport_Click(sender, e);
        }
        protected virtual void btmImage_Click(object sender, EventArgs e)
        {
            ofd.Filter = "";
            ofd.Multiselect = false;
            ofd.Filter = "Supported image files (*.bmp, *.png, *.jpg, *.jpeg, *.gif, *.tif)|*.bmp;*.png;*.jpg;*.jpeg;*.gif;*.tif|All Files (*.*)|*.*";
            ofd.DefaultExt = ".bmp"; 
            ofd.Title = "Import Image File";
            ofd.FileName = string.Empty;
            DialogResult result = ofd.ShowDialog(this);
            if (result != DialogResult.OK)
                return;
            var bitmap = new SpiralLab.Sirius.Bitmap(ofd.FileName);
            if (null != bitmap)
                this.Document.Action.ActEntityAdd(bitmap);
        }
        protected virtual void mnuStitchedImage_Click(object sender, EventArgs e)
        {
            var stitchedImage = new StitchedImage();
            var form = new PropertyForm(stitchedImage);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(stitchedImage);
        }
        protected virtual void mnuLoadCellImages_Click(object sender, EventArgs e)
        {
            if (0 == Document.Action.SelectedEntity.Count)
                return;
            var stitchedImage = Document.Action.SelectedEntity[0] as StitchedImage;
            if (null == stitchedImage)
                return;

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            string sep = string.Empty;
            ofd.Filter = "";
            foreach (var c in codecs)
            {
                string codecName = c.CodecName.Substring(8).Replace("Codec", "Files").Trim();
                ofd.Filter = String.Format("{0}{1}{2} ({3})|{3}", ofd.Filter, sep, codecName, c.FilenameExtension);
                sep = "|";
            }
            ofd.Filter = String.Format("{0}{1}{2} ({3})|{3}", ofd.Filter, sep, "All Files", "*.*");
            ofd.DefaultExt = ".bmp"; 
            ofd.Title = "Import Image File";
            ofd.FileName = string.Empty;
            ofd.Multiselect = true;
            DialogResult result = ofd.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            Array.Sort(ofd.FileNames);
            for (int i = 0; i < ofd.FileNames.Length; i++)
                stitchedImage.UpdateImage(i, ofd.FileNames[i]);

            this.view.Render();
        }
        protected virtual void mnuClearCells_Click(object sender, EventArgs e)
        {
            if (0 == Document.Action.SelectedEntity.Count)
                return;
            var stitchedImage = Document.Action.SelectedEntity[0] as StitchedImage;
            if (null == stitchedImage)
                return;
            stitchedImage.ClearImages();
            this.view.Render();
        }
        protected virtual void btnSiriusArcText_Click(object sender, EventArgs e)
        {
            var text = new SiriusTextArc("HELLO");
            var form = new PropertyForm(text);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(text);
        }
        protected virtual void mnuPenReturn_Click(object sender, EventArgs e)
        {
            var layer = this.Document.Layers.Active;
            uint counts = 0;
            foreach (var entity in layer)
                if (entity is IPen)
                    counts++;
            if (counts < 2)
            {
                MessageBox.Show($"Pen entity counts must be greater than 2");
                return;
            }
            var pen = new PenReturn();
            //var form = new PropertyForm(pen);
            //if (DialogResult.OK != form.ShowDialog(this))
            //    return;
            this.Document.Action.ActEntityAdd(pen);
        }
        protected virtual void btnEllipse_Click(object sender, EventArgs e)
        {
            var ellipse = new Ellipse(0, 0, 10, 6, 0, 360, 0);
            var form = new PropertyForm(ellipse);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(ellipse);
        }
        protected virtual void btnJump_Click(object sender, EventArgs e)
        {
            var jump = new SpiralLab.Sirius.Jump();
            var form = new PropertyForm(jump);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(jump);
        }
        protected virtual void mnuWriteData_Click(object sender, EventArgs e)
        {
            var data = new WriteData();
            var form = new PropertyForm(data);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(data);
        }
        protected virtual void mnuWriteExt16_Click(object sender, EventArgs e)
        {
            var data = new WriteDataExt16();
            var form = new PropertyForm(data);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(data);
        }
        protected virtual void mnuWriteDataExt16If_Click(object sender, EventArgs e)
        {
            var data = new WriteDataExt16If();
            var form = new PropertyForm(data);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(data);
        }
        protected virtual void waitExt16IfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var data = new WaitDataExt16If();
            var form = new PropertyForm(data);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(data);
        }
        protected virtual void mnuText_Click(object sender, EventArgs e)
        {
            var text = new Text("HELLO");
            var form = new PropertyForm(text);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(text);
        }
        protected virtual void btnTextArc_Click(object sender, EventArgs e)
        {
            var text = new TextArc("HELLO");
            var form = new PropertyForm(text);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(text);
        }
        protected virtual void mnuTextTime_Click(object sender, EventArgs e)
        {
            var text = new TextTime();
            var form = new PropertyForm(text);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(text);
        }
        protected virtual void mnuTextDate_Click(object sender, EventArgs e)
        {
            var text = new TextDate();
            var form = new PropertyForm(text);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(text);
        }
        protected virtual void mnuTextSerial_Click(object sender, EventArgs e)
        {
            var text = new TextSerial();
            var form = new PropertyForm(text);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(text);
        }
        protected virtual void mnuZOffset_Click(object sender, EventArgs e)
        {
            var zOffset = new ZOffset();
            var form = new PropertyForm(zOffset);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(zOffset);
        }
        protected virtual void mnuZDefocus_Click(object sender, EventArgs e)
        {
            var zDefocus = new ZDefocus();
            var form = new PropertyForm(zDefocus);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(zDefocus);
        }
        #endregion
        protected virtual void GroupButtonEnableOrNot()
        {
            bool layer = false;
            bool group = false;
            if (null == Document || null == Document.Action || null == Document.Action.SelectedEntity || 0 == Document.Action.SelectedEntity.Count)
            {
                ddbGroup.Enabled = false;
                return;
            }
            else
                ddbGroup.Enabled = true;

            foreach (var entity in Document.Action.SelectedEntity)
            {
                if (entity is Layer)
                    layer |= true;
                else if (entity is Group)
                    group |= true;
            }

            if (layer)
            {
                mnuGroup.Enabled = false;
                mnuGroupOffset.Enabled = false;
                mnuUnGroup.Enabled = false;
            }
            else
            {
                mnuGroup.Enabled = true;
                if (Document.Action.SelectedEntity.Count > 0)
                    mnuGroupOffset.Enabled = true;
                mnuUnGroup.Enabled = true;
            }
        }
        protected virtual void mnuGroup_Click(object sender, EventArgs e)
        {
            trvEntity.BeginUpdate();
            Document.Action.ActEntityGroup(Document.Action.SelectedEntity, Document.Layers.Active);
            trvEntity.EndUpdate();
        }
        protected virtual void mnuGroupOffset_Click(object sender, EventArgs e)
        {
            trvEntity.BeginUpdate();
            Document.Action.ActEntityGroupWithOffsets(Document.Action.SelectedEntity, Document.Layers.Active);
            trvEntity.EndUpdate();
        }
        protected virtual void mnuUngroup_Click(object sender, EventArgs e)
        {
            trvEntity.BeginUpdate();
            Document.Action.ActEntityUngroup(Document.Action.SelectedEntity);
            trvEntity.EndUpdate();
        }

        Form markerForm = null;
        protected virtual void mnuMarker_Click(object sender, EventArgs e)
        {
            if (null != this.OnMarker)
            {
                this.OnMarkerShow();
            }
            else
            {
                if (null == this.Document)
                {
                    MessageBox.Show($"Document is not assigned ", "WARNING!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (null == this.Laser)
                {
                    MessageBox.Show($"Laser source is not assigned ", "WARNING!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (null == this.Rtc)
                {
                    MessageBox.Show($"Rtc controller is not assigned ", "WARNING!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (null == this.Marker)
                {
                    MessageBox.Show($"Marker is not assigned ", "WARNING!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (null == this.Marker.MarkerArg)
                    return;
                if (null != this.markerForm)
                    return; //already showing...
                if (this.Marker.IsBusy)
                {
                    if (DialogResult.Yes == MessageBox.Show($"Do you really want to abort the mark process ?", "WARNING!!! LASER IS BUSY", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        this.Marker.Stop();
                        //0 ,0 으로 초기화 (이전 오프셋이 남아 있으므로)
                        this.marker.MarkerArg.Offsets.Clear();
                    }
                    else
                        return; //no control ! no download
                 }
                else
                {
                }

                var rtcSyncAxis = this.Rtc as IRtcSyncAxis;
                if (null != rtcSyncAxis)
                {
                    var form = new CustomMarkerSyncAxisForm();
                    form.AliasName = this.Marker?.Name;
                    form.Marker = this.Marker;
                    form.Document = this.Document;
                    form.Rtc = this.Rtc;
                    form.Laser = this.Laser;
                    form.MotorZ = this.MotorZ;
                    form.Editor = this; 
                    form.PowerMap = this.PowerMap;
                    form.FormClosed += Form_FormClosed;
                    form.Show(this);
                    form.BringToFront();
                    this.markerForm = form;
                }
                else
                {
                    var form = new CustomMarkerForm();
                    form.AliasName = this.Marker?.Name;
                    form.Marker = this.Marker;
                    form.Document = this.Document;
                    form.Rtc = this.Rtc;
                    form.Laser = this.Laser;
                    form.MotorZ = this.MotorZ;
                    form.Editor = this; 
                    form.PowerMap = this.PowerMap;
                    form.FormClosed += Form_FormClosed;
                    form.Show(this);
                    form.BringToFront();
                    this.markerForm = form;
                }
            }
        }

        protected Laser.LaserForm laserForm = null;
        protected virtual void mnuLaser_Click(object sender, EventArgs e)
        {
            if (null != this.OnLaser)
            {
                this.OnLaserShow();
            }
            else
            {
                if (null == this.Laser)
                    return;
                if (null == laserForm || laserForm.IsDisposed)
                {
                    this.laserForm = new Laser.LaserForm(this.Laser);
                }
                laserForm.Show();
                laserForm.BringToFront();
            }
        }

        protected Correction2DRtcForm correction2DForm = null;
        protected virtual void mnuCorrection2D_Click(object sender, EventArgs e)
        {
            if (null != this.OnCorrection2D)
            {
                this.OnCorrection2DShow();
            }
            else
            {
                if (null == this.Rtc)
                    return;

                if (null == correction2DForm || correction2DForm.IsDisposed)
                {
                    int rows = 5;
                    int cols = 5;
                    float interval = 10.0f;
                    var correction2D = new Correction2DRtc(this.Rtc.KFactor, rows, cols, interval, interval, this.Rtc.CorrectionFiles[0].FileName, string.Empty);
                    float left = -interval * (float)(int)(cols / 2);
                    float top = interval * (float)(int)(rows / 2);
                    var rand = new Random();
                    for (int row = 0; row < rows; row++)
                    {
                        for (int col = 0; col < cols; col++)
                        {
                            correction2D.AddRelative(row, col,
                                new Vector2(
                                    left + col * interval,
                                    top - row * interval),
                                new Vector2(
                                    rand.Next(20) / 1000.0f - 0.01f,
                                    rand.Next(20) / 1000.0f - 0.01f)
                                );
                        }
                    }
                    this.correction2DForm = new Correction2DRtcForm(correction2D);
                }
                correction2DForm.Show();
                correction2DForm.BringToFront();
            }
        }

        protected Correction3DRtcForm correction3DRtcForm = null;
        protected virtual void mnuCorrection3D_Click(object sender, EventArgs e)
        {
            if (null != this.OnCorrection3D)
            {
                this.OnCorrection3DShow();
            }
            else
            {
                if (null == this.Rtc)
                    return;
                
                if (null == correction2DForm || correction2DForm.IsDisposed)
                {
                    int rows = 3;
                    int cols = 3;
                    float interval = 30;
                    float upper = 0;
                    float lower = -20;
                    var correction3D = new Correction3DRtc(this.Rtc.KFactor, rows, cols, interval, upper, lower, this.Rtc.CorrectionFiles[0].FileName, string.Empty);
                    float left = -interval * (float)(int)(cols / 2);
                    float top = interval * (float)(int)(rows / 2);
                    var rand = new Random();
                    for (int row = 0; row < rows; row++)
                    {
                        for (int col = 0; col < cols; col++)
                        {
                            correction3D.AddRelative(row, col,
                                new Vector3(
                                    left + col * interval,
                                    top - row * interval,
                                    upper),
                                new Vector3(
                                    rand.Next(20) / 1000.0f - 0.01f,
                                    rand.Next(20) / 1000.0f - 0.01f,
                                    upper)
                                );
                        }
                    }
                    for (int row = 0; row < rows; row++)
                    {
                        for (int col = 0; col < cols; col++)
                        {
                            correction3D.AddRelative(row, col,
                                new Vector3(
                                    left + col * interval,
                                    top - row * interval,
                                    lower),
                                new Vector3(
                                    rand.Next(20) / 1000.0f - 0.01f,
                                    rand.Next(20) / 1000.0f - 0.01f,
                                    lower)
                                );
                        }
                    }
                    this.correction3DRtcForm = new Correction3DRtcForm(correction3D);
                }
                correction3DRtcForm.Show();
                correction3DRtcForm.BringToFront();
            }
        }
        protected virtual void mnuMeasurementFilePlotTool_Click(object sender, EventArgs e)
        {
            if (null != this.OnMeasurement)
            {
                this.OnMeasurementShow();
            }
            else
            {
                var dlg = new OpenFileDialog();
                dlg.Filter = "measurement data files (*.txt)|*.txt|All Files (*.*)|*.*";
                dlg.Title = "Open measurement data file";
                dlg.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plot");
                DialogResult result = dlg.ShowDialog();
                if (result != DialogResult.OK)
                    return;
                MeasurementHelper.Plot(dlg.FileName);
            }
        }

        protected RtcIOForm rtcIOForm = null;
        protected virtual void mnuIO_Click(object sender, EventArgs e)
        {
            if (null != this.OnIO)
            {
                this.OnIOShow();
            }
            else
            {
                if (null == rtcIOForm || rtcIOForm.IsDisposed)
                {
                    this.rtcIOForm = new RtcIOForm(this.Rtc, RtcPin2Input, RtcPin2Output, RtcExtension1Input, RtcExtension1Output, RtcExtension2Output);
                }
                rtcIOForm.Show();
                rtcIOForm.BringToFront();
            }
        }

        protected PowerMeterForm powerMeterForm = null;
        protected virtual void mnuPowerMeter_Click(object sender, EventArgs e)
        {
            if (null != this.OnPowerMeter)
            {
                this.OnPowerMeterShow();
            }
            else
            {
                if (null != this.PowerMeter)
                {
                    if (null == powerMeterForm || powerMeterForm.IsDisposed)
                    {
                        this.powerMeterForm = new PowerMeterForm(this.PowerMeter, this.Laser);
                    }
                    powerMeterForm.Show();
                    powerMeterForm.BringToFront();
                }
            }
        }

        protected PowerMapForm powerMapForm = null;
        protected virtual void mnuPowerMap_Click(object sender, EventArgs e)
        {
            if (null != this.OnPowerMap)
            {
                this.OnPowerMapShow();
            }
            else
            {
                if (null != this.PowerMap)
                {

                    if (null == powerMapForm || powerMapForm.IsDisposed)
                    {
                        this.powerMapForm = new PowerMapForm(this.PowerMap, this.Rtc, this.Laser, this.PowerMeter);
                    }
                    powerMapForm.Show(); 
                    powerMapForm.BringToFront();
                }
            }
        }

        protected Motor.MotorForm motorForm = null;
        protected Motor.MotorsForm motorsForm = null;
        protected virtual void mnuMotor_Click(object sender, EventArgs e)
        {
            if (null != this.OnMotor)
            {
                this.OnMotorShow();
            }
            else
            {
                if (null != this.MotorZ)
                {
                    if (null == motorForm || motorForm.IsDisposed)
                    {
                        this.motorForm = new SpiralLab.Sirius.Motor.MotorForm(this.MotorZ);
                    }
                    motorForm.Show();
                    motorForm.BringToFront();
                }
                if (null != this.Motors)
                {
                    if (null == motorsForm || motorsForm.IsDisposed)
                    {
                        this.motorsForm = new SpiralLab.Sirius.Motor.MotorsForm(this.Document, this.Motors);
                    }
                    motorsForm.Show();
                    motorsForm.BringToFront();
                }
            }
        }
        protected virtual void mnuSimulateSlow_Click(object sender, EventArgs e)
        {
            Document.Action.ActEntityLaserPathSimulateStart(Document.Action.SelectedEntity, this.View, SpiralLab.Sirius.Action.LaserPathSimSpped.Slow);
        }
        protected virtual void mnuSimulateNormal_Click(object sender, EventArgs e)
        {
            Document.Action.ActEntityLaserPathSimulateStart(Document.Action.SelectedEntity, this.View, SpiralLab.Sirius.Action.LaserPathSimSpped.Normal);
        }
        protected virtual void mnuSimulateFast_Click(object sender, EventArgs e)
        {
            Document.Action.ActEntityLaserPathSimulateStart(Document.Action.SelectedEntity, this.View, SpiralLab.Sirius.Action.LaserPathSimSpped.Fast);
        }
        protected virtual void mnuSimulateStop_Click(object sender, EventArgs e)
        {
            Document.Action.ActEntityLaserPathSimulateStop();
        }
        protected virtual void mnuJog_Click(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;
            string[] tokens = ((string)item.Tag).Split(',');
            Debug.Assert(tokens.Length == 2);
            float dx = float.Parse(tokens[0]);
            float dy = float.Parse(tokens[1]);
            Document.Action.ActEntityTransit(Document.Action.SelectedEntity, dx, dy);
        }
        protected virtual void mnuLogWindow_Click(object sender, EventArgs e)
        {
            if (null == logForm || logForm.IsDisposed)
            {
                CustomEditorForm.logForm = new LogForm();
                Logger.OnLogged += Logger_OnLogged;
            }
            logForm.Show();
            logForm.BringToFront();
        }
        protected virtual void btnDivide_Click(object sender, EventArgs e)
        {
            if (null == this.Document.Action.SelectedEntity || 0 == this.Document.Action.SelectedEntity.Count)
            {
                MessageBox.Show($"Please select target entity at first", "Hatch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var form = new DivideForm(this.Document.Action.SelectedEntity, this.View);
            if (DialogResult.OK != form.ShowDialog(this))
                return;

            this.Document.Action.ActEntityDivide(this.Document.Action.SelectedEntity, form.Rects);
        }
        protected virtual void mnuMeasurementBegin_Click(object sender, EventArgs e)
        {
            {
                var begin = new MeasurementBegin();
                var form = new PropertyForm(begin);
                if (DialogResult.OK != form.ShowDialog(this))
                    return;
                this.Document.Action.ActEntityAdd(begin);
            }
            {
                var end = new MeasurementEnd();
                this.Document.Action.ActEntityAdd(end);
            }
        }
        protected virtual void btnToArc_Click(object sender, EventArgs e)
        {
            if (null == this.Document.Action.SelectedEntity || 1 != this.Document.Action.SelectedEntity.Count || !(this.Document.Action.SelectedEntity[0] is LwPolyline))
            {
                MessageBox.Show($"Please select target polyline at first", "Hatch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            trvEntity.BeginUpdate();
            this.Document.Action.ActEntityLWPolylineToArc(this.Document.Action.SelectedEntity[0] as LwPolyline);
            trvEntity.EndUpdate();
        }
        protected virtual void btnTransit_Click(object sender, EventArgs e)
        {
            var form = new TransitForm(this.Document);
            form.ShowDialog(this);
        }
        protected virtual void btnPens_Click(object sender, EventArgs e)
        {
            var doc = this.Document as DocumentDefault;
            if (null == doc)
                return;

            var clonedPens = (Pens)doc.Pens.Clone();
            clonedPens.Owner = doc;
            var form = new PensForm(clonedPens.ToArray(), this.PowerMap);
            DialogResult result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;
            doc.Pens.Clear();
            for (int i = 0; i < form.Pens.Count; i++)
            {
                doc.Pens.Add((IPen)form.Pens[i].Clone());
            }
            Logger.Log(Logger.Type.Debug, $"updated pens list at editor");
        }
        protected virtual void chbLock_CheckedChanged(object sender, EventArgs e)
        {
            if (null == this.Document)
                return;
            this.Document.IsEditorLock = chbLock.Checked;
            if (this.Document.IsEditorLock)
            {
                trvEntity.Enabled = false;
                ppgEntity.Enabled = false;
            }
            else
            {
                trvEntity.Enabled = true;
                ppgEntity.Enabled = true;
            }
        }
        protected virtual void mnuRtc_Click(object sender, EventArgs e)
        {
            if (null != this.OnRtc)
            {
                this.OnRtcShow();
            }
            else
            {
                if (null == this.Rtc)
                    return;

                var form = new PropertyForm(this.Rtc);
                form.ShowDialog();
            }
        }
        protected virtual void mnuAbout_Click(object sender, EventArgs e)
        {
            var form = new AboutForm();
            form.ShowDialog();
        }
        protected virtual void mnuCircle_Click(object sender, EventArgs e)
        {
            var circle = new Circle3D(0, 0, 0, 10);
            var form = new PropertyForm(circle);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(circle);
        }
        protected virtual void mnuTriangle_Click(object sender, EventArgs e)
        {
            var triangle = new Triangle3D(0, 0, 0, 10, 10);
            var form = new PropertyForm(triangle);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(triangle);
        }
        protected virtual void mnuFiducial_Click(object sender, EventArgs e)
        {
            var fiducial = new Fiducial(0, 0);
            var form = new PropertyForm(fiducial);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(fiducial);
        }
        protected virtual void mnuCalculationDynamics_Click(object sender, EventArgs e)
        {
            var cd = new SyncAxisCalculationDynamics();
            var form = new PropertyForm(cd);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(cd);
        }
        protected virtual void mnuMOTFAngularBeginEnd_Click(object sender, EventArgs e)
        {
            {
                var motfBegin = new MotfAngularBegin();
                var form = new PropertyForm(motfBegin);
                if (DialogResult.OK != form.ShowDialog(this))
                    return;
                this.Document.Action.ActEntityAdd(motfBegin);
                var motfEnd = new MotfEnd();
                this.Document.Action.ActEntityAdd(motfEnd);
            }
        }
        protected virtual void mnuMotfAngularWait_Click(object sender, EventArgs e)
        {
            var motf = new MotfAngularWait();
            var form = new PropertyForm(motf);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(motf);
        }
    }
}
