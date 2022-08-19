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
 * Custom Viewer Form
 * Description : 
 * Author : hong chan, choi / hcchoi@spirallab.co.kr (http://spirallab.co.kr)
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpGL;
using System.Diagnostics;

namespace SpiralLab.Sirius
{
    /// <summary>
    /// 커스텀 시리우스 뷰어
    /// </summary>
    public partial class CustomViewerForm 
        : Form
        //: UserControl
    {
        /// <summary>
        /// 식별 번호
        /// </summary>
        public virtual uint Index { get; set; }
        /// <summary>
        /// 상태바에 출력되는 이름
        /// </summary>
        public virtual string AliasName
        {
            get { return this.lblName.Text; }
            set { this.lblName.Text = value; }
        }
        /// <summary>
        /// 상태바에 출력되는 진행상태 (0~100)
        /// </summary>
        public virtual int Progress
        {
            get { return this.pgbProgress.Value; }
            set
            {
                if (!this.IsHandleCreated || this.IsDisposed)
                    return;
                statusStrip1.BeginInvoke(new MethodInvoker(delegate ()
                {
                    this.pgbProgress.Value = value;
                }));
            }
        }
        /// <summary>
        /// 상태바에 출력되는 작업 파일이름
        /// </summary>
        public virtual string FileName
        {
            get { return this.lblFileName.Text; }
            set
            {
                if (!this.IsHandleCreated)
                    return;
                statusStrip1.BeginInvoke(new MethodInvoker(delegate ()
                {
                    this.lblFileName.Text = value;
                }));
            }
        }
        /// <summary>
        /// 가공 소요 시간  (sec)
        /// </summary>
        public virtual float ProcessingTime
        {          
            set { lblProcessingTime.Text = $"{value:F1} s"; ; }
        }
        /// <summary>
        /// 뷰 객체
        /// </summary>
        public virtual IView View
        {
            get { return this.view; }
        }
        protected IView view;
        /// <summary>
        /// 문서 컨테이너 객체
        /// </summary>
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
                if (0 == this.doc.Layers.Count)  //default layer create
                    this.doc.Action.ActNew();
                List<IView> oldViews = new List<IView>();
                if (null != this.Document)
                {
                    oldViews.AddRange(this.Document.Views);
                    if (null != this.view)
                    {
                        this.doc.Views.Remove(this.view);
                        oldViews.Remove(this.view);
                    }
                }
                this.FileName = this.doc.FileName;
                this.view = new ViewDefault(doc, this.GLcontrol)
                {
                    EditorMode = false,
                };
                this.doc.Views.Add(this.view);
                this.doc.Views.AddRange(oldViews);
                this.view.Render();
                this.view.OnZoomFit();
            }
        }
        protected IDocument doc;

        /// <summary>
        /// 생성자
        /// </summary>
        public CustomViewerForm()
        {
            InitializeComponent();

            this.GLcontrol.OpenGLInitialized += new EventHandler(this.OnInitialized);
            this.GLcontrol.Resize += new EventHandler(this.OnResized);
            this.GLcontrol.MouseDown += new MouseEventHandler(this.OnMouseDown);
            this.GLcontrol.MouseUp += new MouseEventHandler(this.OnMouseUp);
            this.GLcontrol.MouseMove += new MouseEventHandler(this.OnMouseMove);
            this.GLcontrol.MouseWheel += new MouseEventHandler(this.OnMouseWheel);
            this.GLcontrol.OpenGLDraw += new RenderEventHandler(this.OnDraw);
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
        }
        protected virtual void OnMouseMove(object sender, MouseEventArgs e)
        {
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
            if (null == this.view)
                return;
            var sw = Stopwatch.StartNew();
            this.view.OnDraw();
            //lblRenderTime.Text = $"Render: {sw.ElapsedMilliseconds} ms";            
        }
        public override void Refresh()
        {
            base.Refresh();
            this.view.Render();
        }
        protected virtual void btnZoomOut_Click(object sender, EventArgs e)
        {
            this.view?.OnZoomOut(new System.Drawing.Point(GLcontrol.Width / 2, GLcontrol.Height / 2));
        }
        protected virtual void btnZoomIn_Click(object sender, EventArgs e)
        {
            this.view?.OnZoomIn(new System.Drawing.Point(GLcontrol.Width / 2, GLcontrol.Height / 2));
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
            this.view?.OnPan(btnPan.Checked);
        }
    }
}
