# SpiralLab.Sirius Library

**1. Descriptions**

 SuperEasy library for Control Scanner and Laser

 
 ![pulse on demand](http://www.spirallab.co.kr/wp-content/uploads/2022/01/image-6.png)
 
 ![scanner field correction by image](http://www.spirallab.co.kr/wp-content/uploads/2022/01/image-5.png)
 
 ![scanner field correction](http://www.spirallab.co.kr/wp-content/uploads/2022/01/image-7.png)

 ![script](http://www.spirallab.co.kr/wp-content/uploads/2022/01/image-8.png)

 ![path](http://www.spirallab.co.kr/wp-content/uploads/2022/01/image-9.png)

 ![barcode](http://www.spirallab.co.kr/wp-content/uploads/2021/06/image-21-1024x686-2.png)
 
 ![stitchedimage](http://www.spirallab.co.kr/wp-content/uploads/2022/01/image-11.png)

 ![xlscan](http://www.spirallab.co.kr/wp-content/uploads/2022/02/Screenshot_Viewer_XLSCAN_colorMap-1.png)
 
 ![xlscan with job](http://www.spirallab.co.kr/wp-content/uploads/2022/02/image-24.png)

 ![xlscan with viewer](http://www.spirallab.co.kr/wp-content/uploads/2022/02/image-25-1508x1080.png)

 ![text with arc](https://user-images.githubusercontent.com/58460570/117915901-215d7e00-b321-11eb-8055-5502aad8bf85.png)

 ![multiple views](http://www.spirallab.co.kr/wp-content/uploads/2022/01/image-10.png)

 ![measurement](http://www.spirallab.co.kr/wp-content/uploads/2022/01/image-13.png)

 ![path optimizer](http://www.spirallab.co.kr/wp-content/uploads/2022/01/ezgif.com-gif-maker.gif)

  ----

**2. Features**

 - support SCANLAB's RTC4, RTC5, RTC6, RTC6 Ethernet multiple products.
 - support SyncAxis(XL-SCAN) with multiple heads/stages combination.
 - support powerful 3x3 matrix operation with stack.
 - support processing unlimited vector data by automatically.
 - support MOTF(marking on the fly), dual head, 3d (like as VarioSCAN) features.
 - support 2D/3D field correction with easy to use and with powerful image analyzer.
 - support scanner motion/signal profiles with plotted graph.
 - support executable C# script codes.
 - support powermeters and power mapping interface
 - support vary laser power with PoD(pulse on demand by analog, 8/16bits digital, frequency, pulse width modulation) and sky-writing.
 - support point, line, arc, LW polyline, rectangle, circle, true type font, cxf font, 1D/2D barcodes, spiral, trepan, dxf, hpgl(plt) and cutomizable entities with layers.
 - support 2D barcodes with various cell type like as dots, lines, outlines, hatch.
 - support laser beam path visualizer (simulator) and path optimizer algorithm.
 - support powerful undo/redo actions and single document data with multiple view targets.
 - support vary laser parameters with 'Pen' (frequency, pulse width, power, speeds, laser delays, sky writing , ...)
 - support customizable and extensible marker interface.
 - support many kinds of power meter
   - Ophir USBI
   - Thorlab PM100USB
 - support many kinds of laser sources
   - Advanced Optowave Fotia
   - Coherent Avia LX
   - Photonics Industry DX
   - IPG YLP Type D/E
   - SPI G3/4
   - JPT Type E
 
  ----

**3. How to use ?**

 - Development Environment : .NET dll library 
 - Add spirallab.core.dll, spirallab.sirius.rtc.dll and spirallab.sirius.dll file reference into Microsoft Visual Studio.
 - user control in spirallab.sirius.dll file : SpiralLab.Sirius.EditorForm, SpiralLab.Sirius.ViewerForm
 - x64 Environment : copy files from bin\x64 to bin\  
 - x32 Environment : copy files from bin\x32 to bin\ 
 - There are multiple demo programs in DEMOS directory
 - There are stand-alone programs in DEMOS\Sirius directory

 *The program running about 30 mins in evalution copy mode !*
 
 
 ----

**4. Author**

 - developer page : http://www.spirallab.co.kr/?page_id=229
 - git repository : https://github.com/labspiral/sirius.git
 - e-mail : labspiral@gmail.com
 - homepage : http://spirallab.co.kr                        
 
 
----

**5. Version history**

* 2022.3.7 v.1.98
   - added) improved hatch options (��ġ �ɼ� ��� ��ȭ)
     - hatch repeat count (��ġ ���� �ݺ� ȸ�� ��������)
     - mark outline or not (��ġ�� �ܰ��� �������� ����)
     - mark outline first (��ġ�� �ܰ����� �켱 �������� ����)
   - added) jump entity (���� ��ü �߰���)
   - fixed) internal angle offset within group entity bug (�׷찳ü���� ������ ���� x,y,angle ���� ����)
   - fixed) negative datetime offset bug (��¥ �ð� �����¿� ������ ó�� ����)
   - fixed) works only 30mins at evaluation copy mode (���� ��忡�� 30�и� ����)

* 2022.2.28 v.1.97
   - added) sky writing with mode 1,2,3 at pen parameter (�� �Ķ������ sky writing ��� 1,2,3 �� ���� ����)
   - fixed) serial entity (SiriuslTextSerial, TextSerial)
     - internal/external trigger mode (��ȣ ������ ���� Ʈ���Ÿ� ����/�ܺ� ���� ����)
     - internal : increased whenever try to mark manually (��ŷ�� �������� �Ҷ� ���� ���� ����)
     - external : external /START triggger automatically (�ܺ� Ʈ���Ű� �߻��Ҷ� ���� �ڵ� ����)
   - added) text with datetime format (�ؽ�Ʈ ��ü�� �ð� �������� �ڵ� ��ȯ ����)
     - if datetime option enabled, text data re-formatted by automatically (DateTime �ɼ� Ȱ��ȭ�� ��ŷ �ؽ�Ʈ�� ������ �м��Ǿ� �����)
     - datetime start/end seperator : '%'' or '{'', '}''
     - example1) {yyyyMMdd} -> 20220225
     - example2) {yyyy/MM/dd HH:mm:ss} -> 2022/02/25 05:50:06
     - example3) A123{yyyyMMdd}GOOD -> A12320220225GOOD 
     - example4) A123{yyyy}XY{MM}AB{dd}GOOD -> A1232022XY02AB25GOOD 
   - added) IRtcDateTimeOffset
     - to support modify offset datetime (��¥ �ð� ������ ����� ���� IRtcDateTimeOffset �������̽� �߰�)
   - added) IRtc3D interface 
     - read/writable A,B,C coefficient at correction file (3D ���� ������ ��� ������ ������ A,B,C ��� �а�,���� �� ��������)
     - get focal length at specific x,y,z location (Ư�� 3D ��ġ���� ���� �Ÿ� ��� ��� �߰�)
   - fixed) restart versioning 1.9.6 -> 1.97 (���� ���� ��ȣ ����)

* 2022.2.21 v1.9.6
   - improved) scanner field correction with vary images (�پ��� �̹��� ���ǿ� �µ��� �˰��� �߰� ����)
   - added) jump to origin position after finished to mark (���� �Ϸ��� �������� �ڵ� ���� �ϴ� �ɼ�)
   - added) changable correction file within rtc property dialog (RTC �Ӽ�â���� ��ĳ�� ���� ���� ���� ����)
   - qualified) syncaxis   
     - field tested at equipment (��񿡼� �ʵ� �׽�Ʈ)
   - fixed) ramp with arc/ polyline bug (arc, polyline �� ������ ramp ��� ���� ����)
   - fixed) layer 
     - always works as repeat by 1 bug (���̾� �ݺ� ȸ���� 1�� �����Ǵ� ���� �ذ�)
   - fixed) datamatrix /qrcode barcode when cell type line
     - pitch error with zig zag mark

* 2022.2.14 v1.9.5
   - fixed) syncaxis   
     - enumerable job history (�ִ� 50�� ������ �۾� �̷� ��ȸ�� �����մϴ�)
     - auto start when job is enough (���� ����Ÿ�� ��л��°� �Ǹ� �ڵ� ������ �ǽ��ϴ� ��� ����)
     - multiple head offsets with matrix bug (��Ƽ ��庰 ����� ���� ������ ���� �ذ�)
     - utilization ratio with position, acc, jerk (��ġ, ���ӵ�, �����ӵ��� ���� �м� ����)
   - fixed) rtc 4,5,6 
     - laser control signal (�Ӽ�â���� ������ ��� ��ȣ ���� ����)
     - laser mode (�Ӽ�â���� ������ ��� ���� ����)
     - external control (�Ӽ�â���� ������ �ܺ� ��ȣ ���� ���� ����)
     - marking info (�Ӽ�â���� marking info ���� ��ȸ ����)
   - added) new barcode entity for easy to use (����� ������ ���ο� 2D ���ڵ� ��ü ����)
     - datamatrix v2 
     - qrcode v2

* 2022.2.7 v1.9.4
   - fixed) syncaxis v1.6 (syncaxis 1.6 ���������� 4���� ��Ƽ ��ĵ��� * 4���� �������� ������)
       - job characteristic (�۾� Ư�� �м� ����)
       - config trajectory (mark/geometry) (��� ���� ����)
       - config dynamics (scanner/stage) (���� ���� ����)
   - added) datetime offset with rtc 5, 6 

* 2022.2.2 v1.9.3
   - added) spirallab.sirius.fieldcorrection with hatched images
   - fixed) syncaxis
       - pen parameter : spot distance compensation, min.mark speed 
       - layer parameter : bandwidth 
       - marker form with job/trajectory/dynamics 

* 2022.1.24 v1.9.2
   - fixed) vector-defined laser control bug (���� ��� ������ ��� ���� ���� ����)
   - fixed) simplify automatic laser control by velocity (�ӵ��� ���� ������ �ڵ� ���� ����ȭ)
   - added) SiriusEditorForm with lock/unlock feature (������ ������ ��� ��� �߰�)
   - fixed) syncaxis 
        - added) config dynamics
        - added) job status / job event callback / job characteristic
        - added) on/off simulation or hardware mode / simulation filename 
        - added) get actual postion of stage, scanner(s)
        - added) operation status (green/yellow/red)
        - fixed) improved demo program with editor_syncaxis

* 2022.1.12 v1.9.1
   - added) docs\sirius.pdf document file (����� ���� ������Ʈ)
   - added) support powerful measurement of signals (������ ���� ��� ����)
     - MeasurementBegin/End entity (MeasurementBegin/End ��ƼƼ �̿�)
     - create measurement data automatically and plot to graph in Marker Form (��Ŀ ȭ�鿡�� �ɼǻ������� ���� ����Ÿ ���� ����, �׷����� �÷� ��� ����)
   - added) measurement demo project (���� ���� ������Ʈ �߰�)
   - added) support various way of read/write extension i/o port (Ȯ�� ��Ʈ�� �̿��� ����Ÿ �б�/���� ���� ����)
     - write data entity (�پ��� Ȯ�� ��Ʈ�� ����Ÿ ����)
     - write data ext16 entity (Ȯ�� 1��Ʈ�� ���� ��Ʈ ����)
     - write data ext16 if entity (Ȯ�� 1��Ʈ �Է� ��Ʈ ����ũ ���ǿ� ���� ��� ��Ʈ ����ũ�� ����)
     - wait data ext16 if entity (Ȯ�� 1��Ʈ �Է� ��Ʈ ����ũ ���ǿ� ���� ����ϱ�)
    - fixed) improve scanner field correction by image  (�̹����� �̿��� ��ĳ�� ���� ��� ����)
    - fixed) property values of timer entity is not shown (Ÿ�̸� ��ü�� �Ӽ����� �Ϻ� ǥ�õ��� �ʴ� ���� �ذ�)

* 2022.1.3 v1.9.0
   - added) image analyzer for scanner field correction (��ĳ�� ������ ���� �̹��� �м��� �߰�)
     - added) spirallab.sirius.fieldcorrection.dll (�ش� dll �߰���)
	 - copy x64\OpenCvSharpExtern.dll(or x32) into bin directory (�÷����� �°� �ش� ������ bin ���丮�� �����Ұ�)
   - added) laser control demo programs (������ ���� Ȱ�� ���� �ڵ� �߰�)

* 2021.12.22 v1.8.9
   - fixed) register fonts(characterset) into rtc bug (��Ʈ �ٿ�ε�� ���� ������ ���� ���� �� ���ʿ� ����Ʈ ���� ����)

* 2021.12.18 v1.8.8
   - fixed) no nodes at treeview bug (Ʈ���信 ��尡 ������Ʈ ���� �ʴ� ����)
   - fixed) encoder pulses/mm can be negate value with motf (MOTF �� ���� ���ڴ� �����ϰ��� ���� �Է� ����)

* 2021.12.10 v1.8.7
   - added) support laser signal activate/deactivate at RTC (������ ��ȣ Ȱ��ȭ/��Ȱ��ȭ ��� ����)
   - added) editable MOTF encoder simulation speed in marker winforms (��Ŀâ���� �ùķ��̼� ���ڴ� �ӵ� �Է�â ����)
   - fixed) invalid encoder reset when MOTF begins (MOTF ���۽� ���ڴ� ���� ���� Ȱ��ȭ ���� ����)
   - fixed) crash bug when refresh propertygrid at laser winform (������ �ҽ������� �Ӽ�â �������� ����)
   - fixed) lag of aborting marker bug (��Ŀ���� ������ �������� ����), marker support measurement event (��Ŀ�� ���� �̺�Ʈ �ڵ鷯 �߰���)
   - fixed) sub entities withing group entity are supported script and register fonts (�׷쳻�� ���Ե� ��ũ��Ʈ ��ü�� ��Ʈ ��ü ������)
   - added) powermeter/ powermap winforms in SiriusEditorForm (�Ŀ�����/�Ŀ����� ���� �߰�)

* 2021.12.5 v1.8.6
   - fixed) scanner 3d field correction bug (3D ������ dat ���� ���� ���� ����)
   - fixed) readdata/writedata bug in rtc (RTC read/write data ȣ��� ���� Ÿ�� ��ȯ ���� ����)
   - added) adlink DAQ ����� ����
   - added) powermeter interface (�Ŀ����� �������̽� �߰�)
     - ophir usbi (OPHIR ���� USBI ��ǰ)
     - thorlab pm100usb (Thorlabs ���� PM100USB ��ǰ)
    - added) powermap interface (�Ŀ����� �������̽� �߰�)

* 2021.11.26 v1.8.5
   - updated) Scanlab Rtc5 up-to-date v.2021_10_22 (RTC5 dll ���̺귯�� �ֽ� �������� ������Ʈ)
   - updated) Scanlab Rtc6 up-to-date v.2021_11_12  (RTC6 dll ���̺귯�� �ֽ� �������� ������Ʈ)
   - added) Convert correction option with "delete input dat file"  (��ĳ�� �ʵ� ������ dat ���� �������� �ɼ�)
   - fixed) Convert correctionPro.exe bug (reference bits) (CorrectionPro ������ ������ �Է� ���� �ذ�)
   - fixed) Reset correction3d z-upper/lower value bug (��ĳ�� 3D ������ Z �� ���� ���� �ذ�)
   - fixed) Ellipse entity's mark bug - leakage beam at jump started (Ÿ�� ������ ������ ��ġ�� �� ���� �ذ�)

* 2021.11.21 v1.8.4
   - added) new scanner correction with (c)SCANLAB Correction Pro (��ĵ���� CorrectionPro ���α׷� ����� ���� ����)
     - added) Correction2DRtc / Correction2DRtcForm (�ΰ��� �ű� Ŭ���� �߰���)
   - fixed) fail to scanner field correct when edge position of area has inputted (X,Y ������ġ�� ������ ���� ���� ��� �߻��ϴ� ���� �ذ�)

* 2021.11.19 v1.8.3
   - fixed) rtc4 list buffer handling bug, rtc4 CtlWriteData bug (RTC4 ���� ��뷮 ����Ÿ ���Ƿ� ���� ������ 3õ���� ����Ÿ�� �����Ǵ� ���� �ذ�)
   - fixed) readyonly bug at correction3d winforms (Correction3D �������� �Ϻ� ����Ÿ �Է� �Ұ� ���� �ذ�)
   - added) stitched images are saved now (StitchedImage �� ������ ������ �̹����� ���Ͽ� ���� ��������)

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