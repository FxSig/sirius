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
            var refLayerRight = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"LAYER", "REF_RIGHT");
            var refLayerLeft = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"LAYER", "REF_RIGHT");
            var defLayerRight = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"LAYER", "DEFECT_RIGHT");
            var defLayerLeft  = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"LAYER", "DEFECT_LEFT");

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
                    if (layer.Name.IndexOf(defLayerLeft, StringComparison.OrdinalIgnoreCase) < 0)
                        return true;
                    break;
                case SpiralLab.Sirius.FCEU.LaserSequence.Process.Defect_Left: //left
                    if (layer.Name.IndexOf(defLayerLeft, StringComparison.OrdinalIgnoreCase) < 0)
                        return true;
                    break;
                default: //null or 0
                    break;
            }
            //if (!layer.IsMarkerable)
            //    return true;
            bool success = true;
            var rtc = this.MarkerArg.Rtc;
            var rtcAlc = rtc as IRtcAutoLaserControl;
            var laser = this.MarkerArg.Laser;
            var motorZ = this.MarkerArg.MotorZ;
            switch (layer.MotionType)
            {
                case MotionType.ScannerOnly:
                case MotionType.StageOnly:
                case MotionType.StageAndScanner:
                    #region 레이어에 설정된 ALC 설정 적용 (스케일 보정 파일및 모드 설정)
                    if (null != rtcAlc)
                    {
                        rtcAlc.AutoLaserControlByPositionFileName = layer.AlcPositionFileName;
                        rtcAlc.AutoLaserControlByPositionTableNo = layer.AlcPositionTableNo;
                        switch (layer.AlcSignal)
                        {
                            case AutoLaserControlSignal.ExtDO16:
                            case AutoLaserControlSignal.ExtDO8Bit:
                                success &= rtcAlc.CtlAutoLaserControl<uint>(layer.AlcSignal, layer.AlcMode, (uint)layer.AlcPercentage100, (uint)layer.AlcMinValue, (uint)layer.AlcMaxValue);
                                break;
                            default:
                                success &= rtcAlc.CtlAutoLaserControl<float>(layer.AlcSignal, layer.AlcMode, (uint)layer.AlcPercentage100, (uint)layer.AlcMinValue, (uint)layer.AlcMaxValue);
                                break;
                        }
                    }
                    #endregion

                    #region 매 레이어마다 RTC 리스트 명령 실행
                    success &= rtc.ListBegin(laser);
                    for (int k = 0; k < layer.Count; k++)
                    {
                        var entity = layer[k];
                        success &= this.EntityWork(i, j, k, entity);
                        if (!success)
                            break;
                    }
                    if (success)
                    {
                        success &= rtc.ListEnd();
                        success &= rtc.ListExecute(true);
                    }
                    #endregion
                    break;
            }//end of switch
            return success;
        }
    }
}
