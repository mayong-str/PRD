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
    public partial class frmArchersTest : Form
    {
        public frmArchersTest()
        {
            InitializeComponent();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            DisplayTimer.Enabled = false;
            this.Close();
        }

        private void frmArchersTest_Load(object sender, EventArgs e)
        {
            txtSQL.Focus();
          

            //DisplayTimer.Enabled = true;

            //foreach (Control ctl in this.gbBtn.Controls)
            //{
            //    if(ctl is Button)
            //    {
            //        ctl.Click += Btn_Click;                        
            //    }             
            //}

            //foreach (Control ctl in this.gbParameter.Controls)
            //{
            //    if (ctl is TextBox)
            //    {
            //        clsTool.txtStatus((TextBox)ctl, ctl.Tag.ToString(), "Normal", false);
            //    }
            //}

          


        }

        private static void Btn_Click(object sender, EventArgs e)
        {
            Button SBtn = (Button)sender;
            //一般按鈕On 一個 Pulse
            clsPLC.WriPLC_Pulse(SBtn.Tag.ToString());
        }

        private void DisplayTimer_Tick(object sender, EventArgs e)
        {
             //int[] Test_Temp = new int[25];
             //clsPLC.ReadPLCBatch_Word("D39968", ref Test_Temp);
            
             //foreach (Control ctl in this.gbDisplay.Controls)
             //{
             //    if (ctl is Label)
             //    {
             //        if (ctl.Name.IndexOf("lblDis") > -1)
             //        {
             //            int Index = Convert.ToInt16(ctl.Name.Replace("lblDis","")) + 1;
             //            ctl.Text = Test_Temp[Index].ToString();
             //        }
             //    }
             //}

            int[] Test_Temp = new int[7001];
            clsPLC.ReadPLCBatch_Word("D2999", ref Test_Temp);

            lbl1.Text = Test_Temp[0].ToString();
            lbl2.Text = Test_Temp[1000].ToString();
            lbl3.Text = Test_Temp[2000].ToString();
            lbl4.Text = Test_Temp[3000].ToString();
            lbl5.Text = Test_Temp[4000].ToString();
            lbl6.Text = Test_Temp[5000].ToString();
            lbl7.Text = Test_Temp[6000].ToString();
            lbl8.Text = Test_Temp[7000].ToString();

        }

        private void btnExecute_Click(object sender, EventArgs e)
        {

            try
            {
                if(clsDBN.ExecuteSqlCommandTest(txtSQL.Text) > -1)
                {
                    MessageBox.Show("Command excute successful", "Congratulation");
                }
            }
            catch(Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "");
            }
        
        }




    }
}
