using SpiralLab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralLab.Sirius.FCEU
{
    /// <summary>
    /// 비전 통신용 TCP Client
    /// </summary>
    public class VisionTcpClient : IDisposable
    {
        bool terminated;
        Thread thread;
        TcpClient client;
        string ipaddress;
        int port;
        FormMain formMain;
        bool disposed = false;

        public VisionTcpClient(FormMain formMain)
        {
            this.formMain = formMain;
        }
        ~VisionTcpClient()
        {
            if (this.disposed)
                return;
            this.Dispose(false);
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
                return;
            if (disposing)
            {
                this.Stop();
            }
            this.disposed = true;
        }

        public bool Start(string ipaddress, int port)
        {
            this.Stop();

            this.ipaddress = ipaddress;
            this.port = port;
            this.client = new TcpClient(this.ipaddress, this.port);

            this.thread = new Thread(this.WorkerThread);
            this.thread.Name = $"Vision Tcp Client";
            this.thread.Start();
            return true;
        }

        public bool Stop()
        {
            this.client?.Dispose();
            this.terminated = true;
            this.thread?.Join();
            return true;
        }

        private void WorkerThread()
        {
            while (!this.terminated)
            {
                try
                {


                    Thread.Sleep(5);
                }
                catch (Exception ex)
                {
                    if (!this.terminated)
                        Logger.Log(Logger.Type.Error, ex);
                }
            }
        }

    }
}
