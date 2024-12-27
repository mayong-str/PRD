using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;

namespace PCC_One
{
    public partial class PCC : UserControl
    {
        #region PCC 屬性

        #region status
        private string _sRStatus = "";
        [Category("RStatus"), Description("R-GUN Status")]
        public string RStatus
        {
            get { return _sRStatus; }
            set
            {
                _sRStatus = value;
                lab_RGunState.Text = _sRStatus;

                switch (_sRStatus)
                {
                    case "Main Discharge":
                        rs_RF.FillColor = Color.Chartreuse;
                        break;
                    case "Ignition Running":
                        rs_RF.FillColor = Color.Pink;
                        break;
                    case "Beam Guide Discharge":
                        rs_RF.FillColor = Color.Purple;
                        break;
                    case "Cooling Stopping":
                        rs_RF.FillColor = Color.Gray;
                        break;
                    case "Cooling Stop":
                        rs_RF.FillColor = SystemColors.ControlDark;
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region Cooling Water sensor
    
        //R-GUN
        private bool _bRGUNCW = false;
        [Category("RGUNCW"), Description("R-GUN Cooling Water sensor")]
        public bool bRGUNCW
        {
            get { return _bRGUNCW; }
            set
            {
                _bRGUNCW = value;

                if (!_bRGUNCW)
                {
                    os_RF.FillColor = Color.Red;
                    os_RF.FillStyle = FillStyle.Solid;
                }
                else
                {
                    os_RF.FillColor = Color.Chartreuse;
                    os_RF.FillStyle = FillStyle.Solid;
                }
            }
        }

        //G1 Cooling Water
        private bool _bG1W = false;
        [Category("G1W"), Description("G1 Cooling Water sensor")]
        public bool bG1W
        {
            get { return _bG1W; }
            set
            {
                _bG1W = value;

                if (!_bG1W)
                {
                    os_G1.FillColor = Color.Red;
                    os_G1.FillStyle = FillStyle.Solid;
                }
                else
                {
                    os_G1.FillColor = Color.Chartreuse;
                    os_G1.FillStyle = FillStyle.Solid;
                }
            }
        }

        //G2 Cooling Water
        private bool _bG2W = false;
        [Category("G2W"), Description("G2 Cooling Water sensor")]
        public bool bG2W
        {
            get { return _bG2W; }
            set
            {
                _bG2W = value;

                if (!_bG2W)
                {
                    os_G2.FillColor = Color.Red;
                    os_G2.FillStyle = FillStyle.Solid;
                }
                else
                {
                    os_G2.FillColor = Color.Chartreuse;
                    os_G2.FillStyle = FillStyle.Solid;
                }
            }
        }

        //Steering Coil Cooling Water
        private bool _bSTCW = false;
        [Category("STCW"), Description("Steering Coil Cooling Water sensor")]
        public bool bSTCW
        {
            get { return _bSTCW; }
            set
            {
                _bSTCW = value;

                if (!_bSTCW)
                {
                    os_STCW.FillColor = Color.Red;
                    os_STCW.FillStyle = FillStyle.Solid;
                }
                else
                {
                    os_STCW.FillColor = Color.Chartreuse;
                    os_STCW.FillStyle = FillStyle.Solid;
                }
            }
        }

        //Cooling Plate Cooling Water
        private bool _bCPCW = false;
        [Category("CPCW"), Description("Cooling Plate Cooling Water sensor")]
        public bool bCPCW
        {
            get { return _bCPCW; }
            set
            {
                _bCPCW = value;

                if (!_bCPCW)
                {
                    os_CPW.FillColor = Color.Red;
                    os_CPW.FillStyle = FillStyle.Solid;
                }
                else
                {
                    os_CPW.FillColor = Color.Chartreuse;
                    os_CPW.FillStyle = FillStyle.Solid;
                }
            }
        }

        //Main HearthCooling Cooling Water
        private bool _bHCW = false;
        [Category("HCW"), Description("Main Hearth Cooling Water sensor")]
        public bool bHCW
        {
            get { return _bHCW; }
            set
            {
                _bHCW = value;

                if (!_bHCW)
                {
                    os_HearthW.FillColor = Color.Red;
                    os_HearthW.FillStyle = FillStyle.Solid;
                }
                else
                {
                    os_HearthW.FillColor = Color.Chartreuse;
                    os_HearthW.FillStyle = FillStyle.Solid;
                }
            }
        }

        //Beam Guide Cooling Water
        private bool _bBGCW = false;
        [Category("BGCW"), Description("Beam Guide Cooling Water sensor")]
        public bool bBGCW
        {
            get { return _bBGCW; }
            set
            {
                _bBGCW = value;

                if (!_bBGCW)
                {
                    os_BGCW.FillColor = Color.Red;
                    os_BGCW.FillStyle = FillStyle.Solid;
                }
                else
                {
                    os_BGCW.FillColor = Color.Chartreuse;
                    os_BGCW.FillStyle = FillStyle.Solid;
                }
            }
        }

        //Chamber Inwall Cooling Water
        private bool _bCICW = false;
        [Category("CICW"), Description("Chamber Inwall Cooling Water sensor")]
        public bool bCICW
        {
            get { return _bCICW; }
            set
            {
                _bCICW = value;

                if (!_bCICW)
                {
                    os_CIWallW.FillColor = Color.Red;
                    os_CIWallW.FillStyle = FillStyle.Solid;
                }
                else
                {
                    os_CIWallW.FillColor = Color.Chartreuse;
                    os_CIWallW.FillStyle = FillStyle.Solid;
                }
            }
        }

        //Door Inwall Cooling Water
        private bool _bDICW = false;
        [Category("CICW"), Description("Door Inwall Cooling Water sensor")]
        public bool bDICW
        {
            get { return _bDICW; }
            set
            {
                _bDICW = value;

                if (!_bDICW)
                {
                    os_DInwallW.FillColor = Color.Red;
                    os_DInwallW.FillStyle = FillStyle.Solid;
                }
                else
                {
                    os_DInwallW.FillColor = Color.Chartreuse;
                    os_DInwallW.FillStyle = FillStyle.Solid;
                }
            }
        }

        #endregion

        #region is PCC ONE in vacuum
        private bool _bIsVac = false;
        [Category("IsVacuum"), Description("Is PCC one in vacuum")]
        public bool IsVac
        {
            get { return _bIsVac; }
            set
            {
                _bIsVac = value;

                if (_bIsVac) //in vacuum change Back color
                {
                    rs_BackBG1.FillColor = Color.Wheat;
                    rs_BackBG1r.FillColor = Color.Wheat;
                    //lab_WSTitle7.BackColor = Color.Wheat;
                    //rs_BackBG1l.FillColor = Color.Wheat;

                    rectangleShape5.FillColor = Color.Wheat;
                    rs_BackBG2.FillColor = Color.Wheat;
                    //lab_ATMG.BackColor = SystemColors.Control;
                    //os_TRATMG.FillColor = SystemColors.Control;
                    //TRPHGauge.Signal = 1;
                }
                else
                {
                    rs_BackBG1.FillColor = Color.White;
                    rs_BackBG1r.FillColor = Color.White;
                    //rs_BackBG1l.FillColor = Color.White;
                    rectangleShape5.FillColor = Color.White;
                    rs_BackBG2.FillColor = Color.White;
                    //lab_WSTitle7.BackColor = Color.White;
                    //lab_ATMG.BackColor = Color.Chartreuse;
                    //os_TRATMG.FillColor = Color.Chartreuse;
                    //TRPHGauge.Signal = 2;
                }
            }
        }

        #endregion

        #region  water flow value

        //G1 Cooling Water flow value
        private string _sG1WV = "";
        [Category("G1WV"), Description("G1 Cooling Water flow value")]
        public string sG1WV
        {
            get { return _sG1WV; }
            set
            {
                _sG1WV = value;

                if (_sG1WV != null && _sG1WV != "")
                {
                    txt_G1.Text = _sG1WV;
                }
                else
                {
                    txt_G1.Text = "Err.";
                }
            }
        }

        //G2 Cooling Water flow value
        private string _sG2WV = "";
        [Category("G2WV"), Description("G2 Cooling Water flow value")]
        public string sG2WV
        {
            get { return _sG2WV; }
            set
            {
                _sG2WV = value;

                if (_sG2WV != null && _sG2WV != "")
                {
                    txt_G2.Text = _sG2WV;
                }
                else
                {
                    txt_G2.Text = "Err.";
                }
            }
        }

        //Steering Coil Cooling Water flow value
        private string _sSCWV = "";
        [Category("SCWV"), Description("Steering Coil Water flow value")]
        public string sSCWV
        {
            get { return _sSCWV; }
            set
            {
                _sSCWV = value;

                if (_sSCWV != null && _sSCWV != "")
                {
                    txt_SC.Text = _sSCWV;
                }
                else
                {
                    txt_SC.Text = "Err.";
                }
            }
        }

        //Cooling Plate Water flow value
        private string _sCPWV = "";
        [Category("CPWV"), Description("Cooling Plate Water flow value")]
        public string sCPWV
        {
            get { return _sCPWV; }
            set
            {
                _sCPWV = value;

                if (_sCPWV != null && _sCPWV != "")
                {
                    txt_CP.Text = _sCPWV;
                }
                else
                {
                    txt_CP.Text = "Err.";
                }
            }
        }

        //Hearth Water flow value
        private string _sMHWV = "";
        [Category("CPWV"), Description("Hearth Water flow value")]
        public string sMHWV
        {
            get { return _sMHWV; }
            set
            {
                _sMHWV = value;

                if (_sMHWV != null && _sMHWV != "")
                {
                    txt_MH.Text = _sMHWV;
                }
                else
                {
                    txt_MH.Text = "Err.";
                }
            }
        }

        //BGC Water flow value
        private string _sBGCWV = "";
        [Category("BGCWV"), Description("BGC Water flow value")]
        public string sBGCWV
        {
            get { return _sBGCWV; }
            set
            {
                _sBGCWV = value;

                if (_sBGCWV != null && _sBGCWV != "")
                {
                    txt_BGC.Text = _sBGCWV;
                }
                else
                {
                    txt_BGC.Text = "Err.";
                }
            }
        }

        //chamber BH Water flow value
        private string _sCBHWV = "";
        [Category("CBHWV"), Description("Chamber BH Water flow value")]
        public string sCBHWV
        {
            get { return _sCBHWV; }
            set
            {
                _sCBHWV = value;

                if (_sCBHWV != null && _sCBHWV != "")
                {
                    txt_CBH.Text = _sCBHWV;
                }
                else
                {
                    txt_CBH.Text = "Err.";
                }
            }
        }

        #endregion

        #region TS1 states
        private int _iTS1Open = 0;
        [Category("TS1Open"), Description("Is TS1 open")]
        public int TS1Open
        {
            get { return _iTS1Open; }
            set
            {
                _iTS1Open = value;

                if (_iTS1Open == 1)  //TS1 OPEN states
                {
                    rsTS1.Location = new Point(116, 230);
                }
                else
                {
                    rsTS1.Location = new Point(139, 230);
                }
            }
        }

        #endregion

        #region PB1 states
        private int _iPB1States = 0;
        [Category("PB1States"), Description("PB1 states")]
        public int PB1States
        {
            get { return _iPB1States; }
            set
            {
                _iPB1States = value;

                if (_iPB1States == 0)  //TS1 OPEN states
                {
                    rsPB1.Location = new Point(245, 200);
                    rsPB1.Height = 28;
                }
                else
                {
                    rsPB1.Location = new Point(245, 170);
                    rsPB1.Height = 58;
                }
            }
        }

        #endregion

        #endregion


        public PCC()
        {
            InitializeComponent();
        }

        private void rectangleShape8_Click(object sender, EventArgs e)
        {

        }
    }

}
