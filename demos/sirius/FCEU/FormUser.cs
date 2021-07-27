using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralLab.Sirius.FCEU
{
    public partial class FormUser : Form
    {
        int retry = 0;

        public FormUser()
        {
            InitializeComponent();

            this.cbbLevel.Items.AddRange( Enum.GetNames(typeof(UserLevel)) );
            this.Shown += LogIn_Shown;
        }

        /// <summary>
        /// Drop Shadow (그림자) 효과
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        private void LogIn_Shown(object sender, EventArgs e)
        {
            this.retry = 0;
            this.cbbLevel.SelectedIndex = (int)User.Level;
            this.txtName.Text = User.Name;
            this.txtName.SelectAll();

        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            if (User.LogIn(txtName.Text, (UserLevel)cbbLevel.SelectedIndex, txtPassword.Text))
                this.Close();
            else
            {
                this.retry++;
                if (this.retry >= 3)
                    this.Close();
                this.txtPassword.Text = string.Empty;
                this.txtPassword.Focus();
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            User.LogOut();
        }
    }
}
