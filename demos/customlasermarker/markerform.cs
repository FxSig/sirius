using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    public partial class YourMarkerForm : System.Windows.Forms.UserControl
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

        public YourMarkerForm()
        {
            InitializeComponent();
        }
        private void Marker_OnFinished(IMarker sender, IMarkerArg arg)
        {
            var span = arg.EndTime - arg.StartTime;
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
        private void Marker_OnProgress(IMarker sender, IMarkerArg arg)
        {
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

            if (null != marker.MarkerArg)
            {
                this.marker.MarkerArg.Rtc?.CtlMove(0, 0);
                Thread.Sleep(100);
                this.marker.MarkerArg.Rtc?.CtlLaserOn();
            }
        }
        private void btnManualOff_Click(object sender, EventArgs e)
        {
            if (null != marker.MarkerArg)
            {
                this.marker.MarkerArg.Rtc?.CtlLaserOff();
                this.marker.MarkerArg.Rtc?.CtlMove(0, 0);
            }
        }
        private void YourMarkerForm_VisibleChanged(object sender, EventArgs e)
        {
            this.timer1.Enabled = this.Visible;
        }
    }
}
