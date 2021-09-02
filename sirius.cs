/*
 *                                                              ,--,      ,--,                              
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
 * Copyright (C) 2020-2021 SpiralLab. All rights reserved.
 * Description : spirallab.sirius lib
 * Author : hong chan, choi / labspiral@gmail.com (http://spirallab.co.kr)
 * 
 * 
 * 
 *                                                                                                           ILaser        -----------  {spirallab.sirius.rtc.dll} --------------- IMotor/IDInput/IDOutput
 *    {spirallab.sirius.dll}                                                                          (레이저소스 인터페이스)                       |
 *                                                                                                            /|\ 
 *        IDocument                                                              [IShutter] [IPowerControl]    |                                   IRtc  
 *   (문서 인터페이스)                                                                                         |                              (RTC 인터페이스)
 *          /|\                                                                                                |                                   /|\
 *           |                                                                             IPGYLPTypeE         |  IPGYLPTypeD                       |   [IRtcExtension] [IRtc3D] [IRtcDualHead] [IRtcMOTF] [ISyncAxis]
 *           |                                                                                  JPTTypeE       |   CoherentAviaLX                   | (RTC 확장 인터페이스, 3D 가공 인터페이스, 듀얼헤드 인터페이스, MOTF 인터페이스)
 *           |                                                                           PhotonicsIndustryDX   |   AdvancedOptoWaveFotia            |
 *           |                                                                                SPIG4            |                                    |
 *           |                                                                                                 |                                    |
 *    DocumentDefault ----------------- Action (Undo/Redo 를 처리하기 위한 액션 담당)                          |                       Rtc4, Rtc5, Rtc6, RtcSyncAxis...
 *    (기본 문서)                       Blocks (AutoCad 의 Block 집합 (BlockInsert 용))                   (레이저 소스)                       (스캐너 제어)
 *                                      Layers (AutoCad 의 Layer 집합) ---------------------------|
 *                             |------- View  (뷰 : 화면)                                         |
 *                             |                                                                  |
 *                             |                                                                  |
 *                             |                                                                  |
 *                             |                                                                  |
 *                             |                                                                  |
 *                             |                                                                  |
 *         IView  <------------|                          IMarker                               Layer ----------- IEntity   
 *     (뷰 인터페이스)                                (마커 인터페이스)                        (레이어)     (엔티티 인터페이스)
 *          /|\                                             /|\                                                     /|\    
 *           |                                               |                                                       |      [IMarkerable] [ICloneable] [IDrawable] [IExplodable] [IHatchable] [IScriptable]
 *           |                                               |                                                       |                      (마킹 / 복제 / 그리기 / 분해 / 해치 / 스크립트)
 *           |                                               |                                                       |
 *           |                                               |                                                       |
 *           |                                               |                                                       |
 *           |                                               |                                                       |
 *           |                                               |                                                       |
 *      ViewDefault                                     MarkerDefault               Point Line Arc Circle Rectangle Spiral LwPolyline Trepean Text ...
 *      (기본 뷰)                                        (기본 마커)                         (점 선 호 원 사각형 나선 폴리라인 트래팬 텍스트)
 *                                                                                                 BarcodeDataMatrix Timer Group                IPen
 *                                                                                                 (바코드 타이머 그룹)                   (펜 인터페이스)
 *                                                                                                                                               /|\
 *                                                                                                                                                |
 *                                                                                                                                                |
 *                                                                                                                                                |
 *                                                                                                                                            PenDefault         
 *                                                                                                                                            (기본 펜)
 * 
 * Winform 관련
 * 
 *   SiriusViewForm (사용자 컨트롤)
 *   SiriusEditorForm (사용자 컨트롤)              
 * 
 */
