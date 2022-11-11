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
 * Copyright (C) 2019-2023 SpiralLab. All rights reserved. 
 * marker form
 * Description : 
 * Author : hong chan, choi / hcchoi@spirallab.co.kr (http://spirallab.co.kr)
 * 
 */


using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    /// <summary>
    /// 커스텀 마커 폼
    /// SyncAxis 제어기 전용
    /// </summary>
    public partial class CustomMarkerSyncAxisForm : Form
    {
        public virtual string AliasName
        {
            get { return this.lblName.Text; }
            set { this.lblName.Text = value; }
        }
        public IMarker Marker { get; set; }
        public IDocument Document { get; set; }
        public IRtc Rtc { get; set; }
        public ILaser Laser { get; set; }
        public IMotor MotorZ { get; set; }
        public IPowerMap PowerMap { get; set; }
        public CustomEditorForm Editor 
        {
            get { return this.owner; }
            set {
                this.timer1.Enabled = false;
                this.owner = value;
                if (null != this.owner)
                {
                    Marker.OnProgress += Marker_OnProgress;
                    Marker.OnFinished += Marker_OnFinished;
                    if (null != Marker.MarkerArg)
                    {
                        this.offsets.RaiseListChangedEvents = false;
                        this.offsets.Clear();
                        foreach (var o in Marker.MarkerArg.Offsets)
                            offsets.Add(o);
                        this.offsets.RaiseListChangedEvents = true;
                        this.offsets.ResetBindings();
                    }

                    //first once
                    var arg = new MarkerArgDefault(Document, Rtc, Laser, MotorZ);
                    arg.ViewTargets.Add(this.Editor.View); //viewer?
                    if (null != Viewer)
                        arg.ViewTargets.Add(Viewer.View);
                    this.Marker.Ready(arg);
                    this.timer1.Enabled = true;
                }
            }
        }
        private CustomEditorForm owner;

        public CustomViewerForm Viewer { get; set; }

        /// <summary>
        /// 오프셋 리스트 (바인딩 용)
        /// </summary>
        public BindingList<Offset> Offsets
        {
            get { return this.offsets; }
            set { this.offsets = value; }
        }
        private BindingList<Offset> offsets = new BindingList<Offset>();

        /// <summary>
        /// 생성자
        /// </summary>
        public CustomMarkerSyncAxisForm()
        {
            InitializeComponent();

            this.ppgOffset.PropertySort = PropertySort.Categorized;

            lsbOffset.DrawMode = DrawMode.OwnerDrawVariable;
            lsbOffset.DataSource = this.offsets;
            lsbOffset.SelectedIndexChanged += LsbOffset_SelectedIndexChanged;
            lsbOffset.MeasureItem += LsbOffset_MeasureItem;
            lsbOffset.DrawItem += LsbOffset_DrawItem;

            btnAdd.Click += BtnAdd_Click;
            btnRemove.Click += BtnRemove_Click;
            btnClear.Click += BtnClear_Click;
            btnUp.Click += BtnUp_Click;
            btnDown.Click += BtnDown_Click;
            ppgOffset.PropertyValueChanged += PpgOffset_PropertyValueChanged;
            cbbMarkTarget.SelectedIndex = 0;

            txtFrequency.KeyUp += TxtFrequency_KeyPress;
            txtPulseWidth.KeyUp += TxtPulseWidth_KeyPress;
            txtDuty.KeyUp += TxtDuty_KeyPress;

            this.Load += MarkerForm_Load;
            this.FormClosing += MarkerForm_FormClosing;
            this.FormClosed += MarkerForm_FormClosed;
        }

        private void TxtFrequency_KeyPress(object sender, KeyEventArgs e)
        {
            if (float.TryParse(txtFrequency.Text, out float hz))
            {
                if (float.TryParse(txtPulseWidth.Text, out float usec))
                {
                    float t = 1.0f / hz; //s
                    float t_usec = t * 1000000.0f;
                    txtDuty.Text =  $"{usec / t_usec * 100.0f:F3}";
                }
            }
        }
        private void TxtPulseWidth_KeyPress(object sender, KeyEventArgs e)
        {
            TxtFrequency_KeyPress(sender, e);
        }
        private void TxtDuty_KeyPress(object sender, KeyEventArgs e)
        {
            if (float.TryParse(txtFrequency.Text, out float hz))
            {
                if (float.TryParse(txtDuty.Text, out float duty))
                {
                    float t = 1.0f / hz; //s
                    float t_usec = t * 1000000.0f;
                    txtPulseWidth.Text = $"{t_usec * duty / 100.0f:F3}";
                }
            }
        }
        private void MarkerForm_Load(object sender, EventArgs e)
        {
            var laser = this.Laser;
            if (laser != null)
            {
                lblPower.Enabled = laser.IsPowerControl;
                txtPower.Enabled = laser.IsPowerControl;
                chbGuide.Enabled = laser.IsGuideControl;
                if (laser.IsGuideControl)
                {
                    var guide = laser as IGuideControl;
                    chbGuide.Checked = guide.IsGuideOn;
                    if (chbGuide.Checked)
                        pnGuide.BackColor = Color.Lime;
                    else
                        pnGuide.BackColor = Color.Green;
                }
            }
            var syncAxis = Rtc as IRtcSyncAxis;
            if (null != syncAxis)
            {
                txtStageSpeed.Text = $"{syncAxis.StageMoveSpeed:F1}";
                txtStageTimeOut.Text = $"{syncAxis.StageMoveTimeOut:F1}";                
                syncAxis.OnJob += SyncAxis_OnJobStatus;

                ppgJob.SelectedObject = syncAxis.Job;
                ppgTrajectory.SelectedObject = syncAxis.Trajectory;
                ppgDynamics.SelectedObject = syncAxis.Dynamics;

                chbSimulationMode.Checked = syncAxis.IsSimulationMode;

                cbbStage.Items.Clear();
                for (int i=0; i< syncAxis.StageCounts; i++)
                {
                    cbbStage.Items.Add($"Stage{i + 1}");
                }
                cbbStage.SelectedIndex = (int)syncAxis.TargetStage - 1;

                cbbCorrection.SelectedIndex = (int)Rtc.PrimaryHeadTable;

                txtHead1Offset.Text = $"{syncAxis.Head1Offset.X:F3}, {syncAxis.Head1Offset.Y:F3}, {syncAxis.Head1Offset.Z:F3}";                
                txtHead2Offset.Text = $"{syncAxis.Head2Offset.X:F3}, {syncAxis.Head2Offset.Y:F3}, {syncAxis.Head2Offset.Z:F3}";
                txtHead3Offset.Text = $"{syncAxis.Head3Offset.X:F3}, {syncAxis.Head3Offset.Y:F3}, {syncAxis.Head3Offset.Z:F3}";
                txtHead4Offset.Text = $"{syncAxis.Head4Offset.X:F3}, {syncAxis.Head4Offset.Y:F3}, {syncAxis.Head4Offset.Z:F3}";

                switch(syncAxis.HeadCounts)
                {
                    case 1:
                        txtHead2Offset.Enabled = false;
                        btnHead2.Enabled = false;
                        txtHead3Offset.Enabled = false;
                        btnHead3.Enabled = false;
                        txtHead4Offset.Enabled = false;
                        btnHead4.Enabled = false;
                        break;
                    case 2:
                        txtHead3Offset.Enabled = false;
                        btnHead3.Enabled = false;
                        txtHead4Offset.Enabled = false;
                        btnHead4.Enabled = false;
                        break;
                    case 3:
                        txtHead4Offset.Enabled = false;
                        btnHead4.Enabled = false;
                        break;
                }
            }

            cbbPowerCategory.Items.Clear();
            if (null != this.PowerMap)
            {
                this.PowerMap.Categories(out string[] categories);
                cbbPowerCategory.Items.AddRange(categories);
            }
            this.lsbOffset.AllowDrop = true;
        }

        private void SyncAxis_OnJobStatus(IRtcSyncAxis sender, Job jobStatus)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                var syncAxis = this.Rtc as IRtcSyncAxis;
                if (null == syncAxis)
                    return;
                ppgJob.SelectedObject = jobStatus;
                ppgJob.Refresh();
                ppgTrajectory.SelectedObject = syncAxis.Trajectory;
                ppgTrajectory.Refresh();
                ppgDynamics.SelectedObject = syncAxis.Dynamics;
                ppgDynamics.Refresh();
            }));
        }

        private void LsbOffset_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            ListBox lst = sender as ListBox;
            var item = (Offset)lst.Items[e.Index];

            e.DrawBackground();

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                using (Font drawFontBold = new Font("Arial", 10, FontStyle.Bold))
                {
                    e.Graphics.DrawString($"{e.Index + 1}: {item.ToString()}", drawFontBold, SystemBrushes.HighlightText, e.Bounds.Left, e.Bounds.Top);
                }
            }
            else
            {
                using (SolidBrush br = new SolidBrush(e.ForeColor))
                {
                    e.Graphics.DrawString($"{e.Index+1}: {item.ToString()}", lsbOffset.Font, br, e.Bounds.Left, e.Bounds.Top);
                }
            }
            e.DrawFocusRectangle();
        }
        private void LsbOffset_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            ListBox lst = sender as ListBox;
            var item = (Offset)lst.Items[e.Index];
            SizeF txt_size = e.Graphics.MeasureString($"{e.Index+1}: {item.ToString()}", lsbOffset.Font);
            e.ItemHeight = (int)txt_size.Height + 2;
            e.ItemWidth = (int)txt_size.Width;
        }
        private void BtnUp_Click(object sender, EventArgs e)
        {
            //up
            int index = lsbOffset.SelectedIndex;
            if (index <= 0)
                return;
            this.MoveItem(index, index - 1);
            lsbOffset.SelectedIndex = index - 1;
        }
        private void BtnDown_Click(object sender, EventArgs e)
        {
            //down
            int index = lsbOffset.SelectedIndex;
            if (index < 0 || index == lsbOffset.Items.Count - 1)
                return;
            this.MoveItem(index, index + 1);
            lsbOffset.SelectedIndex = index + 1;
        }
        private void MoveItem(int oldIndex, int newIndex)
        {
            Offset item = this.offsets[oldIndex];
            this.offsets.RemoveAt(oldIndex);
            this.offsets.Insert(newIndex, item);
        }
        private void PpgOffset_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            string propertyName = e.ChangedItem.PropertyDescriptor.Name;
            var oldValue = e.OldValue;
            var newValue = e.ChangedItem.Value;

            int index = lsbOffset.SelectedIndex;
            var item = this.offsets.ElementAt(index);
            object boxed = item;

            Type type = typeof(Offset);
            var propType = type.GetProperty(propertyName);
            propType.SetValue(boxed, newValue, null);
            item = (Offset)boxed;
            this.offsets[index] = item;
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            this.offsets.Add(new Offset(0, 0));
            var item = lsbOffset.SelectedItem;
            this.ppgOffset.SelectedObject = item;
        }
        private void BtnRemove_Click(object sender, EventArgs e)
        {
            int index = lsbOffset.SelectedIndex;
            if (index >= 0)
            {
                this.offsets.RemoveAt(index);
            }
            var item = lsbOffset.SelectedItem;
            this.ppgOffset.SelectedObject = item;
        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            this.offsets.Clear();
            this.ppgOffset.SelectedObject = null;
        }
        private void LsbOffset_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = lsbOffset.SelectedItem;
            this.ppgOffset.SelectedObject = item;
            if (null != item)
            {
                var o = (Offset)item;
            }
        }
        private void Marker_OnProgress(IMarker sender, IMarkerArg arg)
        {
            if (!this.IsHandleCreated)
                return;
            if (statusStrip1.InvokeRequired)
            {
                statusStrip1.BeginInvoke(new MethodInvoker(delegate ()
                {
                    pgbProgress.Value = (int)arg.Progress;
                }));
            }
            else
            {
                pgbProgress.Value = (int)arg.Progress;
            }
        }
        private void Marker_OnFinished(IMarker sender, IMarkerArg arg)
        {
            if (!this.IsHandleCreated)
                return;
            var span = arg.EndTime - arg.StartTime;
            if (statusStrip1.InvokeRequired)
            {
                statusStrip1.BeginInvoke(new MethodInvoker(delegate ()
                {
                    lblTime.Text = $" {span.TotalSeconds:F3}s ";
                    pgbProgress.Value = (int)arg.Progress;
                }));
            }
            else
            {
                lblTime.Text = $" {span.TotalSeconds:F3}s ";
                pgbProgress.Value = (int)arg.Progress;
            }
        }
        private void MarkerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Marker.IsBusy)
            {
                if (DialogResult.Yes != MessageBox.Show($"Do you really want to abort the process ?", "WARNING!!! LASER IS BUSY", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    e.Cancel = true;
                    return;
                }
                else
                {
                    this.Marker.Stop();
                }
            }
        }
        private void MarkerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (null == this.Marker)
                return;
            this.Marker.OnProgress -= Marker_OnProgress;
            this.Marker.OnFinished -= Marker_OnFinished;
            
            var syncAxis = Rtc as IRtcSyncAxis;
            if (null != syncAxis)
            {
                syncAxis.OnJob -= SyncAxis_OnJobStatus;
            }
        }

        uint tick = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (null == this.Marker)
                return;
            if (this.Marker.IsReady)
            {
                lblReady.ForeColor = Color.Black;
                lblReady.BackColor = Color.Lime;
            }
            else
            {
                lblReady.ForeColor = Color.White;
                lblReady.BackColor = Color.Green;
            }

            if (this.Marker.IsBusy)
            {
                if (0 == tick % 2)
                {
                    lblBusy.BackColor = Color.Red;
                    lblBusy.ForeColor = Color.White;

                    btnStop.BackColor = Color.Maroon;
                    btnStop.ForeColor = Color.White;
                }
                else
                {
                    lblBusy.BackColor = Color.Maroon;
                    lblBusy.ForeColor = Color.White;

                    btnStop.BackColor = Color.Red;
                    btnStop.ForeColor = Color.White;
                }
                tick++;
            }
            else
            {
                lblBusy.ForeColor = Color.White;
                lblBusy.BackColor = Color.Maroon;

                btnStop.BackColor = Color.WhiteSmoke;
                btnStop.ForeColor = SystemColors.ControlText;
                tick = 0;
            }

            if (this.Marker.IsError)
            {
                lblError.ForeColor = Color.White;
                lblError.BackColor = Color.Red;
            }
            else
            {
                lblError.ForeColor = Color.White;
                lblError.BackColor = Color.Maroon;
            }

            var syncAxis = Rtc as IRtcSyncAxis;
            if (null != syncAxis)
            {
                switch (syncAxis.OpStatus)
                {
                    case OperationStatus.Green:                        
                        lblOperationStatus.BackColor = Color.Lime;
                        lblOperationStatus.ForeColor = Color.Black;
                        break;
                    case OperationStatus.Red:
                        lblOperationStatus.BackColor = Color.Red;
                        lblOperationStatus.ForeColor = Color.White;
                        break;
                    case OperationStatus.Yellow:
                        lblOperationStatus.BackColor = Color.Yellow;
                        lblOperationStatus.ForeColor = Color.Black;
                        break;
                    default:
                        lblOperationStatus.BackColor = Color.Gray;
                        lblOperationStatus.ForeColor = Color.White;
                        break;
                }
                switch(syncAxis.MotionMode)
                {
                    case MotionMode.Unfollow:
                        pnUnfollow.BackColor = Color.Lime;
                        pnFollow.BackColor = Color.Green;
                        break;
                    case MotionMode.Follow:
                        pnUnfollow.BackColor = Color.Green;
                        pnFollow.BackColor = Color.Lime;
                        break;
                }
                syncAxis.CtlGetStagePosition(out float stageX, out float stageY);
                txtStageActualPosition.Text = $"{stageX:F3}, {stageY:F3}";

                syncAxis.CtlGetScannerPosition(ScanDevice.ScanDevice1, out float scannerX, out float scannerY);
                txtScannerActualPosition.Text = $"{scannerX:F3}, {scannerY:F3}";
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (null == this.Marker)
                return;
            if (null == this.Marker.MarkerArg)
                return;
            if (this.Marker.IsBusy)
            {
                MessageBox.Show($"Marker is still working on process", "WARNING!!! LASER", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ccbDownload.Checked)
            {
                var arg = new MarkerArgDefault(this.Document, this.Rtc, this.Laser, this.MotorZ);
                arg.ViewTargets.Add(this.Editor.View);
                if (null != this.Viewer)
                    arg.ViewTargets.Add(this.Viewer.View);
                this.Marker.Ready(arg);
            }
            //오프셋 배열 적용
            if (0 == this.offsets.Count)
                this.offsets.Add(Offset.Zero);
            this.Marker.MarkerArg.Offsets = this.offsets.ToList();
            switch((MarkTargets)cbbMarkTarget.SelectedIndex)
            {
                default:
                    this.Marker.MarkerArg.MarkTargets = MarkTargets.All;
                    break;
                case MarkTargets.Selected:
                    this.Marker.MarkerArg.MarkTargets = MarkTargets.Selected;
                    break;              
            }
            var laser = this.Laser;
            if (!ccbDisableWarnDlgInOp.Checked)
                if (DialogResult.Yes != MessageBox.Show($"Do you really want to start ?{Environment.NewLine}Target: { this.Marker.MarkerArg.MarkTargets.ToString()}", "WARNING!!! LASER", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    return;

            this.Marker.MarkerArg.IsJumpToOriginAfterFinished = chbAutoJumpToOrigin.Checked;
            var syncAxis = Rtc as IRtcSyncAxis;
            if (null != syncAxis)
                syncAxis.CtlSimulationMode(chbSimulationMode.Checked);
            this.Marker?.Start();
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            this.Marker?.Stop();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.F5))
            {
                btnStop_Click(btnStop, EventArgs.Empty);
                return true;
            }
            else if (keyData == (Keys.F5))
            {
                btnStart_Click(btnStop, EventArgs.Empty);
                return true;
            }
            else if (keyData == (Keys.F6))
            {
                btnReset_Click(btnStop, EventArgs.Empty);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        System.Timers.Timer delayTimer = new System.Timers.Timer();
        private void btnManualOn_Click(object sender, EventArgs e)
        {
            if (null == this.Marker)
                return;
            if (null == this.Marker.MarkerArg)
                return;

            if (!ccbDisableDlgInManual.Checked)
                if (DialogResult.Yes != MessageBox.Show($"Do you really want to manual laser on ?", "WARNING!!! LASER", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    return;
            this.Marker.MarkerArg.Rtc?.CtlFrequency(float.Parse(txtFrequency.Text), float.Parse(txtPulseWidth.Text));
            if (null != this.Laser)
            {
                var powerControl = this.Laser as IPowerControl;
                if (null != powerControl)
                {
                    var powerCategory = cbbPowerCategory.Text;
                    powerControl.CtlPower(float.Parse(txtPower.Text), powerCategory);
                }
            }
            this.Marker.MarkerArg.Rtc?.CtlLaserOn();
            if (chkAutoOff.Checked)
            {
                this.delayTimer = new System.Timers.Timer();
                delayTimer.Interval = (float)nudSecs.Value * 1000;
                delayTimer.Elapsed += DelayTimer_Elapsed;
                delayTimer.Start();
            }
        }
        private void DelayTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Marker.MarkerArg.Rtc?.CtlLaserOff();
            this.delayTimer.Stop();
            this.delayTimer.Dispose();
        }
        private void btnManualOff_Click(object sender, EventArgs e)
        {
            if (null == this.Marker)
                return;
            if (null == this.Marker.MarkerArg)
                return;

            this.Marker.MarkerArg.Rtc?.CtlLaserOff();
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            this.Marker?.Reset();
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //import
            var dlg = new OpenFileDialog();
            dlg.Title = "Import Offsets Data ...";
            dlg.Filter = "txt files(*.txt)|*.txt|dat files (*.dat)|*.dat|All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.offsets.RaiseListChangedEvents = false;
                this.offsets.Clear();
                int counts = 0;
                using (var stream = new StreamReader(dlg.FileName))
                {
                    while (!stream.EndOfStream)
                    {
                        var line = stream.ReadLine();
                        if (string.IsNullOrEmpty(line))
                            continue;

                        if (line.ElementAt(0) == ';')
                            continue;
                        string[] tokens = line.Split(',');
                        float x = float.Parse(tokens[0]);
                        float y = float.Parse(tokens[1]);
                        float angle = float.Parse(tokens[2]);

                        this.offsets.Add(new Offset(x, y, angle));
                        counts++;
                    }
                    MessageBox.Show($"Offsets data : {counts} has read from {dlg.FileName}", "Import", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                this.offsets.RaiseListChangedEvents = true;
                this.offsets.ResetBindings();
            }
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //export
            var dlg = new SaveFileDialog();
            dlg.Title = "Export Offsets Data ...";
            dlg.Filter = "txt files(*.txt)|*.txt|dat files (*.dat)|*.dat|All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                using (var stream = new StreamWriter(dlg.FileName))
                {
                    stream.WriteLine($"; 2020 copyright to spirallab.sirius");
                    stream.WriteLine($"; data format : x, y, angle");
                    foreach (var o in offsets)
                    {
                        stream.WriteLine($"{o.X:F3}, {o.Y:F3}, {o.Angle:F3}");
                    }
                    MessageBox.Show($"Offsets data : {offsets.Count} has written to {dlg.FileName}", "Export", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        private void addRowColArrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ArrayForm();
            if (DialogResult.OK != form.ShowDialog(this))
                return;

            float rowPitch = form.RowPitch;
            float colPitch = form.ColPitch;
            int rows = form.Rows;
            int cols = form.Cols;
            bool zigZag = form.IsZigZag;
            float baseX = form.BaseX;
            float baseY = form.BaseY;

            this.offsets.RaiseListChangedEvents = false;
            float x = 0;
            float y = 0;
            for (int row = 0; row < rows; row++)
            {
                x = 0;
                y = row * rowPitch;
                if (zigZag == true && row % 2 == 1)
                {
                    for (int col = cols - 1; col >= 0; col--)
                    {
                        x = col * colPitch;
                        this.offsets.Add(new Offset(x + baseX, y + baseY));
                    }
                }
                else
                {
                    for (int col = 0; col < cols; col++)
                    {
                        x = col * colPitch;
                        this.offsets.Add(new Offset(x + baseX, y + baseY));
                    }
                }
            }
            this.offsets.RaiseListChangedEvents = true;
            this.offsets.ResetBindings();
        }

        #region drag and drop
        bool isMouseDown = false;
        System.Drawing.Point mouseDownPoint = System.Drawing.Point.Empty;
        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.lsbOffset.SelectedItems == null) return;
            if (1 == this.lsbOffset.SelectedItems.Count)
            {
                LsbOffset_SelectedIndexChanged(lsbOffset, e);
            }
            mouseDownPoint = lsbOffset.PointToClient(new System.Drawing.Point(e.X, e.Y));
            isMouseDown = true;

        }
        private void listBox1_MouseMove(object sender, MouseEventArgs e)
        {
            var mouseCurrent = lsbOffset.PointToClient(new System.Drawing.Point(e.X, e.Y));
            if (isMouseDown)
            {
                double distance = Math.Sqrt(Math.Pow(mouseCurrent.X - mouseDownPoint.X, 2) + Math.Pow(mouseCurrent.Y - mouseDownPoint.Y, 2));
                if (distance > 5)
                    this.lsbOffset.DoDragDrop(this.lsbOffset.SelectedItems, DragDropEffects.Move);
            }
        }
        private void listBox1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            isMouseDown = false;
            System.Drawing.Point point = lsbOffset.PointToClient(new System.Drawing.Point(e.X, e.Y));
            int index = this.lsbOffset.IndexFromPoint(point);
            if (index == 65535)
                return;
            if (index < 0) index = this.lsbOffset.Items.Count - 1;

            this.offsets.RaiseListChangedEvents = false;
            var items = (ListBox.SelectedObjectCollection)e.Data.GetData(typeof(ListBox.SelectedObjectCollection));
            foreach (var item in items)
            {
                var offset = (Offset)item;
                this.offsets.Remove(offset);
                this.offsets.Insert(index, offset);
            }
            this.offsets.RaiseListChangedEvents = true;
            this.offsets.ResetBindings();

            this.lsbOffset.SelectedIndex = index;
        }

        private void listBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }
        #endregion

        private void chbGuide_Click(object sender, EventArgs e)
        {
            var laser = this.Laser;
            if (false == laser.IsGuideControl)
                return;
            if (Marker.IsBusy)
                return;
            var guide = laser as IGuideControl;
            guide.IsGuideOn = chbGuide.Checked;
            if (chbGuide.Checked)
                pnGuide.BackColor = Color.Lime;
            else
                pnGuide.BackColor = Color.Green;
        }

        private void cbbStage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null == this.Rtc)
                return;
            var syncAxis = this.Rtc as IRtcSyncAxis;
            if (null == syncAxis)
                return;
            if (this.Rtc.CtlGetStatus(RtcStatus.Busy))
                return;
           
            syncAxis.CtlSelectStage((Stage)(cbbStage.SelectedIndex + 1),  (CorrectionTableIndex)cbbCorrection.SelectedIndex);
        }
        private void cbbCorrection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null == this.Rtc)
                return;
            var syncAxis = this.Rtc as IRtcSyncAxis;
            if (null == syncAxis)
                return;
            if (this.Rtc.CtlGetStatus(RtcStatus.Busy))
                return;

            syncAxis.CtlSelectStage((Stage)(cbbStage.SelectedIndex + 1), (CorrectionTableIndex)cbbCorrection.SelectedIndex);
        }

        private void btnPosition_Click(object sender, EventArgs e)
        {
            if (null == this.Rtc)
                return;
            var syncAxis = this.Rtc as IRtcSyncAxis;
            if (null == syncAxis)
                return;
            if (this.Rtc.CtlGetStatus(RtcStatus.Busy))
                return;
            if (tacDevice.SelectedTab == tapStage)
            {
                string[] tokens = txtStageCmdPosition.Text.Split(new char[] { ',' });
                if (2 != tokens.Length)
                    return;
                if (float.TryParse(tokens[0], out float x) && float.TryParse(tokens[1], out float y))
                {
                    syncAxis.StageMoveSpeed = float.Parse(txtStageSpeed.Text);
                    syncAxis.StageMoveTimeOut = float.Parse(txtStageTimeOut.Text);
                    if (syncAxis.StageCounts > 1)
                        syncAxis.CtlSelectStage( (Stage)(cbbStage.SelectedIndex+1));
                    syncAxis.CtlSetStagePosition(x, y);
                }
            }
            else
            {
                string[] tokens = txtScannerCmdPosition.Text.Split(new char[] { ',' });
                if (2 != tokens.Length)
                    return;
                if (float.TryParse(tokens[0], out float x) &&
                    float.TryParse(tokens[1], out float y))
                {
                    syncAxis.CtlSetScannerPosition(x, y);
                }
            }
        }

        private void btnUnfollow_Click(object sender, EventArgs e)
        {
            if (null == this.Rtc)
                return;
            var syncAxis = this.Rtc as IRtcSyncAxis;
            if (null == syncAxis)
                return;
            //if (this.Rtc.CtlGetStatus(RtcStatus.Busy))
            //    return;
            syncAxis.CtlMotionMode(MotionMode.Unfollow);

        }

        private void btnFollow_Click(object sender, EventArgs e)
        {
            if (null == this.Rtc)
                return;
            var syncAxis = this.Rtc as IRtcSyncAxis;
            if (null == syncAxis)
                return;
            //if (this.Rtc.CtlGetStatus(RtcStatus.Busy))
            //    return;
            syncAxis.CtlMotionMode(MotionMode.Follow);
        }

        private void lblError_Click(object sender, EventArgs e)
        {
            if (null == this.Rtc)
                return;
            var syncAxis = this.Rtc as IRtcSyncAxis;
            if (null == syncAxis)
                return;

            syncAxis.CtlGetInternalErrMsg(out List<(ulong, string)> errors);
            if (errors.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < errors.Count; i++)
                {
                    sb.AppendLine($"[{errors[i].Item1}] : {errors[i].Item2}");
                }
                MessageBox.Show(this, sb.ToString(), "Internal SyncAXIS Error(s)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        private void btnOpenSimulationFile_Click(object sender, EventArgs e)
        {
            var syncAxis = this.Rtc as IRtcSyncAxis;
            if (null == syncAxis)
                return;

            var simulatedFileName = syncAxis.SimulationFileName;
            if (!File.Exists(Config.ConfigSyncAxisViewerFileName))
            {
                MessageBox.Show(this, "syncAxis viewer is not founded", "SyncAxis Viewer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
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
                Cursor.Current = Cursors.WaitCursor; 
                var proc = Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Logger.Log(Logger.Type.Error, ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnTrajectory_Click(object sender, EventArgs e)
        {
            var syncAxis = this.Rtc as IRtcSyncAxis;
            if (null == syncAxis)
                return;
            var trajectory = ppgTrajectory.SelectedObject as Trajectory;
            if (null == trajectory)
                return;
            syncAxis.Trajectory = trajectory;
            ppgTrajectory.SelectedObject = syncAxis.Trajectory;
            ppgTrajectory.Refresh();
        }

        private void btnDynamics_Click(object sender, EventArgs e)
        {
            var syncAxis = this.Rtc as IRtcSyncAxis;
            if (null == syncAxis)
                return;
            var dynamics = ppgDynamics.SelectedObject as Dynamics;
            if (null == dynamics)
                return;
            syncAxis.Dynamics = dynamics;
            ppgDynamics.SelectedObject = syncAxis.Dynamics;
            ppgDynamics.Refresh();
        }

        private void btnHead1_Click(object sender, EventArgs e)
        {
            var syncAxis = this.Rtc as IRtcSyncAxis;
            if (null == syncAxis)
                return;

            string[] tokens = txtHead1Offset.Text.Split(new char[] { ',' });
            if (3 != tokens.Length)
                return;
            float dx = float.Parse(tokens[0]);
            float dy = float.Parse(tokens[1]);
            float angle = float.Parse(tokens[2]);
            syncAxis.CtlHeadOffset(ScanDevice.ScanDevice1, new Vector2(dx, dy), angle);
        }

        private void btnHead2_Click(object sender, EventArgs e)
        {
            var syncAxis = this.Rtc as IRtcSyncAxis;
            if (null == syncAxis)
                return;

            string[] tokens = txtHead2Offset.Text.Split(new char[] { ',' });
            if (3 != tokens.Length)
                return;
            float dx = float.Parse(tokens[0]);
            float dy = float.Parse(tokens[1]);
            float angle = float.Parse(tokens[2]);
            syncAxis.CtlHeadOffset(ScanDevice.ScanDevice2, new Vector2(dx, dy), angle);
        }

        private void btnHead3_Click(object sender, EventArgs e)
        {
            var syncAxis = this.Rtc as IRtcSyncAxis;
            if (null == syncAxis)
                return;
            
            string[] tokens = txtHead3Offset.Text.Split(new char[] { ',' });
            if (3 != tokens.Length)
                return;
            float dx = float.Parse(tokens[0]);
            float dy = float.Parse(tokens[1]);
            float angle = float.Parse(tokens[2]);
            syncAxis.CtlHeadOffset(ScanDevice.ScanDevice3, new Vector2(dx, dy), angle);
        }

        private void btnHead4_Click(object sender, EventArgs e)
        {
            var syncAxis = this.Rtc as IRtcSyncAxis;
            if (null == syncAxis)
                return;

            string[] tokens = txtHead4Offset.Text.Split(new char[] { ',' });
            if (3 != tokens.Length)
                return;
            float dx = float.Parse(tokens[0]);
            float dy = float.Parse(tokens[1]);
            float angle = float.Parse(tokens[2]);
            syncAxis.CtlHeadOffset(ScanDevice.ScanDevice4, new Vector2(dx, dy), angle);
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            var syncAxis = Rtc as IRtcSyncAxis;
            if (null == syncAxis)
                return;
            if (e.TabPage == tabOffset)
            {
                txtHead1Offset.Text = $"{syncAxis.Head1Offset.X:F3}, {syncAxis.Head1Offset.Y:F3}, {syncAxis.Head1Offset.Z:F3}";
                txtHead2Offset.Text = $"{syncAxis.Head2Offset.X:F3}, {syncAxis.Head2Offset.Y:F3}, {syncAxis.Head2Offset.Z:F3}";
                txtHead3Offset.Text = $"{syncAxis.Head3Offset.X:F3}, {syncAxis.Head3Offset.Y:F3}, {syncAxis.Head3Offset.Z:F3}";
                txtHead4Offset.Text = $"{syncAxis.Head4Offset.X:F3}, {syncAxis.Head4Offset.Y:F3}, {syncAxis.Head4Offset.Z:F3}";
            }
        }
    }
}