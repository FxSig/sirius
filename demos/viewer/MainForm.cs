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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            SpiralLab.Core.Initialize();

            var doc = new DocumentDefault();
            siriusViewerForm1.Document = doc;
        }

        public bool Open(string fileName)
        {
            var doc = DocumentSerializer.OpenSirius(fileName);
            siriusViewerForm1.Document = doc;
            return true;
        }
    }
}
