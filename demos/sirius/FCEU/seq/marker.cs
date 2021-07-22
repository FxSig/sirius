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
            var refLayerLeft = NativeMethods.ReadIni<string>(FormMain.ConfigFileName, $"LAYER", "REF_RIGHT");
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
                    if (layer.Name.IndexOf(defLayerLeft, StringComparison.OrdinalIgnoreCase) < 0)
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

            if (!layer.IsMarkerable)
                return true;
            bool success = true;
            var rtc = this.MarkerArg.Rtc;
            var rtcListType = this.MarkerArg.RtcListType;
            if (this.MarkerArg.IsExternalStart)
                rtcListType = ListType.Single; //외부 트리거 사용시 강제로 단일 리스트로 고정
            var rtcAlc = rtc as IRtcAutoLaserControl;
            var laser = this.MarkerArg.Laser;
            var motorZ = this.MarkerArg.MotorZ;
            switch (layer.MotionType)
            {
                case MotionType.ScannerOnly:
                case MotionType.StageOnly:
                case MotionType.StageAndScanner:
                    #region Z 축 모션 제어
                    if (null != motorZ)
                    {
                        success &= motorZ.IsReady;
                        success &= !motorZ.IsBusy;
                        success &= !motorZ.IsError;
                        if (success)
                            success &= motorZ.CtlMoveAbs(layer.ZPosition);
                        if (!success)
                            Logger.Log(Logger.Type.Error, $"marker [{this.Index}] {this.Name}: motor Z invalid position/status");
                    }
                    #endregion
                    if (!success) break;

                    #region 레이어에 설정된 ALC 설정 적용 (스케일 보정 파일및 모드 설정)
                    if (null != rtcAlc && layer.IsALC)
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
                    success &= rtc.ListBegin(laser, rtcListType);
                    for (int k = 0; k < layer.Count; k++)
                    {
                        var entity = layer[k];
                        success &= this.EntityWork(i, j, k, entity);
                        if (!success)
                            break;

                        float progress = (float)progressIndex / (float)this.progressTotal * 100.0f;
                        this.MarkerArg.Progress = progress;
                        base.OnProgressing();
                        progressIndex++;
                    }
                    if (success)
                        success &= rtc.ListEnd();

                    if (success && !this.MarkerArg.IsExternalStart)
                        success &= rtc.ListExecute(true);
                    #endregion
                    break;
            }//end of switch
            return success;
        }
    }
}
