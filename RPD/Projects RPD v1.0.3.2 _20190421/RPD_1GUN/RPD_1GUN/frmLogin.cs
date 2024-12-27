using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPD_1GUN
{
    public partial class frmLogin : Form
    {
        string _SQL = @"";
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            FormInit();
        }

        public string ID
        {
            get;
            set;
        }
        public string PW
        {
            get;
            set;
        }
        public string Level
        {
            get;
            set;
        }
        private void FormInit()
        {
            txtUser.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            if (txtUser.Text == "") { MessageBox.Show("Please input User ID !!"); txtUser.Focus(); return; }
            if (txtPwd.Text == "") { MessageBox.Show("Please input New Password !!"); txtPwd.Focus(); return; }

            this.ID = txtUser.Text;
            this.PW = txtPwd.Text;

            _SQL = @"SELECT * FROM USER_MST WHERE USER_ID = '" + this.ID + "' AND USER_PWD ='" + this.PW + "'";
            DataRow dr_User = clsDBN.GetRow(_SQL);
            if (dr_User != null)
            {
                this.Level = dr_User["USER_LVL"].ToString();           
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            else
            {
                MessageBox.Show("Login Failed","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

        }

        //按下ENTER
        private void Enter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(this, null);
            }
        }

    }
}
