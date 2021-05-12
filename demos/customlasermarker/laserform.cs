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
    public partial class LaserForm : UserControl
    {
        ILaser laser;
        public LaserForm(YourCustomLaser laser)
        {
            InitializeComponent();
            this.laser = laser;
        }
    }
}
