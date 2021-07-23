using System;
using System.ComponentModel;

namespace SpiralLab.Sirius.FCEU
{
    public enum ErrEnum
    {
        None = 0,

        [Description("스캐너및 레이저 초기화에 실패하였습니다")]
        Initialize,

        [Description("레이저 레시피 준비에 실패하였습니다")]
        Recipe,

        [Description("레이저 가공이 이미 진행중입니다")]
        Busy,

        [Description("스캐너 전원 공급이 되지 않습니다")]
        ScannerPower,

        [Description("스캐너 위치 응답 오류가 발생되었습니다")]
        ScannerPosAck,

        [Description("레이저 소스가 알람 상태입니다")]
        LaserError,

        [Description("가공을 할 데이타가 존재하지 않습니다")]
        NoEntitiesToMark,

        [Description("레이저 가공에 실패하였습니다")]
        FailToMark,

        [Description("비전 시스템으로 부터 가공 정보을 읽을 수 없습니다")]
        VisionDataOpen,

        [Description("비전 시스템으로 부터 스캐너 보정 정보을 읽을 수 없습니다")]
        VisionFieldCorrectionOpen,

        [Description("불량 정보 업데이트용 레이어가 없습니다")]
        NoDefectLayer,
    }

    public enum WarnEnum
    {
        None = 0,

        [Description("레시피 변경이 완료되었습니다")]
        RecipeChanged,

        [Description("비전 시스템과 통신이 연결되지 않았습니다")]
        VisionCommunication,

        [Description("자동운전 화면으로 전환해 주십시오")]
        Auto,

        [Description("레이저 가공 시작을 대기중 입니다")]
        Ready,

        [Description("레이저 가공을 시작합니다")]
        StartingToMark,

        [Description("레이저 가공을 중단합니다")]
        StoppingToMark,

        [Description("레이저 가공을 진행중입니다")]
        Busy,

        [Description("레이저 가공이 완료되었습니다")]
        FinishToMark,

        [Description("시스템 티칭용 가공을 시작합니다")]
        SystemTeachToMark,

        [Description("스캐너 필드 보정을 위한 가공을 시작합니다")]
        ScannerFieldCorrectionToMark,

        [Description("우측 기준 도면 마킹을 시작합니다")]
        ReferenceMarkRight,

        [Description("좌측 기준 도면 마킹을 시작합니다")]
        ReferenceMarkLeft,

        [Description("비전 시스템으로 부터 가공 정보을 전달받았습니다")]
        VisionDataOpen,

        [Description("스캐너 필드 보정 작업이 진행중입니다")]
        ScannerFieldCorrectioning,

        [Description("새로운 스캐너 필드 보정 파일이 적용됨")]
        ScannerFieldCorrectionChanged,

        [Description("우측 가공을 시작합니다")]
        DefectMarkRight,

        [Description("좌측 가공을 시작합니다")]
        DefectMarkLeft,

    }


    /// <summary>
    /// 속성 헬퍼 클래스
    /// 열거형 데이타에 대한 사용자 정의 속성을 확장하기 위한 헬퍼
    /// </summary>
    public static class AttrHelper
    {
        /// <summary>
        /// 열거형 멤버에 지정된 [Description("정보")] 정보를 읽어온다
        /// </summary>
        /// <param name="e">열거형 멤버</param>
        /// <returns></returns>
        public static DescriptionAttribute Description(Enum e)
        {
            DescriptionAttribute attr = GetAttributeOfType<DescriptionAttribute>(e);
            return attr;
        }       

        /// <summary>
        /// Gets an attribute on an enum field value
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="enumVal">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        /// <example><![CDATA[string desc = myEnumVariable.GetAttributeOfType<DescriptionAttribute>().Description;]]></example>
        internal static T GetAttributeOfType<T>(this Enum enumVal) where T : System.Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }
    }
}