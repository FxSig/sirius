using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //XY 엔코더 기반
            Application.Run((Form)new Sirius.MainFormXY());

            //회전 엔코더 기반
            //Application.Run((Form)new Sirius.MainFormAngular());
        }
    }
}
