namespace SpiralLab.Sirius
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
            this.tlsTop = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
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
            this.btnPasteClone = new System.Windows.Forms.ToolStripButton();
            this.btnPasteArray = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUndo = new System.Windows.Forms.ToolStripButton();
            this.btnReDo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.btnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.btnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnZoomFit = new System.Windows.Forms.ToolStripButton();
            this.chbPan = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExplode = new System.Windows.Forms.ToolStripButton();
            this.btnToArc = new System.Windows.Forms.ToolStripButton();
            this.btnHatch = new System.Windows.Forms.ToolStripButton();
            this.btnDivide = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRotateCCW = new System.Windows.Forms.ToolStripButton();
            this.btnRotateCW = new System.Windows.Forms.ToolStripButton();
            this.btnRotateCustom = new System.Windows.Forms.ToolStripButton();
            this.btnTransit = new System.Windows.Forms.ToolStripButton();
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
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.ddbSimulate = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuSlow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNormal = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFast = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuStop = new System.Windows.Forms.ToolStripMenuItem();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.ctmsMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnImport = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.rotateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cWToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cCWToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.translateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.leftToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.forwardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backwardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem18 = new System.Windows.Forms.ToolStripSeparator();
            this.right01MmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.left01MmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.upToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.down01mmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem19 = new System.Windows.Forms.ToolStripSeparator();
            this.right001MmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.left001MmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.up001MmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.down001MmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteCloneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteArrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem17 = new System.Windows.Forms.ToolStripSeparator();
            this.simulatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.slowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripSeparator();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuMarker = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLaser = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuIO = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRtc = new System.Windows.Forms.ToolStripMenuItem();
            this.fieldCorrectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCorrection2D = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCorrection3D = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMeasurementFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPowerMeter = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPowerMap = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMotor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLogWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.lblName = new System.Windows.Forms.ToolStripStatusLabel();
            this.pgbProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.lblXPos = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblYPos = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblEntityCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblRenderTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblFileName = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsBottom = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblWH = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlsTop2 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPoint = new System.Windows.Forms.ToolStripButton();
            this.btnPoints = new System.Windows.Forms.ToolStripButton();
            this.btnRaster = new System.Windows.Forms.ToolStripButton();
            this.btnLine = new System.Windows.Forms.ToolStripButton();
            this.btnArc = new System.Windows.Forms.ToolStripButton();
            this.btnCircle = new System.Windows.Forms.ToolStripButton();
            this.btnEllipse = new System.Windows.Forms.ToolStripButton();
            this.btnTrepan = new System.Windows.Forms.ToolStripButton();
            this.btnTriangle = new System.Windows.Forms.ToolStripButton();
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
            this.btnImage = new System.Windows.Forms.ToolStripButton();
            this.ddbStitchedImage = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuStitchedImage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLoadCells = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuClearCells = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuTimer = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMeasurementBeginEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.zOffsetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuZOffset = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuZDefocus = new System.Windows.Forms.ToolStripMenuItem();
            this.writeDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.writeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ext16ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ext16IfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.waitExt16IfToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMOTF = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMOTFBeginEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMOTFExtStartDelay = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMOTFWait = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMOTFRepeat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuMOTFAngularBeginEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMotfAngularWait = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAlc = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPoD = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVectorBeginEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem20 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAlcSyncAxisBeginEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuJump = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFiducial = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCalculationDynamics = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.chbLock = new System.Windows.Forms.ToolStripButton();
            this.btnLayer = new System.Windows.Forms.ToolStripButton();
            this.ddbGroup = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGroupOffset = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUnGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPens = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.GLcontrol = new SharpGL.OpenGLControl();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.trvEntity = new SpiralLab.Sirius.MultiSelectTreeview();
            this.stsDimension = new System.Windows.Forms.StatusStrip();
            this.lblBound = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCenter = new System.Windows.Forms.ToolStripStatusLabel();
            this.ppgEntity = new System.Windows.Forms.PropertyGrid();
            this.tlsTop.SuspendLayout();
            this.ctmsMain.SuspendLayout();
            this.stsBottom.SuspendLayout();
            this.tlsTop2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GLcontrol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.stsDimension.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlsTop
            // 
            this.tlsTop.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsTop.GripMargin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.tlsTop.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlsTop.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tlsTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator11,
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
            this.btnPasteClone,
            this.btnPasteArray,
            this.toolStripSeparator7,
            this.btnUndo,
            this.btnReDo,
            this.toolStripSeparator8,
            this.btnZoomOut,
            this.btnZoomIn,
            this.btnZoomFit,
            this.chbPan,
            this.toolStripSeparator9,
            this.btnExplode,
            this.btnToArc,
            this.btnHatch,
            this.btnDivide,
            this.btnDelete,
            this.toolStripSeparator4,
            this.btnRotateCCW,
            this.btnRotateCW,
            this.btnRotateCustom,
            this.btnTransit,
            this.toolStripSeparator10,
            this.ddbAlignment,
            this.ddbSort,
            this.toolStripSeparator13,
            this.ddbSimulate});
            this.tlsTop.Location = new System.Drawing.Point(0, 0);
            this.tlsTop.Name = "tlsTop";
            this.tlsTop.Size = new System.Drawing.Size(1075, 33);
            this.tlsTop.TabIndex = 10;
            this.tlsTop.Text = "toolStrip1";
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 33);
            // 
            // btnDocumentInfo
            // 
            this.btnDocumentInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDocumentInfo.Image = ((System.Drawing.Image)(resources.GetObject("btnDocumentInfo.Image")));
            this.btnDocumentInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDocumentInfo.Name = "btnDocumentInfo";
            this.btnDocumentInfo.Size = new System.Drawing.Size(34, 28);
            this.btnDocumentInfo.Text = "File Information";
            this.btnDocumentInfo.ToolTipText = "File Information";
            this.btnDocumentInfo.Click += new System.EventHandler(this.btnDocumentInfo_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 33);
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(34, 28);
            this.btnNew.Text = "New";
            this.btnNew.ToolTipText = "New";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(34, 28);
            this.btnOpen.Text = "Open";
            this.btnOpen.ToolTipText = "Open";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(34, 28);
            this.btnSave.Text = "Save";
            this.btnSave.ToolTipText = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveAs.Image")));
            this.btnSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(34, 28);
            this.btnSaveAs.Text = "Save As";
            this.btnSaveAs.ToolTipText = "Save As ...";
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 33);
            // 
            // btnCopy
            // 
            this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(34, 28);
            this.btnCopy.Text = "Copy";
            this.btnCopy.ToolTipText = "Copy";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnCut
            // 
            this.btnCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCut.Image = ((System.Drawing.Image)(resources.GetObject("btnCut.Image")));
            this.btnCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(34, 28);
            this.btnCut.Text = "Cut";
            this.btnCut.ToolTipText = "Cut";
            this.btnCut.Click += new System.EventHandler(this.btnCut_Click);
            // 
            // btnPaste
            // 
            this.btnPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPaste.Image = ((System.Drawing.Image)(resources.GetObject("btnPaste.Image")));
            this.btnPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(34, 28);
            this.btnPaste.Text = "Paste";
            this.btnPaste.ToolTipText = "Paste";
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // btnPasteClone
            // 
            this.btnPasteClone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPasteClone.Image = ((System.Drawing.Image)(resources.GetObject("btnPasteClone.Image")));
            this.btnPasteClone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPasteClone.Name = "btnPasteClone";
            this.btnPasteClone.Size = new System.Drawing.Size(34, 28);
            this.btnPasteClone.Text = "Paste Clone";
            this.btnPasteClone.ToolTipText = "Paste Clone";
            this.btnPasteClone.Click += new System.EventHandler(this.btnPasteClone_Click);
            // 
            // btnPasteArray
            // 
            this.btnPasteArray.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPasteArray.Image = ((System.Drawing.Image)(resources.GetObject("btnPasteArray.Image")));
            this.btnPasteArray.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPasteArray.Name = "btnPasteArray";
            this.btnPasteArray.Size = new System.Drawing.Size(34, 28);
            this.btnPasteArray.Text = "Paste Array";
            this.btnPasteArray.ToolTipText = "Paste Array";
            this.btnPasteArray.Click += new System.EventHandler(this.btnPasteArray_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 33);
            // 
            // btnUndo
            // 
            this.btnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUndo.Image = ((System.Drawing.Image)(resources.GetObject("btnUndo.Image")));
            this.btnUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(34, 28);
            this.btnUndo.Text = "Undo";
            this.btnUndo.ToolTipText = "UnDo";
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnReDo
            // 
            this.btnReDo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnReDo.Image = ((System.Drawing.Image)(resources.GetObject("btnReDo.Image")));
            this.btnReDo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReDo.Name = "btnReDo";
            this.btnReDo.Size = new System.Drawing.Size(34, 28);
            this.btnReDo.Text = "Redo";
            this.btnReDo.ToolTipText = "ReDo";
            this.btnReDo.Click += new System.EventHandler(this.btnReDo_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 33);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomOut.Image")));
            this.btnZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(34, 28);
            this.btnZoomOut.Text = "Zoom Out";
            this.btnZoomOut.ToolTipText = "Zoom Out";
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomIn.Image")));
            this.btnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(34, 28);
            this.btnZoomIn.Text = "Zoom In";
            this.btnZoomIn.ToolTipText = "Zoom In";
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // btnZoomFit
            // 
            this.btnZoomFit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomFit.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomFit.Image")));
            this.btnZoomFit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomFit.Name = "btnZoomFit";
            this.btnZoomFit.Size = new System.Drawing.Size(34, 28);
            this.btnZoomFit.Text = "Zoom Fit";
            this.btnZoomFit.ToolTipText = "Zoom Fit";
            this.btnZoomFit.Click += new System.EventHandler(this.btnZoomFit_Click);
            // 
            // chbPan
            // 
            this.chbPan.CheckOnClick = true;
            this.chbPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.chbPan.Image = ((System.Drawing.Image)(resources.GetObject("chbPan.Image")));
            this.chbPan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.chbPan.Name = "chbPan";
            this.chbPan.Size = new System.Drawing.Size(34, 28);
            this.chbPan.Text = "Pan";
            this.chbPan.ToolTipText = "Pan";
            this.chbPan.Click += new System.EventHandler(this.btnPan_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 33);
            // 
            // btnExplode
            // 
            this.btnExplode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExplode.Image = ((System.Drawing.Image)(resources.GetObject("btnExplode.Image")));
            this.btnExplode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExplode.Name = "btnExplode";
            this.btnExplode.Size = new System.Drawing.Size(34, 28);
            this.btnExplode.Text = "Explode";
            this.btnExplode.ToolTipText = "Explode";
            this.btnExplode.Click += new System.EventHandler(this.btnExplode_Click);
            // 
            // btnToArc
            // 
            this.btnToArc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnToArc.Image = ((System.Drawing.Image)(resources.GetObject("btnToArc.Image")));
            this.btnToArc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnToArc.Name = "btnToArc";
            this.btnToArc.Size = new System.Drawing.Size(34, 28);
            this.btnToArc.Text = "toolStripButton1";
            this.btnToArc.ToolTipText = "Polyline To Arc";
            this.btnToArc.Click += new System.EventHandler(this.btnToArc_Click);
            // 
            // btnHatch
            // 
            this.btnHatch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnHatch.Image = ((System.Drawing.Image)(resources.GetObject("btnHatch.Image")));
            this.btnHatch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHatch.Name = "btnHatch";
            this.btnHatch.Size = new System.Drawing.Size(34, 28);
            this.btnHatch.Text = "Hatch";
            this.btnHatch.ToolTipText = "Hatch";
            this.btnHatch.Click += new System.EventHandler(this.btnHatch_Click);
            // 
            // btnDivide
            // 
            this.btnDivide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDivide.Image = ((System.Drawing.Image)(resources.GetObject("btnDivide.Image")));
            this.btnDivide.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDivide.Name = "btnDivide";
            this.btnDivide.Size = new System.Drawing.Size(34, 28);
            this.btnDivide.Text = "Divide";
            this.btnDivide.ToolTipText = "Divide";
            this.btnDivide.Click += new System.EventHandler(this.btnDivide_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(34, 28);
            this.btnDelete.Text = "Delete";
            this.btnDelete.ToolTipText = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 33);
            // 
            // btnRotateCCW
            // 
            this.btnRotateCCW.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRotateCCW.Image = ((System.Drawing.Image)(resources.GetObject("btnRotateCCW.Image")));
            this.btnRotateCCW.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRotateCCW.Name = "btnRotateCCW";
            this.btnRotateCCW.Size = new System.Drawing.Size(34, 28);
            this.btnRotateCCW.Text = "Rotate CCW";
            this.btnRotateCCW.ToolTipText = "Rotate CCW";
            this.btnRotateCCW.Click += new System.EventHandler(this.btnRotateCCW_Click);
            // 
            // btnRotateCW
            // 
            this.btnRotateCW.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRotateCW.Image = ((System.Drawing.Image)(resources.GetObject("btnRotateCW.Image")));
            this.btnRotateCW.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRotateCW.Name = "btnRotateCW";
            this.btnRotateCW.Size = new System.Drawing.Size(34, 28);
            this.btnRotateCW.Text = "Rotate CW";
            this.btnRotateCW.ToolTipText = "Rotate CW";
            this.btnRotateCW.Click += new System.EventHandler(this.btnRotateCW_Click);
            // 
            // btnRotateCustom
            // 
            this.btnRotateCustom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRotateCustom.Image = ((System.Drawing.Image)(resources.GetObject("btnRotateCustom.Image")));
            this.btnRotateCustom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRotateCustom.Name = "btnRotateCustom";
            this.btnRotateCustom.Size = new System.Drawing.Size(34, 28);
            this.btnRotateCustom.Text = "Rotate Custom";
            this.btnRotateCustom.Click += new System.EventHandler(this.btnRotateCustom_Click);
            // 
            // btnTransit
            // 
            this.btnTransit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTransit.Image = ((System.Drawing.Image)(resources.GetObject("btnTransit.Image")));
            this.btnTransit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTransit.Name = "btnTransit";
            this.btnTransit.Size = new System.Drawing.Size(34, 28);
            this.btnTransit.Text = "Transit";
            this.btnTransit.Click += new System.EventHandler(this.btnTransit_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 33);
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
            this.ddbAlignment.Image = ((System.Drawing.Image)(resources.GetObject("ddbAlignment.Image")));
            this.ddbAlignment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ddbAlignment.Name = "ddbAlignment";
            this.ddbAlignment.Size = new System.Drawing.Size(42, 28);
            this.ddbAlignment.Text = "Alignment";
            this.ddbAlignment.ToolTipText = "Alignment";
            // 
            // mnuOrigin
            // 
            this.mnuOrigin.Image = ((System.Drawing.Image)(resources.GetObject("mnuOrigin.Image")));
            this.mnuOrigin.Name = "mnuOrigin";
            this.mnuOrigin.Size = new System.Drawing.Size(173, 34);
            this.mnuOrigin.Text = "Origin";
            this.mnuOrigin.ToolTipText = "Origin Location";
            this.mnuOrigin.Click += new System.EventHandler(this.mnuOrigin_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(170, 6);
            // 
            // mnuLeft
            // 
            this.mnuLeft.Image = ((System.Drawing.Image)(resources.GetObject("mnuLeft.Image")));
            this.mnuLeft.Name = "mnuLeft";
            this.mnuLeft.Size = new System.Drawing.Size(173, 34);
            this.mnuLeft.Text = "Left";
            this.mnuLeft.ToolTipText = "To Left";
            this.mnuLeft.Click += new System.EventHandler(this.mnuLeft_Click);
            // 
            // mnuRight
            // 
            this.mnuRight.Image = ((System.Drawing.Image)(resources.GetObject("mnuRight.Image")));
            this.mnuRight.Name = "mnuRight";
            this.mnuRight.Size = new System.Drawing.Size(173, 34);
            this.mnuRight.Text = "Right";
            this.mnuRight.ToolTipText = "To Right";
            this.mnuRight.Click += new System.EventHandler(this.mnuRight_Click);
            // 
            // mnuTop
            // 
            this.mnuTop.Image = ((System.Drawing.Image)(resources.GetObject("mnuTop.Image")));
            this.mnuTop.Name = "mnuTop";
            this.mnuTop.Size = new System.Drawing.Size(173, 34);
            this.mnuTop.Text = "Top";
            this.mnuTop.ToolTipText = "To Top";
            this.mnuTop.Click += new System.EventHandler(this.mnuTop_Click);
            // 
            // mnuBottom
            // 
            this.mnuBottom.Image = ((System.Drawing.Image)(resources.GetObject("mnuBottom.Image")));
            this.mnuBottom.Name = "mnuBottom";
            this.mnuBottom.Size = new System.Drawing.Size(173, 34);
            this.mnuBottom.Text = "Bottom";
            this.mnuBottom.ToolTipText = "To Bottom";
            this.mnuBottom.Click += new System.EventHandler(this.mnuBottom_Click);
            // 
            // ddbSort
            // 
            this.ddbSort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ddbSort.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBottomTop,
            this.mnuTopBottom,
            this.mnuLeftRight,
            this.mnuRightLeft});
            this.ddbSort.Image = ((System.Drawing.Image)(resources.GetObject("ddbSort.Image")));
            this.ddbSort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ddbSort.Name = "ddbSort";
            this.ddbSort.Size = new System.Drawing.Size(42, 28);
            this.ddbSort.Text = "Sort";
            this.ddbSort.ToolTipText = "Sort";
            // 
            // mnuBottomTop
            // 
            this.mnuBottomTop.Image = ((System.Drawing.Image)(resources.GetObject("mnuBottomTop.Image")));
            this.mnuBottomTop.Name = "mnuBottomTop";
            this.mnuBottomTop.Size = new System.Drawing.Size(236, 34);
            this.mnuBottomTop.Text = "Bottom -> Top";
            this.mnuBottomTop.ToolTipText = "Bottom To Top";
            this.mnuBottomTop.Click += new System.EventHandler(this.mnuBottomTop_Click);
            // 
            // mnuTopBottom
            // 
            this.mnuTopBottom.Image = ((System.Drawing.Image)(resources.GetObject("mnuTopBottom.Image")));
            this.mnuTopBottom.Name = "mnuTopBottom";
            this.mnuTopBottom.Size = new System.Drawing.Size(236, 34);
            this.mnuTopBottom.Text = "Top -> Bottom";
            this.mnuTopBottom.ToolTipText = "Top To Bottom";
            this.mnuTopBottom.Click += new System.EventHandler(this.mnuTopBottom_Click);
            // 
            // mnuLeftRight
            // 
            this.mnuLeftRight.Image = ((System.Drawing.Image)(resources.GetObject("mnuLeftRight.Image")));
            this.mnuLeftRight.Name = "mnuLeftRight";
            this.mnuLeftRight.Size = new System.Drawing.Size(236, 34);
            this.mnuLeftRight.Text = "Left -> Right";
            this.mnuLeftRight.ToolTipText = "Left To Right";
            this.mnuLeftRight.Click += new System.EventHandler(this.mnuLeftRight_Click);
            // 
            // mnuRightLeft
            // 
            this.mnuRightLeft.Image = ((System.Drawing.Image)(resources.GetObject("mnuRightLeft.Image")));
            this.mnuRightLeft.Name = "mnuRightLeft";
            this.mnuRightLeft.Size = new System.Drawing.Size(236, 34);
            this.mnuRightLeft.Text = "Right -> Left";
            this.mnuRightLeft.ToolTipText = "Right To Left";
            this.mnuRightLeft.Click += new System.EventHandler(this.mnuRightLeft_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 33);
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
            this.ddbSimulate.Image = ((System.Drawing.Image)(resources.GetObject("ddbSimulate.Image")));
            this.ddbSimulate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ddbSimulate.Name = "ddbSimulate";
            this.ddbSimulate.Size = new System.Drawing.Size(42, 28);
            this.ddbSimulate.Text = "Laser Path Simulator";
            // 
            // mnuSlow
            // 
            this.mnuSlow.Image = ((System.Drawing.Image)(resources.GetObject("mnuSlow.Image")));
            this.mnuSlow.Name = "mnuSlow";
            this.mnuSlow.Size = new System.Drawing.Size(172, 34);
            this.mnuSlow.Text = "Slow";
            this.mnuSlow.ToolTipText = "Slow";
            this.mnuSlow.Click += new System.EventHandler(this.mnuSimulateSlow_Click);
            // 
            // mnuNormal
            // 
            this.mnuNormal.Image = ((System.Drawing.Image)(resources.GetObject("mnuNormal.Image")));
            this.mnuNormal.Name = "mnuNormal";
            this.mnuNormal.Size = new System.Drawing.Size(172, 34);
            this.mnuNormal.Text = "Normal";
            this.mnuNormal.ToolTipText = "Normal";
            this.mnuNormal.Click += new System.EventHandler(this.mnuSimulateNormal_Click);
            // 
            // mnuFast
            // 
            this.mnuFast.Image = ((System.Drawing.Image)(resources.GetObject("mnuFast.Image")));
            this.mnuFast.Name = "mnuFast";
            this.mnuFast.Size = new System.Drawing.Size(172, 34);
            this.mnuFast.Text = "Fast";
            this.mnuFast.ToolTipText = "Fast";
            this.mnuFast.Click += new System.EventHandler(this.mnuSimulateFast_Click);
            // 
            // toolStripMenuItem15
            // 
            this.toolStripMenuItem15.Name = "toolStripMenuItem15";
            this.toolStripMenuItem15.Size = new System.Drawing.Size(169, 6);
            // 
            // mnuStop
            // 
            this.mnuStop.Image = ((System.Drawing.Image)(resources.GetObject("mnuStop.Image")));
            this.mnuStop.Name = "mnuStop";
            this.mnuStop.Size = new System.Drawing.Size(172, 34);
            this.mnuStop.Text = "Stop";
            this.mnuStop.ToolTipText = "Stop";
            this.mnuStop.Click += new System.EventHandler(this.mnuSimulateStop_Click);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 33);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1075, 1);
            this.splitter1.TabIndex = 11;
            this.splitter1.TabStop = false;
            // 
            // ctmsMain
            // 
            this.ctmsMain.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctmsMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.ctmsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.zoomToolStripMenuItem,
            this.editToolStripMenuItem,
            this.simulatorToolStripMenuItem,
            this.toolStripMenuItem3,
            this.mnuMarker,
            this.mnuLaser,
            this.mnuIO,
            this.mnuRtc,
            this.fieldCorrectionToolStripMenuItem,
            this.mnuMeasurementFile,
            this.mnuPowerMeter,
            this.mnuPowerMap,
            this.mnuMotor,
            this.mnuLogWindow,
            this.toolStripMenuItem14,
            this.mnuAbout});
            this.ctmsMain.Name = "contextMenuStrip1";
            this.ctmsMain.Size = new System.Drawing.Size(246, 496);
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
            this.fileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("fileToolStripMenuItem.Image")));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(245, 32);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(323, 34);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(323, 34);
            this.openToolStripMenuItem.Text = "&Open ...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnImport
            // 
            this.btnImport.Image = ((System.Drawing.Image)(resources.GetObject("btnImport.Image")));
            this.btnImport.Name = "btnImport";
            this.btnImport.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.I)));
            this.btnImport.Size = new System.Drawing.Size(323, 34);
            this.btnImport.Text = "Import ...";
            this.btnImport.ToolTipText = "Import";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(323, 34);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveAsToolStripMenuItem.Image")));
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(323, 34);
            this.saveAsToolStripMenuItem.Text = "Save &As ...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(320, 6);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("infoToolStripMenuItem.Image")));
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(323, 34);
            this.infoToolStripMenuItem.Text = "File &Info";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.btnDocumentInfo_Click);
            // 
            // zoomToolStripMenuItem
            // 
            this.zoomToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iNToolStripMenuItem,
            this.outToolStripMenuItem,
            this.fitToolStripMenuItem,
            this.toolStripMenuItem9,
            this.panToolStripMenuItem});
            this.zoomToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("zoomToolStripMenuItem.Image")));
            this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
            this.zoomToolStripMenuItem.Size = new System.Drawing.Size(245, 32);
            this.zoomToolStripMenuItem.Text = "&Zoom";
            // 
            // iNToolStripMenuItem
            // 
            this.iNToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("iNToolStripMenuItem.Image")));
            this.iNToolStripMenuItem.Name = "iNToolStripMenuItem";
            this.iNToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Oemplus)));
            this.iNToolStripMenuItem.Size = new System.Drawing.Size(289, 34);
            this.iNToolStripMenuItem.Text = "In";
            this.iNToolStripMenuItem.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // outToolStripMenuItem
            // 
            this.outToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("outToolStripMenuItem.Image")));
            this.outToolStripMenuItem.Name = "outToolStripMenuItem";
            this.outToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.OemMinus)));
            this.outToolStripMenuItem.Size = new System.Drawing.Size(289, 34);
            this.outToolStripMenuItem.Text = "Out";
            this.outToolStripMenuItem.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // fitToolStripMenuItem
            // 
            this.fitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("fitToolStripMenuItem.Image")));
            this.fitToolStripMenuItem.Name = "fitToolStripMenuItem";
            this.fitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.fitToolStripMenuItem.Size = new System.Drawing.Size(289, 34);
            this.fitToolStripMenuItem.Text = "Fit";
            this.fitToolStripMenuItem.Click += new System.EventHandler(this.btnZoomFit_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(286, 6);
            // 
            // panToolStripMenuItem
            // 
            this.panToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("panToolStripMenuItem.Image")));
            this.panToolStripMenuItem.Name = "panToolStripMenuItem";
            this.panToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.panToolStripMenuItem.Size = new System.Drawing.Size(289, 34);
            this.panToolStripMenuItem.Text = "Pan";
            this.panToolStripMenuItem.Click += new System.EventHandler(this.btnPan_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripMenuItem8,
            this.rotateToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.alignToolStripMenuItem,
            this.sortToolStripMenuItem,
            this.translateToolStripMenuItem,
            this.toolStripMenuItem5,
            this.copyToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.pasteCloneToolStripMenuItem,
            this.pasteArrayToolStripMenuItem,
            this.toolStripMenuItem17});
            this.editToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("editToolStripMenuItem.Image")));
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(245, 32);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("undoToolStripMenuItem.Image")));
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(366, 34);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("redoToolStripMenuItem.Image")));
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(366, 34);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.btnReDo_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(363, 6);
            // 
            // rotateToolStripMenuItem
            // 
            this.rotateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cWToolStripMenuItem,
            this.cCWToolStripMenuItem,
            this.customToolStripMenuItem});
            this.rotateToolStripMenuItem.Name = "rotateToolStripMenuItem";
            this.rotateToolStripMenuItem.Size = new System.Drawing.Size(366, 34);
            this.rotateToolStripMenuItem.Text = "Rotate";
            // 
            // cWToolStripMenuItem
            // 
            this.cWToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cWToolStripMenuItem.Image")));
            this.cWToolStripMenuItem.Name = "cWToolStripMenuItem";
            this.cWToolStripMenuItem.Size = new System.Drawing.Size(177, 34);
            this.cWToolStripMenuItem.Text = "CW";
            this.cWToolStripMenuItem.Click += new System.EventHandler(this.btnRotateCW_Click);
            // 
            // cCWToolStripMenuItem
            // 
            this.cCWToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cCWToolStripMenuItem.Image")));
            this.cCWToolStripMenuItem.Name = "cCWToolStripMenuItem";
            this.cCWToolStripMenuItem.Size = new System.Drawing.Size(177, 34);
            this.cCWToolStripMenuItem.Text = "CCW";
            this.cCWToolStripMenuItem.Click += new System.EventHandler(this.btnRotateCCW_Click);
            // 
            // customToolStripMenuItem
            // 
            this.customToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("customToolStripMenuItem.Image")));
            this.customToolStripMenuItem.Name = "customToolStripMenuItem";
            this.customToolStripMenuItem.Size = new System.Drawing.Size(177, 34);
            this.customToolStripMenuItem.Text = "Custom";
            this.customToolStripMenuItem.Click += new System.EventHandler(this.btnRotateCustom_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripMenuItem.Image")));
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)));
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(366, 34);
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
            this.alignToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("alignToolStripMenuItem.Image")));
            this.alignToolStripMenuItem.Name = "alignToolStripMenuItem";
            this.alignToolStripMenuItem.Size = new System.Drawing.Size(366, 34);
            this.alignToolStripMenuItem.Text = "Align";
            // 
            // originToolStripMenuItem
            // 
            this.originToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("originToolStripMenuItem.Image")));
            this.originToolStripMenuItem.Name = "originToolStripMenuItem";
            this.originToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.originToolStripMenuItem.Size = new System.Drawing.Size(228, 34);
            this.originToolStripMenuItem.Text = "Origin";
            this.originToolStripMenuItem.Click += new System.EventHandler(this.mnuOrigin_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(225, 6);
            // 
            // leftToolStripMenuItem
            // 
            this.leftToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("leftToolStripMenuItem.Image")));
            this.leftToolStripMenuItem.Name = "leftToolStripMenuItem";
            this.leftToolStripMenuItem.Size = new System.Drawing.Size(228, 34);
            this.leftToolStripMenuItem.Text = "Left";
            this.leftToolStripMenuItem.Click += new System.EventHandler(this.mnuLeft_Click);
            // 
            // rightToolStripMenuItem
            // 
            this.rightToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("rightToolStripMenuItem.Image")));
            this.rightToolStripMenuItem.Name = "rightToolStripMenuItem";
            this.rightToolStripMenuItem.Size = new System.Drawing.Size(228, 34);
            this.rightToolStripMenuItem.Text = "Right";
            this.rightToolStripMenuItem.Click += new System.EventHandler(this.mnuRight_Click);
            // 
            // topToolStripMenuItem
            // 
            this.topToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("topToolStripMenuItem.Image")));
            this.topToolStripMenuItem.Name = "topToolStripMenuItem";
            this.topToolStripMenuItem.Size = new System.Drawing.Size(228, 34);
            this.topToolStripMenuItem.Text = "Top";
            this.topToolStripMenuItem.Click += new System.EventHandler(this.mnuTop_Click);
            // 
            // bottomToolStripMenuItem
            // 
            this.bottomToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("bottomToolStripMenuItem.Image")));
            this.bottomToolStripMenuItem.Name = "bottomToolStripMenuItem";
            this.bottomToolStripMenuItem.Size = new System.Drawing.Size(228, 34);
            this.bottomToolStripMenuItem.Text = "Bottom";
            this.bottomToolStripMenuItem.Click += new System.EventHandler(this.mnuBottom_Click);
            // 
            // sortToolStripMenuItem
            // 
            this.sortToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bottomTopToolStripMenuItem1,
            this.topBottomToolStripMenuItem1,
            this.leftRightToolStripMenuItem1,
            this.rightLeftToolStripMenuItem1});
            this.sortToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sortToolStripMenuItem.Image")));
            this.sortToolStripMenuItem.Name = "sortToolStripMenuItem";
            this.sortToolStripMenuItem.Size = new System.Drawing.Size(366, 34);
            this.sortToolStripMenuItem.Text = "Sort";
            // 
            // bottomTopToolStripMenuItem1
            // 
            this.bottomTopToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("bottomTopToolStripMenuItem1.Image")));
            this.bottomTopToolStripMenuItem1.Name = "bottomTopToolStripMenuItem1";
            this.bottomTopToolStripMenuItem1.Size = new System.Drawing.Size(236, 34);
            this.bottomTopToolStripMenuItem1.Text = "Bottom -> Top";
            this.bottomTopToolStripMenuItem1.Click += new System.EventHandler(this.mnuBottomTop_Click);
            // 
            // topBottomToolStripMenuItem1
            // 
            this.topBottomToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("topBottomToolStripMenuItem1.Image")));
            this.topBottomToolStripMenuItem1.Name = "topBottomToolStripMenuItem1";
            this.topBottomToolStripMenuItem1.Size = new System.Drawing.Size(236, 34);
            this.topBottomToolStripMenuItem1.Text = "Top -> Bottom";
            this.topBottomToolStripMenuItem1.Click += new System.EventHandler(this.mnuTopBottom_Click);
            // 
            // leftRightToolStripMenuItem1
            // 
            this.leftRightToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("leftRightToolStripMenuItem1.Image")));
            this.leftRightToolStripMenuItem1.Name = "leftRightToolStripMenuItem1";
            this.leftRightToolStripMenuItem1.Size = new System.Drawing.Size(236, 34);
            this.leftRightToolStripMenuItem1.Text = "Left -> Right";
            this.leftRightToolStripMenuItem1.Click += new System.EventHandler(this.mnuLeftRight_Click);
            // 
            // rightLeftToolStripMenuItem1
            // 
            this.rightLeftToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("rightLeftToolStripMenuItem1.Image")));
            this.rightLeftToolStripMenuItem1.Name = "rightLeftToolStripMenuItem1";
            this.rightLeftToolStripMenuItem1.Size = new System.Drawing.Size(236, 34);
            this.rightLeftToolStripMenuItem1.Text = "Right -> Left";
            this.rightLeftToolStripMenuItem1.Click += new System.EventHandler(this.mnuRightLeft_Click);
            // 
            // translateToolStripMenuItem
            // 
            this.translateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rightToolStripMenuItem1,
            this.leftToolStripMenuItem1,
            this.forwardToolStripMenuItem,
            this.backwardToolStripMenuItem,
            this.toolStripMenuItem18,
            this.right01MmToolStripMenuItem,
            this.left01MmToolStripMenuItem,
            this.upToolStripMenuItem,
            this.down01mmToolStripMenuItem,
            this.toolStripMenuItem19,
            this.right001MmToolStripMenuItem,
            this.left001MmToolStripMenuItem,
            this.up001MmToolStripMenuItem,
            this.down001MmToolStripMenuItem});
            this.translateToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("translateToolStripMenuItem.Image")));
            this.translateToolStripMenuItem.Name = "translateToolStripMenuItem";
            this.translateToolStripMenuItem.Size = new System.Drawing.Size(366, 34);
            this.translateToolStripMenuItem.Text = "Jog";
            // 
            // rightToolStripMenuItem1
            // 
            this.rightToolStripMenuItem1.Name = "rightToolStripMenuItem1";
            this.rightToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)));
            this.rightToolStripMenuItem1.Size = new System.Drawing.Size(395, 34);
            this.rightToolStripMenuItem1.Tag = "1,0";
            this.rightToolStripMenuItem1.Text = "Right (1 mm)";
            this.rightToolStripMenuItem1.Click += new System.EventHandler(this.mnuJog_Click);
            // 
            // leftToolStripMenuItem1
            // 
            this.leftToolStripMenuItem1.Name = "leftToolStripMenuItem1";
            this.leftToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)));
            this.leftToolStripMenuItem1.Size = new System.Drawing.Size(395, 34);
            this.leftToolStripMenuItem1.Tag = "-1,0";
            this.leftToolStripMenuItem1.Text = "Left (1 mm)";
            this.leftToolStripMenuItem1.Click += new System.EventHandler(this.mnuJog_Click);
            // 
            // forwardToolStripMenuItem
            // 
            this.forwardToolStripMenuItem.Name = "forwardToolStripMenuItem";
            this.forwardToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.forwardToolStripMenuItem.Size = new System.Drawing.Size(395, 34);
            this.forwardToolStripMenuItem.Tag = "0,1";
            this.forwardToolStripMenuItem.Text = "Up (1 mm)";
            this.forwardToolStripMenuItem.Click += new System.EventHandler(this.mnuJog_Click);
            // 
            // backwardToolStripMenuItem
            // 
            this.backwardToolStripMenuItem.Name = "backwardToolStripMenuItem";
            this.backwardToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.backwardToolStripMenuItem.Size = new System.Drawing.Size(395, 34);
            this.backwardToolStripMenuItem.Tag = "0,-1";
            this.backwardToolStripMenuItem.Text = "Down(1 mm)";
            this.backwardToolStripMenuItem.Click += new System.EventHandler(this.mnuJog_Click);
            // 
            // toolStripMenuItem18
            // 
            this.toolStripMenuItem18.Name = "toolStripMenuItem18";
            this.toolStripMenuItem18.Size = new System.Drawing.Size(392, 6);
            // 
            // right01MmToolStripMenuItem
            // 
            this.right01MmToolStripMenuItem.Name = "right01MmToolStripMenuItem";
            this.right01MmToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Right)));
            this.right01MmToolStripMenuItem.Size = new System.Drawing.Size(395, 34);
            this.right01MmToolStripMenuItem.Tag = "0.1,0";
            this.right01MmToolStripMenuItem.Text = "Right (0.1 mm)";
            this.right01MmToolStripMenuItem.Click += new System.EventHandler(this.mnuJog_Click);
            // 
            // left01MmToolStripMenuItem
            // 
            this.left01MmToolStripMenuItem.Name = "left01MmToolStripMenuItem";
            this.left01MmToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Left)));
            this.left01MmToolStripMenuItem.Size = new System.Drawing.Size(395, 34);
            this.left01MmToolStripMenuItem.Tag = "-0.1,0";
            this.left01MmToolStripMenuItem.Text = "Left (0.1 mm)";
            this.left01MmToolStripMenuItem.Click += new System.EventHandler(this.mnuJog_Click);
            // 
            // upToolStripMenuItem
            // 
            this.upToolStripMenuItem.Name = "upToolStripMenuItem";
            this.upToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Up)));
            this.upToolStripMenuItem.Size = new System.Drawing.Size(395, 34);
            this.upToolStripMenuItem.Tag = "0,0.1";
            this.upToolStripMenuItem.Text = "Up (0.1 mm)";
            this.upToolStripMenuItem.Click += new System.EventHandler(this.mnuJog_Click);
            // 
            // down01mmToolStripMenuItem
            // 
            this.down01mmToolStripMenuItem.Name = "down01mmToolStripMenuItem";
            this.down01mmToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)));
            this.down01mmToolStripMenuItem.Size = new System.Drawing.Size(395, 34);
            this.down01mmToolStripMenuItem.Tag = "0,-0.1";
            this.down01mmToolStripMenuItem.Text = "Down (0,1mm)";
            this.down01mmToolStripMenuItem.Click += new System.EventHandler(this.mnuJog_Click);
            // 
            // toolStripMenuItem19
            // 
            this.toolStripMenuItem19.Name = "toolStripMenuItem19";
            this.toolStripMenuItem19.Size = new System.Drawing.Size(392, 6);
            // 
            // right001MmToolStripMenuItem
            // 
            this.right001MmToolStripMenuItem.Name = "right001MmToolStripMenuItem";
            this.right001MmToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Right)));
            this.right001MmToolStripMenuItem.Size = new System.Drawing.Size(395, 34);
            this.right001MmToolStripMenuItem.Tag = "0.01,0";
            this.right001MmToolStripMenuItem.Text = "Right (0.01 mm)";
            this.right001MmToolStripMenuItem.Click += new System.EventHandler(this.mnuJog_Click);
            // 
            // left001MmToolStripMenuItem
            // 
            this.left001MmToolStripMenuItem.Name = "left001MmToolStripMenuItem";
            this.left001MmToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Left)));
            this.left001MmToolStripMenuItem.Size = new System.Drawing.Size(395, 34);
            this.left001MmToolStripMenuItem.Tag = "-0.01,0";
            this.left001MmToolStripMenuItem.Text = "Left (0.01 mm)";
            this.left001MmToolStripMenuItem.Click += new System.EventHandler(this.mnuJog_Click);
            // 
            // up001MmToolStripMenuItem
            // 
            this.up001MmToolStripMenuItem.Name = "up001MmToolStripMenuItem";
            this.up001MmToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Up)));
            this.up001MmToolStripMenuItem.Size = new System.Drawing.Size(395, 34);
            this.up001MmToolStripMenuItem.Tag = "0,0.01";
            this.up001MmToolStripMenuItem.Text = "Up (0.01 mm)";
            this.up001MmToolStripMenuItem.Click += new System.EventHandler(this.mnuJog_Click);
            // 
            // down001MmToolStripMenuItem
            // 
            this.down001MmToolStripMenuItem.Name = "down001MmToolStripMenuItem";
            this.down001MmToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Down)));
            this.down001MmToolStripMenuItem.Size = new System.Drawing.Size(395, 34);
            this.down001MmToolStripMenuItem.Tag = "0,-0.01";
            this.down001MmToolStripMenuItem.Text = "Down (0.01 mm)";
            this.down001MmToolStripMenuItem.Click += new System.EventHandler(this.mnuJog_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(363, 6);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(366, 34);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(366, 34);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.btnCut_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(366, 34);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // pasteCloneToolStripMenuItem
            // 
            this.pasteCloneToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteCloneToolStripMenuItem.Image")));
            this.pasteCloneToolStripMenuItem.Name = "pasteCloneToolStripMenuItem";
            this.pasteCloneToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.V)));
            this.pasteCloneToolStripMenuItem.Size = new System.Drawing.Size(366, 34);
            this.pasteCloneToolStripMenuItem.Text = "Paste Clone";
            this.pasteCloneToolStripMenuItem.Click += new System.EventHandler(this.btnPasteClone_Click);
            // 
            // pasteArrayToolStripMenuItem
            // 
            this.pasteArrayToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteArrayToolStripMenuItem.Image")));
            this.pasteArrayToolStripMenuItem.Name = "pasteArrayToolStripMenuItem";
            this.pasteArrayToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.V)));
            this.pasteArrayToolStripMenuItem.Size = new System.Drawing.Size(366, 34);
            this.pasteArrayToolStripMenuItem.Text = "Paste Array";
            this.pasteArrayToolStripMenuItem.Click += new System.EventHandler(this.btnPasteArray_Click);
            // 
            // toolStripMenuItem17
            // 
            this.toolStripMenuItem17.Name = "toolStripMenuItem17";
            this.toolStripMenuItem17.Size = new System.Drawing.Size(363, 6);
            // 
            // simulatorToolStripMenuItem
            // 
            this.simulatorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slowToolStripMenuItem,
            this.normalToolStripMenuItem,
            this.fastToolStripMenuItem,
            this.toolStripMenuItem13,
            this.stopToolStripMenuItem});
            this.simulatorToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("simulatorToolStripMenuItem.Image")));
            this.simulatorToolStripMenuItem.Name = "simulatorToolStripMenuItem";
            this.simulatorToolStripMenuItem.Size = new System.Drawing.Size(245, 32);
            this.simulatorToolStripMenuItem.Text = "Simulator";
            // 
            // slowToolStripMenuItem
            // 
            this.slowToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("slowToolStripMenuItem.Image")));
            this.slowToolStripMenuItem.Name = "slowToolStripMenuItem";
            this.slowToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F9)));
            this.slowToolStripMenuItem.Size = new System.Drawing.Size(259, 34);
            this.slowToolStripMenuItem.Text = "Slow";
            this.slowToolStripMenuItem.Click += new System.EventHandler(this.mnuSimulateSlow_Click);
            // 
            // normalToolStripMenuItem
            // 
            this.normalToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("normalToolStripMenuItem.Image")));
            this.normalToolStripMenuItem.Name = "normalToolStripMenuItem";
            this.normalToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F10)));
            this.normalToolStripMenuItem.Size = new System.Drawing.Size(259, 34);
            this.normalToolStripMenuItem.Text = "Normal";
            this.normalToolStripMenuItem.Click += new System.EventHandler(this.mnuSimulateNormal_Click);
            // 
            // fastToolStripMenuItem
            // 
            this.fastToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("fastToolStripMenuItem.Image")));
            this.fastToolStripMenuItem.Name = "fastToolStripMenuItem";
            this.fastToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F11)));
            this.fastToolStripMenuItem.Size = new System.Drawing.Size(259, 34);
            this.fastToolStripMenuItem.Text = "Fast";
            this.fastToolStripMenuItem.Click += new System.EventHandler(this.mnuSimulateFast_Click);
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(256, 6);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("stopToolStripMenuItem.Image")));
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F12)));
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(259, 34);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.mnuSimulateStop_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(242, 6);
            // 
            // mnuMarker
            // 
            this.mnuMarker.Image = ((System.Drawing.Image)(resources.GetObject("mnuMarker.Image")));
            this.mnuMarker.Name = "mnuMarker";
            this.mnuMarker.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.mnuMarker.Size = new System.Drawing.Size(245, 32);
            this.mnuMarker.Text = "Marker";
            this.mnuMarker.Click += new System.EventHandler(this.mnuMarker_Click);
            // 
            // mnuLaser
            // 
            this.mnuLaser.Image = ((System.Drawing.Image)(resources.GetObject("mnuLaser.Image")));
            this.mnuLaser.Name = "mnuLaser";
            this.mnuLaser.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.mnuLaser.Size = new System.Drawing.Size(245, 32);
            this.mnuLaser.Text = "Laser";
            this.mnuLaser.Click += new System.EventHandler(this.mnuLaser_Click);
            // 
            // mnuIO
            // 
            this.mnuIO.Name = "mnuIO";
            this.mnuIO.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.mnuIO.Size = new System.Drawing.Size(245, 32);
            this.mnuIO.Text = "I/O";
            this.mnuIO.Click += new System.EventHandler(this.mnuIO_Click);
            // 
            // mnuRtc
            // 
            this.mnuRtc.Image = ((System.Drawing.Image)(resources.GetObject("mnuRtc.Image")));
            this.mnuRtc.Name = "mnuRtc";
            this.mnuRtc.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.mnuRtc.Size = new System.Drawing.Size(245, 32);
            this.mnuRtc.Text = "RTC";
            this.mnuRtc.Click += new System.EventHandler(this.mnuRtc_Click);
            // 
            // fieldCorrectionToolStripMenuItem
            // 
            this.fieldCorrectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCorrection2D,
            this.mnuCorrection3D});
            this.fieldCorrectionToolStripMenuItem.Name = "fieldCorrectionToolStripMenuItem";
            this.fieldCorrectionToolStripMenuItem.Size = new System.Drawing.Size(245, 32);
            this.fieldCorrectionToolStripMenuItem.Text = "Field Correction";
            // 
            // mnuCorrection2D
            // 
            this.mnuCorrection2D.Name = "mnuCorrection2D";
            this.mnuCorrection2D.Size = new System.Drawing.Size(135, 34);
            this.mnuCorrection2D.Text = "2D";
            this.mnuCorrection2D.Click += new System.EventHandler(this.mnuCorrection2D_Click);
            // 
            // mnuCorrection3D
            // 
            this.mnuCorrection3D.Name = "mnuCorrection3D";
            this.mnuCorrection3D.Size = new System.Drawing.Size(135, 34);
            this.mnuCorrection3D.Text = "3D";
            this.mnuCorrection3D.Click += new System.EventHandler(this.mnuCorrection3D_Click);
            // 
            // mnuMeasurementFile
            // 
            this.mnuMeasurementFile.Image = ((System.Drawing.Image)(resources.GetObject("mnuMeasurementFile.Image")));
            this.mnuMeasurementFile.Name = "mnuMeasurementFile";
            this.mnuMeasurementFile.Size = new System.Drawing.Size(245, 32);
            this.mnuMeasurementFile.Text = "Measurement File";
            this.mnuMeasurementFile.Click += new System.EventHandler(this.mnuMeasurementFilePlotTool_Click);
            // 
            // mnuPowerMeter
            // 
            this.mnuPowerMeter.Enabled = false;
            this.mnuPowerMeter.Image = ((System.Drawing.Image)(resources.GetObject("mnuPowerMeter.Image")));
            this.mnuPowerMeter.Name = "mnuPowerMeter";
            this.mnuPowerMeter.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.mnuPowerMeter.Size = new System.Drawing.Size(245, 32);
            this.mnuPowerMeter.Text = "PowerMeter";
            this.mnuPowerMeter.Click += new System.EventHandler(this.mnuPowerMeter_Click);
            // 
            // mnuPowerMap
            // 
            this.mnuPowerMap.Enabled = false;
            this.mnuPowerMap.Image = ((System.Drawing.Image)(resources.GetObject("mnuPowerMap.Image")));
            this.mnuPowerMap.Name = "mnuPowerMap";
            this.mnuPowerMap.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.mnuPowerMap.Size = new System.Drawing.Size(245, 32);
            this.mnuPowerMap.Text = "PowerMap";
            this.mnuPowerMap.Click += new System.EventHandler(this.mnuPowerMap_Click);
            // 
            // mnuMotor
            // 
            this.mnuMotor.Enabled = false;
            this.mnuMotor.Image = ((System.Drawing.Image)(resources.GetObject("mnuMotor.Image")));
            this.mnuMotor.Name = "mnuMotor";
            this.mnuMotor.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.mnuMotor.Size = new System.Drawing.Size(245, 32);
            this.mnuMotor.Text = "Motor";
            this.mnuMotor.Click += new System.EventHandler(this.mnuMotor_Click);
            // 
            // mnuLogWindow
            // 
            this.mnuLogWindow.Image = ((System.Drawing.Image)(resources.GetObject("mnuLogWindow.Image")));
            this.mnuLogWindow.Name = "mnuLogWindow";
            this.mnuLogWindow.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.mnuLogWindow.Size = new System.Drawing.Size(245, 32);
            this.mnuLogWindow.Text = "Log Window";
            this.mnuLogWindow.Click += new System.EventHandler(this.mnuLogWindow_Click);
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(242, 6);
            // 
            // mnuAbout
            // 
            this.mnuAbout.Image = ((System.Drawing.Image)(resources.GetObject("mnuAbout.Image")));
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(245, 32);
            this.mnuAbout.Text = "About";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(85, 22);
            this.lblName.Text = "NoName";
            // 
            // pgbProgress
            // 
            this.pgbProgress.AutoSize = false;
            this.pgbProgress.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pgbProgress.Name = "pgbProgress";
            this.pgbProgress.Size = new System.Drawing.Size(50, 21);
            this.pgbProgress.Step = 1;
            // 
            // lblXPos
            // 
            this.lblXPos.AutoSize = false;
            this.lblXPos.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblXPos.Name = "lblXPos";
            this.lblXPos.Size = new System.Drawing.Size(80, 22);
            this.lblXPos.Text = "X: 0.000";
            // 
            // lblYPos
            // 
            this.lblYPos.AutoSize = false;
            this.lblYPos.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYPos.Name = "lblYPos";
            this.lblYPos.Size = new System.Drawing.Size(80, 22);
            this.lblYPos.Text = "Y: 0.000";
            // 
            // lblEntityCount
            // 
            this.lblEntityCount.AutoSize = false;
            this.lblEntityCount.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntityCount.Name = "lblEntityCount";
            this.lblEntityCount.Size = new System.Drawing.Size(80, 22);
            this.lblEntityCount.Text = "Selected: 0";
            // 
            // lblRenderTime
            // 
            this.lblRenderTime.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenderTime.Name = "lblRenderTime";
            this.lblRenderTime.Size = new System.Drawing.Size(98, 22);
            this.lblRenderTime.Text = "Render: 0 ms";
            // 
            // lblFileName
            // 
            this.lblFileName.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(61, 22);
            this.lblFileName.Text = "(Empty)";
            // 
            // stsBottom
            // 
            this.stsBottom.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stsBottom.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.stsBottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblName,
            this.toolStripStatusLabel3,
            this.pgbProgress,
            this.toolStripStatusLabel1,
            this.lblFileName,
            this.toolStripStatusLabel2,
            this.lblEntityCount,
            this.lblXPos,
            this.lblYPos,
            this.lblWH,
            this.toolStripStatusLabel4,
            this.lblRenderTime});
            this.stsBottom.Location = new System.Drawing.Point(0, 678);
            this.stsBottom.Name = "stsBottom";
            this.stsBottom.Size = new System.Drawing.Size(1075, 29);
            this.stsBottom.SizingGrip = false;
            this.stsBottom.TabIndex = 7;
            this.stsBottom.Text = "statusStrip1";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.AutoSize = false;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(8, 22);
            this.toolStripStatusLabel3.Text = " ";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(8, 22);
            this.toolStripStatusLabel1.Text = " ";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.AutoSize = false;
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(8, 22);
            this.toolStripStatusLabel2.Text = " ";
            // 
            // lblWH
            // 
            this.lblWH.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWH.Name = "lblWH";
            this.lblWH.Size = new System.Drawing.Size(129, 22);
            this.lblWH.Text = "WH: 0.000 x 0.000";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.AutoSize = false;
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(8, 22);
            this.toolStripStatusLabel4.Text = " ";
            // 
            // tlsTop2
            // 
            this.tlsTop2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlsTop2.GripMargin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.tlsTop2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlsTop2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tlsTop2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator12,
            this.btnPoint,
            this.btnPoints,
            this.btnRaster,
            this.btnLine,
            this.btnArc,
            this.btnCircle,
            this.btnEllipse,
            this.btnTrepan,
            this.btnTriangle,
            this.btnRectangle,
            this.btnLWPolyline,
            this.btnSpiral,
            this.ddbSiriusText,
            this.ddbText,
            this.btnBarcode1D,
            this.ddbBarcode,
            this.btnHPGL,
            this.btnImage,
            this.ddbStitchedImage,
            this.toolStripSeparator1,
            this.toolStripSeparator3,
            this.toolStripDropDownButton1,
            this.toolStripSeparator6,
            this.chbLock,
            this.btnLayer,
            this.ddbGroup,
            this.btnPens});
            this.tlsTop2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.tlsTop2.Location = new System.Drawing.Point(0, 34);
            this.tlsTop2.Name = "tlsTop2";
            this.tlsTop2.Size = new System.Drawing.Size(1075, 33);
            this.tlsTop2.TabIndex = 13;
            this.tlsTop2.Text = "toolStrip2";
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 33);
            // 
            // btnPoint
            // 
            this.btnPoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPoint.Image = ((System.Drawing.Image)(resources.GetObject("btnPoint.Image")));
            this.btnPoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPoint.Name = "btnPoint";
            this.btnPoint.Size = new System.Drawing.Size(34, 28);
            this.btnPoint.Text = "Point Entity";
            this.btnPoint.ToolTipText = "Point";
            this.btnPoint.Click += new System.EventHandler(this.btnPoint_Click);
            // 
            // btnPoints
            // 
            this.btnPoints.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPoints.Image = ((System.Drawing.Image)(resources.GetObject("btnPoints.Image")));
            this.btnPoints.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPoints.Name = "btnPoints";
            this.btnPoints.Size = new System.Drawing.Size(34, 28);
            this.btnPoints.Text = "Points Entity";
            this.btnPoints.ToolTipText = "Points";
            this.btnPoints.Click += new System.EventHandler(this.btnPoints_Click);
            // 
            // btnRaster
            // 
            this.btnRaster.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRaster.Image = ((System.Drawing.Image)(resources.GetObject("btnRaster.Image")));
            this.btnRaster.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRaster.Name = "btnRaster";
            this.btnRaster.Size = new System.Drawing.Size(34, 28);
            this.btnRaster.Text = "Raster";
            this.btnRaster.ToolTipText = "Raster";
            this.btnRaster.Click += new System.EventHandler(this.btnRaster_Click);
            // 
            // btnLine
            // 
            this.btnLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLine.Image = ((System.Drawing.Image)(resources.GetObject("btnLine.Image")));
            this.btnLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(34, 28);
            this.btnLine.Text = "Line Entity";
            this.btnLine.ToolTipText = "Line";
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // btnArc
            // 
            this.btnArc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnArc.Image = ((System.Drawing.Image)(resources.GetObject("btnArc.Image")));
            this.btnArc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnArc.Name = "btnArc";
            this.btnArc.Size = new System.Drawing.Size(34, 28);
            this.btnArc.Text = "Arc Entity";
            this.btnArc.ToolTipText = "Arc";
            this.btnArc.Click += new System.EventHandler(this.btnArc_Click);
            // 
            // btnCircle
            // 
            this.btnCircle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCircle.Image = ((System.Drawing.Image)(resources.GetObject("btnCircle.Image")));
            this.btnCircle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.Size = new System.Drawing.Size(34, 28);
            this.btnCircle.Text = "Circle Entity";
            this.btnCircle.ToolTipText = "Circle";
            this.btnCircle.Click += new System.EventHandler(this.btnCircle_Click);
            // 
            // btnEllipse
            // 
            this.btnEllipse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEllipse.Image = ((System.Drawing.Image)(resources.GetObject("btnEllipse.Image")));
            this.btnEllipse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEllipse.Name = "btnEllipse";
            this.btnEllipse.Size = new System.Drawing.Size(34, 28);
            this.btnEllipse.Text = "Ellipse";
            this.btnEllipse.Click += new System.EventHandler(this.btnEllipse_Click);
            // 
            // btnTrepan
            // 
            this.btnTrepan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTrepan.Image = ((System.Drawing.Image)(resources.GetObject("btnTrepan.Image")));
            this.btnTrepan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTrepan.Name = "btnTrepan";
            this.btnTrepan.Size = new System.Drawing.Size(34, 28);
            this.btnTrepan.ToolTipText = "Trepan";
            this.btnTrepan.Click += new System.EventHandler(this.btnTrepan_Click);
            // 
            // btnTriangle
            // 
            this.btnTriangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTriangle.Image = ((System.Drawing.Image)(resources.GetObject("btnTriangle.Image")));
            this.btnTriangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTriangle.Name = "btnTriangle";
            this.btnTriangle.Size = new System.Drawing.Size(34, 28);
            this.btnTriangle.Text = "Triangle";
            this.btnTriangle.Click += new System.EventHandler(this.btnTriangle_Click);
            // 
            // btnRectangle
            // 
            this.btnRectangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRectangle.Image = ((System.Drawing.Image)(resources.GetObject("btnRectangle.Image")));
            this.btnRectangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRectangle.Name = "btnRectangle";
            this.btnRectangle.Size = new System.Drawing.Size(34, 28);
            this.btnRectangle.Text = "Rectangle Entity";
            this.btnRectangle.ToolTipText = "Rectangle";
            this.btnRectangle.Click += new System.EventHandler(this.btnRectangle_Click);
            // 
            // btnLWPolyline
            // 
            this.btnLWPolyline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLWPolyline.Image = ((System.Drawing.Image)(resources.GetObject("btnLWPolyline.Image")));
            this.btnLWPolyline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLWPolyline.Name = "btnLWPolyline";
            this.btnLWPolyline.Size = new System.Drawing.Size(34, 28);
            this.btnLWPolyline.Text = "Polyline Entity";
            this.btnLWPolyline.ToolTipText = "LW Polyline";
            this.btnLWPolyline.Click += new System.EventHandler(this.btnLWPolyline_Click);
            // 
            // btnSpiral
            // 
            this.btnSpiral.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSpiral.Image = ((System.Drawing.Image)(resources.GetObject("btnSpiral.Image")));
            this.btnSpiral.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSpiral.Name = "btnSpiral";
            this.btnSpiral.Size = new System.Drawing.Size(34, 28);
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
            this.ddbSiriusText.Image = ((System.Drawing.Image)(resources.GetObject("ddbSiriusText.Image")));
            this.ddbSiriusText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ddbSiriusText.Name = "ddbSiriusText";
            this.ddbSiriusText.Size = new System.Drawing.Size(42, 28);
            this.ddbSiriusText.Text = "Special text entity";
            this.ddbSiriusText.ToolTipText = "Sirius Text";
            // 
            // mnuSiriusText
            // 
            this.mnuSiriusText.Image = ((System.Drawing.Image)(resources.GetObject("mnuSiriusText.Image")));
            this.mnuSiriusText.Name = "mnuSiriusText";
            this.mnuSiriusText.Size = new System.Drawing.Size(196, 34);
            this.mnuSiriusText.Text = "Text";
            this.mnuSiriusText.ToolTipText = "Text";
            this.mnuSiriusText.Click += new System.EventHandler(this.btnSiriusText_Click);
            // 
            // mnuSiriusTextArc
            // 
            this.mnuSiriusTextArc.Image = ((System.Drawing.Image)(resources.GetObject("mnuSiriusTextArc.Image")));
            this.mnuSiriusTextArc.Name = "mnuSiriusTextArc";
            this.mnuSiriusTextArc.Size = new System.Drawing.Size(196, 34);
            this.mnuSiriusTextArc.Text = "Text (Arc)";
            this.mnuSiriusTextArc.Click += new System.EventHandler(this.btnSiriusArcText_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(193, 6);
            // 
            // mnuSiriusTime
            // 
            this.mnuSiriusTime.Image = ((System.Drawing.Image)(resources.GetObject("mnuSiriusTime.Image")));
            this.mnuSiriusTime.Name = "mnuSiriusTime";
            this.mnuSiriusTime.Size = new System.Drawing.Size(196, 34);
            this.mnuSiriusTime.Text = "Time";
            this.mnuSiriusTime.ToolTipText = "Time";
            this.mnuSiriusTime.Click += new System.EventHandler(this.mnuTime_Click);
            // 
            // mnuSiriusDate
            // 
            this.mnuSiriusDate.Image = ((System.Drawing.Image)(resources.GetObject("mnuSiriusDate.Image")));
            this.mnuSiriusDate.Name = "mnuSiriusDate";
            this.mnuSiriusDate.Size = new System.Drawing.Size(196, 34);
            this.mnuSiriusDate.Text = "Date";
            this.mnuSiriusDate.ToolTipText = "Date";
            this.mnuSiriusDate.Click += new System.EventHandler(this.mnuDate_Click);
            // 
            // mnuSiriusSerial
            // 
            this.mnuSiriusSerial.Image = ((System.Drawing.Image)(resources.GetObject("mnuSiriusSerial.Image")));
            this.mnuSiriusSerial.Name = "mnuSiriusSerial";
            this.mnuSiriusSerial.Size = new System.Drawing.Size(196, 34);
            this.mnuSiriusSerial.Text = "Serial No";
            this.mnuSiriusSerial.ToolTipText = "Serial No";
            this.mnuSiriusSerial.Click += new System.EventHandler(this.mnuSerial_Click);
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
            this.ddbText.Image = ((System.Drawing.Image)(resources.GetObject("ddbText.Image")));
            this.ddbText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ddbText.Name = "ddbText";
            this.ddbText.Size = new System.Drawing.Size(42, 28);
            this.ddbText.Text = "True type font";
            this.ddbText.ToolTipText = "True Type Font";
            // 
            // mnuText
            // 
            this.mnuText.Image = ((System.Drawing.Image)(resources.GetObject("mnuText.Image")));
            this.mnuText.Name = "mnuText";
            this.mnuText.Size = new System.Drawing.Size(196, 34);
            this.mnuText.Text = "Text";
            this.mnuText.ToolTipText = "Text";
            this.mnuText.Click += new System.EventHandler(this.mnuText_Click);
            // 
            // mnuTextArc
            // 
            this.mnuTextArc.Image = ((System.Drawing.Image)(resources.GetObject("mnuTextArc.Image")));
            this.mnuTextArc.Name = "mnuTextArc";
            this.mnuTextArc.Size = new System.Drawing.Size(196, 34);
            this.mnuTextArc.Text = "Text (Arc)";
            this.mnuTextArc.Click += new System.EventHandler(this.btnTextArc_Click);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(193, 6);
            // 
            // mnuTextTime
            // 
            this.mnuTextTime.Enabled = false;
            this.mnuTextTime.Image = ((System.Drawing.Image)(resources.GetObject("mnuTextTime.Image")));
            this.mnuTextTime.Name = "mnuTextTime";
            this.mnuTextTime.Size = new System.Drawing.Size(196, 34);
            this.mnuTextTime.Text = "Time";
            this.mnuTextTime.ToolTipText = "Time";
            this.mnuTextTime.Click += new System.EventHandler(this.mnuTextTime_Click);
            // 
            // mnuTextDate
            // 
            this.mnuTextDate.Enabled = false;
            this.mnuTextDate.Image = ((System.Drawing.Image)(resources.GetObject("mnuTextDate.Image")));
            this.mnuTextDate.Name = "mnuTextDate";
            this.mnuTextDate.Size = new System.Drawing.Size(196, 34);
            this.mnuTextDate.Text = "Date";
            this.mnuTextDate.ToolTipText = "Date";
            this.mnuTextDate.Click += new System.EventHandler(this.mnuTextDate_Click);
            // 
            // mnuTextSerial
            // 
            this.mnuTextSerial.Enabled = false;
            this.mnuTextSerial.Image = ((System.Drawing.Image)(resources.GetObject("mnuTextSerial.Image")));
            this.mnuTextSerial.Name = "mnuTextSerial";
            this.mnuTextSerial.Size = new System.Drawing.Size(196, 34);
            this.mnuTextSerial.Text = "Serial No";
            this.mnuTextSerial.ToolTipText = "Serial No";
            this.mnuTextSerial.Click += new System.EventHandler(this.mnuTextSerial_Click);
            // 
            // btnBarcode1D
            // 
            this.btnBarcode1D.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBarcode1D.Image = ((System.Drawing.Image)(resources.GetObject("btnBarcode1D.Image")));
            this.btnBarcode1D.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBarcode1D.Name = "btnBarcode1D";
            this.btnBarcode1D.Size = new System.Drawing.Size(34, 28);
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
            this.ddbBarcode.Image = ((System.Drawing.Image)(resources.GetObject("ddbBarcode.Image")));
            this.ddbBarcode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ddbBarcode.Name = "ddbBarcode";
            this.ddbBarcode.Size = new System.Drawing.Size(42, 28);
            this.ddbBarcode.Text = "Data matrix entity";
            this.ddbBarcode.ToolTipText = "2D Barcode";
            // 
            // mnuDataMatrix
            // 
            this.mnuDataMatrix.Image = ((System.Drawing.Image)(resources.GetObject("mnuDataMatrix.Image")));
            this.mnuDataMatrix.Name = "mnuDataMatrix";
            this.mnuDataMatrix.Size = new System.Drawing.Size(206, 34);
            this.mnuDataMatrix.Text = "DataMatrix";
            this.mnuDataMatrix.ToolTipText = "DataMatrix";
            this.mnuDataMatrix.Click += new System.EventHandler(this.mnuDataMatrix_Click);
            // 
            // mnuQRCode
            // 
            this.mnuQRCode.Image = ((System.Drawing.Image)(resources.GetObject("mnuQRCode.Image")));
            this.mnuQRCode.Name = "mnuQRCode";
            this.mnuQRCode.Size = new System.Drawing.Size(206, 34);
            this.mnuQRCode.Text = "QR Code";
            this.mnuQRCode.ToolTipText = "QR Code";
            this.mnuQRCode.Click += new System.EventHandler(this.mnuQRCode_Click);
            // 
            // btnHPGL
            // 
            this.btnHPGL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnHPGL.Image = ((System.Drawing.Image)(resources.GetObject("btnHPGL.Image")));
            this.btnHPGL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHPGL.Name = "btnHPGL";
            this.btnHPGL.Size = new System.Drawing.Size(34, 28);
            this.btnHPGL.Text = "DXF & PLT";
            this.btnHPGL.ToolTipText = "Logo Files (DXF, PLT, STL, ...)";
            this.btnHPGL.Click += new System.EventHandler(this.btnHPGL_Click);
            // 
            // btnImage
            // 
            this.btnImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImage.Image = ((System.Drawing.Image)(resources.GetObject("btnImage.Image")));
            this.btnImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImage.Name = "btnImage";
            this.btnImage.Size = new System.Drawing.Size(34, 28);
            this.btnImage.Text = "Image";
            this.btnImage.Click += new System.EventHandler(this.btmImage_Click);
            // 
            // ddbStitchedImage
            // 
            this.ddbStitchedImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ddbStitchedImage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuStitchedImage,
            this.mnuLoadCells,
            this.toolStripMenuItem11,
            this.mnuClearCells});
            this.ddbStitchedImage.Image = ((System.Drawing.Image)(resources.GetObject("ddbStitchedImage.Image")));
            this.ddbStitchedImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ddbStitchedImage.Name = "ddbStitchedImage";
            this.ddbStitchedImage.Size = new System.Drawing.Size(42, 28);
            this.ddbStitchedImage.Text = "Stitched Image";
            this.ddbStitchedImage.ToolTipText = "Stitched Image";
            // 
            // mnuStitchedImage
            // 
            this.mnuStitchedImage.Image = ((System.Drawing.Image)(resources.GetObject("mnuStitchedImage.Image")));
            this.mnuStitchedImage.Name = "mnuStitchedImage";
            this.mnuStitchedImage.Size = new System.Drawing.Size(206, 34);
            this.mnuStitchedImage.Text = "Create";
            this.mnuStitchedImage.ToolTipText = "Create Stitched Image";
            this.mnuStitchedImage.Click += new System.EventHandler(this.mnuStitchedImage_Click);
            // 
            // mnuLoadCells
            // 
            this.mnuLoadCells.Image = ((System.Drawing.Image)(resources.GetObject("mnuLoadCells.Image")));
            this.mnuLoadCells.Name = "mnuLoadCells";
            this.mnuLoadCells.Size = new System.Drawing.Size(206, 34);
            this.mnuLoadCells.Text = "Load Cells";
            this.mnuLoadCells.ToolTipText = "Load Cells";
            this.mnuLoadCells.Click += new System.EventHandler(this.mnuLoadCellImages_Click);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(203, 6);
            // 
            // mnuClearCells
            // 
            this.mnuClearCells.Image = ((System.Drawing.Image)(resources.GetObject("mnuClearCells.Image")));
            this.mnuClearCells.Name = "mnuClearCells";
            this.mnuClearCells.Size = new System.Drawing.Size(206, 34);
            this.mnuClearCells.Text = "Clear Cells";
            this.mnuClearCells.ToolTipText = "Clear Cells";
            this.mnuClearCells.Click += new System.EventHandler(this.mnuClearCells_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 33);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 33);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTimer,
            this.mnuMeasurementBeginEnd,
            this.zOffsetToolStripMenuItem,
            this.writeDataToolStripMenuItem,
            this.mnuMOTF,
            this.mnuAlc,
            this.toolStripMenuItem4,
            this.mnuJump,
            this.mnuFiducial,
            this.mnuCalculationDynamics});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(42, 28);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.ToolTipText = "Control Entities";
            // 
            // mnuTimer
            // 
            this.mnuTimer.Image = ((System.Drawing.Image)(resources.GetObject("mnuTimer.Image")));
            this.mnuTimer.Name = "mnuTimer";
            this.mnuTimer.Size = new System.Drawing.Size(325, 34);
            this.mnuTimer.Text = "Timer";
            this.mnuTimer.ToolTipText = "Timer";
            this.mnuTimer.Click += new System.EventHandler(this.btnTimer_Click);
            // 
            // mnuMeasurementBeginEnd
            // 
            this.mnuMeasurementBeginEnd.Image = ((System.Drawing.Image)(resources.GetObject("mnuMeasurementBeginEnd.Image")));
            this.mnuMeasurementBeginEnd.Name = "mnuMeasurementBeginEnd";
            this.mnuMeasurementBeginEnd.Size = new System.Drawing.Size(325, 34);
            this.mnuMeasurementBeginEnd.Text = "Measurement Begin/End";
            this.mnuMeasurementBeginEnd.ToolTipText = "Measurement Begin/End";
            this.mnuMeasurementBeginEnd.Click += new System.EventHandler(this.mnuMeasurementBegin_Click);
            // 
            // zOffsetToolStripMenuItem
            // 
            this.zOffsetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuZOffset,
            this.mnuZDefocus});
            this.zOffsetToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("zOffsetToolStripMenuItem.Image")));
            this.zOffsetToolStripMenuItem.Name = "zOffsetToolStripMenuItem";
            this.zOffsetToolStripMenuItem.Size = new System.Drawing.Size(325, 34);
            this.zOffsetToolStripMenuItem.Text = "Z Control";
            this.zOffsetToolStripMenuItem.ToolTipText = "Z Control";
            // 
            // mnuZOffset
            // 
            this.mnuZOffset.Image = ((System.Drawing.Image)(resources.GetObject("mnuZOffset.Image")));
            this.mnuZOffset.Name = "mnuZOffset";
            this.mnuZOffset.Size = new System.Drawing.Size(182, 34);
            this.mnuZOffset.Text = "Offset";
            this.mnuZOffset.ToolTipText = "Z Offset";
            this.mnuZOffset.Click += new System.EventHandler(this.mnuZOffset_Click);
            // 
            // mnuZDefocus
            // 
            this.mnuZDefocus.Image = ((System.Drawing.Image)(resources.GetObject("mnuZDefocus.Image")));
            this.mnuZDefocus.Name = "mnuZDefocus";
            this.mnuZDefocus.Size = new System.Drawing.Size(182, 34);
            this.mnuZDefocus.Text = "Defocus";
            this.mnuZDefocus.ToolTipText = "Z Defocus";
            this.mnuZDefocus.Click += new System.EventHandler(this.mnuZDefocus_Click);
            // 
            // writeDataToolStripMenuItem
            // 
            this.writeDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.writeToolStripMenuItem,
            this.ext16ToolStripMenuItem,
            this.ext16IfToolStripMenuItem,
            this.waitExt16IfToolStripMenuItem1});
            this.writeDataToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("writeDataToolStripMenuItem.Image")));
            this.writeDataToolStripMenuItem.Name = "writeDataToolStripMenuItem";
            this.writeDataToolStripMenuItem.Size = new System.Drawing.Size(325, 34);
            this.writeDataToolStripMenuItem.Text = "Read/Write Data";
            this.writeDataToolStripMenuItem.ToolTipText = "Read/Write Data";
            // 
            // writeToolStripMenuItem
            // 
            this.writeToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("writeToolStripMenuItem.Image")));
            this.writeToolStripMenuItem.Name = "writeToolStripMenuItem";
            this.writeToolStripMenuItem.Size = new System.Drawing.Size(223, 34);
            this.writeToolStripMenuItem.Text = "Write";
            this.writeToolStripMenuItem.ToolTipText = "Write";
            this.writeToolStripMenuItem.Click += new System.EventHandler(this.mnuWriteData_Click);
            // 
            // ext16ToolStripMenuItem
            // 
            this.ext16ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ext16ToolStripMenuItem.Image")));
            this.ext16ToolStripMenuItem.Name = "ext16ToolStripMenuItem";
            this.ext16ToolStripMenuItem.Size = new System.Drawing.Size(223, 34);
            this.ext16ToolStripMenuItem.Text = "Ext16";
            this.ext16ToolStripMenuItem.ToolTipText = "Extension16";
            this.ext16ToolStripMenuItem.Click += new System.EventHandler(this.mnuWriteExt16_Click);
            // 
            // ext16IfToolStripMenuItem
            // 
            this.ext16IfToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ext16IfToolStripMenuItem.Image")));
            this.ext16IfToolStripMenuItem.Name = "ext16IfToolStripMenuItem";
            this.ext16IfToolStripMenuItem.Size = new System.Drawing.Size(223, 34);
            this.ext16IfToolStripMenuItem.Text = "Ext16 If";
            this.ext16IfToolStripMenuItem.ToolTipText = "Extension16 If Cond";
            this.ext16IfToolStripMenuItem.Click += new System.EventHandler(this.mnuWriteDataExt16If_Click);
            // 
            // waitExt16IfToolStripMenuItem1
            // 
            this.waitExt16IfToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("waitExt16IfToolStripMenuItem1.Image")));
            this.waitExt16IfToolStripMenuItem1.Name = "waitExt16IfToolStripMenuItem1";
            this.waitExt16IfToolStripMenuItem1.Size = new System.Drawing.Size(223, 34);
            this.waitExt16IfToolStripMenuItem1.Text = "Wait Ext16 If";
            this.waitExt16IfToolStripMenuItem1.ToolTipText = "Wait Extension16 If cond";
            this.waitExt16IfToolStripMenuItem1.Click += new System.EventHandler(this.waitExt16IfToolStripMenuItem_Click);
            // 
            // mnuMOTF
            // 
            this.mnuMOTF.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMOTFBeginEnd,
            this.mnuMOTFExtStartDelay,
            this.mnuMOTFWait,
            this.mnuMOTFRepeat,
            this.toolStripMenuItem1,
            this.mnuMOTFAngularBeginEnd,
            this.mnuMotfAngularWait});
            this.mnuMOTF.Image = ((System.Drawing.Image)(resources.GetObject("mnuMOTF.Image")));
            this.mnuMOTF.Name = "mnuMOTF";
            this.mnuMOTF.Size = new System.Drawing.Size(325, 34);
            this.mnuMOTF.Text = "MOTF";
            this.mnuMOTF.ToolTipText = "Marking On The Fly";
            // 
            // mnuMOTFBeginEnd
            // 
            this.mnuMOTFBeginEnd.Image = ((System.Drawing.Image)(resources.GetObject("mnuMOTFBeginEnd.Image")));
            this.mnuMOTFBeginEnd.Name = "mnuMOTFBeginEnd";
            this.mnuMOTFBeginEnd.Size = new System.Drawing.Size(271, 34);
            this.mnuMOTFBeginEnd.Text = "Begin/End";
            this.mnuMOTFBeginEnd.ToolTipText = "MOTF Begin/End";
            this.mnuMOTFBeginEnd.Click += new System.EventHandler(this.mnuMOTFBeginEnd_Click);
            // 
            // mnuMOTFExtStartDelay
            // 
            this.mnuMOTFExtStartDelay.Image = ((System.Drawing.Image)(resources.GetObject("mnuMOTFExtStartDelay.Image")));
            this.mnuMOTFExtStartDelay.Name = "mnuMOTFExtStartDelay";
            this.mnuMOTFExtStartDelay.Size = new System.Drawing.Size(271, 34);
            this.mnuMOTFExtStartDelay.Text = "Ext Start Delay";
            this.mnuMOTFExtStartDelay.ToolTipText = "MOTF Ext Start Delay";
            this.mnuMOTFExtStartDelay.Click += new System.EventHandler(this.mnuOTFExtStartDelay_Click);
            // 
            // mnuMOTFWait
            // 
            this.mnuMOTFWait.Image = ((System.Drawing.Image)(resources.GetObject("mnuMOTFWait.Image")));
            this.mnuMOTFWait.Name = "mnuMOTFWait";
            this.mnuMOTFWait.Size = new System.Drawing.Size(271, 34);
            this.mnuMOTFWait.Text = "Wait";
            this.mnuMOTFWait.ToolTipText = "MOTF Wait";
            this.mnuMOTFWait.Click += new System.EventHandler(this.mnuMOTFWait_Click);
            // 
            // mnuMOTFRepeat
            // 
            this.mnuMOTFRepeat.Image = ((System.Drawing.Image)(resources.GetObject("mnuMOTFRepeat.Image")));
            this.mnuMOTFRepeat.Name = "mnuMOTFRepeat";
            this.mnuMOTFRepeat.Size = new System.Drawing.Size(271, 34);
            this.mnuMOTFRepeat.Text = "Repeat";
            this.mnuMOTFRepeat.ToolTipText = "MOTF Repeat";
            this.mnuMOTFRepeat.Click += new System.EventHandler(this.mnuMOTFCall_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(268, 6);
            // 
            // mnuMOTFAngularBeginEnd
            // 
            this.mnuMOTFAngularBeginEnd.Image = ((System.Drawing.Image)(resources.GetObject("mnuMOTFAngularBeginEnd.Image")));
            this.mnuMOTFAngularBeginEnd.Name = "mnuMOTFAngularBeginEnd";
            this.mnuMOTFAngularBeginEnd.Size = new System.Drawing.Size(271, 34);
            this.mnuMOTFAngularBeginEnd.Text = "Angular Begin/End";
            this.mnuMOTFAngularBeginEnd.ToolTipText = "MOTF Angular Begin/End";
            this.mnuMOTFAngularBeginEnd.Click += new System.EventHandler(this.mnuMOTFAngularBeginEnd_Click);
            // 
            // mnuMotfAngularWait
            // 
            this.mnuMotfAngularWait.Image = ((System.Drawing.Image)(resources.GetObject("mnuMotfAngularWait.Image")));
            this.mnuMotfAngularWait.Name = "mnuMotfAngularWait";
            this.mnuMotfAngularWait.Size = new System.Drawing.Size(271, 34);
            this.mnuMotfAngularWait.Text = "Angular Wait";
            this.mnuMotfAngularWait.ToolTipText = "MOTF Angular Wait";
            this.mnuMotfAngularWait.Click += new System.EventHandler(this.mnuMotfAngularWait_Click);
            // 
            // mnuAlc
            // 
            this.mnuAlc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPoD,
            this.mnuVectorBeginEnd,
            this.toolStripMenuItem20,
            this.mnuAlcSyncAxisBeginEnd});
            this.mnuAlc.Image = ((System.Drawing.Image)(resources.GetObject("mnuAlc.Image")));
            this.mnuAlc.Name = "mnuAlc";
            this.mnuAlc.Size = new System.Drawing.Size(325, 34);
            this.mnuAlc.Text = "Automatic Laser Control";
            this.mnuAlc.ToolTipText = "Automatic Laser Control";
            // 
            // mnuPoD
            // 
            this.mnuPoD.Image = ((System.Drawing.Image)(resources.GetObject("mnuPoD.Image")));
            this.mnuPoD.Name = "mnuPoD";
            this.mnuPoD.Size = new System.Drawing.Size(364, 34);
            this.mnuPoD.Text = "Pulse On Demand";
            this.mnuPoD.ToolTipText = "Pulse On Demand";
            this.mnuPoD.Click += new System.EventHandler(this.mnuPoDBeginEnd_Click);
            // 
            // mnuVectorBeginEnd
            // 
            this.mnuVectorBeginEnd.Image = ((System.Drawing.Image)(resources.GetObject("mnuVectorBeginEnd.Image")));
            this.mnuVectorBeginEnd.Name = "mnuVectorBeginEnd";
            this.mnuVectorBeginEnd.Size = new System.Drawing.Size(364, 34);
            this.mnuVectorBeginEnd.Text = "Vector Defined Begin/End";
            this.mnuVectorBeginEnd.ToolTipText = "Vector Defined Begin/End";
            this.mnuVectorBeginEnd.Click += new System.EventHandler(this.mnuVectorBeginEnd_Click);
            // 
            // toolStripMenuItem20
            // 
            this.toolStripMenuItem20.Name = "toolStripMenuItem20";
            this.toolStripMenuItem20.Size = new System.Drawing.Size(361, 6);
            // 
            // mnuAlcSyncAxisBeginEnd
            // 
            this.mnuAlcSyncAxisBeginEnd.Image = ((System.Drawing.Image)(resources.GetObject("mnuAlcSyncAxisBeginEnd.Image")));
            this.mnuAlcSyncAxisBeginEnd.Name = "mnuAlcSyncAxisBeginEnd";
            this.mnuAlcSyncAxisBeginEnd.Size = new System.Drawing.Size(364, 34);
            this.mnuAlcSyncAxisBeginEnd.Text = "Pulse On Demand Begin/End";
            this.mnuAlcSyncAxisBeginEnd.ToolTipText = "Pulse On Demand Begin/End for SyncAxis";
            this.mnuAlcSyncAxisBeginEnd.Click += new System.EventHandler(this.mnuPoDSyncAxisBeginEnd_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(322, 6);
            // 
            // mnuJump
            // 
            this.mnuJump.Image = ((System.Drawing.Image)(resources.GetObject("mnuJump.Image")));
            this.mnuJump.Name = "mnuJump";
            this.mnuJump.Size = new System.Drawing.Size(325, 34);
            this.mnuJump.Text = "Jump";
            this.mnuJump.ToolTipText = "Jump";
            this.mnuJump.Click += new System.EventHandler(this.btnJump_Click);
            // 
            // mnuFiducial
            // 
            this.mnuFiducial.Image = ((System.Drawing.Image)(resources.GetObject("mnuFiducial.Image")));
            this.mnuFiducial.Name = "mnuFiducial";
            this.mnuFiducial.Size = new System.Drawing.Size(325, 34);
            this.mnuFiducial.Text = "Fiducial";
            this.mnuFiducial.ToolTipText = "Fiducial";
            this.mnuFiducial.Click += new System.EventHandler(this.mnuFiducial_Click);
            // 
            // mnuCalculationDynamics
            // 
            this.mnuCalculationDynamics.Image = ((System.Drawing.Image)(resources.GetObject("mnuCalculationDynamics.Image")));
            this.mnuCalculationDynamics.Name = "mnuCalculationDynamics";
            this.mnuCalculationDynamics.Size = new System.Drawing.Size(325, 34);
            this.mnuCalculationDynamics.Text = "Calculation Dynamics";
            this.mnuCalculationDynamics.ToolTipText = "Calculation Dynamics (Scan Device)";
            this.mnuCalculationDynamics.Click += new System.EventHandler(this.mnuCalculationDynamics_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 33);
            // 
            // chbLock
            // 
            this.chbLock.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.chbLock.AutoSize = false;
            this.chbLock.CheckOnClick = true;
            this.chbLock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.chbLock.Image = ((System.Drawing.Image)(resources.GetObject("chbLock.Image")));
            this.chbLock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.chbLock.Name = "chbLock";
            this.chbLock.Size = new System.Drawing.Size(36, 28);
            this.chbLock.Text = "Pan";
            this.chbLock.ToolTipText = "Pan";
            this.chbLock.CheckedChanged += new System.EventHandler(this.chbLock_CheckedChanged);
            // 
            // btnLayer
            // 
            this.btnLayer.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnLayer.AutoSize = false;
            this.btnLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLayer.Image = ((System.Drawing.Image)(resources.GetObject("btnLayer.Image")));
            this.btnLayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLayer.Name = "btnLayer";
            this.btnLayer.Size = new System.Drawing.Size(36, 28);
            this.btnLayer.ToolTipText = "Add Layer";
            this.btnLayer.Click += new System.EventHandler(this.btnLayer_Click);
            // 
            // ddbGroup
            // 
            this.ddbGroup.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ddbGroup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ddbGroup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGroup,
            this.mnuGroupOffset,
            this.mnuUnGroup});
            this.ddbGroup.Image = ((System.Drawing.Image)(resources.GetObject("ddbGroup.Image")));
            this.ddbGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ddbGroup.Name = "ddbGroup";
            this.ddbGroup.Size = new System.Drawing.Size(42, 28);
            this.ddbGroup.Text = "toolStripDropDownButton2";
            // 
            // mnuGroup
            // 
            this.mnuGroup.Image = ((System.Drawing.Image)(resources.GetObject("mnuGroup.Image")));
            this.mnuGroup.Name = "mnuGroup";
            this.mnuGroup.Size = new System.Drawing.Size(281, 34);
            this.mnuGroup.Text = "Group";
            this.mnuGroup.Click += new System.EventHandler(this.mnuGroup_Click);
            // 
            // mnuGroupOffset
            // 
            this.mnuGroupOffset.Image = ((System.Drawing.Image)(resources.GetObject("mnuGroupOffset.Image")));
            this.mnuGroupOffset.Name = "mnuGroupOffset";
            this.mnuGroupOffset.Size = new System.Drawing.Size(281, 34);
            this.mnuGroupOffset.Text = "Group (with Offset)";
            this.mnuGroupOffset.Click += new System.EventHandler(this.mnuGroupOffset_Click);
            // 
            // mnuUnGroup
            // 
            this.mnuUnGroup.Image = ((System.Drawing.Image)(resources.GetObject("mnuUnGroup.Image")));
            this.mnuUnGroup.Name = "mnuUnGroup";
            this.mnuUnGroup.Size = new System.Drawing.Size(281, 34);
            this.mnuUnGroup.Text = "UnGroup";
            this.mnuUnGroup.Click += new System.EventHandler(this.mnuUngroup_Click);
            // 
            // btnPens
            // 
            this.btnPens.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnPens.AutoSize = false;
            this.btnPens.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPens.Enabled = false;
            this.btnPens.Image = ((System.Drawing.Image)(resources.GetObject("btnPens.Image")));
            this.btnPens.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPens.Name = "btnPens";
            this.btnPens.Size = new System.Drawing.Size(36, 28);
            this.btnPens.ToolTipText = "Pens Editor";
            this.btnPens.Click += new System.EventHandler(this.btnPens_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 67);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.GLcontrol);
            this.splitContainer1.Panel1MinSize = 300;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2MinSize = 100;
            this.splitContainer1.Size = new System.Drawing.Size(1075, 611);
            this.splitContainer1.SplitterDistance = 786;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 14;
            // 
            // GLcontrol
            // 
            this.GLcontrol.ContextMenuStrip = this.ctmsMain;
            this.GLcontrol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GLcontrol.DrawFPS = false;
            this.GLcontrol.FrameRate = 30;
            this.GLcontrol.Location = new System.Drawing.Point(0, 0);
            this.GLcontrol.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.GLcontrol.Name = "GLcontrol";
            //this.GLcontrol.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.GLcontrol.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.GLcontrol.RenderTrigger = SharpGL.RenderTrigger.Manual;
            this.GLcontrol.Size = new System.Drawing.Size(786, 611);
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
            this.splitContainer2.Panel1.Controls.Add(this.stsDimension);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ppgEntity);
            this.splitContainer2.Size = new System.Drawing.Size(286, 611);
            this.splitContainer2.SplitterDistance = 182;
            this.splitContainer2.TabIndex = 2;
            // 
            // trvEntity
            // 
            this.trvEntity.AllowDrop = true;
            this.trvEntity.ContextMenuStrip = this.ctmsMain;
            this.trvEntity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvEntity.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvEntity.Location = new System.Drawing.Point(0, 0);
            this.trvEntity.Name = "trvEntity";
            this.trvEntity.SelectedNodes = ((System.Collections.Generic.List<System.Windows.Forms.TreeNode>)(resources.GetObject("trvEntity.SelectedNodes")));
            this.trvEntity.Size = new System.Drawing.Size(286, 153);
            this.trvEntity.TabIndex = 3;
            this.trvEntity.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.trvEntity_ItemDrag);
            this.trvEntity.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvEntity_AfterSelect);
            this.trvEntity.DragDrop += new System.Windows.Forms.DragEventHandler(this.trvEntity_DragDrop);
            this.trvEntity.DragEnter += new System.Windows.Forms.DragEventHandler(this.trvEntity_DragEnter);
            this.trvEntity.DragOver += new System.Windows.Forms.DragEventHandler(this.trvEntity_DragOver);
            // 
            // stsDimension
            // 
            this.stsDimension.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stsDimension.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.stsDimension.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblBound,
            this.lblCenter});
            this.stsDimension.Location = new System.Drawing.Point(0, 153);
            this.stsDimension.Name = "stsDimension";
            this.stsDimension.Size = new System.Drawing.Size(286, 29);
            this.stsDimension.SizingGrip = false;
            this.stsDimension.TabIndex = 2;
            this.stsDimension.Text = "statusStrip2";
            // 
            // lblBound
            // 
            this.lblBound.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.lblBound.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBound.Name = "lblBound";
            this.lblBound.Size = new System.Drawing.Size(186, 22);
            this.lblBound.Text = " 0.000, 0.000  0.000, 0.000 ";
            // 
            // lblCenter
            // 
            this.lblCenter.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCenter.Name = "lblCenter";
            this.lblCenter.Size = new System.Drawing.Size(86, 22);
            this.lblCenter.Text = "0.000 0.000";
            // 
            // ppgEntity
            // 
            this.ppgEntity.ContextMenuStrip = this.ctmsMain;
            this.ppgEntity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ppgEntity.Location = new System.Drawing.Point(0, 0);
            this.ppgEntity.Name = "ppgEntity";
            this.ppgEntity.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.ppgEntity.Size = new System.Drawing.Size(286, 425);
            this.ppgEntity.TabIndex = 1;
            this.ppgEntity.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.ppgEntity_PropertyValueChanged);
            // 
            // CustomEditorForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1075, 707);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.tlsTop2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.tlsTop);
            this.Controls.Add(this.stsBottom);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CustomEditorForm";
            this.Text = "Custom Editor - (c)SpralLAB";
            this.tlsTop.ResumeLayout(false);
            this.tlsTop.PerformLayout();
            this.ctmsMain.ResumeLayout(false);
            this.stsBottom.ResumeLayout(false);
            this.stsBottom.PerformLayout();
            this.tlsTop2.ResumeLayout(false);
            this.tlsTop2.PerformLayout();
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
            this.stsDimension.ResumeLayout(false);
            this.stsDimension.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.SaveFileDialog sfd;
        private System.Windows.Forms.ToolStrip tlsTop;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnDocumentInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ContextMenuStrip ctmsMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alignToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem originToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnPaste;
        private System.Windows.Forms.ToolStripButton btnCut;
        private System.Windows.Forms.ToolStripButton btnCopy;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnImport;
        private System.Windows.Forms.ToolStripStatusLabel lblName;
        private System.Windows.Forms.ToolStripProgressBar pgbProgress;
        private System.Windows.Forms.ToolStripStatusLabel lblXPos;
        private System.Windows.Forms.ToolStripStatusLabel lblYPos;
        private System.Windows.Forms.ToolStripStatusLabel lblEntityCount;
        private System.Windows.Forms.ToolStripStatusLabel lblRenderTime;
        private System.Windows.Forms.ToolStripStatusLabel lblFileName;
        private System.Windows.Forms.StatusStrip stsBottom;
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
        private System.Windows.Forms.ToolStrip tlsTop2;
        private System.Windows.Forms.ToolStripButton btnPoint;
        private System.Windows.Forms.ToolStripButton btnLine;
        private System.Windows.Forms.ToolStripButton btnArc;
        private System.Windows.Forms.ToolStripButton btnCircle;
        private System.Windows.Forms.ToolStripButton btnRectangle;
        private System.Windows.Forms.ToolStripButton btnLWPolyline;
        private System.Windows.Forms.ToolStripButton btnSpiral;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnLayer;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public SharpGL.OpenGLControl GLcontrol;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private MultiSelectTreeview trvEntity;
        private System.Windows.Forms.StatusStrip stsDimension;
        private System.Windows.Forms.ToolStripStatusLabel lblBound;
        private System.Windows.Forms.PropertyGrid ppgEntity;
        private System.Windows.Forms.ToolStripButton btnZoomOut;
        private System.Windows.Forms.ToolStripButton btnZoomIn;
        private System.Windows.Forms.ToolStripButton btnZoomFit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton chbPan;
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
        private System.Windows.Forms.ToolStripDropDownButton ddbBarcode;
        private System.Windows.Forms.ToolStripMenuItem mnuDataMatrix;
        private System.Windows.Forms.ToolStripButton btnPoints;
        private System.Windows.Forms.ToolStripButton btnHatch;
        private System.Windows.Forms.ToolStripButton btnTrepan;
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
        private System.Windows.Forms.ToolStripDropDownButton ddbText;
        private System.Windows.Forms.ToolStripMenuItem mnuText;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem12;
        private System.Windows.Forms.ToolStripMenuItem mnuTextTime;
        private System.Windows.Forms.ToolStripMenuItem mnuTextDate;
        private System.Windows.Forms.ToolStripMenuItem mnuTextSerial;
        private System.Windows.Forms.ToolStripMenuItem mnuMarker;
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
        private System.Windows.Forms.ToolStripMenuItem mnuLaser;
        private System.Windows.Forms.ToolStripButton btnImage;
        private System.Windows.Forms.ToolStripButton btnRaster;
        private System.Windows.Forms.ToolStripMenuItem fieldCorrectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuCorrection2D;
        private System.Windows.Forms.ToolStripMenuItem mnuCorrection3D;
        private System.Windows.Forms.ToolStripButton btnEllipse;
        private System.Windows.Forms.ToolStripButton btnRotateCustom;
        private System.Windows.Forms.ToolStripDropDownButton ddbStitchedImage;
        private System.Windows.Forms.ToolStripMenuItem mnuStitchedImage;
        private System.Windows.Forms.ToolStripMenuItem mnuLoadCells;
        private System.Windows.Forms.ToolStripMenuItem mnuClearCells;
        private System.Windows.Forms.ToolStripMenuItem mnuIO;
        private System.Windows.Forms.ToolStripMenuItem pasteCloneToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnPasteClone;
        private System.Windows.Forms.ToolStripMenuItem rotateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cWToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cCWToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem17;
        private System.Windows.Forms.ToolStripMenuItem translateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rightToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem leftToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem forwardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backwardToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem18;
        private System.Windows.Forms.ToolStripMenuItem right01MmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem left01MmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem upToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem down01mmToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem19;
        private System.Windows.Forms.ToolStripMenuItem right001MmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem left001MmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem up001MmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem down001MmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuLogWindow;
        private System.Windows.Forms.ToolStripButton btnDivide;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnToArc;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem simulatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem slowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fastToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem13;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnTransit;
        private System.Windows.Forms.ToolStripButton btnPens;
        private System.Windows.Forms.ToolStripMenuItem mnuPowerMeter;
        private System.Windows.Forms.ToolStripMenuItem mnuPowerMap;
        private System.Windows.Forms.ToolStripMenuItem mnuMeasurementFile;
        private System.Windows.Forms.ToolStripButton chbLock;
        private System.Windows.Forms.ToolStripMenuItem mnuRtc;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem14;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.ToolStripStatusLabel lblWH;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripButton btnTriangle;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem mnuJump;
        private System.Windows.Forms.ToolStripMenuItem mnuTimer;
        private System.Windows.Forms.ToolStripMenuItem mnuMeasurementBeginEnd;
        private System.Windows.Forms.ToolStripMenuItem zOffsetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuZOffset;
        private System.Windows.Forms.ToolStripMenuItem mnuZDefocus;
        private System.Windows.Forms.ToolStripMenuItem writeDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem writeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ext16ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ext16IfToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem waitExt16IfToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuFiducial;
        private System.Windows.Forms.ToolStripMenuItem mnuMotor;
        private System.Windows.Forms.ToolStripMenuItem mnuMOTF;
        private System.Windows.Forms.ToolStripMenuItem mnuMOTFBeginEnd;
        private System.Windows.Forms.ToolStripMenuItem mnuMOTFExtStartDelay;
        private System.Windows.Forms.ToolStripMenuItem mnuMOTFWait;
        private System.Windows.Forms.ToolStripMenuItem mnuAlc;
        private System.Windows.Forms.ToolStripMenuItem mnuPoD;
        private System.Windows.Forms.ToolStripMenuItem mnuVectorBeginEnd;
        private System.Windows.Forms.ToolStripMenuItem mnuAlcSyncAxisBeginEnd;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem20;
        private System.Windows.Forms.ToolStripDropDownButton ddbGroup;
        private System.Windows.Forms.ToolStripMenuItem mnuGroup;
        private System.Windows.Forms.ToolStripMenuItem mnuGroupOffset;
        private System.Windows.Forms.ToolStripMenuItem mnuUnGroup;
        private System.Windows.Forms.ToolStripStatusLabel lblCenter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripMenuItem mnuMOTFRepeat;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripMenuItem mnuCalculationDynamics;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuMOTFAngularBeginEnd;
        private System.Windows.Forms.ToolStripMenuItem mnuMotfAngularWait;
    }
}
