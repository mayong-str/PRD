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
    public partial class frmTrayMaintain : Form
    {
        bool _RegisterEvent = false;
        public frmTrayMaintain()
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
        private void frmTrayMaintain_Load(object sender, EventArgs e)
        {

            clsTool.txtStatus(TD_TrayID, "D35100", "Normal", false);

            DisplayTimer.Enabled = true;
        }

        private void cmbModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cmbModule.Text)
            {
                case "EF":
                    clsPLC.WriPLC_Word("D35099", "1");
                    TD_TrayID.Text = RPD.STRCTURE.EF.Data.TD_TrayID.ToString();
                    
                    break;
                case "LL":
                    clsPLC.WriPLC_Word("D35099", "2");
                    TD_TrayID.Text = RPD.STRCTURE.LL.Data.TD_TrayID.ToString();      
                    break;
                case "TR1":
                    clsPLC.WriPLC_Word("D35099", "4");
                    TD_TrayID.Text = RPD.STRCTURE.TR.Data.TD_TR1_TrayID.ToString();      
                    break;
                case "TR2":
                    clsPLC.WriPLC_Word("D35099", "5");
                    TD_TrayID.Text = RPD.STRCTURE.TR.Data.TD_TR2_TrayID.ToString();  
                    break;
                case "TR3":
                    clsPLC.WriPLC_Word("D35099", "6");
                    TD_TrayID.Text = RPD.STRCTURE.TR.Data.TD_TR3_TrayID.ToString();  
                    break;
                case "UL":
                    clsPLC.WriPLC_Word("D35099", "8");
                    TD_TrayID.Text = RPD.STRCTURE.UL.Data.TD_TrayID.ToString();      
                    break;
                case "DF":
                    clsPLC.WriPLC_Word("D35099", "9");
                    TD_TrayID.Text = RPD.STRCTURE.DF.Data.TD_TrayID.ToString();      
                    break;
            }

        }

        private void DisplayTimer_Tick(object sender, EventArgs e)
        {
            clsTool.ButtonStatus(btnUpdate, RPD.STRCTURE.SYS.Btn.TrayDataModifySwitch, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.ButtonStatus(btnDelete, RPD.STRCTURE.SYS.Btn.TrayDeleteSwitch, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);

            _RegisterEvent = true;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
