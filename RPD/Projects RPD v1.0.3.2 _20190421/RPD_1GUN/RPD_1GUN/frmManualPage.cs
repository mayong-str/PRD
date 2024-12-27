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
    public partial class frmManualPage : Form
    {
        bool _RegisterEvent = false;
        public frmManualPage()
        {
            InitializeComponent();
        }
        private void frmManualPage_Load(object sender, EventArgs e)
        {
            DisplayTimer.Enabled = true;
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

        private void DisplayTimer_Tick(object sender, EventArgs e)
        {
            clsTool.ButtonStatus(btnAllPumpDown, RPD.STRCTURE.SYS.Btn.ALLPumpDownSwitch,'A', _RegisterEvent,3,RPD.STRCTURE.SYS.Data.Level);
            clsTool.ButtonStatus(btnALLVent, RPD.STRCTURE.SYS.Btn.ALLVentSwitch, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.ButtonStatus(btnALLTPStart, RPD.STRCTURE.SYS.Btn.ALLTPStartSwitch, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);

            //add by william
            clsTool.ButtonStatus(btnALLLineInit, RPD.STRCTURE.SYS.Btn.ALLLineInit, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);

            clsTool.ButtonStatus(btnLLPumpDown, RPD.STRCTURE.LL.Btn.LLPumpDownSwitch, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.ButtonStatus(btnLLVent, RPD.STRCTURE.LL.Btn.LLVentSwitch, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.ButtonStatus(btnLLTPStart, RPD.STRCTURE.LL.Btn.LLTPStartSwitch, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);

            clsTool.ButtonStatus(btnTRPumpDown, RPD.STRCTURE.TR.Btn.TRPumpDownSwitch, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.ButtonStatus(btnTRVent, RPD.STRCTURE.TR.Btn.TRVentSwitch, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.ButtonStatus(btnTRTPStart, RPD.STRCTURE.TR.Btn.TRTPStartSwitch, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            //add by sophia 20160822
            clsTool.ButtonStatus(btnTRMFCTest, RPD.STRCTURE.TR.Btn.TRMFCTestSwitch, 'B', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);

            clsTool.ButtonStatus(btnULPumpDown, RPD.STRCTURE.UL.Btn.ULPumpDownSwitch, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.ButtonStatus(btnULVent, RPD.STRCTURE.UL.Btn.ULVentSwitch, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.ButtonStatus(btnULTPStart, RPD.STRCTURE.UL.Btn.ULTPStartSwitch, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);

            clsTool.ButtonStatus(btnTSSPumpDown, RPD.STRCTURE.PCC2.Btn.TSSPumpDownSwitch, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.ButtonStatus(btnTSSVent, RPD.STRCTURE.PCC2.Btn.TSSVentSwitch, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.ButtonStatus(btnTSSTPStart, RPD.STRCTURE.PCC2.Btn.TSSTPStartSwitch, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);

            clsTool.ButtonStatus(btnPlasmaStart, RPD.STRCTURE.PCC1.Btn.PlasmaStart, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.ButtonStatus(btnPlasmaStop,RPD.STRCTURE.PCC1.Btn.PlasmaStop, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.ButtonStatus(btnMainDischargechange,RPD.STRCTURE.PCC1.Btn.MainDischargeChange, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.ButtonStatus(btnBeamGuideDischargeChange,RPD.STRCTURE.PCC1.Btn.BeamGuideDischargeChange, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.ButtonStatus(btnResistanceMeasurementOperation, RPD.STRCTURE.PCC1.Btn.ResistanceMeasurementOperation, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);

            _RegisterEvent = true;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       


    }
}
