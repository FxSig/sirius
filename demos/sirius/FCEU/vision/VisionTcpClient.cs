using SpiralLab;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                return isConnected;
                //if (null == this.client)
                //    return false;
                //return this.client.Connected;
            }
        }
        bool terminated = false;
        TcpClient client;
        string ipaddress;
        int port;
        bool isConnected;
        SpiralLab.Sirius.FCEU.FormMain formMain;
        Thread thread;

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
                this.client?.Dispose();
                if (null != this.thread)
                    this.thread.Join(2 * 1000);
            }
            this.disposed = true;
        }

        public bool Start()
        {
            terminated = true;
            if (null != this.thread)
                this.thread.Join(2 * 1000);
            this.client = new TcpClient();
            terminated = false;
            this.thread = new Thread(this.WorkerThread);
            this.thread.Name = $"Vision Client Thread";
            this.thread.Start();
            return true;
        }

        public void WorkerThread()
        {
            do
            {
                try
                {
                    isConnected = false;
                    this.client?.Dispose();
                    this.client = new TcpClient();
                    //Logger.Log(Logger.Type.Warn, $"vision tcp client is not connected");
                    this.client.Connect(this.ipaddress, this.port);
                    this.client.NoDelay = true;
                    isConnected = true;
                    Logger.Log(Logger.Type.Info, $"vision tcp client connected to {this.ipaddress}:{this.port}");
                    this.Send(MessageProtocol.IM_LASER);
                    do
                    {
                        if (!this.Receive(out var resp))
                        {
                            Logger.Log(Logger.Type.Error, $"vision tcp client fail to receive data !");
                            this.client.Close();
                            break;
                        }
                        if (false == this.Parse(resp))
                            Logger.Log(Logger.Type.Error, $"vision tcp client unknown data format? {(int)resp}");                            

                    } while (!terminated && this.client.Connected);
                }
                catch (Exception )
                {
                    //Logger.Log(Logger.Type.Error, ex);
                }
            }
            while (!terminated);
        }

        public bool Send(MessageProtocol data)
        {
            if (!this.client.Connected)
                return false;
            try
            {
                var nstream = client.GetStream();
                byte[] bytes = BitConverter.GetBytes( Convert.ToInt32(data));
                Debug.Assert(4 == bytes.Length);
                //nstream.Write(bytes, 0, bytes.Length);
                nstream.WriteAsync(bytes, 0, bytes.Length);
                Logger.Log(Logger.Type.Debug, $"vision comm send : {data.ToString()} [{(int)data}, 0x{(int)data:X4}]");
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
                byte[] buffer = new byte[4];
                int bytes = nstream.Read(buffer, 0, buffer.Length);
                if (4 != bytes)
                {
                    Logger.Log(Logger.Type.Error, $"fail to receive vision comm bytes= {bytes}");
                    return false;
                }

                data = (MessageProtocol)BitConverter.ToInt32(buffer, 0);
                Logger.Log(Logger.Type.Debug, $"vision comm recv : {data.ToString()} [{(int)data}, 0x{(int)data:X4}]");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log(Logger.Type.Error, ex);
            }
            return false;
        }
        bool Parse(MessageProtocol message)
        {
            bool success = true;
            var seq = this.formMain.Seq;
            var svc = seq.Service as LaserService;
            int modelChange = (int)message;
            if (modelChange >= (int)MessageProtocol.MODEL_LOAD && modelChange < (int)MessageProtocol.MODEL_LOAD_OK)
            {
                //model change 
                success &= svc.RecipeChange(modelChange);
                if (success)
                    this.Send(MessageProtocol.MODEL_LOAD_OK);
                else
                    this.Send(MessageProtocol.MODEL_LOAD_NG);
            }
            else
            {
                switch (message)
                {
                    default:
                        //unknown message format
                        success = false;
                        break;
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
                    case MessageProtocol.LASER_STATUS_RESET:
                        seq.Reset();
                        this.Send(MessageProtocol.LASER_STATUS_RESET_OK);                        
                        break;
                    #endregion

                    #region 시스템 티칭및 스캐너 보정
                    case MessageProtocol.LASER_SCANNER_SYSTEM_TEACH:
                        //if (seq.Start(LaserSequence.Process.SystemTeach))
                        //    this.Send(MessageProtocol.LASER_SCANNER_SYSTEM_TEACH_OK);
                        //else
                            this.Send(MessageProtocol.LASER_SCANNER_SYSTEM_TEACH_NG);
                        break;
                    case MessageProtocol.LASER_SCANNER_COMPENSATE_3X3:
                        svc.FieldCorrectionRows = svc.FieldCorrectionCols = 3;
                        svc.FieldCorrectionInterval = seq.Fov / 2;
                        //if (seq.Start(LaserSequence.Process.FieldCorrection))
                        //    this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_OK);
                        //else
                            this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_NG);
                        break;
                    case MessageProtocol.LASER_SCANNER_COMPENSATE_5X5:
                        svc.FieldCorrectionRows = svc.FieldCorrectionCols = 5;
                        svc.FieldCorrectionInterval = seq.Fov / 4;
                        //if (seq.Start(LaserSequence.Process.FieldCorrection))
                        //    this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_OK);
                        //else
                            this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_NG);
                        break;
                    case MessageProtocol.LASER_SCANNER_COMPENSATE_7X7:
                        svc.FieldCorrectionRows = svc.FieldCorrectionCols = 7;
                        svc.FieldCorrectionInterval = seq.Fov / 6;
                        //if (seq.Start(LaserSequence.Process.FieldCorrection))
                        //    this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_OK);
                        //else
                            this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_NG);
                        break;
                    case MessageProtocol.LASER_SCANNER_COMPENSATE_9X9:
                        svc.FieldCorrectionRows = svc.FieldCorrectionCols = 9;
                        svc.FieldCorrectionInterval = seq.Fov / 8;
                        //if (seq.Start(LaserSequence.Process.FieldCorrection))
                        //    this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_OK);
                        //else
                            this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_NG);
                        break;
                    case MessageProtocol.LASER_SCANNER_COMPENSATE_11X11:
                        svc.FieldCorrectionRows = svc.FieldCorrectionCols = 11;
                        //svc.FieldCorrectionInterval = seq.Fov / 10;
                        //if (seq.Start(LaserSequence.Process.FieldCorrection))
                        //    this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_OK);
                        //else
                            this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_NG);
                        break;
                    case MessageProtocol.LASER_SCANNER_COMPENSATE_13X13:
                        svc.FieldCorrectionRows = svc.FieldCorrectionCols = 13;
                        svc.FieldCorrectionInterval = seq.Fov / 12;
                        //if (seq.Start(LaserSequence.Process.FieldCorrection))
                        //    this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_OK);
                        //else
                            this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_NG);
                        break;
                    case MessageProtocol.LASER_SCANNER_COMPENSATE_15X15:
                        svc.FieldCorrectionRows = svc.FieldCorrectionCols = 15;
                        svc.FieldCorrectionInterval = seq.Fov / 14;
                        //if (seq.Start(LaserSequence.Process.FieldCorrection))
                        //    this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_OK);
                        //else
                            this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_NG);
                        break;
                    case MessageProtocol.LASER_SCANNER_COMPENSATE_17X17:
                        svc.FieldCorrectionRows = svc.FieldCorrectionCols = 17;
                        svc.FieldCorrectionInterval = seq.Fov / 16;
                        //if (seq.Start(LaserSequence.Process.FieldCorrection))
                        //    this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_OK);
                        //else
                            this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_NG);
                        break;

                    case MessageProtocol.LASER_SCANNER_COMPENSATE_READ:
                        if (svc.ReadScannerFieldCorrection())
                            this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_READ_OK);
                        else
                            this.Send(MessageProtocol.LASER_SCANNER_COMPENSATE_READ_NG);
                        break;
                    #endregion

                    #region Ref 타켓 마킹
                    case MessageProtocol.LASER_SCANNER_REF_01_IMAGE:
                        //if (seq.Start(LaserSequence.Process.Ref_Right))
                        //    this.Send(MessageProtocol.LASER_SCANNER_REF_01_IMAGE_OK);
                        //else
                            this.Send(MessageProtocol.LASER_SCANNER_REF_01_IMAGE_NG);
                        break;

                    case MessageProtocol.LASER_SCANNER_REF_02_IMAGE:
                        //if (seq.Start(LaserSequence.Process.Ref_Left))
                        //    this.Send(MessageProtocol.LASER_SCANNER_REF_02_IMAGE_OK);
                        //else
                            this.Send(MessageProtocol.LASER_SCANNER_REF_02_IMAGE_NG);
                        break;
                    #endregion

                    #region 불량 정보를 1/2 읽어서 에디터에 표시 (도면 기준)
                    //case MessageProtocol.LASER_READ_INSPECT_01:
                    //    {
                    //        //string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "동일파일 표현 분할.txt");
                    //        string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.txt");
                    //        if (svc.ReadDefectFromFile(fileName, out var group))
                    //        {
                    //            this.Send(MessageProtocol.LASER_READ_INSPECT_01_OK);
                    //            if (svc.PrepareDefectInEditor(1, group))
                    //                this.Send(MessageProtocol.LASER_READ_INSPECT_01_FINISH);
                    //        }
                    //        else
                    //            this.Send(MessageProtocol.LASER_READ_INSPECT_01_NG);
                    //    }
                    //    break;
                    //case MessageProtocol.LASER_READ_INSPECT_02:
                    //    {
                    //        string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "동일파일 표현 없음.txt");
                    //        if (svc.ReadDefectFromFile(fileName, out var group))
                    //        {
                    //            this.Send(MessageProtocol.LASER_READ_INSPECT_02_OK);
                    //            if (svc.PrepareDefectInEditor(2, group))
                    //                this.Send(MessageProtocol.LASER_READ_INSPECT_02_FINISH);
                    //        }
                    //        else
                    //            this.Send(MessageProtocol.LASER_READ_INSPECT_02_NG);
                    //    }
                    //    break;
                    #endregion

                    #region 불량 정보를 1/2 읽어서 마커에 ready (자재기준)
                    case MessageProtocol.LASER_READ_HATCHING_01:
                        {
                            var defFile = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"FILE", "DEFECT_RIGHT");
                            if (svc.ReadDefectFromFile(1, defFile, out var group))
                            {
                                this.Send(MessageProtocol.LASER_READ_HATCHING_01_OK);
                                if (svc.PrepareDefectInEditor(1, group))
                                //if (svc.PrepareDefectInMarker(1, group)) //에디터만 업데이트해 놓으면 추후 start 시 복제됨
                                    this.Send(MessageProtocol.LASER_READ_HATCHING_01_FINISH);
                            }
                            else
                                this.Send(MessageProtocol.LASER_READ_HATCHING_01_NG);
                        }
                        break;
                    case MessageProtocol.LASER_READ_HATCHING_02:
                        {
                            var defFile = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"FILE", "DEFECT_LEFT");
                            if (svc.ReadDefectFromFile(2, defFile, out var group))
                            {
                                this.Send(MessageProtocol.LASER_READ_HATCHING_02_OK);
                                if (svc.PrepareDefectInEditor(2, group))
                                //if (svc.PrepareDefectInMarker(2, group)) //에디터만 업데이트해 놓으면 추후 start 시 복제됨
                                    this.Send(MessageProtocol.LASER_READ_HATCHING_02_FINISH);
                            }
                            else
                                this.Send(MessageProtocol.LASER_READ_HATCHING_02_NG);
                        }
                        break;
                    #endregion

                    #region 불량 정보를 출사 (1: 우, 2: 좌)                        
                    case MessageProtocol.MOVE_HATCHING_01_POITION_DONE:
                        this.Send(MessageProtocol.MOVE_HATCHING_01_POITION_DONE_OK);
                        break;
                    case MessageProtocol.DO_HATCHING_01_START:
                        //if (seq.Start(LaserSequence.Process.Defect_Right))
                        //    this.Send(MessageProtocol.DO_HATCHING_01_START_OK);
                        //else
                            this.Send(MessageProtocol.DO_HATCHING_01_START_NG);
                        break;
                    case MessageProtocol.MOVE_HATCHING_02_POITION_DONE:
                        this.Send(MessageProtocol.MOVE_HATCHING_02_POITION_DONE_OK);
                        break;
                    case MessageProtocol.DO_HATCHING_02_START:
                        //if (seq.Start(LaserSequence.Process.Defect_Left))
                        //    this.Send(MessageProtocol.DO_HATCHING_02_START_OK);
                        //else
                            this.Send(MessageProtocol.DO_HATCHING_02_START_NG);
                        break;
                    #endregion
                }
            }
            return success;
        }
    }
}
