using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiralLab.Sirius
{

    /// <summary>
    /// 사용자 정의 마킹 정보
    /// </summary>
    public class CustomMarkArg
    {
        /// <summary>
        /// X 이동 위치 (mm)
        /// </summary>
        public float X { get; set; }
        /// <summary>
        /// Y 이동 위치 (mm)
        /// </summary>
        public float Y { get; set; }
        /// <summary>
        /// 회전 각도 (deg)
        /// (회전 중심 위치가 어디인지가 필요할 수도 ?)
        /// </summary>
        public float Angle { get; set; }

        /// <summary>
        /// 바코드 데이타
        /// MarkConfig.BarcodeEntityName  이름의 개체를 찾아 변환됨
        /// </summary>
        public string BarcodeEntityData { get; set; }
        /// <summary>
        /// 텍스트 데이타
        /// MarkConfig.TextEntityName  이름의 개체를 찾아 변환됨
        /// </summary>
        public string TextData { get; set; }

        /// <summary>
        /// 사용자 정의 데이타
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// 환경 설정값
        /// 레시피에 설정된 바코드 개체의 이름
        /// </summary>
        public static string BarcodeEntityName { get; set; } = "Barcode";

        /// <summary>
        /// 환경 설정값
        /// 레시피에 설정된 텍스트 개체의 이름
        /// </summary>
        public static string TextEntityName { get; set; } = "Text";
    }
}
