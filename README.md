# SpiralLab.Sirius Library

**1. Descriptions**


 SuperEasy library for Control Scanner and Laser

  
 ![pulse on demand](https://user-images.githubusercontent.com/58460570/191867625-b6c05f9d-1293-4c52-b194-4801c1ae47ce.png)
 
 ![scanner field correction by image](https://user-images.githubusercontent.com/58460570/191867640-e121898a-0607-4aee-af1f-4de12429c865.png)
 
 ![scanner field correction](https://user-images.githubusercontent.com/58460570/191867651-5cf656b1-0117-4b79-9a1b-3a9c9083a861.png)

 ![mark path](https://user-images.githubusercontent.com/58460570/191867659-9b78b865-dbb8-428f-8257-e8c0b1d412ff.png)

 ![barcode](https://user-images.githubusercontent.com/58460570/191867673-c81fdb44-d645-4c53-858c-0fcdde0fe566.png)
 
 ![motf text](https://user-images.githubusercontent.com/58460570/191867682-bda75230-b909-43f3-836b-7b248da5d2aa.png)
 
 ![motf wait](https://user-images.githubusercontent.com/58460570/193395419-1c99bd2f-e8bb-4b61-8f43-9793729a96d2.png) 

 ![stitched image](https://user-images.githubusercontent.com/58460570/191867691-e47ea8fb-8a97-4c1c-914d-453e50e37bdd.png)

 ![vision integration](https://user-images.githubusercontent.com/58460570/191867708-c279547a-9ef5-4ce4-ba6a-08001f003a51.png)
 
 ![stl file](https://user-images.githubusercontent.com/58460570/191867719-1dd16c56-faeb-444f-9784-9b391aaf6c85.png)

 ![measurement](https://user-images.githubusercontent.com/58460570/191867726-59f79bad-ae19-4b80-a1dd-5705c6724364.png)

 ![path optimizer](https://user-images.githubusercontent.com/58460570/191867750-0f6c01f6-3a0e-421a-bace-9268d85e21a9.png)

 ![synsaxis](https://user-images.githubusercontent.com/58460570/191867760-75703024-51ef-4ee3-ba53-f22d6ed7b67a.png)

 ![job characteristic](https://user-images.githubusercontent.com/58460570/191867764-9de1dd77-e6f8-4eaa-b915-3069b2a770ed.png)

 ![synsaxis viewer](https://user-images.githubusercontent.com/58460570/191867770-1cbf065d-769a-40f4-813a-8f332aa53224.png)

 ![multiple viewer](https://user-images.githubusercontent.com/58460570/191867776-5cf3d804-83eb-4a76-b43b-61914fc683c8.png) 

 
  ----


**2. Features**

 - support SCANLAB's multiples RTC4, RTC5, RTC6 products.
 - support SCANLAB's SyncAxis(XL-SCAN) with multiple heads/stages combination.
 - support powerful 3x3 matrix operations with stack.
 - support processing unlimited vector data by internally.
 - support MOTF(marking on the fly), dual head, 3d (like as VarioSCAN) features.
 - support 2D/3D field correction with easy to use and with powerful image analyzer.
 - support profile scanner motion and signals with plotted graph.
 - support point, line, arc, LW polyline, rectangle, circle, true type font, cxf font, 1D/2D barcodes, spiral, trepan, dxf, hpgl(plt) and customizable entities with layers.
 - support 2D barcodes with various cell type like as dots, lines, outlines, hatch and generated data with formatted datetime, serial no by automatically.
 - support rendering 3D STL(StereoLithography) file.
 - support powerful undo/redo actions and single document data with multiple view targets.
 - support laser beam path visualizer (simulator) and path optimizer algorithm.
 - support vary output laser power with PoD(pulse on demand by analog, 8/16bits digital, frequency, pulse width modulation) and sky-writing.
 - support vary scanner/laser parameters with 'Pen' (frequency, pulse width, power, speeds, laser delays, ...)
 - support customizable and extensible marker interface.
 - support many kinds of powermeters.
    - Thorlab PM series
    - Ophir products
    - Coherent PowerMax (USB/RS) series 
 - support power mapping interface for compensate output laser power.
 - support many kinds of laser sources
    - Advanced Optowave Fotia
    - Advanced Optowave AOPico 
    - Coherent Avia LX (preliminary)
    - Coherent Diamond C-Series
    - Coherent Diamond J-Series (preliminary)
    - Photonics Industry DX
    - Photonics Industry RGHAIO 
    - IPG YLP Type D
    - IPG YLP Type E
    - IPG YLP N Series (preliminary)
	- IPG ULPN Series 
    - JPT Type E
    - SPI G3/G4 (preliminary)
    - Spectra Physics Talon (preliminary)
    - Spectra Physics Hippo 
    - Inngu GraceX Series (preliminary)
 - support many kinds of motion controllers
    - AJINExtek AXL
    - ACS SPiiPlusNET (preliminary)
    - Newport ESP301 
 

  ----


**3. How to use ?**

 - development environment : .NET framework 4.8
 - dll assemblies : spirallab.core.dll, spirallab.sirius.rtc.dll, spirallab.sirius.dll and spirallab.sirius.fieldcorrection.dll 
 - winforms user control : SpiralLab.Sirius.EditorForm, SpiralLab.Sirius.ViewerForm
 - x64 Environment : copy files from bin\x64 to bin\
 - x32 Environment : copy files from bin\x32 to bin\
 - additional dll files : freetype6.dll, NLog.dll, ... (MS VC++ redistributable package)
 - subdirectory hierarchy
    - config (ini config files)
    - correction (ctb/ct5 files and converter programs)
    - fonts (ttf fonts)
    - logo (hpgl, plt, dxf, stl files)
    - logs (output log files)
    - powermap (laser powermap files)
    - plot (measurement output files)
       - to plot as chart, please download and copy gnuplot program into plot\gnuplot\ directory
       - gnuplot download link : http://tmacchant33.starfree.jp/gnuplot_bin.html
       - executed plot\gnuplot\wgnuplot.exe program by internally
    - scripts (csharp script files)
    - siriusfonts (cxf font files)
	- syncaxis (xml config file, configurator and viewer)

 *The program running about 30 mins in evalution copy mode !*
 
 
 ----


**4. Author**

 - developer page : http://www.spirallab.co.kr/?page_id=229
 - git repository : https://github.com/labspiral/sirius.git
 - e-mail : hcchoi@spirallab.co.kr
 - homepage : http://spirallab.co.kr                        


----


**5. Version history**

* 2023.2.14 v.129
  - added) support 8 measurement channels at RTC6
  - added) syncaxis 
     - job history at marker form
     - list arc bug	 

* 2023.1.25 v1.128
  - migrated) SCANLAB syncaxis v1.8 
     - release note : https://www.scanlab.de/sites/default/files/2023-01/Release_Notes_syncAXIS_1.8.0.pdf
  - updated) RTC6_Software_Package_Rev.1.15.4 
  - added) convert text entity when loading dxf file (DXF 파일 로딩시 텍스트 개체 변환 지원)
     - converted ''Arial Unicode MS Font.ttf' if installed system font (Arial 트루타입 폰트로 변환됨)
     - converted 'romans2.cxf' font if not installed system font (romans2 단선 폰트로 변환됨)
  - fixed) too many nodes at treeview cause exception (대량의 트리뷰 노드 사용시 예외발생 해결)
  - fixed) compare equality of pen color bug (펜 색상 비교 실패 버그)

* 2023.1.13 v1.127
  - fixed) apply button at scanner field correction (스캐너 필드 보정 윈폼에서 apply 버튼 동작시 보정 파일 자동 로딩 지원)
  - fixed) adjust step distance at path simulator (가공 시뮬레이터에서 스텝거리값 변경 지원)
  - fixed) move to end at entity's group editor (엔티티 아이템 배열 편집기에서 끝 위치로 이동 지원)
  - fixed) list of active channels at syncaxis (syncAXIS 제어기에서 ALC 채널 목록 조회 지원)
  - fixed) textdata(text/barcode entities) has changed by automatically when mark at each offsets (오프셋 위치에 텍스트/바코드 가공시 데이타 자동변환 지원)

* 2022.12.23 v1.126
  - updated) RTC6_Software_Package_Rev.1.15.2_2022_11_24 

* 2022.12.2 v1.125
  - added) new path optimizer with powerful TSP solver (신규 경로 최적화 알고리즘 추가)
  - added) laser spot bite size at circle, rectangle, lwpolyline entities (원, 사각형, 폴리라인에 bitesize 크기 지원)
  - fixed) powerverify but detected watt is 0 (파워 검증시 측정 데이타 오류 수정)
  - fixed) result event at correciton form bug (스캐너 보정 폼 이벤트 핸들러 호출 오류 수정)

* 2022.11.11 v1.124
  - updated) .net framework v4.8 (.NET 프레임워크 4.8 버전 적용)
  - added) syncAxis      
     - move stage to center of each layers with scanner only mode (레이어 옵션: 중심위치로 스테이지 이동 후 가공 - Scanner Only 전용)

* 2022.11.4 v1.123
  - fixed) improved XML documentation
  - added) plot measurement result with another program (계측 데이타 그래프 출력 프로그램 추가)
     - 1st : gnuplot program
	 - 2nd : internal winform program
  - fixed) SiriusText/Text font register bug into RTC (RTC 카드에 폰트 등록 버그 수정)
  - fixed) MOTF angular rotate direction way (시계 방향 = 엔코더 증가 방향 = 각도 증가 방향)
  - fixed) IRtcDualHead renamed to IRtc2ndHead (IRtc2ndHead 으로 이름 변경)
  - fixed) powermap verification with detected watt (파워맵 검증시 측정 파워값도 기록)
  - fixed) hung up at syncAxis marker form (syncAxis 마커창 응답 없음 버그)
  - fixed) lwpolyline to arc bug (폴리라인을 호 객체로 변환 버그)
  - fixed) bulge vertex at lwpolyline when import dxf file into arc entity by default (폴리라인의 곡선 부분을 호 객체로 기본 변환)
     - bulge to arc (default) (bulge 구간을 호 개체 변환시 : 기본값) 
        - Config.LwPolylineBulgeToLines = false;
     - bulge to lines (bulge 구간을 선분으로 변환시) 
        - Config.LwPolylineBulgeToLines = true; 
        - Config.LwPolylineBulgePrecision = resolution (분해 개수);
  - demos) 
     - more MOTF anglar demo "MainFormAngular2" with rotate center (회전 중심 위치를 처리하는 강화된 MainFormAngular2 데모 추가)
     - load/select correction file after finish to convert at field correction demo (스캐너 필드 보정후 이를 RTC 에 적용하는 기능 추가)

* 2022.10.21 v1.122
  - added) Program5.cs demo at motf_dualhead_ext project (motf_dualhead_ext  프로젝트에 Program5 예제 추가)
     - powerful example demo codes use of Angular MOTF (강력한 회전 기반 MOTF 에제 제공됨)
  - added) IRtcDualHead (IRtcDualHead 인터페이스 기능 추가)
     - ListSelectCorrection function (리스트 명령 실행중 스캐너 보정 테이블 변경 지원)
  - added) IRtcMotf interfaces (MOTF 인터페이스 기능 추가)
     - ListMotfLimits function (영역침범 확인 기능 추가)
     - CtlMotfOverflowClear function (영역침범시 리셋 기능 추가)

* 2022.10.13 v1.121
  - fixed) speed up when open/import/clone sirius file (시리우스 파일 읽기 속도 향상)
  - fixed) initialize rtc6 with auto delay and corner,end,acc scale values if scanahead activated (SCANa 옵션을 가진 RTC6 초기화시 자동 지연 기능 활성화및 라인 품질 파라메터 자동 설정)
  - added) configure rtc signal level (active high/low) before initialize (RTC5/6 초기화 시점에 레이저1,2및 ON 출력 신호 레벨 설정 지원)
  - added) divide regions with threshold (개체를 영역으로 나누기 할 경우 제외할 최소 크기 지정 지원)
  - added) demo project head4_bcd_text (데모 프로젝트 추가)
     - multiple 4 instances  (4개의 인스턴스 사용)
     - mark user defined text data with offsets (오프셋 위치에 사용자 데이타 적용하여 가공)
  - added) selectable jump tuning mode (점프 튜닝 모드 선택 지원)
  - tested) Photonics Industry RGHAIO, Spectra Physics Hippo has tested for communication (레이저 2종 통신 테스트 완료)
  - fixed) invalid 2nd head offset (2nd 스캔 헤드에 적용된 오프셋값이 초기화되는 문제 수정)
  - fixed) exception of SiriusEditorForm/SIriusViewerForm usercontrol when create (사용자 컨트롤 생성시 예외 발생 버그)
  - fixed) Newport ESP301 (ESP301 모션 제어기 테스트 완료)
    - update motor status takes too much time (상태 업데이트에 시간이 많이 소요되는 문제 해결)
    - defined home offset (사용자 지정 홈 오프셋 지원)
  
* 2022.10.2 v1.120
  - added) rectangle with reverse winding (가공 순서 뒤집기 지원)
  - added) support SCANa options at RTC6 (SCANa 옵션 기능 지원)
     - enable/disable auto delays (자동 지연 사용 지원)
     - added) excelliscan demo project (데모 프로젝트 추가)
     - delay values are converted automatically (SCANa + 자동 지연 활성화시 기존 레이저 지연 시간이 scan ahead 용 지연값으로 자동 변환됨
  - added) move z axis in layer with abs/relative (레이어 속성에서 스캐너 Z 축 사용시 절대/상대 이동 기능 추가)
  - fixed) remove duplicated data when divide (영역 나누기 실행시 중복 정점 데이타 제거)
  - fixed) notify event handler with async (이벤트들을 비동기 처리되는 방식으로 일원화)
     - log message
     - user log in/log out
     - motor event (home/property changed)
     - laser event (property changed)
     - marker, powermeter, powermap 
  - fixed) motf demo project 
     - by xy encoder : load bin\recipes\motf.sirius file (XY 엔코더 기반 MOTF 사용 예제)
     - by angular encoder : load bin\recipes\motf_angular.sirius file (회전 엔코더 기반 MOTF 사용 예제)

* 2022.9.23 v1.119
  - updated) (c)SCANLAB's correXionPro.exe up-to-date v1.06 (스캔랩의 correXionPro 1.06 버전 업데이트)
  - added) more laser modes
     - yag3,5 and mode 4,6 (레이저 모드 추가됨)
     - Qswitch delay (Yag5 사용시 Q 스위치 지연시간 설정 지원)
  - added) layer with z motor control 
     - actual position check option (Z 모터 이동후 위치 비교 여부 옵션 지원. 엔코더 피드백 없는 모터 지원용)
  - added) motor with IJogControl interface (모터 조그 제어용 인터페이스 추가)
  - fixed) angular motf with simulated encoder generate invalid position value (회전 기반 MOTF 와 엔코더 시뮬레이션을 같이 사용시 각도 이동량 연산 버그)
  - fixed) angular motf begin with encoder reset option (회전 기반 MOTF 시작시 엔코더 리셋 여부 처리)
  - fixed) syncaxis 
     - remove log messages when get scanner/stage position (syncaxis 사용시 대량의 에러 로그 메시지 생성 제거)

* 2022.9.6 v1.118
  - added) serial communication with monitoring/dump data (시리얼 통신 16진수로 모니터링/덤프 지원)
  - added) ditial input triggers (디지털 입력 트리거 집합 추가)
  - added) rtc serial comm interface (RTC 제어기 내부 시리얼 통신포트 사용을 위한 인터페이스 추가)
  - added) view with custom rendering handler (뷰에 사용자가 직접 opengl 렌더링 처리 지원)
  - added) MOTF angular with measurement (회전 MOTF 계측 데이타 각도 변환 지원)
  - fixed) MOTF angular wait bug (회전 MOTF 대기 각도 처리 버그)
  - fixed) motor homed event with argument (모터 원점 초기화 이벤트 인자 개선)
  - fixed) LWPolyline hatch bug (폴리라인 해치 사용시 버그 수정)
  - fixed) SiriusEditor out of memory bug at treeview (편집기에서 트리뷰 노드 전체 삭제시 메모리 관련 오류 수정)

* 2022.8.31 v1.117
  - added) grid checker with crop ROI and save  (스캐너 보정용 이미지의 ROI 영역 자르기, 저장 기능 추가)
  - added) IRtcDualHead with base/user offset (2nd헤드사용시 헤드별 오프셋 확장 지원 :  base + user)
  - fixed) IRtcMotf with Motf Repeat bug (MOTF 반복 가공시 무제한 및 회수 지정 가능)
  - fixed) powermeter/powermap/verify event handler with notification bugs (파워메터/파워맵/검증시 호출 버그 수정및 인자 통일)

* 2022.8.29 v1.116
  - added) demo project 'custom entity'
  - fixed) fail to create SiriusEditorForm/SIriusViewerForm user control 

* 2022.8.24 v1.116
  - added) path optimizer with simple/custom sort (단순 경로: 상하좌우및 최단), 사용자 정의 경로 최적화 추가)
  - added) powermeter chart are zoomable/scrollable now (확대 및 스크롤 가능한 파워맵 그래픽 차트)
  - added) experimental. bitmap font entity (실험적 기능. 비트맵 기반 폰트 개체 추가됨)
  - fixed) bypass raster line unless exist data at mark bitmap (비트맵 레스터 가공시 빈 데이타줄은 자동 스킵 처리)

* 2022.8.19 v1.115
  - updated) Demos projects (데모 프로젝트 업데이트)
     - customeditor project : customizable editor/view winforms (에디터, 뷰어 윈폼 소스 공개를 통한 사용자화 지원)
     - customeditor project : customizable marker winforms (마커 윈폼 소스  공개를 통한 사용자화 지원)
     - customlasermarker project : customizable marker (마커 구현부 소스  공개를 통한 사용자화 지원)
  - added) IMarker with OnFailed event handler (마커 가공 실패 이벤트 핸들러 추가됨)
  - added) enable/disable powermap/verify category items (파워매핑, 검증 항목의 사용 여부 체크 지원)

* 2022.8.15 v1.115
  - added) processing on the fly with angular (회전축 엔코더 기반의 MOTF 지원)
     - added) motf angular begin entity (회전 시작 개체 추가)
     - added) motf angular wait entity (각도 대기 개체 추가)
     - added) motf angular with rotate center position (회전 중심 위치 변경 지원)
     - added) simulated motf with angular velocity (회전축 엔코더 시뮬레이션 지원)
  - added) start/stop powermapping routines and support event handlers (파워매핑 시작/정지 루틴 지원, 이벤트 핸들러 지원)
  - fixed) mark bug with point entity at rtc4 (RTC4 에서 점 개체 가공 버그)

* 2022.7.29 v1.114
  - updated) rtc6 dlls files by v2022.7.22      
  - added) barcode2d : hatched cross lines (2D 바코드 해치 셀 타입내에 격자 모양 해치 지원)
  - added) barcode2d : new cell type of circle (셀 타입에 원형 추가)
  - fixed) barcode2d : invalid start of line's cell type with zig zag (지그재그 라인 셀 타입사용시 시작 위치 오류 수정)
  - fixed) textarc with hatch bug (원호형 텍스트내부의 해치 지원 버그 수정)

* 2022.7.15 v1.113
  - fixed) powermap winforms
     - category as frequency value (카테고리값은 주파수로 처리)
     - remove category (카테고리 삭제기능)
     - apply new powermap bug (신규 파워맵 파일 미적용 되는 버그 수정)
  - fixed) IPG ULPN laser communication bug (ULPN 레이저 통신 버그 수정)
  - fixed) very large image based scanner field correction bug (대용량 이미지 기반 스캐너 필드 보정 버그 수정)
  - fixed) syncaxis
     - modified time resolution for MarkConfig (MarkConfig 설정값의 시간 해상도 변경)
     - editable calculation dynamics for scan device (스캐너의 계산 역학값 변경 지원)
	
* 2022.7.1 v.1.112
   - added) IPG YLP ULPN laser (레이저 소스 추가됨)
      - preliminary
   - added) MotfCall entity (개체 추가)
   - added) pens editor with power categories (펜 편집기에서 파워 카테고리 목록 지원)
   - fixed) simulated encoder with scale bug (시뮬레이션 엔코더 사용시 스케일 버그 수정)
      - jump list position at begin for repeat  (반복 가공시 리스트 명령의 시작위치로 이동 지원)
      - list type with Single only (리스트 타입을 단일로 설정 후 사용할것)
   - fixed) powermap winform with events (파워맵 시작/정지/열기/저장에 대한 사용자 이벤트 핸들러 지원)
	  - customizable mapping routine (사용자화 가능한 매핑 루틴 지원)
   - fixed) powerscaler (파워메터 스케일러 개선)
      - editable factor values (비율값 편집 가능)

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
   - added) syncaxis demo for multiple instances (syncAXIS 기반의 2nd헤드 데모 기능 추가)
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


----


**6. Moduels (Interfaces, Classes and Winforms)**


* spirallab.core.dll 
   - ICore (라이브러리 엔진 인터페이스)  
     - Core (라이브러리 엔진 객체)
   - Winforms 
     - FormLicense (라이센스 정보 출력 윈폼)


* spirallab.sirius.rtc.dll
   - IRtc (RTC 제어기 인터페이스)
      - IRtc3D (3D 지원 인터페이스)
      - IRtcAutoLaserControl (자동 레이저 제어 지원 인터페이스)
      - IRtcCharacterSet (문자 집합 지원 인터페이스)
      - IRtcDateTimeOffset (날짜시간 오프셋 지원 인터페이스)
      - IRtc2ndHead (2nd 헤드 지원 인터페이스)
      - IRtcExtension (확장 기능 지원 인터페이스)
      - IRtcMeasurement (계측 지원 인터페이스)
      - IRtcMOTF (Processing on the fly 지원 인터페이스)
      - IRtcRaster (레스터 픽셀 가공 지원 인터페이스)
      - IRtcSerialNo (일련번호 가공 지원 인터페이스)
      - IRtcSyncAxis (syncAXIS/XLSCAN 가공 지원 인터페이스)
         - RtcVirtual (RTC 제어기 가상)
         - Rtc4 (RTC4 제어기)
         - Rtc5 (RTC5 제어기)
         - Rtc6 (RTC6 제어기)
         - Rtc6Ethernet (RTC6 이더넷 제어기)
         - Rtc6SyncAxis (RTC6 제어기 + ExcelliSCAN + ACS 모션)
   - ICorrection2D (스캐너 2D 보정 인터페이스)
      - Correction2DRtc (스캐너 2D 보정. ctb/ct5 모두 지원. correXionPro.exe 기반)
      - Correction2DRtcCt5 (스캐너 2D 보정. ct5 지원. correXion5.exe 기반)
      - Correction2DRtcCtb (스캐너 2D 보정. ctb 지원. cfmp.exe 기반)
   - ICorrection3D (스캐너 3D 보정 인터페이스)
      - Correction3DRtc (스캐너 3D 보정. ctb/ct5 모두 지원. stretchcorreXion5.exe 기반)
      - Correction3DRtcCt5  (스캐너 3D 보정. ct5 지원. stretchcorreXion5.exe 기반)
      - Correction3DRtcCtb  (스캐너 3D 보정. ctb 지원. stretchcorreXion5.exe 기반)
   - IMatrixStack (3x3 행렬 인터페이스)
      - MatrixStack (3x3 행렬 기본버전)
   - ILaser (레이저 소스 인터페이스)
      - IShutterControl (셔터 제어 지원 인터페이스)
      - IPowerControl (파워 변경 지원 인터페이스)
      - IGuideControl (지시용 레이저 지원 인터페이스)
         - LaserVirtual (레이저 가상)
         - AdvancedOptoWaveAOPico 
         - AdvancedOptoWaveFotia
         - CoherentAviaLX
         - CoherentDiamondCSeries
         - CoherentDiamondJSeries
         - InnguGraceXSeries
         - IPGYLPN
         - IPGYLPTypeD
         - IPGYLPTypeE
         - IPGYLPULPN
         - JPTTypeE
         - PhotonicsIndustryDX
         - PhotonicsIndustryRGHAIO
         - SpectraPhysicsHippo
         - SPIG4
         - SpectraPhysicsTalon
   - IAttenuator (감쇄기 인터페이스)
      - AttenuatorVirtual (감쇄기 가상)
      - AltechnaWattPilot (Altechna사 Watt Pilot 제품)
   - IMotor (모터 인터페이스)
      - MotorVirtual (모터 가상)
      - MotorACS (ACS.SPiiPlusNET 사용 모터)
      - MotorAjinExtek (아진엑스텍 AXL 사용 모터)
      - MotorESP301 (NewPort ESP301 사용 모터)
   - IMotors (모터 집합 인터페이스)
      - MotorsDefault (모터 집합 기본 버전)    
   - IDInput (디지털 입력 인터페이스)
      - DInputVirtual (디지털 입력 가상)
      - RtcDInputExt1 (디지털 입력 RTC4/5/6 확장1 포트)
      - RtcDInput2Pin (디지털 입력 RTC5/6 레이저 포트 2핀)
      - AdlinkDInput (디지털 입력 ADLINK DASK 제품)
      - AjinExtekDInput (디지털 입력 아진엑스텍 제품)
   - IDInputTrigger (디지털 입력 트리거 인터페이스)
      - DInputTriggerA (디지털 입력 A접점용 트리거)
      - DInputTriggerB (디지털 입력 B접점용 트리거)
   - IDOutput (디지털 출력 인터페이스)
      - DOutputVirtual (디지털 출력 가상)
      - RtcDOutputExt1 (디지털 출력 RTC4/5/6 확장1 포트)
      - RtcDOutputExt2 (디지털 출력 RTC4/5/6 확장2 포트)
      - RtcDOutput2Pin (디지털 출력 RTC5/6 레이저 포트 2핀)
      - AdlinkDOutput (디지털 출력 ADLINK DASK 제품)
      - AjinExtekDOutput (디지털 출력 아진엑스텍 제품)     
   - IPowerMap (파워매핑 인터페이스)
      - PowerMapDefault (파워매핑 기본버전)
      - PowerMapSerializer (파워매핑 파일 읽기/쓰기)
   - IPowerMeter (파워메터 인터페이스)
      - PowerMeterVirtual (파워메터 가상)
      - PowerMeterCoherentPowerMax (코히런트 제품)
      - PowerMeterOphir (Ophir 제품)
      - PowerMeterThorLabsPMSeries (Thorlabs 제품)
         - PowerScaler (파워 배율 계산)


* spirallab.sirius.dll
   - SiriusViewerForm (뷰어 윈폼용 사용자 컨트롤)
   - SiriusEditorForm (편집기 윈폼용 사용자 컨트롤)
   - PathOptimizerForm (경로 최적화 윈폼)
   - Correction2DRtcForm (스캐너 2D 보정용 윈폼)
   - Correction3DRtcForm (스캐너 3D 보정용 윈폼)
   - MarkerForm (마커용 윈폼)
   - MarkerSyncAxisForm (syncAxis/XLSCAN 마커용 윈폼)
   - PensForm (펜 집합 편집용 윈폼)
   - RtcIOForm (RTC 확장 IO 제어용 윈폼)
   - MotorForm (단축 모터 제어용 윈폼)
   - MotorsForm (모터 집합 제어용 윈폼)
   - PowerMapForm (파워매핑 제어용 윈폼)
   - PowerMeterForm (파워메터 제어용 윈폼)
   - IMarker (마커 인터페이스)
      - MarkerDefault (마커 기본 버전)
      - IMarkerArg (마커 인자 인터페이스)
         - MarkerArgDefault (마커 인자 기본 버전)
   - IPens (펜 집합 인터페이스)
      - Pens (펜 집합 기본 버전)
      - PensSerializer (펜 집합 읽기/쓰기)
   - IDocument (문서)
      - DocumentDefault (문서 기본 버전)
      - DocumentSerializer (문서 읽기/쓰기)
      - Layers (레이어 집합)
   - IView (뷰 인터페이스)
      - ViewDefault (뷰 기본 버전)
   - IEntity (엔티티 개체 인터페이스)
      - IPen (펜 인터페이스)
         - PenDefault (펜 기본버전)
      - IDrawable (렌더링 지원 인터페이스)
      - IExplodable (분해 지원 인터페이스)
      - IHatchable (해치 지원 인터페이스)
      - IMarkerable (가공 지원 인터페이스)
      - IScriptable (스크립트 지원 인터페이스)
      - ITextChangeable (텍스트 데이타 변경 지원 인터페이스)
         - AlcBegin (자동 레이저 제어 시작)
         - AlcSyncAxisBegin / AlcSyncAxisEnd (syncAXIS 용 자동 레이저 제어 시작/종료)
         - Arc (호)
         - BarcodeDataMatrix2 (DataMatrix 바코드)
         - BarcodeQR2 (QR 바코드)
         - Bitmap (비트맵)
         - BlockInsert 
         - Circle (원)
         - Circle3D
         - Ellipse (타원)
         - Fiducial (기준점)
         - Group (그룹)
         - HPGL (HPGL 로고)
         - Jump (점프)
         - Layer (레이어)
         - Line (선분)
         - LwPolyline (폴리라인)
         - MeasurementBegin / MeasurementEnd (계측 시작/종료)
         - MotfAngularBegin (MOTF 회전 기반 시작)
         - MotfAngularWait (MOTF 회전 엔코더 각도 대기)
         - MotfBegin (MOTF 시작)
         - MotfEnd (MOTF 종료)
         - MotfExternalStartDelay (MOTF 지연)
         - MotfRepeat (MOTF 데이타 반복 가공)
         - MotfWait (MOTF 엔코더 위치 대기)
         - Point (점)
         - Points (점 복수개)
         - Raster (레스터 면적)
         - RasterLine (레스터 선분)
         - Rectangle (사각형)
         - SiriusText (시리우스 텍스트)
            - SiriusTextArc (시리우스 텍스트 호)
            - SiriusTextDate (시리우스 텍스트 날짜)
            - SiriusTextSerial (시리우스 텍스트 일련번호)
            - SiriusTextTime (시리우스 텍스트 시간)
         - Spiral  (나선)
         - StitchedImage (머신비전 이미지)
         - Stereolithography (STL 파일)
         - SyncAxisCalculationDynamics (syncAXIS 역학 제한값)
         - Text (텍스트)
            - TextArc (텍스트 호)
            - TextDate (텍스트 날짜)
            - TextSerial (텍스트 일련번호)
            - TextTime (텍스트 시간)
         - Timer (대기 타이머)
         - Trepan (트리팬)
         - Triangle (삼각형)
         - Triangle3D
         - VectorBegin / VectorEnd (벡터 의존 자동 레이저 제어 시작/종료)
         - WaitDataExt16If (확장1 포트 16비트 조건에 따른 대기)
         - WriteData (확장포트/아나로그 등 데이타 출력)
         - WriteDataExt16 (확장1 포트 16비트 출력)
         - WriteDataExt16If (조건에 따른 확장1 포트 16비트 출력)
         - ZDefocus (Z 디포커스)
         - ZOffset (Z 오프셋)
   - Action (액션 지원)
   - Block (블럭)
   - Blocks (블럭 집합)
   - User (사용자 로그인, 로그아웃 및 권한 처리)


* spirallab.sirius.fieldcorrection.dll
   - FormGridChecker (Grid Checker 출력 윈폼)


* spirallab.hpgl.dll, spirallab.hpglx64.dll 
   - native dll : win32/x64 
   - HPGLImportFile (HPGL 파일 가져오기)
   - HPGLPolyLineCount (내부 폴리라인 개수)
   - HPGLPolyLineVertexCount (지정된 폴라라인 정점 개수)
   - HPGLPolyLineVertexData (지정된 폴라라인의 정점 정보)
