using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpiralLab.Sirius;

namespace SpiralLab.Sirius
{
    /// <summary>
    /// 레이저 소스 (사용자 커스텀 버전)
    /// RS-232 통신과 같이 통신 지연을 가지고 있는 레이저 소스 예제
    /// RTC 카드에 내장된 시리얼 통신 포트 사용
    /// </summary>
    public class YourCustomLaser3
        : SpiralLab.Sirius.ILaser
        , SpiralLab.Sirius.IPowerControl
        //, SpiralLab.Sirius.IShutterControl
        //, SpiralLab.Sirius.IGuideControl
    {
        /// <summary>
        /// 속성 변경 이벤트 핸들러
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.BeginInvoke(this, new PropertyChangedEventArgs(propertyName), null, null);
        }

        /// <summary>
        /// 동기화 객체
        /// </summary>
        public object SyncRoot { get; set; }
        /// <summary>
        /// 식별 번호
        /// </summary>
        public int Index { get; set; }
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
        public bool IsTimedOut { get; protected set; }
        public bool IsProtocolError { get; protected set; }
        // <summary>
        /// IRtc 객체
        /// </summary>
        public IRtc Rtc { get; set; }
        public bool IsPowerControl { get; set; }
        public PowerControlMethod PowerControlMethod { get; set; }
        public IPowerMap PowerMap { get; set; }

        public bool IsShutterControl { get; set; }
        public bool IsGuideControl { get; set; }
        public object Tag { get; set; }

        private bool disposed = false;

        public YourCustomLaser3(int index, string name, float maxPowerWatt, int comPort)
        {
            this.SyncRoot = new object();
            this.Index = index;
            this.Name = name;
            this.MaxPowerWatt = maxPowerWatt;
            this.IsPowerControl = true;
            this.PowerControlMethod = PowerControlMethod.Rs232;
            this.IsShutterControl = false;
            this.IsGuideControl = false;
        }
        ~YourCustomLaser3()
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
            var serial = Rtc as IRtcSerialComm;
            serial.CtlSerialConfig(9600);
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
                var serial = Rtc as IRtcSerialComm;
                //serial.CtlSerialWrite
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
                var serial = Rtc as IRtcSerialComm;
                //serial.CtlSerialWrite
                IsError = false;
                return true;
            }
        }
        /// <summary>
        /// 지정된 출력(watt)으로 레이저 파워 변경
        /// 컨트롤 명령 (즉시 명령)
        /// </summary>
        /// <param name="watt">Watt</param>
        /// <param name="powerMapCategory">파워맵 룩업 대상 카테고리</param>
        /// <returns></returns>
        public bool CtlPower(float watt, string powerMapCategory = "")
        {          
            bool success = true;
            if (watt > this.MaxPowerWatt)
                watt = this.MaxPowerWatt;
            float compensatedWatt = watt;
            if (null != this.PowerMap && !string.IsNullOrEmpty(powerMapCategory))
            {
                if (false == this.PowerMap.Interpolate(powerMapCategory, watt, out compensatedWatt))
                {
                    Logger.Log(Logger.Type.Error, $"laser [{this.Index}]: fail to search target powermap category: {powerMapCategory}");
                    return false;
                }
            }
            //통신을 통한 파워 변경 시도
            var serial = Rtc as IRtcSerialComm;
            success &= serial.CtlSerialWrite($"{watt:F1}"); 
            return success;
        }
        public bool ListBegin()
        {
            return true;
        }

        /// <summary>
        /// 지정된 출력(watt)으로 레이저 파워 변경
        /// 리스트 명령 (RTC 버퍼에 삽입되는 명령)
        /// 
        /// IPen 객체가 가공(Mark 함수)을 최초에 시작하며 이때 함수가 호출된다.
        /// 만약 여러개의 IPen 을 사용하고, RS232 통신과 같이 통신 지연 시간이 필요하면 
        /// 통신 완료후 다음 리스트 명령들이 실행가능하도록, 지금까지 삽입된 모든 List 명령을 실행 완료후
        /// 파워 변경 통신을 시도하고, 이후에 신규 RTC 버퍼를 시작한다
        /// </summary>
        /// <param name="watt">Watt</param>
        /// <param name="powerMapCategory">파워맵 룩업 대상 카테고리</param>
        /// <returns></returns>
        public bool ListPower( float watt, string powerMapCategory = "")
        {
            if (null == this.Rtc)
                return false;
            if (watt > this.MaxPowerWatt)
                watt = this.MaxPowerWatt;
            float compensatedWatt = watt;
            if (null != this.PowerMap && !string.IsNullOrEmpty(powerMapCategory))
            {
                if (false == this.PowerMap.Interpolate(powerMapCategory, watt, out compensatedWatt))
                    return false;
            }
            bool success = true;
            var serial = Rtc as IRtcSerialComm;
            success &= serial.ListSerialWrite($"{compensatedWatt:F1}");
            //레이저 파워 변경 대기를 위한 2초 지연
            success &= Rtc.ListWait(2 * 1000);
            return success;
        }
        public bool ListEnd()
        {
            return true;
        }
    }
}
