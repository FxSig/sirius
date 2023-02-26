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
 * custom marker form
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    /// <summary>
    /// 커스텀 마커 폼
    /// </summary>
    public partial class CustomMarkerForm : Form
    {
        /// <summary>
        /// 상태바에 출력되는 이름 (별명)
        /// </summary>
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
                    arg.ViewTargets.Add(Editor.View);
                    if (null != Viewer)
                        arg.ViewTargets.Add(Viewer.View);

                    if (this.Marker!= null && this.Marker.MarkerArg != null)
                    {
                        arg.RtcListType = this.Marker.MarkerArg.RtcListType;
                        arg.IsGuided = this.Marker.MarkerArg.IsGuided;
                        arg.IsExternalStart = this.Marker.MarkerArg.IsExternalStart;
                        arg.IsJumpToOriginAfterFinished = this.Marker.MarkerArg.IsJumpToOriginAfterFinished;
                        arg.IsEnablePens = this.Marker.MarkerArg.IsEnablePens;
                        arg.IsVerifyScannerPowerFault = this.Marker.MarkerArg.IsVerifyScannerPowerFault;
                        arg.IsRegisteringFonts = this.Marker.MarkerArg.IsRegisteringFonts;
                        arg.IsMeasurementToPolt = this.Marker.MarkerArg.IsMeasurementToPolt;
                        arg.MeasurementPlotProgram = this.Marker.MarkerArg.MeasurementPlotProgram;
                    }
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
        public CustomMarkerForm()
        {
            InitializeComponent();

            this.ppgOffset.PropertySort = PropertySort.Categorized;

            listBox1.DrawMode = DrawMode.OwnerDrawVariable;
            listBox1.DataSource = this.offsets;
            listBox1.SelectedIndexChanged += LsbOffset_SelectedIndexChanged;
            listBox1.MeasureItem += LsbOffset_MeasureItem;
            listBox1.DrawItem += LsbOffset_DrawItem;

            btnAdd.Click += BtnAdd_Click;
            btnRemove.Click += BtnRemove_Click;
            btnClear.Click += BtnClear_Click;
            btnUp.Click += BtnUp_Click;
            btnDown.Click += BtnDown_Click;
            ppgOffset.PropertyValueChanged += PpgOffset_PropertyValueChanged;
            cbbMarkTarget.SelectedIndex = 0;
            cbbListType.SelectedIndex = 1;

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
                    grbGuideBeamMode.Enabled = guide.IsGuideOn;
                }
            }

            if (null != this.Marker && null != this.Marker.MarkerArg)
            {
                this.chkExternalStart.Checked = this.Marker.MarkerArg.IsExternalStart;
                if (this.Marker.MarkerArg.IsExternalStart)
                {
                    this.Marker.MarkerArg.RtcListType = ListType.Single;
                    cbbListType.SelectedIndex = 0;//single
                }

                this.chbMeasurementPlot.Checked = this.Marker.MarkerArg.IsMeasurementToPolt;
            }

            //motf 탭 활성화 여부
            bool isMotfEnabled = false;
            if (null != this.Rtc)
                isMotfEnabled = this.Rtc.IsMOTF;
            ((Control)this.tapMotf).Enabled = isMotfEnabled;

            if (isMotfEnabled)
            {
                var rtcMOTF = this.Rtc as IRtcMOTF;
                if (null != rtcMOTF)
                {
                    txtEncXSpeed.Text = $"{rtcMOTF.EncXSimulatedSpeed:F3}";
                    txtEncYSpeed.Text = $"{rtcMOTF.EncYSimulatedSpeed:F3}";
                    txtEncAngularSpeed.Text = $"{rtcMOTF.EncSimulatedAngularSpeed:F3}";
                    txtAngularCenter.Text = $"{rtcMOTF.MotfAngularCenter.X:F3}, {rtcMOTF.MotfAngularCenter.Y:F3}"; 
                }
            }

            cbbPowerCategory.Items.Clear();
            if (null != this.PowerMap)
            {
                this.PowerMap.Categories(out string[] categories);
                cbbPowerCategory.Items.AddRange(categories);
            }

            this.listBox1.AllowDrop = true;
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
                    e.Graphics.DrawString($"{e.Index + 1}: {item.ToString()}", drawFontBold, SystemBrushes.HighlightText, e.Bounds.Left, e.Bounds.Top);
            }
            else
            {
                using (SolidBrush br = new SolidBrush(e.ForeColor))
                    e.Graphics.DrawString($"{e.Index+1}: {item.ToString()}", listBox1.Font, br, e.Bounds.Left, e.Bounds.Top);
            }
            e.DrawFocusRectangle();
        }
        private void LsbOffset_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            ListBox lst = sender as ListBox;
            var item = (Offset)lst.Items[e.Index];
            SizeF txt_size = e.Graphics.MeasureString($"{e.Index+1}: {item.ToString()}", listBox1.Font);
            e.ItemHeight = (int)txt_size.Height + 2;
            e.ItemWidth = (int)txt_size.Width;
        }
        private void LsbOffset_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = listBox1.SelectedItem;
            this.ppgOffset.SelectedObject = item;
            if (null != item)
            {
                var o = (Offset)item;
            }
        }
        private void BtnUp_Click(object sender, EventArgs e)
        {
            //up
            int index = listBox1.SelectedIndex;
            if (index <= 0)
                return;
            this.MoveItem(index, index - 1);
            listBox1.SelectedIndex = index - 1;
        }
        private void BtnDown_Click(object sender, EventArgs e)
        {
            //down
            int index = listBox1.SelectedIndex;
            if (index < 0 || index == listBox1.Items.Count - 1)
                return;
            this.MoveItem(index, index + 1);
            listBox1.SelectedIndex = index + 1;
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

            int index = listBox1.SelectedIndex;
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
            var item = listBox1.SelectedItem;
            this.ppgOffset.SelectedObject = item;

        }
        private void BtnRemove_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            if (index >= 0)
            {
                this.offsets.RemoveAt(index);
            }
            var item = listBox1.SelectedItem;
            this.ppgOffset.SelectedObject = item;
        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            this.offsets.Clear();
            this.ppgOffset.SelectedObject = null;
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
                if (DialogResult.Yes != MessageBox.Show($"Do you really want to abort the laser emission ?", $"Warning!!! [{this.Marker?.Name}]", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
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

                btnSimulationSet.Enabled = false;
                btnSimulateDisable.Enabled = false;
            }
            else
            {
                lblBusy.ForeColor = Color.White;
                lblBusy.BackColor = Color.Maroon;

                btnStop.BackColor = Color.WhiteSmoke;
                btnStop.ForeColor = SystemColors.ControlText;
                tick = 0;

                btnSimulationSet.Enabled = true;
                btnSimulateDisable.Enabled = true;
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

            if (null != this.Rtc)
            {
                var rtcMOTF = this.Rtc as IRtcMOTF;
                if (null == rtcMOTF)
                    return;
                rtcMOTF.CtlMotfGetEncoder(out int encX, out int encY, out float encXmm, out float encYmm);
                txtEncX.Text = $"{encX}";
                txtEncY.Text = $"{encY}";
                txtEncXmm.Text = $"{encXmm:F3}";
                txtEncYmm.Text = $"{encYmm:F3}";

                rtcMOTF.CtlMotfGetAngularEncoder(out int enc, out float angle);
                txtEncAngular.Text = $"{enc}";
                txtEncAngle.Text = $"{angle:F3}";
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
                Logger.Log(Logger.Type.Warn, $"marker is still working on progress ...");
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
                    this.Marker.MarkerArg.RepeatSelected = uint.Parse(nudRepeat.Value.ToString());
                    this.Marker.MarkerArg.SpeedSelected = float.Parse(txtOverrideSpeed.Text);
                    break;
                case MarkTargets.SelectedButBoundRect:
                    this.Marker.MarkerArg.MarkTargets = MarkTargets.SelectedButBoundRect;
                    this.Marker.MarkerArg.RepeatSelected = uint.Parse(nudRepeat.Value.ToString());
                    this.Marker.MarkerArg.SpeedSelected = float.Parse(txtOverrideSpeed.Text);
                    break;
                case MarkTargets.Custom:
                    this.Marker.MarkerArg.MarkTargets = MarkTargets.Custom;
                    //do something if you want
                    break;
            }
            var laser = this.Laser;
            if (laser.IsGuideControl)
            {
                var guide = laser as IGuideControl;
                this.Marker.MarkerArg.IsGuided = guide.IsGuideOn;
            }

            this.Marker.MarkerArg.RtcListType = (ListType)cbbListType.SelectedIndex;
            this.Marker.MarkerArg.IsMeasurementToPolt = chbMeasurementPlot.Checked;
            this.Marker.MarkerArg.IsVerifyScannerPowerFault = chbVerifyPowerAckStatus.Checked;

            if (!ccbDisableWarnDlgInOp.Checked)
                if (DialogResult.Yes != MessageBox.Show($"Do you really want to start laser emission ?", $"Warning!!! Target: {this.Marker.MarkerArg.MarkTargets.ToString()} [{this.Marker?.Name}]", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    return;

            this.Marker.MarkerArg.IsJumpToOriginAfterFinished = chbAutoJumpToOrigin.Checked;
            this.Marker.MarkerArg.IsExternalStart = this.chkExternalStart.Checked;
            if (this.Marker.MarkerArg.IsExternalStart)
            {
                this.Marker.MarkerArg.RtcListType = ListType.Single;
                cbbListType.SelectedIndex = 0;//single
            }

            this.Marker.Start();

            if (this.chkExternalStart.Checked)
            {
                new Thread(new ThreadStart(delegate
                {
                    MessageBox.Show($"Waiting for external /start signal...", $"External Start [{this.Marker?.Name}]", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    var markerDefault = this.Marker as MarkerDefault;
                    markerDefault?.ExternalStop();
                })).Start();
            }
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
                if (DialogResult.Yes != MessageBox.Show($"Do you really want to start laser emiision by manually ?", $"Warning!!! Laser [{this.Marker?.Name}]", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
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
                    MessageBox.Show($"Offsets data : {counts} has read from {dlg.FileName}", $"Import [{this.Marker?.Name}]", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    stream.WriteLine($"; 2023 copyright to spirallab.sirius");
                    stream.WriteLine($"; data format : x, y, angle");
                    foreach (var o in offsets)
                    {
                        stream.WriteLine($"{o.X:F3}, {o.Y:F3}, {o.Angle:F3}");
                    }
                    MessageBox.Show($"Offsets data : {offsets.Count} has written to {dlg.FileName}", $"Export [{this.Marker?.Name}]", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
        private void chkExternalStart_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExternalStart.Checked)
            {
                cbbListType.SelectedIndex = 0;//single
                this.Marker.MarkerArg.RtcListType = ListType.Single;
                //Editor.Marker.MarkerArg.IsExternalStart = true;
            }
            else
            {
                cbbListType.SelectedIndex = 1;//auto
                this.Marker.MarkerArg.RtcListType = ListType.Auto;
                //Editor.Marker.MarkerArg.IsExternalStart = false;
            }
        }

    
        private void btnScannerPosition_Click(object sender, EventArgs e)
        {
            if (null == this.Rtc)
                return;
            if (this.Rtc.CtlGetStatus(RtcStatus.Busy))
                return;

            string[] tokens = txtScannerPos.Text.Split(new char[] { ',' });
            if (2 != tokens.Length)
                return;
            if (float.TryParse(tokens[0], out float x) &&
                float.TryParse(tokens[1], out float y))
            {
                this.Rtc.CtlMove(x, y);
            }
        }
        private void btnEncReset_Click(object sender, EventArgs e)
        {
            if (null == this.Rtc)
                return;
            var rtcMOTF = this.Rtc as IRtcMOTF;
            if (null == rtcMOTF)
                return;
            if (DialogResult.Yes != MessageBox.Show($"Do you really want to reset encoder counts ?", $"MOTF Encoder [{this.Marker?.Name}]", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;
            rtcMOTF.CtlMotfEncoderReset();
        }


        #region drag and drop
        bool isMouseDown = false;
        System.Drawing.Point mouseDownPoint = System.Drawing.Point.Empty;
        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.listBox1.SelectedItems == null) return;
            if (1 == this.listBox1.SelectedItems.Count)
            {
                LsbOffset_SelectedIndexChanged(listBox1, e);
            }
            mouseDownPoint = listBox1.PointToClient(new System.Drawing.Point(e.X, e.Y));
            isMouseDown = true;

        }
        private void listBox1_MouseMove(object sender, MouseEventArgs e)
        {
            var mouseCurrent = listBox1.PointToClient(new System.Drawing.Point(e.X, e.Y));
            if (isMouseDown)
            {
                double distance = Math.Sqrt(Math.Pow(mouseCurrent.X - mouseDownPoint.X, 2) + Math.Pow(mouseCurrent.Y - mouseDownPoint.Y, 2));
                if (distance > 5)
                    this.listBox1.DoDragDrop(this.listBox1.SelectedItems, DragDropEffects.Move);
            }
        }
        private void listBox1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            isMouseDown = false;
            System.Drawing.Point point = listBox1.PointToClient(new System.Drawing.Point(e.X, e.Y));
            int index = this.listBox1.IndexFromPoint(point);
            if (index == 65535)
                return;
            if (index < 0) index = this.listBox1.Items.Count - 1;

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

            this.listBox1.SelectedIndex = index;
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
            grbGuideBeamMode.Enabled = chbGuide.Checked;
            if (chbGuide.Checked)
                pnGuide.BackColor = Color.Lime;
            else
                pnGuide.BackColor = Color.Green;
        }

        private void btnSimulationDisable_Click(object sender, EventArgs e)
        {
            if (null == this.Rtc)
                return;
            var rtcMOTF = this.Rtc as IRtcMOTF;
            if (null == rtcMOTF)
                return;
            rtcMOTF.CtlMotfEncoderSpeed(0, 0);
        }

        private void btnSimulationSet_Click(object sender, EventArgs e)
        {
            if (null == this.Rtc)
                return;
            var rtcMOTF = this.Rtc as IRtcMOTF;
            if (null == rtcMOTF)
                return;
            float xSpeed = float.Parse(txtEncXSpeed.Text);
            float ySpeed = float.Parse(txtEncYSpeed.Text);
            if (DialogResult.Yes != MessageBox.Show($"Do you really want to set encoder simulation speed by x={xSpeed}, y={ySpeed} mm/s ?", $"MOTF Encoder Simulation Speed [{this.Marker?.Name}]", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            rtcMOTF.CtlMotfEncoderSpeed(xSpeed, ySpeed);
        }

        private void btnSimulationAngularSet_Click(object sender, EventArgs e)
        {
            if (null == this.Rtc)
                return;
            var rtcMOTF = this.Rtc as IRtcMOTF;
            if (null == rtcMOTF)
                return;
            float speed = float.Parse(txtEncAngularSpeed.Text);
            if (DialogResult.Yes != MessageBox.Show($"Do you really want to set encoder simulation speed by angle= {speed} ˚/s ?", $"MOTF Encoder Simulation Speed [{this.Marker?.Name}]", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;
            rtcMOTF.CtlMotfEncoderAngularSpeed(speed);
        }

        private void btnSimulateAngularDisable_Click(object sender, EventArgs e)
        {
            if (null == this.Rtc)
                return;
            var rtcMOTF = this.Rtc as IRtcMOTF;
            if (null == rtcMOTF)
                return;
            rtcMOTF.CtlMotfEncoderAngularSpeed(0);
        }

        private void btnAngularCenter_Click(object sender, EventArgs e)
        {
            if (null == this.Rtc)
                return;
            var rtcMOTF = this.Rtc as IRtcMOTF;
            if (null == rtcMOTF)
                return;
            string[] tokens = txtAngularCenter.Text.Split(new char[] { ',' });
            if (2 != tokens.Length)
                return;
            if (float.TryParse(tokens[0], out float x) &&
                float.TryParse(tokens[1], out float y))
            {
                if (DialogResult.Yes != MessageBox.Show($"Do you really want to set rotate center to {txtAngularCenter.Text} ?", $"MOTF Rotate Center [{this.Marker?.Name}]", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    return;
                rtcMOTF.MotfAngularCenter = new Vector2(x, y);
            }
        }

    }
}