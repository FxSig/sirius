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
    /// <summary>
    /// 스캐너 Z 축 모션 제어 사용예제
    /// </summary>
    public class MotorZ : SpiralLab.Sirius.IMotor
    {
        /// <summary>
        /// 동기화 객체
        /// </summary>
        public object SyncRoot { get; private set; }
        /// <summary>
        /// 축 번호
        /// </summary>
        public int No { get; set; }
        /// <summary>
        /// 축 이름
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 위치
        /// </summary>
        public float Position { get; private set; }

        /// <summary>
        /// 모터 준비 상태 여부
        /// </summary>
        public bool IsReady { get; private set; }
        /// <summary>
        /// 모터 동작중 여부
        /// </summary>
        public bool IsBusy { get; private set; }
        /// <summary>
        /// 모터 에러 상태 여부
        /// </summary>
        public bool IsError { get; private set; }
        /// <summary>
        /// 사용자 정의 데이타
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// 생성자
        /// </summary>
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

        public bool Initialize()
        {
            return true;
        }
        /// <summary>
        /// 원점 초기화
        /// </summary>
        /// <returns></returns>
        public bool CtlHomeSearch()
        {
            IsReady = true;
            IsError = false;
            Position = 0;
            return true;
        }
        /// <summary>
        /// 절대 위치 구동
        /// </summary>
        /// <param name="position">위치</param>
        /// <returns></returns>
        public bool CtlMoveAbs(float position, float vel, float acc)
        {
            Thread.Sleep(100);
            this.Position = position;
            return true;
        }
        /// <summary>
        /// 상대 위치 구동
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        public bool CtlMoveRel(float distance, float vel, float acc)
        {
            Thread.Sleep(100);
            this.Position += distance;
            return true;
        }
        /// <summary>
        /// 모션 정지
        /// </summary>
        /// <returns></returns>
        public bool CtlMoveStop()
        {
            this.IsReady = false;
            return true;
        }
        /// <summary>
        /// 에러 해제 시도
        /// </summary>
        /// <returns></returns>
        public bool CtlReset()
        {
            this.IsReady = true;
            this.IsError = false;
            return true;
        }
    }
}
