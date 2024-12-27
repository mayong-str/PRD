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
    public partial class frmPBTNew : Form
    {
        public frmPBTNew()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string sSqlCommand = "";
            string sSqlTamp = "";
            sSqlTamp += "'" + txtPBTName.Text + "'";
            sSqlTamp += ",'" + txtDescription.Text + "'";
            sSqlTamp += ",'" + txtCreateBy.Text + "'";
            sSqlTamp += ",'" + DateTime.Now.ToString("yyy-MM-dd HH:mm:ss") + "'";


            sSqlCommand = @"INSERT INTO PBT   ([Name]
                                              ,[Des]
                                              ,[CreateUser]
                                              ,[CreateTime]) VALUES (" + sSqlTamp + ")";

            if (clsDBN.ExecuteSqlCommand(sSqlCommand) <= 0)
            {
                MessageBox.Show("Write to DB Failed");
            }
            else
            {
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
