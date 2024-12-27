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
    public partial class frmRecipeNew : Form
    {
        string _SQL = @"";
     
        public frmRecipeNew()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
          

            string sSqlCommand = "";
            string sSqlTamp = "";
            sSqlTamp += "'" + txtRecipe.Text + "',";

            sSqlTamp += "'15' , ";
            sSqlTamp += "'15' , ";
            sSqlTamp += "'20' , ";
            sSqlTamp += "'30' , ";
            sSqlTamp += "'30' , ";
            sSqlTamp += "'0' , ";
            sSqlTamp += "'1' , ";
            sSqlTamp += "'1' , ";
            sSqlTamp += "'1' , ";
            sSqlTamp += "'0' , ";
            sSqlTamp += "'0' , ";
            sSqlTamp += "'0' , ";
            sSqlTamp += "'0' , ";
            sSqlTamp += "'0' , ";
            sSqlTamp += "'' , ";

            sSqlTamp = sSqlTamp.Substring(0, sSqlTamp.Length - 2);
            sSqlTamp += ",'" + txtDescription.Text + "'";
            sSqlTamp += ",'" + txtCreateBy.Text + "'";
            sSqlTamp += ",'" + DateTime.Now.ToString("yyy-MM-dd HH:mm:ss") + "'";


            sSqlCommand = @"INSERT INTO RECIPE([Name]
                                              ,[HCC]
                                              ,[SCC]
                                              ,[G2CC]
                                              ,[BGDC]
                                              ,[MHDC]
                                              ,[TBPS]
                                              ,[CHCI]
                                              ,[CSCI]
                                              ,[CG2CI]
                                              ,[TBPSBGD]
                                              ,[RGV]
                                              ,[CO2V]
                                              ,[PCArV]
                                              ,[PCT]
                                              ,[PBT]
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

        private void frmRecipeNew_Load(object sender, EventArgs e)
        {
            //clsTool.txtStatus(txtRecipe, "", "A", false);
        }




    }
}
