﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpiralLab.Sirius;

namespace SpiralLab.Sirius
{
    /// <summary>
    /// 레이저 소스 (사용자 커스텀 용)
    /// </summary>
    public class YourCustomLaser 
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

        /// <summary>
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

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="index">식별번호</param>
        /// <param name="name">이름</param>
        /// <param name="maxPowerWatt">최대 출력 (Watt)</param>
        public YourCustomLaser(int index, string name, float maxPowerWatt)
        {
            this.SyncRoot = new object();
            this.Index = index;
            this.Name = name;
            this.MaxPowerWatt = maxPowerWatt;
            this.IsPowerControl = true;
            this.PowerControlMethod = PowerControlMethod.Unknown;
            this.IsShutterControl = false;
            this.IsGuideControl = false;
        }
        ~YourCustomLaser()
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
        /// <param name="powerMapCategory">파워맵 룩업 대상 카테고리</param>
        /// <returns></returns>
        public bool CtlPower(float watt, string powerMapCategory = "")
        {
            lock (this.SyncRoot)
            {
                if (null == this.Rtc)
                    return false;
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
                bool success = true;
                float analogV = compensatedWatt / this.MaxPowerWatt * 10.0f; //max 10V
                success = this.Rtc.CtlWriteData<float>(ExtensionChannel.ExtAO2, analogV); // 아나로그로 출력이 제어되는 레이저 소스 (RTC 의 아나로그 2번 포트로 연결되었을 경우)

                if (success)
                {
                    Logger.Log(Logger.Type.Warn, $"laser [{this.Index}]: power to {compensatedWatt:F3} W");
                }
                return success;
            }
        }
        public bool ListBegin()
        {
            return true;
        }
        public bool ListEnd()
        {
            return true;
        }

        /// <summary>
        /// 지정된 출력(watt)으로 레이저 파워 변경
        /// 리스트 명령 (RTC 버퍼에 삽입되는 명령)
        /// </summary>
        /// <param name="watt">Watt</param>
        /// <param name="powerMapCategory">파워맵 룩업 대상 카테고리</param>
        /// <returns></returns>
        public bool ListPower( float watt, string powerMapCategory = "")
        {
            lock (this.SyncRoot)
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
                float analogV = compensatedWatt / this.MaxPowerWatt * 10.0f; //max 10V
                success &= this.Rtc.ListWriteData<float>(ExtensionChannel.ExtAO2, analogV);   // 아나로그로 출력이 제어되는 레이저 소스 (RTC 의 아나로그 2번 포트로 연결되었을 경우)
                return success;
            }
        }
    }
}
