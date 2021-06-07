# SpiralLab.Sirius Library

----
**1. Descriptions**

 SuperEasy library for Control for Scanner and Lasers
 
 ![sirius](https://user-images.githubusercontent.com/58460570/70974494-38c41780-20eb-11ea-8567-afe02fab5441.png)

 ![barcode](https://user-images.githubusercontent.com/58460570/117915869-130f6200-b321-11eb-928f-7c4f08c1af70.png)
 
 ![text with arc](https://user-images.githubusercontent.com/58460570/117915901-215d7e00-b321-11eb-8055-5502aad8bf85.png)


 ----
**2. Features**

 - support SCANLAB's RTC5, RTC6, RTC6 Ethernet, XL-SCAN(SyncAXIS) multiple products. 
 - support RTC control with 3x3 matrix stack operation.
 - support RTC field correction with easy to use.
 - support unlimited vector data by RTC controller automatically.
 - support RTC's MOTF(marking on the fly), Dual Head, 3D (like as VarioSCAN) options.
 - support many kinds of commerical laser sources (support customizable ILaser interface)
 - support laser power control with varios methods (like as analog, digital, frequency, pulse width modulation)
 - support entities : line, arc, LW polyline, rectangle, circle, true type font, cxf custome font, 1D/2D barcodes, spiral, trepan, group for multiple entities and layers.
 - support powerful undo/redo actions.
 - support Dxf/HPGL file format importer and sirius custom file format.
 - support 2D barcodes (DataMatrix, QR Code), 1D barcodes format (mark with dots, lines, outlines, hatch or custom patter each cells)
 - support 1 source(single document data), multiple view windows.
 - support customizable and extensible marker interface.
 - support laser path visualizer and simulator.
 - support all vector data are explodable by every sub entities.
 - support group entity with repeatable support each x,y, angle offsets.
 - support vary laser parameters with special entity called 'Pen' (frequency, pulse width, power(watt), scanner speeds, laser delays, sky writing , ...)
 
 ![sirius1](https://user-images.githubusercontent.com/58460570/70033764-74db8080-15f3-11ea-9e54-75b868e7d5ae.png)  
 
  ----
**3. How to use ?**

 - Development Environment : .NET dll library with x64
 - Add references spirallab.core.dll, spirallab.sirius.rtc.dll and spirallab.sirius.dll file into Microsoft Visual Studio.
 - spirallab.sirius.dll file support user control : Sirius.EditorForm and Sirius.ViewerForm
 - There are multiple demo programs in DEMOS directory

![sirius3](https://user-images.githubusercontent.com/58460570/70033763-74db8080-15f3-11ea-926d-447ac6739d72.png)
 *The program running about 3 hours in evalution copy mode !*
 
 ----
**4. Author**

 - e-mail : labspiral@gmail.com
 - homepage : http://spirallab.co.kr                        
 - git repository : https://github.com/labspiral/sirius.git
 - phone : +82-10-9619-3896
 - Please contact to me to use commerically.
  
![sirius2](https://user-images.githubusercontent.com/58460570/70033762-7442ea00-15f3-11ea-8788-2aae70ceacf8.png)

----
**5. Version history**

* 2021.06.07 v1.6 added Stitched Image entity, added Write Data Ext16 io entity, added Sirius Project
* 2021.06.01 v1.5 added List Data entity. support JPG, GIF, Png Image Format. added Write Data entity
* 2021.05.29 v1.5 added Custom Editor/Viewer Demo Project
* 2021.05.21 v1.5 added Bitmap entity, added ICompensator interface each analog digital io. added Raster entity, AlcVectorBegin/End entity
* 2021.05.20 v1.4 added IRtcAutoLaserControl interface, support position, speed, vector define automatic laser control, xy coordinate with Vector2/3 struct
* 2021.05.19 v1.3 added HPGL entity
* 2021.05.12 v1.3 added CharacterSet, 3D, MOTF, Dual head demo project 
                  added Sirius Text Arc entity, Text Arc entity
                  added WPF demo project
                  fixed barcode (QR/Datamatrix/1D ...) cell with dot/line/outline/hatch or imported pattern
                  merged separated dll into single spiral.sirius.rtc.dll
                  fixed to be similary properties at sirius text and text entity 
                  fixed location/rotate bugs each entities
                  fixed minor bugs

* 2020.01.29 v0.9 added Hatch function, repeat count, Demo project for custom RTC, fixed SiriusView crash on design time, fixed minor bugs
* 2020.01.07 v0.8 added IRtcDualHead interface, enhanced) points editor form, group offset form editor and minor bugs
* 2019.12.24 v0.7 added IRtc3D interface, added Group entity (with MOTF), fixed ICorrection2D, added ICorrection3D interface. 
* 2019.12.19 v0.6 added points entity (with path optimizer), support 3d (varioscan/z-shift) offset/defocus function in RTC. 
* 2019.12.17 v0.5 added 1/2D  barcode entities / support wobbel and raster operation in RTC
* 2019.12.12 v0.4 new sirius text entity  (support font format : *.cxf)
* 2019.12.11 v0.3 support MOTF(Marking On The Fly), XL-SCAN  (SyncAxis) by optional dll.
* 2019.12.10 v0.2 support HPGL(plt) file / Marker with scanner rotated angle / Spirallab Identifier program / IPen interface
* 2019.12.03 v0.1 first release

 
 [![](http://img.youtube.com/vi/pc70q_jc1Yw/0.jpg)](http://www.youtube.com/watch?v=pc70q_jc1Yw "SpiralLab.Sirius Library Demo")
