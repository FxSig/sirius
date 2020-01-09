
using System;
using System.Diagnostics;
using RTC5Import;
using System.Numerics;
using System.IO;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    /// <summary>
    /// custom user rtc class
    /// </summary>
    public class RtcCustom
        : IRtc
    {
        public uint Index { get; set; }
        public string Name { get; set; }
        public Form Form { get; set; }
        public MatrixStack MatrixStack { get; set; }
        public float KFactor { get; set; }
        public bool IsMOTF { get; set; }
        public bool Is2ndHead { get; set; }
        public bool Is3D { get; set; }
        public bool IsScanAhead { get; set; }
        public bool IsUFPM { get; set; }
        public bool IsSyncAxis { get; set; }
        public string[] CorrectionFiles
        {
            get
            {
                return ctbFileName;
            }
        }
        private string[] ctbFileName = new string[4 + 1];
        public CorrectionTableIndex PrimaryHeadTable { get; set; }
        public CorrectionTableIndex SecondaryHeadTable { get; set; }
        protected StreamWriter stream;
        protected string outputFileName;
        protected bool aborted;
        protected bool busy;
        private bool disposed = false;

        public RtcCustom(uint index, string outputFileName = "")
        {
            this.Index = index;
            this.MatrixStack = new MatrixStack();
            this.outputFileName = outputFileName;
            this.Form = new RtcCustomForm(this);
        }
        ~RtcCustom()
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
                this.stream?.Dispose();
            this.disposed = true;
        }
        public bool Initialize(float kFactor, LaserMode laserMode, string ctbFileName)
        {
            Debug.Assert(kFactor > 0);
            return true;
        }
        public bool CtlLoadCorrectionFile(CorrectionTableIndex tableIndex, string ctbFileName)
        {
            this.ctbFileName[(uint)tableIndex] = ctbFileName;
            return true;
        }
        public bool CtlSelectCorrection(CorrectionTableIndex primaryHeadTableIndex, CorrectionTableIndex secondaryHeadTableIndex = CorrectionTableIndex.TableNone)
        {
            return true;
        }
        public bool CtlLaserControl(PowerXFactor powerXFactor, float powerXValue)
        {
            return true;
        }
        public bool CtlLaserOn()
        {
            return true;
        }
        public bool CtlLaserOff()
        {
            return true;
        }
        public bool CtlMove(Vector2 vPosition)
        {
            return true;
        }
        public bool CtlFrequency(float frequency, float pulseWidth)
        {
            return true;
        }
        public bool CtlDelay(float laserOn, float laserOff, float scannerJump, float scannerMark, float scannerPolygon)
        {
            return true;
        }
        public bool CtlSpeed(float jump, float mark)
        {
            return true;
        }
        public bool CtlGetStatus(RtcStatus status)
        {
            bool result = false;
            switch (status)
            {
                case RtcStatus.Busy:
                    result = this.busy;
                    break;
                case RtcStatus.NotBusy:
                    result = true;
                    break;
                case RtcStatus.List1Busy:
                    result = false;
                    break;
                case RtcStatus.List2Busy:
                    result = false;
                    break;
                case RtcStatus.NoError:
                    result = true;
                    break;
                case RtcStatus.Aborted:
                    result = this.aborted;
                    break;
                case RtcStatus.PositionAckOK:
                    result = true;
                    break;
                case RtcStatus.PowerOK:
                    result = true;
                    break;
                case RtcStatus.TempOK:
                    result = true;
                    break;
            }
            return result;
        }
        public string CtlGetErrMsg(uint errorCode)
        {
            return string.Empty;

        }
        public bool CtlBusyWait()
        {
            return true;
        }
        public bool CtlAbort()
        {
            this.aborted = true;
            this.stream?.Flush();
            this.stream?.Dispose();
            this.stream = null;
            this.busy = false;
            return true;
        }
        public bool CtlReset()
        {
            this.aborted = false;
            return true;
        }
        public bool CtlWriteData<T>(ExtensionChannel ch, T value)
        {
            return true;
        }

        public bool ListBegin(ILaser laser, MotionType motionType)
        {
            Core.GenuineTest();
            if (!string.IsNullOrEmpty(this.outputFileName))
            {
                this.stream?.Dispose();
                this.stream = new StreamWriter(this.outputFileName);
            }
            this.aborted = false;
            stream?.WriteLine($"; LIST HAS BEGAN : {DateTime.Now.ToString()}");          
            return true;
        }
        public bool ListWriteData<T>(ExtensionChannel ch, T value)
        {
            stream?.WriteLine($"DATA : {ch.ToString()} : {value.ToString()}");
            return true;
        }
        public bool ListFrequency(float frequency, float pulseWidth)
        {
            if (this.CtlGetStatus(RtcStatus.Aborted))
                return false;
            stream?.WriteLine($"FREQUENCY_HZ = {frequency:F3}");
            stream?.WriteLine($"PULSE_WIDTH_US = {pulseWidth:F3}");
            return true;
        }
        public bool ListDelay(float laserOn, float laserOff, float scannerJump, float scannerMark, float scannerPolygon)
        {
            if (this.CtlGetStatus(RtcStatus.Aborted))
                return false;
            stream?.WriteLine($"LASER_ON_DELAY_US = {laserOn:F3}");
            stream?.WriteLine($"LASER_OFF_DELAY_US = {laserOff:F3}");
            stream?.WriteLine($"SCANNER_JUMP_DELAY_US = {scannerJump:F3}");
            stream?.WriteLine($"SCANNER_MARK_DELAY_US = {scannerMark:F3}");
            stream?.WriteLine($"SCANNER_POLYGON_DELAY_US = {scannerPolygon:F3}");
            return true;
        }
        public bool ListSpeed(float jump, float mark)
        {
            if (this.CtlGetStatus(RtcStatus.Aborted))
                return false;
            stream?.WriteLine($"SCANNER_JUMP_SPEED_MM_S = {jump:F3}");
            stream?.WriteLine($"SCANNER_MARK_SPEED_MM_S = {mark:F3}");
            return true;
        }
        public bool ListWait(float msec)
        {
            if (this.CtlGetStatus(RtcStatus.Aborted))
                return false;
            stream?.WriteLine($"WAIT_DURING_MS = {msec:F6}");
            return true;
        }
        public bool ListLaserOn(float msec)
        {
            stream?.WriteLine($"LASER_ON_DURING_MS = {msec:F6}");
            return true;
        }
        public bool ListLaserOn()
        {
            stream?.WriteLine($"LASER_ON");
            return true;
        }
        public bool ListLaserOff()
        {
            //stream?.WriteLine($"LASER_OFF");
            stream?.WriteLine($"S0");
            return true;
        }
        public bool ListLaserControl(float startValue)
        {
            if (this.CtlGetStatus(RtcStatus.Aborted))
                return false;
            return true;
        }
        public bool ListSkyWriting(float laserOnShift_usec, float timeLag_usec, float angularLimit)
        {
            return true;
        }
        public bool ListPixelLine(float usec, ExtensionChannel ext, Vector2 vDelta, uint pixelCount)
        {
            return true;
        }
        public bool ListPixel(float usec, float voltage = 0.0f)
        {
            return true;
        }
        public bool ListWobbel(float amplitudeX, float amplitudeY, float frequencyHz)
        {
            return true;
        }
        public bool ListJump(Vector2 vPosition, float rampingFactor = 1.0f)
        {
            if (this.CtlGetStatus(RtcStatus.Aborted))
                return false;
            Vector2 v = Vector2.Transform(vPosition, this.MatrixStack.ToResult);
            this.stream?.WriteLine($"JUMP_TO = {v.X:F3}, {v.Y:F3}");
            return true;
        }
        public bool ListMark(Vector2 vPosition, float rampingFactor = 1.0f)
        {
            if (this.CtlGetStatus(RtcStatus.Aborted))
                return false;
            Vector2 v = Vector2.Transform(vPosition, this.MatrixStack.ToResult);
            stream?.WriteLine($"MARK_TO = {v.X:F3}, {v.Y:F3}");
            return true;
        }
        public bool ListArc(Vector2 vCenter, float sweepAngle, float rampingFactor = 1.0f)
        {
            if (this.CtlGetStatus(RtcStatus.Aborted))
                return false;
            Vector2 v = Vector2.Transform(vCenter, this.MatrixStack.ToResult);
            stream?.WriteLine($"ARC_BY_CENTER = {v.X:F3}, {v.Y:F3}, SWEEP_ANGLE = {sweepAngle:F3}");
            return true;
        }
        public bool ListEnd()
        {
            stream?.WriteLine($"; LIST ENDED : {DateTime.Now.ToString()}");
            return true;
        }
        public bool ListExecute(bool busyWait = true)
        {
            if (this.CtlGetStatus(RtcStatus.Aborted))
                return false;
            this.busy = true;
            stream?.Flush();
            stream?.Dispose();
            stream = null;

            /// busy during 1 secs by forcily
            var timer = Stopwatch.StartNew();
            do
            {
                if (this.CtlGetStatus(RtcStatus.Aborted))
                    break;
                System.Threading.Thread.Sleep(10);
            } while (timer.ElapsedMilliseconds < 1000); 
            this.busy = false;
            return !this.CtlGetStatus(RtcStatus.Aborted);
        }        
    }
}
