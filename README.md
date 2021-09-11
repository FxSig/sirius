# SpiralLab.Sirius Library

**1. Descriptions**

 SuperEasy library for Control for Scanner and Lasers

 
 ![bitmap](http://www.spirallab.co.kr/wp-content/uploads/2021/08/pod.png)
 
 ![path](http://www.spirallab.co.kr/wp-content/uploads/2021/05/image-19-1024x622.png)
 
 ![sirius](https://user-images.githubusercontent.com/58460570/70974494-38c41780-20eb-11ea-8567-afe02fab5441.png)

 ![barcode](https://user-images.githubusercontent.com/58460570/117915869-130f6200-b321-11eb-928f-7c4f08c1af70.png)
 
 ![stitchedimage](http://www.spirallab.co.kr/wp-content/uploads/2021/06/unnamed-5.png)
 
 ![text with arc](https://user-images.githubusercontent.com/58460570/117915901-215d7e00-b321-11eb-8055-5502aad8bf85.png)


  ----

**2. Features**

 - support SCANLAB's RTC4, RTC5, RTC6, RTC6 Ethernet, XL-SCAN(SyncAXIS) multiple products. 
 - support RTC control with 3x3 matrix stack operation.
 - support RTC field correction with easy to use.
 - support unlimited vector data by RTC controller automatically.
 - support RTC's MOTF(marking on the fly), Dual Head, 3D (like as VarioSCAN) options.
 - support many kinds of commerical laser sources (support customizable ILaser interface)
 - support laser power control with POD(pulse on demand by analog, 8/16bits digital, frequency, pulse width modulation) and sky-writing
 - support entities : line, arc, LW polyline, rectangle, circle, true type font, cxf custome font, 1D/2D barcodes, spiral, trepan, dxf, hpgl(plt), group for multiple entities and layers.
 - support 2D barcodes mark with dots, lines, outlines, hatch or custom patter each cells
 - support powerful undo/redo actions.
 - support 1 source(single document data), multiple view windows.
 - support customizable and extensible marker interface.
 - support laser beam path visualizer (simulator) and path optimizer algorithm.
 - support vary laser parameters with special entity called 'Pen' (frequency, pulse width, power(watt), scanner speeds, laser delays, sky writing , ...)
 - support user executable script codes (C#) with scriptable entities
 
  ![path optimizer](http://www.spirallab.co.kr/wp-content/uploads/2021/07/pathopt-1024x806.png)  
 
  ----

**3. How to use ?**

 - Development Environment : .NET dll library 
 - Add references spirallab.core.dll, spirallab.sirius.rtc.dll and spirallab.sirius.dll file into Microsoft Visual Studio.
 - spirallab.sirius.dll file support user control : SpiralLab.Sirius.EditorForm / SpiralLab.Sirius.ViewerForm
 - There are multiple demo programs in DEMOS directory
 - There are stand-alone programs in DEMOS\Sirius directory
 - (x64) Post build event at Visual Studio "Copy /Y $(TargetDir)freetype6_x64.dll $(TargetDir)freetype6.dll"
 - (x32) Post build event at Visual Studio "Copy /Y $(TargetDir)freetype6_x32.dll $(TargetDir)freetype6.dll"

![stitched image](http://www.spirallab.co.kr/wp-content/uploads/2021/06/unnamed-5-1-2.png)

 *The program running about 3 hours in evalution copy mode !*
 
 ----

**4. Author**

 - e-mail : labspiral@gmail.com
 - homepage : http://spirallab.co.kr                        
 - git repository : https://github.com/labspiral/sirius.git
 - phone : +82-10-9619-3896
 - Please contact to me to use commerically.
  
----

**5. Version history**

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