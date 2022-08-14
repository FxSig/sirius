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

        bool stop = false;
        string category;
        int steps;

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

        public override bool CtlStart(string category, int steps, IPowerMeter powerMeter, IRtc rtc, ILaser laser, uint holdTimeMs = 6000, float thresholdWatt = 0)
        {
            if (this.IsBusy)
            {
                Logger.Log(Logger.Type.Error, $"fail to start powermapping. it's busy running ...");
                return false;
            }

            if (!float.TryParse(category, out float hz))
            {
                Logger.Log(Logger.Type.Error, $"fail to start powermapping. target category is not valid hz= {category}");
                return false;
            }

            if (null == powerMeter || powerMeter.IsError || powerMeter.IsReady)
            {
                Logger.Log(Logger.Type.Error, $"fail to start powermapping. invalid powermeter status");
                return false;
            }
            if (null == laser || laser.IsBusy || laser.IsError || !laser.IsReady)
            {
                Logger.Log(Logger.Type.Error, $"fail to start powermapping. invalid laser status");
                return false;
            }

            if (null == rtc || rtc.CtlGetStatus(RtcStatus.Busy))
            {
                Logger.Log(Logger.Type.Error, $"fail to start powermapping. rtc status is invalid (busy ?)");
                return false;
            }

            var powerControl = laser as SpiralLab.Sirius.IPowerControl;
            if (null == powerControl)
            {
                Logger.Log(Logger.Type.Error, $"fail to start powermapping. laser is not support power control function");
                return false;
            }

            this.stop = false;
            this.category = category;
            this.steps = steps;
            return this.DoPowerMapping();
        }

        protected bool DoPowerMapping()
        {
            bool success = true;
            base.NotifyStarted(this.category, this.steps);

            for (int step = 0; step < this.steps; step++)
            {
                if (stop)
                    break;

                base.NotifyProgress();
                // your power mapping routines
                //
                //
            }
            if (success)
                base.NotifyFinished();
            else
                base.NotifyStopped();
            return success;
        }

        public override bool CtlStop()
        {
            // try to stop
            stop = true;
            return true;
        }

    }
}
