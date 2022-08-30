using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpiralLab.Sirius
{
    /// <summary>
    /// 사용자 커스텀 한 파워맵
    /// </summary>
    public class PowerMapUserDefined : PowerMapDefault
    {

        /// <summary>
        /// 생성자
        /// </summary>
        public PowerMapUserDefined()
            : base()
        {
        }
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="index">식별 번호</param>
        /// <param name="name">이름</param>
        /// <param name="xName">X 항목 이름</param>
        public PowerMapUserDefined(int index, string name, string xName)
            : this()
        {
            this.Index = index;
            this.Name = name;
            this.XName = xName;
        }

        public override bool CtlStart(IPowerMapArg powerMapStartArg)
        {
            if (this.IsBusy)
            {
                this.IsError = true;
                Logger.Log(Logger.Type.Error, $"powermap [{this.Index}]: fail to start powermapping. it's busy running ...");
                return false;
            }
            if (null == powerMapStartArg.Categories || 0 == powerMapStartArg.Categories.Length)
            {
                this.IsError = true;
                Logger.Log(Logger.Type.Error, $"powermap [{this.Index}]: fail to start powermapping. target categories is not valid");
                return false;
            }
            foreach (var category in powerMapStartArg.Categories)
            {
                if (!float.TryParse(category, out float hz))
                {
                    this.IsError = true;
                    Logger.Log(Logger.Type.Error, $"powermap [{this.Index}]: fail to start powermapping. target category is not valid hz= {category}");
                    return false;
                }
            }
            if (null == powerMapStartArg.PowerMeter || powerMapStartArg.PowerMeter.IsError || !powerMapStartArg.PowerMeter.IsReady)
            {
                this.IsError = true;
                Logger.Log(Logger.Type.Error, $"powermap [{this.Index}]: fail to start powermapping. invalid powermeter status");
                return false;
            }
            if (null == powerMapStartArg.Laser || powerMapStartArg.Laser.IsBusy || powerMapStartArg.Laser.IsError || !powerMapStartArg.Laser.IsReady)
            {
                this.IsError = true;
                Logger.Log(Logger.Type.Error, $"powermap [{this.Index}]: fail to start powermapping. invalid laser status");
                return false;
            }
            if (null == powerMapStartArg.Rtc || powerMapStartArg.Rtc.CtlGetStatus(RtcStatus.Busy))
            {
                this.IsError = true;
                Logger.Log(Logger.Type.Error, $"powermap [{this.Index}]: fail to start powermapping. rtc status is invalid (busy ?)");
                return false;
            }
            var powerControl = powerMapStartArg.Laser as SpiralLab.Sirius.IPowerControl;
            if (null == powerControl)
            {
                this.IsError = true;
                Logger.Log(Logger.Type.Error, $"powermap [{this.Index}]: fail to start powermapping. laser is not support power control function");
                return false;
            }


            return this.YourDoPowerMapping(powerMapStartArg);
        }

        bool YourDoPowerMapping(IPowerMapArg arg)
        {
            bool success = true;
            base.NotifyMappingStarted(arg);
            this.IsBusy = true;
            foreach (var category in arg.Categories)
            {
                for (int step = 0; step < arg.Steps; step++)
                {

                    base.NotifyMappingProgress(arg);
                    // your codes ...
                    //
                    //
                }
            }
            if (success)
                base.NotifyMappingFinished(arg);
            else
                base.NotifyMappingFailed(arg);

            this.IsBusy = false;
            return success;
        }

        public override bool CtlVerify(IPowerVerifyArg powerMapVerifyArg)
        {
            if (this.IsBusy)
            {
                this.IsError = true;
                Logger.Log(Logger.Type.Error, $"powermap [{this.Index}]: fail to start verify power. it's busy running ...");
                return false;
            }
            if (null == powerMapVerifyArg.CategoryAndTargetWatts || 0 == powerMapVerifyArg.CategoryAndTargetWatts.Length)
            {
                this.IsError = true;
                Logger.Log(Logger.Type.Error, $"powermap [{this.Index}]: fail to start verify power. target category is not valid");
                return false;
            }
            foreach (var kv in powerMapVerifyArg.CategoryAndTargetWatts)
            {
                if (!float.TryParse(kv.category, out float hz))
                {
                    this.IsError = true;
                    Logger.Log(Logger.Type.Error, $"powermap [{this.Index}]: fail to start verify power. target category is not valid hz= {kv.category}");
                    return false;
                }
            }
            if (null == powerMapVerifyArg.PowerMeter || powerMapVerifyArg.PowerMeter.IsError || !powerMapVerifyArg.PowerMeter.IsReady)
            {
                this.IsError = true;
                Logger.Log(Logger.Type.Error, $"powermap [{this.Index}]: fail to start verify power. invalid powermeter status");
                return false;
            }
            if (null == powerMapVerifyArg.Laser || powerMapVerifyArg.Laser.IsBusy || powerMapVerifyArg.Laser.IsError || !powerMapVerifyArg.Laser.IsReady)
            {
                this.IsError = true;
                Logger.Log(Logger.Type.Error, $"powermap [{this.Index}]: fail to start verify power. invalid laser status");
                return false;
            }
            if (null == powerMapVerifyArg.Rtc || powerMapVerifyArg.Rtc.CtlGetStatus(RtcStatus.Busy))
            {
                this.IsError = true;
                Logger.Log(Logger.Type.Error, $"powermap [{this.Index}]: fail to start verify power. rtc status is invalid (busy ?)");
                return false;
            }
            var powerControl = powerMapVerifyArg.Laser as SpiralLab.Sirius.IPowerControl;
            if (null == powerControl)
            {
                this.IsError = true;
                Logger.Log(Logger.Type.Error, $"powermap [{this.Index}]: fail to start verify power. laser is not support power control function");
                return false;
            }
            return this.YourPowerVerify(powerMapVerifyArg);
        }

        bool YourPowerVerify(IPowerVerifyArg arg)
        {
            bool success = true;
            var powerControl = arg.Laser as SpiralLab.Sirius.IPowerControl;

            this.IsBusy = true;
            this.NotifyVerifyStarted(arg);
            foreach (var kv in arg.CategoryAndTargetWatts)
            {
                float hz = float.Parse(kv.category);
                float targetWatt = kv.watt;
                if (powerControl.CtlPower(targetWatt, kv.category))
                {
                    //your codes ...
                    //
                    //
                }
            }
            this.NotifyVerifyFinished(arg);
            this.IsBusy = false;
            return success;
        }

        public override bool CtlStop()
        {
            return true;
        }

        public override bool CtlReset()
        {
            return true;
        }
    }
}
