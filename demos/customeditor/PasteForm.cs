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
 * Copyright(C) 2020 hong chan, choi. labspiral@gmail.com
 * Copyright (C) 2010-2020 SpiralLab. All rights reserved. 
 * paste form
 * Description : 붙혀넣기용 폼
 * Author : hong chan, choi / labspiral@gmail.com (http://spirallab.co.kr)
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{

    internal partial class PasteForm : Form
    {
        public List<IEntity> Clipboard
        {
            set
            {
                this.lbsClipboard.Items.Clear();
                this.lbsClipboard.Items.AddRange(value.ToArray());

                this.txtRowPitch.Text = string.Empty;
                this.txtColPitch.Text = string.Empty;
                var br = new BoundRect();
                foreach(var e in value)
                {
                    br.Union(e.BoundRect);
                }
                if (!br.IsEmpty)
                {
                    this.txtColPitch.Text = $"{br.Width:F3}";
                    this.txtRowPitch.Text = $"{br.Height:F3}";
                }
            }
        }

        public List<Vector2> Result
        {
            get
            {
                float rowPitch = float.Parse(this.txtRowPitch.Text);
                float colPitch = float.Parse(this.txtColPitch.Text);
                int rows = int.Parse(this.numRows.Value.ToString());
                int cols = int.Parse(this.numCols.Value.ToString());
                var list = new List<Vector2>();
                bool zigZag = chkZigZag.Checked;

                float x = 0;
                float y = 0;
                for (int row = 0; row < rows; row++)
                {
                    x = 0;
                    y = row * rowPitch;
                    if (zigZag == true && row % 2 == 1)
                    {
                        for (int col = cols-1; col >= 0; col--)
                        {
                            x = col * colPitch;
                            list.Add(new Vector2(x, y));
                        }
                    }
                    else
                    {
                        for (int col = 0; col < cols; col++)
                        {
                            x = col * colPitch;
                            list.Add(new Vector2(x, y));
                        }
                    }
                }
                return list;
            }
        }

        public Vector2 Position
        {
            get
            {
                float x = float.Parse(this.txtX.Text);
                float y = float.Parse(this.txtY.Text);
                return new Vector2(x,y);
            }
        }
        public PasteForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += PasteForm_KeyDown;
        }

        private void PasteForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }
    }
}
