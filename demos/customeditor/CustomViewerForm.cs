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
 * Copyright(C) 2020 hong chan, choi. labspiral@gmail.com
 * Copyright (C) 2010-2020 SpiralLab. All rights reserved. 
 * SiriusViewerForm
 * Description : 가공 데이타 (Document)를 화면에 출력 하는 뷰어(viewer) 기능을 수행한다.
 * Author : hong chan, choi / labspiral@gmail.com (http://spirallab.co.kr)
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
using SpiralLab.Sirius;

namespace CustomEditor
{
    public partial class CustomViewerForm : Form
    {
        /// <summary>
        /// 식별 번호
        /// </summary>
        public uint Index { get; set; }
        /// <summary>
        /// 상태바에 출력되는 이름
        /// </summary>
        public string AliasName
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
        public int Progress
        {
            get
            {
                return this.pgbProgress.Value;
            }
            set
            {
                this.pgbProgress.Value = value;
            }
        }
        /// <summary>
        /// 상태바에 출력되는 작업 파일이름
        /// </summary>
        public string FileName
        {
            get
            {
                return this.lblFileName.Text;
            }
            private set
            {
                this.lblFileName.Text = value;
            }
        }

        /// <summary>
        /// 뷰 객체
        /// </summary>
        public IView View
        {
            get { return this.view; }
        }
        IView view;
        /// <summary>
        /// 문서 컨테이너 객체
        /// </summary>
        public IDocument Document
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
        IDocument doc;

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
        private void OnInitialized(object sender, EventArgs e)
        {
            this.view?.OnInitialized(sender, e);
        }
        private void OnResized(object sender, EventArgs e)
        {
            this.view?.OnResized(sender, e);
        }
        private void OnMouseDown(object sender, MouseEventArgs e)
        {           
            this.view?.OnMouseDown(sender, e);
        }
        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            this.view?.OnMouseUp(sender, e);
        }
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (null != this.view)
            {
                this.view.OnMouseMove(sender, e);
                this.view.Dp2Lp(e.Location, out float x, out float y);
                this.lblXPos.Text = $"X: {x:F3}";
                this.lblYPos.Text = $"Y: {y:F3}";
            }
        }
        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            this.view?.OnMouseWheel(sender, e);
        }
        private void OnDraw(object sender, RenderEventArgs args)
        {
            if (null == this.view)
                return;
            var sw = Stopwatch.StartNew();
            this.view.OnDraw();
            lblRenderTime.Text = $"Render: {sw.ElapsedMilliseconds} ms";            
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            this.view?.OnZoomOut(new System.Drawing.Point(GLcontrol.Width / 2, GLcontrol.Height / 2));
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            this.view?.OnZoomIn(new System.Drawing.Point(GLcontrol.Width / 2, GLcontrol.Height / 2));
        }

        private void btnZoomFit_Click(object sender, EventArgs e)
        {
            this.view?.OnZoomFit();
        }

        private void btnPan_Click(object sender, EventArgs e)
        {
            this.view?.OnPan(btnPan.Checked);
        }
    }
}
