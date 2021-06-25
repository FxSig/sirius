using SpiralLab;
using System;
using System.Collections.Generic;
using System.IO;
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
        public bool IsConnected 
        { 
            get  
            { 
                if (null == this.client)
                    return false;
                return this.client.Connected;  
            }
        }
        bool terminated = false;
        TcpClient client;
        string ipaddress;
        int port;
        SpiralLab.Sirius.FCEU.FormMain formMain;                    
        bool disposed = false;

        public VisionTcpClient(string ipaddress, int port)
        {
            this.formMain = Program.MainForm as SpiralLab.Sirius.FCEU.FormMain;
            this.ipaddress = ipaddress;
            this.port = port;
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
                terminated = true;
                //this.client = null;
                //this.client?.Close();
                //this.client?.Dispose();
            }
            this.disposed = true;
        }

        public async Task<bool> Connect()
        {
            this.client?.Dispose();
            this.client = new TcpClient();
            do
            {
                try
                {
                    Logger.Log(Logger.Type.Warn, $"vision tcp client is not connected");
                    await this.client.ConnectAsync(this.ipaddress, this.port);
                    this.client.NoDelay = true;
                    terminated = false;
                    Logger.Log(Logger.Type.Info, $"vision tcp client connected to {this.ipaddress}:{this.port}");
                    this.Send(MessageProtocol.IM_LASER);
                    do
                    {
                        if (this.Receive(out MessageProtocol resp))
                            this.Parse(resp);
                        else
                        {
                            Logger.Log(Logger.Type.Error, $"vision tcp client fail to receive");
                            break;
                        }
                    } while (!terminated && this.client.Connected);
                }
                catch (Exception ex)
                {
                    //Logger.Log(Logger.Type.Error, ex);
                }
            }
            while (!terminated);
            return true;
        }

        void Parse(MessageProtocol message)
        {
            Logger.Log(Logger.Type.Debug, $"vision comm received : {message.ToString()}");
            bool success = true;
            var seq = this.formMain.Seq;
            int modelChange = (int)message;
            if (modelChange>= (int)MessageProtocol.MODEL_LOAD && modelChange < (int)MessageProtocol.MODEL_LOAD_OK)
            {
                //model change 
                success &= seq.RecipeChange(modelChange);
                if (success)
                    this.Send(MessageProtocol.MODEL_LOAD_OK);
                else
                    this.Send(MessageProtocol.MODEL_LOAD_NG);
            }

            switch(message)
            {
                #region 제어 상태
                case MessageProtocol.LASER_STATUS_READY:
                    if (seq.IsReady)
                        this.Send(MessageProtocol.LASER_STATUS_READY_OK);
                    else
                        this.Send(MessageProtocol.LASER_STATUS_READY_NG);
                    break;
                case MessageProtocol.LASER_STATUS_BUSY:
                    if (seq.IsBusy)
                        this.Send(MessageProtocol.LASER_STATUS_BUSY_OK);
                    else
                        this.Send(MessageProtocol.LASER_STATUS_BUSY_NG);
                    break;
                case MessageProtocol.LASER_STATUS_ERR:
                    if (seq.IsError)
                        this.Send(MessageProtocol.LASER_STATUS_ERR_NG);
                    else
                        this.Send(MessageProtocol.LASER_STATUS_ERR_OK);
                    break; 
                #endregion

                #region 시스템 티칭및 스캐너 보정
                case MessageProtocol.LASER_SCANNER_SYSTEM_TEACH:
                    if (seq.Start(LaserSeq.Process.SystemTeach))
                        this.Send(MessageProtocol.LASER_SCANNER_SYSTEM_TEACH_OK);
                    else
                        this.Send(MessageProtocol.LASER_SCANNER_SYSTEM_TEACH_NG);
                    break;

                case MessageProtocol.LASER_SCANNER_COMPENSATE_3X3:
                    seq.FieldCorrectionRows = 3;
                    seq.FieldCorrectionCols = 3;
                    seq.FieldCorrectionInterval = seq.Fov / 2;
                    if (seq.Start(LaserSeq.Process.FieldCorrection))
                        this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_OK);
                    else
                        this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_NG);
                    break;
                case MessageProtocol.LASER_SCANNER_COMPENSATE_5X5:
                    seq.FieldCorrectionRows = 5;
                    seq.FieldCorrectionCols = 5;
                    seq.FieldCorrectionInterval = seq.Fov / 4;
                    if (seq.Start(LaserSeq.Process.FieldCorrection))
                        this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_OK);
                    else
                        this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_NG);
                    break;
                case MessageProtocol.LASER_SCANNER_COMPENSATE_7X7:
                    seq.FieldCorrectionRows = 7;
                    seq.FieldCorrectionCols = 7;
                    seq.FieldCorrectionInterval = seq.Fov / 6;
                    if (seq.Start(LaserSeq.Process.FieldCorrection))
                        this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_OK);
                    else
                        this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_NG);
                    break;
                case MessageProtocol.LASER_SCANNER_COMPENSATE_9X9:
                    seq.FieldCorrectionRows = 9;
                    seq.FieldCorrectionCols = 9;
                    seq.FieldCorrectionInterval = seq.Fov / 8;
                    if (seq.Start(LaserSeq.Process.FieldCorrection))
                        this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_OK);
                    else
                        this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_NG);
                    break;
                case MessageProtocol.LASER_SCANNER_COMPENSATE_11X11:
                    seq.FieldCorrectionRows = 11;
                    seq.FieldCorrectionCols = 11;
                    seq.FieldCorrectionInterval = seq.Fov / 10;
                    if (seq.Start(LaserSeq.Process.FieldCorrection))
                        this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_OK);
                    else
                        this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_NG);
                    break;
                case MessageProtocol.LASER_SCANNER_COMPENSATE_13X13:
                    seq.FieldCorrectionRows = 13;
                    seq.FieldCorrectionCols = 13;
                    seq.FieldCorrectionInterval = seq.Fov / 12;
                    if (seq.Start(LaserSeq.Process.FieldCorrection))
                        this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_OK);
                    else
                        this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_NG);
                    break;
                case MessageProtocol.LASER_SCANNER_COMPENSATE_15X15:
                    seq.FieldCorrectionRows = 15;
                    seq.FieldCorrectionCols = 15;
                    seq.FieldCorrectionInterval = seq.Fov / 14;
                    if (seq.Start(LaserSeq.Process.FieldCorrection))
                        this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_OK);
                    else
                        this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_NG);
                    break;
                case MessageProtocol.LASER_SCANNER_COMPENSATE_17X17:
                    seq.FieldCorrectionRows = 17;
                    seq.FieldCorrectionCols = 17;
                    seq.FieldCorrectionInterval = seq.Fov / 16;
                    if (seq.Start(LaserSeq.Process.FieldCorrection))
                        this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_OK);
                    else
                        this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_NG);
                    break;

                case MessageProtocol.LASER_SCANNER_COMPENSATE_READ:
                    if (seq.ReadScannerFieldCorrection())
                        this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_READ_OK);
                    else
                        this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_READ_NG);
                    break;
                #endregion

                #region Ref 타켓 마킹
                case MessageProtocol.LASER_SCANNER_REF_01_IMAGE:
                    if (seq.Start(LaserSeq.Process.Ref1))
                        this.Send(MessageProtocol.LASER_SCANNER_REF_01_IMAGE_OK);
                    else
                        this.Send(MessageProtocol.LASER_SCANNER_REF_01_IMAGE_NG);
                    break;

                case MessageProtocol.LASER_SCANNER_REF_02_IMAGE:
                    if (seq.Start(LaserSeq.Process.Ref2))
                        this.Send(MessageProtocol.LASER_SCANNER_REF_02_IMAGE_OK);
                    else
                        this.Send(MessageProtocol.LASER_SCANNER_REF_02_IMAGE_NG);
                    break;
                #endregion

                #region 불량 정보를 1/2 읽어서 에디터에 표시 (도면 기준)
                case MessageProtocol.LASER_READ_INSPECT_01:
                    {
                        if (seq.ReadDefectFromFile("1 file.txt", out var group))
                        {
                            this.Send(MessageProtocol.LASER_READ_INSPECT_01_OK);
                            if (seq.PrepareDefectInEditor(1, group))
                                this.Send(MessageProtocol.LASER_READ_INSPECT_01_FINISH);
                        }
                        else
                            this.Send(MessageProtocol.LASER_READ_INSPECT_01_NG);
                    }
                    break;
                case MessageProtocol.LASER_READ_INSPECT_02:
                    {
                        if (seq.ReadDefectFromFile("2 file.txt", out var group))
                        {
                            this.Send(MessageProtocol.LASER_READ_INSPECT_02_OK);
                            if (seq.PrepareDefectInEditor(2, group))
                                this.Send(MessageProtocol.LASER_READ_INSPECT_02_FINISH);
                        }
                        else
                            this.Send(MessageProtocol.LASER_READ_INSPECT_02_NG);
                    }
                    break;
                #endregion


                #region 불량 정보를 1/2 읽어서 마커에 ready (자재기준)
                case MessageProtocol.LASER_READ_HATCHING_01:
                    {
                        if (seq.ReadDefectFromFile("1 file.txt", out var group))
                        {
                            this.Send(MessageProtocol.LASER_READ_HATCHING_01_OK);
                            if (seq.PrepareDefectInMarker(1, group))
                                this.Send(MessageProtocol.LASER_READ_HATCHING_01_FINISH);
                        }
                        else
                            this.Send(MessageProtocol.LASER_READ_HATCHING_01_NG);
                    }
                    break;
                case MessageProtocol.LASER_READ_HATCHING_02:
                    {
                        if (seq.ReadDefectFromFile("2 file.txt", out var group))
                        {
                            this.Send(MessageProtocol.LASER_READ_HATCHING_02_OK);
                            if (seq.PrepareDefectInMarker(2, group))
                                this.Send(MessageProtocol.LASER_READ_HATCHING_02_FINISH);
                        }
                        else
                            this.Send(MessageProtocol.LASER_READ_HATCHING_02_NG);
                    }
                    break;
                    #endregion
            }
        }

        public bool Send(MessageProtocol data)
        {
            if (!this.client.Connected)
                return false;
            try
            {
                var nstream = client.GetStream();
                using (StreamWriter writer = new StreamWriter(nstream))
                {
                    writer.Write((int)data);
                }
                return true;
                
            }
            catch (Exception ex)
            {
                Logger.Log(Logger.Type.Error, ex);
                return false;
            }
        }
        public bool Receive(out MessageProtocol data)
        {
            data = MessageProtocol.UNKNOWN;
            if (!this.client.Connected)
                return false;
            try
            {
                var nstream = client.GetStream();
                byte[] buffer = new byte[1024];

                int bytes = nstream.Read(buffer, 0, buffer.Length);
                if (4 != bytes)
                    return false;

                data = (MessageProtocol)BitConverter.ToInt32(buffer, 0);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log(Logger.Type.Error, ex);
                return false;
            }
        }
    }
}
