namespace CustomEditor
{
    partial class CustomEditorForm
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomEditorForm));
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnDocumentInfo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnSaveAs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.btnCut = new System.Windows.Forms.ToolStripButton();
            this.btnPaste = new System.Windows.Forms.ToolStripButton();
            this.btnPasteArray = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUndo = new System.Windows.Forms.ToolStripButton();
            this.btnReDo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.btnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.btnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnZoomFit = new System.Windows.Forms.ToolStripButton();
            this.btnPan = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExplode = new System.Windows.Forms.ToolStripButton();
            this.btnHatch = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRotateCCW = new System.Windows.Forms.ToolStripButton();
            this.btnRotateCW = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.ddbAlignment = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuOrigin = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRight = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTop = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBottom = new System.Windows.Forms.ToolStripMenuItem();
            this.ddbSort = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuBottomTop = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTopBottom = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLeftRight = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRightLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.ddbSimulate = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuSlow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNormal = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFast = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuStop = new System.Windows.Forms.ToolStripMenuItem();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnImport = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.entityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.pointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.circleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rectangleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lWPolylineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spiralToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.siriusTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.penToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripSeparator();
            this.zoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.panToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alignToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.originToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.leftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bottomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bottomTopToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.topBottomToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.leftRightToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.rightLeftToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteArrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.groupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripSeparator();
            this.ungroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblName = new System.Windows.Forms.ToolStripStatusLabel();
            this.pgbProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.lblXPos = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblYPos = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblEntityCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblRenderTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblFileName = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnPoint = new System.Windows.Forms.ToolStripButton();
            this.btnPoints = new System.Windows.Forms.ToolStripButton();
            this.btnRaster = new System.Windows.Forms.ToolStripButton();
            this.btnLine = new System.Windows.Forms.ToolStripButton();
            this.btnArc = new System.Windows.Forms.ToolStripButton();
            this.btnCircle = new System.Windows.Forms.ToolStripButton();
            this.btnEllipse = new System.Windows.Forms.ToolStripButton();
            this.btnTrepan = new System.Windows.Forms.ToolStripButton();
            this.btnRectangle = new System.Windows.Forms.ToolStripButton();
            this.btnLWPolyline = new System.Windows.Forms.ToolStripButton();
            this.btnSpiral = new System.Windows.Forms.ToolStripButton();
            this.ddbSiriusText = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuSiriusText = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSiriusTextArc = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSiriusTime = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSiriusDate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSiriusSerial = new System.Windows.Forms.ToolStripMenuItem();
            this.ddbText = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuText = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTextArc = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuTextTime = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTextDate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTextSerial = new System.Windows.Forms.ToolStripMenuItem();
            this.btnBarcode1D = new System.Windows.Forms.ToolStripButton();
            this.ddbBarcode = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuDataMatrix = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQRCode = new System.Windows.Forms.ToolStripMenuItem();
            this.btnHPGL = new System.Windows.Forms.ToolStripButton();
            this.btmImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ddbPen = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuPen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem16 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuPenReturn = new System.Windows.Forms.ToolStripMenuItem();
            this.btnTimer = new System.Windows.Forms.ToolStripButton();
            this.ddbMotf = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuMOTFBeginEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOTFExtStartDelay = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMOTFWait = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLayer = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuVectorBeginEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.GLcontrol = new SharpGL.OpenGLControl();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.trvEntity = new SpiralLab.Sirius.MultiSelectTreeview();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblWH = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCenter = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblBound = new System.Windows.Forms.ToolStripStatusLabel();
            this.ppgEntity = new System.Windows.Forms.PropertyGrid();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GLcontrol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDocumentInfo,
            this.toolStripSeparator5,
            this.btnNew,
            this.btnOpen,
            this.btnSave,
            this.btnSaveAs,
            this.toolStripSeparator2,
            this.btnCopy,
            this.btnCut,
            this.btnPaste,
            this.btnPasteArray,
            this.toolStripSeparator7,
            this.btnUndo,
            this.btnReDo,
            this.toolStripSeparator8,
            this.btnZoomOut,
            this.btnZoomIn,
            this.btnZoomFit,
            this.btnPan,
            this.toolStripSeparator9,
            this.btnExplode,
            this.btnHatch,
            this.btnDelete,
            this.toolStripSeparator4,
            this.btnRotateCCW,
            this.btnRotateCW,
            this.toolStripSeparator10,
            this.ddbAlignment,
            this.ddbSort,
            this.ddbSimulate});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(761, 25);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnDocumentInfo
            // 
            this.btnDocumentInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDocumentInfo.Image = global::CustomEditor.Properties.Resources.icons8_coordinate_system_16;
            this.btnDocumentInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDocumentInfo.Name = "btnDocumentInfo";
            this.btnDocumentInfo.Size = new System.Drawing.Size(23, 22);
            this.btnDocumentInfo.Text = "Dimension";
            this.btnDocumentInfo.ToolTipText = "Document Information";
            this.btnDocumentInfo.Click += new System.EventHandler(this.btnDocumentInfo_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = global::CustomEditor.Properties.Resources.icons8_file_16;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(23, 22);
            this.btnNew.Text = "New";
            this.btnNew.ToolTipText = "New";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = global::CustomEditor.Properties.Resources.icons8_opened_folder_16;
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(23, 22);
            this.btnOpen.Text = "Open";
            this.btnOpen.ToolTipText = "Open";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = global::CustomEditor.Properties.Resources.icons8_save_16;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.Text = "Save";
            this.btnSave.ToolTipText = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveAs.Image = global::CustomEditor.Properties.Resources.icons8_save_as_16;
            this.btnSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(23, 22);
            this.btnSaveAs.Text = "Save As";
            this.btnSaveAs.ToolTipText = "Save As ...";
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCopy
            // 
            this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCopy.Image = global::CustomEditor.Properties.Resources.icons8_copy_16;
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(23, 22);
            this.btnCopy.Text = "Copy";
            this.btnCopy.ToolTipText = "Copy";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnCut
            // 
            this.btnCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCut.Image = global::CustomEditor.Properties.Resources.icons8_cutting_coupon_16;
            this.btnCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(23, 22);
            this.btnCut.Text = "Cut";
            this.btnCut.ToolTipText = "Cut";
            this.btnCut.Click += new System.EventHandler(this.btnCut_Click);
            // 
            // btnPaste
            // 
            this.btnPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPaste.Image = global::CustomEditor.Properties.Resources.icons8_paste_16;
            this.btnPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(23, 22);
            this.btnPaste.Text = "Paste";
            this.btnPaste.ToolTipText = "Paste";
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // btnPasteArray
            // 
            this.btnPasteArray.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPasteArray.Image = global::CustomEditor.Properties.Resources.paste_array;
            this.btnPasteArray.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPasteArray.Name = "btnPasteArray";
            this.btnPasteArray.Size = new System.Drawing.Size(23, 22);
            this.btnPasteArray.Text = "Paste Array";
            this.btnPasteArray.ToolTipText = "Paste Array";
            this.btnPasteArray.Click += new System.EventHandler(this.btnPasteArray_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // btnUndo
            // 
            this.btnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUndo.Image = global::CustomEditor.Properties.Resources.icons8_circled_left_2_filled_16;
            this.btnUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(23, 22);
            this.btnUndo.Text = "Undo";
            this.btnUndo.ToolTipText = "UnDo";
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnReDo
            // 
            this.btnReDo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnReDo.Image = global::CustomEditor.Properties.Resources.redo;
            this.btnReDo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReDo.Name = "btnReDo";
            this.btnReDo.Size = new System.Drawing.Size(23, 22);
            this.btnReDo.Text = "Redo";
            this.btnReDo.ToolTipText = "ReDo";
            this.btnReDo.Click += new System.EventHandler(this.btnReDo_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomOut.Image = global::CustomEditor.Properties.Resources.icons8_zoom_out_filled_16;
            this.btnZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(23, 22);
            this.btnZoomOut.Text = "Zoom Out";
            this.btnZoomOut.ToolTipText = "Zoom Out";
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomIn.Image = global::CustomEditor.Properties.Resources.icons8_zoom_in_16;
            this.btnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(23, 22);
            this.btnZoomIn.Text = "Zoom In";
            this.btnZoomIn.ToolTipText = "Zoom In";
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // btnZoomFit
            // 
            this.btnZoomFit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomFit.Image = global::CustomEditor.Properties.Resources.icons8_zoom_to_extents_filled_16;
            this.btnZoomFit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomFit.Name = "btnZoomFit";
            this.btnZoomFit.Size = new System.Drawing.Size(23, 22);
            this.btnZoomFit.Text = "Zoom Fit";
            this.btnZoomFit.ToolTipText = "Zoom Fit";
            this.btnZoomFit.Click += new System.EventHandler(this.btnZoomFit_Click);
            // 
            // btnPan
            // 
            this.btnPan.CheckOnClick = true;
            this.btnPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPan.Image = global::CustomEditor.Properties.Resources.hand_move;
            this.btnPan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPan.Name = "btnPan";
            this.btnPan.Size = new System.Drawing.Size(23, 22);
            this.btnPan.Text = "Pan";
            this.btnPan.ToolTipText = "Pan";
            this.btnPan.Click += new System.EventHandler(this.btnPan_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExplode
            // 
            this.btnExplode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExplode.Image = global::CustomEditor.Properties.Resources.explode;
            this.btnExplode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExplode.Name = "btnExplode";
            this.btnExplode.Size = new System.Drawing.Size(23, 22);
            this.btnExplode.Text = "Explode";
            this.btnExplode.ToolTipText = "Explode";
            this.btnExplode.Click += new System.EventHandler(this.btnExplode_Click);
            // 
            // btnHatch
            // 
            this.btnHatch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnHatch.Image = global::CustomEditor.Properties.Resources.hatch;
            this.btnHatch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHatch.Name = "btnHatch";
            this.btnHatch.Size = new System.Drawing.Size(23, 22);
            this.btnHatch.Text = "btnHatch";
            this.btnHatch.ToolTipText = "Hatch";
            this.btnHatch.Click += new System.EventHandler(this.btnHatch_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelete.Image = global::CustomEditor.Properties.Resources.icons8_delete_file_16;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(23, 22);
            this.btnDelete.Text = "Delete";
            this.btnDelete.ToolTipText = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnRotateCCW
            // 
            this.btnRotateCCW.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRotateCCW.Image = global::CustomEditor.Properties.Resources.rotate_left;
            this.btnRotateCCW.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRotateCCW.Name = "btnRotateCCW";
            this.btnRotateCCW.Size = new System.Drawing.Size(23, 22);
            this.btnRotateCCW.Text = "Rotate CW";
            this.btnRotateCCW.ToolTipText = "Rotate Counter Clock Wise";
            this.btnRotateCCW.Click += new System.EventHandler(this.btnRotateCCW_Click);
            // 
            // btnRotateCW
            // 
            this.btnRotateCW.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRotateCW.Image = global::CustomEditor.Properties.Resources.rotate_right;
            this.btnRotateCW.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRotateCW.Name = "btnRotateCW";
            this.btnRotateCW.Size = new System.Drawing.Size(23, 22);
            this.btnRotateCW.Text = "Rotate CCW";
            this.btnRotateCW.ToolTipText = "Rotate Clock Wise";
            this.btnRotateCW.Click += new System.EventHandler(this.btnRotateCW_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
            // 
            // ddbAlignment
            // 
            this.ddbAlignment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ddbAlignment.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOrigin,
            this.toolStripMenuItem10,
            this.mnuLeft,
            this.mnuRight,
            this.mnuTop,
            this.mnuBottom});
            this.ddbAlignment.Image = global::CustomEditor.Properties.Resources.right;
            this.ddbAlignment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ddbAlignment.Name = "ddbAlignment";
            this.ddbAlignment.Size = new System.Drawing.Size(29, 22);
            this.ddbAlignment.Text = "Alignment";
            this.ddbAlignment.ToolTipText = "Align";
            // 
            // mnuOrigin
            // 
            this.mnuOrigin.Image = global::CustomEditor.Properties.Resources.icons8_define_location_16;
            this.mnuOrigin.Name = "mnuOrigin";
            this.mnuOrigin.Size = new System.Drawing.Size(113, 22);
            this.mnuOrigin.Text = "Origin";
            this.mnuOrigin.Click += new System.EventHandler(this.originToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(110, 6);
            // 
            // mnuLeft
            // 
            this.mnuLeft.Image = global::CustomEditor.Properties.Resources.left;
            this.mnuLeft.Name = "mnuLeft";
            this.mnuLeft.Size = new System.Drawing.Size(113, 22);
            this.mnuLeft.Text = "Left";
            this.mnuLeft.Click += new System.EventHandler(this.leftToolStripMenuItem1_Click);
            // 
            // mnuRight
            // 
            this.mnuRight.Image = global::CustomEditor.Properties.Resources.right;
            this.mnuRight.Name = "mnuRight";
            this.mnuRight.Size = new System.Drawing.Size(113, 22);
            this.mnuRight.Text = "mnu";
            this.mnuRight.Click += new System.EventHandler(this.rightToolStripMenuItem1_Click);
            // 
            // mnuTop
            // 
            this.mnuTop.Image = global::CustomEditor.Properties.Resources.top;
            this.mnuTop.Name = "mnuTop";
            this.mnuTop.Size = new System.Drawing.Size(113, 22);
            this.mnuTop.Text = "Top";
            this.mnuTop.Click += new System.EventHandler(this.topToolStripMenuItem1_Click);
            // 
            // mnuBottom
            // 
            this.mnuBottom.Image = global::CustomEditor.Properties.Resources.bottom;
            this.mnuBottom.Name = "mnuBottom";
            this.mnuBottom.Size = new System.Drawing.Size(113, 22);
            this.mnuBottom.Text = "Bottom";
            this.mnuBottom.Click += new System.EventHandler(this.bottomToolStripMenuItem1_Click);
            // 
            // ddbSort
            // 
            this.ddbSort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ddbSort.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBottomTop,
            this.mnuTopBottom,
            this.mnuLeftRight,
            this.mnuRightLeft});
            this.ddbSort.Image = global::CustomEditor.Properties.Resources.Journey_32px;
            this.ddbSort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ddbSort.Name = "ddbSort";
            this.ddbSort.Size = new System.Drawing.Size(29, 22);
            this.ddbSort.Text = "Sort";
            this.ddbSort.ToolTipText = "Sort";
            // 
            // mnuBottomTop
            // 
            this.mnuBottomTop.Name = "mnuBottomTop";
            this.mnuBottomTop.Size = new System.Drawing.Size(150, 22);
            this.mnuBottomTop.Text = "Bottom -> Top";
            this.mnuBottomTop.Click += new System.EventHandler(this.bottomTopToolStripMenuItem_Click);
            // 
            // mnuTopBottom
            // 
            this.mnuTopBottom.Name = "mnuTopBottom";
            this.mnuTopBottom.Size = new System.Drawing.Size(150, 22);
            this.mnuTopBottom.Text = "Top -> Bottom";
            this.mnuTopBottom.Click += new System.EventHandler(this.topBottomToolStripMenuItem_Click);
            // 
            // mnuLeftRight
            // 
            this.mnuLeftRight.Name = "mnuLeftRight";
            this.mnuLeftRight.Size = new System.Drawing.Size(150, 22);
            this.mnuLeftRight.Text = "Left -> Right";
            this.mnuLeftRight.Click += new System.EventHandler(this.leftRightToolStripMenuItem_Click);
            // 
            // mnuRightLeft
            // 
            this.mnuRightLeft.Name = "mnuRightLeft";
            this.mnuRightLeft.Size = new System.Drawing.Size(150, 22);
            this.mnuRightLeft.Text = "Right -> Left";
            this.mnuRightLeft.Click += new System.EventHandler(this.rightLeftToolStripMenuItem_Click);
            // 
            // ddbSimulate
            // 
            this.ddbSimulate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ddbSimulate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSlow,
            this.mnuNormal,
            this.mnuFast,
            this.toolStripMenuItem15,
            this.mnuStop});
            this.ddbSimulate.Image = global::CustomEditor.Properties.Resources.start_48px;
            this.ddbSimulate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ddbSimulate.Name = "ddbSimulate";
            this.ddbSimulate.Size = new System.Drawing.Size(29, 22);
            this.ddbSimulate.Text = "Laser Path Simulator";
            // 
            // mnuSlow
            // 
            this.mnuSlow.Name = "mnuSlow";
            this.mnuSlow.Size = new System.Drawing.Size(115, 22);
            this.mnuSlow.Text = "Slow";
            this.mnuSlow.ToolTipText = "Slow";
            this.mnuSlow.Click += new System.EventHandler(this.slowToolStripMenuItem_Click);
            // 
            // mnuNormal
            // 
            this.mnuNormal.Name = "mnuNormal";
            this.mnuNormal.Size = new System.Drawing.Size(115, 22);
            this.mnuNormal.Text = "Normal";
            this.mnuNormal.ToolTipText = "Normal";
            this.mnuNormal.Click += new System.EventHandler(this.normalToolStripMenuItem_Click);
            // 
            // mnuFast
            // 
            this.mnuFast.Name = "mnuFast";
            this.mnuFast.Size = new System.Drawing.Size(115, 22);
            this.mnuFast.Text = "Fast";
            this.mnuFast.ToolTipText = "Fast";
            this.mnuFast.Click += new System.EventHandler(this.fastToolStripMenuItem_Click);
            // 
            // toolStripMenuItem15
            // 
            this.toolStripMenuItem15.Name = "toolStripMenuItem15";
            this.toolStripMenuItem15.Size = new System.Drawing.Size(112, 6);
            // 
            // mnuStop
            // 
            this.mnuStop.Image = global::CustomEditor.Properties.Resources.stop_16px1;
            this.mnuStop.Name = "mnuStop";
            this.mnuStop.Size = new System.Drawing.Size(115, 22);
            this.mnuStop.Text = "Stop";
            this.mnuStop.ToolTipText = "Stop";
            this.mnuStop.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 25);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(761, 1);
            this.splitter1.TabIndex = 11;
            this.splitter1.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.entityToolStripMenuItem,
            this.zoomToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolStripMenuItem3});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(107, 98);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.btnImport,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.infoToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.icons8_file_16;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.icons8_opened_folder_16;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.openToolStripMenuItem.Text = "&Open ...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnImport
            // 
            this.btnImport.Image = global::CustomEditor.Properties.Resources.import_50px;
            this.btnImport.Name = "btnImport";
            this.btnImport.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.I)));
            this.btnImport.Size = new System.Drawing.Size(201, 22);
            this.btnImport.Text = "Import ...";
            this.btnImport.ToolTipText = "Import";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.icons8_save_16;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.icons8_save_as_16;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As ...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(198, 6);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.icons8_coordinate_system_16;
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.infoToolStripMenuItem.Text = "&Info";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.btnDocumentInfo_Click);
            // 
            // entityToolStripMenuItem
            // 
            this.entityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.layerToolStripMenuItem,
            this.toolStripMenuItem4,
            this.pointToolStripMenuItem,
            this.lineToolStripMenuItem,
            this.arcToolStripMenuItem,
            this.circleToolStripMenuItem,
            this.rectangleToolStripMenuItem,
            this.lWPolylineToolStripMenuItem,
            this.spiralToolStripMenuItem,
            this.siriusTextToolStripMenuItem,
            this.textToolStripMenuItem,
            this.toolStripMenuItem1,
            this.penToolStripMenuItem,
            this.timerToolStripMenuItem,
            this.toolStripMenuItem11});
            this.entityToolStripMenuItem.Name = "entityToolStripMenuItem";
            this.entityToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.entityToolStripMenuItem.Text = "Entity";
            // 
            // layerToolStripMenuItem
            // 
            this.layerToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.icons8_layers_16;
            this.layerToolStripMenuItem.Name = "layerToolStripMenuItem";
            this.layerToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.layerToolStripMenuItem.Text = "Layer";
            this.layerToolStripMenuItem.Click += new System.EventHandler(this.btnLayer_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(134, 6);
            // 
            // pointToolStripMenuItem
            // 
            this.pointToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.point;
            this.pointToolStripMenuItem.Name = "pointToolStripMenuItem";
            this.pointToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.pointToolStripMenuItem.Text = "Point";
            this.pointToolStripMenuItem.Click += new System.EventHandler(this.btnPoint_Click);
            // 
            // lineToolStripMenuItem
            // 
            this.lineToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.icons8_line_16;
            this.lineToolStripMenuItem.Name = "lineToolStripMenuItem";
            this.lineToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.lineToolStripMenuItem.Text = "Line";
            this.lineToolStripMenuItem.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // arcToolStripMenuItem
            // 
            this.arcToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.icons8_halfcircle;
            this.arcToolStripMenuItem.Name = "arcToolStripMenuItem";
            this.arcToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.arcToolStripMenuItem.Text = "Arc";
            this.arcToolStripMenuItem.Click += new System.EventHandler(this.btnArc_Click);
            // 
            // circleToolStripMenuItem
            // 
            this.circleToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.icons8_circle_16;
            this.circleToolStripMenuItem.Name = "circleToolStripMenuItem";
            this.circleToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.circleToolStripMenuItem.Text = "Circle";
            this.circleToolStripMenuItem.Click += new System.EventHandler(this.btnCircle_Click);
            // 
            // rectangleToolStripMenuItem
            // 
            this.rectangleToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.icons8_rectangle_stroked_16;
            this.rectangleToolStripMenuItem.Name = "rectangleToolStripMenuItem";
            this.rectangleToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.rectangleToolStripMenuItem.Text = "Rectangle";
            this.rectangleToolStripMenuItem.Click += new System.EventHandler(this.btnRectangle_Click);
            // 
            // lWPolylineToolStripMenuItem
            // 
            this.lWPolylineToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.icons8_polyline_16;
            this.lWPolylineToolStripMenuItem.Name = "lWPolylineToolStripMenuItem";
            this.lWPolylineToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.lWPolylineToolStripMenuItem.Text = "LW Polyline";
            this.lWPolylineToolStripMenuItem.Click += new System.EventHandler(this.btnLWPolyline_Click);
            // 
            // spiralToolStripMenuItem
            // 
            this.spiralToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.spiral;
            this.spiralToolStripMenuItem.Name = "spiralToolStripMenuItem";
            this.spiralToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.spiralToolStripMenuItem.Text = "Spiral";
            this.spiralToolStripMenuItem.Click += new System.EventHandler(this.btnSpiral_Click);
            // 
            // siriusTextToolStripMenuItem
            // 
            this.siriusTextToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.font_size_16px;
            this.siriusTextToolStripMenuItem.Name = "siriusTextToolStripMenuItem";
            this.siriusTextToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.siriusTextToolStripMenuItem.Text = "Sirius Text";
            this.siriusTextToolStripMenuItem.Click += new System.EventHandler(this.siriusTextToolStripMenuItem_Click);
            // 
            // textToolStripMenuItem
            // 
            this.textToolStripMenuItem.Name = "textToolStripMenuItem";
            this.textToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(134, 6);
            // 
            // penToolStripMenuItem
            // 
            this.penToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.icons8_pencil_16;
            this.penToolStripMenuItem.Name = "penToolStripMenuItem";
            this.penToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.penToolStripMenuItem.Text = "Pen";
            // 
            // timerToolStripMenuItem
            // 
            this.timerToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.icons8_timer_16;
            this.timerToolStripMenuItem.Name = "timerToolStripMenuItem";
            this.timerToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.timerToolStripMenuItem.Text = "Timer";
            this.timerToolStripMenuItem.Click += new System.EventHandler(this.btnTimer_Click);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(134, 6);
            // 
            // zoomToolStripMenuItem
            // 
            this.zoomToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iNToolStripMenuItem,
            this.outToolStripMenuItem,
            this.fitToolStripMenuItem,
            this.toolStripMenuItem9,
            this.panToolStripMenuItem});
            this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
            this.zoomToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.zoomToolStripMenuItem.Text = "&Zoom";
            // 
            // iNToolStripMenuItem
            // 
            this.iNToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.icons8_zoom_in_16;
            this.iNToolStripMenuItem.Name = "iNToolStripMenuItem";
            this.iNToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Oemplus)));
            this.iNToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.iNToolStripMenuItem.Text = "In";
            this.iNToolStripMenuItem.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // outToolStripMenuItem
            // 
            this.outToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.icons8_zoom_out_filled_16;
            this.outToolStripMenuItem.Name = "outToolStripMenuItem";
            this.outToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.OemMinus)));
            this.outToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.outToolStripMenuItem.Text = "Out";
            this.outToolStripMenuItem.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // fitToolStripMenuItem
            // 
            this.fitToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.icons8_zoom_to_extents_filled_16;
            this.fitToolStripMenuItem.Name = "fitToolStripMenuItem";
            this.fitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.fitToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.fitToolStripMenuItem.Text = "Fit";
            this.fitToolStripMenuItem.Click += new System.EventHandler(this.btnZoomFit_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(183, 6);
            // 
            // panToolStripMenuItem
            // 
            this.panToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.hand_move;
            this.panToolStripMenuItem.Name = "panToolStripMenuItem";
            this.panToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.panToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.panToolStripMenuItem.Text = "Pan";
            this.panToolStripMenuItem.Click += new System.EventHandler(this.btnPan_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripMenuItem8,
            this.deleteToolStripMenuItem,
            this.alignToolStripMenuItem,
            this.sortToolStripMenuItem,
            this.toolStripMenuItem5,
            this.copyToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.pasteArrayToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.icons8_circled_left_2_filled_16;
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.redo;
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.btnReDo_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(192, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.icons8_delete_file_16;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)));
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // alignToolStripMenuItem
            // 
            this.alignToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.originToolStripMenuItem,
            this.toolStripMenuItem7,
            this.leftToolStripMenuItem,
            this.rightToolStripMenuItem,
            this.topToolStripMenuItem,
            this.bottomToolStripMenuItem});
            this.alignToolStripMenuItem.Name = "alignToolStripMenuItem";
            this.alignToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.alignToolStripMenuItem.Text = "Align";
            // 
            // originToolStripMenuItem
            // 
            this.originToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.icons8_define_location_16;
            this.originToolStripMenuItem.Name = "originToolStripMenuItem";
            this.originToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.originToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.originToolStripMenuItem.Text = "Origin";
            this.originToolStripMenuItem.Click += new System.EventHandler(this.originToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(175, 6);
            // 
            // leftToolStripMenuItem
            // 
            this.leftToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.left;
            this.leftToolStripMenuItem.Name = "leftToolStripMenuItem";
            this.leftToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)));
            this.leftToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.leftToolStripMenuItem.Text = "Left";
            this.leftToolStripMenuItem.Click += new System.EventHandler(this.leftToolStripMenuItem1_Click);
            // 
            // rightToolStripMenuItem
            // 
            this.rightToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.right;
            this.rightToolStripMenuItem.Name = "rightToolStripMenuItem";
            this.rightToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)));
            this.rightToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.rightToolStripMenuItem.Text = "Right";
            this.rightToolStripMenuItem.Click += new System.EventHandler(this.rightToolStripMenuItem1_Click);
            // 
            // topToolStripMenuItem
            // 
            this.topToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.top;
            this.topToolStripMenuItem.Name = "topToolStripMenuItem";
            this.topToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.topToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.topToolStripMenuItem.Text = "Top";
            this.topToolStripMenuItem.Click += new System.EventHandler(this.topToolStripMenuItem1_Click);
            // 
            // bottomToolStripMenuItem
            // 
            this.bottomToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.bottom;
            this.bottomToolStripMenuItem.Name = "bottomToolStripMenuItem";
            this.bottomToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.bottomToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.bottomToolStripMenuItem.Text = "Bottom";
            this.bottomToolStripMenuItem.Click += new System.EventHandler(this.bottomToolStripMenuItem1_Click);
            // 
            // sortToolStripMenuItem
            // 
            this.sortToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bottomTopToolStripMenuItem1,
            this.topBottomToolStripMenuItem1,
            this.leftRightToolStripMenuItem1,
            this.rightLeftToolStripMenuItem1});
            this.sortToolStripMenuItem.Name = "sortToolStripMenuItem";
            this.sortToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.sortToolStripMenuItem.Text = "Sort";
            // 
            // bottomTopToolStripMenuItem1
            // 
            this.bottomTopToolStripMenuItem1.Name = "bottomTopToolStripMenuItem1";
            this.bottomTopToolStripMenuItem1.Size = new System.Drawing.Size(150, 22);
            this.bottomTopToolStripMenuItem1.Text = "Bottom -> Top";
            this.bottomTopToolStripMenuItem1.Click += new System.EventHandler(this.bottomTopToolStripMenuItem_Click);
            // 
            // topBottomToolStripMenuItem1
            // 
            this.topBottomToolStripMenuItem1.Name = "topBottomToolStripMenuItem1";
            this.topBottomToolStripMenuItem1.Size = new System.Drawing.Size(150, 22);
            this.topBottomToolStripMenuItem1.Text = "Top -> Bottom";
            this.topBottomToolStripMenuItem1.Click += new System.EventHandler(this.topBottomToolStripMenuItem_Click);
            // 
            // leftRightToolStripMenuItem1
            // 
            this.leftRightToolStripMenuItem1.Name = "leftRightToolStripMenuItem1";
            this.leftRightToolStripMenuItem1.Size = new System.Drawing.Size(150, 22);
            this.leftRightToolStripMenuItem1.Text = "Left -> Right";
            this.leftRightToolStripMenuItem1.Click += new System.EventHandler(this.leftRightToolStripMenuItem_Click);
            // 
            // rightLeftToolStripMenuItem1
            // 
            this.rightLeftToolStripMenuItem1.Name = "rightLeftToolStripMenuItem1";
            this.rightLeftToolStripMenuItem1.Size = new System.Drawing.Size(150, 22);
            this.rightLeftToolStripMenuItem1.Text = "Right -> Left";
            this.rightLeftToolStripMenuItem1.Click += new System.EventHandler(this.rightLeftToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(192, 6);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.icons8_copy_16;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.icons8_cutting_coupon_16;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.btnCut_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.icons8_paste_16;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // pasteArrayToolStripMenuItem
            // 
            this.pasteArrayToolStripMenuItem.Image = global::CustomEditor.Properties.Resources.paste_array;
            this.pasteArrayToolStripMenuItem.Name = "pasteArrayToolStripMenuItem";
            this.pasteArrayToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.V)));
            this.pasteArrayToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.pasteArrayToolStripMenuItem.Text = "Paste Array";
            this.pasteArrayToolStripMenuItem.Click += new System.EventHandler(this.btnPasteArray_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(103, 6);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.groupToolStripMenuItem,
            this.toolStripMenuItem13,
            this.ungroupToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(122, 54);
            this.contextMenuStrip2.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip2_Opening);
            // 
            // groupToolStripMenuItem
            // 
            this.groupToolStripMenuItem.Name = "groupToolStripMenuItem";
            this.groupToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.groupToolStripMenuItem.Text = "Group";
            this.groupToolStripMenuItem.Click += new System.EventHandler(this.groupToolStripMenuItem_Click);
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(118, 6);
            // 
            // ungroupToolStripMenuItem
            // 
            this.ungroupToolStripMenuItem.Name = "ungroupToolStripMenuItem";
            this.ungroupToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.ungroupToolStripMenuItem.Text = "Ungroup";
            this.ungroupToolStripMenuItem.Click += new System.EventHandler(this.ungroupToolStripMenuItem_Click);
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(63, 17);
            this.lblName.Text = "NoName";
            // 
            // pgbProgress
            // 
            this.pgbProgress.AutoSize = false;
            this.pgbProgress.Name = "pgbProgress";
            this.pgbProgress.Size = new System.Drawing.Size(67, 16);
            this.pgbProgress.Step = 1;
            // 
            // lblXPos
            // 
            this.lblXPos.AutoSize = false;
            this.lblXPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblXPos.Name = "lblXPos";
            this.lblXPos.Size = new System.Drawing.Size(80, 17);
            this.lblXPos.Text = "X: 0.000";
            // 
            // lblYPos
            // 
            this.lblYPos.AutoSize = false;
            this.lblYPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYPos.Name = "lblYPos";
            this.lblYPos.Size = new System.Drawing.Size(80, 17);
            this.lblYPos.Text = "Y: 0.000";
            // 
            // lblEntityCount
            // 
            this.lblEntityCount.AutoSize = false;
            this.lblEntityCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntityCount.Name = "lblEntityCount";
            this.lblEntityCount.Size = new System.Drawing.Size(80, 17);
            this.lblEntityCount.Text = "Selected: 0";
            // 
            // lblRenderTime
            // 
            this.lblRenderTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenderTime.Name = "lblRenderTime";
            this.lblRenderTime.Size = new System.Drawing.Size(81, 17);
            this.lblRenderTime.Text = "Render: 0 ms";
            // 
            // lblFileName
            // 
            this.lblFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(57, 17);
            this.lblFileName.Text = "NoName";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblName,
            this.pgbProgress,
            this.lblXPos,
            this.lblYPos,
            this.lblEntityCount,
            this.lblRenderTime,
            this.lblFileName});
            this.statusStrip1.Location = new System.Drawing.Point(0, 596);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(761, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip2.GripMargin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPoint,
            this.btnPoints,
            this.btnRaster,
            this.btnLine,
            this.btnArc,
            this.btnCircle,
            this.btnEllipse,
            this.btnTrepan,
            this.btnRectangle,
            this.btnLWPolyline,
            this.btnSpiral,
            this.ddbSiriusText,
            this.ddbText,
            this.btnBarcode1D,
            this.ddbBarcode,
            this.btnHPGL,
            this.btmImage,
            this.toolStripSeparator1,
            this.ddbPen,
            this.btnTimer,
            this.ddbMotf,
            this.toolStripSeparator3,
            this.btnLayer,
            this.toolStripDropDownButton1});
            this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip2.Location = new System.Drawing.Point(0, 26);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(761, 25);
            this.toolStrip2.TabIndex = 13;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnPoint
            // 
            this.btnPoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPoint.Image = global::CustomEditor.Properties.Resources.point;
            this.btnPoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPoint.Name = "btnPoint";
            this.btnPoint.Size = new System.Drawing.Size(23, 22);
            this.btnPoint.Text = "Point Entity";
            this.btnPoint.ToolTipText = "Point";
            this.btnPoint.Click += new System.EventHandler(this.btnPoint_Click);
            // 
            // btnPoints
            // 
            this.btnPoints.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPoints.Image = global::CustomEditor.Properties.Resources.points;
            this.btnPoints.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPoints.Name = "btnPoints";
            this.btnPoints.Size = new System.Drawing.Size(23, 22);
            this.btnPoints.Text = "Points Entity";
            this.btnPoints.ToolTipText = "Points";
            this.btnPoints.Click += new System.EventHandler(this.btnPoints_Click);
            // 
            // btnRaster
            // 
            this.btnRaster.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRaster.Image = global::CustomEditor.Properties.Resources.raster;
            this.btnRaster.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRaster.Name = "btnRaster";
            this.btnRaster.Size = new System.Drawing.Size(23, 22);
            this.btnRaster.Text = "Raster";
            this.btnRaster.ToolTipText = "Raster";
            this.btnRaster.Click += new System.EventHandler(this.btnRaster_Click);
            // 
            // btnLine
            // 
            this.btnLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLine.Image = global::CustomEditor.Properties.Resources.icons8_line_16;
            this.btnLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(23, 22);
            this.btnLine.Text = "Line Entity";
            this.btnLine.ToolTipText = "Line";
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // btnArc
            // 
            this.btnArc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnArc.Image = global::CustomEditor.Properties.Resources.icons8_halfcircle;
            this.btnArc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnArc.Name = "btnArc";
            this.btnArc.Size = new System.Drawing.Size(23, 22);
            this.btnArc.Text = "Arc Entity";
            this.btnArc.ToolTipText = "Arc";
            this.btnArc.Click += new System.EventHandler(this.btnArc_Click);
            // 
            // btnCircle
            // 
            this.btnCircle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCircle.Image = global::CustomEditor.Properties.Resources.icons8_circle_16;
            this.btnCircle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.Size = new System.Drawing.Size(23, 22);
            this.btnCircle.Text = "Circle Entity";
            this.btnCircle.ToolTipText = "Circle";
            this.btnCircle.Click += new System.EventHandler(this.btnCircle_Click);
            // 
            // btnEllipse
            // 
            this.btnEllipse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEllipse.Image = global::CustomEditor.Properties.Resources.ellipse;
            this.btnEllipse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEllipse.Name = "btnEllipse";
            this.btnEllipse.Size = new System.Drawing.Size(23, 22);
            this.btnEllipse.Text = "Ellipse";
            this.btnEllipse.Click += new System.EventHandler(this.btnEllipse_Click);
            // 
            // btnTrepan
            // 
            this.btnTrepan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTrepan.Image = global::CustomEditor.Properties.Resources.trepan;
            this.btnTrepan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTrepan.Name = "btnTrepan";
            this.btnTrepan.Size = new System.Drawing.Size(23, 22);
            this.btnTrepan.ToolTipText = "Trepan";
            this.btnTrepan.Click += new System.EventHandler(this.btnTrepan_Click);
            // 
            // btnRectangle
            // 
            this.btnRectangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRectangle.Image = global::CustomEditor.Properties.Resources.icons8_rectangle_stroked_16;
            this.btnRectangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRectangle.Name = "btnRectangle";
            this.btnRectangle.Size = new System.Drawing.Size(23, 22);
            this.btnRectangle.Text = "Rectangle Entity";
            this.btnRectangle.ToolTipText = "Rectangle";
            this.btnRectangle.Click += new System.EventHandler(this.btnRectangle_Click);
            // 
            // btnLWPolyline
            // 
            this.btnLWPolyline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLWPolyline.Image = global::CustomEditor.Properties.Resources.polyline_16px;
            this.btnLWPolyline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLWPolyline.Name = "btnLWPolyline";
            this.btnLWPolyline.Size = new System.Drawing.Size(23, 22);
            this.btnLWPolyline.Text = "Polyline Entity";
            this.btnLWPolyline.ToolTipText = "LW Polyline";
            this.btnLWPolyline.Click += new System.EventHandler(this.btnLWPolyline_Click);
            // 
            // btnSpiral
            // 
            this.btnSpiral.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSpiral.Image = global::CustomEditor.Properties.Resources.spiral;
            this.btnSpiral.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSpiral.Name = "btnSpiral";
            this.btnSpiral.Size = new System.Drawing.Size(23, 22);
            this.btnSpiral.Text = "Spiral Entity";
            this.btnSpiral.ToolTipText = "Spiral";
            this.btnSpiral.Click += new System.EventHandler(this.btnSpiral_Click);
            // 
            // ddbSiriusText
            // 
            this.ddbSiriusText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ddbSiriusText.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSiriusText,
            this.mnuSiriusTextArc,
            this.toolStripMenuItem6,
            this.mnuSiriusTime,
            this.mnuSiriusDate,
            this.mnuSiriusSerial});
            this.ddbSiriusText.Image = global::CustomEditor.Properties.Resources.font_size_16px;
            this.ddbSiriusText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ddbSiriusText.Name = "ddbSiriusText";
            this.ddbSiriusText.Size = new System.Drawing.Size(29, 22);
            this.ddbSiriusText.Text = "Special text entity";
            this.ddbSiriusText.ToolTipText = "Sirius Text";
            // 
            // mnuSiriusText
            // 
            this.mnuSiriusText.Image = global::CustomEditor.Properties.Resources.font_size_16px;
            this.mnuSiriusText.Name = "mnuSiriusText";
            this.mnuSiriusText.Size = new System.Drawing.Size(123, 22);
            this.mnuSiriusText.Text = "Text";
            this.mnuSiriusText.ToolTipText = "Text";
            this.mnuSiriusText.Click += new System.EventHandler(this.btnSiriusText_Click);
            // 
            // mnuSiriusTextArc
            // 
            this.mnuSiriusTextArc.Image = global::CustomEditor.Properties.Resources.font_rotate;
            this.mnuSiriusTextArc.Name = "mnuSiriusTextArc";
            this.mnuSiriusTextArc.Size = new System.Drawing.Size(123, 22);
            this.mnuSiriusTextArc.Text = "Text (Arc)";
            this.mnuSiriusTextArc.Click += new System.EventHandler(this.btnSiriusArcText_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(120, 6);
            // 
            // mnuSiriusTime
            // 
            this.mnuSiriusTime.Image = global::CustomEditor.Properties.Resources.hour;
            this.mnuSiriusTime.Name = "mnuSiriusTime";
            this.mnuSiriusTime.Size = new System.Drawing.Size(123, 22);
            this.mnuSiriusTime.Text = "Time";
            this.mnuSiriusTime.ToolTipText = "Time";
            this.mnuSiriusTime.Click += new System.EventHandler(this.timeToolStripMenuItem_Click);
            // 
            // mnuSiriusDate
            // 
            this.mnuSiriusDate.Image = global::CustomEditor.Properties.Resources.date;
            this.mnuSiriusDate.Name = "mnuSiriusDate";
            this.mnuSiriusDate.Size = new System.Drawing.Size(123, 22);
            this.mnuSiriusDate.Text = "Date";
            this.mnuSiriusDate.ToolTipText = "Date";
            this.mnuSiriusDate.Click += new System.EventHandler(this.dateToolStripMenuItem_Click);
            // 
            // mnuSiriusSerial
            // 
            this.mnuSiriusSerial.Image = global::CustomEditor.Properties.Resources.serial;
            this.mnuSiriusSerial.Name = "mnuSiriusSerial";
            this.mnuSiriusSerial.Size = new System.Drawing.Size(123, 22);
            this.mnuSiriusSerial.Text = "Serial";
            this.mnuSiriusSerial.ToolTipText = "Serial";
            this.mnuSiriusSerial.Click += new System.EventHandler(this.serialToolStripMenuItem_Click);
            // 
            // ddbText
            // 
            this.ddbText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ddbText.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuText,
            this.mnuTextArc,
            this.toolStripMenuItem12,
            this.mnuTextTime,
            this.mnuTextDate,
            this.mnuTextSerial});
            this.ddbText.Image = global::CustomEditor.Properties.Resources.icons8_generic_text_16;
            this.ddbText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ddbText.Name = "ddbText";
            this.ddbText.Size = new System.Drawing.Size(29, 22);
            this.ddbText.Text = "True type font";
            this.ddbText.ToolTipText = "True Type Font";
            // 
            // mnuText
            // 
            this.mnuText.Image = global::CustomEditor.Properties.Resources.icons8_generic_text_16;
            this.mnuText.Name = "mnuText";
            this.mnuText.Size = new System.Drawing.Size(123, 22);
            this.mnuText.Text = "Text";
            this.mnuText.ToolTipText = "Text";
            this.mnuText.Click += new System.EventHandler(this.textToolStripMenuItem1_Click);
            // 
            // mnuTextArc
            // 
            this.mnuTextArc.Name = "mnuTextArc";
            this.mnuTextArc.Size = new System.Drawing.Size(123, 22);
            this.mnuTextArc.Text = "Text (Arc)";
            this.mnuTextArc.Click += new System.EventHandler(this.btnTextArc_Click);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(120, 6);
            // 
            // mnuTextTime
            // 
            this.mnuTextTime.Enabled = false;
            this.mnuTextTime.Image = global::CustomEditor.Properties.Resources.hour;
            this.mnuTextTime.Name = "mnuTextTime";
            this.mnuTextTime.Size = new System.Drawing.Size(123, 22);
            this.mnuTextTime.Text = "Time";
            this.mnuTextTime.ToolTipText = "Time";
            this.mnuTextTime.Click += new System.EventHandler(this.timeToolStripMenuItem1_Click);
            // 
            // mnuTextDate
            // 
            this.mnuTextDate.Enabled = false;
            this.mnuTextDate.Image = global::CustomEditor.Properties.Resources.date;
            this.mnuTextDate.Name = "mnuTextDate";
            this.mnuTextDate.Size = new System.Drawing.Size(123, 22);
            this.mnuTextDate.Text = "Date";
            this.mnuTextDate.ToolTipText = "Date";
            this.mnuTextDate.Click += new System.EventHandler(this.dateToolStripMenuItem1_Click);
            // 
            // mnuTextSerial
            // 
            this.mnuTextSerial.Enabled = false;
            this.mnuTextSerial.Image = global::CustomEditor.Properties.Resources.serial;
            this.mnuTextSerial.Name = "mnuTextSerial";
            this.mnuTextSerial.Size = new System.Drawing.Size(123, 22);
            this.mnuTextSerial.Text = "Serial";
            this.mnuTextSerial.ToolTipText = "Serial";
            this.mnuTextSerial.Click += new System.EventHandler(this.serialToolStripMenuItem1_Click);
            // 
            // btnBarcode1D
            // 
            this.btnBarcode1D.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBarcode1D.Image = global::CustomEditor.Properties.Resources.barcode_24px;
            this.btnBarcode1D.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBarcode1D.Name = "btnBarcode1D";
            this.btnBarcode1D.Size = new System.Drawing.Size(23, 22);
            this.btnBarcode1D.Text = "1D Barcode";
            this.btnBarcode1D.ToolTipText = "1D Barcode";
            this.btnBarcode1D.Click += new System.EventHandler(this.btnBarcode1D_Click);
            // 
            // ddbBarcode
            // 
            this.ddbBarcode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ddbBarcode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDataMatrix,
            this.mnuQRCode});
            this.ddbBarcode.Image = global::CustomEditor.Properties.Resources.data_matrix_code_50px;
            this.ddbBarcode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ddbBarcode.Name = "ddbBarcode";
            this.ddbBarcode.Size = new System.Drawing.Size(29, 22);
            this.ddbBarcode.Text = "Data matrix entity";
            this.ddbBarcode.ToolTipText = "2D Barcode";
            // 
            // mnuDataMatrix
            // 
            this.mnuDataMatrix.Image = global::CustomEditor.Properties.Resources.data_matrix_code_50px;
            this.mnuDataMatrix.Name = "mnuDataMatrix";
            this.mnuDataMatrix.Size = new System.Drawing.Size(131, 22);
            this.mnuDataMatrix.Text = "DataMatrix";
            this.mnuDataMatrix.ToolTipText = "DataMatrix";
            this.mnuDataMatrix.Click += new System.EventHandler(this.mnuDataMatrix_Click);
            // 
            // mnuQRCode
            // 
            this.mnuQRCode.Image = global::CustomEditor.Properties.Resources.qr_code_24px;
            this.mnuQRCode.Name = "mnuQRCode";
            this.mnuQRCode.Size = new System.Drawing.Size(131, 22);
            this.mnuQRCode.Text = "QR Code";
            this.mnuQRCode.Click += new System.EventHandler(this.mnuQRCode_Click);
            // 
            // btnHPGL
            // 
            this.btnHPGL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnHPGL.Image = global::CustomEditor.Properties.Resources.hpgl;
            this.btnHPGL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHPGL.Name = "btnHPGL";
            this.btnHPGL.Size = new System.Drawing.Size(23, 22);
            this.btnHPGL.Text = "HPGL";
            this.btnHPGL.ToolTipText = "HPGL (PLT)";
            this.btnHPGL.Click += new System.EventHandler(this.btnHPGL_Click);
            // 
            // btmImage
            // 
            this.btmImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btmImage.Image = global::CustomEditor.Properties.Resources.image_16px;
            this.btmImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btmImage.Name = "btmImage";
            this.btmImage.Size = new System.Drawing.Size(23, 22);
            this.btmImage.Text = "Image";
            this.btmImage.Click += new System.EventHandler(this.btmImage_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ddbPen
            // 
            this.ddbPen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ddbPen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPen,
            this.toolStripMenuItem16,
            this.mnuPenReturn});
            this.ddbPen.Image = global::CustomEditor.Properties.Resources.icons8_pencil_16;
            this.ddbPen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ddbPen.Name = "ddbPen";
            this.ddbPen.Size = new System.Drawing.Size(29, 22);
            this.ddbPen.Text = "Pen entity";
            this.ddbPen.ToolTipText = "Pen";
            // 
            // mnuPen
            // 
            this.mnuPen.Name = "mnuPen";
            this.mnuPen.Size = new System.Drawing.Size(136, 22);
            this.mnuPen.Text = "&Pen";
            this.mnuPen.ToolTipText = "Default Pen";
            this.mnuPen.Click += new System.EventHandler(this.mnuPen_Click);
            // 
            // toolStripMenuItem16
            // 
            this.toolStripMenuItem16.Name = "toolStripMenuItem16";
            this.toolStripMenuItem16.Size = new System.Drawing.Size(133, 6);
            // 
            // mnuPenReturn
            // 
            this.mnuPenReturn.Image = global::CustomEditor.Properties.Resources.penreturn;
            this.mnuPenReturn.Name = "mnuPenReturn";
            this.mnuPenReturn.Size = new System.Drawing.Size(136, 22);
            this.mnuPenReturn.Text = "Pen Return";
            this.mnuPenReturn.ToolTipText = "Pen Return";
            this.mnuPenReturn.Click += new System.EventHandler(this.mnuPenReturn_Click);
            // 
            // btnTimer
            // 
            this.btnTimer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTimer.Image = global::CustomEditor.Properties.Resources.icons8_timer_16;
            this.btnTimer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTimer.Name = "btnTimer";
            this.btnTimer.Size = new System.Drawing.Size(23, 22);
            this.btnTimer.Text = "Timer Entity";
            this.btnTimer.ToolTipText = "Timer";
            this.btnTimer.Click += new System.EventHandler(this.btnTimer_Click);
            // 
            // ddbMotf
            // 
            this.ddbMotf.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ddbMotf.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMOTFBeginEnd,
            this.mnuOTFExtStartDelay,
            this.mnuMOTFWait});
            this.ddbMotf.Image = global::CustomEditor.Properties.Resources.wheel_30px;
            this.ddbMotf.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ddbMotf.Name = "ddbMotf";
            this.ddbMotf.Size = new System.Drawing.Size(29, 22);
            this.ddbMotf.Text = "Motf entity";
            this.ddbMotf.ToolTipText = "MOTF";
            // 
            // mnuMOTFBeginEnd
            // 
            this.mnuMOTFBeginEnd.Image = global::CustomEditor.Properties.Resources.motfbeginend;
            this.mnuMOTFBeginEnd.Name = "mnuMOTFBeginEnd";
            this.mnuMOTFBeginEnd.Size = new System.Drawing.Size(187, 22);
            this.mnuMOTFBeginEnd.Text = "MOTF Begin/End";
            this.mnuMOTFBeginEnd.ToolTipText = "Begin/End";
            this.mnuMOTFBeginEnd.Click += new System.EventHandler(this.mnuMOTFBeginEnd_Click);
            // 
            // mnuOTFExtStartDelay
            // 
            this.mnuOTFExtStartDelay.Image = global::CustomEditor.Properties.Resources.motfdelay;
            this.mnuOTFExtStartDelay.Name = "mnuOTFExtStartDelay";
            this.mnuOTFExtStartDelay.Size = new System.Drawing.Size(187, 22);
            this.mnuOTFExtStartDelay.Text = "MOTF Ext Start Delay";
            this.mnuOTFExtStartDelay.ToolTipText = "External Start Delay";
            this.mnuOTFExtStartDelay.Click += new System.EventHandler(this.mnuOTFExtStartDelay_Click);
            // 
            // mnuMOTFWait
            // 
            this.mnuMOTFWait.Image = global::CustomEditor.Properties.Resources.motfwait;
            this.mnuMOTFWait.Name = "mnuMOTFWait";
            this.mnuMOTFWait.Size = new System.Drawing.Size(187, 22);
            this.mnuMOTFWait.Text = "MOTF Wait";
            this.mnuMOTFWait.ToolTipText = "Wait";
            this.mnuMOTFWait.Click += new System.EventHandler(this.mnuMOTFWait_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnLayer
            // 
            this.btnLayer.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLayer.Image = global::CustomEditor.Properties.Resources.icons8_layers_16;
            this.btnLayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLayer.Name = "btnLayer";
            this.btnLayer.Size = new System.Drawing.Size(23, 22);
            this.btnLayer.ToolTipText = "Add Layer";
            this.btnLayer.Click += new System.EventHandler(this.btnLayer_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuVectorBeginEnd});
            this.toolStripDropDownButton1.Image = global::CustomEditor.Properties.Resources.vector;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.ToolTipText = "ddbAlcVector";
            // 
            // mnuVectorBeginEnd
            // 
            this.mnuVectorBeginEnd.Name = "mnuVectorBeginEnd";
            this.mnuVectorBeginEnd.Size = new System.Drawing.Size(167, 22);
            this.mnuVectorBeginEnd.Text = "Vector Begin/End";
            this.mnuVectorBeginEnd.Click += new System.EventHandler(this.mnuVectorBeginEnd_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 51);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.GLcontrol);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(761, 545);
            this.splitContainer1.SplitterDistance = 535;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 14;
            // 
            // GLcontrol
            // 
            this.GLcontrol.ContextMenuStrip = this.contextMenuStrip1;
            this.GLcontrol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GLcontrol.DrawFPS = false;
            this.GLcontrol.FrameRate = 30;
            this.GLcontrol.Location = new System.Drawing.Point(0, 0);
            this.GLcontrol.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.GLcontrol.Name = "GLcontrol";
            this.GLcontrol.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.GLcontrol.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.GLcontrol.RenderTrigger = SharpGL.RenderTrigger.Manual;
            this.GLcontrol.Size = new System.Drawing.Size(535, 545);
            this.GLcontrol.TabIndex = 3;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.trvEntity);
            this.splitContainer2.Panel1.Controls.Add(this.statusStrip2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ppgEntity);
            this.splitContainer2.Size = new System.Drawing.Size(223, 545);
            this.splitContainer2.SplitterDistance = 172;
            this.splitContainer2.TabIndex = 2;
            // 
            // trvEntity
            // 
            this.trvEntity.AllowDrop = true;
            this.trvEntity.ContextMenuStrip = this.contextMenuStrip2;
            this.trvEntity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvEntity.Location = new System.Drawing.Point(0, 0);
            this.trvEntity.Name = "trvEntity";
            this.trvEntity.SelectedNodes = ((System.Collections.Generic.List<System.Windows.Forms.TreeNode>)(resources.GetObject("trvEntity.SelectedNodes")));
            this.trvEntity.Size = new System.Drawing.Size(223, 148);
            this.trvEntity.TabIndex = 3;
            this.trvEntity.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.trvEntity_ItemDrag);
            this.trvEntity.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvEntity_AfterSelect);
            this.trvEntity.DragDrop += new System.Windows.Forms.DragEventHandler(this.trvEntity_DragDrop);
            this.trvEntity.DragEnter += new System.Windows.Forms.DragEventHandler(this.trvEntity_DragEnter);
            this.trvEntity.DragOver += new System.Windows.Forms.DragEventHandler(this.trvEntity_DragOver);
            // 
            // statusStrip2
            // 
            this.statusStrip2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.lblWH,
            this.toolStripStatusLabel3,
            this.lblCenter,
            this.toolStripStatusLabel1,
            this.lblBound});
            this.statusStrip2.Location = new System.Drawing.Point(0, 148);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(223, 24);
            this.statusStrip2.SizingGrip = false;
            this.statusStrip2.TabIndex = 2;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(37, 19);
            this.toolStripStatusLabel2.Text = "WxH";
            // 
            // lblWH
            // 
            this.lblWH.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.lblWH.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWH.Name = "lblWH";
            this.lblWH.Size = new System.Drawing.Size(24, 19);
            this.lblWH.Text = "0,0";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel3.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(19, 19);
            this.toolStripStatusLabel3.Text = "C";
            // 
            // lblCenter
            // 
            this.lblCenter.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.lblCenter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCenter.Name = "lblCenter";
            this.lblCenter.Size = new System.Drawing.Size(24, 19);
            this.lblCenter.Text = "0,0";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(19, 19);
            this.toolStripStatusLabel1.Text = "B";
            // 
            // lblBound
            // 
            this.lblBound.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.lblBound.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBound.Name = "lblBound";
            this.lblBound.Size = new System.Drawing.Size(44, 19);
            this.lblBound.Text = "0,0 0,0";
            // 
            // ppgEntity
            // 
            this.ppgEntity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ppgEntity.Location = new System.Drawing.Point(0, 0);
            this.ppgEntity.Name = "ppgEntity";
            this.ppgEntity.Size = new System.Drawing.Size(223, 369);
            this.ppgEntity.TabIndex = 1;
            this.ppgEntity.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.ppgEntity_PropertyValueChanged);
            // 
            // CustomEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 618);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CustomEditorForm";
            this.Text = "(c)SpiralLab - Custom Editor";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GLcontrol)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.SaveFileDialog sfd;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnDocumentInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem entityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alignToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem originToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arcToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem circleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rectangleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lWPolylineToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem penToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem groupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ungroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem layerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripButton btnPaste;
        private System.Windows.Forms.ToolStripButton btnCut;
        private System.Windows.Forms.ToolStripButton btnCopy;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spiralToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem timerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnImport;
        private System.Windows.Forms.ToolStripStatusLabel lblName;
        private System.Windows.Forms.ToolStripProgressBar pgbProgress;
        private System.Windows.Forms.ToolStripStatusLabel lblXPos;
        private System.Windows.Forms.ToolStripStatusLabel lblYPos;
        private System.Windows.Forms.ToolStripStatusLabel lblEntityCount;
        private System.Windows.Forms.ToolStripStatusLabel lblRenderTime;
        private System.Windows.Forms.ToolStripStatusLabel lblFileName;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem leftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem topToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bottomToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnUndo;
        private System.Windows.Forms.ToolStripButton btnReDo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripButton btnExplode;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnPoint;
        private System.Windows.Forms.ToolStripButton btnLine;
        private System.Windows.Forms.ToolStripButton btnArc;
        private System.Windows.Forms.ToolStripButton btnCircle;
        private System.Windows.Forms.ToolStripButton btnRectangle;
        private System.Windows.Forms.ToolStripButton btnLWPolyline;
        private System.Windows.Forms.ToolStripButton btnSpiral;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnTimer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnLayer;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public SharpGL.OpenGLControl GLcontrol;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private SpiralLab.Sirius.MultiSelectTreeview trvEntity;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel lblWH;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel lblCenter;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblBound;
        private System.Windows.Forms.PropertyGrid ppgEntity;
        private System.Windows.Forms.ToolStripButton btnZoomOut;
        private System.Windows.Forms.ToolStripButton btnZoomIn;
        private System.Windows.Forms.ToolStripButton btnZoomFit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton btnPan;
        private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem outToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem panToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnRotateCCW;
        private System.Windows.Forms.ToolStripButton btnRotateCW;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripButton btnPasteArray;
        private System.Windows.Forms.ToolStripMenuItem pasteArrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem11;
        private System.Windows.Forms.ToolStripDropDownButton ddbBarcode;
        private System.Windows.Forms.ToolStripMenuItem mnuDataMatrix;
        private System.Windows.Forms.ToolStripButton btnPoints;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem13;
        private System.Windows.Forms.ToolStripButton btnHatch;
        private System.Windows.Forms.ToolStripButton btnTrepan;
        private System.Windows.Forms.ToolStripDropDownButton ddbPen;
        private System.Windows.Forms.ToolStripMenuItem mnuPen;
        private System.Windows.Forms.ToolStripMenuItem siriusTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton ddbMotf;
        private System.Windows.Forms.ToolStripMenuItem mnuMOTFBeginEnd;
        private System.Windows.Forms.ToolStripMenuItem mnuMOTFWait;
        private System.Windows.Forms.ToolStripDropDownButton ddbSiriusText;
        private System.Windows.Forms.ToolStripMenuItem mnuSiriusText;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem mnuSiriusTime;
        private System.Windows.Forms.ToolStripMenuItem mnuSiriusDate;
        private System.Windows.Forms.ToolStripMenuItem mnuSiriusSerial;
        private System.Windows.Forms.ToolStripDropDownButton ddbSort;
        private System.Windows.Forms.ToolStripMenuItem mnuBottomTop;
        private System.Windows.Forms.ToolStripMenuItem mnuTopBottom;
        private System.Windows.Forms.ToolStripMenuItem mnuLeftRight;
        private System.Windows.Forms.ToolStripMenuItem mnuRightLeft;
        private System.Windows.Forms.ToolStripMenuItem sortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bottomTopToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem topBottomToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem leftRightToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem rightLeftToolStripMenuItem1;
        private System.Windows.Forms.ToolStripDropDownButton ddbAlignment;
        private System.Windows.Forms.ToolStripMenuItem mnuOrigin;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem mnuLeft;
        private System.Windows.Forms.ToolStripMenuItem mnuRight;
        private System.Windows.Forms.ToolStripMenuItem mnuTop;
        private System.Windows.Forms.ToolStripMenuItem mnuBottom;
        private System.Windows.Forms.ToolStripMenuItem mnuOTFExtStartDelay;
        private System.Windows.Forms.ToolStripDropDownButton ddbText;
        private System.Windows.Forms.ToolStripMenuItem mnuText;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem12;
        private System.Windows.Forms.ToolStripMenuItem mnuTextTime;
        private System.Windows.Forms.ToolStripMenuItem mnuTextDate;
        private System.Windows.Forms.ToolStripMenuItem mnuTextSerial;
        private System.Windows.Forms.ToolStripDropDownButton ddbSimulate;
        private System.Windows.Forms.ToolStripMenuItem mnuSlow;
        private System.Windows.Forms.ToolStripMenuItem mnuNormal;
        private System.Windows.Forms.ToolStripMenuItem mnuFast;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem15;
        private System.Windows.Forms.ToolStripMenuItem mnuStop;
        private System.Windows.Forms.ToolStripMenuItem mnuSiriusTextArc;
        private System.Windows.Forms.ToolStripMenuItem mnuQRCode;
        private System.Windows.Forms.ToolStripMenuItem mnuTextArc;
        private System.Windows.Forms.ToolStripButton btnBarcode1D;
        private System.Windows.Forms.ToolStripButton btnHPGL;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem16;
        private System.Windows.Forms.ToolStripMenuItem mnuPenReturn;
        private System.Windows.Forms.ToolStripButton btmImage;
        private System.Windows.Forms.ToolStripButton btnRaster;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem mnuVectorBeginEnd;
        private System.Windows.Forms.ToolStripButton btnEllipse;
    }
}
