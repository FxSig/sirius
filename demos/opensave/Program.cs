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
 * Author : hong chan, choi / sepwind @gmail.com(https://sepwind.blogspot.com)
 * 
 */


using System;
using System.Diagnostics;
using System.Numerics;

namespace SpiralLab.Sirius
{
    class Program
    {
        static void Main(string[] args)
        {
            SpiralLab.Core.Initialize();

            #region create entities 
            var doc1 = new DocumentDefault("Unnamed");
            var layer = new Layer("default");
            doc1.Layers.Add(layer);
            layer.Add(new Line(0, 10, 20,20));
            layer.Add(new Circle(0, 0, 10));
            layer.Add(new Spiral(-20.0f, 0.0f, 0.5f, 2.0f, 5, true));
            #endregion

            Console.WriteLine("press any key to save ...");
            Console.ReadKey(false);
            string filename = "default.sirius";

            var ds = new DocumentSerializer();
            ds.Save(doc1, filename);

            Console.WriteLine("press any key to open ...");
            Console.ReadKey(false);
            var doc2 = DocumentSerializer.OpenSirius(filename);

            Console.WriteLine("press any key to rtc initialize ...");
            Console.ReadKey(false);

            #region initialize RTC 
            IRtc rtc = new RtcVirtual(0); ///가상 rtc 제어기 생성
            //IRtc rtc = new Rtc5(0); ///rtc 5 제어기 생성
            double fov = 60.0;    /// scanner field of view : 60mm            
            double kfactor = Math.Pow(2, 20) / fov; /// k factor (bits/mm) = 2^20 / fov
            rtc.Initialize(kfactor, LaserMode.Yag1, "cor_1to1.ct5");    /// 스캐너 보정 파일 지정 : correction file
            rtc.CtlFrequency(50 * 1000, 2); /// laser frequency : 50KHz, pulse width : 2usec
            rtc.CtlSpeed(100, 100); /// default jump and mark speed : 100mm/s
            rtc.CtlDelay(10, 100, 200, 200, 0); /// scanner and laser delays
            #endregion

            #region initialize Laser (virtial)
            ILaser laser = new LaserVirtual(0, "virtual", 20);
            #endregion

            Console.WriteLine("press any key to laser processing ...WARNING !!!  LASER EMISSION");
            Console.ReadKey(false);
            DoBegin(laser, rtc, doc2);

            Console.WriteLine("press any key to terminate program");
            Console.ReadKey(false);
        }

        static void DoBegin(ILaser laser, IRtc rtc, IDocument doc)
        {
            var timer = new Stopwatch();
            bool success = true;
            rtc.ListBegin(laser);
            foreach (var layer in doc.Layers)
            {
                foreach (var entity in layer)
                {
                    var markerable = entity as IMarkerable;
                    if (null != markerable)
                        success &= markerable.Mark(rtc);
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
