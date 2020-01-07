using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpiralLab.Sirius;

namespace SpiralLab.Sirius
{
    public class YourCustomLaser : SpiralLab.Sirius.ILaser
    {
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
        public float MaxPowerWatt { get; }

        /// <summary>
        /// 현재 설정된 출력 와트
        /// </summary>
        public float CurrentPowerWatt { get; set; }

        /// <summary>
        /// 레이저 파워를 제어하는 방법
        /// </summary>
        public SpiralLab.Sirius.PowerXFactor PowerXFactor { get; set; }

        /// <summary>
        /// 최대 파워를 위한 X 요소값
        /// </summary>
        public float MaxPowerX { get; set; }

        /// <summary>
        /// 현재 파워 출력을 위한 X 요소값
        /// </summary>
        public float CurrentPowerX { get; set; }

        public bool IsReady
        {
            get { return !this.IsError; }
        }
        public bool IsBusy
        {
            get { return false; }
        }
        public bool IsError { get; set; }
        public SpiralLab.Sirius.Pen CurrentPen { get; set; }
        public Form Form { get; set; }
        public object Tag { get; set; }
        private bool disposed = false;


        public YourCustomLaser(uint index, string name, float maxPowerWatt, PowerXFactor powerXFactor)
        {
            this.Index = index;
            this.Name = name;
            this.MaxPowerWatt = maxPowerWatt;
            this.PowerXFactor = powerXFactor;
            this.Form = new LaserForm(this);
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
            ///rs-232 or tcp/ip communcation for you laser source
            return true;
        }

        public bool CtlReset()
        {
            ///error reset on laser source by communcation.
            ///
            IsError = false;
            return true;
        }

        public bool CtlPower(IRtc rtc, Pen pen)
        {
            if (this.IsBusy)
                return false;
            if (this.IsError)
                return false;
            bool success = true;
            if (pen.Power == CurrentPowerWatt)
                return true;
            /// powerWatt 출력을 내기 위한  x factor (normalPowerX) 검색
            this.LookUpPowerX(pen.Power, out float powerX);
            if (this.PowerXFactor == PowerXFactor.ByUser)
            {
                ///do it yourself
                ///vary the laser power by communcation betweeen laser source
            }
            else
                success = rtc.CtlLaserControl(this.PowerXFactor, powerX);

            if (success)
            {
                this.CurrentPowerWatt = pen.Power;
                this.CurrentPowerX = powerX;
                this.CurrentPen = pen;
                Logger.Log(Logger.Type.Warn, $"set laser power to {pen.Power}W");
            }
            return success;
        }
        private bool LookUpPowerX(float watt, out float foundedPowerX)
        {
            ///look up x factor value in table
            /// 해당 파워(watt) 가 출력되기 위한 x 요소값을 조회하여 
            /// x 요소값을 전달 !
            foundedPowerX = watt;
            return true;
        }
        public bool ListPower(IRtc rtc, Pen pen)
        {
            if (this.IsError)
                return false;
            bool success = true;
            if (pen.Power == this.CurrentPowerWatt)
                return true;
            this.LookUpPowerX(pen.Power, out float powerX);
            success &= rtc.ListLaserControl(powerX);
            if (this.PowerXFactor == PowerXFactor.ByUser)
            {
                //rs-232 or tcp/ip 통신을 사용한다면 
                //rtc의 버퍼에 저장된 모든 명령이 실행되기를 기다린후
                //레이저 소스의 출력을 변경하고 
                //다시 rtc 명령버퍼를 시작한다.
                // 아래와 같다.


                //success &= rtc.ListEnd();
                //success &= rtc.ListExecute(true);
                //if (!success)
                //    return false;

                //통신 명령을 통한 레이저 소스 출력 변경
                //변경이 완료되기를 대기

                //rtc 버퍼 명령 삽입 시작
                //success &= rtc.ListBegin(this);
            }
            if (success)
            {
                this.CurrentPowerWatt = pen.Power;
                this.CurrentPowerX = powerX;
                this.CurrentPen = pen;
            }
            return success;
        }
    }
}
