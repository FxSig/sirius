using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        /// 원점 탐색 완료시 발생되는 이벤트 핸들러
        /// </summary>
        public event EventHandler MotorHomed;
        /// <summary>
        /// INotifyPropertyChanged 인터페이스 구현
        /// 속성값 변경시 발생되는 이벤트 핸들러 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
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
        /// 명령 위치 (unit)
        /// </summary>
        public float CommandPosition { get; private set; }
        /// <summary>
        /// 실제 위치 (unit)
        /// </summary>
        public float ActualPosition { get; private set; }
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
        /// 모터 서보 알람 여부
        /// </summary>
        public bool IsServoAlarm { get; private set; }
        /// <summary>
        /// 모터 홈 초기화 여부
        /// </summary>
        public bool IsHomeSearched { get; private set; }
        /// <summary>
        /// InPos 여부
        /// </summary>
        public bool IsInPos { get; private set; }
        /// <summary>
        /// 모터 서보 온 여부
        /// </summary>
        public bool IsServoOn { get; private set; }
        /// <summary>
        /// 모터 구동중 여부
        /// </summary>
        public bool IsDriving { get; private set; }
        /// <summary>
        /// 모터 - 종단 센서 감지 여부
        /// </summary>
        public bool IsCCwSenOn { get; private set; }
        /// <summary>
        /// 모터 + 종단 센서 감지 여부
        /// </summary>
        public bool IsCwSenOn { get; private set; }
        /// <summary>
        /// 모터 ORG 센서 감지 여부
        /// </summary>
        public bool IsOrgSenOn { get; private set; }

        public float MaxSpeed { get; set; }
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
            IsReady = false;
            IsBusy = false;
            IsError = true;
        }
        public void Dispose()
        { }

        public bool Initialize()
        {
            return true;
        }
        public bool CtlServo(bool onOff)
        {
            IsServoOn = onOff;
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
            IsHomeSearched = true;
            CommandPosition = ActualPosition = 0;
            return true;
        }
        /// <summary>
        /// 절대 위치 구동
        /// </summary>
        /// <param name="position">위치</param>
        /// <returns></returns>
        public bool CtlMoveAbs(float position, float vel)
        {
            Thread.Sleep(100);
            CommandPosition = ActualPosition = position;
            return true;
        }
        /// <summary>
        /// 상대 위치 구동
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        public bool CtlMoveRel(float distance, float vel)
        {
            Thread.Sleep(100);
            CommandPosition += distance;
            ActualPosition += distance;
            return true;
        }
        public bool CtlMoveJog(float vel)
        {
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

        public bool Update()
        {
            return true;
        }
    }
}
