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
 * SiriusEditorForm
 * Description : 가공 데이타 (Document)를 화면에 출력및 사용자의 이벤트에 의해 데이타를 변경하는 등의 뷰어(viewer)및 에디터(editor) 기능을 수행한다.
 * Author : hong chan, choi / labspiral@gmail.com (http://spirallab.co.kr)
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
using SpiralLab.Sirius;

namespace CustomEditor
{
    public partial class CustomEditorForm : Form
    {
        /// <summary>
        /// 문서(Document) 가 변경되었을때 발생하는 이벤트
        /// </summary>
        public event SiriusDocumentSourceChanged OnDocumentSourceChanged;

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
        private IView view;
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
                if (0 == this.doc.Layers.Count)  //default layer create if no exist
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
                this.trvEntity.Nodes.Clear();
                this.FileName = this.doc.FileName;

                this.view = new ViewDefault(doc, this.GLcontrol);
                this.doc.Views.Add(this.view);
                this.doc.Views.AddRange(oldViews);
                this.view.Render();
                this.view.OnZoomFit();
                this.RegenTreeView();

                //this.OnDocumentSourceChanged?.Invoke(this, this.doc);
                var receivers = this.OnDocumentSourceChanged?.GetInvocationList();
                if (null != receivers)
                    foreach (SiriusDocumentSourceChanged receiver in receivers)
                        receiver.Invoke(this, this.doc);
            }
        }
        IDocument doc;
        /// <summary>
        /// RTC 제어 객체
        /// </summary>
        public IRtc Rtc 
        { 
            get { return this.rtc; }
            set { this.rtc = value; }
        }
        IRtc rtc;       
        /// <summary>
        /// 레이저 소스 제어 객체
        /// </summary>
        public ILaser Laser 
        {
            get { return this.laser; }
            set { this.laser = value; }
        }
        ILaser laser;
        /// <summary>
        /// 마커 제어 객체
        /// </summary>
        public IMarker Marker
        {
            get { return this.marker; }
            set
            {
                if (null != this.marker)
                {
                    this.marker.OnProgress -= Marker_OnProgress;
                    this.pgbProgress.Value = 0;
                }
                this.marker = value;
                if (null != this.marker)
                {
                    this.marker.OnProgress += Marker_OnProgress;
                }
            }
        }
        IMarker marker;

        private void Marker_OnProgress(IMarker sender, IMarkerArg arg)
        {
            if (statusStrip1.InvokeRequired)
            {
                statusStrip1.Invoke(new MarkerEventHandler(Marker_OnProgress), new object[] { sender, arg });
                return;
            }
            else
            {
                pgbProgress.Value = (int)arg.Progress;
            }
        }
        /// <summary>
        /// 오른쪽 속성창 보여주기 여부
        /// </summary>
        public bool HidePropertyGrid
        {
            get { return hidePropertyGrid; }
            set
            {
                hidePropertyGrid = value;
                if (hidePropertyGrid)
                {
                    splitContainer1.Panel2Collapsed = true;
                    splitContainer1.Panel2.Hide();
                }
                else
                {
                    splitContainer1.Panel2Collapsed = false;
                    splitContainer1.Panel2.Show();
                }
            }
        }
        bool hidePropertyGrid;

        System.Drawing.Point currentMousePos;


        /// <summary>
        /// 생성자
        /// </summary>
        public CustomEditorForm()
        {
            InitializeComponent();

             this.GLcontrol.Resize += new EventHandler(this.OnResized);
            this.GLcontrol.MouseDown += new MouseEventHandler(this.OnMouseDown);
            this.GLcontrol.MouseUp += new MouseEventHandler(this.OnMouseUp);
            this.GLcontrol.MouseMove += new MouseEventHandler(this.OnMouseMove);
            this.GLcontrol.MouseWheel += new MouseEventHandler(this.OnMouseWheel);
            this.GLcontrol.OpenGLDraw += new RenderEventHandler(this.OnDraw);

            this.ppgEntity.PropertySort = PropertySort.Categorized;
            this.Disposed += SiriusEditorForm_Disposed;

            MyInitialize();
        }

        private bool MyInitialize()
        {
            #region 사용자의 초기화 코드 예제
            bool success = true;
            
            success &= SpiralLab.Core.Initialize();
            var doc = new DocumentDefault();
            this.Document = doc;

            //var rtc = new RtcVirtual(0); //create Rtc for dummy
            var rtc = new Rtc5(0); //create Rtc5 controller
            //var rtc = new Rtc6(0); //create Rtc6 controller
            //var rtc = new Rtc6Ethernet(0, "192.168.0.100", "255.255.255.0"); //실험적인 상태 (Scanlab Rtc6 Ethernet 제어기)
            //var rtc = new Rtc6SyncAxis(0, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configuration", "syncAXISConfig.xml")); //실험적인 상태 (Scanlab XLSCAN 솔류션)

            float fov = 60.0f;    ///scanner field of view : 60mm            
            float kfactor = (float)Math.Pow(2, 20) / fov; // k factor (bits/mm) = 2^20 / fov
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            success &= rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);    // 스캐너 보정 파일 지정 : correction file
            success &= rtc.CtlFrequency(50 * 1000, 2); // laser frequency : 50KHz, pulse width : 2usec
            success &= rtc.CtlSpeed(100, 100); // default jump and mark speed : 100mm/s
            success &= rtc.CtlDelay(10, 100, 200, 200, 0); // scanner and laser delays
            this.Rtc = rtc;

            ILaser laser = new LaserVirtual(0, "virtual", 20.0f);
            laser.Rtc = rtc;
            success &= laser.Initialize();
            success &= laser.CtlPower(10);
            this.Laser = laser;

            var marker = new MarkerDefault(0);
            this.Marker = marker;
            #endregion
            return success;
        }

        private void SiriusEditorForm_Disposed(object sender, EventArgs e)
        {
            // 가공 시뮬레이션 중이면 중단
            Document?.Action?.ActEntityLaserPathSimulateStop();
            //RTC 가공 중이면 가공 정지
            Rtc?.CtlAbort();
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
            this.currentMousePos = e.Location;
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
            //화면 렌더링
            long msec = this.view.OnDraw();
            lblRenderTime.Text = $"Render: {msec} ms";
            //선택된 사각영역이 있으면 해당 크기 업데이트
            if (this.view.SelectedBoundRect.IsEmpty)
            {
                lblBound.Text = string.Empty;
                lblCenter.Text = string.Empty;
                lblWH.Text = string.Empty;                
            }
            else
            {
                lblBound.Text = this.view.SelectedBoundRect.ToString();
                lblCenter.Text = $"{this.view.SelectedBoundRect.Center.X:F3}, {this.view.SelectedBoundRect.Center.Y:F3}";
                lblWH.Text = $"{this.view.SelectedBoundRect.Width:F3}, {this.view.SelectedBoundRect.Height:F3}";
            }
        }      
        private void RegenTreeView()
        {
            //트리뷰 업데이트
            trvEntity.SuspendLayout();
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
            trvEntity.ResumeLayout();
        }

        private void Layer_OnAddItem(ObservableList<Layer> sender, int index, Layer l)
        {
            l.OnAddItem += Entity_OnAddItem;
            l.OnRemoveItem += Entity_OnRemoveItem;
            l.Node.Tag = l;

            var layers = sender as Layers;
            if (layers.Count == index)
                trvEntity.Nodes.Add(l.Node);
            else
                trvEntity.Nodes.Insert(index, l.Node);

            l.Index = index;
        }
        private void Layer_OnRemoveItem(ObservableList<Layer> sender, int index, Layer l)
        {
            l.OnAddItem -= Entity_OnAddItem;
            l.OnRemoveItem -= Entity_OnRemoveItem;
            trvEntity.Nodes.Remove(l.Node);
        }
        private void Entity_OnAddItem(ObservableList<IEntity> sender, int index, IEntity e)
        {
            e.Node.Tag = e;
            var layer = sender as Layer;            
            if (layer.Node.Nodes.Count == index)
                layer.Node.Nodes.Add(e.Node);
            else
                layer.Node.Nodes.Insert(index, e.Node);
            e.Owner = layer;
            e.Index = index;
        }
        private void Entity_OnRemoveItem(ObservableList<IEntity> sender, int index, IEntity e)
        {
            var layer = sender as Layer;
            layer.Node.Nodes.Remove(e.Node);
        }
        /// <summary>
        /// 마우스 선택, 트리뷰 선택 등의 이벤트 발생시 내부 action 에 의해 호출됨
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="list"></param>
        private void Action_OnSelectedEntityChanged(IDocument doc, List<IEntity> list)
        {
            //treeview
            var nodes = new List<TreeNode>(list.Count);
            trvEntity.SuspendLayout();
            foreach (var e in list)
                nodes.Add(e.Node);
            trvEntity.SelectedNodes = nodes;
            if (nodes.Count > 0)
                nodes[nodes.Count - 1].EnsureVisible();
            trvEntity.ResumeLayout();
            trvEntity.Refresh();

            lblEntityCount.Text = $"Selected: {list.Count.ToString()}";
            if (0 == list.Count)
                this.ppgEntity.SelectedObjects = null;
            else
                this.ppgEntity.SelectedObjects = list.ToArray();
        }
        /// <summary>
        /// 트리뷰에서 엔티티 선택시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvEntity_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var list = new List<IEntity>(trvEntity.SelectedNodes.Count);
            //foreach (var n in trvEntity.SelectedNodes)
            //    list.Add(n.Tag as IEntity);
            //treeview 순서가 섞이니 다시 정렬
            foreach (var layer in Document.Layers)
            {
                if (trvEntity.SelectedNodes.Contains(layer.Node)) // O(N*N)
                    list.Add(layer);
                foreach (var entity in layer)
                    if (trvEntity.SelectedNodes.Contains(entity.Node)) // O(N*N )
                        list.Add(entity);
            }

             this.Document.Action.ActEntitySelect(list);
        }
        private void trvEntity_DragEnter(object sender, DragEventArgs e)
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
        private void trvEntity_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                DoDragDrop(e.Item, DragDropEffects.Move);
        }
        private void trvEntity_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(TreeNode)))
                return;

            var pos = trvEntity.PointToClient(new System.Drawing.Point(e.X, e.Y));
            TreeNode targetNode = trvEntity.GetNodeAt(pos);

            // do nothing if itself
            if (trvEntity.SelectedNodes.Count == 1)
                if (trvEntity.SelectedNodes[0].Equals(targetNode))
                    return;

            trvEntity.SuspendLayout();
            var targetEntity = (IEntity)targetNode.Tag;
            Layer targetLayer = null;

            if (targetEntity is Layer)
            {
                targetLayer = targetEntity as Layer;

                // 레이어 이동
                if (trvEntity.SelectedNodes.Count == 1)
                {
                    if (trvEntity.SelectedNodes[0].Tag is Layer)
                    {
                        Document.Action.ActLayerDragMove(trvEntity.SelectedNodes[0].Tag as Layer, targetLayer, targetNode.Index);
                        return;
                    }
                }
                // 엔티티 이동
                Document.Action.ActEntityDragMove(this.Document.Action.SelectedEntity, targetLayer, 0);
            }
            else
            {
                targetLayer = targetEntity.Owner as Layer;
                //엔티티 이동
                Document.Action.ActEntityDragMove(this.Document.Action.SelectedEntity, targetLayer, targetNode.Index);
            }
            trvEntity.ResumeLayout();
        }
        private void trvEntity_DragOver(object sender, DragEventArgs e)
        {
            var p = trvEntity.PointToClient(new System.Drawing.Point(e.X, e.Y));
            TreeNode targetNode = trvEntity.GetNodeAt(p);
            if (targetNode == null)
                e.Effect = DragDropEffects.None;
            else
                e.Effect = DragDropEffects.Move;
        }
        private void ppgEntity_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            string propertyName = e.ChangedItem.PropertyDescriptor.Name;
            var oldValue = e.OldValue;
            var newValue = e.ChangedItem.Value;
            Document.Action.ActEntityPropertyChanged(Document.Action.SelectedEntity, propertyName, oldValue, newValue);
        }

        #region ToolStrip 버튼들
        private void btnNew_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes != MessageBox.Show($"Do you really want to clear the document ?", "Document New", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;
            trvEntity.Nodes.Clear();
            this.FileName = string.Empty;
            this.Document.Action.ActNew();

            var pen = new PenDefault();
            this.Document.Action.ActEntityAdd(pen);
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            ofd.Filter = "sirius data files (*.sirius)|*.sirius|dxf cad files (*.dxf)|*.dxf|All Files (*.*)|*.*";
            ofd.Title = "Open File";
            ofd.FileName = string.Empty;
            DialogResult result = ofd.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                string ext = Path.GetExtension(ofd.FileName);
                if (0 == string.Compare(ext, ".dxf", true))
                {
                    trvEntity.SuspendLayout();
                    var doc = DocumentSerializer.OpenDxf(ofd.FileName);
                    trvEntity.ResumeLayout();
                    if (null == doc)
                    {
                        MessageBox.Show($"Fail to open : {ofd.FileName}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    this.Document = doc;
                }
                else if (0 == string.Compare(ext, ".sirius", true))
                {
                    trvEntity.SuspendLayout();
                    var doc = DocumentSerializer.OpenSirius(ofd.FileName);
                    trvEntity.ResumeLayout();
                    if (null == doc)
                    {
                        MessageBox.Show($"Fail to open : {ofd.FileName}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    this.Document = doc;
                }
                else
                {
                    MessageBox.Show($"Unsupported file extension : {ofd.FileName}", "File Type Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnImport_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Sirius data files (*.sirius)|*.sirius|DXF cad files (*.dxf)|*.dxf|All Files (*.*)|*.*";
            ofd.Title = "Import File";
            ofd.FileName = string.Empty;
            DialogResult result = ofd.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                string ext = Path.GetExtension(ofd.FileName);
                if (0 == string.Compare(ext, ".dxf", true))
                {
                    this.Document.Action.ActImportDxf(ofd.FileName, out var dummy);
                }
                else if (0 == string.Compare(ext, ".sirius", true))
                {
                    this.Document.Action.ActImportSirius(ofd.FileName, out var dummy);
                }
                else
                {
                    MessageBox.Show($"Unsupported file extension : {ofd.FileName}", "File Type Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.FileName))
                this.btnSaveAs_Click(sender, e);
            else
            {
                if (true == this.Document.Action.ActSave(this.FileName))
                    MessageBox.Show($"Success to save : {this.FileName}", "Document Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show($"Fail to save : {this.FileName}", "Document Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            sfd.Filter = "sirius data files (*.sirius)|*.sirius|All Files (*.*)|*.*";
            sfd.Title = "Save As ...";
            sfd.FileName = string.Empty;
            DialogResult result = sfd.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                this.Document.Action.ActSave(sfd.FileName);
                this.FileName = sfd.FileName;
            }
        }
        private void btnUndo_Click(object sender, EventArgs e)
        {
            trvEntity.SuspendLayout();
            Document.Action.ActUndo();
            trvEntity.ResumeLayout();
        }
        private void btnReDo_Click(object sender, EventArgs e)
        {
            trvEntity.SuspendLayout();
            Document.Action.ActRedo();
            trvEntity.ResumeLayout();
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
        private void btnPoint_Click(object sender, EventArgs e)
        {
            var point = new SpiralLab.Sirius.Point(0,0);
            var form = new PropertyForm(point);
            if (DialogResult.OK != form.ShowDialog(this))
                return;

            this.Document.Action.ActEntityAdd(point);
        }
        private void btnPoints_Click(object sender, EventArgs e)
        {
            var point = new Points(new List<Vertex>()
            {
                new Vertex(1,1),
                new Vertex(1,2),
                new Vertex(2,2),
                new Vertex(3,4),
            });
            var form = new PropertyForm(point);
            if (DialogResult.OK != form.ShowDialog(this))
                return;

            this.Document.Action.ActEntityAdd(point);
        }
        private void btnRaster_Click(object sender, EventArgs e)
        {
            var raster = new Raster();
            var form = new PropertyForm(raster);
            if (DialogResult.OK != form.ShowDialog(this))
                return;

            this.Document.Action.ActEntityAdd(raster);
        }
        private void btnLine_Click(object sender, EventArgs e)
        {
            var line = new Line(0, 0, 10, 10);
            var form = new PropertyForm(line);
            if (DialogResult.OK != form.ShowDialog(this))
                return;

            this.Document.Action.ActEntityAdd(line);
        }
        private void btnArc_Click(object sender, EventArgs e)
        {
            var arc = new Arc(0, 0, 10, 0, 90);
            var form = new PropertyForm(arc);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(arc);
        }
        private void btnCircle_Click(object sender, EventArgs e)
        {
            var circle = new Circle(0, 0, 10);
            var form = new PropertyForm(circle);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(circle);
        }
        private void btnTrepan_Click(object sender, EventArgs e)
        {
            var trepan = new Trepan(0, 0, 10, 2);
            var form = new PropertyForm(trepan);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(trepan);
        }
        private void btnRectangle_Click(object sender, EventArgs e)
        {
            var rectangle = new SpiralLab.Sirius.Rectangle(0, 0, 10, 10);
            var form = new PropertyForm(rectangle);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(rectangle);
        }
        private void btnLWPolyline_Click(object sender, EventArgs e)
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
        private void mnuPen_Click(object sender, EventArgs e)
        {
            IPen pen = new PenDefault();
            var form = new PropertyForm(pen);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(pen);
        }
        private void mnuMOTFBeginEnd_Click(object sender, EventArgs e)
        {
            {
                var motf = new MotfBegin();
                var form = new PropertyForm(motf);
                if (DialogResult.OK != form.ShowDialog(this))
                    return;
                this.Document.Action.ActEntityAdd(motf);
            }
            {
                var motf = new MotfEnd();
                var form = new PropertyForm(motf);
                if (DialogResult.OK != form.ShowDialog(this))
                    return;
                this.Document.Action.ActEntityAdd(motf);
            }
        }
        private void mnuOTFExtStartDelay_Click(object sender, EventArgs e)
        {
            var motf = new MotfExternalStartDelay();
            var form = new PropertyForm(motf);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(motf);
        }
        private void mnuMOTFWait_Click(object sender, EventArgs e)
        {
            var motf = new MotfWait();
            var form = new PropertyForm(motf);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(motf);
        }
        private void btnTimer_Click(object sender, EventArgs e)
        {
            var timer = new SpiralLab.Sirius.Timer();
            var form = new PropertyForm(timer);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(timer);
        }
        private void mnuVectorBeginEnd_Click(object sender, EventArgs e)
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
                var form = new PropertyForm(vec);
                if (DialogResult.OK != form.ShowDialog(this))
                    return;
                this.Document.Action.ActEntityAdd(vec);
            }
        }
        private void btnSpiral_Click(object sender, EventArgs e)
        {
            var spiral = new Spiral(2, 10, 10, true);
            var form = new PropertyForm(spiral);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(spiral);
        }
        private void btnSiriusText_Click(object sender, EventArgs e)
        {
            var text = new SiriusText("HELLO");
            var form = new PropertyForm(text);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(text);
        }
        private void timeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var text = new SiriusTextTime();
            var form = new PropertyForm(text);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(text);
        }
        private void dateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var text = new SiriusTextDate();
            var form = new PropertyForm(text);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(text);
        }
        private void serialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var text = new SiriusTextSerial();
            var form = new PropertyForm(text);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(text);
        }
        private void siriusTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var text = new SiriusText("HELLO");
            var form = new PropertyForm(text);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(text);
        }
        private void btnRotateCW_Click(object sender, EventArgs e)
        {
            var br = new BoundRect();
            foreach (var entity in this.Document.Action.SelectedEntity)
                br.Union(entity.BoundRect);
            if (br.IsEmpty)
                return;

            this.Document.Action.ActEntityRotate(this.Document.Action.SelectedEntity, -90, br.Center.X, br.Center.Y);
        }
        private void btnRotateCCW_Click(object sender, EventArgs e)
        {
            var br = new BoundRect();
            foreach (var entity in this.Document.Action.SelectedEntity)
                br.Union(entity.BoundRect);
            if (br.IsEmpty)
                return;

            this.Document.Action.ActEntityRotate(this.Document.Action.SelectedEntity, 90, br.Center.X, br.Center.Y);
        }
        private void originToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Document.Action.ActEntityAlign(this.Document.Action.SelectedEntity, EntityAlign.Origin);
        }
        private void leftToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Document.Action.ActEntityAlign(this.Document.Action.SelectedEntity, EntityAlign.Left);
        }
        private void rightToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Document.Action.ActEntityAlign(this.Document.Action.SelectedEntity, EntityAlign.Right);
        }
        private void topToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Document.Action.ActEntityAlign(this.Document.Action.SelectedEntity, EntityAlign.Top);
        }
        private void bottomToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Document.Action.ActEntityAlign(this.Document.Action.SelectedEntity, EntityAlign.Bottom);
        }
        private void bottomTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Document.Action.ActEntitySort(this.Document.Action.SelectedEntity, this.Document.Layers.Active, EntitySort.BottomToTop);
        }
        private void topBottomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Document.Action.ActEntitySort(this.Document.Action.SelectedEntity, this.Document.Layers.Active, EntitySort.TopToBottom);
        }
        private void leftRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Document.Action.ActEntitySort(this.Document.Action.SelectedEntity, this.Document.Layers.Active, EntitySort.LeftToRight);
        }
        private void rightLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Document.Action.ActEntitySort(this.Document.Action.SelectedEntity, this.Document.Layers.Active, EntitySort.RightToLeft);
        }
        private void btnDocumentInfo_Click(object sender, EventArgs e)
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
        private void Document_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            string propertyName = e.ChangedItem.PropertyDescriptor.Name;
            var oldValue = e.OldValue;
            var newValue = e.ChangedItem.Value;
            Document.Action.ActDocumentPropertyChanged(Document, propertyName, oldValue, newValue);
        }
        private void btnExplode_Click(object sender, EventArgs e)
        {
            trvEntity.SuspendLayout();
            this.Document.Action.ActEntityExplode(this.Document.Action.SelectedEntity);
            trvEntity.ResumeLayout();
        }
        private void btnHatch_Click(object sender, EventArgs e)
        {
            var form = new HatchForm();
            if (DialogResult.OK != form.ShowDialog(this))
                return;

            this.Document.Action.ActEntityHatch(this.Document.Action.SelectedEntity, form.Mode, form.Angle, form.Angle2, form.Interval, form.Exclude);
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            trvEntity.SuspendLayout();
            this.Document.Action.ActEntityDelete(this.Document.Action.SelectedEntity);
            trvEntity.ResumeLayout();
        }
        private void btnCopy_Click(object sender, EventArgs e)
        {
            Document.Action.ActEntityCopy(Document.Action.SelectedEntity);
        }
        private void btnCut_Click(object sender, EventArgs e)
        {
            Document.Action.ActEntityCut(Document.Action.SelectedEntity);
        }
        private void btnPaste_Click(object sender, EventArgs e)
        {
            this.view.Dp2Lp(this.currentMousePos, out float x, out float y);
            Document.Action.ActEntityPaste( Document.Layers.Active, x, y);
        }
        private void btnPasteArray_Click(object sender, EventArgs e)
        {
            var form = new PasteForm();
            form.Clipboard = SpiralLab.Sirius.Action.ClipBoard;
            DialogResult result = form.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                trvEntity.SuspendLayout();
                this.Document.Action.ActEntityPasteArray(Document.Layers.Active, form.Result, form.Position.X, form.Position.Y);
                trvEntity.ResumeLayout();
            }
        }
        private void btnLayer_Click(object sender, EventArgs e)
        {
            var layer = new Layer($"NoName{this.Document.Action.NewLayerIndex++}");
            var form = new PropertyForm(layer);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(layer);
        }
        private void btnBarcode1D_Click(object sender, EventArgs e)
        {
            var bcd = new Barcode1D("123456789");
            var form = new PropertyForm(bcd);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(bcd);
        }
        private void mnuDataMatrix_Click(object sender, EventArgs e)
        {
            var text = new BarcodeDataMatrix("SIRIUS");
            var form = new PropertyForm(text);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(text);
        }
        private void mnuQRCode_Click(object sender, EventArgs e)
        {
            var text = new BarcodeQR("SIRIUS");
            var form = new PropertyForm(text);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(text);
        }
        private void btnHPGL_Click(object sender, EventArgs e)
        {
            ofd.Filter = "hpgl files (*.plt)|*.plt|All Files (*.*)|*.*";
            ofd.Title = "Import HPGL File";
            ofd.FileName = string.Empty;
            DialogResult result = ofd.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            this.Document.Action.ActImportHPGL(ofd.FileName, out var dummy);
        }
        private void btmImage_Click(object sender, EventArgs e)
        {
            ofd.Filter = "image files (*.bmp)|*.bmp|All Files (*.*)|*.*";
            ofd.Title = "Import Image File";
            ofd.FileName = string.Empty;
            DialogResult result = ofd.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            var bitmap = new SpiralLab.Sirius.Bitmap(ofd.FileName);
            this.Document.Action.ActEntityAdd(bitmap);
        }
        private void textToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var text = new Text("HELLO");
            var form = new PropertyForm(text);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(text);
        }
        private void btnTextArc_Click(object sender, EventArgs e)
        {
            var text = new TextArc("HELLO");
            var form = new PropertyForm(text);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(text);
        }

        private void timeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var text = new TextTime();
            var form = new PropertyForm(text);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(text);
        }

        private void dateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var text = new TextDate();
            var form = new PropertyForm(text);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(text);
        }

        private void serialToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var text = new TextSerial();
            var form = new PropertyForm(text);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(text);
        }
        private void btnSiriusArcText_Click(object sender, EventArgs e)
        {
            var text = new SiriusTextArc("HELLO");
            var form = new PropertyForm(text);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(text);
        }

        private void mnuPenReturn_Click(object sender, EventArgs e)
        {
            var layer = this.Document.Layers.Active;
            uint counts = 0;
            foreach (var entity in layer)
                if (entity is IPen)
                    counts++;
            //현재 활성 레이어에 2개 이상의 펜이 있을 경우 생성가능하도록 처리
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

        private void btnEllipse_Click(object sender, EventArgs e)
        {
            var ellipse = new Ellipse(0, 0, 10, 6, 0, 360, 0);
            var form = new PropertyForm(ellipse);
            if (DialogResult.OK != form.ShowDialog(this))
                return;
            this.Document.Action.ActEntityAdd(ellipse);
        }
        #endregion

        private void contextMenuStrip2_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 그룹(Group), 언그룹(UnGroup) 메뉴의 활성화 여부 처리
            bool groupVisible = false;
            bool ungroupVisible = false;
            bool layer = false;
            bool group = false;
            foreach (var entity in Document.Action.SelectedEntity)
            {
                if (entity is Layer)
                {
                    layer = true;
                    break;
                }
                if (entity is Group)
                    group |= true;
            }
            if (layer)
            {
                e.Cancel = true;
                return;
            }
            groupVisible = Document.Action.SelectedEntity.Count >= 1;
            groupToolStripMenuItem.Enabled = groupVisible;            
            ungroupVisible = group;
            ungroupToolStripMenuItem.Enabled = ungroupVisible;
            e.Cancel = !groupVisible && !ungroupVisible;
        }
        private void groupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trvEntity.SuspendLayout();
            Document.Action.ActEntityGroup(Document.Action.SelectedEntity);
            trvEntity.ResumeLayout();
        }
        private void ungroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trvEntity.SuspendLayout();
            Document.Action.ActEntityUngroup(Document.Action.SelectedEntity);
            trvEntity.ResumeLayout();
        }

        private void slowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Document.Action.ActEntityLaserPathSimulateStart(Document.Action.SelectedEntity, this.View, SpiralLab.Sirius.Action.LaserPathSimSpped.Slow);
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Document.Action.ActEntityLaserPathSimulateStart(Document.Action.SelectedEntity, this.View, SpiralLab.Sirius.Action.LaserPathSimSpped.Normal);
        }

        private void fastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Document.Action.ActEntityLaserPathSimulateStart(Document.Action.SelectedEntity, this.View, SpiralLab.Sirius.Action.LaserPathSimSpped.Fast);
        }
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Document.Action.ActEntityLaserPathSimulateStop();
        }
    }
}
