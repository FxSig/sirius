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
    public partial class FormEditor : Form
    {
        public SiriusEditorForm Editor
        {
            get
            {
                return this.siriusEditorForm1;
            }
        }

        public FormEditor()
        {
            InitializeComponent();
        }


    }
}
