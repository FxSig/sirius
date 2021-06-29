/*
 *                                                             ,--,      ,--,                              
 *              ,-.----.                                     ,---.'|   ,---.'|                              
 *    .--.--.   \    /  \     ,---,,-.----.      ,---,       |   | :   |   | :      ,---,           ,---,.  
 *   /  /    '. |   :    \ ,`--.' |\    /  \    '  .' \      :   : |   :   : |     '  .' \        ,'  .'  \ 
 *  |  :  /`. / |   |  .\ :|   :  :;   :    \  /  ;    '.    |   ' :   |   ' :    /  ;    '.    ,---.' .' | 
 *  ;  |  |--`  .   :  |: |:   |  '|   | .\ : :  :       \   ;   ; '   ;   ; '   :  :       \   |   |  |: | 
 *  |  :  ;_    |   |   \ :|   :  |.   : |: | :  |   /\   \  '   | |__ '   | |__ :  |   /\   \  :   :  :  / 
 *   \  \    `. |   : .   /'   '  ;|   |  \ : |  :  ' ;.   : |   | :.'||   | :.'||  :  ' ;.   : :   |    ;  
 *    `----.   \;   | |`-' |   |  ||   : .  / |  |  ;/  \   \'   :    ;'   :    ;|  |  ;/  \   \|   :     \ 
 *    __ \  \  ||   | ;    '   :  ;;   | |  \ '  :  | \  \ ,'|   |  ./ |   |  ./ '  :  | \  \ ,'|   |   . | 
 *   /  /`--'  /:   ' |    |   |  '|   | ;\  \|  |  '  '--'  ;   : ;   ;   : ;   |  |  '  '--'  '   :  '; | 
 *  '--'.     / :   : :    '   :  |:   ' | \.'|  :  :        |   ,/    |   ,/    |  :  :        |   |  | ;  
 *    `--'---'  |   | :    ;   |.' :   : :-'  |  | ,'        '---'     '---'     |  | ,'        |   :   /   
 *              `---'.|    '---'   |   |.'    `--''                              `--''          |   | ,'    
 *                `---`            `---'                                                        `----'   
 *                
 * Copyright(C) 2020 hong chan, choi. labspiral@gmail.com
 * Copyright (C) 2010-2020 SpiralLab. All rights reserved.
 * default document 
 * Description : draft version
 * Author : hong chan, choi / labspiral@gmail.com (http://spirallab.co.kr)
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SpiralLab.Sirius
{
    /// <summary>
    /// DocumentDefault 객체를 확장한 커스텀 문서
    /// 주의) SpiralLab.Sirius 네임 스페이지 유지해야 함
    /// </summary>
    public class DocumentFCEU : DocumentDefault
    {

        #region 전용 문서의 추가 데이타 들
        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Defect Hatch")]
        [DisplayName("Hatch")]
        [Description("Hatch 여부")]
        public bool IsHatchable { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Defect Hatch")]
        [DisplayName("Hatch Mode")]
        [Description("Hatch Mode")]
        public HatchMode HatchMode { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Defect Hatch")]
        [DisplayName("Hatch Angle")]
        [Description("Hatch Angle")]
        public float HatchAngle { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Defect Hatch")]
        [DisplayName("Hatch Interval")]
        [Description("Hatch Interval = Line Interval (mm)")]
        public float HatchInterval { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Category("Defect Hatch")]
        [DisplayName("Hatch Exclude")]
        [Description("Hatch Exclude = Gap Interval (mm)")]
        public float HatchExclude { get; set; }
        #endregion

        /// <summary>
        /// 생성자
        /// </summary>
        public DocumentFCEU()
            :  base()
        {
            IsHatchable = false;
            HatchMode = HatchMode.Line;
            HatchAngle = 0;
            HatchInterval = 0.2f;
            HatchExclude = 0;
        }
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="name">문서 이름</param>
        public DocumentFCEU(string name)
            : base(name)
        {
        }
     
        /// <summary>
        /// 문서 복제
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            var clonedDoc = new DocumentFCEU
            {
                //Version = this.Version,
                Name = this.Name,
                Description = this.Description,
                FileName = this.FileName,
                //Action 자동생성
                //Dimension 자동생성
                Layers = (Layers)this.Layers.Clone(),
                Blocks = (Blocks)this.Blocks.Clone(),
                Dimension = (BoundRect)this.Dimension.Clone(),
                RotateOffset = this.RotateOffset.Clone(),
                Tag = this.Tag,   
                Views = new List<IView>(this.Views),

                //추가 데이타 복제
                IsHatchable = this.IsHatchable,
                HatchMode = this.HatchMode,
                HatchAngle = this.HatchAngle,
                HatchInterval = this.HatchInterval,
                HatchExclude = this.HatchExclude,
            };
            return clonedDoc;
        }
        
        public override void New()
        {
            base.New();
            //데이타 삭제하지 않고 유지
            //IsHatchable = false;
            //HatchMode = HatchMode.Line;
            //HatchAngle = 0;
            //HatchInterval = 0.2f;
            //HatchExclude = 0;
        }
    }
}
