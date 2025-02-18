﻿/*
 *                                                            ,--,      ,--,                              
 *             ,-.----.                                     ,---.'|   ,---.'|                              
 *   .--.--.   \    /  \     ,---,,-.----.      ,---,       |   | :   |   | :      ,---,           ,---,.  
 *  /  /    '. |   :    \ ,`--.' |\    /  \    '  .' \      :   : |   :   : |     '  .' \        ,'  .'  \ 
 * |  :  /`. / |   |  .\ :|   :  :;   :    \  /  ;    '.    |   ' :   |   ' :    /  ;    '.    ,---.' .' | 
 * ;  |  |--`  .   :  |: |:   |  '|   | .\ : :  :       \   ;   ; '   ;   ; '   :  :       \   |   |  |: | 
 * |  :  ;_    |   |   \ :|   :  |.   : |: | :  |   /\   \  '   | |__ '   | |__ :  |   /\   \  :   :  :  / 
 *  \  \    `. |   : .   /'   '  ;|   |  \ : |  :  ' ;.   : |   | :.'||   | :.'||  :  ' ;.   : :   |    ;  
 *   `----.   \;   | |`-' |   |  ||   : .  / |  |  ;/  \   \'   :    ;'   :    ;|  |  ;/  \   \|   :     \ 
 *   __ \  \  ||   | ;    '   :  ;;   | |  \ '  :  | \  \ ,'|   |  ./ |   |  ./ '  :  | \  \ ,'|   |   . | 
 *  /  /`--'  /:   ' |    |   |  '|   | ;\  \|  |  '  '--'  ;   : ;   ;   : ;   |  |  '  '--'  '   :  '; | 
 * '--'.     / :   : :    '   :  |:   ' | \.'|  :  :        |   ,/    |   ,/    |  :  :        |   |  | ;  
 *   `--'---'  |   | :    ;   |.' :   : :-'  |  | ,'        '---'     '---'     |  | ,'        |   :   /   
 *             `---'.|    '---'   |   |.'    `--''                              `--''          |   | ,'    
 *               `---`            `---'                                                        `----'   
 * 
 * 레이저및 스캐너의 가공 파라메터를 일컬어 통상 "펜(Pen)" 파라메터라 하며, 이 펜 객체(Entity)를 사용해 다양한 가공 조건 (속도및 지연값등)을 설정한다.
 * 문서(IDocument)에는 기본적으로 10개의 펜(IPen)을 내부에서 생성후 제공한다. 이를 활용하는 방법을 설명한다
 * How to query and edit pen entity on document 
 * 
 * 신규방식) 현재 버전에서는 편집기에서 자동으로 펜 집합 기능이 활성화 되어 있어, 약 10여개의 미리 생성된 펜 집합을 제공해 줍니다.
 * 편집기 내부에는 펜 집합 편집기(Pens Editor)가 내장되어 개별 개체의 색상(Color2)에 의해 자동으로 해당 펜 객체의 가공 파라메타가 적용되는 방식을 제공하고 있습니다.
 * 
 * Author : hong chan, choi / hcchoi@spirallab.co.kr (http://spirallab.co.kr)
 * 
 */


using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    class Program2
    {
        [STAThread]
        static void Main2(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SpiralLab.Core.Initialize();

            #region initialize RTC 
            // create Rtc for dummy (가상 RTC 카드)
            //var rtc = new RtcVirtual(0); 
            // create Rtc5 controller
            var rtc = new Rtc5(0);
            // create Rtc6 controller
            //var rtc = new Rtc6(0); 
            // create Rtc6 Ethernet controller
            //var rtc = new Rtc6Ethernet(0, "192.168.0.100", "255.255.255.0"); 

            // theoretically size of scanner field of view (이론적인 FOV 크기) : 60mm
            float fov = 60.0f;
            // RTC4: k factor (bits/mm) = 2^16 / fov
            //float kfactor = (float)Math.Pow(2, 16) / fov;
            // RTC5/6: k factor (bits/mm) = 2^20 / fov
            float kfactor = (float)Math.Pow(2, 20) / fov;

            // RTC4: full path of correction file
            //var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ctb");
            // RTC5/6: full path of correction file
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            // initialize RTC controller
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);
            // laser frequency : 50KHz, pulse width : 2usec (주파수 50KHz, 펄스폭 2usec)
            rtc.CtlFrequency(50 * 1000, 2);
            // scanner jump and mark speed : 500mm/s (점프, 마크 속도 500mm/s)
            rtc.CtlSpeed(500, 500);
            // laser and scanner delays (레이저/스캐너 지연값 설정)
            rtc.CtlDelay(10, 100, 200, 200, 0);
            #endregion

            #region initialize Laser (virtual)
            // virtual laser source with max 20W power (최대 출력 20W 의 가상 레이저 소스 생성)
            var laser = new LaserVirtual(0, "virtual", 20);
            laser.PowerControlMethod = PowerControlMethod.Unknown;
            //var laser = new IPGYLPTypeD(0, "IPG YLP D", 1, 20);
            //var laser = new IPGYLPTypeE(0, "IPG YLP E", 1, 20);
            //var laser = new IPGYLPN(0, "IPG YLP N", 1, 100);
            //var laser = new JPTTypeE(0, "JPT Type E", 1, 20);
            //var laser = new SPIG4(0, "SPI G3/4", 1, 20);
            //var laser = new PhotonicsIndustryDX(0, "DX", 1, 20);
            //var laser = new PhotonicsIndustryRGHAIO(0, "RGHAIO", 1, 20);
            //var laser = new AdvancedOptoWaveFotia(0, "Fotia", 1, 20);
            //var laser = new AdvancedOptoWaveAOPico(0, "AOPico", 1, 20);
            //var laser = new CoherentAviaLX(0, "Avia LX", 1, 20);
            //var laser = new CoherentDiamondJSeries(0, "Diamond JSeries", "10.0.0.1", 200.0f);
            //var laser = new CoherentDiamondCSeries(0, "Diamond CSeries", 1, 100.0f);
            //var laser = new SpectraPhysicsHippo(0, "Hippo", 1, 30);
            //var laser = new SpectraPhysicsTalon(0, "Talon", 1, 30);

            // assign RTC controller at laser 
            laser.Rtc = rtc;
            // initialize laser source
            laser.Initialize();
            // set basic power output to 2W
            laser.CtlPower(2);
            #endregion

            #region create entities
            // 문서 생성
            // create sirius document
            var doc = new DocumentDefault("Unnamed");

            // doc.Pens 에는 10개의 기본 펜(PenDefault)이 준비되어 있다. 기본색(흰색) 펜을 가져와서 편집
            // 10가지의 색상정보는 Config.PensColor 배열 참고            
            // query pen entity (white) at document
            var pen1 = doc.Pens.ColorOf(System.Drawing.Color.White);
            // 파라메터 값을 변경
            // edit
            var penDefault1 = pen1 as PenDefault;
            penDefault1.Frequency = 100 * 1000; //주파수 Hz
            penDefault1.PulseWidth = 2; //펄스폭 usec
            penDefault1.LaserOnDelay = 0; // 레이저 시작 지연 usec
            penDefault1.LaserOffDelay = 0; // 레이저 끝 지연 usec
            penDefault1.ScannerJumpDelay = 100; // 스캐너 점프 지연 usec
            penDefault1.ScannerMarkDelay = 200; // 스캐너 마크 지연 usec
            penDefault1.ScannerPolygonDelay = 0; // 스캐너 폴리곤 지연 usec
            penDefault1.JumpSpeed = 500; // 스캐너 점프 속도 mm/s
            penDefault1.MarkSpeed = 500; // 스캐너 마크 속도 mm/s

            // query pen entity (yellow) at document
            var pen2 = doc.Pens.ColorOf(System.Drawing.Color.Yellow);
            // 파라메터 값을 변경
            // edit
            var penDefault2 = pen2 as PenDefault;
            penDefault1.Frequency = 50 * 1000; //주파수 Hz
            penDefault1.PulseWidth = 2; //펄스폭 usec
            penDefault1.LaserOnDelay = 0; // 레이저 시작 지연 usec
            penDefault1.LaserOffDelay = 0; // 레이저 끝 지연 usec
            penDefault1.ScannerJumpDelay = 100; // 스캐너 점프 지연 usec
            penDefault1.ScannerMarkDelay = 200; // 스캐너 마크 지연 usec
            penDefault1.ScannerPolygonDelay = 0; // 스캐너 폴리곤 지연 usec
            penDefault1.JumpSpeed = 1000; // 스캐너 점프 속도 mm/s
            penDefault1.MarkSpeed = 1000; // 스캐너 마크 속도 mm/s

            // 레이어 생성및 문서에 추가
            var layer = new Layer("default");
            // 선 개체 레이어에 추가
            var line = new Line(0, 0, 10, 20);
            line.Color2 = System.Drawing.Color.White; //선 개체의 펜 색상을 흰색으로 지정
            layer.Add(line);
            // 원 개체 레이어에 추가
            var circle = new Circle(0, 0, 10);
            line.Color2 = System.Drawing.Color.White;
            layer.Add(circle);
            // 나선 개체 레이어에 추가
            var spiral = new Spiral(-20.0f, 0.0f, 0.5f, 2.0f, 5, true);
            spiral.Color2 = System.Drawing.Color.Yellow; //나선 개체의 펜 색상을 노란색으로 지정
            layer.Add(spiral);
            // 레이어의 모든 개채들 내부 데이타 계산및 갱신
            layer.Regen();
            // 문서에 레이어 추가
            doc.Layers.Add(layer);
            doc.Layers.Active = layer;

            // 문서를 지정된 파일에 저장
            // save sirius document
            DocumentSerializer.Save(doc, "test.sirius");
            #endregion

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("Testcase for spirallab.sirius. powered by hcchoi@spirallab.co.kr (http://spirallab.co.kr)");
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("'D' : draw entities by pen");
                Console.WriteLine("'Q' : quit");
                Console.WriteLine($"{Environment.NewLine}");
                Console.Write("select your target : ");
                key = Console.ReadKey(false);
                if (key.Key == ConsoleKey.Q)
                    break;
                Console.WriteLine($"{Environment.NewLine}");
                switch (key.Key)
                {
                    case ConsoleKey.D:
                        Console.WriteLine("WARNING !!! LASER IS BUSY ...");
                        var timer = Stopwatch.StartNew();
                        DrawForFieldCorrection(laser, rtc, doc);
                        Console.WriteLine($"processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s");
                        break;
                }

            } while (true);

            rtc.Dispose();
            laser.Dispose();
        }
        /// <summary>
        /// 레이어 안에 있는 모든 객체들을 마킹하기 (3x3 의 나선 객체가 마킹됨)
        /// </summary>
        /// <param name="rtc"></param>
        /// <param name="doc"></param>
        private static bool DrawForFieldCorrection(ILaser laser, IRtc rtc, IDocument doc)
        {
            bool success = true;
            var markerArg = new MarkerArgDefault()
            {
                Document = doc,
                Rtc = rtc,
                Laser = laser,
                IsEnablePens = true, //문서(doc) 내부에 사전에 생성된 10개의 펜 집합(Pens)을 사용하므로 이를 활성화
            };
            success &= rtc.ListBegin(laser);
            // 레이어 순회
            foreach (var layer in doc.Layers)
            {
                //레이어 가공
                success &= layer.Mark(markerArg);
                // or
                // 직접 하나씩 처리방법. 레이어 내의 개체들을 순회
                //foreach (var entity in layer)
                //{
                //    var markerable = entity as IMarkerable;
                //    // 해당 개체가 레이저 가공이 가능한지 여부를 판별
                //    if (null != markerable)
                //        success &= markerable.Mark(markerArg);    // 레이저 가공 실시
                //    if (!success)
                //        break;
                //}
                if (!success)
                    break;
            }
            if (success)
            {
                success &= rtc.ListEnd();
                success &= rtc.ListExecute(true);
            }
            return success;
        }        
    }
}
