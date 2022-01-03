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
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // initialize spirallab core engine 
            SpiralLab.Core.Initialize();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Application.Run(new LaserControlForm());
            }
            catch (Exception ex)
            {
                Logger.Log(Logger.Type.Error, ex);
                MessageBox.Show(ex.Message, "Exception - (c)SpiralLAB", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
    }
}
