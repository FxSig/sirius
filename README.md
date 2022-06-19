# SpiralLab.Sirius Library

**1. Descriptions**


 SuperEasy library for Control Scanner and Laser


 
 ![pulse on demand](http://www.spirallab.co.kr/wp-content/uploads/2022/01/image-6.png)
 
 ![scanner field correction by image](http://www.spirallab.co.kr/wp-content/uploads/2022/01/image-5.png)
 
 ![scanner field correction](http://www.spirallab.co.kr/wp-content/uploads/2022/01/image-7.png)

 ![path](http://www.spirallab.co.kr/wp-content/uploads/2022/01/image-9.png)

 ![barcode](http://www.spirallab.co.kr/wp-content/uploads/2021/06/image-21-1024x686-2.png)
 
 ![stitchedimage](http://www.spirallab.co.kr/wp-content/uploads/2022/01/image-11.png)

 ![stlfile](http://www.spirallab.co.kr/wp-content/uploads/2022/04/image.png)

 ![xlscan](http://www.spirallab.co.kr/wp-content/uploads/2022/02/Screenshot_Viewer_XLSCAN_colorMap-1.png)
 
 ![xlscan with job](http://www.spirallab.co.kr/wp-content/uploads/2022/02/image-24.png)

 ![xlscan with viewer](http://www.spirallab.co.kr/wp-content/uploads/2022/02/image-25-1508x1080.png)

 ![text with arc](https://user-images.githubusercontent.com/58460570/117915901-215d7e00-b321-11eb-8055-5502aad8bf85.png)

 ![multiple views](http://www.spirallab.co.kr/wp-content/uploads/2022/01/image-10.png)

 ![measurement](http://www.spirallab.co.kr/wp-content/uploads/2022/01/image-13.png)

 ![path optimizer](http://www.spirallab.co.kr/wp-content/uploads/2022/01/ezgif.com-gif-maker.gif)

  ----

**2. Features**

 - support SCANLAB's RTC4, RTC5, RTC6 products with multiples.
 - support SyncAxis(XL-SCAN) with multiple heads/stages combination.
 - support powerful 3x3 matrix operations with stack.
 - support processing unlimited vector data by internally.
 - support MOTF(marking on the fly), dual head, 3d (like as VarioSCAN) features.
 - support 2D/3D field correction with easy to use and with powerful image analyzer.
 - support scanner motion/signal profiles with plotted graph.
 - support point, line, arc, LW polyline, rectangle, circle, true type font, cxf font, 1D/2D barcodes, spiral, trepan, dxf, hpgl(plt) and customizable entities with layers.
 - support 2D barcodes with various cell type like as dots, lines, outlines, hatch and generated data with formatted datetime, serial no by automatically.
 - support rendering 3D STL(StereoLithography) file.
 - support powermeters and power mapping interface.
 - support laser beam path visualizer (simulator) and path optimizer algorithm.
 - support powerful undo/redo actions and single document data with multiple view targets.
 - support vary laser power with PoD(pulse on demand by analog, 8/16bits digital, frequency, pulse width modulation) and sky-writing.
 - support vary laser parameters with 'Pen' (frequency, pulse width, power, speeds, laser delays, sky writing , ...)
 - support customizable and extensible marker interface.
 - support many kinds of powermeters
    - Ophir (preliminary)
    - Thorlab PM100D, PM100A, PM100USB, PM200, PM400 (preliminary)
 - support powermap for calibrate output laser power.
    - Thorlab PM series
    - Ophir products
    - Coherent PowerMax (USB/RS) series
 - support many kinds of laser sources
    - Advanced Optowave Fotia
    - Advanced Optowave AOPico (preliminary)
    - Coherent Avia LX (preliminary)
    - Coherent Diamond C-Series (preliminary)
    - Coherent Diamond J-Series (preliminary)
    - Photonics Industry DX
    - Photonics Industry RGHAIO (preliminary)
    - IPG YLP Type D
    - IPG YLP Type E
    - IPG YLP N Series (preliminary)
    - JPT Type E
    - SPI G3/G4 (preliminary)
    - Spectra Physics Talon (preliminary)
    - Spectra Physics Hippo (preliminary)
 - support many kinds of motion controllers
    - AJINExtek AXL
    - ACS SPiiPlusNET (preliminary)
    - Newport ESP301 (preliminary)
 
  ----

**3. How to use ?**

 - development environment : .NET dll library
 - there are multiple demo programs in DEMOS directory
 - add spirallab.core.dll, spirallab.sirius.rtc.dll, spirallab.sirius.dll and spirallab.sirius.fieldcorrection.dll files as a .NET DLL reference
 - use SpiralLab.Sirius.EditorForm, SpiralLab.Sirius.ViewerForm user controls
 - x64 Environment : copy files from bin\x64 to bin\
 - x32 Environment : copy files from bin\x32 to bin\
 - additional dll files : freetype6.dll, NLog.dll, ...
 - subdirectory hierarchy

   > config (config .ini files)

   > correction (ctb/ct5 files and convert programs)

   > fonts (ttf fonts)

   > logo (plt, dxf, stl files)

   > logs (config and output log files)

   > map (powermap files)

   > plot (measurement output files)

   > scripts (script .cs files)

   > siriusfonts (cxf font files)


 *The program running about 30 mins in evalution copy mode !*
 
 
 ----

**4. Author**

 - developer page : http://www.spirallab.co.kr/?page_id=229
 - git repository : https://github.com/labspiral/sirius.git
 - e-mail : labspiral@gmail.com
 - homepage : http://spirallab.co.kr                        
 

----


**5. Version history**


* 2022.6.17 v.1.111
   - added) power scaler (파워 스케일러 - 계측 에너지에 대한 사용자 비율 적용 기능)
   - added) coherent powermax powermeter (코히런트 파워맥스 계측기 추가)
   - fixed) ophir powermeter with scan devices (Ophir 파워메터 장치 초기화시 스캔하도록 수정)
   - fixed) powermeter winforms (파워메터 윈폼 개선)
   - fixed) removed unused properties at textr/siriustext (텍스트 개체의 미사용 속성 표시 삭제)
   - fixed) syncaxis
      - select target stage with correction table (대상 스테이지를 스캐너 보정 테이블과 함께 선택 지원)
      - show counts of scan devices, stages (스캔 헤드, 스테이지 개수 표시)

* 2022.6.6 v.1.110
   - added) powermap winform (파워맵용 윈폼 추가)
      - open/save powermap file (파워맵 저장/불러오기 지원)
   - added) powermeter arguments (파워메터 모델별 계측 인자 인터페이스 추가)
      - PowerMeasureVirtualArg, PowerMeasureOphirUSBIArg, PowerMeasureThorlabArg (모델별 파워메터 인자) 
   - fixed) busy bug with rtc4 measurement session (RTC4 계측 버그)
   - fixed) rtc io output on/off log message (RTC 확장 DIO 변경시 부적절한 로그 메시지 수정)
   - fixed) motors winform with position table bug (모터 윈폼내의 위치 테이블 오류 수정)
   
* 2022.5.30 v.1.109
   - fixed) support script function with text entities (텍스트 개체들에 스크립트 기능 지원)
   - added) powermap added into laser interface to support powermap (파워매핑을 기능을 위해 레이저 인터페이스 변경)
      - pen entity with powermap (펜 개체에 파워맵 대상 카테고리 지정)
      - demo programs for powermeter/powermap (파워메터/파워맵 데모 프로그램 추가)
   - fixed) motor/motors inteface (모터 단축 다축용 인터페이스 변경)
      - with control winforms (제어용 윈폼 추가)
   - added) motorhelper class (모터 헬퍼 클래스 추가)

* 2022.5.20 v.1.108
   - added) laser source for advanced optowave AOPico (preliminary)
   - added) laser source for spectra physics Hippo (preliminary)
   - added) laser source for photonics industry RGH AIO (preliminary)
   - added) laser source for coherent diamond c-series (preliminary)
   - added) motion control for ESP 301 (preliminary)
   - added) control forms for motor(s) (모터 제어용 폼 추가: 단축용, 다축용, Z축/XY축/XYZ축/XYZR축 용)
   - added) accumulated mark counts in marker form (마커폼에 누적 가공 개수 표시)
   - fixed) paste array with center position of clipboard entities (배열 복사 붙혀넣기시 클립모드 개체의 중심위치 기준 적용)
   - fixed) relocated group/ungroup buttons in editor (편집기에 그룹/언그룹 버튼 위치 이동)
   - fixed) disappered images like as bitmap (이미지 개체 렌더링시 사라지는 버그 수정)
   - fixed) fail to register characterset into RTC (문자집합을 RTC 메모리에 다운로드 실패하는 버그 수정)

* 2022.5.13 v.1.107
   - added) syncaxis demo for multiple instances (syncAXIS 기반의 듀얼헤드 데모 기능 추가)
   - added) create group entity with offsets (옵셋을 가진 그룹 개체 생성 추가 : 마스터 개체 + 오프셋 배열 방식)
   - added) draw grids within view (뷰에 격자 렌더링 지원)
   - fixed) group entity's color rendering bug (그룹 개체및 오프셋 처리시 렌더링 색상이 바뀌지 않는 문제 해결)
   - fixed) control entities are moved to single menu (편집기에서 제어용 개체들을 단일 메뉴 아래로 이동)
   - fixed) transit error with textarc/siriustextarc entity (원형 텍스트 개체 이동시 표시 문제 수정)

* 2022.5.6 v.1.106
   - fixed) 3d (for varioscan)
      - reset z offset/defocus to zero when start (3d 가공시작시 오프셋및 디포커스 값 자동 리셋)
      - editable kz-factor and kz-scale (KZ-스케일값 수정및 Z-+ 공간에 대한 선형 스케일값 제공)
   - fixed) non-drawing issue with group entity when fast rendering (그룹 개체 화면에 렌더링 되지 않는 문제 수정)
   - added) hittest property with entities (모든 개체에 선택 여부 속성 제공)
   - added) offsetable interface 
      - calculate bound area with offsets (개체 오프셋 목록 지정시 외곽 영역 포함해 처리하도록 수정)
      - create group entity with offsets (그룹 생성시 마스터 개체 + 오프셋 배열 방식 생성 지원)
   - updated) comments for dll library (라이브러리 xml 주석 업데이트)

* 2022.4.29 v.1.105
   - fixed) points entity (점 개체 렌더링 속도 향상)
      - fast rendering for speed up 
   - fixed) auto-increase serialno at text/barcode with offsets (바코드와 텍스트 내부 오프셋 자동 증가 지원)
   - fixed) skip jump position when enabled 3d option (옵션 가공시 점프 구간 가공 버그 수정)
   - fixed) support hit test option at every drawable entities (출력 개체들에 모두 Hit Test 여부 선택 지원됨)
   - fixed) disabled scripts function (스크립트 기능을 비활성화)
   - fixed) reset 3d offset position when begin list
   - fixed) syncaxis 
      - simulated with multiple syncaxis viewers using multiple layers (다중 레이어 가공시 뷰어 결과도 레이어 개수만큼 출력)
      - syncaxis demo c++ code (c++ 환경에서 COM 객체 등록후 사용 예제 코드 추가)

* 2022.4.22 v.1.104
   - added) laser source : SpectraPhysics Talon (스펙트라 피직스 탈론 레이저 소스 추가)
   - added) laser source : IPG YLP N (IPG 레이저 소스 추가)
   - fixed) draw ellipse with angle delta (타원의 미분 각도에 따라 렌더링 변경)
   - added) execute RTC's execution buffer with fast response (RTC 버퍼 실행 시작을 단축하는 버퍼 처리 기능 추가)
   - fixed) barcode 2d (DataMatrix/QR)
      - cell type : hatch with shift (셀 타입 해치사용시 쉬프트 오프셋 거리 추가)
      - cell type : line with vertical/horizontal direction (셀 타입 라인 사용시 가로세로 방향 설정)
      - offset array with serial no (바코드에 오프셋 기능을 통한 일련번호 증가 기능 추가)
   - fixed) text 
      - offset array with serial no (텍스트에 오프셋 기능을 통한 일련번호 증가 기능 추가)

* 2022.4.15 v.1.103
   - added) motor form with xyz table (모터폼에 XYZ 상태 출력 및 위치 테이블 지원)
   - added) powermeter form at SiriusEditorForm (파워메터용 출력 폼 추가됨)
   - fixed) adjust laser duty cycle by automatically when vary target power (Duty Cycle 로 제어 되는 레이저 소소의 경우 파워값 변경시 펄스폭 자동 계산 지원)

* 2022.4.4 v.1.102
   - added) import stl(stereolithography) file for viewing (STL 파일 가져오기 및 화면에 렌더링 지원)
   - added) triangle3d/circle3d entity (삼각형 3d, 원3d 개체 추가)
      - rotate each x,y axis (X,Y 축에 개별 회전각도 지정 가능)
   - fixed) vary laser spot size with simulate laser path by z location (시뮬레이션 가공시 높이에 따른 가공 점 크기 가변 지원)
   - added) fiducial entity (fiducial mark 전용 개체 추가)
   - fixed) single menu item for control entities at editor (제어용 개체들을 하나의 메뉴 아래로 정리)
   - fixed) RTC 의 내장 1MHz 가상 엔코더 동작시 한 축만 활성화되는 버그 (simulate encoder)
   - fixed) minor bugs (각종 버그 해결)

* 2022.3.28 v.1.101
   - added) triangle entity (삼각형 개체 추가)
   - added) laser for coherent diamond j-series (코히런트 다이아몬드 J 시리즈 통신지원)
   - added) DIO with rtc's D-sub 15pin (RTC 15핀 레이저 포트의 2/2 디지털 입출력 지원)
   - support) import file format .sirius as single group entity (시리우스 파일 가져오기 지원. 하나의 개체로 변환됨)
   - improved) performance with marker's offsets (마커에 오프셋 배열 지정시 가공 처리 속도 향상)

* 2022.3.21 v.1.100
   - added) text/siriustext/barcode1,2d entity support converting datetime/serialno/gs1 format (텍스트 변환 개체들의 데이타를 분석하여 날짜시간, 일련번호, GS1 포맷으로 자동변환 지원)
   - added) IRtcSerialNo interface (일련번호 제어용 인터페이스 추가)
   - fixed) invalid position when using group offsets

* 2022.3.15 v.1.99
   - added) wobbel in pen (와블 가공 기능을 펜 개체에 추가)
      - wobbel's transverse, longitudinal, frequency and shape (가로 세로 크기 주파수및 모양 설정)
      - shape : ellipse, figure of 8 (타원, 8자 모양)
      - max frequency is lower than 0.1/tracking error (와블 주기값은 스캐너의 tracking error 에 의해 최대 설정값이 제한됨)

* 2022.3.7 v.1.98
   - added) improved hatch options (해치 옵션 기능 강화)
      - hatch repeat count (해치 전용 반복 회수 지정가능)
      - mark outline or not (해치시 외곽선 가공할지 여부)
      - mark outline first (해치시 외곽선을 우선 가공할지 여부)
   - added) jump entity (점프 개체 추가됨)
   - fixed) internal angle offset within group entity bug (그룹개체내의 오프셋 사용시 x,y,angle 적용 버그)
   - fixed) negative datetime offset bug (날짜 시간 오프셋에 음수값 처리 버그)
   - fixed) works only 30mins at evaluation copy mode (평가판 모드에서 30분만 동작)

* 2022.2.28 v.1.97
   - added) sky writing with mode 1,2,3 at pen parameter (펜 파라메터의 sky writing 모드 1,2,3 중 선택 지원)
   - fixed) serial entity (SiriuslTextSerial, TextSerial)
      - internal/external trigger mode (번호 증가를 위한 트리거를 내부/외부 선택 가능)
      - internal : increased whenever try to mark manually (마킹을 수동으로 할때 마다 수동 증가)
      - external : external /START triggger automatically (외부 트리거가 발생할때 마다 자동 증가)
   - added) text with datetime format (텍스트 개체가 시간 포맷으로 자동 변환 지원)
      - if datetime option enabled, text data re-formatted by automatically (DateTime 옵션 활성화시 마킹 텍스트의 내용이 분석되어 변경됨)
      - datetime start/end seperator : '%'' or '{'', '}''
      - example1) {yyyyMMdd} -> 20220225
      - example2) {yyyy/MM/dd HH:mm:ss} -> 2022/02/25 05:50:06
      - example3) A123{yyyyMMdd}GOOD -> A12320220225GOOD 
      - example4) A123{yyyy}XY{MM}AB{dd}GOOD -> A1232022XY02AB25GOOD 
   - added) IRtcDateTimeOffset
      - to support modify offset datetime (날짜 시간 오프셋 기능을 위해 IRtcDateTimeOffset 인터페이스 추가)
   - added) IRtc3D interface 
      - read/writable A,B,C coefficient at correction file (3D 보정 파일의 헤더 영역에 포물선 A,B,C 계수 읽고,쓰기 및 변경지원)
      - get focal length at specific x,y,z location (특정 3D 위치에서 초점 거리 계산 기능 추가)
   - fixed) restart versioning 1.9.6 -> 1.97 (버전 관리 번호 변경)

* 2022.2.21 v1.9.6
   - improved) scanner field correction with vary images (다양한 이미지 조건에 맞도록 알고리즘 추가 개선)
   - added) jump to origin position after finished to mark (가공 완료후 원점으로 자동 점프 하는 옵션)
   - added) changable correction file within rtc property dialog (RTC 속성창에서 스캐너 보정 파일 변경 지원)
   - qualified) syncaxis   
      - field tested at equipment (장비에서 필드 테스트)
   - fixed) ramp with arc/ polyline bug (arc, polyline 의 구간별 ramp 출력 버그 수정)
   - fixed) layer 
      - always works as repeat by 1 bug (레이어 반복 회수가 1로 고정되는 문제 해결)
   - fixed) datamatrix /qrcode barcode when cell type line
      - pitch error with zig zag mark

* 2022.2.14 v1.9.5
   - fixed) syncaxis   
      - enumerable job history (최대 50개 까지의 작업 이력 조회가 가능합니다)
      - auto start when job is enough (가공 데이타가 충분상태가 되면 자동 가공을 실시하는 방식 도입)
      - multiple head offsets with matrix bug (멀티 헤드별 행렬을 통한 오프셋 버그 해결)
      - utilization ratio with position, acc, jerk (위치, 가속도, 가가속도의 사용률 분석 지원)
   - fixed) rtc 4,5,6 
      - laser control signal (속성창에서 레이저 출력 신호 변경 지원)
      - laser mode (속성창에서 레이저 모드 변경 지원)
      - external control (속성창에서 레이저 외부 신호 제어 변경 지원)
      - marking info (속성창에서 marking info 상태 조회 지원)
   - added) new barcode entity for easy to use (사용이 용이한 새로운 2D 바코드 개체 지원)
      - datamatrix v2 
      - qrcode v2

* 2022.2.7 v1.9.4
   - fixed) syncaxis v1.6 (syncaxis 1.6 버전지원및 4개의 멀티 스캔헤드 * 4개의 스테이지 지원용)
      - job characteristic (작업 특성 분석 지원)
      - config trajectory (mark/geometry) (경로 설정 지원)
      - config dynamics (scanner/stage) (역할 설정 지원)
   - added) datetime offset with rtc 5, 6 

* 2022.2.2 v1.9.3
   - added) spirallab.sirius.fieldcorrection with hatched images
   - fixed) syncaxis
      - pen parameter : spot distance compensation, min.mark speed 
      - layer parameter : bandwidth 
      - marker form with job/trajectory/dynamics 

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
