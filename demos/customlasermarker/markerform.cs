using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    public partial class YourMarkerForm : System.Windows.Forms.Form
    {
        IMarker marker;
        public YourMarkerForm(YourCustomMarker marker)
        {
            InitializeComponent();

            Debug.Assert(marker != null);
            this.marker = marker;
            this.marker = marker;
            this.marker.OnProgress += Marker_OnProgress; 
            this.marker.OnFinished += Marker_OnFinished;

            this.KeyPreview = true;
            this.KeyDown += YourLaserForm_KeyDown; ;
        }

        private void YourLaserForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { this.Close(); }
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

        private void YourLaserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.timer1.Enabled = false;
            this.marker.OnProgress -= Marker_OnProgress;
            this.marker.OnFinished -= Marker_OnFinished;
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
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.marker.Reset();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes != MessageBox.Show($"Do you really want to start ?", "WARNING!!! LASER", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            if (0 == this.marker.Offsets.Count)
                this.marker.Offsets.Add(Offset.Zero);///기본 오프셋 추가
            listBox1.Items.Add($"{DateTime.Now.ToString()} : trying to start ...");
            marker?.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add($"{DateTime.Now.ToString()} : trying to stop ...");
            marker?.Stop();
        }

        private void btnManualOn_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes != MessageBox.Show($"Do you really want to manual laser on ?", "WARNING!!! LASER", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            this.marker.Rtc.CtlLaserOn();
        }

        private void btnManualOff_Click(object sender, EventArgs e)
        {
            this.marker.Rtc.CtlLaserOff();
        }

        private void YourMarkerForm_VisibleChanged(object sender, EventArgs e)
        {
            this.timer1.Enabled = this.Visible;
        }
    }
}
