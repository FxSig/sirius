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
 *             `---'.|    '---'   |   |.'    `--''                              `--''          |   | ,'    
 *                `---`            `---'                                                        `----'   
 * Copyright(C) 2020 hong chan, choi. labspiral@gmail.com
 * Copyright (C) 2010-2020 SpiralLab. All rights reserved.
 * MarkerFCEU
 * Description : 
 * Author : hong chan, choi / labspiral@gmail.com (http://spirallab.co.kr)
 * 
 */

using SpiralLab.Sirius.FCEU;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    /// <summary>
    /// 마커 객체 (MarkerFCEU)
    /// </summary>
    public class MarkerFCEU : MarkerDefault
    {        
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="index"></param>
        public MarkerFCEU(uint index)
            : base(index)
        {
        }
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="index"></param>
        /// <param name="name"></param>
        public MarkerFCEU(uint index, string name)
            : base(index, name)
        {
            this.Name = name;
        }

        protected override bool LayerWork(int i, int j, Layer layer)
        {
            #region 추가된 항목
            var refLayerRight = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"LAYER", "REF_RIGHT");
            var refLayerLeft = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"LAYER", "REF_LEFT");
            var defLayerRight = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"LAYER", "DEFECT_RIGHT");
            var defLayerLeft = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"LAYER", "DEFECT_LEFT");
            switch ((SpiralLab.Sirius.FCEU.LaserSequence.Process)Tag)
            {
                case SpiralLab.Sirius.FCEU.LaserSequence.Process.Ref_Right: //right
                    if (layer.Name.IndexOf(refLayerRight, StringComparison.OrdinalIgnoreCase) < 0)
                        return true;
                    break;
                case SpiralLab.Sirius.FCEU.LaserSequence.Process.Ref_Left: //left
                    if (layer.Name.IndexOf(refLayerLeft, StringComparison.OrdinalIgnoreCase) < 0)
                        return true;
                    break;
                case SpiralLab.Sirius.FCEU.LaserSequence.Process.Defect_Right: //right
                    if (layer.Name.IndexOf(defLayerRight, StringComparison.OrdinalIgnoreCase) < 0)
                        return true;
                    break;
                case SpiralLab.Sirius.FCEU.LaserSequence.Process.Defect_Left: //left
                    if (layer.Name.IndexOf(defLayerLeft, StringComparison.OrdinalIgnoreCase) < 0)
                        return true;
                    break;
                default: //SpiralLab.Sirius.FCEU.LaserSequence.Process.Default
                    break;
            }
            #endregion
            return base.LayerWork(i, j, layer);
        }

        protected override bool EntityWork(int i, int j, int k, IEntity entity)
        {
            return base.EntityWork(i, j, k, entity);
        }
    }
}
