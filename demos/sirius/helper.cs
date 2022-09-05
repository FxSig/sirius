using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SpiralLab.Sirius
{

    #region Enum 에서 Description 속성 추출하는 기능

    /// <summary>
    /// 열거형 데이타에서 속성(Description 정보 추출
    /// 에러/경고 에 대한 문자열 변환에 사용됨
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class DescriptionAttributes<T>
    {
        protected List<DescriptionAttribute> Attributes = new List<DescriptionAttribute>();
        public List<string> Descriptions { get; set; }

        public DescriptionAttributes()
        {
            RetrieveAttributes();
            Descriptions = Attributes.Select(x => x.Description).ToList();
        }

        private void RetrieveAttributes()
        {
            foreach (var attribute in typeof(T).GetMembers().SelectMany(member => member.GetCustomAttributes(typeof(DescriptionAttribute), true).Cast<DescriptionAttribute>()))
            {
                Attributes.Add(attribute);
            }
        }
    }
    #endregion
}
