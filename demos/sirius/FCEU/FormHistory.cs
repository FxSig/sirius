using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Numerics;

namespace SpiralLab.Sirius.FCEU
{
    public partial class FormHistory: Form
    {
        private System.Collections.Specialized.StringCollection folderCol;

        public FormHistory()
        {
            InitializeComponent();

            this.VisibleChanged += FormHistory_VisibleChanged;
            folderCol = new System.Collections.Specialized.StringCollection();
            CreateHeadersAndFillListView();

        }
        private void CreateHeadersAndFillListView()
        {
            ColumnHeader colHead;

            colHead = new ColumnHeader();
            colHead.Text = "Date";
            this.listView1.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Level";
            this.listView1.Columns.Add(colHead);

            colHead = new ColumnHeader();
            colHead.Text = "Message";
            this.listView1.Columns.Add(colHead);

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        public void Log(Logger.Type type, string message)
        {
            listView1.BeginUpdate();
            while (listView1.Items.Count > 2000)
            {
                listView1.Items[0].Remove();
            }
            ListViewItem lvi;
            ListViewItem.ListViewSubItem lvsi;
            lvi = new ListViewItem();
            lvi.Text = DateTime.Now.ToString("MM-dd HH:mm:ss");

            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = type.ToString();
            switch(type)
            {
                case Logger.Type.Error:
                case Logger.Type.Fatal:
                    lvsi.BackColor = Color.Red;
                    lvi.UseItemStyleForSubItems = false;
                    break;
                case Logger.Type.Warn:
                    lvsi.BackColor = Color.Yellow;
                    lvi.UseItemStyleForSubItems = false;
                    break;
                default:
                    break;
            }
            lvi.SubItems.Add(lvsi);

            lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = message;
            lvi.SubItems.Add(lvsi);

            this.listView1.Items.Add(lvi);
            listView1.TopItem = listView1.Items[listView1.Items.Count - 1];

            listView1.EndUpdate();
        }

        private void FormHistory_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            else
            {

            }
        }
       
    }
}