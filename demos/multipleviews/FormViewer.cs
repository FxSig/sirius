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
        /// <summary>
        /// 뷰어 1번
        /// </summary>
        public SiriusViewerForm Viewer1
        {
            get { return this.siriusViewerForm1; }
        }
        /// <summary>
        /// 뷰어 2번
        /// </summary>
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
