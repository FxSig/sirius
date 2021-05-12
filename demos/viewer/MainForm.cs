using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralLab.Sirius
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            // 라이브러리 초기화
            SpiralLab.Core.Initialize();

            // 문서 생성후 뷰어에 지정
            var doc = new DocumentDefault();
            siriusViewerForm1.Document = doc;
        }

        public bool Open(string fileName)
        {
            // 문서 파일 오픈후 뷰어에 지정
            var doc = DocumentSerializer.OpenSirius(fileName);
            siriusViewerForm1.Document = doc;
            return true;
        }
    }
}
