using SpiralLab;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    /// <summary>
    /// 통신을 통해 연결된 편집기의 개체 속성을 변경하는 예제 TCP 클라이언트
    /// </summary>
    public class SiriusTcpClient : IDisposable
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
        Thread thread;
        SiriusEditorForm editor;

        readonly byte[] OK = Encoding.UTF8.GetBytes("OK;");
        readonly byte[] NG = Encoding.UTF8.GetBytes("NG;");

        bool disposed = false;

        public SiriusTcpClient(SiriusEditorForm editor, string ipaddress, int port)
        {
            this.editor = editor;
            this.ipaddress = ipaddress;
            this.port = port;
        }
        ~SiriusTcpClient()
        {            
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
                //if (null != this.thread)
                //    this.thread.Join(2 * 1000);
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
            this.thread.Name = $"Sirius Client Thread";
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
                    Logger.Log(Logger.Type.Warn, $"vision tcp client is not connected");
                    this.client.Connect(this.ipaddress, this.port);
                    this.client.NoDelay = true;
                    isConnected = true;
                    Logger.Log(Logger.Type.Info, $"vision tcp client connected to {this.ipaddress}:{this.port}");
                    do
                    {
                        if (!this.Receive(out var resp))
                        {
                            this.client.Close();
                            break;
                        }
                        if (false == this.Parse(resp))
                            Logger.Log(Logger.Type.Error, $"fail to parse tcp client data format");

                    } while (!terminated && this.client.Connected);
                }
                catch (Exception )
                {
                    //Logger.Log(Logger.Type.Error, ex);
                }
            }
            while (!terminated);
        }

        public bool Send(byte[] bytes)
        {
            if (!this.client.Connected)
                return false;
            try
            {
                var nstream = client.GetStream();
                nstream.WriteAsync(bytes, 0, bytes.Length);
                return true;
                
            }
            catch (Exception ex)
            {
                Logger.Log(Logger.Type.Error, ex);
                return false;
            }
        }
        public bool Receive(out byte[] data)
        {
            data = null;
            if (!this.client.Connected)
                return false;
            try
            {
                var nstream = client.GetStream();
                if (!nstream.CanRead)
                    return false;
                byte[] buffer = new byte[1024];
                int bytes = nstream.Read(buffer, 0, buffer.Length);
                if (0 ==  bytes)
                {
                    return false;
                }

                data = buffer;
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log(Logger.Type.Error, ex);
            }
            return false;
        }
        bool Parse(byte[] data)
        {
            bool success = true;
            string str = Encoding.Default.GetString(data);

            char[] seps = { '\r', '\n', ';' };
            string[] tokens = str.Split(seps);

            switch( tokens[0])
            {
                case "Entity":
                    // 엔티티의 속성값 변경
                    success = this.EntityParse(tokens[1], tokens[2], tokens[3]);
                    break;
                case "Recipe":
                    // 문서(Document) 변경 - 레시피 변경
                    success = this.RecipeParse(tokens[1]);
                    break;
                default:
                    success = false;
                    break;
            }
            return success;
        }

        private bool EntityParse(string name, string propOrMethodName, string param)
        {
            editor.Invoke(new MethodInvoker(delegate ()
            {
                var doc = editor.Document as DocumentDefault;
                var entity = doc.Layers.NameOf(name, out Layer parentLayer);
                if (null == entity)
                {
                    this.Send(NG);
                    return;
                }

                Type type = entity.GetType();
                var propInfo = type.GetProperty(propOrMethodName, BindingFlags.Public | BindingFlags.Instance);
                if (null == propInfo || !propInfo.CanWrite)
                {
                    //not exist
                    this.Send(NG);
                    return;
                }

                bool success = false;
                try
                {
                    var value = Convert.ChangeType(param, propInfo.PropertyType);
                    propInfo.SetValue(entity, value, null);
                    success = true;
                }
                catch (Exception ex)
                {
                    Logger.Log(Logger.Type.Error, ex);
                    success = false;
                }

                string[] tokens = param.Split(new char[] { ',' });
                if (!success)
                { 
                    try
                    {
                        switch (propInfo.PropertyType.ToString())
                        {
                            case "System.Numerics.Vector2":
                                {
                                    var value = (System.Numerics.Vector2)Activator.CreateInstance(propInfo.PropertyType);
                                    value.X = float.Parse(tokens[0]);
                                    value.Y = float.Parse(tokens[1]);
                                    propInfo.SetValue(entity, value, null);
                                    success = true;
                                }
                                break;
                            case "System.Numerics.Vector3":
                                {
                                    var value = (System.Numerics.Vector3)Activator.CreateInstance(propInfo.PropertyType);
                                    value.X = float.Parse(tokens[0]);
                                    value.Y = float.Parse(tokens[1]);
                                    value.Z = float.Parse(tokens[2]);
                                    propInfo.SetValue(entity, value, null);
                                    success = true;
                                }
                                break;
                            case "SpiralLab.Sirius.Alignment":
                                {
                                    var align = (SpiralLab.Sirius.Alignment)Enum.Parse(typeof(SpiralLab.Sirius.Alignment), tokens[0]);
                                    propInfo.SetValue(entity, align, null);
                                    success = true;
                                }
                                break;
                            case "SpiralLab.Sirius.BarcodeShapeType":
                                {
                                    var shapeType = (SpiralLab.Sirius.BarcodeShapeType)Enum.Parse(typeof(SpiralLab.Sirius.BarcodeShapeType), tokens[0]);
                                    propInfo.SetValue(entity, shapeType, null);
                                    success = true;
                                }
                                break;
                            case "SpiralLab.Sirius.Barcode1DFormat":
                                {
                                    var bcd1DFormat = (SpiralLab.Sirius.Barcode1DFormat)Enum.Parse(typeof(SpiralLab.Sirius.Barcode1DFormat), tokens[0]);
                                    propInfo.SetValue(entity, bcd1DFormat, null);
                                    success = true;
                                }
                                break;
                            case "SpiralLab.Sirius.BarcodeQR.ErrorCorrectionLevel":
                                {
                                    var errorCorrection = (SpiralLab.Sirius.BarcodeQR.ErrorCorrectionLevel)Enum.Parse(typeof(SpiralLab.Sirius.BarcodeQR.ErrorCorrectionLevel), tokens[0]);
                                    propInfo.SetValue(entity, errorCorrection, null);
                                    success = true;
                                }
                                break;
                            //case "ZXing.Datamatrix.Encoder.SymbolShapeHint":
                                    //var shapeHint = (ZXing.Datamatrix.Encoder.SymbolShapeHint)Enum.Parse(typeof(ZXing.Datamatrix.Encoder.SymbolShapeHint), tokens[0]);
                                    //propInfo.SetValue(entity, shapeHint, null);
                                    //success = true;
                                //break;
                            case "SpiralLab.Sirius.ExtensionChannel":
                                {
                                    var extChannel = (SpiralLab.Sirius.ExtensionChannel)Enum.Parse(typeof(SpiralLab.Sirius.ExtensionChannel), tokens[0]);
                                    propInfo.SetValue(entity, extChannel, null);
                                    success = true;
                                }
                                break;
                            case "SpiralLab.Sirius.RasterDirection":
                                {
                                    var direction = (SpiralLab.Sirius.RasterDirection)Enum.Parse(typeof(SpiralLab.Sirius.RasterDirection), tokens[0]);
                                    propInfo.SetValue(entity, direction, null);
                                    success = true;
                                }
                                break;
                            case "SpiralLab.Sirius.HatchMode":
                                {
                                    var hatchMode = (SpiralLab.Sirius.HatchMode)Enum.Parse(typeof(SpiralLab.Sirius.HatchMode), tokens[0]);
                                    propInfo.SetValue(entity, hatchMode, null);
                                    success = true;
                                }
                                break;
                            case "SpiralLab.Sirius.DirectionWay":
                                {
                                    var direction = (SpiralLab.Sirius.DirectionWay)Enum.Parse(typeof(SpiralLab.Sirius.DirectionWay), tokens[0]);
                                    propInfo.SetValue(entity, direction, null);
                                    success = true;
                                }
                                break;
                            case "SpiralLab.Sirius.DateFormat":
                                {
                                    var dateFormat = (SpiralLab.Sirius.DateFormat)Enum.Parse(typeof(SpiralLab.Sirius.DateFormat), tokens[0]);
                                    propInfo.SetValue(entity, dateFormat, null);
                                    success = true;
                                }
                                break;
                            case "SpiralLab.Sirius.SerialFormat":
                                {
                                    var serialFormat = (SpiralLab.Sirius.SerialFormat)Enum.Parse(typeof(SpiralLab.Sirius.SerialFormat), tokens[0]);
                                    propInfo.SetValue(entity, serialFormat, null);
                                    success = true;
                                }
                                break;
                            case "SpiralLab.Sirius.TimeFormat":
                                {
                                    var timeFormat = (SpiralLab.Sirius.TimeFormat)Enum.Parse(typeof(SpiralLab.Sirius.TimeFormat), tokens[0]);
                                    propInfo.SetValue(entity, timeFormat, null);
                                    success = true;
                                }
                                break;
                            case "SpiralLab.Sirius.LetterSpaceWay":
                                {
                                    var letterSpaceWay = (SpiralLab.Sirius.LetterSpaceWay)Enum.Parse(typeof(SpiralLab.Sirius.LetterSpaceWay), tokens[0]);
                                    propInfo.SetValue(entity, letterSpaceWay, null);
                                    success = true;
                                }
                                break;
                            case "SpiralLab.Sirius.AutoLaserControlSignal":
                                {
                                    var alcSignal = (SpiralLab.Sirius.AutoLaserControlSignal)Enum.Parse(typeof(SpiralLab.Sirius.AutoLaserControlSignal), tokens[0]);
                                    propInfo.SetValue(entity, alcSignal, null);
                                    success = true;
                                }
                                break;
                            default:
                                {
                                    var value = Activator.CreateInstance(propInfo.PropertyType);
                                    propInfo.SetValue(entity, value, null);
                                    success = true;
                                }
                                break;
                        }
                    }
                    catch(Exception ex)
                    {
                        Logger.Log(Logger.Type.Error, ex);
                        success = false;
                    }
                }

                if (!success)
                {
                    try
                    {
                        MethodInfo method = type.GetMethod(propOrMethodName);
                        object value = Convert.ChangeType(param, type);
                        method.Invoke(entity, new object[] { value });
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(Logger.Type.Error, ex);
                        success = false;
                    }
                }

                if (success)
                {
                    entity.Regen();
                    editor.Refresh();
                }

                if (success)
                {
                    this.Send(OK);
                }
                else
                {
                    this.Send(NG);
                }
            }));
            return true;
        }

        private bool RecipeParse(string name)
        {
            if (editor.Marker.IsBusy)
            {
                this.Send(NG);
                return false;
            }
            string recipeFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "recipes", name);
            if (File.Exists(recipeFileName))
            {
                this.Send(NG);
                return false;
            }

            var doc = DocumentSerializer.OpenSirius(recipeFileName);
            if (null == doc)
            {
                this.Send(NG);
                return false;
            }

            editor.Invoke(new MethodInvoker(delegate ()
            {
                editor.Document = doc;
            }));
            return true;
        }
    }
}
