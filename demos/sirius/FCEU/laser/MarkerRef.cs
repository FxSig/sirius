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
 * marker with x,y, angle offsets
 * Description : 
 * Author : hong chan, choi / labspiral@gmail.com (http://spirallab.co.kr)
 * 
 */

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
    /// 마커 객체 (Ref 마킹용) Tag 1 or 2 로 구분
    /// </summary>
    public class MarkerRef : MarkerDefault
    {        
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="index"></param>
        public MarkerRef(uint index)
            : base(index)
        {
        }
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="index"></param>
        /// <param name="name"></param>
        public MarkerRef(uint index, string name)
            : base(index, name)
        {
            this.Name = name;
        }

        protected override bool LayerWork(int i, int j, Layer layer)
        {
            switch((string)Tag)
            {
                case "1": //right
                    if (layer.Name.IndexOf("Ref1", StringComparison.OrdinalIgnoreCase) < 0)
                        return true;
                    break;
                case "2": //left
                    if (layer.Name.IndexOf("Ref2", StringComparison.OrdinalIgnoreCase) < 0)
                        return true;
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
