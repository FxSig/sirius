using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace SpiralLab.Sirius.FCEU
{
    public partial class FormAuto : Form
    {
        public SiriusViewerForm SiriusViewer 
        { 
            get
            {
                return this.siriusViewerForm1;
            } 
        }
        public FormAuto()
        {
            InitializeComponent();

        }


    }
}