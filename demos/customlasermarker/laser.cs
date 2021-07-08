using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpiralLab.Sirius;

namespace SpiralLab.Sirius
{
    /// <summary>
    /// 레이저 소스 (사용자 커스텀 용)
    /// </summary>
    public class YourCustomLaser : SpiralLab.Sirius.ILaser
    {
        /// <summary>
        /// 동기화 객체
        /// </summary>
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

        /// <summary>
        /// 준비 상태 
        /// </summary>
        public bool IsReady
        {
            get { return !this.IsError; }
        }
        /// <summary>
        /// 가공중 상태
        /// </summary>
        public bool IsBusy
        {
            get { return false; }
        }
        /// <summary>
        /// 에러 상태
        /// </summary>
        public bool IsError { get; set; }
        /// <summary>
        /// IRtc 객체
        /// </summary>
        public IRtc Rtc { get; set; }

        public object Tag { get; set; }
        private bool disposed = false;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="index">식별번호</param>
        /// <param name="name">이름</param>
        /// <param name="maxPowerWatt">최대 출력 (Watt)</param>
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

        /// <summary>
        /// 가공 중지
        /// </summary>
        /// <returns></returns>
        public bool CtlAbort()
        {
            lock (this.SyncRoot)
            {
                return true;
            }
        }
        /// <summary>
        /// 에러 해제 시도
        /// </summary>
        /// <returns></returns>
        public bool CtlReset()
        {
            lock (this.SyncRoot)
            {
                //error reset on laser source by communcation.            
                IsError = false;
                return true;
            }
        }
        /// <summary>
        /// 지정된 출력(watt)으로 레이저 파워 변경
        /// 컨트롤 명령 (즉시 명령)
        /// </summary>
        /// <param name="watt">Watt</param>
        /// <returns></returns>
        public bool CtlPower(float watt)
        {
            lock (this.SyncRoot)
            {
                if (null == this.Rtc)
                    return false;
                bool success = true;
                success = this.Rtc.CtlWriteData<float>(ExtensionChannel.ExtAO2, 0.5f); // 아나로그로 제어되는 레이저 소스

                if (success)
                {
                    Logger.Log(Logger.Type.Warn, $"set laser power to {watt:F3}W");
                }
                return success;
            }
        }
        /// <summary>
        /// 지정된 출력(watt)으로 레이저 파워 변경
        /// 리스트 명령 (RTC 버퍼에 삽입되는 명령)
        /// </summary>
        /// <param name="watt">Watt</param>
        /// <returns></returns>
        public bool ListPower( float watt)
        {
            lock (this.SyncRoot)
            {
                if (null == this.Rtc)
                    return false;
                bool success = true;
                float ratio = watt / MaxPowerWatt;
                success &= this.Rtc.ListWriteData<float>(ExtensionChannel.ExtAO2, ratio); // 아나로그로 제어되는 레이저 소스
                return success;
            }
        }
    }
}
