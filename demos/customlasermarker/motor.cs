using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpiralLab.Sirius;

namespace SpiralLab.Sirius
{
    //Z Axis Motor
    public class MotorZ : SpiralLab.Sirius.IMotor
    {
        public object SyncRoot { get; private set; }
        public uint No { get; set; }
        public string Name { get; set; }
        public float Position { get; private set; }
        public bool IsReady { get; private set; }
        public bool IsBusy { get; private set; }
        public bool IsError { get; private set; }
        public object Tag { get; set; }

        public MotorZ()
        {
            SyncRoot = new object();
            No = 0;
            Name = "Scanner Z Axis";
            Position = -1;
            IsReady = false;
            IsBusy = false;
            IsError = true;
        }
        public bool CtlHomeSearch()
        {
            IsReady = true;
            IsError = false;
            Position = 0;
            return true;
        }

        public bool CtlMoveAbs(float position)
        {
            Thread.Sleep(100);
            this.Position = position;
            return true;
        }

        public bool CtlMoveRel(float distance)
        {
            Thread.Sleep(100);
            this.Position += distance;
            return true;
        }

        public bool CtlMoveStop()
        {
            this.IsReady = false;
            return true;
        }

        public bool CtlReset()
        {
            this.IsReady = true;
            this.IsError = false;
            return true;
        }
    }
}
