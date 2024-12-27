using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThreadingTimer = System.Threading.Timer;
using TimersTimer = System.Timers.Timer;
using System.Diagnostics;
using Microsoft.VisualBasic.PowerPacks;
using System.Reflection;

namespace TR
{
    public partial class TR : UserControl
    {
        #region TR 屬性

        #region TrateVisible in TR1 TR2 TR3
        private bool _bTrateVisibleTR1 = false;
        [Category("SubstrateTR1 Visible"), Description("TR1 Substrate Display")]
        public bool TrateVisibleTR1
        {
            get { return _bTrateVisibleTR1; }
            set
            {
                _bTrateVisibleTR1 = value;

                if (!_bTrateVisibleTR1)
                {
                    //plSubstrateTR1.Visible = true;
                    plSubstrateTR1.BackColor = Color.WhiteSmoke;
                    plSubstrateTR1.BorderStyle = BorderStyle.None;
                    lab_STIDTR1.Visible = false;
                }
                else
                {
                    plSubstrateTR1.BackColor = Color.MediumBlue;
                    plSubstrateTR1.BorderStyle = BorderStyle.FixedSingle;
                    lab_STIDTR1.Visible = true;

                    if (TIDTR1.Length == 0)
                    {
                        TIDTR1 = "tray";
                        lab_STIDTR1.Text = TIDTR1;
                        //return;
                    }

                    TIDTR1 = TIDTR1.Trim();
                    lab_STIDTR1.Text = TIDTR1;
                }
            }
        }

        private bool _bTrateVisibleTR2 = false;
        [Category("SubstrateTR2 Visible"), Description("TR2 Substrate Display")]
        public bool TrateVisibleTR2
        {
            get { return _bTrateVisibleTR2; }
            set
            {
                _bTrateVisibleTR2 = value;

                if (!_bTrateVisibleTR2)
                {
                    //plSubstrateTR2.Visible = true;
                    plSubstrateTR2.BackColor = Color.WhiteSmoke;
                    plSubstrateTR2.BorderStyle = BorderStyle.None;
                    lab_STIDTR2.Visible = false;
                }
                else
                {
                    plSubstrateTR2.BackColor = Color.MediumBlue;
                    plSubstrateTR2.BorderStyle = BorderStyle.FixedSingle;
                    lab_STIDTR2.Visible = true;

                    if (TIDTR2.Length == 0)
                    {
                        TIDTR2 = "tray";
                        lab_STIDTR2.Text = TIDTR2;
                        //return;
                    }

                    TIDTR2 = TIDTR2.Trim();
                    lab_STIDTR2.Text = TIDTR2;
                }
            }
        }

        private bool _bTrateVisibleTR3 = false;
        [Category("SubstrateTR3 Visible"), Description("TR3 Substrate Display")]
        public bool TrateVisibleTR3
        {
            get { return _bTrateVisibleTR3; }
            set
            {
                _bTrateVisibleTR3 = value;

                if (!_bTrateVisibleTR3)
                {
                    //plSubstrateTR3.Visible = true;
                    plSubstrateTR3.BackColor = Color.WhiteSmoke;
                    plSubstrateTR3.BorderStyle = BorderStyle.None;
                    lab_STIDTR3.Visible = false;
                }
                else
                {
                    plSubstrateTR3.BackColor = Color.MediumBlue;
                    plSubstrateTR3.BorderStyle = BorderStyle.FixedSingle;
                    lab_STIDTR3.Visible = true;

                    if (TIDTR3.Length == 0)
                    {
                        TIDTR3 = "tray";
                        lab_STIDTR3.Text = TIDTR3;
                        //return;
                    }

                    TIDTR3 = TIDTR3.Trim();
                    lab_STIDTR3.Text = TIDTR3;
                }
            }
        }

        #endregion

        #region Trate ID in TR1 TR2 TR3
        private string _sTIDTR1 = "";
        [Category("TIDTR1"), Description("TR1 trate ID")]
        public string TIDTR1
        {
            get { return _sTIDTR1; }
            set { _sTIDTR1 = value; }
        }

        private string _sTIDTR2 = "";
        [Category("TIDTR2"), Description("TR2 trate ID")]
        public string TIDTR2
        {
            get { return _sTIDTR2; }
            set { _sTIDTR2 = value; }
        }

        private string _sTIDTR3 = "";
        [Category("TIDTR3"), Description("TR3 trate ID")]
        public string TIDTR3
        {
            get { return _sTIDTR3; }
            set { _sTIDTR3 = value; }
        }

        #endregion

        #region TR1 TR2 TR3 Front position sensor
        private bool _bFPSTR1 = false;
        [Category("FPSTR1"), Description("TR1 Front position sensor")]
        public bool FPSTR1
        {
            get { return _bFPSTR1; }
            set
            {
                _bFPSTR1 = value;

                if (_bFPSTR1)
                {
                    osFPSTR1.FillColor = Color.Chartreuse;
                }
                else
                {
                    osFPSTR1.FillColor = SystemColors.Control;// Color.Red;
                }
            }
        }

        private bool _bFPSTR2 = false;
        [Category("FPSTR2"), Description("TR2 Front position sensor")]
        public bool FPSTR2
        {
            get { return _bFPSTR2; }
            set
            {
                _bFPSTR2 = value;

                if (_bFPSTR2)
                {
                    osFPSTR2.FillColor = Color.Chartreuse;
                }
                else
                {
                    osFPSTR2.FillColor = SystemColors.Control;// Color.Red;
                }
            }
        }

        private bool _bFPSTR3 = false;
        [Category("FPSTR3"), Description("TR3 Front position sensor")]
        public bool FPSTR3
        {
            get { return _bFPSTR3; }
            set
            {
                _bFPSTR3 = value;

                if (_bFPSTR3)
                {
                    osFPSTR3.FillColor = Color.Chartreuse;
                }
                else
                {
                    osFPSTR3.FillColor = SystemColors.Control; //Color.Red;
                }
            }
        }

        #endregion

        #region TR1 TR2 TR3 Rear position sensor
        private bool _bRPSTR1 = false;
        [Category("RPSTR1"), Description("TR1 Rear position sensor")]
        public bool RPSTR1
        {
            get { return _bRPSTR1; }
            set
            {
                _bRPSTR1 = value;

                if (_bRPSTR1)
                {
                    osRPSTR1.FillColor = Color.Chartreuse;
                }
                else
                {
                    osRPSTR1.FillColor = SystemColors.Control; //Color.Red;
                }
            }
        }

        private bool _bRPSTR2 = false;
        [Category("RPSTR2"), Description("TR2 Rear position sensor")]
        public bool RPSTR2
        {
            get { return _bRPSTR2; }
            set
            {
                _bRPSTR2 = value;

                if (_bRPSTR2)
                {
                    osRPSTR2.FillColor = Color.Chartreuse;
                }
                else
                {
                    osRPSTR2.FillColor = SystemColors.Control; //Color.Red;
                }
            }
        }

        private bool _bRPSTR3 = false;
        [Category("RPSTR3"), Description("TR3 Rear position sensor")]
        public bool RPSTR3
        {
            get { return _bRPSTR3; }
            set
            {
                _bRPSTR3 = value;

                if (_bRPSTR3)
                {
                    osRPSTR3.FillColor = Color.Chartreuse;
                }
                else
                {
                    osRPSTR3.FillColor = SystemColors.Control; //Color.Red;
                }
            }
        }

        #endregion

        #region TR1 TR3 Center position sensor
        private bool _bCPSTR1 = false;
        [Category("CPSTR1"), Description("TR1 Break position sensor")]
        public bool CPSTR1
        {
            get { return _bCPSTR1; }
            set
            {
                _bCPSTR1 = value;

                if (_bCPSTR1)
                {
                    osCPSTR1.FillColor = Color.Chartreuse;
                }
                else
                {
                    osCPSTR1.FillColor = SystemColors.Control; //Color.Red;
                }
            }
        }

        private bool _bCPSTR2 = false;
        [Category("CPSTR2"), Description("TR2 Front position sensor")]
        public bool CPSTR2
        {
            get { return _bCPSTR2; }
            set
            {
                _bCPSTR2 = value;

                if (_bCPSTR2)
                {
                    osCPSTR2.FillColor = Color.Chartreuse;
                }
                else
                {
                    osCPSTR2.FillColor = SystemColors.Control; //Color.Red;
                }
            }
        }

        private bool _bCPSTR3 = false;
        [Category("CPSTR3"), Description("TR3 Break position sensor")]
        public bool CPSTR3
        {
            get { return _bCPSTR3; }
            set
            {
                _bCPSTR3 = value;

                if (_bCPSTR3)
                {
                    osCPSTR3.FillColor = Color.Chartreuse;
                }
                else
                {
                    osCPSTR3.FillColor = SystemColors.Control; //Color.Red;
                }
            }
        }

        #endregion

        #region TR Cooling Plate
        private bool _bCoolPlate = false;
        [Category("CoolPlate"), Description("TR Cooling Plate states")]
        public bool CoolPlate
        {
            get { return _bIsATM; }
            set
            {
                _bCoolPlate = value;

                if (!_bCoolPlate) // Cooling Plate actives
                {
                    re_CoolingPlate1.FillColor = SystemColors.Control;
                }
                else
                {
                    re_CoolingPlate1.FillColor = Color.Chartreuse;
                }
            }
        }

        #endregion

        #region TR1 TR2 TR3 Cooling sensor
        private bool _bCoolTR1 = false;
        [Category("CoolTR1"), Description("TR1 Cooling sensor")]
        public bool CoolTR1
        {
            get { return _bCoolTR1; }
            set
            {
                _bCoolTR1 = value;

                if (_bCoolTR1)
                {
                    osCoolTR1.FillColor = Color.Chartreuse;
                }
                else
                {
                    osCoolTR1.FillColor = Color.Red;
                }
            }
        }

        private bool _bCoolTR2 = false;
        [Category("CoolTR2"), Description("TR2 Cooling sensor")]
        public bool CoolTR2
        {
            get { return _bCoolTR2; }
            set
            {
                _bCoolTR2 = value;

                if (_bCoolTR2)
                {
                    osCoolTR2.FillColor = Color.Chartreuse;
                }
                else
                {
                    osCoolTR2.FillColor = Color.Red;
                }
            }
        }

        private string _bTR2CWV = "";
        [Category("TR2CWV"), Description("TR2 Cooling Water flow value")]
        public string TR2CWV
        {
            get { return _bTR2CWV; }
            set
            {
                _bTR2CWV = value;

                if (_bTR2CWV != null && _bTR2CWV != "")
                {
                    txt_TR2CW.Text = _bTR2CWV;
                }
                else
                {
                    txt_TR2CW.Text = "Err.";
                }
            }
        }

        private bool _bCPTR2 = false;
        [Category("CPTR2"), Description("TR2 Cooling Plate")]
        public bool CPTR2
        {
            get { return _bCPTR2; }
            set
            {
                _bCPTR2 = value;

                if (_bCPTR2)
                {
                    os_TR2CP.FillColor = Color.Chartreuse;
                }
                else
                {
                    os_TR2CP.FillColor = Color.Red;
                }
            }
        }

        private string _bTR2CPWV = "";
        [Category("TR2CPWV"), Description("TR2 Cooling Plate Water flow value")]
        public string TR2CPWV
        {
            get { return _bTR2CPWV; }
            set
            {
                _bTR2CPWV = value;

                if (_bTR2CPWV != null && _bTR2CPWV != "")
                {
                    txt_TR2CPW.Text = _bTR2CPWV;
                }
                else
                {
                    txt_TR2CPW.Text = "Err.";
                }
            }
        }

        private bool _bCoolTR3 = false;
        [Category("CoolTR3"), Description("TR3 Cooling sensor")]
        public bool CoolTR3
        {
            get { return _bCoolTR3; }
            set
            {
                _bCoolTR3 = value;

                if (_bCoolTR3)
                {
                    osCoolTR3.FillColor = Color.Chartreuse;
                }
                else
                {
                    osCoolTR3.FillColor = Color.Red;
                }
            }
        }

        private string _bTR3CWV = "";
        [Category("TR3CWV"), Description("TR3 Cooling Water flow value")]
        public string TR3CWV
        {
            get { return _bTR3CWV; }
            set
            {
                _bTR3CWV = value;

                if (_bTR3CWV != null && _bTR3CWV != "")
                {
                    txt_TR3CW.Text = _bTR3CWV;
                }
                else
                {
                    txt_TR3CW.Text = "Err.";
                }
            }
        }


        #endregion

        #region DV2 DV3 open states
        private bool _bDV2Open = false;
        [Category("DV2Open"), Description("DV2 open states")]
        public bool DV2Open
        {
            get { return _bDV2Open; }
            set
            {
                _bDV2Open = value;

                if (_bDV2Open)
                {
                    nsDoor1.Height = 10;
                }
                else
                {
                    nsDoor1.Height = 130;
                }
            }
        }

        private bool _bDV3Open = false;
        [Category("DV3Open"), Description("DV3 open states")]
        public bool DV3Open
        {
            get { return _bDV3Open; }
            set
            {
                _bDV3Open = value;

                if (_bDV3Open)
                {
                    nsDoor2.Height = 10;
                }
                else
                {
                    nsDoor2.Height = 130;
                }
            }
        }

        #endregion

        #region TR1 TR2 TR3 CV activate states
        private bool _bCVActTR1 = false;
        [Category("CVActTR1"), Description("TR1 CV activate states")]
        public bool CVActTR1
        {
            get { return _bCVActTR1; }
            set
            {
                _bCVActTR1 = value;

                if (_bCVActTR1)
                {
                    osCVTR1.FillColor = Color.Chartreuse;
                    osCVTR1.FillStyle = FillStyle.Solid;
                }
                else
                {
                    osCVTR1.FillColor = Color.Navy;
                    osCVTR1.FillStyle = FillStyle.Solid;
                }
            }
        }

        private bool _bCVActTR2 = false;
        [Category("CVActTR2"), Description("TR2 CV activate states")]
        public bool CVActTR2
        {
            get { return _bCVActTR2; }
            set
            {
                _bCVActTR2 = value;

                if (_bCVActTR2)
                {
                    osCVTR2.FillColor = Color.Chartreuse;
                    osCVTR2.FillStyle = FillStyle.Solid;
                }
                else
                {
                    osCVTR2.FillColor = Color.Navy;
                    osCVTR2.FillStyle = FillStyle.Solid;
                }
            }
        }

        private bool _bCVActTR3 = false;
        [Category("CVActTR3"), Description("TR3 CV activate states")]
        public bool CVActTR3
        {
            get { return _bCVActTR3; }
            set
            {
                _bCVActTR3 = value;

                if (_bCVActTR3)
                {
                    osCVTR3.FillColor = Color.Chartreuse;
                    osCVTR3.FillStyle = FillStyle.Solid;
                }
                else
                {
                    osCVTR3.FillColor = Color.Navy;
                    osCVTR3.FillStyle = FillStyle.Solid;
                }
            }
        }

        #endregion

        #region TR in Auto or Not
        private bool _bAuto = false;
        [Category("AutoMode"), Description("Is TR in Auto mode")]
        public bool AutoMode
        {
            get { return _bAuto; }
            set
            {
                _bAuto = value;

                if (_bAuto && lab_Online.Text != "OnLine")
                {
                    lab_Online.Text = "OnLine";
                    lab_Online.BackColor = Color.Chartreuse;
                }

                if (!_bAuto && lab_Online.Text != "OffLine")
                {
                    lab_Online.Text = "OffLine";
                    lab_Online.BackColor = SystemColors.Control;// Color.Red;
                }
            }
        }

        #endregion

        #region is TR in vacuum PH
        private bool _bIsVac = false;
        [Category("IsVacuum"), Description("Is TR in vacuum")]
        public bool IsVac
        {
            get { return _bIsVac; }
            set
            {
                _bIsVac = value;

                if (_bIsVac) //in vacuum change Back color
                {
                    pal_BackG.BackColor = Color.Wheat;
                    lab_STIDTR1.BackColor = Color.Wheat;
                    lab_STIDTR2.BackColor = Color.Wheat;
                    lab_STIDTR3.BackColor = Color.Wheat;
                    pal_BackG.SendToBack();
                    lab_ATMG.BackColor = SystemColors.Control;
                    os_TRATMG.FillColor = SystemColors.Control;
                    TRPHGauge.Signal = 1;
                    TRCGauge.Signal = 1;
                }
                else
                {
                    pal_BackG.BackColor = Color.Transparent;
                    lab_STIDTR1.BackColor = Color.Transparent;
                    lab_STIDTR2.BackColor = Color.Transparent;
                    lab_STIDTR3.BackColor = Color.Transparent;
                    pal_BackG.SendToBack();
                    lab_ATMG.BackColor = Color.Chartreuse;
                    os_TRATMG.FillColor = Color.Chartreuse;
                    TRPHGauge.Signal = 2;
                    TRCGauge.Signal = 2;
                }
            }
        }

        #endregion

        #region is TR in ATM
        private bool _bIsATM = false;
        [Category("IsATM"), Description("Is TR in ATM")]
        public bool IsATM
        {
            get { return _bIsATM; }
            set
            {
                _bIsATM = value;

                if (!_bIsATM) //in vacuum change Back color
                {
                    lab_ATMG.BackColor = SystemColors.Control;
                    os_TRATMG.FillColor = SystemColors.Control;
                    pal_BackG.BackColor = Color.Wheat;
                    TRPHGauge.Signal = 1;
                    TRCGauge.Signal = 1;
                }
                else
                {
                    lab_ATMG.BackColor = Color.Chartreuse;
                    os_TRATMG.FillColor = Color.Chartreuse;
                    pal_BackG.BackColor = Color.White;
                    TRPHGauge.Signal = 2;
                    TRCGauge.Signal = 2;
                }
            }
        }

        #endregion

        #region is TR pressure value
        private string _sPreVal = "";
        [Category("PreVal"), Description("TR pressure value")]
        public string PreVal
        {
            get { return _sPreVal; }
            set
            {
                _sPreVal = value;
                if (_sPreVal.Length == 0)
                {
                    _sPreVal = "Err";
                    lab_PHGValue.Text = _sPreVal;
                    TRPHGauge.Signal = 0;
                    TRCGauge.Signal = 0;
                    return;
                }

                //if (!ClsTool.IsNumeric(_sPreVal))
                //{
                //    _sPreVal = "Err";
                //    lab_PHGValue.Text = _sPreVal;
                //    TRPHGauge.Signal = 0;
                //    TRCGauge.Signal = 0;
                //    return;
                //}

                _sPreVal = _sPreVal.Trim();

                lab_PHGValue.Text = _sPreVal;
                TRPHGauge.Signal = 1;
                TRCGauge.Signal = 1;

                //if (Convert.ToDouble(_sPreVal) < 760)
                //{
                //    TRPHGauge.Signal = 1;
                //    TRCGauge.Signal = 1;
                //}
                //else
                //{
                //    TRPHGauge.Signal = 2;
                //    TRCGauge.Signal = 2;
                //}

            }
        }

        #endregion

        #region is TR pressure value
        private string _sPreVal_C = "";
        [Category("PreVal_C"), Description("TR pressure value C")]
        public string PreVal_C
        {
            get { return _sPreVal_C; }
            set
            {
                _sPreVal_C = value;
                if (_sPreVal_C.Length == 0)
                {
                    _sPreVal_C = "Err";
                    lab_PHCValue.Text = _sPreVal_C;
                    TRPHGauge.Signal = 0;
                    TRCGauge.Signal = 0;
                    return;
                }

                //if (!ClsTool.IsNumeric(_sPreVal_C))
                //{
                //    _sPreVal_C = "Err";
                //    lab_PHCValue.Text = _sPreVal_C;
                //    TRPHGauge.Signal = 0;
                //    TRCGauge.Signal = 0;
                //    return;
                //}

                _sPreVal_C = _sPreVal_C.Trim();

                lab_PHCValue.Text = _sPreVal_C;
                TRPHGauge.Signal = 1;
                TRCGauge.Signal = 1;

                //if (Convert.ToDouble(_sPreVal_C) < 760)
                //{
                //    TRPHGauge.Signal = 1;
                //    TRCGauge.Signal = 1;
                //}
                //else
                //{
                //    TRPHGauge.Signal = 2;
                //    TRCGauge.Signal = 2;
                //}
            }
        }

        #endregion

        #region is TR 門變色
        private string _DV2_Up_InterLock = "";
        [Category("DV2_Up_InterLock"), Description("DV2 Up InterLock")]
        public string DV2_Up_InterLock
        {
            get { return _DV2_Up_InterLock; }
            set
            {
                _DV2_Up_InterLock = value;
                if (_DV2_Up_InterLock.Equals("1"))
                    rsUpBtn1.BackgroundImage = Properties.Resources.green_up;
                else
                    rsUpBtn1.BackgroundImage = Properties.Resources.red_up;


            }
        }

        private string _DV2_Down_InterLock = "";
        [Category("DV2_Down_InterLock"), Description("DV2 Down InterLock")]
        public string DV2_Down_InterLock
        {
            get { return _DV2_Down_InterLock; }
            set
            {
                _DV2_Down_InterLock = value;
                if (_DV2_Down_InterLock.Equals("1"))
                    rsDownBtn1.BackgroundImage = Properties.Resources.green_down;
                else
                    rsDownBtn1.BackgroundImage = Properties.Resources.red_down;
            }
        }
        
        private string _DV3_Up_InterLock = "";
        [Category("DV3_Up_InterLock"), Description("DV3 Up InterLock")]
        public string DV3_Up_InterLock
        {
            get { return _DV3_Up_InterLock; }
            set
            {
                _DV3_Up_InterLock = value;
                if (_DV3_Up_InterLock.Equals("1"))
                    rsUpBtn2.BackgroundImage = Properties.Resources.green_up;
                else
                    rsUpBtn2.BackgroundImage = Properties.Resources.red_up;
            }
        }

        private string _DV3_Down_InterLock = "";
        [Category("DV3_Down_InterLock"), Description("DV3 Down InterLock")]
        public string DV3_Down_InterLock
        {
            get { return _DV3_Down_InterLock; }
            set
            {
                _DV3_Down_InterLock = value;
                if (_DV3_Down_InterLock.Equals("1"))
                    rsDownBtn2.BackgroundImage = Properties.Resources.green_down;
                else
                    rsDownBtn2.BackgroundImage = Properties.Resources.red_down;
            }
        }

        #endregion

        //Button Left display
        private bool _bleft_TR1 = false;
        [Category("BtnLeft_TR1"), Description("Button Left Display")]
        public bool BtnLeft_TR1
        {
            get { return _bleft_TR1; }
            set
            {
                _bleft_TR1 = value;

                if (_bleft_TR1)
                {
                    btn_TR1_Left.BackColor = Color.Lime;
                }
                else
                {
                    btn_TR1_Left.BackColor = Color.Transparent;
                }
            }
        }

        //Button Right display
        private bool _bright_TR1 = false;
        [Category("BtnRight_TR1"), Description("Button Right Display")]
        public bool BtnRight_TR1
        {
            get { return _bright_TR1; }
            set
            {
                _bright_TR1 = value;

                if (_bright_TR1)
                {
                    btn_TR1_Right.BackColor = Color.Lime;
                }
                else
                {
                    btn_TR1_Right.BackColor = Color.Transparent;
                }
            }
        }

        //Button Right display
        private bool _bclose_TR1 = false;
        [Category("BtnClose_TR1"), Description("Button Close Display")]
        public bool BtnClose_TR1
        {
            get { return _bclose_TR1; }
            set
            {
                _bclose_TR1 = value;

                if (_bclose_TR1)
                {
                    btn_TR1_Stop.BackColor = Color.Lime;
                }
                else
                {
                    btn_TR1_Stop.BackColor = Color.Transparent;
                }
            }
        }

        //Button Left display
        private bool _bleft_TR2 = false;
        [Category("BtnLeft_TR2"), Description("Button Left Display")]
        public bool BtnLeft_TR2
        {
            get { return _bleft_TR2; }
            set
            {
                _bleft_TR2 = value;

                if (_bleft_TR2)
                {
                    btn_TR2_Left.BackColor = Color.Lime;
                }
                else
                {
                    btn_TR2_Left.BackColor = Color.Transparent;
                }
            }
        }

        //Button Right display
        private bool _bright_TR2 = false;
        [Category("BtnRight_TR2"), Description("Button Right Display")]
        public bool BtnRight_TR2
        {
            get { return _bright_TR2; }
            set
            {
                _bright_TR2 = value;

                if (_bright_TR2)
                {
                    btn_TR2_Right.BackColor = Color.Lime;
                }
                else
                {
                    btn_TR2_Right.BackColor = Color.Transparent;
                }
            }
        }

        //Button Right display
        private bool _bclose_TR2 = false;
        [Category("BtnClose_TR2"), Description("Button Close Display")]
        public bool BtnClose_TR2
        {
            get { return _bclose_TR2; }
            set
            {
                _bclose_TR2 = value;

                if (_bclose_TR2)
                {
                    btn_TR2_Stop.BackColor = Color.Lime;
                }
                else
                {
                    btn_TR2_Stop.BackColor = Color.Transparent;
                }
            }
        }

        //Button Left display
        private bool _bleft_TR3 = false;
        [Category("BtnLeft_TR3"), Description("Button Left Display")]
        public bool BtnLeft_TR3
        {
            get { return _bleft_TR3; }
            set
            {
                _bleft_TR3 = value;

                if (_bleft_TR3)
                {
                    btn_TR3_Left.BackColor = Color.Lime;
                }
                else
                {
                    btn_TR3_Left.BackColor = Color.Transparent;
                }
            }
        }

        //Button Right display
        private bool _bright_TR3 = false;
        [Category("BtnRight_TR3"), Description("Button Right Display")]
        public bool BtnRight_TR3
        {
            get { return _bright_TR3; }
            set
            {
                _bright_TR3 = value;

                if (_bright_TR3)
                {
                    btn_TR3_Right.BackColor = Color.Lime;
                }
                else
                {
                    btn_TR3_Right.BackColor = Color.Transparent;
                }
            }
        }

        //Button Right display
        private bool _bclose_TR3 = false;
        [Category("BtnClose_TR3"), Description("Button Close Display")]
        public bool BtnClose_TR3
        {
            get { return _bclose_TR3; }
            set
            {
                _bclose_TR3 = value;

                if (_bclose_TR3)
                {
                    btn_TR3_Stop.BackColor = Color.Lime;
                }
                else
                {
                    btn_TR3_Stop.BackColor = Color.Transparent;
                }
            }
        }
        #endregion

        ThreadingTimer _ThreadTimerC = null;
        TimersTimer _TimersTimer = null;
        private string _sDoorName;
        private string _sCDoorName;


        public delegate void BtnClickDelegate(object sender, EventArgs e); // 自定一個委派
        public event BtnClickDelegate Co_Btn_click; //使用此委派宣告event
        public delegate void AutoBtnClickDelegate(object sender, EventArgs e); // 自定一個委派
        public event AutoBtnClickDelegate Auto_Btn_click; //使用此委派宣告event
        public delegate void TR1CVBtnClickDelegate(object sender, EventArgs e); // 自定一個委派
        public event TR1CVBtnClickDelegate TR1CV_Btn_click; //使用此委派宣告event

        public delegate void CVBtnClickDelegate(object sender, EventArgs e); // 自定一個委派
        public event CVBtnClickDelegate CV_Btn_click; //使用此委派宣告event


        public TR()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor
              | ControlStyles.UserPaint
              | ControlStyles.AllPaintingInWmPaint
              | ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Opaque, false);

            InitializeComponent();

            pal_BackG.SendToBack();

            PropertyInfo info = this.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            info.SetValue(plSubstrateTR1, true, null);
            this.DoubleBuffered = true;
            this.BackColor = Color.FromKnownColor(KnownColor.Transparent);

            //CV control pal
            pal_CVS.Width = 1;
            pal_CVS.Height = 1;
            pal_CVS.Visible = false;
        }

        //for backgrounf transparent
        protected override CreateParams CreateParams
        {
            get //return base.CreateParams;
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT 
                return cp;
            }
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            Visible = false;
            Visible = true;
        }


        #region Form Event
        private void PCC_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Transparent;
        }

        private void rsUpBtn1_Click(object sender, EventArgs e)
        {
            if (this.AutoMode != true)
            {
                if (DV2Open == true)
                {
                    MessageBox.Show("DV2 is already open ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                DV2Open = true;
                Co_Btn_click(sender, e);
            }
            else
            {
                ClsTool.ShowWarningMsg("Auto Mode donot accept manual operation!!");
            }
        }

        private void rsUpBtn2_Click(object sender, EventArgs e)
        {
            if (this.AutoMode != true)
            {
                if (DV3Open == true)
                {
                    MessageBox.Show("DV3 is already open ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                DV3Open = true;
                Co_Btn_click(sender, e);
            }
            else
            {
                ClsTool.ShowWarningMsg("Auto Mode donot accept manual operation!!");
            }
        }

        private void rsDownBtn1_Click(object sender, EventArgs e)
        {
            if (this.AutoMode != true)
            {
                if (DV2Open == false)
                {
                    MessageBox.Show("DV2 is already Close ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                DV2Open = false;
                Co_Btn_click(sender, e);
            }
            else
            {
                ClsTool.ShowWarningMsg("Auto Mode donot accept manual operation!!");
            }
        }

        private void rsDownBtn2_Click(object sender, EventArgs e)
        {
            //funCloseDoor("nsDoor2");
            if (this.AutoMode != true)
            {
                if (DV3Open == false)
                {
                    MessageBox.Show("DV3 is already Close ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                DV3Open = false;
                Co_Btn_click(sender, e);
            }
            else
            {
                ClsTool.ShowWarningMsg("Auto Mode donot accept manual operation!!");
            }
        }


        #endregion


        #region Function

        #region Open Gate Funtion
        public void funOpenDoor(string sDoorName)
        {

            if (sDoorName == "nsDoor1")
            {
                if (nsDoor1.Height < 130 && nsDoor1.Height > 10)
                {
                    MessageBox.Show("入口閘門動作中");
                    return;
                }
                else if (nsDoor2.Height < 130 && nsDoor2.Height > 10)
                {
                    MessageBox.Show("出口閘門開啟中");
                    return;
                }
            }
            else
            {
                if (nsDoor2.Height < 130 && nsDoor2.Height > 10)
                {
                    MessageBox.Show("出口閘門開啟中");
                    return;
                }
                else if (nsDoor1.Height < 130 && nsDoor1.Height > 10)
                {
                    MessageBox.Show("入口閘門動作中");
                    return;
                }
            }

            _sDoorName = sDoorName;
            this._TimersTimer = new TimersTimer();
            this._TimersTimer.Interval = 100;
            this._TimersTimer.Elapsed += new System.Timers.ElapsedEventHandler(_TimersTimer_Elapsed);
            this._TimersTimer.Start();
        }

        delegate void _timerElapsed(string sDoorName);

        private void timerElapsed(string sDoorName)
        {
            if (sDoorName == "nsDoor1")
            {
                if (nsDoor1.Height > 10)
                {
                    nsDoor1.Height = nsDoor1.Height - 10;
                }
                else
                {
                    this._TimersTimer.Stop();
                }
            }

            if (sDoorName == "nsDoor2")
            {
                if (nsDoor2.Height > 10)
                {
                    nsDoor2.Height = nsDoor2.Height - 10;
                }
                else
                {
                    this._TimersTimer.Stop();
                }
            }
        }

        void _TimersTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                string sTemp = _sDoorName;
                _timerElapsed teO = new _timerElapsed(timerElapsed);
                this.Invoke(teO, new object[] { sTemp });
            }
            catch (Exception)
            {
                //throw;
            }
        }

        # endregion

        #region Close Gate Funtion
        public void funCloseDoor(string sDoorName)
        {

            if (sDoorName == "nsDoor1")
            {
                if (nsDoor1.Height > 10 && nsDoor1.Height < 130)
                {
                    MessageBox.Show("入口閘門動作中");
                    return;
                }
                else if (nsDoor2.Height > 10 && nsDoor2.Height < 130)
                {
                    MessageBox.Show("出口閘門動作中");
                    return;
                }
            }
            else
            {
                if (nsDoor2.Height > 10 && nsDoor2.Height < 130)
                {
                    MessageBox.Show("出口閘門動作中");
                    return;
                }
                else if (nsDoor1.Height > 10 && nsDoor1.Height < 130)
                {
                    MessageBox.Show("入口閘門動作中");
                    return;
                }
            }


            _sCDoorName = sDoorName;

            string currentName = new StackTrace(true).GetFrame(0).GetMethod().Name;
            this._ThreadTimerC = new ThreadingTimer(new System.Threading.TimerCallback(CallbackMethod), currentName, 1, 100);

            //this._TimersTimerC = new TimersTimer();
            //this._TimersTimerC.Interval = 100;
            //this._TimersTimerC.Elapsed += new System.Timers.ElapsedEventHandler(_TimersTimerC_Elapsed);
            //this._TimersTimerC.Start();
        }

        delegate void _timerCElapsed(string sDoorName);

        private void timerCElapsed(string sDoorName)
        {
            if (_sCDoorName == "nsDoor1")
            {
                if (nsDoor1.Height < 130)
                {
                    nsDoor1.Height = nsDoor1.Height + 5;
                }
                else
                {
                    //this._TimersTimerC.Stop();
                    this._ThreadTimerC.Dispose();
                }
            }

            if (_sCDoorName == "nsDoor2")
            {
                if (nsDoor2.Height < 130)
                {
                    nsDoor2.Height = nsDoor2.Height + 5;
                }
                else
                {
                    //this._TimersTimerC.Stop();
                    this._ThreadTimerC.Dispose();
                }
            }
        }

        void _TimersTimerC_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            string sTemp = _sCDoorName;
            _timerElapsed teC = new _timerElapsed(timerCElapsed);
            this.Invoke(teC, new object[] { sTemp });
        }

        void CallbackMethod(object State)
        {
            try
            {
                string methodName = State.ToString();
                this.BeginInvoke(new _timerCElapsed(timerCElapsed), new object[] { _sCDoorName });
            }
            catch (Exception)
            {

                //throw;
            }
        }

        # endregion

        private void TR_Resize(object sender, EventArgs e)
        {
            this.Width = 800;
            this.Height = 248;
            pal_BackG.SendToBack();
        }

        #region CV control

        private void btn_Close_Click(object sender, EventArgs e)
        {
            if (this.AutoMode != true)
            {
                CV_Btn_click(sender, e);
            }
            else
            {
                ClsTool.ShowWarningMsg("Auto Mode donot accept manual operation!!");
                //CV control pal
                pal_CVS.Width = 1;
                pal_CVS.Height = 1;
                pal_CVS.Visible = false;
            }
        }


        private void panel1_Click(object sender, EventArgs e)
        {
            if (this.AutoMode != true)
            {
                //show CV control pal
                pal_CVS.Width = 97;
                pal_CVS.Height = 30;
                pal_CVS.Visible = true;
            }
            else
            {
                ClsTool.ShowWarningMsg("Auto Mode donot accept manual operation!!");
            }
        }

        private void txt_CVSpeed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void btn_left_Click(object sender, EventArgs e)
        {
            if (this.AutoMode != true)
            {
                CV_Btn_click(sender, e);
            }
            else
            {
                ClsTool.ShowWarningMsg("Auto Mode donot accept manual operation!!");
                //CV control pal
                pal_CVS.Width = 1;
                pal_CVS.Height = 1;
                pal_CVS.Visible = false;
            }
        }

        private void btn_right_Click(object sender, EventArgs e)
        {
            if (this.AutoMode != true)
            {
                CV_Btn_click(sender, e);
            }
            else
            {
                ClsTool.ShowWarningMsg("Auto Mode donot accept manual operation!!");
                //CV control pal
                pal_CVS.Width = 1;
                pal_CVS.Height = 1;
                pal_CVS.Visible = false;
            }
        }

        #endregion

        #endregion

        private void btn_OnLine_Click(object sender, EventArgs e)
        {
            if (this.AutoMode == true)
            {
                AutoMode = false;
                Auto_Btn_click(sender, e);
            }
            else
            {
                AutoMode = true;
                Auto_Btn_click(sender, e);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lab_PHCValue_Click(object sender, EventArgs e)
        {

        }
    }
}
