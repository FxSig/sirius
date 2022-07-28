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
 * Copyright (C) 2010-2020 SpiralLab. All rights reserved.
 * Laser Source : IPG YLP Type E
 * Description : 
 * Author : hong chan, choi / hcchoi@spirallab.co.kr (http://spirallab.co.kr)
 * 
 */

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SpiralLab;

namespace SpiralLab.Sirius
{
    
    #region IPG YLP Type E 레이저의 제어방식을 RTC 확장포트 1번으로 Guide 레이저 제어, RTC 확장포트 2번으로 8비트 조합으로 레이저 파워 제어
    public class DemoIPGYLPTypeE : IPGYLPTypeE
    {
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Control")]
        [DisplayName("Guide Laser")]
        [Description("Guide 레이저 사용 유무")]
        public override bool IsGuideOn
        {
            get
            {
                //RTC 확장1 포트를 이용해 가이드 레이저 온오프 한다는 가정
                return this.RtcDOutExt1.IsOutOn(9);
            }
            set
            {
                if (value)
                {
                    //RTC 확장1 포트를 이용해 가이드 레이저 온오프 한다는 가정
                    this.RtcDOutExt1.OutOn(9);
                }
                else
                {
                    //RTC 확장1 포트를 이용해 가이드 레이저 온오프 한다는 가정
                    this.RtcDOutExt1.OutOff(9);
                }
                this.RtcDOutExt1.Update();
            }
        }
        /// <summary>
        /// RTC 확장1 입력포트 
        /// </summary>
        [Browsable(false)]
        public virtual IDInput RtcDInExt1 { get; set; }

        /// <summary>
        /// RTC 확장1 출력포트 
        /// </summary>
        [Browsable(false)]
        public virtual IDOutput RtcDOutExt1 { get; set; }

        /// <summary>
        /// RTC 확장2 출력포트 
        /// </summary>
        [Browsable(false)]
        public virtual IDOutput RtcDOutExt2 { get; set; }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="index"></param>
        /// <param name="name"></param>
        /// <param name="comPort"></param>
        /// <param name="maxPowerWatt"></param>
        public DemoIPGYLPTypeE(int index, string name, uint comPort, float maxPowerWatt=20)
            : base(index, name, comPort, maxPowerWatt)
        { }

        /// <summary>
        /// 파워 변경 (즉시 명령)
        /// </summary>
        /// <param name="watt"></param>
        /// <param name="powerMapCategory">파워맵 룩업 대상 카테고리</param>
        /// <returns></returns>
        public override bool CtlPower(float watt, string powerMapCategory = "")
        {
            Debug.Assert(this.MaxPowerWatt > 0);

            bool success = true;
            if (watt > this.MaxPowerWatt)
                watt = this.MaxPowerWatt;
            float compensatedWatt = watt;
            if (null != this.PowerMap && !string.IsNullOrEmpty(powerMapCategory))
            {
                if (false == this.PowerMap.Interpolate(powerMapCategory, watt, out compensatedWatt))
                    return false;
            }
            float percentage = compensatedWatt / this.MaxPowerWatt * 100.0f;
            lock (SyncRoot)
            {
                //ext2 를 이용한 방식 / 8비트 해상도로 변환
                ushort data8Bits = (ushort)(percentage / 100.0f * 255.0f);
                success &= this.RtcDOutExt2.SetChannel(0, data8Bits);
                success &= this.RtcDOutExt2.Update();
            }
            if (success)
            {
                prevPowerWatt = watt;
                Logger.Log(Logger.Type.Warn, $"laser [{this.Index}]: trying to change power to {compensatedWatt:F3}W (by 8bits d-out)");
            }
            return success;
        }

        /// <summary>
        /// 파워 변경 (RTC의 리스트 명령)
        /// </summary>
        /// <param name="watt">파워 (W)</param>
        /// <param name="powerMapCategory">파워맵 룩업 대상 카테고리</param>
        /// <returns></returns>
        public override bool ListPower(float watt, string powerMapCategory = "")
        {
            Debug.Assert(this.MaxPowerWatt > 0);

            bool success = true;
            if (watt > this.MaxPowerWatt)
                watt = this.MaxPowerWatt;
            float compensatedWatt = watt;
            if (null != this.PowerMap && !string.IsNullOrEmpty(powerMapCategory))
            {
                if (false == this.PowerMap.Interpolate(powerMapCategory, watt, out compensatedWatt))
                    return false;
            }
            float percentage = compensatedWatt / this.MaxPowerWatt * 100.0f;

            //ext2 를 이용한 방식 / 8비트 해상도로 변환
            ushort data8Bits = (ushort)(percentage / 100.0f * 255.0f);
            success &= this.Rtc.ListWriteData<uint>(ExtensionChannel.ExtDO8, data8Bits);
            success &= this.Rtc.ListWait(0.5f);//500usec for delay
            //success &= this.RtcDOutExt2.SetChannel(0, data8Bits);
            //success &= this.RtcExt2.Update();
            if (success)
            {
                Logger.Log(Logger.Type.Warn, $"laser [{this.Index}]: trying to change power to {compensatedWatt:F3}W");
            }
            return success;
        }
    }
    #endregion

}
