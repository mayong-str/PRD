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
    public partial class frmRegister : Form
    {
        public frmRegister()
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
            this.Hide();
        }

        private void frmRegister_Load(object sender, EventArgs e)
        {          
            DisplayTimer.Enabled = true;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtRegisterNum.Text.Length.Equals(20))
            {  
                clsPLC.WriPLC_Word("D35600", ByteToInt16(txtRegisterNum.Text.Substring(0, 2)));
                clsPLC.WriPLC_Word("D35601", ByteToInt16(txtRegisterNum.Text.Substring(2, 2)));
                clsPLC.WriPLC_Word("D35602", ByteToInt16(txtRegisterNum.Text.Substring(4, 2)));
                clsPLC.WriPLC_Word("D35603", ByteToInt16(txtRegisterNum.Text.Substring(6, 2)));
                clsPLC.WriPLC_Word("D35604", ByteToInt16(txtRegisterNum.Text.Substring(8, 2)));
                clsPLC.WriPLC_Word("D35605", ByteToInt16(txtRegisterNum.Text.Substring(10, 2)));
                clsPLC.WriPLC_Word("D35606", ByteToInt16(txtRegisterNum.Text.Substring(12, 2)));
                clsPLC.WriPLC_Word("D35607", ByteToInt16(txtRegisterNum.Text.Substring(14, 2)));
                clsPLC.WriPLC_Word("D35608", ByteToInt16(txtRegisterNum.Text.Substring(16, 2)));
                clsPLC.WriPLC_Word("D35609", ByteToInt16(txtRegisterNum.Text.Substring(18, 2)));
          
              
                clsPLC.WriPLC_Pulse(RPD.STRCTURE.SYS.Btn.StartCodeInputKeySwitch.Address);
            }
            else
            {
                MessageBox.Show("Invailed Code Please Check !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        private void DisplayTimer_Tick(object sender, EventArgs e)
        {
            clsTool.ButtonStatus(btnRegister, RPD.STRCTURE.SYS.Btn.StartCodeInputKeySwitch, 'A', true, 3, RPD.STRCTURE.SYS.Data.Level); //Register Submit 只刷狀態

            if (RPD.STRCTURE.SYS.Map.StartCodeError.Equals(1)) //打錯
                lblRegStatus.Text = "Start Code is Invailed ";
            else
                lblRegStatus.Text = "";
        }

        public string ByteToInt16(string Code)
        {
            Byte[] a = Encoding.ASCII.GetBytes(Code);
            return ((short)(a[1] << 8 | a[0])).ToString();
        }




    }
}
