﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    /// <summary>
    /// 다양한 스캐너 보정용 윈폼
    /// </summary>
    public partial class CorrectionForm : Form
    {
        public CorrectionForm()
        {
            InitializeComponent();
        }

        private void btn2DCtb_Click(object sender, EventArgs e)
        {
            // current correction file
            // 현재 스캐너 보정 파일
            var srcFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ctb");
            // target correction file
            // 신규로 생성할 스캐너 보정 파일
            var targetFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", $"newfile.ctb");

            // 3x3 (9개) 위치에 대한 보정 테이블 입력 용
            float fieldSize = 60;
            float interval = 20;
            int rows = 3;
            int cols = 3;
            float kfactor = (float)Math.Pow(2, 16) / fieldSize;
            var correction = new Correction2DRtc(kfactor, rows, cols, interval, interval, srcFile, targetFile);

            #region inputs relative error deviation : 상대적인 오차위치 값을 넣는 방법 (머신 비전 오차값을 넣는 것과 유사)
            // 해당 X,Y 위치에서의 오차값 dx, dy 입력
            correction.AddRelative(0, 0, new Vector2(-20, 20), new Vector2(0.01f, 0.01f));
            correction.AddRelative(0, 1, new Vector2(0, 20), new Vector2(0.002f, 0.001f));
            correction.AddRelative(0, 2, new Vector2(20, 20), new Vector2(-0.0051f, 0.01f));
            correction.AddRelative(1, 0, new Vector2(-20, 0), new Vector2(0.01f, 0.01f));
            correction.AddRelative(1, 1, new Vector2(0, 0), new Vector2(0.0f, 0.0f));
            correction.AddRelative(1, 2, new Vector2(20, 0), new Vector2(-0.01f, 0.01f));
            correction.AddRelative(2, 0, new Vector2(-20, -20), new Vector2(0.01f, 0.002f));
            correction.AddRelative(2, 1, new Vector2(0, -20), new Vector2(0.005f, -0.003f));
            correction.AddRelative(2, 2, new Vector2(20, -20), new Vector2(0.002f, -0.008f));
            #endregion

            var form = new Correction2DRtcForm(correction);
            form.ShowDialog();
        }
        private void btn3DCtb_Click(object sender, EventArgs e)
        {
            // current correction file
            // 현재 스캐너 보정 파일
            var srcFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ctb");
            // target correction file
            // 신규로 생성할 스캐너 보정 파일
            var targetFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", $"newfile.ctb");

            // it should be done with field correction at Z= 0 for 3D correction
            // 우선 Z= 0 (2D) 영역에 대한 정밀 보정을 진행한후 3D 보정이 진행되어야 한다 
            float fieldSize = 60;
            float interval = 20;
            int row = 3;
            int col = 3;
            float zUpper = 5;
            float zLower = -5;
            float kfactor = (float)Math.Pow(2, 16) / fieldSize;
            var correction = new Correction3DRtc(kfactor, row, col, interval, zUpper, zLower, srcFile, targetFile);

            #region inputs relative error deviation : 상대적인 오차위치 값을 넣는 방법 (머신 비전 오차값을 넣는 것과 유사)
            // Z= 5mm 위치에 포커스를 하여 레이저를 출사하고 그 오차를 입력
            // 두께 5mm 의 fixture 장착후 그 위에 마킹후 측정한다던지
            // 혹은 스캐너 Z 축을 -5 mm 이동시켜 마킹후 측정한다던지
            correction.AddRelative(0, 0, new Vector3(-20, 20, zUpper), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(0, 1, new Vector3(0, 20, zUpper), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(0, 2, new Vector3(20, 20, zUpper), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 0, new Vector3(-20, 0, zUpper), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 1, new Vector3(0, 0, zUpper), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 2, new Vector3(20, 0, zUpper), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 0, new Vector3(-20, -20, zUpper), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 1, new Vector3(0, -20, zUpper), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 2, new Vector3(20, -20, zUpper), new Vector3(0.01f, 0.01f, 0));

            // Z= -5mm 위치에 포커스를 하여 레이저를 출사하고 그 오차를 입력
            // 혹은 스캐너 Z 축을 5 mm 이동시켜 마킹후 측정한다던지
            correction.AddRelative(0, 0, new Vector3(-20, 20, zLower), new Vector3(0.01f, -0.02f, 0));
            correction.AddRelative(0, 1, new Vector3(0, 20, zLower), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(0, 2, new Vector3(20, 20, zLower), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 0, new Vector3(-20, 0, zLower), new Vector3(-0.01f, 0.01f, 0));
            correction.AddRelative(1, 1, new Vector3(0, 0, zLower), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 2, new Vector3(20, 0, zLower), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 0, new Vector3(-20, -20, zLower), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 1, new Vector3(0, -20, zLower), new Vector3(0.01f, -0.01f, 0));
            correction.AddRelative(2, 2, new Vector3(20, -20, zLower), new Vector3(0.01f, 0.01f, 0));
            #endregion

            var form = new Correction3DRtcForm(correction);
            form.ShowDialog();
        }
        private void btn2DCt5_Click(object sender, EventArgs e)
        {
            // 현재 스캐너 보정 파일
            var srcFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            // 신규로 생성할 스캐너 보정 파일
            var targetFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", $"newfile.ct5");

            // 3x3 (9개) 위치에 대한 보정 테이블 입력 용
            float fieldSize = 60;
            float rowInterval = 20;
            float colInterval = 20;
            int row = 3;
            int col = 3;
            float kfactor = (float)Math.Pow(2, 20) / fieldSize;
            var correction = new Correction2DRtc(kfactor, row, col, rowInterval, colInterval, srcFile, targetFile);

            #region inputs relative error deviation : 상대적인 오차위치 값을 넣는 방법 (머신 비전 오차값을 넣는 것과 유사)
            correction.AddRelative(0, 0, new Vector2(-20, 20), new Vector2(0.01f, 0.01f));
            correction.AddRelative(0, 1, new Vector2(0, 20), new Vector2(0.002f, 0.001f));
            correction.AddRelative(0, 2, new Vector2(20, 20), new Vector2(-0.0051f, 0.01f));
            correction.AddRelative(1, 0, new Vector2(-20, 0), new Vector2(0.01f, 0.01f));
            correction.AddRelative(1, 1, new Vector2(0, 0), new Vector2(0.0f, 0.0f));
            correction.AddRelative(1, 2, new Vector2(20, 0), new Vector2(-0.01f, 0.01f));
            correction.AddRelative(2, 0, new Vector2(-20, -20), new Vector2(0.01f, 0.002f));
            correction.AddRelative(2, 1, new Vector2(0, -20), new Vector2(0.005f, -0.003f));
            correction.AddRelative(2, 2, new Vector2(20, -20), new Vector2(0.002f, -0.008f));
            #endregion

            var form = new Correction2DRtcForm(correction);
            form.ShowDialog();
        }
        private void btn3DCt5_Click(object sender, EventArgs e)
        {
            // 현재 스캐너 보정 파일
            var srcFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", "cor_1to1.ct5");
            // 신규로 생성할 스캐너 보정 파일
            var targetFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "correction", $"newfile.ct5");

            // 우선 Z= 0 (2D) 영역에 대한 정밀 2D 보정을 완료한 이후 3D 보정이 진행되어야 한다 !
            float fieldSize = 60;
            float interval = 20;
            int row = 3;
            int col = 3;

            //
            //   Z = +5 mm  -------------- Z Upper ------------------- 
            //
            //
            //   Z = 0      -------------- Z  =  0-------------------
            //
            //
            //   Z = -5 mm  -------------- Z Lower ------------------- 
            //
            float zUpper = 5;
            float zLower = -5;
            float kfactor = (float)Math.Pow(2, 20) / fieldSize;
            var correction = new Correction3DRtc(kfactor, row, col, interval, zUpper, zLower, srcFile, targetFile);

            #region inputs relative error deviation : 상대적인 오차위치 값을 넣는 방법 (머신 비전 오차값을 넣는 것과 유사)
            // Z= 5mm 위치에 포커스를 하여 레이저를 출사하고 그 오차를 입력
            correction.AddRelative(0, 0, new Vector3(-20, 20, zUpper), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(0, 1, new Vector3(0, 20, zUpper), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(0, 2, new Vector3(20, 20, zUpper), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 0, new Vector3(-20, 0, zUpper), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 1, new Vector3(0, 0, zUpper), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 2, new Vector3(20, 0, zUpper), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 0, new Vector3(-20, -20, zUpper), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 1, new Vector3(0, -20, zUpper), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 2, new Vector3(20, -20, zUpper), new Vector3(0.01f, 0.01f, 0));

            // Z= -5mm 위치에 포커스를 하여 레이저를 출사하고 그 오차를 입력
            correction.AddRelative(0, 0, new Vector3(-20, 20, zLower), new Vector3(0.01f, -0.02f, 0));
            correction.AddRelative(0, 1, new Vector3(0, 20, zLower), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(0, 2, new Vector3(20, 20, zLower), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 0, new Vector3(-20, 0, zLower), new Vector3(-0.01f, 0.01f, 0));
            correction.AddRelative(1, 1, new Vector3(0, 0, zLower), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(1, 2, new Vector3(20, 0, zLower), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 0, new Vector3(-20, -20, zLower), new Vector3(0.01f, 0.01f, 0));
            correction.AddRelative(2, 1, new Vector3(0, -20, zLower), new Vector3(0.01f, -0.01f, 0));
            correction.AddRelative(2, 2, new Vector3(20, -20, zLower), new Vector3(0.01f, 0.01f, 0));
            #endregion

            var form = new Correction3DRtcForm(correction);
            form.ShowDialog();
        }
    }
}
