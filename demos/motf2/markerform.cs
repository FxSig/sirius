using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    public partial class MotfMarkerForm : Form
    {
        public IMarker Marker
        {
            get { return this.marker; }
            set
            {
                if (null == value)
                {
                    this.marker.OnProgress -= Marker_OnProgress;
                    this.marker.OnFinished -= Marker_OnFinished;
                    this.timer1.Enabled = false;
                }
                else
                {
                    this.marker = value;
                    this.marker.OnProgress += Marker_OnProgress;
                    this.marker.OnFinished += Marker_OnFinished;
                    this.timer1.Enabled = true;
                }
            }
        }
        private IMarker marker;

        public MotfMarkerForm(IMarker marker)
        {
            InitializeComponent();
            this.Marker = marker;
        }

        private void Marker_OnFinished(IMarker sender, TimeSpan span)
        {
            if (listBox1.InvokeRequired)
            {
                listBox1.BeginInvoke(new MethodInvoker(delegate ()
                {
                    listBox1.Items.Add($"{DateTime.Now.ToString()} : finished. {span.TotalSeconds:F3} sec");
                }));
            }
            else
            {
                listBox1.Items.Add($"{DateTime.Now.ToString()} : finished. {span.TotalSeconds:F3} sec");
            }
        }

        private void Marker_OnProgress(IMarker sender, float progress)
        {
            if (statusStrip1.InvokeRequired)
            {
                statusStrip1.BeginInvoke(new MethodInvoker(delegate ()
                {
                    pgbProgress.Value = (int)progress;
                }));
            }
            else
            {
                pgbProgress.Value = (int)progress;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.marker.IsReady)
                panReady.BackColor = Color.Lime;
            else
                panReady.BackColor = Color.Green;

            if (this.marker.IsBusy)
                panBusy.BackColor = Color.Red;
            else
                panBusy.BackColor = Color.Maroon;

            if (this.marker.IsError)
                panError.BackColor = Color.Red;
            else
                panError.BackColor = Color.Maroon;

            if (null != marker.MarkerArg)
            {
                var rtcMOTF = marker.MarkerArg.Rtc as IRtcMOTF;
                if (null == rtcMOTF)
                    return;
                rtcMOTF.CtlGetEncoder(out int encX, out int encY, out float encXmm, out float encYmm);
                float distance = float.Parse(txtDistance.Text);
                if (Math.Abs(encXmm) >= Math.Abs(distance))
                {
                    //do what you want
                }
            }

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.marker.Reset();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes != MessageBox.Show($"Do you really want to start ?", "WARNING!!! LASER", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            listBox1.Items.Add($"{DateTime.Now.ToString()} : trying to start ...");            
            this.marker.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add($"{DateTime.Now.ToString()} : trying to stop ...");
            this.marker?.Stop();
        }

        private void YourMarkerForm_VisibleChanged(object sender, EventArgs e)
        {
            this.timer1.Enabled = this.Visible;
        }

        private void manualOnToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes != MessageBox.Show($"Do you really want to manual laser on ?", "WARNING!!! LASER", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            if (null != marker.MarkerArg)
            {
                this.marker.MarkerArg.Rtc.CtlMove(0, 0);
                Thread.Sleep(100);
                this.marker.MarkerArg.Rtc.CtlLaserOn();
            }
        }

        private void manualOffToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (null != marker.MarkerArg)
            {
                this.marker.MarkerArg.Rtc.CtlLaserOff();
                this.marker.MarkerArg.Rtc.CtlMove(0, 0);
            }
        }

        private void chbExternalTrigger_CheckedChanged(object sender, EventArgs e)
        {
            var motfMarker = this.marker as MotfMarker;
            if (null != motfMarker)
            {
                motfMarker.IsExternalTrigger = this.chbExternalTrigger.Checked;
            }
        }
    }
}
