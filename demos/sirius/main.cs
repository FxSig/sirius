/* 
 * 
 *   _____ ______   _____ ______   ___     
 *  |\   _ \  _   \|\   _ \  _   \|\  \    
 *  \ \  \\\__\ \  \ \  \\\__\ \  \ \  \   
 *   \ \  \\|__| \  \ \  \\|__| \  \ \  \  
 *    \ \  \    \ \  \ \  \    \ \  \ \  \ 
 *     \ \__\    \ \__\ \__\    \ \__\ \__\
 *      \|__|     \|__|\|__|     \|__|\|__|
 *                                     
 * 
 */

using Singularity.Mmi.winforms;
using Singularity.Shared;
using System;
using System.Windows.Forms;

namespace Singularity.Mmi
{
    public class Program
    {
        public static IServiceHandlerEx SvcHandler;
        public static IServiceAuxiliaryEx SvcAux;
        public static IServiceVisionEx SvcVision;

        public static Singularity.Handler.SequenceHandler SeqHandler;
        public static Vision.SequenceVision SeqVision;
        public static FormMain MainForm;

        public static bool Initialize()
        {           
            bool success = true;

            #region 시퀀스 초기화
            success &= Handler.Program.Initialize();
            success &= Vision.Program.Initialize();
            //success &= Aux.Program.Initialize();
            #endregion

            SeqHandler = Handler.Program.SequenceHandler as Handler.SequenceHandler;
            SeqVision = Vision.Program.SequenceVision as Vision.SequenceVision;
            return success;
        }

        [STAThread]
        static void Main(string[] args)
        {
            /// winforms
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            /// 엔진 초기화
            Core.Core.Initialize();

            /// 콘솔창 이벤트 핸들러 등록
            //NativeMethods.SetConsoleCtrlHandler(ConsoleCtrlCheck, true);

            Initialize();

            ///쌍방간 서비스 연결
            ///                              Aux Service
            ///                                   |
            ///                                   |
            /// Vision Service  <------->  Handler Service  <-------->  Laser Service
            //Handler.Program.SequenceHandler.ServiceAuxiliary = Aux.Program.SequenceAuxiliary.Service;
            Handler.Program.SequenceHandler.ServiceVision = Vision.Program.SequenceVision.Service;
            Vision.Program.SequenceVision.ServiceHandler = Handler.Program.SequenceHandler.Service;


            /// service 캐스트하여 준비
            SvcHandler = Handler.Program.SequenceHandler.Service as IServiceHandlerEx;
            SvcAux = Handler.Program.SequenceHandler.ServiceAuxiliary as IServiceAuxiliaryEx;
            SvcVision = Handler.Program.SequenceHandler.ServiceVision as IServiceVisionEx;

            /// Winforms window
            //Application.Run(new FormMain());

            //Application.Run(new FurexTestForm());
            Program.MainForm = new FormMain();
            Application.Run(Program.MainForm);

            /// WPF window
            //var app = new AppMain();
            //app.Run();

            Handler.Program.SequenceHandler?.Dispose();
            Vision.Program.SequenceVision?.Dispose();
            //Aux.Program.SequenceAuxiliary?.Dispose();
        }
    }

}
