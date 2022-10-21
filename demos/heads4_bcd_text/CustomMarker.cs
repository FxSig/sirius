using SpiralLab.Sirius;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SpiralLab.Sirius
{
    /// <summary>
    /// 마커 상속 구현을 통해 개별 엔티티 가공시점에 데이타 조작 지원
    /// </summary>
    public class CustomMarker : MarkerDefault
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="index">0,1,2,3</param>
        public CustomMarker(int index)
            : base(index)
        { }
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="index">0,1,2,3</param>
        /// <param name="name"></param>
        public CustomMarker(int index, string name)
            :  base(index, name)    
        {
        }


        /// <summary>
        /// 엔티티 가공 (레이어 내의 매 엔티티 마다 호출됨)
        /// </summary>
        /// <param name="i">오프셋 번호</param>
        /// <param name="j">레이어 번호</param>
        /// <param name="k">엔티티 번호</param>
        /// <param name="entity">엔티티 객체</param>
        /// <returns></returns>
        protected override bool EntityWork(int i, int j, int k, IEntity entity)
        {
            // 현재 가공 인자
            var arg = base.MarkerArg;

            // 가공 인자의 오프셋 정보 추출
            var offset = arg.Offsets[i];
            // 오프셋 tag 에 추가 정보 전달함
            (string barcodeData, string textData)? tuple = ((string, string)?)offset.UserData;

            // 바코드 개체 이름과 같을 경우
            if (0 == string.CompareOrdinal(CustomMarkArg.BarcodeEntityName, entity.Name.Trim()))
            {
                // 변경해야 할 데이타를 지정 한 경우
                if (null != tuple && tuple.HasValue)
                {
                    // 바코드 객체를 복제하여 데이타 변경
                    var bcdEntityCloned = (IEntity)((ICloneable)entity).Clone();
                    if (bcdEntityCloned is ITextChangeable textChangeable)
                    {
                        // 데이타 변경
                        textChangeable.TextData = tuple.Value.barcodeData;
                    }
                    // 바코드 객체 가공 시작
                    // 오프셋 위치 변환 행렬은 사전에 자동 적용됨
                    bcdEntityCloned.Regen();
                    if (bcdEntityCloned is IMarkerable markerable)
                        return markerable.Mark(arg);
                }
                else
                    // 데이타 수정하지 않고 원본 가공
                    return base.EntityWork(i, j, k, entity);

            }
            // 텍스트 개체 이름과 같을 경우
            else if (0 == string.CompareOrdinal(CustomMarkArg.TextEntityName, entity.Name.Trim()))
            {
                // 변경해야 할 데이타를 지정 한 경우
                if (null != tuple && tuple.HasValue)
                {
                    // 텍스트 객체를 복제하여 데이타 변경
                    var textEntityCloned = (IEntity)((ICloneable)entity).Clone();
                    if (textEntityCloned is ITextChangeable textChangeable)
                    {
                        // 데이타 변경
                        textChangeable.TextData = tuple.Value.textData;
                       
                    }
                    // 텍스트 객체 가공 시작
                    // 오프셋 위치 변환 행렬은 사전에 자동 적용됨
                    textEntityCloned.Regen();
                    if (textEntityCloned is IMarkerable markerable)
                        return markerable.Mark(arg);
                }
                else
                    // 데이타 수정하지 않고 원본 가공
                    // 오프셋 위치 변환 행렬은 사전에 자동 적용됨
                    return base.EntityWork(i, j, k, entity);
            }

            // 데이타 수정하지 않고 원본 가공
            // 오프셋 위치 변환 행렬은 사전에 자동 적용됨
            return base.EntityWork(i, j, k, entity);
        }
    }
}
