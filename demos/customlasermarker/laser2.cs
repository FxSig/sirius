using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpiralLab.Sirius;

namespace SpiralLab.Sirius
{
    /// <summary>
    /// 레이저 소스 (사용자 커스텀 버전(
    /// RS-232 통신과 같이 통신 지연을 가지고 있는 레이저 소스 예제
    /// </summary>
    public class YourCustomLaser2 : SpiralLab.Sirius.ILaser
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
        // <summary>
        /// IRtc 객체
        /// </summary>
        public IRtc Rtc { get; set; }
        public object Tag { get; set; }

        private SerialPort serialPort;
        private bool disposed = false;

        public YourCustomLaser2(uint index, string name, float maxPowerWatt, int comPort)
        {
            this.SyncRoot = new object();
            this.Index = index;
            this.Name = name;
            this.MaxPowerWatt = maxPowerWatt;
            this.serialPort = new SerialPort($"COM{comPort}");
            this.serialPort.BaudRate = 9600;
            this.serialPort.DataBits = 8;
            this.serialPort.StopBits = StopBits.One;
            this.serialPort.Parity = Parity.None;
        }
        ~YourCustomLaser2()
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
                this.serialPort?.Close();
                this.serialPort.Dispose();
            }
            this.disposed = true;
        }

        /// <summary>
        /// 초기화
        /// </summary>
        /// <returns></returns>
        public bool Initialize()
        {
            try
            {
                //rs-232 or tcp/ip communcation for you laser source
                this.serialPort.Open();
            }
            catch(Exception ex)
            {
                Logger.Log(Logger.Type.Error, ex);
                return false;
            }
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
                //this.serialPort.Write
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
                //this.serialPort.Write
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
            if (!this.serialPort.IsOpen)
                return false;
            bool success = true;
            //통신을 통한 파워 변경 시도
            success &= this.CommandToChangePower(watt);
            return success;
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
        /// <returns></returns>
        public bool ListPower( float watt)
        {
            if (!this.serialPort.IsOpen)
                return false;
            if (null == this.Rtc)
                return false;

            bool success = true;
            //현재 RTC 버퍼의 모든 명령을 수행 완료한다
            success &= this.Rtc.ListEnd();
            success &= this.Rtc.ListExecute(true);
            if (!success)
                return false;
            //통신을 통한 파워 변경 시도
            success &= this.CommandToChangePower(watt);
            if (!success)
                return false;
            //RTC 버퍼 준비를 새로 시작한다
            success &= this.Rtc.ListBegin(this, ListType.Single);
            return success;
        }

        private bool CommandToChangePower(float watt)
        {
            if (!this.serialPort.IsOpen)
                return false;
            bool success = true;
            lock (this.SyncRoot)
            {
                //해당 레이저 소스의 통신 메뉴얼을 참고할것
                //
                //파워 변경에 필요한 문자열 포맷을 바이트 배열로 만들경우
                //byte[] bytes = BitConverter.GetBytes(Convert.ToInt32(watt));
                //this.serialPort.Write(bytes, 0, bytes.Length);
                //or
                //ASCII 기반 통신일 경우
                this.serialPort.Write($"{watt:F1}");

                //통신 이후 실제 파워가 변경되었는지를 확인하는 추가적인 통신이 필요할 수 있음
                //여기에서는 0.5초 지연을 통해 통신을 통한 레이저 파워 변경이 완료되었다고 가정함 
                Thread.Sleep(500);
            }
            if (success)
                Logger.Log(Logger.Type.Warn, $"set laser power to {watt:F3}W");
            else
                Logger.Log(Logger.Type.Error, $"fail to change laser power to {watt:F3}W");
            return success; 
        }
    }
}
