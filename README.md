# SpiralLab.Sirius Library

----
**1. Descriptions**

 SuperEasy library for Control for Scanner and Lasers
 
 ![sirius](https://user-images.githubusercontent.com/58460570/70974494-38c41780-20eb-11ea-8567-afe02fab5441.png)

 
 ----
**2. Features**

 - support SCANLAB's RTC5, RTC6, RTC6 Ethernet multiple products. 
 - support RTC control with 3x3 matrix stack operation.
 - support RTC field correction with easy to use.
 - support unlimited vector data by RTC controller automatically.
 - support RTC's MOTF(marking on the fly), XL-SCAN(SyncAXIS) and sky-writing functions.
 - support many kinds of commerical laser sources (to the future...)
 - support laser power control with varios methods (like as analog, digital, frequency, pulse width modulation)
 - support entities : line, arc, LW polyline, rectangle, circle, true type font, spiral, trepan, group for multiple entities and layers.
 - support powerful undo/redo actions.
 - support Dxf/HPGL file format importer and sirius custom file format.
 - support 2D barcodes (DataMatrix, QR Code), 1D barcodes format (mark with dots or lines)
 - support 1 source(single document data), multiple view windows.
 - support customizable and extensible laser source and marker interface.
 - support laser path visualizer and simulator.
 - support all vector data are explodable by every lines and arcs.
 - support group entity with repeatable and reversible laser process.
 - support vary laser parameters with special entity called 'Pen' (frequency, pulse width, power(watt), scanner speeds, laser delays, sky writing , ...)
 
 ![sirius1](https://user-images.githubusercontent.com/58460570/70033764-74db8080-15f3-11ea-9e54-75b868e7d5ae.png)  
 
  ----
**3. How to use ?**

 - Development Environment : .NET dll library with x64 (x32 avaiable in the future)
 - Add spirallab.sirius.dll file as user control into Microsoft Visual Studio.
 - spirallab.sirius.motf.dll, spirallab.sirius.syncaxis.dll are optional
 - There are 2 winforms controls (Sirius.EditorForm and Sirius.ViewerForm)
 - There are multiple demo programs in DEMOS directory

![sirius3](https://user-images.githubusercontent.com/58460570/70033763-74db8080-15f3-11ea-926d-447ac6739d72.png)
 *The program running about 10 minutes in evalution copy mode !*
 
 ----
**4. Author**

 - hong chan, choi (labspiral@gmail.com)                           
 - homepage : http://sepwind.blogspot.com                        
 - git repository : https://github.com/labspiral/sirius.git
 - Please send email to me to use commerically.
  
![sirius2](https://user-images.githubusercontent.com/58460570/70033762-7442ea00-15f3-11ea-8788-2aae70ceacf8.png)

----
**5. Version history**

* 2020.01.07 v0.8 added IRtcDualHead interface, enhanced) points editor form, group offset form editor and minor bugs
* 2019.12.24 v0.7 added IRtc3D interface, added Group entity (with MOTF), fixed ICorrection2D, added ICorrection3D interface. 
* 2019.12.19 v0.6 added points entity (with path optimizer), support 3d (varioscan/z-shift) offset/defocus function in RTC. 
* 2019.12.17 v0.5 added 1/2D  barcode entities / support wobbel and raster operation in RTC
* 2019.12.12 v0.4 new sirius text entity  (support font format : *.cxf)
* 2019.12.11 v0.3 support MOTF(Marking On The Fly), XL-SCAN  (SyncAxis) by optional dll.
* 2019.12.10 v0.2 support HPGL(plt) file / Marker with scanner rotated angle / Spirallab Identifier program / IPen interface
* 2019.12.03 v0.1 first release

 
 [![](http://img.youtube.com/vi/pc70q_jc1Yw/0.jpg)](http://www.youtube.com/watch?v=pc70q_jc1Yw "SpiralLab.Sirius Library Demo")
