/*
 *                                                             ,--,      ,--,                              
 *              ,-.----.                                     ,---.'|   ,---.'|                              
 *    .--.--.   \    /  \     ,---,,-.----.      ,---,       |   | :   |   | :      ,---,           ,---,.  
 *   /  /    '. |   :    \ ,`--.' |\    /  \    '  .' \      :   : |   :   : |     '  .' \        ,'  .'  \ 
 *  |  :  /`. / |   |  .\ :|   :  :;   :    \  /  ;    '.    |   ' :   |   ' :    /  ;    '.    ,---.' .' | 
 *  ;  |  |--`  .   :  |: |:   |  '|   | .\ : :  :       \   ;   ; '   ;   ; '   :  :       \   |   |  |: | 
 *  |  :  ;_    |   |   \ :|   :  |.   : |: | :  |   /\   \  '   | |__ '   | |__ :  |   /\   \  :   :  :  / 
 *   \  \    `. |   : .   /'   '  ;|   |  \ : |  :  ' ;.   : |   | :.'||   | :.'||  :  ' ;.   : :   |    ;  
 *    `----.   \;   | |`-' |   |  ||   : .  / |  |  ;/  \   \'   :    ;'   :    ;|  |  ;/  \   \|   :     \ 
 *    __ \  \  ||   | ;    '   :  ;;   | |  \ '  :  | \  \ ,'|   |  ./ |   |  ./ '  :  | \  \ ,'|   |   . | 
 *   /  /`--'  /:   ' |    |   |  '|   | ;\  \|  |  '  '--'  ;   : ;   ;   : ;   |  |  '  '--'  '   :  '; | 
 *  '--'.     / :   : :    '   :  |:   ' | \.'|  :  :        |   ,/    |   ,/    |  :  :        |   |  | ;  
 *    `--'---'  |   | :    ;   |.' :   : :-'  |  | ,'        '---'     '---'     |  | ,'        |   :   /   
 *              `---'.|    '---'   |   |.'    `--''                              `--''          |   | ,'    
 *                `---`            `---'                                                        `----'   
 * 
 * Copyright (C) 2019-2023 SpiralLab. All rights reserved.
 * custom laser source
 * Description : 
 * Author : hong chan, choi / hcchoi@spirallab.co.kr (http://spirallab.co.kr)
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace SpiralLab.Sirius
{
    /// <summary>
    /// 레이저 소스 
    /// </summary>
    public class MyLaser
        : ILaser
        , IShutterControl
        , IPowerControl
        , IGuideControl
    {
        /// <summary>
        /// 속성 변경 이벤트 핸들러
        /// </summary>
        public virtual event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            var receivers = this.PropertyChanged?.GetInvocationList();
            if (null != receivers)
                foreach (PropertyChangedEventHandler receiver in receivers)
                    receiver.BeginInvoke(this, new PropertyChangedEventArgs(propertyName), null, null);
        }

        /// <summary>
        /// 동기화 객체
        /// </summary>
        [Browsable(false)]
        public virtual object SyncRoot { get; protected set; }

        /// <summary>
        /// 식별 번호
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Basic")]
        [DisplayName("Index")]
        [Description("소스 번호")]
        public virtual int Index { get; set; }

        /// <summary>
        /// 이름
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Basic")]
        [DisplayName("Name")]
        [Description("소스 이름")]
        public virtual string Name { get; set; }

        /// <inheritdoc/>  
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [ReadOnly(true)]
        [Category("Basic")]
        [DisplayName("Type")]
        [Description("소스 타입")]
        public LaserType LaserType { get { return LaserType.UserDefined1; } }

        /// <summary>
        /// 최대 파워 (W)
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [ReadOnly(true)]
        [Category("Basic")]
        [DisplayName("Max Power")]
        [Description("소스 최대 출력 파워(W)")]
        public virtual float MaxPowerWatt { get; set; }

        /// <summary>
        /// 상태 (준비완료 여부)
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Status")]
        [DisplayName("Ready")]
        [Description("준비 상태")]
        public virtual bool IsReady
        {
            get { return true; }
        }

        /// <summary>
        /// 상태 (출사중 여부)
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Status")]
        [DisplayName("Busy")]
        [Description("출사 상태")]
        public virtual bool IsBusy 
        {
            get { return false;  }
        }

        /// <summary>
        /// 상태 (에러 발생 여부)
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Status")]
        [DisplayName("Error")]
        [Description("에러 상태")]
        public virtual bool IsError { get; protected set; }

        /// <summary>
        /// 통신 타임아웃 여부
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Status")]
        [DisplayName("Timed Out")]
        [Description("통신 타임 아웃 여부")]
        public virtual bool IsTimedOut { get; protected set; }

        /// <summary>
        /// 통신 프로토콜 포맷 에러 여부
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Status")]
        [DisplayName("Protocol Error")]
        [Description("통신 프로토콜 에러 여부")]
        public virtual bool IsProtocolError { get; protected set; }

        /// <summary>
        /// 레이저 제어에 필요한 IRtc 인터페이스
        /// </summary>
        [Browsable(false)]
        public virtual IRtc Rtc { get; set; }

        /// <summary>
        /// 파워 변경을 지원하는지 여부
        /// </summary>
        [Browsable(false)]
        public virtual bool IsPowerControl { get; protected set; }

        /// <summary>
        /// 파워 변경 방식
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [ReadOnly(true)]
        [Category("Control")]
        [DisplayName("Power Control Method")]
        [Description("파워 제어 방식")]
        public PowerControlMethod PowerControlMethod { get; protected set; }

        /// <summary>
        /// 파워 매핑 테이블
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Control")]
        [DisplayName("Power Map")]
        [Description("파워 매핑 테이블")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual IPowerMap PowerMap { get; set; }

        /// <summary>
        /// 셔터 제어를 지원하는지 여부
        /// </summary>
        [Browsable(false)]
        public virtual bool IsShutterControl { get; protected set; }

        /// <summary>
        /// Guide 레이저 빔 지원하는지 여부
        /// </summary>
        [Browsable(false)]
        public virtual bool IsGuideControl { get; protected set; }

        #region IShutter 인터페이스 구현
        /// <summary>
        /// 셔터(Shutter) 상태
        /// </summary>
        public virtual bool IsShutterOpen { get; set; }
        #endregion

        #region IGuide 인터페이스 구현
        /// <summary>
        /// 가이드 빔(Guide) On/Off 상태
        /// </summary>
        public virtual bool IsGuideOn { get; set; }
        #endregion

        /// <summary>
        /// 사용자 데이타
        /// </summary>
        [Browsable(false)]
        public virtual object Tag { get; set; }

        private bool disposed = false;

        /// <summary>
        /// 생성자
        /// </summary>
        public MyLaser()
        {
            this.SyncRoot = new object();
            this.Name = "My Laser";
            this.IsPowerControl = true;
            this.PowerControlMethod = PowerControlMethod.Unknown;
            this.IsShutterControl = false;
            this.IsGuideControl = false;
        }
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="index">식별번호</param>
        /// <param name="name">이름</param>
        /// <param name="maxPowerWatt">최대 파워 (W)</param>
        public MyLaser(int index, string name, float maxPowerWatt)
            : this()
        {
            this.Index = index;
            this.Name = name;
            this.MaxPowerWatt = maxPowerWatt;
        }
        #region 종결자 처리
        ~MyLaser()
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
        #endregion

        /// <summary>
        /// 에러(IsError) 조건 설정
        /// </summary>
        /// <returns></returns>
        protected virtual bool CheckErrors()
        {
            //IsError = 
            return true;
        }
        /// <summary>
        /// 준비(IsReady) 조건 설정
        /// </summary>
        /// <returns></returns>
        protected virtual bool CheckReady()
        {
            //IsReady = 
            return true;
        }
        /// <summary>
        /// 출사중(IsBusy) 조건 설정
        /// </summary>
        /// <returns></returns>
        protected virtual bool CheckBusy()
        {
            //IsBusy = 
            return true;
        }


        /// <summary>
        /// 초기화
        /// </summary>
        /// <returns></returns>
        public virtual bool Initialize()
        {
            lock (SyncRoot)
            {


                return true;
            }
        }
        /// <summary>
        /// 강제 출사 정지
        /// </summary>
        /// <returns></returns>
        public virtual bool CtlAbort()
        {
          
            return true;
        }
        /// <summary>
        /// 리셋
        /// </summary>
        /// <returns></returns>
        public virtual bool CtlReset()
        {
            lock (SyncRoot)
            {
                IsTimedOut = false;
                IsError = false;
                return true;
            }
        }

        #region IPowerControl 인터페이스 구현
        /// <summary>
        /// 파워 변경 (즉시 명령)
        /// </summary>
        /// <param name="watt">파워 (W)</param>
        /// <param name="powerMapCategory">파워맵 룩업 대상 카테고리</param>
        /// <returns></returns>
        public virtual bool CtlPower(float watt, string powerMapCategory = "")
        {
            lock (SyncRoot)
            {
                bool success = true;
                if (watt > this.MaxPowerWatt)
                    watt = this.MaxPowerWatt;
                float compensatedWatt = watt;
                if (null != this.PowerMap && !string.IsNullOrEmpty(powerMapCategory))
                {
                    if (false == this.PowerMap.Interpolate(powerMapCategory, watt, out compensatedWatt))
                    {
                        Logger.Log(Logger.Type.Error, $"laser [{this.Index}]: fail to search target power map category: {powerMapCategory}");
                        return false;
                    }
                }
                // compensatedWatt (W)값을 레이저 소스 출력으로 설정
                // or
                // % 값을 레이저 소스 출력으로 설정
                //var percentage = compensatedWatt / this.MaxPowerWatt;
                //
                //
                if (success)
                    Logger.Log(Logger.Type.Warn, $"laser [{this.Index}]: power to {watt:F3} -> {compensatedWatt:F3} W");
                return success;
            }
        }

        /// <summary>
        /// 리스트 시작 통지용
        /// (가공 시작시 출력 파워를 초기값등으로 전환하는 용도)
        /// </summary>
        /// <returns></returns>
        public virtual bool ListBegin()
        {
            //this.prevPowerWatt = 0;
            return true;
        }
        /// <summary>
        /// 리스트 명령 끝 통지용
        /// </summary>
        /// <returns></returns>
        public virtual bool ListEnd()
        {
            return true;
        }
        /// <summary>
        /// 파워 변경 (RTC의 리스트 명령으로 처리시)
        /// </summary>
        /// <param name="watt">파워 (W)</param>
        /// <param name="powerMapCategory">파워맵 룩업 대상 카테고리</param>
        /// <returns></returns>
        public virtual bool ListPower(float watt, string powerMapCategory = "")
        {
            lock (SyncRoot)
            {
                bool success = true;
                if (watt > this.MaxPowerWatt)
                    watt = this.MaxPowerWatt;
                float compensatedWatt = watt;
                if (null != this.PowerMap && !string.IsNullOrEmpty(powerMapCategory))
                {
                    if (false == this.PowerMap.Interpolate(powerMapCategory, watt, out compensatedWatt))
                    {
                        Logger.Log(Logger.Type.Error, $"laser [{this.Index}]: fail to search target power map category: {powerMapCategory}");
                        return false;
                    }
                }

                // compensatedWatt (W)값을 레이저 소스 출력으로 설정 (RTC 의 리스트 명령을 통해)
                // or
                // % 값을 레이저 소스 출력으로 설정  (RTC 의 리스트 명령을 통해)
                //var percentage = compensatedWatt / this.MaxPowerWatt;
                //
                //
                return success;
            }
        }
        #endregion
    }
}
