using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpiralLab.Sirius;

namespace SpiralLab.Sirius
{
    //레이저 소스 (사용자 커스텀 용)
    public class YourCustomLaser : SpiralLab.Sirius.ILaser
    {
        public object SyncRoot { get; set; }
        /// <summary>
        /// 식별 번호
        /// </summary>
        public uint Index { get; set; }

        /// <summary>
        /// 이름
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 최대 출력 와트
        /// </summary>
        public float MaxPowerWatt { get; set; }

        public bool IsReady
        {
            get { return !this.IsError; }
        }
        public bool IsBusy
        {
            get { return false; }
        }
        public bool IsError { get; set; }
        public IRtc Rtc { get; set; }
        public object Tag { get; set; }
        private bool disposed = false;

        public YourCustomLaser(uint index, string name, float maxPowerWatt)
        {
            this.SyncRoot = new object();
            this.Index = index;
            this.Name = name;
            this.MaxPowerWatt = maxPowerWatt;
        }
        ~YourCustomLaser()
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
        private void Dispose(bool disposing)
        {
            if (this.disposed)
                return;
            if (disposing)
            {
            }
            this.disposed = true;
        }

        /// <summary>
        /// 초기화
        /// </summary>
        /// <returns></returns>
        public bool Initialize()
        {
            //rs-232 or tcp/ip communcation for you laser source
            return true;
        }
        public bool CtlAbort()
        {
            lock (this.SyncRoot)
            {
                return true;
            }
        }
        public bool CtlReset()
        {
            lock (this.SyncRoot)
            {
                //error reset on laser source by communcation.            
                IsError = false;
                return true;
            }
        }

        public bool CtlPower(float watt)
        {
            lock (this.SyncRoot)
            {
                if (null == this.Rtc)
                    return false;
                bool success = true;
                success = this.Rtc.CtlWriteData<float>(ExtensionChannel.ExtAO1, 5); // 아나로그로 제어되는 레이저 소스

                if (success)
                {
                    Logger.Log(Logger.Type.Warn, $"set laser power to {watt:F3}W");
                }
                return success;
            }
        }
      
        public bool ListPower( float watt)
        {
            lock (this.SyncRoot)
            {
                if (null == this.Rtc)
                    return false;
                bool success = true;
                float voltage = watt / MaxPowerWatt * 10.0f;
                success &= this.Rtc.ListWriteData<float>(ExtensionChannel.ExtAO1, voltage); // 아나로그로 제어되는 레이저 소스
                return success;
            }
        }
    }
}
