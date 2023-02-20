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
 * Copyright (C) 2019-2023 SpiralLab. All rights reserved.
 * 
 * Create custom entity (사용자 정의 개체 만들기)
 * Description : 
 * add references (참조 추가)
 * - Newtonsoft.Json.dll (JSON 파일 처리용)
 * - SharpGL.dll (OpenGL 렌더링용)
 * - System.Numerics.dll (Vector 관련 구조체 지원용)
 * 
 * Author : hong chan, choi / hcchoi@spirallab.co.kr (http://spirallab.co.kr)
 * 
 */

using Newtonsoft.Json;
using SharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SpiralLab.Sirius
{
    /// <summary>
    /// User Custom Entity 'Line'
    /// 사용자 구현 선분 엔티티
    /// 
    /// 객체의 속성(Property)은 winforms 의 PropertyGrid 를 이용해 사용자에게 
    /// 출력 되므로 PropertyGrid 출력이 가능하도록 모든 속성 처리를 해준다
    /// 예를 들어 : [Browsable(true)] / [Category("Basic")] / [DisplayName("Name")] 과 같은
    ///
    /// 또한 객체 데이타는 JSON 포맷으로 변환되어 저장/ 열기 처리되므로
    /// 저장이 불필요한 속성의 경우  [JsonIgnore] 를 처리해 준다
    /// </summary>
    public class CustomLine
        : IEntity           // 기본 엔티티(개체) 인터페이스 구현
        , IMarkerable       // 레이저 가공(Mark)을 지원하는 인터페이스 구현
        , IDrawable         // 화면에 렌더링(Draw)을 지원하는 인터페이스 구현
        , ICloneable        // 복제(Clone) 를 지원하는 인터페이스 구현
        //, IExplodable     // 분해(Explode) 지원시 상속 구현해야 하는 인터페이스
        //, IHatchable      // 해치(Hatch) 지원시 상속 구현해야 하는 인터페이스
        //, IScriptable     // 스크립트 (C# 코드) 지원시 상속 구현해야 하는 인터페이스
        //, ITextChangeable // 가공 데이타 문자열 변환 지원시 상속 구현해야 하는 인터페이스
    {
        /// <summary>
        /// 부모 엔티티
        /// parent entity
        /// </summary>
        [JsonIgnore]
        [Browsable(false)]
        public virtual IEntity Owner { get; set; }

        /// <summary>
        /// 개체 타입
        /// type of entity
        /// </summary>
        [JsonIgnore]
        [Browsable(false)]
        public virtual EType EntityType { get { return EType.UserDefined1; } }

        /// <summary>
        /// 이름
        /// entity name
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
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
                this.Node.Text = $"{this.ToString()}";
            }
        }
        protected string name;

        /// <summary>
        /// 설명
        /// entity description
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Basic")]
        [DisplayName("Description")]
        [Description("엔티티에 대한 설명")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public virtual string Description { get; set; }

        /// <summary>
        /// Deprecated 
        /// 예전 버전 호환을 위한 항목  (미사용)
        /// </summary>
        [Browsable(false)]
        [JsonIgnore]
        public AciColor Color { get; set; }

        /// <summary>
        /// pen color of entity
        /// 개체의 펜 색상 
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Basic")]
        [DisplayName("Color")]
        [Description("색상")]
        [Editor(typeof(SpiralLab.Sirius.PenColorEditor), typeof(UITypeEditor))]
        public virtual System.Drawing.Color Color2
        {
            get { return this.color; }
            set { this.color = value; }
        }
        protected System.Drawing.Color color;

        /// <summary>
        /// 외곽 영역
        /// bounding rectangle area
        /// </summary>
        [JsonIgnore]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Basic")]
        [DisplayName("Bound")]
        [Description("외각 영역")]
        public virtual BoundRect BoundRect { get; set; }

        /// <summary>
        /// 선택 여부
        /// selected?
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Status")]
        [DisplayName("Selected")]
        [Description("선택여부")]
        public virtual bool IsSelected { get; set; }

        /// <summary>
        /// 미지원
        /// unsupported yet
        /// </summary>
        [Browsable(false)]
        public virtual bool IsHighlighted { get; set; }

        /// <summary>
        /// 마우스 선택 기능 지원 여부
        /// support hit test by mouse 
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Status")]
        [DisplayName("Hit Test")]
        [Description("마우스 선택 기능")]
        public virtual bool IsHitTest
        {
            get { return isHitTest; }
            set { isHitTest = value; }
        }
        protected bool isHitTest;

        /// <summary>
        /// 화면에 렌더링(출력) 여부
        /// draw entity
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Status")]
        [DisplayName("Visible")]
        [Description("스크린에 출력 여부")]
        public virtual bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }
        protected bool isVisible;

        /// <summary>
        /// 가공 여부
        /// mark entity
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Status")]
        [DisplayName("Markerable")]
        [Description("레이저 가공 여부")]
        public virtual bool IsMarkerable
        {
            get { return isMarkerable; }
            set {
                if (null != this.Owner && this.isLocked)
                    return;
                isMarkerable = value;
                if (isMarkerable)
                    Node.NodeFont = Config.NodeFont;
                else
                    Node.NodeFont = Config.NodeFontStrikeOut;
            }
        }
        protected bool isMarkerable;

        /// <summary>
        /// 가공 경로를 추가로 표시할지 여부
        /// draw mark pathes
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Status")]
        [DisplayName("Mark Path")]
        [Description("가공 경로를 표시")]
        public virtual bool IsDrawPath { get; set; }

        /// <summary>
        /// 편집 (속성 데이타 수정) 허용 여부
        /// data(properties) is editable
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
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

        /// <summary>
        /// 반복 가공 회수
        /// mark repeat counts
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Data")]
        [DisplayName("Repeat")]
        [Description("가공 반복 횟수")]
        public virtual uint Repeat { get; set; }

        /// <summary>
        /// 시작 점
        /// start point
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Data")]
        [DisplayName("Start")]
        [Description("시작점 좌표값")]
        [TypeConverter(typeof(Vector2Converter))]
        public virtual System.Numerics.Vector2 Start {
            get { return start; }
            set {
                if (null != this.Owner && this.isLocked)
                    return;
                start = value; this.isRegen = true;
                this.Node.Text = $"{this.ToString()}";
            }
        }
        protected System.Numerics.Vector2 start;

        /// <summary>
        /// 끝점
        /// end point
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Data")]
        [DisplayName("End")]
        [Description("끝점 좌표값")]
        [TypeConverter(typeof(Vector2Converter))]
        public virtual System.Numerics.Vector2 End {
            get { return end; }
            set {
                if (null != this.Owner && this.isLocked)
                    return;
                end = value; this.isRegen = true;
                this.Node.Text = $"{this.ToString()}";
            }
        }
        protected System.Numerics.Vector2 end;

        /// <summary>
        /// 회전 각도
        /// angle
        /// </summary>
        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Data")]
        [DisplayName("Angle")]
        [Description("회전 각 (°)")]
        [TypeConverter(typeof(FloatTypeConverter))]
        public virtual float Angle
        {
            get { return angle; }
            set {
                if (null != this.Owner && this.isLocked)
                    return;
                float delta = value - angle;
                if (null != this.Owner)
                    this.Rotate(delta);
                this.angle = value;
            }
        }
        protected float angle;

        /// <summary>
        /// 트리뷰 노드 (내부에서 사용됨)
        /// node of treeview (internal use only)
        /// </summary>
        [JsonIgnore]
        [Browsable(false)]
        public virtual TreeNode Node { get; set; }

        /// <summary>
        /// 트리뷰 인덱스 (내부에서 사용됨)
        /// node id of treeview (internal use only)
        /// </summary>
        [JsonIgnore]
        [Browsable(false)]
        public virtual int Index { get; set; }

        /// <summary>
        /// 사용자 정의 데이타
        /// user data
        /// </summary>
        [JsonIgnore]
        [Browsable(false)]
        public virtual object Tag { get; set; }

        /// <summary>
        /// 트리뷰 노드에 출력될 문자열
        /// string for treeview node
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Name}";
        }

        /// <summary>
        /// 내부 데이타 변경으로 인한 재계산이 필요 상태
        /// need to re-generate internal data
        /// </summary>
        protected bool isRegen;


        /// <summary>
        /// 생성자
        /// </summary>
        public CustomLine()
        {
            this.Node = new TreeNode();
            //this.Node.NodeFont = Config.NodeFont;
            this.Name = "Custom Line";
            this.IsSelected = false;
            this.isVisible = true;
            this.isMarkerable = true;
            this.isLocked = false;
            this.isHitTest = true;
            this.color = Config.PenDefaultColor;
            this.BoundRect = BoundRect.Empty;
            this.Start = Vector2.Zero;
            this.End = new Vector2(10, 10);
            this.isRegen = true;
            this.Repeat = 1;
        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        public CustomLine(float startX, float startY, float endX, float endY)
            : this()
        {
            this.Start = new Vector2(startX, startY);
            this.End = new Vector2(endX, endY);
        }
       
        #region ICloneable 복제 구현
        /// <summary>
        /// 복사본 생성
        /// cloneable 
        /// </summary>
        /// <returns></returns>
        public virtual object Clone()
        {
            CustomLine line = new CustomLine
            {
                Name = this.Name,
                Description = this.Description,
                Owner = this.Owner,
                IsSelected = this.IsSelected,
                IsHighlighted = this.IsHighlighted,
                isVisible = this.IsVisible,
                isMarkerable = this.IsMarkerable,
                isLocked = this.IsLocked,
                isHitTest = this.IsHitTest,
                Repeat = this.Repeat,
                color = this.Color2,
                BoundRect = (BoundRect)this.BoundRect.Clone(),
                start = this.start,
                end = this.end,
                angle = this.angle,
                Tag = this.Tag,
                Node = new TreeNode()
                {
                    Text = Node.Text,
                    Tag = Node.Tag,
                    NodeFont = Node.NodeFont,
                },
            };
            return line;
        }
        #endregion

        #region IMarkerable 가공 구현
        /// <summary>
        /// 레이저 가공 구현
        /// markerable
        /// </summary>
        /// <param name="markerArg">IMarkerArg 인자</param>
        /// <returns></returns>
        public virtual bool Mark(IMarkerArg markerArg)
        {
            // 가공 옵션이 활성화 되어야 가공을 진행
            if (!this.IsMarkerable)
                return true;
            bool success = true;

            // (필수) 폰트 다운로드중일 때는 펜 색상 처리를 하지 않는다
            if (!markerArg.IsRegisteringFonts)
                success &= PenDefault.MarkPen(markerArg, Color2, this);
            if (!success)
                return false;
            // IRtc 인터페이스를 사용해 스캐너 제어
            var rtc = markerArg.Rtc;
            for (int i = 0; i < this.Repeat; i++)
            {
                success &= rtc.ListJump(this.Start);
                success &= rtc.ListMark(this.End);
                if (!success)
                    break;
            }
            return success;
        }
        #endregion

        #region IDrawable 데이타 갱신, 데이타를 기반으로 대상 뷰에 그리기 구현
        /// <summary>
        /// 내부 데이타를 재 갱신하는 부분
        /// 데이타 (속성) 변경후 재 계산
        /// </summary>
        protected virtual void RegenVertextList()
        {

        }
        /// <summary>
        /// 외곽 영역 (bounding rect)를 계산
        /// </summary>
        protected virtual void RegenBoundRect()
        {

            float left = this.Start.X < this.End.X ? this.Start.X : this.End.X;
            float right = this.Start.X < this.End.X ? this.End.X : this.Start.X;
            float top = this.Start.Y > this.End.Y ? this.Start.Y : this.End.Y;
            float bottom = this.Start.Y > this.End.Y ? this.End.Y : this.Start.Y;
            this.BoundRect = new BoundRect(left, top, right, bottom);
        }
        /// <summary>
        /// 데이타 (속성) 변경에 의해 내부 재 계산 루틴 실시
        /// </summary>
        public virtual void Regen()
        {
            this.RegenVertextList();
            this.RegenBoundRect();
            // 재 계산 완료
            this.isRegen = false;
        }
        /// <summary>
        /// OpenGL 을 이용해 화면에 렌더링(그리기) 실시
        /// </summary>
        /// <param name="view">대상 뷰(IView)인터페이스</param>
        /// <returns></returns>
        public virtual bool Draw(IView view)
        {
            // 데이타 변경이 있으면 이를 우선 실시
            if (isRegen)
                this.Regen();

            // 화면 렌더링 옵션이 활성화 되어야 그리기 실시
            if (!this.IsVisible)
                return true;

            // 실제 대상 뷰
            var viewDefault = view as ViewDefault;

            // opengl 
            var gl = view.Renderer;

            // openg 행렬 스택 push
            gl.PushMatrix();

            // 그리기 시작 
            // line 
            gl.Begin(OpenGL.GL_LINES);

            // 색상 지정
            if (this.IsSelected)
                gl.Color((Config.EntitySelectedColor[0]), (Config.EntitySelectedColor[1]), (Config.EntitySelectedColor[2]), Config.EntitySelectedColor[3]);
            else
                gl.Color(this.Color2.R, this.Color2.G, this.Color2.B);
            // 시작 점
            gl.Vertex(Start.X, Start.Y);

            // 끝점
            gl.Vertex(End.X, End.Y);

            // 그리기 끝
            gl.End();

            //opengl 행렬 스택 pop
            gl.PopMatrix();
            return true;
        }

        /// <summary>
        /// 가공 시작점, 끝점 조회
        /// </summary>
        /// <param name="vIn"></param>
        /// <param name="vOut"></param>
        /// <returns></returns>
        public virtual bool GetInOut(out Vector2 vIn, out Vector2 vOut)
        {
            vIn = this.Start;
            vOut = this.End;
            return true;
        } 
        #endregion

        #region 선형 변환 처리 (Linear Transformation)
        /// <summary>
        /// 이동 처리
        /// transit
        /// </summary>
        /// <param name="delta"></param>
        public virtual void Transit(Vector2 delta)
        {
            if (this.IsLocked) return;
            if (delta == Vector2.Zero) return;
            this.start = Vector2.Add(this.start, delta);
            this.end = Vector2.Add(this.end, delta);

            this.Regen();
        }
        /// <summary>
        /// 회전 
        /// </summary>
        /// <param name="angle">각도</param>
        public virtual void Rotate(float angle)
        {
            // 기준점(location)이 없는 회전이므로 원점을 중심으로 처리하도록 처리함
            if (this.IsLocked) return;
            if (MathHelper.IsZero(angle)) return;
            this.start = Vector2.Transform(this.start, Matrix3x2.CreateRotation(angle * MathHelper.DegToRad, BoundRect.Center));
            this.end = Vector2.Transform(this.end, Matrix3x2.CreateRotation(angle * MathHelper.DegToRad, BoundRect.Center));
            this.angle += angle;
            this.angle = MathHelper.NormalizeAngle(this.angle);

            this.Regen();
        }
        /// <summary>
        /// 회전  
        /// </summary>
        /// <param name="angle">각도</param>
        /// <param name="rotateCenter">회전중심</param>
        public virtual void Rotate(float angle, Vector2 rotateCenter)
        {
            if (this.IsLocked) return;
            if (MathHelper.IsZero(angle)) return;
            this.start = Vector2.Transform(this.start, Matrix3x2.CreateRotation(angle * MathHelper.DegToRad, rotateCenter));
            this.end = Vector2.Transform(this.end, Matrix3x2.CreateRotation(angle * MathHelper.DegToRad, rotateCenter));
            this.angle += angle;
            this.angle = MathHelper.NormalizeAngle(this.angle);
            this.Regen();
        }
        /// <summary>
        /// 크기 (비율)
        /// </summary>
        /// <param name="scale">비율값</param>
        public virtual void Scale(Vector2 scale)
        {
            if (this.IsLocked) return;
            if (scale == Vector2.Zero || scale == Vector2.One) return;

            // 기준점(location)이 없는 크기 변환 이므로 선분의 중심으로 처리하도록 처리함
            var center = (this.start + this.end) * 0.5f;

            var delta1 = this.start - center;
            var temp1 = delta1 * scale;
            this.start = temp1 + center;
            var delta2 = this.end - center;
            var temp2 = delta2 * scale;
            this.end = temp2 + center;
            this.Regen();
        }
        /// <summary>
        /// 크기 (비율)
        /// </summary>
        /// <param name="scale">비율값</param>
        /// <param name="scaleCenter">비율적용 중심위치</param>
        public virtual void Scale(Vector2 scale, Vector2 scaleCenter)
        {
            if (this.IsLocked) return;
            if (scale == Vector2.Zero || scale == Vector2.One) return;
            var delta1 = this.start - scaleCenter;
            var temp1 = delta1 * scale;
            this.start = temp1 + scaleCenter;
            var delta2 = this.end - scaleCenter;
            var temp2 = delta2 * scale;
            this.end = temp2 + scaleCenter;
            this.Regen();
        }
        #endregion

        #region Hit Test 선택 처리
        /// <summary>
        /// Hit 테스트 (선택 여부를 판단하는 알고리즘 필요)
        /// 마우스 선택 (한점 클릭) 위치와 현 개체의 좌표점과의 거리(충돌/교차 지점)가
        /// Config.HitTestThreshold 이내이면 선택이 성공한것으로 처리
        /// </summary>
        /// <param name="x">선택 X 위치</param>
        /// <param name="y">선택 Y 위치</param>
        /// <param name="threshold">허용 한계 거리값(mm)</param>
        /// <returns></returns>
        public virtual bool HitTest(float x, float y, float threshold = Config.HitTestThreshold)
        {
            if (!this.IsVisible)
                return false;
            if (!this.IsHitTest)
                return false;
            //외각 사각형 1차 선택 영역내인지 판별
            if (!this.BoundRect.HitTest(x, y, threshold))
                return false;
            //2차 내부 객체 좌표 데이타들과 실제 판단
            bool hit = MathHelper.IntersectPointInLine(Start.X, Start.Y, End.X, End.Y, x, y, threshold);
            return hit;
        }
        /// <summary>
        /// Hit 테스트(선택 여부를 판단하는 알고리즘 필요)
        /// 마우스 영역(사각) 위치와 현 개체의 좌표점과의 거리(충돌/교차 지점)가
        /// Config.HitTestThreshold 이내이면 선택이 성공한것으로 처리
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        /// <param name="threshold"></param>
        /// <returns></returns>
        public virtual bool HitTest(float left, float top, float right, float bottom, float threshold = Config.HitTestThreshold)
        {
            if (!this.IsVisible)
                return false;
            if (!this.IsHitTest)
                return false;
            return this.HitTest(new BoundRect(left, top, right, bottom), threshold);
        }
        /// <summary>
        /// Hit 테스트(선택 여부를 판단하는 알고리즘 필요)
        /// 마우스 선택 사각 영역 위치와 현 개체의 좌표점과의 거리(충돌/교차 지점)가
        /// Config.HitTestThreshold 이내이면 선택이 성공한것으로 처리
        /// </summary>
        /// <param name="br"></param>
        /// <param name="threshold"></param>
        /// <returns></returns>
        public virtual bool HitTest(BoundRect br, float threshold = Config.HitTestThreshold)
        {
            if (!this.IsVisible)
                return false;
            if (!this.IsHitTest)
                return false;
            //외각 사각형 1차 선택 영역내인지 판별
            if (!this.BoundRect.HitTest(br, threshold))
                return false;
            //2차 내부 객체 좌표 데이타들과 실제 판단
            bool hit = MathHelper.IntersectLineInRect(br, Start.X, Start.Y, End.X, End.Y);
            return hit;
        }
        #endregion

        #region Grip 포인트 처리 (개체 선택시 Grip 점 위치를 출력하고 이를 사용자가 마우스를 이용해 조작하는 행위 처리)
        /// <summary>
        /// 선분은 2개의 위치(시작, 끝점)에 대한 Grip 정보가 있으므로 2영역에 대한 Grip 정보를 미리 생성
        /// </summary>
        protected BoundRect[] grips = new BoundRect[]
        {
            BoundRect.Empty,
            BoundRect.Empty,
        };
        public virtual bool HitTestGrip(float x, float y, float threshold, out int gripId, out System.Windows.Forms.Cursor cursor)
        {
            gripId = -1;
            cursor = System.Windows.Forms.Cursors.Default;
            if (!this.IsVisible)
                return false;
            if (!this.IsSelected)
                return false;
            if (!this.IsHitTest)
                return false;
            for (int i = 0; i < grips.Length; i++)
            {
                // Grip 위치 선택 성공시
                if (grips[i].HitTest(x, y, threshold))
                {
                    // 선택된 Grip 위치값
                    gripId = i;
                    // 마우스 커서의 모양 변경
                    cursor = System.Windows.Forms.Cursors.SizeAll;
                    // Grip 위치 선택 성공
                    return true;
                }
            }
            // Grip 위치 선택 실패
            return false;
        }
        /// <summary>
        /// Grip 정보를 뷰에 그리기
        /// </summary>
        /// <param name="view">대상 뷰</param>
        /// <returns></returns>
        public virtual bool DrawGrips(IView view)
        {
            if (!this.IsVisible)
                return true;
            if (!this.IsHitTest)
                return false;
            var gl = view.Renderer;

            // Grip 사각형의 크기(pixel)를 현재 뷰의 Zoom 상태에 맞는 사용자 단위계(mm)로 변환
            float size = view.Dp2Lp(Config.GripPointSize);

            // Start 사각형
            grips[0].Width = size;
            grips[0].Height = size;
            grips[0].Center = Start;

            // End 사각형 
            grips[1].Width = size;
            grips[1].Height = size;
            grips[1].Center = End;

            // Grip 사각형 색상 지정
            gl.Color(Config.DocumentSelectedBoundRectGripColor);
            // Start 사각형 그리기
            grips[0].Draw(gl);
            // End 사각형 그리기
            grips[1].Draw(gl);
            return true;
        }
        /// <summary>
        /// 지정된 Grip 위치를 사용자가 선택후 Dragging 중인 상태를 그리기
        /// </summary>
        /// <param name="view">대상 뷰</param>
        /// <param name="gripId">Grip 번호</param>
        /// <param name="dx">X 이동량</param>
        /// <param name="dy">Y 이동량</param>
        /// <returns></returns>
        public virtual bool DrawGripping(IView view, int gripId, float dx, float dy)
        {
            if (!this.IsVisible)
                return true;

            var gl = view.Renderer;
            // Grip 사각형의 크기(pixel)를 현재 뷰의 Zoom 상태에 맞는 사용자 단위계(mm)로 변환
            float size = view.Dp2Lp(Config.GripPointSize);

            switch (gripId)
            {
                case 0: //Start
                    {
                        // 이동된 시작 위치 계산
                        var newStart = Start + new Vector2(dx, dy);
                        // 이동되었을때 를 예측해 선분 새로 그려주기
                        gl.Begin(OpenGL.GL_LINES);
                        gl.Color(this.Color2.R ,this.Color2.G, this.Color2.B);
                        gl.Vertex(newStart.X, newStart.Y);
                        gl.Vertex(End.X, End.Y);
                        gl.End();

                        // 이동된 시작 위치에 새로운 사각형 Grip 모양 그려주기
                        BoundRect grip = BoundRect.Empty;
                        grip.Width = size;
                        grip.Height = size;
                        grip.Center = newStart;
                        gl.Color(Config.DocumentSelectedBoundRectGripColor);
                        grip.Draw(gl);
                    }
                    break;
                case 1: //End 
                    {
                        // 이동된 끝 위치 계산
                        var newEnd = End + new Vector2(dx, dy);
                        // 이동되었을때 를 예측해 선분 새로 그려주기
                        gl.Begin(OpenGL.GL_LINES);
                        gl.Color(this.Color2.R, this.Color2.G, this.Color2.B);
                        gl.Vertex(Start.X, Start.Y);
                        gl.Color(this.Color2.R, this.Color2.G, this.Color2.B);
                        gl.Vertex(newEnd.X, newEnd.Y);
                        gl.End();

                        // 이동된 끝 위치에 새로운 사각형 Grip 모양 그려주기
                        BoundRect grip = BoundRect.Empty;
                        grip.Width = size;
                        grip.Height = size;
                        grip.Center = newEnd;
                        gl.Color(Config.DocumentSelectedBoundRectGripColor);
                        grip.Draw(gl);
                    }
                    break;
                default:
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Grip 을 완료해야 할때 (사용자가 Dragging 을 마쳤기 때문에 데이타를 적용해야 하는 상태)
        /// </summary>
        /// <param name="gripId">Grip 번호</param>
        /// <param name="delta">변화량 (x,y) </param>
        /// <param name="isUndoing">Undo 작업을 하고 있는지 여부 (내부에서 사용됨)</param>
        /// <returns></returns>
        public virtual bool TransitGrip(int gripId, ref Vector2 delta, bool isUndoing = false)
        {
            switch (gripId)
            {
                case 0:
                    // 시작점 위치 적용
                    Start = Start + delta;
                    break;
                case 1:
                    // 끝점 위치 적용
                    End = End + delta;
                    break;
                default:
                    return false;
            }
            return true;
        }
        #endregion
    }
}
