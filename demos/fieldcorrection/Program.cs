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
 * 스캐너 필드 보정
 * 
 * 3x3 (9개) 영역에 대한 스캐너 필드 왜곡 보정을 실시한다.
 * 나선 모양을 객체(Spiral Entity)를 생성하여 매 9개 위치에 각각 레이저를 출사한다.
 * 머신 비전등의 계측 장치로 측정된 9개의 위치 오차값을 사용한 새로운 스캐너 보정파일 생성한다.
 * Author : hong chan, choi / sepwind @gmail.com(https://sepwind.blogspot.com)
 * 
 */


using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using SpiralLab.Sirius;

namespace SpiralLab.Sirius
{
    class Program
    {
        static float kfactor = 0.0f;
        static void Main(string[] args)
        {
            SpiralLab.Core.Initialize();

            #region initialize RTC 
            var rtc = new RtcVirtual(0); ///가상 rtc 제어기 생성
            //var rtc = new Rtc5(0); ///rtc 5 제어기 생성
            float fov = 60.0f;    /// scanner field of view : 60mm                                
            kfactor = (float)Math.Pow(2, 20) / fov; /// k factor (bits/mm) = 2^20 / fov
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);    ///default correction file
            rtc.CtlFrequency(50 * 1000, 2); ///laser frequency : 50KHz, pulse width : 2usec
            rtc.CtlSpeed(100, 100); /// default jump and mark speed : 100mm/s
            rtc.CtlDelay(10, 100, 200, 200, 0); ///scanner and laser delays
            #endregion

            #region initialize Laser(Virtual)
            ILaser laser = new LaserVirtual(0, "virtual", 20);
            #endregion

            #region create entities for scanner field correction
            var doc = new DocumentDefault("3x3 scanner field correction");
            var layer = new Layer("default");
            doc.Layers.Add(layer);
            /// create spiral shapes = 9 counts (3x3)  
            layer.Add(new Spiral(-20.0f, 20.0f, 0.2f, 2.0f, 5, true));
            layer.Add(new Spiral(0.0f, 20.0f, 0.2f, 2.0f, 5, true));
            layer.Add(new Spiral(20.0f, 20.0f, 0.2f, 2.0f, 5, true));
            layer.Add(new Spiral(-20.0f, 0.0f, 0.2f, 2.0f, 5, true));
            layer.Add(new Spiral(0.0f, 0.0f, 0.2f, 2.0f, 5, true));
            layer.Add(new Spiral(20.0f, 0.0f, 0.2f, 2.0f, 5, true));
            layer.Add(new Spiral(-20.0f, -20.0f, 0.2f, 2.0f, 5, true));
            layer.Add(new Spiral(0.0f, -20.0f, 0.2f, 2.0f, 5, true));
            layer.Add(new Spiral(20.0f, -20.0f, 0.2f, 2.0f, 5, true));
            var ds = new DocumentSerializer();
            ds.Save(doc, "test.sirius");
            #endregion

            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Testcase for spirallab.sirius. powered by labspiral@gmail.com (https://sepwind.blogspot.com)");
                Console.WriteLine("");
                Console.WriteLine("'F' : draw field correction entities");
                Console.WriteLine("'C' : create new field correction for 2D");
                Console.WriteLine("'Q' : quit");
                Console.WriteLine("");
                Console.Write("select your target : ");
                key = Console.ReadKey(false);
                if (key.Key == ConsoleKey.Q)
                    break;
                switch (key.Key)
                {                    
                    case ConsoleKey.F:
                        Console.WriteLine("\r\nWARNING !!! LASER IS BUSY ...");
                        var timer = new Stopwatch();
                        if (DrawForFieldCorrection(laser, rtc, doc))
                            rtc.ListExecute(true);
                        Console.WriteLine($"processing time = {timer.ElapsedMilliseconds / 1000.0:F3}s");
                        break;
                    case ConsoleKey.C:
                        string result = CreateFieldCorrection(rtc);
                        Console.WriteLine("");
                        Console.WriteLine(result);
                        break;
                }               

            } while (true);

            rtc.Dispose();
        }
        /// <summary>
        /// 레이어 안에 있는 모든 객체들을 마킹하기 (3x3 의 나선 객체가 마킹됨)
        /// </summary>
        /// <param name="rtc"></param>
        /// <param name="doc"></param>
        private static bool DrawForFieldCorrection(ILaser laser, IRtc rtc, IDocument doc)
        {
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
                rtc.ListEnd();
            return success;
        }
        /// <summary>
        /// 나선 객체의 위치를 측정하여 해당 오류값을 넣기 
        /// </summary>
        /// <returns></returns>
        private static string CreateFieldCorrection(IRtc rtc)
        {

            var srcFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            var targetFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", $"newfile.ct5");

            var correction = new RtcCorrection2D(0, kfactor, 3, 3, srcFile, targetFile);

            #region inputs relative error deviation : 상대적인 오차값을 넣는 방법
            correction.AddRelative(0, 0, new Vector2(-20, 20), new Vector2(0.01f, 0.01f));
            correction.AddRelative(0, 1, new Vector2(0, 20), new Vector2(0.01f, 0.01f));
            correction.AddRelative(0, 2, new Vector2(20, 20), new Vector2(0.01f, 0.01f));
            correction.AddRelative(1, 0, new Vector2(-20, 0), new Vector2(0.01f, 0.01f));
            correction.AddRelative(1, 1, new Vector2(0, 0), new Vector2(0.01f, 0.01f));
            correction.AddRelative(1, 2, new Vector2(20, 0), new Vector2(0.01f, 0.01f));
            correction.AddRelative(2, 0, new Vector2(-20, -20), new Vector2(0.01f, 0.01f));
            correction.AddRelative(2, 1, new Vector2(0, -20), new Vector2(0.01f, 0.01f));
            correction.AddRelative(2, 2, new Vector2(20, -20), new Vector2(0.01f, 0.01f));
            #endregion

            #region inputs absolute position values : 절대적인 오차값을 넣는 방법
            //correction.AddAbsolute(0, 0, new Vector3(-20, 20, 0), new Vector3(-20.01f, 20.01f, 0));
            //correction.AddAbsolute(0, 1, new Vector3(0, 20, 0), new Vector3(0.01f, 20.01f, 0));
            //correction.AddAbsolute(0, 2, new Vector3(20, 20, 0), new Vector3(20.01f, 20.01f, 0));
            //correction.AddAbsolute(1, 0, new Vector3(-20, 0, 0), new Vector3(-20.01f, 0.01f, 0));
            //correction.AddAbsolute(1, 1, new Vector3(0, 0, 0), new Vector3(0.01f, 0.01f, 0));
            //correction.AddAbsolute(1, 2, new Vector3(20, 0, 0), new Vector3(20.01f, 0.01f, 0));
            //correction.AddAbsolute(2, 0, new Vector3(-20, -20, 0), new Vector3(-20.01f, -20.01f, 0));
            //correction.AddAbsolute(2, 1, new Vector3(0, -20, 0), new Vector3(0.01f, -20.01f, 0));
            //correction.AddAbsolute(2, 2, new Vector3(20, -20, 0), new Vector3(20.01f, -20.01f, 0));
            #endregion

            bool success = correction.Convert();
            success &= rtc.CtlLoadCorrectionFile(CorrectionTableIndex.Table1, targetFile);
            success &= rtc.CtlSelectCorrection(CorrectionTableIndex.Table1);
            if (success)
                return correction.ResultMessage;
            else
                return "fail to convert new correction file !";
        }
    }
}
