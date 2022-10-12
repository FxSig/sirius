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
 * 레이저 소스 객체 생성 및 통신, 제어 기법
 * 일부 벤더의 레이저 소스의 경우 별도 윈폼(Winforms)이 제공됨
 * Description : 레이저 소스 객체 생성시 통신 포트 및 최대 파워(W)를 반드시 지정 필요
 * Author : hong chan, choi / hcchoi@spirallab.co.kr (http://spirallab.co.kr)
 * 
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    public partial class LaserControlForm : Form
    {


        public LaserControlForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var laser = new AdvancedOptoWaveFotia(0, "Fotia", 1, 5);
            bool success = laser.Initialize();

            {
                //var form = new SpiralLab.Sirius.Laser.LaserForm();
                //form.AliasName = "Fotia";
                //form.Laser = laser;
                //form.ShowDialog(this);
            }

            //전용 폼 제공됨
            {
                var form = new SpiralLab.Sirius.Laser.AdvancedOptoWaveFotiaForm(laser);
                form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                form.ShowDialog(this);
            }
            laser.Dispose();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var laser = new PhotonicsIndustryDX(0, "PI DX", 1, 5);
            bool success = laser.Initialize();

            {
                //var form = new SpiralLab.Sirius.Laser.LaserForm();
                //form.AliasName = "PI DX";
                //form.Laser = laser;
                //form.ShowDialog(this);
            }

            //전용 폼 제공됨
            {
                var form = new SpiralLab.Sirius.Laser.PhotonicsIndustryDXForm(laser);
                form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                form.ShowDialog(this);
            }
            laser.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var laser = new CoherentAviaLX(0, "Coherent Avia LX", 1, 5);
            bool success = laser.Initialize();

            {
                var form = new SpiralLab.Sirius.Laser.LaserForm(laser);
                form.AliasName = "Avia LX";
                form.ShowDialog(this);
            }
            
            laser.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var laser = new IPGYLPTypeD(0, "YLP TYPE D", 1, 20);
            bool success = laser.Initialize();

            {
                //var form = new SpiralLab.Sirius.Laser.LaserForm();
                //form.AliasName = "TYPE D";
                //form.Laser = laser;
                //form.ShowDialog(this);
            }

            //전용 폼 제공됨
            {
                var form = new SpiralLab.Sirius.Laser.IPGYLPTypeDForm(laser);
                form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                form.ShowDialog(this);
            }
            laser.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var laser = new IPGYLPTypeE(0, "YLP TYPE E", 1, 20);
            bool success = laser.Initialize();

            {
                //var form = new SpiralLab.Sirius.Laser.LaserForm();
                //form.AliasName = "TYPE E";
                //form.Laser = laser;
                //form.ShowDialog(this);
            }

            //전용 폼 제공됨
            {
                var form = new SpiralLab.Sirius.Laser.IPGYLPTypeEForm(laser);
                form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                form.ShowDialog(this);
            }
            laser.Dispose();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var laser = new JPTTypeE(0, "JPT TYPE E", 1, 20);
            bool success = laser.Initialize();

            {
                var form = new SpiralLab.Sirius.Laser.LaserForm(laser);
                form.AliasName = "TYPE E";
                form.ShowDialog(this);
            }

            laser.Dispose();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var laser = new JPTTypeE(0, "SPI G3/4", 1, 20);
            bool success = laser.Initialize();

            {
                var form = new SpiralLab.Sirius.Laser.LaserForm(laser);
                form.AliasName = "G3/4";
                form.ShowDialog(this);
            }

            laser.Dispose();
        }

        
        private void button8_Click(object sender, EventArgs e)
        {
            int index = 0;
            var rtc = new Rtc5(index); //create Rtc5 controller
            //var rtc = new Rtc6(index); //create Rtc6 controller
            
            float fov = 60.0f;    // scanner field of view : 60mm            
            float kfactor = (float)Math.Pow(2, 20) / fov; // k factor (bits/mm) = 2^20 / fov
            var correctionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");

            bool success = true;
            success &= rtc.Initialize(kfactor, LaserMode.Yag1, correctionFile);    // correction file (스캐너 보정 파일)
            success &= rtc.CtlFrequency(50 * 1000, 2); // laser frequency : 50KHz, pulse width : 2usec (주파수 50KHz, 펄스폭 2usec)
            success &= rtc.CtlSpeed(500, 500); // jump and mark speed : 500mm/s (점프, 마크 속도 500mm/s)
            success &= rtc.CtlDelay(10, 100, 200, 200, 0); // scanner and laser delays (스캐너/레이저 지연값 설정)

            var laser = new DemoIPGYLPTypeE(index, "YLP TYPE E", 1, 20);
            laser.Rtc = rtc;


            var rtcDInExt1 = new RtcDInputExt1(rtc, index, "DIN RTC EXT1");
            success &= rtcDInExt1.Initialize();
            var rtcDOutExt1 = new RtcDOutputExt1(rtc, index, "DOUT RTC EXT1");
            success &= rtcDOutExt1.Initialize();
            var rtcDOutExt2 = new RtcDOutputExt2(rtc, index, "DIN RTC EXT2");
            success &= rtcDOutExt2.Initialize();

            laser.RtcDInExt1 = rtcDInExt1;
            laser.RtcDOutExt1 = rtcDOutExt1;
            laser.RtcDOutExt2 = rtcDOutExt2;

            success &= laser.Initialize();
            var form = new SpiralLab.Sirius.Laser.LaserForm(laser);
            form.AliasName = "TYPE E";
            form.ShowDialog(this);

            laser.Dispose();
            rtcDInExt1.Dispose();
            rtcDOutExt1.Dispose();
            rtcDOutExt2.Dispose();
            rtc.Dispose();
        }
    }
}
