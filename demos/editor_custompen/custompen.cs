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
 *              `---'.|    '---'   |   |.'    `--''                              `--''          |   | ,'    
 *                `---`            `---'                                                        `----'   
 * 
 * Copyright (C) 2010-2020 SpiralLab. All rights reserved.
 * custom pen entity : 사용자가 특화시킨 펜 엔티티 
 * Description : 
 * Author : hong chan, choi / hcchoi@spirallab.co.kr (http://spirallab.co.kr)
 * 
 */

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    /// <summary>
    /// Pen 엔티티 사용자 커스텀 버전
    /// 객체의 속성들은 Json 포맷으로 저장된다. 
    /// 불필요한 속성은 JsonIgnore 처리하고, PropertyGrid 에 표시하지 않을 속성은 Browsable(false) 처리한다
    /// </summary>
    public class CustomPen
        : IPen
    {
        [JsonIgnore]
        [Browsable(false)]
        public virtual IEntity Owner { get; set; }

        [JsonIgnore]
        [Browsable(false)]
        public virtual EType EntityType { get { return EType.UserCustom1; } }

        [JsonIgnore]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Basic")]
        [DisplayName("Color")]
        [Description("펜 지정 색상")]
        public virtual System.Drawing.Color Color
        {
            get { return System.Drawing.Color.FromArgb(this.intColor); }
            set { this.intColor = value.ToArgb(); }
        }
        [Browsable(false)]
        public virtual int intColor { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Basic")]
        [DisplayName("Name")]
        [Description("엔티티의 이름")]
        public virtual string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                this.Node.Text = $"Pen: {value}";
            }
        }
        protected string name;

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Basic")]
        [DisplayName("Description")]
        [Description("엔티티에 대한 설명")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public virtual string Description { get; set; }

        [JsonIgnore]
        [Browsable(false)]
        public virtual BoundRect BoundRect { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Status")]
        [DisplayName("Selected")]
        [Description("선택여부")]
        public virtual bool IsSelected { get; set; }

        [Browsable(false)]
        public virtual bool IsHighlighted { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Status")]
        [DisplayName("Markerable")]
        [Description("레이저 가공 여부")]
        public virtual bool IsMarkerable
        {
            get { return isMarkerable; }
            set
            {
                if (null != this.Owner && this.isLocked)
                    return;
                isMarkerable = value;
                if (null == Node.NodeFont)
                    Node.NodeFont = new System.Drawing.Font("Arial", 10);
                Node.NodeFont.Dispose();
                if (isMarkerable)
                    Node.NodeFont = new System.Drawing.Font("Arial", 10, Node.NodeFont.Style & ~System.Drawing.FontStyle.Strikeout);
                else
                    Node.NodeFont = new System.Drawing.Font("Arial", 10, Node.NodeFont.Style | System.Drawing.FontStyle.Strikeout);
            }
        }
        protected bool isMarkerable;

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Status")]
        [DisplayName("Locked")]
        [Description("편집 금지 여부")]
        public virtual bool IsLocked
        {
            get { return isLocked; }
            set { isLocked = value; }
        }
        protected bool isLocked;

        [JsonIgnore]
        [Browsable(false)]
        public virtual TreeNode Node { get; set; }

        [JsonIgnore]
        [Browsable(false)]
        public virtual int Index { get; set; }

        [JsonIgnore]
        [Browsable(false)]
        public virtual object Tag { get; set; }

        [Browsable(false)]  
        public virtual uint Repeat { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Laser")]
        [DisplayName("Power (%)")]
        [Description("레이저 가공시의 설정 파워 (0~100%)")]
        public virtual float Power
        {
            get { return this.power; }
            set
            {
                if (null != this.Owner && this.isLocked)
                    return;
                this.power = value;
            }
        }
        protected float power;

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Laser")]
        [DisplayName("Frequency")]
        [Description("레이저 가공시의 설정 주파수 (Hz)")]
        public virtual float Frequency
        {
            get { return this.frequency; }
            set
            {
                if (null != this.Owner && this.isLocked)
                    return;
                this.frequency = value;
            }
        }
        protected float frequency;

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Laser")]
        [DisplayName("Pulse width")]
        [Description("레이저 가공시 주파수의 펄스 폭 (usec)")]
        public virtual float PulseWidth
        {
            get { return this.pulseWidth; }
            set
            {
                if (null != this.Owner && this.isLocked)
                    return;
                this.pulseWidth = value;
            }
        }
        protected float pulseWidth;       

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Laser")]
        [DisplayName("On delay")]
        [Description("레이저 가공시 레이저 시작 펄스의 지연시간 (usec)")]
        public virtual float LaserOnDelay
        {
            get { return this.laserOnDelay; }
            set
            {
                if (null != this.Owner && this.isLocked)
                    return;
                this.laserOnDelay = value;
            }
        }
        protected float laserOnDelay;

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Laser")]
        [DisplayName("Off delay")]
        [Description("레이저 가공시 레이저 끝 펄스의 지연시간 (usec)")]
        public virtual float LaserOffDelay
        {
            get { return this.laserOffDelay; }
            set
            {
                if (null != this.Owner && this.isLocked)
                    return;
                this.laserOffDelay = value;
            }
        }
        protected float laserOffDelay;

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Scanner")]
        [DisplayName("Jump delay")]
        [Description("스캐너 점프 이동후 안정화 시간 (usec)")]
        public virtual float ScannerJumpDelay
        {
            get { return this.scannerJumpDelay; }
            set
            {
                if (null != this.Owner && this.isLocked)
                    return;
                this.scannerJumpDelay = value;
            }
        }
        protected float scannerJumpDelay;

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Scanner")]
        [DisplayName("Mark delay")]
        [Description("스캐너 직선/호 이동후 안정화 시간 (usec)")]
        public virtual float ScannerMarkDelay
        {
            get { return this.scannerMarkDelay; }
            set
            {
                if (null != this.Owner && this.isLocked)
                    return;
                this.scannerMarkDelay = value;
            }
        }
        protected float scannerMarkDelay;

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Scanner")]
        [DisplayName("Polygon delay")]
        [Description("스캐너 폴리곤 혹은 코너(Corner) 이동 사이간의 지연 시간 (usec)")]
        public virtual float ScannerPolygonDelay
        {
            get { return this.scannerPolygonDelay; }
            set
            {
                if (null != this.Owner && this.isLocked)
                    return;
                this.scannerPolygonDelay = value;
            }
        }
        protected float scannerPolygonDelay;

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Scanner")]
        [DisplayName("Jump speed")]
        [Description("스캐너 점프 속도 (mm/sec)")]
        public virtual float JumpSpeed
        {
            get { return this.jumpSpeed; }
            set
            {
                if (null != this.Owner && this.isLocked)
                    return;
                this.jumpSpeed = value;
            }
        }
        protected float jumpSpeed;

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Scanner")]
        [DisplayName("Mark speed")]
        [Description("스캐너 직선/호 가공 속도 (mm/sec)")]
        public virtual float MarkSpeed
        {
            get { return this.markSpeed; }
            set
            {
                if (null != this.Owner && this.isLocked)
                    return;
                this.markSpeed = value;
            }
        }
        protected float markSpeed;

        public override string ToString()
        {
            return $"Pen: {Name}";
        }

        public CustomPen()
        {
            this.Node = new TreeNode();
            this.Node.NodeFont = new System.Drawing.Font("Arial", 10);
            this.Name = "Custom";
            this.IsSelected = false;
            this.isMarkerable = true;
            this.isLocked = false;
            this.BoundRect = BoundRect.Empty;
            this.frequency = 50 * 1000;
            this.pulseWidth = 2;
            this.power = 5.0f;
            this.jumpSpeed = 500;
            this.markSpeed = 500;
            this.Color = System.Drawing.Color.White;
        }

        #region implements ICloneable
        /// <summary>
        /// 복사본 생성
        /// </summary>
        /// <returns></returns>
        public virtual object Clone()
        {
            CustomPen pen = new CustomPen
            {
                Name = this.Name,
                Description = this.Description,
                Owner = this.Owner,
                IsSelected = this.IsSelected,
                isMarkerable = this.IsMarkerable,
                isLocked = this.IsLocked,
                power = this.Power,
                frequency = this.Frequency,
                pulseWidth = this.PulseWidth,
                laserOnDelay = this.LaserOnDelay,
                laserOffDelay = this.LaserOffDelay,
                scannerJumpDelay = this.ScannerJumpDelay,
                scannerMarkDelay = this.ScannerMarkDelay,
                scannerPolygonDelay = this.ScannerPolygonDelay,
                jumpSpeed = this.JumpSpeed,
                markSpeed = this.MarkSpeed,
                Tag = this.Tag,
                Node = new TreeNode()
                {
                    Text = Node.Text,
                    Tag = Node.Tag,
                    NodeFont = (System.Drawing.Font)Node.NodeFont.Clone(),
                },
            };
            return pen;
        }
        #endregion

        /// <summary>
        /// laser processing
        /// </summary>
        /// <param name="markerArg"></param>
        /// <returns></returns>
        public virtual bool Mark(IMarkerArg markerArg)
        {
            if (!this.IsMarkerable)
                return true;
            var rtc = markerArg.Rtc;
            var laser = markerArg.Laser;
            bool success = true;
            bool isDutyControlled = false;
            if (null != laser)
            {
                if (laser is IPowerControl powerControl)
                {
                    success &= powerControl.ListPower(this.Power); // 레이저 소스 객체에 파워값을 전달한다
                    if (powerControl.PowerControlMethod == PowerControlMethod.Duty)
                        isDutyControlled = true;
                }
            }
            if (!success)
                return false;
            success &= rtc.ListDelay(this.LaserOnDelay, this.LaserOffDelay, this.ScannerJumpDelay, this.ScannerMarkDelay, this.ScannerPolygonDelay);
            success &= rtc.ListSpeed(this.JumpSpeed, this.MarkSpeed);
            if (!isDutyControlled)
                success &= rtc.ListFrequency(this.Frequency, this.PulseWidth);

            if (success)
                markerArg.PenStack.Push(this); //현재 사용중인 펜 엔티티를 펜 스텍에 삽입한다 (이는 PenReturn 을 사용할때 이전 펜 상태로 복구하는데 필요하다)
            return success;
        }

        public virtual void Regen()
        { }
    }
}
