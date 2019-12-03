using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    public partial class FormViewer : Form
    {
        public SiriusViewerForm Viewer1
        {
            get { return this.siriusViewerForm1; }
        }
        public SiriusViewerForm Viewer2
        {
            get { return this.siriusViewerForm2; }
        }

        public FormViewer()
        {
            InitializeComponent();
        }
    }
}
