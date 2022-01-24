# SpiralLab.Sirius Library

**1. Descriptions**

 SuperEasy library for Control for Scanner and Lasers

 
 ![pulse on demand](http://www.spirallab.co.kr/wp-content/uploads/2022/01/image-6.png)
 
 ![scanner field correction by image](http://www.spirallab.co.kr/wp-content/uploads/2022/01/image-5.png)
 
 ![scanner field correction](http://www.spirallab.co.kr/wp-content/uploads/2022/01/image-7.png)

 ![script](http://www.spirallab.co.kr/wp-content/uploads/2022/01/image-8.png)

 ![path](http://www.spirallab.co.kr/wp-content/uploads/2022/01/image-9.png)

 ![barcode](http://www.spirallab.co.kr/wp-content/uploads/2021/06/image-21-1024x686-2.png)
 
 ![stitchedimage](http://www.spirallab.co.kr/wp-content/uploads/2022/01/image-11.png)

 ![text with arc](https://user-images.githubusercontent.com/58460570/117915901-215d7e00-b321-11eb-8055-5502aad8bf85.png)

 ![multiple views](http://www.spirallab.co.kr/wp-content/uploads/2022/01/image-10.png)

  ----

**2. Features**

 - support SCANLAB's RTC4, RTC5, RTC6, RTC6 Ethernet, XL-SCAN(SyncAXIS) multiple products. 
 - support RTC control with 3x3 matrix stack operation.
 - support processing unlimited vector data into RTC controller automatically.
 - support RTC's MOTF(marking on the fly), Dual Head, 3D (like as VarioSCAN) features.
 - support laser power control with POD(pulse on demand by analog, 8/16bits digital, frequency, pulse width modulation) and sky-writing
 - support line, arc, LW polyline, rectangle, circle, true type font, cxf custome font, 1D/2D barcodes, spiral, trepan, dxf, hpgl(plt), group for multiple entities and layers.
 - support mark 2D barcodes with dots, lines, outlines, hatch.
 - support powerful undo/redo actions.
 - support 1 source(single document data) with multiple views.
 - support customizable and extensible marker interface.
 - support laser beam path visualizer (simulator) and path optimizer algorithm.
 - support vary laser parameters with 'Pen' (frequency, pulse width, power, speeds, laser delays, sky writing , ...)
 - support SCANLAB's 2D/3D field correction with easy to use and with powerful image analyzer.
 - support RTC motion profile with control signals at real-time and plot to graph.
 - support executable C# script codes.
 - support powermeters and power mapping interface
   - Ophir USBI
   - Thorlab PM100USB
 - support many kinds of commerical laser sources
   - Advanced Optowave Fotia
   - Coherent Avia LX
   - PHOTONICS INDUSTRY DX
   - IPG YLP Type D/E
   - SPI G3/4
   - JPT Type E
 
 ![measurement](http://www.spirallab.co.kr/wp-content/uploads/2022/01/image-13.png)  

 ![path optimizer](http://www.spirallab.co.kr/wp-content/uploads/2022/01/ezgif.com-gif-maker.gif)
 
  ----

**3. How to use ?**

 - Development Environment : .NET dll library 
 - Add references spirallab.core.dll, spirallab.sirius.rtc.dll and spirallab.sirius.dll file into Microsoft Visual Studio.
 - spirallab.sirius.dll file support user control : SpiralLab.Sirius.EditorForm / SpiralLab.Sirius.ViewerForm
 - x64 Environment : copy all files from bin\x64 to bin\  
 - x32 Environment : copy all files from bin\x32 to bin\ 
 - There are multiple demo programs in DEMOS directory
 - There are stand-alone programs in DEMOS\Sirius directory

 *The program running about 3 hours in evalution copy mode !*
 
 ----

**4. Author**

 - e-mail : labspiral@gmail.com
 - homepage : http://spirallab.co.kr                        
 - developer page : http://www.spirallab.co.kr/?page_id=229
 - git repository : https://github.com/labspiral/sirius.git
 - phone : +82-10-9619-3896
 - Please contact to me to use commercially.
  
----

**5. Version history**

* 2022.1.24 v1.9.2
   - fixed) vector-defined laser control bug (벡터 기반 레이저 출력 제어 버그 수정)
   - fixed) simplify automatic laser control by velocity (속도에 의한 레이저 자동 제어 간소화)
   - added) SiriusEditorForm with lock/unlock feature (편집기 내에서 잠금 기능 추가)
   - fixed) syncaxis 
        - added) config dynamics
        - added) job status / job event callback / job characteristic
        - added) on/off simulation or hardware mode / simulation filename 
        - added) get actual postion of stage, scanner(s)
        - added) operation status (green/yellow/red)
        - fixed) improved demo program with editor_syncaxis

* 2022.1.12 v1.9.1
   - added) docs\sirius.pdf document file (사용자 문서 업데이트)
   - added) support powerful measurement of signals (강력한 계측 기능 제공)
     - MeasurementBegin/End entity (MeasurementBegin/End 엔티티 이용)
     - create measurement data automatically and plot to graph in Marker Form (마커 화면에서 옵션사항으로 계측 데이타 취득및 저장, 그래프로 플롯 기능 제공)
   - added) measurement demo project (계측 데모 프로젝트 추가)
   - added) support various way of read/write extension i/o port (확장 포트를 이용한 데이타 읽기/쓰기 조합 지원)
     - write data entity (다양한 확장 포트로 데이타 쓰기)
     - write data ext16 entity (확장 1포트에 개별 비트 쓰기)
     - write data ext16 if entity (확장 1포트 입력 비트 마스크 조건에 따라 출력 비트 마스크에 쓰기)
     - wait data ext16 if entity (확장 1포트 입력 비트 마스크 조건에 따라 대기하기)
    - fixed) improve scanner field correction by image  (이미지를 이용한 스캐너 보정 기능 개선)
    - fixed) property values of timer entity is not shown (타이머 개체의 속성값이 일부 표시되지 않는 문제 해결)

* 2022.1.3 v1.9.0
   - added) image analyzer for scanner field correction (스캐너 보정을 위한 이미지 분석기 추가)
     - added) spirallab.sirius.fieldcorrection.dll (해당 dll 추가됨)
	 - copy x64\OpenCvSharpExtern.dll(or x32) into bin directory (플랫폼에 맞게 해당 파일을 bin 디렉토리에 복사할것)
   - added) laser control demo programs (레이저 제어 활용 데모 코드 추가)

* 2021.12.22 v1.8.9
   - fixed) register fonts(characterset) into rtc bug (폰트 다운로드시 개별 글자의 비율 오류 및 불필요 리스트 생성 버그)

* 2021.12.18 v1.8.8
   - fixed) no nodes at treeview bug (트리뷰에 노드가 업데이트 되지 않는 버그)
   - fixed) encoder pulses/mm can be negate value with motf (MOTF 를 위한 엔코더 스케일값에 음수 입력 가능)

* 2021.12.10 v1.8.7
   - added) support laser signal activate/deactivate at RTC (레이저 신호 활성화/비활성화 기능 제공)
   - added) editable MOTF encoder simulation speed in marker winforms (마커창에서 시뮬레이션 엔코더 속도 입력창 제공)
   - fixed) invalid encoder reset when MOTF begins (MOTF 시작시 엔코더 리셋 여부 활성화 버그 수정)
   - fixed) crash bug when refresh propertygrid at laser winform (레이저 소스폼에서 속성창 리프레쉬 버그)
   - fixed) lag of aborting marker bug (마커에서 중지시 응답지연 버그), marker support measurement event (마커에 계측 이벤트 핸들러 추가됨)
   - fixed) sub entities withing group entity are supported script and register fonts (그룹내에 포함된 스크립트 개체및 폰트 개체 지원됨)
   - added) powermeter/ powermap winforms in SiriusEditorForm (파워메터/파워매핑 윈폼 추가)

* 2021.12.5 v1.8.6
   - fixed) scanner 3d field correction bug (3D 보정시 dat 파일 생성 오류 수정)
   - fixed) readdata/writedata bug in rtc (RTC read/write data 호출시 인자 타입 변환 오류 수정)
   - added) adlink DAQ 입출력 지원
   - added) powermeter interface (파워메터 인터페이스 추가)
     - ophir usbi (OPHIR 사의 USBI 제품)
     - thorlab pm100usb (Thorlabs 사의 PM100USB 제품)
    - added) powermap interface (파워매핑 인터페이스 추가)

* 2021.11.26 v1.8.5
   - updated) Scanlab Rtc5 up-to-date v.2021_10_22 (RTC5 dll 라이브러리 최신 버전으로 업데이트)
   - updated) Scanlab Rtc6 up-to-date v.2021_11_12  (RTC6 dll 라이브러리 최신 버전으로 업데이트)
   - added) Convert correction option with "delete input dat file"  (스캐너 필드 보정시 dat 파일 삭제여부 옵션)
   - fixed) Convert correctionPro.exe bug (reference bits) (CorrectionPro 보정시 기준점 입력 버그 해결)
   - fixed) Reset correction3d z-upper/lower value bug (스캐너 3D 보정시 Z 값 리셋 버그 해결)
   - fixed) Ellipse entity's mark bug - leakage beam at jump started (타원 가공시 시작점 위치의 빔 누수 해결)

* 2021.11.21 v1.8.4
   - added) new scanner correction with (c)SCANLAB Correction Pro (스캔랩의 CorrectionPro 프로그램 기반의 보정 지원)
     - added) Correction2DRtc / Correction2DRtcForm (두개의 신규 클래스 추가됨)
   - fixed) fail to scanner field correct when edge position of area has inputted (X,Y 측정위치가 영역의 가장 끝일 경우 발생하는 에러 해결)

* 2021.11.19 v1.8.3
   - fixed) rtc4 list buffer handling bug, rtc4 CtlWriteData bug (RTC4 사용시 대용량 데이타 유실로 인해 마지막 3천개의 데이타만 가동되는 버그 해결)
   - fixed) readyonly bug at correction3d winforms (Correction3D 윈폼에서 일부 데이타 입력 불가 버그 해결)
   - added) stitched images are saved now (StitchedImage 에 지정된 개별셀 이미지가 파일에 저장 가능해짐)

* 2021.10.29 v1.8.2
   - fixed) mark entities' parameters by pen color and added pen editor 
   - fixed) optimized hatched line path in hatch (text, barcode, ...), hatch with zig zag order
   - fixed) scanner field correction with 3d (ct5, ctb) bugs

* 2021.10.25 v1.8.1
   - fixed) Photonics Industry DX laser supported guide laser by 5% PEC level
   - fixed) support mark target combination (if laser guide on : no mark in internal hatch / possible to override speed and repeat )
   - fixed) wrong mark with barcode entities if shape type are dots
   - fixed) true font type : points are calculated based on distance 
   - added) IMotor with maximum limit of velocity 

* 2021.10.18 v1.8.0
   - fixed) Photonics Industry DX laser communication
   - fixed) IMotor interface and MotorAjinExtek bug
   - fixed) SiriusEditorForm support with DInput/DOutput by IRtcExtension CtlReadData/CtlWriteData 

* 2021.10.04 v1.7.9
   - fixed) sort and merge group entity's path
   - fixed) accuracy of barcode cell's pitch, support cell array size, scale factor
   - fixed) rtc field size for check out of range 
   - fixed) reset rtc's' external start when marker has stopped
   - fixed) laser path simulation with vary speed (short-cut : CTRL + F9,10,11)

* 2021.09.29 v1.7.8
   - updated) icons set
   - added) 1/2d barcode with dot N*N array
   - added) group form with path sort and merge within polylines
   - added) arc with reverse winding
   - added) default laser comm screen in editor
   - fixed) group/ungroup with specific buttons (not by context menu)
   - fixed) working properly keyboard shortcuts 
   - fixed) laser path simulation bug when draw small arc < 0.2 radius

* 2021.09.19 v1.7.7
   - fixed) support drag and drop at listbox (like as group, offset, ...)
   - fixed) arc entity's start/sweep angle with grip point
   - fixed) group entity with continous path (experimental)
   - fixed) barcode entity with hatch scale factor

* 2021.09.11 v1.7.6
   - added) improved selection algorithm by ctrl, shift, alt key combination
   - added) support serial communication with IPG YLP laser source 
   - added) support serial communication with OptoWave Fotia laser source
   - added) support serial communication with Photonics Industry DX laser source
   - added) support serial communication with JPT TypeE laser source
   - added) motor/digital io demo project (based on AJINEXTEK controller)
   - fixed) out of memory exception (at treeview nodes)
   - fixed) ctb/ct5 file extension display bug at field correction winform
   - fixed) hatching within group entity

 * 2021.09.02 v1.7.5
   - added) entity with script (compile and execute by C# codes) : barcode, text, siriustext ,...
   - added) script demo project
   - added) gnuplot for analyze measurement data

 * 2021.08.26 v1.7.4
   - added) scanner field correction with each row/col interval and editable data
   - added) ct5 file header information are queriable
   - added) old ctb format supported
   - added) text/sirius text with aspect ratio
   - added) sizable/rotatable entities with grip points
   - added) automatic laser control entity 
   - fixed) layer repeat counts bug
   - fixed) order of internal hatch lines bug
   - updated) syncaxis v1.6 library

 * 2021.07.22 v1.7.3
   - added) hatch mode (cross line = 2nd hatch angle)
   - added) support divide entities 

 * 2021.07.14 v1.7.2
   - added) path optimizer in group's internal items
   - added) another sirius demo project
   - fixed) rtc list handling bug when auto-list buffer
   - fixed) internal hpgl parser try to create polyline as possible (spirallab.hpgl.dll) and 
   - fixed) bitmap texture crash bug 

 * 2021.07.05 v1.7.1
   - added) path optimizer 
     - points vertex list 
     - group's offset
   - fixed) bitmap pixel calculation bug
   - fixed) multiple Rtc initializing bug
   - fixed) datamatrix/QR code with invert cell and quite zone
   - fixed) document rotate offset 
   - fixed) support group entity's' hatch if closed figure
   - added) event handler in SiriusEditor 
   - added) demo code with rs232 laser power control
   - added) demo code with custom laser pen
   - added) transit entity by keyboard shortcut (CTRL+ALT+SHIFT + key)
   - added) internal hpgl parser dll

 * 2021.06.16 v1.7
   - added) sirius demo project
   - added) IMotor interface. Z position in layer property (Z motor control program in "8.customlasermarker" demo)
   - added) Auto laser control signal in Layer property
   - added) support Rtc4 
   - fixed) modified IRtc, IRtcExtension, ILaser interface
   - added) support IPG YLP Type E source (with usercontrol)
   - added) Supported spline in dxf file
   - fixed) support LwPolyline with hatch
   - added) Raster Entity

* 2021.06.07 v1.6 
   - added) Stitched Image entity, added Write Data Ext16 io entity, added Sirius Project

* 2021.06.01 v1.5 
   - added) List Data entity. support JPG, GIF, Png Image Format. added Write Data entity
   - added) Custom Editor/Viewer Demo Project
   - added) Bitmap entity, added ICompensator interface each analog digital io. added Raster entity, AlcVectorBegin/End entity

* 2021.05.20 v1.4 
   - added) IRtcAutoLaserControl interface, support position, speed, vector define automatic laser control
   - fixed) xy coordinate with Vector2/3 struct

* 2021.05.19 v1.3 
   - added) HPGL entity

* 2021.05.12 v1.3 
   - added) CharacterSet, 3D, MOTF, Dual head demo project 
   - added) Sirius Text Arc entity, Text Arc entity
   - added) WPF demo project
   - fixed) barcode (QR/Datamatrix/1D ...) cell with dot/line/outline/hatch or imported pattern
   - merged) separated dll into single spiral.sirius.rtc.dll
   - fixed) to be similary properties at sirius text and text entity 
   - fixed) location/rotate bugs each entities
   - fixed) minor bugs

* 2020.01.29 v0.9 
   - added) Hatch function, repeat count, Demo project for custom RTC
   - fixed) SiriusView crash on design time, fixed minor bugs

* 2020.01.07 v0.8 
   - added IRtcDualHead interface, 
   - enhanced) points editor form, group offset form editor and minor bugs

* 2019.12.24 v0.7 
   - added IRtc3D interface, 
   - added Group entity (with MOTF), 
   - fixed ICorrection2D, 
   - added ICorrection3D interface. 

* 2019.12.19 v0.6 
   - added points entity (with path optimizer), 
   - support 3d (varioscan/z-shift) offset/defocus function in RTC. 

* 2019.12.17 v0.5 
   - added 1/2D  barcode entities 
   - support wobbel and raster operation in RTC

* 2019.12.12 
   - v0.4 new sirius text entity 
   - support font format : *.cxf

* 2019.12.11 v0.3 
   - support MOTF(Marking On The Fly), XL-SCAN  (SyncAxis) by optional dll.

* 2019.12.10 v0.2 
   - support HPGL(plt) file / Marker with scanner rotated angle 
   - IPen interface

* 2019.12.03 v0.1 
   - first release