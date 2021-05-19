/*
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
 *
 * Document 문서( 가공 데이타) 저장, 열기
 * 
 * 문서(document) 는 레이어, 블럭 , 환경 설정 및 가공에 필요한 다양한 객체(Entity : 선, 호, 원, 폴리라인, 레이저 파라메터 등) 정보를 가지고 있다.
 * 이 가공 객체(Entity)를 생성하고, 저장이 가능하며 또한 레이저 가공을 시도한다.
 * Author : hong chan, choi / labspiral @gmail.com(http://spirallab.co.kr)
 * 
 */


using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;

namespace SpiralLab.Sirius
{
    class Program
    {
        static void Main(string[] args)
        {
            SpiralLab.Core.Initialize();

            #region create entities 
            //신규 문서(Document) 생성
            var doc1 = new DocumentDefault("Unnamed");
            // 레이어 생성
            var layer = new Layer("default");
            //레이어에 선 형상 개체(Entity) 생성및 추가
            layer.Add(new Line(0, 10, 20,20));
            //레이어에 원 형상 개체(Entity) 생성및 추가
            layer.Add(new Circle(0, 0, 10));
            //레이어에 나선 형상 개체(Entity) 생성및 추가
            layer.Add(new Spiral(-20.0f, 0.0f, 0.5f, 2.0f, 5, true));
            // 레이어를 문서에 추가
            doc1.Layers.Add(layer);
            #endregion

            Console.WriteLine("press any key to save ...");
            Console.ReadKey(false);
            string filename = "default.sirius";

            // 문서(Document) 저장하기
            DocumentSerializer.Save(doc1, filename);

            Console.WriteLine("press any key to open ...");
            Console.ReadKey(false);
            // 문서(Document) 불러오기
            var doc2 = DocumentSerializer.OpenSirius(filename);

            Console.WriteLine("press any key to rtc initialize ...");
            Console.ReadKey(false);

            #region initialize RTC 
            //var rtc = new RtcVirtual(0); //create Rtc for dummy
            var rtc = new Rtc5(0, "output.txt"); //create Rtc5 controller with list commands output file
            //var rtc = new Rtc6(0); //create Rtc6 controller
            //var rtc = new Rtc6Ethernet(0, "192.168.0.100", "255.255.255.0"); //실험적인 상태 (Scanlab Rtc6 Ethernet 제어기)
            //var rtc = new Rtc6SyncAxis(0, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configuration", "syncAXISConfig.xml")); //실험적인 상태 (Scanlab XLSCAN 솔류션)

            float fov = 60.0f;    // scanner field of view : 60mm            
            float kfactor = (float)Math.Pow(2, 20) / fov; // k factor (bits/mm) = 2^20 / fov
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);    // 스캐너 보정 파일 지정 : correction file
            rtc.CtlFrequency(50 * 1000, 2); // laser frequency : 50KHz, pulse width : 2usec
            rtc.CtlSpeed(100, 100); // default jump and mark speed : 100mm/s
            rtc.CtlDelay(10, 100, 200, 200, 0); // scanner and laser delays
            #endregion

            #region initialize Laser (virtual)
            ILaser laser = new LaserVirtual(0, "virtual", 20);
            #endregion

            Console.WriteLine("press any key to laser processing ...WARNING !!!  LASER EMISSION");
            Console.ReadKey(false);
            DoBegin(laser, rtc, doc2);

            Console.WriteLine("press any key to terminate program");
            Console.ReadKey(false);
        }

        /// <summary>
        /// 지정된 문서(Document)를 지정된 RTC 제어기로 가공하기
        /// </summary>
        /// <param name="laser"></param>
        /// <param name="rtc"></param>
        /// <param name="doc"></param>
        static void DoBegin(ILaser laser, IRtc rtc, IDocument doc)
        {
            var timer = Stopwatch.StartNew();
            bool success = true;
            var markerArg = new MarkerArgDefault()
            {
                Document = doc,
                Rtc = rtc,
                Laser = laser,
            };
            rtc.ListBegin(laser);
            //레이어를 순회
            foreach (var layer in doc.Layers)
            {
                //레이어 내의 개체(Entity)들을 순회
                foreach (var entity in layer)
                {
                    var markerable = entity as IMarkerable;
                    //레이저 가공이 가능한 개체(markerable)인지를 판단
                    if (null != markerable)
                        success &= markerable.Mark(markerArg);    // 해당 개체(Entity) 가공 
                    if (!success)
                        break;
                }
                if (!success)
                    break;
            }
            if (success)
            {
                rtc.ListEnd();
                rtc.ListExecute(true);
            }
            Console.WriteLine($"processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s");
        }
    }
}
