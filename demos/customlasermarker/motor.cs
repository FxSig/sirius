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
        public uint No { get; set; }
        public string Name { get; set; }
        public float Position { get; set; }
        public bool IsHomed { get; set; }
        public bool IsDriving { get; set; }
        public bool IsAlarm { get; set; }
        public bool IsOrg { get; set; }
        public bool IsCw { get; set; }
        public bool IsCcw { get; set; }
        public object Tag { get; set; }


        public MotorZ()
        {
            No = 0;
            Name = "Scanner Z Axis";
            Position = -1;
            IsHomed = false;
            IsDriving = false;
            IsAlarm = false;
            IsOrg = false;
            IsCw = IsCcw = false;

        }
        public bool HomeSearch()
        {
            IsHomed = false;
            IsDriving = true;
            IsCw = IsCcw = false;
            Thread.Sleep(1000);
            Position = 0;
            IsAlarm = false;
            IsOrg = false;
            IsHomed = true;
            IsDriving = false;
            return true;
        }

        public bool MoveAbs(float position)
        {
            this.IsDriving = true;
            Thread.Sleep(1000);
            this.Position = position;
            this.IsDriving = false;
            return true;
        }

        public bool MoveRel(float distance)
        {
            this.IsDriving = true;
            Thread.Sleep(500);
            this.Position += distance;
            this.IsDriving = false;
            return true;
        }

        public bool MoveStop()
        {
            this.IsDriving = false;
            this.IsAlarm = true;
            return true;
        }

        public bool Reset()
        {
            this.IsAlarm = false;
            return true;
        }
    }
}
