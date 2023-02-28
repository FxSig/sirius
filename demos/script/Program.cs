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
 * C# 코드를 스크립트로 활용하여 특정 엔티티의 데이타 (텍스트 엔티티의 데이타 내용을 조합하는등) 를 변경하는 기능
 * Description : 텍스트및 바코드 개체들은 Script 기능 지원을 위해 IScriptable 인터페이스를 제공함
 * Author : hong chan, choi / hcchoi@spirallab.co.kr (http://spirallab.co.kr)
 * 
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // initializing spirallab.sirius library engine 
            SpiralLab.Core.Initialize();
            
            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine($"{Environment.NewLine}");
                Console.WriteLine("Testcase for spirallab.sirius. powered by hcchoi@spirallab.co.kr (http://spirallab.co.kr)");
                Console.WriteLine($"----------------------------------------------------------------------------------------");
                Console.WriteLine("'C' : C# script");
                Console.WriteLine("'T' : Text entity data by script");
                Console.WriteLine("'B' : Barcode entity data by script");
                Console.WriteLine("'Q' : quit");
                Console.Write("Select your target : ");
                key = Console.ReadKey(false);
                Console.WriteLine($"{Environment.NewLine}");
                if (key.Key == ConsoleKey.Q)
                    break;
                switch (key.Key)
                {                    
                    case ConsoleKey.C:
                        CSharp();
                        break;
                    case ConsoleKey.T:
                        TextData();
                        break;
                    case ConsoleKey.B:
                        BarcodeData();
                        break;
                }
            } while (true);
        }

        static void CSharp()
        {
            string lineOfCodes = @"
            using System; 
            namespace Test 
            {
                class MyClass
                {
                    public string Name { get; set; }
                    
                    public void PrintName()
                    {                        
                        Console.WriteLine(Name);
                    }
                }
            }";

            //create scripe helper
            var script = new ScriptHelper(ScriptHelper.Providers.CSharp, lineOfCodes);
            if (false == script.Compile(out var errors))
            {
                foreach (var err in errors)
                {
                    Console.WriteLine(err.ToString());
                }
                Console.ReadKey();
                return;
            }
            else
            {
                Console.WriteLine("No error(s)");
            }

            //create instance
            if (!script.CreateInstance("Test.MyClass", out var instance))
            {
                Console.WriteLine("fail to create instance !");
                Console.ReadKey();
                return;
            }
            //property set/get 
            script.SetProperty("Name", "Powered by SpiralLab.Sirius");
            if (!script.GetProperty("Name", out var propValue))
            {
                Console.WriteLine("fail to get value from property");
                Console.ReadKey();
                return;
            }
            else
                Console.WriteLine($"Property value : {propValue}");

            //function call
            if (!script.Function("PrintName", null, out var functionReturnValue))
            {
                Console.WriteLine("fail to call function");
                Console.ReadKey();
                return;
            }
            else
                Console.WriteLine($"Function return : {functionReturnValue}");
        }

        static void TextData()
        {
            var text = new Text()
            {
                FontName = "OCRA.ttf",
                IsFixedAspectRatio = false,
                Width = 5,
                CapHeight = 5,
                LetterSpacing = 0,
                WordSpacing = 1,
                Align = Alignment.LeftBottom,
                Location = new System.Numerics.Vector2(0, 0),
            };
            text.FontText = "HELLO WORLD";
            text.Regen();

            string lineOfCodes = @"
            using System; 
            namespace Test 
            {
                class MyClass
                {
                    public string YourText
                    { 
                        get
                        {
                            return string.Format(""Hello World : {0}"", this.Arg);
                        }
                    }
                    public string Arg { get; set; }
                }
            }";
            text.ScriptInstanceName = "Test.MyClass";
            text.ScriptPropertyName = "YourText";
            text.ScriptArguments = new string[] { "Arg=SpiralLab" };

            if (text.ScriptCompile(lineOfCodes, out var errors))
            {
                Console.WriteLine("No error(s)");
            }
            else
            {
                foreach (var err in errors)
                {
                    Console.WriteLine(err.ToString());
                }
                Console.ReadKey();
                return;
            }
            text.ScriptExecute(out var executeResult, true);
            var fontText = text.TextData;
            Debug.Assert(0 == string.Compare(executeResult, fontText));
            Console.WriteLine($"Text entity has changed to {executeResult}");
        }

        static void BarcodeData()
        {
            var qr = new BarcodeQR2()
            {
                Width = 5,
                Height = 5,
                Align = Alignment.LeftBottom,
                Location = new System.Numerics.Vector2(0, 0),
                ShapeType = BarcodeShapeType.Hatch,
            };
            qr.CellHatch.HatchInterval = 0.1f;
            qr.CellHatch.HatchMode = HatchMode.CrossLine;

            qr.Data = "HELLO SPIRALLAB SIRIUS LIBRARY";
            qr.Regen();

            string lineOfCodes = @"
            using System; 
            namespace Test 
            {
                class MyClass
                {
                    public string BarcodeData
                    { 
                        get
                        {
                            return string.Format(""{0}-{1}-{2}"", this.Year, this.Month, this.Day);
                        }
                    }
                    public string Year { get; set; }
                    public string Month { get; set; }
                    public string Day { get; set; }
                }
            }";
            qr.ScriptInstanceName = "Test.MyClass";
            qr.ScriptPropertyName = "BarcodeData";
            qr.ScriptArguments = new string[] 
            { 
                "Year=2022",
                "Month=08",
                "Day=31",
            };

            if (qr.ScriptCompile(lineOfCodes, out var errors))
            {
                Console.WriteLine("No error(s)");
            }
            else
            {
                foreach (var err in errors)
                {
                    Console.WriteLine(err.ToString());
                }
                Console.ReadKey();
                return;
            }
            qr.ScriptExecute(out var executeResult, true);
            var qrData = qr.TextData;
            Debug.Assert(0 == string.Compare(executeResult, qrData));
            Console.WriteLine($"Barcode entity has changed to {executeResult}");
        }
    }
}
