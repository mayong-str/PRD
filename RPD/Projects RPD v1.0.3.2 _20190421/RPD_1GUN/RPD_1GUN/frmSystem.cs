using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic.PowerPacks;
using System.Windows.Forms.DataVisualization.Charting;


namespace RPD_1GUN
{
    public partial class frmSystem : Form
    {

        frmRegister _frmReg = new frmRegister();

        bool _RegisterEvent = false;
        string _SQL = @"";
        DataTable _dtDI = null;
        DataTable _dtDO = null;
        private static string MyPath = Application.StartupPath + "\\ThreadError\\";


        bool _IsRefeshDIO = false;

        System.Threading.Timer _ThreadTimer_System = null;
        //System.Threading.Timer _ThreadTimer = null;
        //private static System.Threading.Mutex Timermutex = new System.Threading.Mutex();

        private string _MDAHourSet = "";
        private string _MDAMinSet = "";

        frmHeater f_Heater = new frmHeater();

        public string MDAHourSet
        {
            get { return _MDAHourSet; }
            set { _MDAHourSet = value; }
        }
        public string MDAMinSet
        {
            get { return _MDAMinSet; }
            set { _MDAMinSet = value; }
        }

        public frmSystem()
        {
            if (!Directory.Exists(MyPath))
                Directory.CreateDirectory(MyPath);

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

        private void frmSystem_Load(object sender, EventArgs e)
        {
            RefreshMDA();

            DisplayTimer.Start();
            //DisplayTimer.Enabled = true;
            //_ThreadTimer_System = new System.Threading.Timer(new System.Threading.TimerCallback(DisplayTimer_Tick), "Timer", 0, 1000);

            GridSysInit(ref dgvRcp);


            #region EF事件委派函式
            cEF.CV_Btn_click += new EF.EF.CVBtnClickDelegate(EF_CV_Btn_click);
            #endregion

            #region LL事件委派函式
            cLL.Co_Btn_click += new LL.LL.BtnClickDelegate(LL_Con_Btn_click);
            cLL.Auto_Btn_click += new LL.LL.AutoBtnClickDelegate(LL_Auto_Btn_click);
            cLL.CV_Btn_click += new LL.LL.CVBtnClickDelegate(LL_CV_Btn_click);
            #endregion

            #region TR事件委派函式
            cTR.Co_Btn_click += new TR.TR.BtnClickDelegate(TR_Con_Btn_click);
            cTR.Auto_Btn_click += new TR.TR.AutoBtnClickDelegate(TR_Auto_Btn_click);
            cTR.CV_Btn_click += new TR.TR.CVBtnClickDelegate(TR_CV_Btn_click);
            #endregion

            #region UL事件委派函式
            cUL.Co_Btn_click += new UL.UL.BtnClickDelegate(UL_Con_Btn_click);
            cUL.Auto_Btn_click += new UL.UL.AutoBtnClickDelegate(UL_Auto_Btn_click);
            cUL.CV_Btn_click += new UL.UL.CVBtnClickDelegate(UL_CV_Btn_click);
            #endregion

            #region DF事件委派函式
            cDF.CV_Btn_click += new EF.EF.CVBtnClickDelegate(DF_CV_Btn_click);
            #endregion


            cMFCLLO2.Co_Btn_click += new MFC.MFC.BtnClickDelegate(LLMFC_Con_Btn_click);
            cMFCLLAr.Co_Btn_click += new MFC.MFC.BtnClickDelegate(LLMFC_Con_Btn_click);

            cMFCTRAr_Gun.Co_Btn_click += new MFC.MFC.BtnClickDelegate(TRMFC_Con_Btn_click);
            cMFCTRO2.Co_Btn_click += new MFC.MFC.BtnClickDelegate(TRMFC_Con_Btn_click);
            cMFCTRAr_Chamber.Co_Btn_click += new MFC.MFC.BtnClickDelegate(TRMFC_Con_Btn_click);
            cMFCTRH2O.Co_Btn_click += new MFC.MFC.BtnClickDelegate(TRMFC_Con_Btn_click);


            cMFCULO2.Co_Btn_click += new MFC.MFC.BtnClickDelegate(ULMFC_Con_Btn_click);
            cMFCULAr.Co_Btn_click += new MFC.MFC.BtnClickDelegate(ULMFC_Con_Btn_click);

            //Create DI & DO Button 
            InitialDI();
            InitialDO();

            //Edit By Henry 2015/12/28 修改刷新DIO移至主畫面Timer
            //Create Refresh DI & DO Dsaplay Timer 
            
            //_ThreadTimer = new System.Threading.Timer(new System.Threading.TimerCallback(DisplayTimer_Tick), "Timer", 0, 400); //宣告Timer
            //Thread T_System = new Thread(new ThreadStart(DisplayTimer_Tick));
            //T_System.Start();

            #region PBT Check

            txt_LLDelayTime.Tag = "Time" + "$";
            txt_LLDelayTime.KeyPress += new KeyPressEventHandler(clsTool.Txt_KeyPress);

            txt_TRDelayTime.Tag = "Time" + "$";
            txt_TRDelayTime.KeyPress += new KeyPressEventHandler(clsTool.Txt_KeyPress);

            txt_LLArSet1.Tag = "Time" + "$";
            txt_LLArSet1.KeyPress += new KeyPressEventHandler(clsTool.Txt_KeyPress);
            txt_LLArSet2.Tag = "Time" + "$";
            txt_LLArSet2.KeyPress += new KeyPressEventHandler(clsTool.Txt_KeyPress);
            txt_LLArSet3.Tag = "Time" + "$";
            txt_LLArSet3.KeyPress += new KeyPressEventHandler(clsTool.Txt_KeyPress);

            txt_LLO2Set1.Tag = "Time" + "$";
            txt_LLO2Set1.KeyPress += new KeyPressEventHandler(clsTool.Txt_KeyPress);
            txt_LLO2Set2.Tag = "Time" + "$";
            txt_LLO2Set2.KeyPress += new KeyPressEventHandler(clsTool.Txt_KeyPress);
            txt_LLO2Set3.Tag = "Time" + "$";
            txt_LLO2Set3.KeyPress += new KeyPressEventHandler(clsTool.Txt_KeyPress);

            txt_ULArSet1.Tag = "Time" + "$";
            txt_ULArSet1.KeyPress += new KeyPressEventHandler(clsTool.Txt_KeyPress);
            txt_ULArSet2.Tag = "Time" + "$";
            txt_ULArSet2.KeyPress += new KeyPressEventHandler(clsTool.Txt_KeyPress);
            txt_ULArSet3.Tag = "Time" + "$";
            txt_ULArSet3.KeyPress += new KeyPressEventHandler(clsTool.Txt_KeyPress);

            txt_ULO2Set1.Tag = "Time" + "$";
            txt_ULO2Set1.KeyPress += new KeyPressEventHandler(clsTool.Txt_KeyPress);
            txt_ULO2Set2.Tag = "Time" + "$";
            txt_ULO2Set2.KeyPress += new KeyPressEventHandler(clsTool.Txt_KeyPress);
            txt_ULO2Set3.Tag = "Time" + "$";
            txt_ULO2Set3.KeyPress += new KeyPressEventHandler(clsTool.Txt_KeyPress);


            #endregion

            txtMDAMin_Set.Tag = "Normal" + "$";
            txtMDAMin_Set.KeyPress += new KeyPressEventHandler(clsTool.Txt_KeyPress);
            txtMDAHour_Set.Tag = "Normal" + "$";
            txtMDAHour_Set.KeyPress += new KeyPressEventHandler(clsTool.Txt_KeyPress);

            InitChart(chtMFCFlows);
            InitChart(chtCurrentValue);
            InitChart(chtPressure);
            InitChart(chtVoltage);

            this.chkMFC1_Set.CheckedChanged += this.chkMFC_CheckedChanged;
            this.chkMFC2_Set.CheckedChanged += this.chkMFC_CheckedChanged;
            this.chkMFC3_Set.CheckedChanged += this.chkMFC_CheckedChanged;
            this.chkMFC1_Act.CheckedChanged += this.chkMFC_CheckedChanged;
            this.chkMFC2_Act.CheckedChanged += this.chkMFC_CheckedChanged;
            this.chkMFC3_Act.CheckedChanged += this.chkMFC_CheckedChanged;

            this.chkPressure.CheckedChanged += this.chkPressure_CheckedChanged;
            this.chkPressure2.CheckedChanged += this.chkPressure2_CheckedChanged;
            this.chkRGunMHPushUpLevel.CheckedChanged += this.chkRGunMHPushUpLevel_CheckedChanged;

            this.chkRGunCurrentofBeamGuide.CheckedChanged += this.chkCurrentValue_CheckedChanged;
            this.chkRGunCurrentofG2Coil.CheckStateChanged += this.chkCurrentValue_CheckedChanged;
            this.chkRGunCurrentofHearthCoil.CheckStateChanged += this.chkCurrentValue_CheckedChanged;
            this.chkRGunCurrentofMainHearth.CheckedChanged += this.chkCurrentValue_CheckedChanged;
            this.chkRGunCurrentofSteeringCoil.CheckedChanged += this.chkCurrentValue_CheckedChanged;

            this.chkRGunVoltageofBeamGuide.CheckedChanged += this.chkVoltage_CheckedChanged;
            this.chkRGunVoltageofMainHearth.CheckedChanged += this.chkVoltage_CheckedChanged;
            this.chkRGunDischargeVoltage.CheckedChanged += this.chkVoltage_CheckedChanged;

            f_Heater.TopLevel = false;
            f_Heater.Parent = tabSystem.TabPages[6];
            f_Heater.Visible = true;
        }

        private void RefreshMDA()
        {
            DataRow drMDA = clsDBN.GetRow("SELECT * FROM MDATIME");
            if (drMDA != null)
            {
                txtMDAHour_Set.Text = drMDA["HOUR"].ToString();
                this.MDAHourSet = drMDA["HOUR"].ToString();
                txtMDAMin_Set.Text = drMDA["MIN"].ToString();
                this.MDAMinSet = drMDA["MIN"].ToString();
            }
        }

        private void frmSystem_Activated(object sender, EventArgs e)
        {
            tabSystem.SelectedIndex = 0;
        }

        private void btn_PCC_Click(object sender, EventArgs e)
        {
            frmPCC f = new frmPCC();           
            btn_PCC.Enabled = false;
            f.FormClosed += new FormClosedEventHandler(frmPCC_FormClosed);
            f.Show();
            f.IsShow = true;
        }

        void frmPCC_FormClosed(object sender, FormClosedEventArgs e)
        {
            btn_PCC.Enabled = true;            
        }

        private void System_Timer(object sender) {

        }

        private void DisplayTimer_Tick(object sender, EventArgs e)
        {
            clsTool.ButtonStatus(btnUpdateMDA, null, 'A', true, 1, RPD.STRCTURE.SYS.Data.Level); //刷新更新MDA按鈕權限

            #region SYS

            lblCurrentTactTime.Text = RPD.STRCTURE.SYS.Map.CurrentTactTime + " Sec";

            lblTackTime1.Text = RPD.STRCTURE.SYS.Map.TactTime1 + " Sec";
            lblTackTime2.Text = RPD.STRCTURE.SYS.Map.TactTime2 + " Sec";
            lblTackTime3.Text = RPD.STRCTURE.SYS.Map.TactTime3 + " Sec";
            lblTackTime4.Text = RPD.STRCTURE.SYS.Map.TactTime4 + " Sec";
            lblTackTime5.Text = RPD.STRCTURE.SYS.Map.TactTime5 + " Sec";
            lblTrayCount.Text = RPD.STRCTURE.SYS.Map.TrayCount.ToString();

            if (RPD.STRCTURE.SYS.Btn.AutoModeSwitch.Lamp.Equals(1))
            {
                cEF.AutoMode = true;
                cLL.AutoMode = true;
                cLL.AutoMode = true;
                cTR.AutoMode = true;
                cUL.AutoMode = true;
                cDF.AutoMode = true;

            }
            else
            {
                cEF.AutoMode = false;
                cLL.AutoMode = false;
                cLL.AutoMode = false;
                cTR.AutoMode = false;
                cUL.AutoMode = false;
                cDF.AutoMode = false;
            }

            clsTool.ButtonStatus(btnLeakTest, RPD.STRCTURE.SYS.Btn.LeakTestSwitchTR, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);

            clsTool.txtStatus(txtLeakWaitTime, "D35010", "Min", _RegisterEvent);
            clsTool.txtStatus(txtLeakTime, "D35011", "Min", _RegisterEvent);
            clsTool.ButtonStatus(btnLeakTest, RPD.STRCTURE.SYS.Btn.LeakTestSwitchTR, 'A', true, 3, RPD.STRCTURE.SYS.Data.Level); //LeakTest 按鈕 只刷狀態
            clsTool.ButtonStatus(btnECUpdate, RPD.STRCTURE.SYS.Btn.MachineDataSaveSwitch, 'A', true, 1, RPD.STRCTURE.SYS.Data.Level); //EC Update 按鈕 只刷狀態 Admin權限才可操作
            clsTool.ButtonStatus(btnTrayCountReset, RPD.STRCTURE.SYS.Btn.TrayCountResetSwitch, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);

            LeakTestRequest();

            if (RPD.STRCTURE.SYS.Map.StartCodeFailure.Equals(1))
            {
                tabSystem.Enabled = false;
                _frmReg.Show();
            }
            else
            {
                tabSystem.Enabled = true;
                _frmReg.Hide();
            }

            if (RPD.STRCTURE.SYS.DI.MainCDAPressureOK.Lamp.Equals(1))
                os_MainCDA.FillColor = Color.Lime;
            else
                os_MainCDA.FillColor = Color.Red;

            if (RPD.STRCTURE.SYS.DI.MainWaterPressureOK.Lamp.Equals(1))
                os_MainWater.FillColor = Color.Lime;
            else
                os_MainWater.FillColor = Color.Red;

            txt_MainWaterTemp.Text = RPD.STRCTURE.SYS.Map.MainWaterTemp.ToString();

            #endregion

            #region EC
            clsTool.txtStatus(EC_CVSpeedSet, "D35201$D35202", "Dword-2", 10, 54000, _RegisterEvent);
            clsTool.txtStatus(EC_ProcessSpeedSet, "D35203$D35204", "Dword-2", 600, 10000, _RegisterEvent);
            clsTool.txtStatus(EC_DVOpenATM, "D35205$D35206", "Dword-6", 700, 760, _RegisterEvent);
            clsTool.txtStatus(EC_TRCV1_TRCV2DelayTime, "D35207", "Time-2", 0, 2, _RegisterEvent);
            clsTool.txtStatus(EC_EFCVSpeedSet, "D35208$D35209", "Dword-3", 0, 10, _RegisterEvent);
            clsTool.txtStatus(EC_EFCVDecSet, "D35210$D35211", "Dword-3", 0, 10, _RegisterEvent);
            clsTool.txtStatus(EC_LLCVSpeedSet, "D35212$D35213", "Dword-3", 0, 10, _RegisterEvent);
            clsTool.txtStatus(EC_LLCVDecSet, "D35214$D35215", "Dword-3", 0, 10, _RegisterEvent);
            clsTool.txtStatus(EC_TR1CVSpeedSet, "D35216$D35217", "Dword-3", 0, 10, _RegisterEvent);
            clsTool.txtStatus(EC_TR1CVDecSet, "D35218$D35219", "Dword-3", 0, 10, _RegisterEvent);
            clsTool.txtStatus(EC_TR2CVSpeedSet, "D35220$D35221", "Dword-3", 0, 10, _RegisterEvent);
            clsTool.txtStatus(EC_TR2CVDecSet, "D35222$D35223", "Dword-3", 0, 10, _RegisterEvent);
            clsTool.txtStatus(EC_TR3CVSpeedSet, "D35224$D35225", "Dword-3", 0, 10, _RegisterEvent);
            clsTool.txtStatus(EC_TR3CVDecSet, "D35226$D35227", "Dword-3", 0, 10, _RegisterEvent);
            clsTool.txtStatus(EC_ULCVSpeedSet, "D35228$D35229", "Dword-3", 0, 10, _RegisterEvent);
            clsTool.txtStatus(EC_ULCVDecSet, "D35230$D35231", "Dword-3", 0, 10, _RegisterEvent);
            clsTool.txtStatus(EC_DFCVSpeedSet, "D35232$D35233", "Dword-3", 0, 10, _RegisterEvent);
            clsTool.txtStatus(EC_DFCVDecSet, "D35234$D35235", "Dword-3", 0, 10, _RegisterEvent);
            clsTool.txtStatus(EC_EFToLLDelayTime, "D35236", "Time-2", 0, 2, _RegisterEvent);
            clsTool.txtStatus(EC_TR3CVToULDelayTime, "D35237", "Time-2", 0, 2, _RegisterEvent);
            clsTool.txtStatus(EC_ULToUNLDPortDelayTime, "D35238", "Time-2", 0, 2, _RegisterEvent);
            clsTool.txtStatus(EC_Water_Higher_Limit, "D35239", "Time", 30, 35, _RegisterEvent);
            clsTool.txtStatus(EC_Water_Lower_Limit, "D35240", "Time", 17, 20, _RegisterEvent);

            //Add by Henry 2015/7/13 新增各水Sensor下限值

            clsTool.txtStatus(EC_LLPumpFlowLowerLimit, "D35242", "Time", 0, 0, _RegisterEvent);
            clsTool.txtStatus(EC_BackingFlowLowerLimit, "D35244", "Time", 0, 0, _RegisterEvent);
            clsTool.txtStatus(EC_ULPumpFlowLowerLimit, "D35246", "Time", 0, 0, _RegisterEvent);
            clsTool.txtStatus(EC_LLTPPumpFlowLowerLimit, "D35248", "Time", 0, 0, _RegisterEvent);
            clsTool.txtStatus(EC_TR2CHBCOVERFlowLowerLimit, "D35250", "Time", 0, 0, _RegisterEvent);
            clsTool.txtStatus(EC_TRTPPumpFlowLowerLimit, "D35252", "Time", 0, 0, _RegisterEvent);
            clsTool.txtStatus(EC_TR3CHBCOVERFlowLowerLimit, "D35254", "Time", 0, 0, _RegisterEvent);
            clsTool.txtStatus(EC_TR2CoolingFlowLowerLimit, "D35256", "Time", 0, 0, _RegisterEvent);

            //modify 2016.7.20 flow lower limit by william
            //Modify by Albert 2019/5/9
            clsTool.txtStatus(EC_PlasmaGeneratorG1FlowLowerLimit, "D35258", "Time", 0, 5, _RegisterEvent);
            clsTool.txtStatus(EC_PlasmaGeneratorG2FlowLowerLimit, "D35260", "Time", 0, 5, _RegisterEvent);
            clsTool.txtStatus(EC_PlasmaGeneratorsteeringcoilFlowLowerLimit, "D35262", "Time", 0, 1.2, _RegisterEvent);
            clsTool.txtStatus(EC_HearthMainFlowLowerLimit, "D35264", "Time", 0, 4, _RegisterEvent);
            clsTool.txtStatus(EC_HearthBeamFlowLowerLimit, "D35266", "Time", 0, 4, _RegisterEvent);


            clsTool.txtStatus(EC_RPDDoorInwallFlowLowerLimit, "D35268", "Time", 0, 0, _RegisterEvent);
            clsTool.txtStatus(EC_RPDChamberInwallFlowLowerLimit, "D35270", "Time", 0, 0, _RegisterEvent);
            clsTool.txtStatus(EC_ULTPPumpFlowLowerLimit, "D35272", "Time", 0, 0, _RegisterEvent);
            
            clsTool.txtStatus(EC_IdelTime, "D35273", "Min", _RegisterEvent);
            clsTool.txtStatus(EC_ReadyTime, "D35274", "Min", _RegisterEvent);

           
            clsTool.txtStatus(EC_LLULTargetPreSet, "D35275$D35276", "Dword-6", 0, 1, _RegisterEvent);

            clsTool.txtStatus(EC_LLSRVTime, "D35277", "Time", 0, 0, _RegisterEvent);
            clsTool.txtStatus(EC_ULSRVTime, "D35278", "Time", 0, 0, _RegisterEvent);

            clsTool.txtStatus(EC_TRCgaugeOffset, "D35279$D35280", "Dword-6", 0, 0, _RegisterEvent);

            clsTool.txtStatus(EC_LLMFCDelayTime, "D35285", "Time", 0, 0, _RegisterEvent);
            clsTool.txtStatus(EC_ULMFCDelayTime, "D35286", "Time", 0, 0, _RegisterEvent);

            clsTool.txtStatus(EC_LLPCAccuracy, "D35287", "Normal", 3, 10, _RegisterEvent);
            clsTool.txtStatus(EC_ULPCAccuracy, "D35288", "Normal", 3, 10, _RegisterEvent);

            //2016.7.20 add G! G2 Temperature  limit EC by william
            //clsTool.txtStatus(EC_PlasmaGeneratorG1BodyTempHigherLimit, "D38489", "Time", 200, 0, _RegisterEvent);
            //clsTool.txtStatus(EC_PlasmaGeneratorG1BodyTempLowerLimit, "D38490", "Time", 5, 0, _RegisterEvent);
            //clsTool.txtStatus(EC_PlasmaGeneratorG1CWTempHigherLimit, "D38491", "Time", 200, 0, _RegisterEvent);
            //clsTool.txtStatus(EC_PlasmaGeneratorG1CWTempLowerLimit, "D38492", "Time", 5, 0, _RegisterEvent);
            

            

            #endregion

            #region EF
            if (RPD.STRCTURE.SYS.DI.EntryFeederStartReady.Equals(1))
                cEF.ReadyBtnShow = true;
            else
                cEF.ReadyBtnShow = false;

            if (RPD.STRCTURE.SYS.DI.EntryFeederTrayExistBWD.Lamp.Equals(1))
                cEF.FPS = true;
            else
                cEF.FPS = false;

            if (RPD.STRCTURE.SYS.DI.EntryFeederTrayExistFWD.Lamp.Equals(1))
                cEF.RPS = true;
            else
                cEF.RPS = false;

            if (RPD.STRCTURE.EF.Btn.LDPortCVForwardSwitch.Lamp.Equals(1) || RPD.STRCTURE.EF.Btn.LDPortCVReverseSwitch.Lamp.Equals(1)) //取前進後退的燈判斷CV是否在運動
                cEF.CVAct = true;
            else
                cEF.CVAct = false;

            if (RPD.STRCTURE.EF.Btn.LDPortCVReverseSwitch.Lamp.Equals(1))
                cEF.BtnLeft = true;
            else
                cEF.BtnLeft = false;

            if (RPD.STRCTURE.EF.Btn.LDPortCVForwardSwitch.Lamp.Equals(1))
                cEF.BtnRight = true;
            else
                cEF.BtnRight = false;

            if (RPD.STRCTURE.EF.Btn.LDPortCVStopSwitch.Lamp.Equals(1))
                cEF.BtnClose = true;
            else
                cEF.BtnClose = false;

            if (RPD.STRCTURE.EF.Map.TaryRegistration.Equals(1))
                cEF.TrateVisible = true;
            else
                cEF.TrateVisible = false;

            #endregion

            #region LL

            if (RPD.STRCTURE.LL.Btn.LLCVForwardSwitch.Lamp.Equals(1) || RPD.STRCTURE.LL.Btn.LLCVReverseSwitch.Lamp.Equals(1))
                cLL.CVAct = true;
            else
                cLL.CVAct = false;

            if (RPD.STRCTURE.LL.Btn.LLCVForwardSwitch.Lamp.Equals(1))
                cLL.BtnRight = true;
            else
                cLL.BtnRight = false;

            if (RPD.STRCTURE.LL.Btn.LLCVReverseSwitch.Lamp.Equals(1))
                cLL.BtnLeft = true;
            else
                cLL.BtnLeft = false;

            if (RPD.STRCTURE.LL.Btn.LLCVStopSwitch.Lamp.Equals(1))
                cLL.BtnClose = true;
            else
                cLL.BtnClose = false;


            if (RPD.STRCTURE.SYS.DI.LLHVBTrayExistBWD.Lamp.Equals(1))
                cLL.FPS = true;
            else
                cLL.FPS = false;

            if (RPD.STRCTURE.SYS.DI.LLHVBTrayExistFWD.Lamp.Equals(1))
                cLL.RPS = true;
            else
                cLL.RPS = false;

            if (RPD.STRCTURE.LL.Btn.DV1OpenSwitch.Lamp.Equals(1) && RPD.STRCTURE.LL.Btn.DV1CloseSwitch.Lamp.Equals(0))
                cLL.DV1Open = true;
            else
                cLL.DV1Open = false;

            if (RPD.STRCTURE.SYS.DI.LLHVBatATM.Lamp.Equals(1))
            {
                cLL.IsATM = true;
                cLL.IsVac = false;
            }
            else
            {
                cLL.IsATM = false;
                cLL.IsVac = true;
            }

            if (RPD.STRCTURE.LL.Map.TaryRegistration.Equals(1))
                cLL.TrateVisible = true;
            else
                cLL.TrateVisible = false;

            if (RPD.STRCTURE.SYS.DI.LLHVBTurboPumpCoolingWaterOK.Lamp.Equals(1))
                os_LLTPCooling.FillColor = Color.Lime;
            else
                os_LLTPCooling.FillColor = Color.Red;

            if (RPD.STRCTURE.SYS.DI.LLHVBDryPumpCoolingWaterOK.Lamp.Equals(1))
                os_LLDPCooling.FillColor = Color.Lime;
            else
                os_LLDPCooling.FillColor = Color.Red;

            if (RPD.STRCTURE.SYS.DI.BackingDryPumpCoolingWaterOK.Lamp.Equals(1))
                os_ToDPCooling.FillColor = Color.Lime;
            else
                os_ToDPCooling.FillColor = Color.Red;

            if (RPD.STRCTURE.LL.Btn.DV1OpenSwitch.Interlock.Equals(1))
                cLL.DV1_Up_InterLock = "1";
            else
                cLL.DV1_Up_InterLock = "0";

            if (RPD.STRCTURE.LL.Btn.DV1CloseSwitch.Interlock.Equals(1))
                cLL.DV1_Down_InterLock = "1";
            else
                cLL.DV1_Down_InterLock = "0";



            clsTool.PVStatus(pv_SRV1, RPD.STRCTURE.LL.Btn.SRV1OpenSwitch, RPD.STRCTURE.LL.Btn.SRV1CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_RV1, RPD.STRCTURE.LL.Btn.RV1OpenSwitch, RPD.STRCTURE.LL.Btn.RV1CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_LLEV1, RPD.STRCTURE.LL.Btn.EV1OpenSwitch, RPD.STRCTURE.LL.Btn.EV1CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.VPStatus(cLLDP, RPD.STRCTURE.LL.Btn.LLBPDPRunSwitch, RPD.STRCTURE.LL.Btn.LLBPDPStopSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.BPStatus(cLLBP, RPD.STRCTURE.LL.Btn.LLBPDPRunSwitch, RPD.STRCTURE.LL.Btn.LLBPDPStopSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.GVStatus(cLL_GV1, RPD.STRCTURE.LL.Btn.GV1OpenSwitch, RPD.STRCTURE.LL.Btn.GV1CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_VMV1, RPD.STRCTURE.LL.Btn.VMV1OpenSwitch, RPD.STRCTURE.LL.Btn.VMV1CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.GVStatus(gv_VV1, RPD.STRCTURE.LL.Btn.VV1OpenSwitch, RPD.STRCTURE.LL.Btn.VV1CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_MDV1, RPD.STRCTURE.LL.Btn.MDV1OpenSwitch, RPD.STRCTURE.LL.Btn.MDV1CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_MDV2, RPD.STRCTURE.LL.Btn.MDV2OpenSwitch, RPD.STRCTURE.LL.Btn.MDV2CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_MDV3, RPD.STRCTURE.LL.Btn.MDV3OpenSwitch, RPD.STRCTURE.LL.Btn.MDV3CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_MDV4, RPD.STRCTURE.LL.Btn.MDV4OpenSwitch, RPD.STRCTURE.LL.Btn.MDV4CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_MDV5, RPD.STRCTURE.LL.Btn.MDV5OpenSwitch, RPD.STRCTURE.LL.Btn.MDV5CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.TPStatus(cLL_TP, RPD.STRCTURE.LL.Btn.LLTPRunSwitch, RPD.STRCTURE.LL.Btn.LLTPStopSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);

            //Add By Henry 2015/7/9 Purge Valve
            clsTool.PVStatus(pv_PMV1, RPD.STRCTURE.LL.Btn.PMV1OpenSwitch, RPD.STRCTURE.LL.Btn.PMV1CloseSwitch, true, 3, RPD.STRCTURE.SYS.Data.Level);

            //Add By Henry 2016/1/22 MFC Valve
            clsTool.PVStatus(pv_MDV20, RPD.STRCTURE.LL.Btn.MDV20OpenSwitch, RPD.STRCTURE.LL.Btn.MDV20CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_MDV21, RPD.STRCTURE.LL.Btn.MDV21OpenSwitch, RPD.STRCTURE.LL.Btn.MDV21CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);

            cMFCLLAr.MFCName = "Ar";
            cMFCLLO2.MFCName = "O2";

            #endregion

            #region TR

            if (RPD.STRCTURE.TR.Btn.TR1CVForwardSwitch.Lamp.Equals(1) || RPD.STRCTURE.TR.Btn.TR1CVReverseSwitch.Lamp.Equals(1))
                cTR.CVActTR1 = true;
            else
                cTR.CVActTR1 = false;

            if (RPD.STRCTURE.TR.Btn.TR1CVForwardSwitch.Lamp.Equals(1))
                cTR.BtnRight_TR1 = true;
            else
                cTR.BtnRight_TR1 = false;

            if (RPD.STRCTURE.TR.Btn.TR1CVReverseSwitch.Lamp.Equals(1))
                cTR.BtnLeft_TR1 = true;
            else
                cTR.BtnLeft_TR1 = false;

            if (RPD.STRCTURE.TR.Btn.TR1CVStopSwitch.Lamp.Equals(1))
                cTR.BtnClose_TR1 = true;
            else
                cTR.BtnClose_TR1 = false;

            if (RPD.STRCTURE.SYS.DI.TR1TrayExistBWD.Lamp.Equals(1))
                cTR.FPSTR1 = true;
            else
                cTR.FPSTR1 = false;

            if (RPD.STRCTURE.SYS.DI.TR1TrayExistFWD.Lamp.Equals(1))
                cTR.RPSTR1 = true;
            else
                cTR.RPSTR1 = false;

            if (RPD.STRCTURE.SYS.DI.TR1TraySpeedUpMID.Lamp.Equals(1))
                cTR.CPSTR1 = true;
            else
                cTR.CPSTR1 = false;

            if (RPD.STRCTURE.SYS.DI.TRatATM.Lamp.Equals(1))
                cTR.IsATM = true;

            else
                cTR.IsATM = false;

            if (RPD.STRCTURE.TR.Btn.TR2CVForwardSwitch.Lamp.Equals(1) || RPD.STRCTURE.TR.Btn.TR2CVReverseSwitch.Lamp.Equals(1))
                cTR.CVActTR2 = true;
            else
                cTR.CVActTR2 = false;

            if (RPD.STRCTURE.TR.Btn.TR2CVForwardSwitch.Lamp.Equals(1))
                cTR.BtnRight_TR2 = true;
            else
                cTR.BtnRight_TR2 = false;

            if (RPD.STRCTURE.TR.Btn.TR2CVReverseSwitch.Lamp.Equals(1))
                cTR.BtnLeft_TR2 = true;
            else
                cTR.BtnLeft_TR2 = false;

            if (RPD.STRCTURE.TR.Btn.TR2CVStopSwitch.Lamp.Equals(1))
                cTR.BtnClose_TR2 = true;
            else
                cTR.BtnClose_TR2 = false;

            if (RPD.STRCTURE.SYS.DI.TR2CoolerCoolingWaterOK.Lamp.Equals(1))
                cTR.CPTR2 = true;
            else
                cTR.CPTR2 = false;

            if (RPD.STRCTURE.SYS.DI.TR2CHBCoverCoolingWaterOK.Lamp.Equals(1))
                cTR.CoolTR2 = true;
            else
                cTR.CoolTR2 = false;

            if (RPD.STRCTURE.SYS.DI.TR2TrayExistBWD.Lamp.Equals(1))
                cTR.FPSTR2 = true;
            else
                cTR.FPSTR2 = false;

            if (RPD.STRCTURE.SYS.DI.TR2TrayExistFWD.Lamp.Equals(1))
                cTR.RPSTR2 = true;
            else
                cTR.RPSTR2 = false;

            if (RPD.STRCTURE.TR.Btn.TR3CVForwardSwitch.Lamp.Equals(1) || RPD.STRCTURE.TR.Btn.TR3CVReverseSwitch.Lamp.Equals(1))
                cTR.CVActTR3 = true;
            else
                cTR.CVActTR3 = false;

            if (RPD.STRCTURE.TR.Btn.TR3CVForwardSwitch.Lamp.Equals(1))
                cTR.BtnRight_TR3 = true;
            else
                cTR.BtnRight_TR3 = false;

            if (RPD.STRCTURE.TR.Btn.TR3CVReverseSwitch.Lamp.Equals(1))
                cTR.BtnLeft_TR3 = true;
            else
                cTR.BtnLeft_TR3 = false;

            if (RPD.STRCTURE.TR.Btn.TR3CVStopSwitch.Lamp.Equals(1))
                cTR.BtnClose_TR3 = true;
            else
                cTR.BtnClose_TR3 = false;

            if (RPD.STRCTURE.SYS.DI.TR3TrayExistBWD.Lamp.Equals(1))
                cTR.FPSTR3 = true;
            else
                cTR.FPSTR3 = false;

            if (RPD.STRCTURE.SYS.DI.TR3TrayExistFWD.Lamp.Equals(1))
                cTR.RPSTR3 = true;
            else
                cTR.RPSTR3 = false;

            if (RPD.STRCTURE.SYS.DI.TR3TraySpeedUpMID.Lamp.Equals(1))
                cTR.CPSTR3 = true;
            else
                cTR.CPSTR3 = false;


            if (RPD.STRCTURE.SYS.DI.TRatATM.Lamp.Equals(1))
                cTR.IsATM = true;
            else
                cTR.IsATM = false;

            if (RPD.STRCTURE.SYS.DI.TR3CHBCoverCoolingWaterOK.Lamp.Equals(1))
                cTR.CoolTR3 = true;
            else
                cTR.CoolTR3 = false;

            if (RPD.STRCTURE.TR.Btn.DV2OpenSwitch.Lamp.Equals(1) && RPD.STRCTURE.TR.Btn.DV2CloseSwitch.Lamp.Equals(0))
                cTR.DV2Open = true;
            else
                cTR.DV2Open = false;

            if (RPD.STRCTURE.TR.Btn.DV3OpenSwitch.Lamp.Equals(1) && RPD.STRCTURE.TR.Btn.DV3CloseSwitch.Lamp.Equals(0))
                cTR.DV3Open = true;
            else
                cTR.DV3Open = false;

            if (RPD.STRCTURE.TR.Btn.DV2OpenSwitch.Interlock.Equals(1))
                cTR.DV2_Up_InterLock = "1";
            else
                cTR.DV2_Up_InterLock = "0";

            if (RPD.STRCTURE.TR.Btn.DV2CloseSwitch.Interlock.Equals(1))
                cTR.DV2_Down_InterLock = "1";
            else
                cTR.DV2_Down_InterLock = "0";


            if (RPD.STRCTURE.TR.Btn.DV3OpenSwitch.Interlock.Equals(1))
                cTR.DV3_Up_InterLock = "1";
            else
                cTR.DV3_Up_InterLock = "0";

            if (RPD.STRCTURE.TR.Btn.DV3CloseSwitch.Interlock.Equals(1))
                cTR.DV3_Down_InterLock = "1";
            else
                cTR.DV3_Down_InterLock = "0";

            clsTool.PVStatus(pv_TRRV3, RPD.STRCTURE.TR.Btn.RV3OpenSwitch, RPD.STRCTURE.TR.Btn.RV3CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_TREV3, RPD.STRCTURE.TR.Btn.EV3OpenSwitch, RPD.STRCTURE.TR.Btn.EV3CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_TRVMV3, RPD.STRCTURE.TR.Btn.VMV3OpenSwitch, RPD.STRCTURE.TR.Btn.VMV3CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.GVStatus(cTR_GV3, RPD.STRCTURE.TR.Btn.GV3OpenSwitch, RPD.STRCTURE.TR.Btn.GV3CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.GVStatus(gv_VV3, RPD.STRCTURE.TR.Btn.VV3OpenSwitch, RPD.STRCTURE.TR.Btn.VV3CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_TRMDV11, RPD.STRCTURE.TR.Btn.MDV11OpenSwitch, RPD.STRCTURE.TR.Btn.MDV11CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_TRMDV12, RPD.STRCTURE.TR.Btn.MDV12OpenSwitch, RPD.STRCTURE.TR.Btn.MDV12CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_TRMDV13, RPD.STRCTURE.TR.Btn.MDV13OpenSwitch, RPD.STRCTURE.TR.Btn.MDV13CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_TRMDV14, RPD.STRCTURE.TR.Btn.MDV14OpenSwitch, RPD.STRCTURE.TR.Btn.MDV14CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_TRMDV15, RPD.STRCTURE.TR.Btn.MDV15OpenSwitch, RPD.STRCTURE.TR.Btn.MDV15CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_TRMDV16, RPD.STRCTURE.TR.Btn.MDV16OpenSwitch, RPD.STRCTURE.TR.Btn.MDV16CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_TRMDV17, RPD.STRCTURE.TR.Btn.MDV17OpenSwitch, RPD.STRCTURE.TR.Btn.MDV17CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_TRMDV18, RPD.STRCTURE.TR.Btn.MDV18OpenSwitch, RPD.STRCTURE.TR.Btn.MDV18CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_TRMDV19, RPD.STRCTURE.TR.Btn.MDV19OpenSwitch, RPD.STRCTURE.TR.Btn.MDV19CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.TPStatus(cTR_TP, RPD.STRCTURE.TR.Btn.TRTPRunSwitch, RPD.STRCTURE.TR.Btn.TRTPStopSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.BPStatus(cToBP, RPD.STRCTURE.TR.Btn.DPRunSwitch, RPD.STRCTURE.TR.Btn.DPStopSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.VPStatus(cToDP, RPD.STRCTURE.TR.Btn.DPRunSwitch, RPD.STRCTURE.TR.Btn.DPStopSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);

            //txtTRTPPumpFlowValue.Text = RPD.STRCTURE.UL.Map.ULPumpFlowValue.ToString();

            if (RPD.STRCTURE.SYS.DI.PCCTurboPumpCoolingWaterOK.Lamp.Equals(1))
                os_TRTPCooling.FillColor = Color.Lime;
            else
                os_TRTPCooling.FillColor = Color.Red;

            if (RPD.STRCTURE.TR.Map.TR1_TaryRegistration.Equals(1))
                cTR.TrateVisibleTR1 = true;
            else
                cTR.TrateVisibleTR1 = false;

            if (RPD.STRCTURE.TR.Map.TR2_TaryRegistration.Equals(1))
                cTR.TrateVisibleTR2 = true;
            else
                cTR.TrateVisibleTR2 = false;

            if (RPD.STRCTURE.TR.Map.TR3_TaryRegistration.Equals(1))
                cTR.TrateVisibleTR3 = true;
            else
                cTR.TrateVisibleTR3 = false;

            cMFCTRAr_Chamber.MFCName = "ArC";
            cMFCTRO2.MFCName = "O2";
            cMFCTRAr_Gun.MFCName = "ArG";
            cMFCTRH2O.MFCName = "H2O";

            //MFC auto 同步       
            cMFCTRAr_Chamber.AutoMode = cTR.AutoMode;
            cMFCTRO2.AutoMode = cTR.AutoMode;
            cMFCTRAr_Gun.AutoMode = cTR.AutoMode;
            cMFCTRH2O.AutoMode = cTR.AutoMode;

            #endregion

            #region UL

            if (RPD.STRCTURE.UL.Btn.ULCVForwardSwitch.Lamp.Equals(1) || RPD.STRCTURE.UL.Btn.ULCVReverseSwitch.Lamp.Equals(1))
                cUL.CVAct = true;
            else
                cUL.CVAct = false;


            if (RPD.STRCTURE.UL.Btn.ULCVForwardSwitch.Lamp.Equals(1))
                cUL.BtnRight = true;
            else
                cUL.BtnRight = false;

            if (RPD.STRCTURE.UL.Btn.ULCVReverseSwitch.Lamp.Equals(1))
                cUL.BtnLeft = true;
            else
                cUL.BtnLeft = false;

            if (RPD.STRCTURE.UL.Btn.ULCVStopSwitch.Lamp.Equals(1))
                cUL.BtnClose = true;
            else
                cUL.BtnClose = false;

            if (RPD.STRCTURE.SYS.DI.ULHVBTrayExistBWD.Lamp.Equals(1))
                cUL.RPS = true;
            else
                cUL.RPS = false;

            if (RPD.STRCTURE.SYS.DI.ULHVBTrayExistFWD.Lamp.Equals(1))
                cUL.FPS = true;
            else
                cUL.FPS = false;

            if (RPD.STRCTURE.UL.Btn.DV4OpenSwitch.Lamp.Equals(1) && RPD.STRCTURE.UL.Btn.DV4CloseSwitch.Lamp.Equals(0))
                cUL.DV4Open = true;
            else
                cUL.DV4Open = false;

            if (RPD.STRCTURE.SYS.DI.ULHVBatATM.Lamp.Equals(1))
            {
                cUL.IsATM = true;
                cUL.IsVac = false;
            }
            else
            {
                cUL.IsATM = false;
                cUL.IsVac = true;
            }

            if (RPD.STRCTURE.UL.Map.TaryRegistration.Equals(1))
                cUL.TrateVisible = true;
            else
                cUL.TrateVisible = false;

            if (RPD.STRCTURE.SYS.DI.ULHVBTurboPumpCoolingWaterOK.Lamp.Equals(1))
                os_ULTPCooling.FillColor = Color.Lime;
            else
                os_ULTPCooling.FillColor = Color.Red;

            if (RPD.STRCTURE.SYS.DI.ULHVBDryBoostPumpCoolingWaterOK.Lamp.Equals(1))
                os_ULDPCooling.FillColor = Color.Lime;
            else
                os_ULDPCooling.FillColor = Color.Red;

            if (RPD.STRCTURE.UL.Btn.DV4OpenSwitch.Interlock.Equals(1))
                cUL.DV4_Up_InterLock = "1";
            else
                cUL.DV4_Up_InterLock = "0";

            if (RPD.STRCTURE.UL.Btn.DV4CloseSwitch.Interlock.Equals(1))
                cUL.DV4_Down_InterLock = "1";
            else
                cUL.DV4_Down_InterLock = "0";

            clsTool.PVStatus(pv_ULSRV2, RPD.STRCTURE.UL.Btn.SRV2OpenSwitch, RPD.STRCTURE.UL.Btn.SRV2CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_ULRV2, RPD.STRCTURE.UL.Btn.RV2OpenSwitch, RPD.STRCTURE.UL.Btn.RV2CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.BPStatus(cULBP, RPD.STRCTURE.UL.Btn.ULBPDPRunSwitch, RPD.STRCTURE.UL.Btn.ULBPDPStopSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.VPStatus(cULDP, RPD.STRCTURE.UL.Btn.ULBPDPRunSwitch, RPD.STRCTURE.UL.Btn.ULBPDPStopSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.GVStatus(cUL_GV2, RPD.STRCTURE.UL.Btn.GV2OpenSwitch, RPD.STRCTURE.UL.Btn.GV2CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_ULEV2, RPD.STRCTURE.UL.Btn.EV2OpenSwitch, RPD.STRCTURE.UL.Btn.EV2CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_VMV2, RPD.STRCTURE.UL.Btn.VMV2OpenSwitch, RPD.STRCTURE.UL.Btn.VMV2CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.GVStatus(gv_VV2, RPD.STRCTURE.UL.Btn.VV2OpenSwitch, RPD.STRCTURE.UL.Btn.VV2CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_MDV6, RPD.STRCTURE.UL.Btn.MDV6OpenSwitch, RPD.STRCTURE.UL.Btn.MDV6CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_MDV7, RPD.STRCTURE.UL.Btn.MDV7OpenSwitch, RPD.STRCTURE.UL.Btn.MDV7CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_MDV8, RPD.STRCTURE.UL.Btn.MDV8OpenSwitch, RPD.STRCTURE.UL.Btn.MDV8CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_MDV9, RPD.STRCTURE.UL.Btn.MDV9OpenSwitch, RPD.STRCTURE.UL.Btn.MDV9CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_MDV10, RPD.STRCTURE.UL.Btn.MDV10OpenSwitch, RPD.STRCTURE.UL.Btn.MDV10CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.TPStatus(cUL_TP, RPD.STRCTURE.UL.Btn.ULTPRunSwitch, RPD.STRCTURE.UL.Btn.ULTPStopSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);

            //Add By Henry 2015/7/9 Purge Valve
            clsTool.PVStatus(pv_PMV2, RPD.STRCTURE.UL.Btn.PMV2OpenSwitch, RPD.STRCTURE.UL.Btn.PMV2CloseSwitch, true, 3, RPD.STRCTURE.SYS.Data.Level);



            cMFCULAr.MFCName = "Ar";
            cMFCULO2.MFCName = "O2";

            //MFC auto 同步
            cMFCULAr.AutoMode = cUL.AutoMode;
            cMFCULO2.AutoMode = cUL.AutoMode;

            #endregion

            #region DF

            if (RPD.STRCTURE.SYS.DI.DeliverFeederTakeoutReady.Lamp.Equals(1))
                cDF.ReadyBtnShow = true;
            else
                cDF.ReadyBtnShow = false;

            if (RPD.STRCTURE.SYS.DI.DeliverFeederTrayExistBWD.Lamp.Equals(1))
                cDF.FPS = true;
            else
                cDF.FPS = false;

            if (RPD.STRCTURE.SYS.DI.DeliverFeederTrayExistFWD.Lamp.Equals(1))
                cDF.RPS = true;
            else
                cDF.RPS = false;

            if (RPD.STRCTURE.DF.Btn.UNLDPortCVForwardSwitch.Lamp.Equals(1) || RPD.STRCTURE.DF.Btn.UNLDPortCVReverseSwitch.Lamp.Equals(1)) //取前進後退的燈判斷CV是否在運動
                cDF.CVAct = true;
            else
                cDF.CVAct = false;

            if (RPD.STRCTURE.DF.Btn.UNLDPortCVReverseSwitch.Lamp.Equals(1))
                cDF.BtnLeft = true;
            else
                cDF.BtnLeft = false;

            if (RPD.STRCTURE.DF.Btn.UNLDPortCVForwardSwitch.Lamp.Equals(1))
                cDF.BtnRight = true;
            else
                cDF.BtnRight = false;

            if (RPD.STRCTURE.DF.Btn.UNLDPortCVStopSwitch.Lamp.Equals(1))
                cDF.BtnClose = true;
            else
                cDF.BtnClose = false;

            if (RPD.STRCTURE.DF.Map.TaryRegistration.Equals(1))
                cDF.TrateVisible = true;
            else
                cDF.TrateVisible = false;

            #endregion

            #region PCC

            clsTool.GVStatus(cPCC_RV4, RPD.STRCTURE.PCC2.Btn.RV4OpenSwitch, RPD.STRCTURE.PCC2.Btn.RV4CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.GVStatus(cPCC_GV6, RPD.STRCTURE.PCC2.Btn.GV6OpenSwitch, RPD.STRCTURE.PCC2.Btn.GV6CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.VPStatus(cPCCDP, RPD.STRCTURE.PCC2.Btn.TSSBPDPRunSwitch, RPD.STRCTURE.PCC2.Btn.TSSBPDPStopSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.TPStatus(cPCC_TP, RPD.STRCTURE.PCC2.Btn.TSSTPRunSwitch, RPD.STRCTURE.PCC2.Btn.TSSTPStopSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_PCCEV4, RPD.STRCTURE.PCC2.Btn.EV4OpenSwitch, RPD.STRCTURE.PCC2.Btn.EV4CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.PVStatus(pv_TSSVMV4, RPD.STRCTURE.PCC2.Btn.VMV4OpenSwitch, RPD.STRCTURE.PCC2.Btn.VMV4CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.GVStatus(gv_VV4, RPD.STRCTURE.PCC2.Btn.VV4OpenSwitch, RPD.STRCTURE.PCC2.Btn.VV4CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.GVStatus(cPCC_GV4, RPD.STRCTURE.PCC2.Btn.GV4OpenSwitch, RPD.STRCTURE.PCC2.Btn.GV4CloseSwitch, _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);

            if (RPD.STRCTURE.PCC1.Map.TS1OpenLamp.Equals(1) && RPD.STRCTURE.PCC1.Map.TS1CloseLamp.Equals(0))
                pcc1.TS1Open = 1;
            else
                pcc1.TS1Open = 0;

            if (RPD.STRCTURE.PCC1.Map.PB1OpenLamp.Equals(1) && RPD.STRCTURE.PCC1.Map.PB1CloseLamp.Equals(0))
                pcc1.PB1States = 1;
            else
                pcc1.PB1States = 0;

            if (RPD.STRCTURE.SYS.DI.RPDDoorInwallCoolingWaterFlowOK.Lamp.Equals(1))
                pcc1.bCPCW = true;
            else
                pcc1.bCPCW = false;

            if (RPD.STRCTURE.SYS.DI.RPDG1ElectrodeCoolingWaterFlowOK.Lamp.Equals(1))
                pcc1.bG1W = true;
            else
                pcc1.bG1W = false;

            if (RPD.STRCTURE.SYS.DI.RPDG2ElectrodeCoolingWaterFlowOK.Lamp.Equals(1))
                pcc1.bG2W = true;
            else
                pcc1.bG2W = false;

            if (RPD.STRCTURE.SYS.DI.RPDSteeringCoilCoolingWaterFlowOK.Lamp.Equals(1))
                pcc1.bSTCW = true;
            else
                pcc1.bSTCW = false;

            if (RPD.STRCTURE.SYS.DI.RPDMainHearthCoolingWaterFlowOK.Lamp.Equals(1))
                pcc1.bHCW = true;
            else
                pcc1.bHCW = false;

            if (RPD.STRCTURE.SYS.DI.RPDBeamGuideCoolingWaterFlowOK.Lamp.Equals(1))
                pcc1.bBGCW = true;
            else
                pcc1.bBGCW = false;

            if (RPD.STRCTURE.SYS.DI.RPDChamberInwallCoolingWaterFlowOK.Lamp.Equals(1))
                pcc1.bCICW = true;
            else
                pcc1.bCICW = false;

            if (RPD.STRCTURE.SYS.DI.TRatATM.Lamp.Equals(1)) //TR在大氣 相對的PCC1也會在大氣 故抓取此點
                pcc1.IsVac = false;
            else
                pcc1.IsVac = true;

            if (RPD.STRCTURE.PCC1.Data.PCC_TABLE != null)
            {
                pcc1.RStatus = "";
                DataTable dtPCC = clsDBN.GetTable(@"SELECT * FROM PCC_TABLE WHERE ARRAY_COUNT = 522 AND ARRAY_INDEX in (0,4,5,7,9)");
                foreach (DataRow drPCC in dtPCC.Rows)
                {
                    if ((RPD.STRCTURE.PCC1.Data.PCC_TABLE[(int)drPCC["ARRAY_COUNT"]] & ((int)(Math.Pow(2, (int)drPCC["ARRAY_INDEX"])))) != 0)
                    {
                        pcc1.RStatus = drPCC["SIGNALNAME"].ToString();
                    }
                }
            }

            if (RPD.STRCTURE.SYS.DI.RPDSourceSupplyatATM.Lamp.Equals(1))
            {
                pcc2.IsATM = true;
                pcc2.IsVac = false;
            }
            else
            {
                pcc2.IsATM = false;
                pcc2.IsVac = true;
            }

            //顯示 SHI 累計放電時間
            txtMDAHour.Text = RPD.STRCTURE.PCC1.Data.RGunAccumulationTimeMainDischargehour.ToString();
            txtMDAMin.Text = RPD.STRCTURE.PCC1.Data.RGunAccumulationTimeMainDischargemin.ToString();

            //2016.07.20  add by william for  G1 body and cooling water Temperature sensor value display 
            //txtG1BodyTemp.Text = RPD.STRCTURE.PCC1.Map.G1BodyTempValue.ToString();
            //txtG1CWTemp.Text = RPD.STRCTURE.PCC1.Map.G1CWTempValue.ToString();

            #endregion

            #region DIO

            //clsTool.ButtonStatus(btn_ForceOutput, RPD.STRCTURE.SYS.Btn.OutputForceOnOffSwitch, 'A', _RegisterEvent, 1, RPD.STRCTURE.SYS.Data.Level); //Admin權限才可操作


            #endregion

            #region Water Sensor MFC

            txtLLPumpFlowValue.Text = RPD.STRCTURE.LL.Map.LLPumpFlowValue.ToString();
            txtLLTPPumpFlowValue.Text = RPD.STRCTURE.LL.Map.LL_TP_PumpFlowValue.ToString();
            txtBackingFlowValue.Text = RPD.STRCTURE.LL.Map.BackingFlowValue.ToString();
            txtTRTPPumpFlowValue.Text = RPD.STRCTURE.TR.Map.TR_TP_PumpFlowValue.ToString();
            txtULTPPumpFlowValue.Text = RPD.STRCTURE.UL.Map.ULTPPumpFlowValue.ToString();
            txtULPumpFlowValue.Text = RPD.STRCTURE.UL.Map.ULPumpFlowValue.ToString();

            cTR.TR2CPWV = RPD.STRCTURE.TR.Map.TR2_CoolerFlowValue.ToString();
            cTR.TR2CWV = RPD.STRCTURE.TR.Map.TR2_CHB_COVERFlowValue.ToString();
            cTR.TR3CWV = RPD.STRCTURE.TR.Map.TR3_CHB_COVERFlowValue.ToString();

            pcc1.sG1WV = RPD.STRCTURE.PCC1.Map.PlasmaGeneratorG1FlowValue.ToString();
            pcc1.sG2WV = RPD.STRCTURE.PCC1.Map.PlasmaGeneratorG2FlowValue.ToString();
            pcc1.sSCWV = RPD.STRCTURE.PCC1.Map.PlasmaGeneratorSteeringCoilFlowValue.ToString();

            pcc1.sBGCWV = RPD.STRCTURE.PCC1.Map.HearthBeamFlowValue.ToString();
            pcc1.sMHWV = RPD.STRCTURE.PCC1.Map.HearthMainFlowValue.ToString();
            pcc1.sCPWV = RPD.STRCTURE.PCC1.Map.DoorInWallFlowValue.ToString();
            pcc1.sCBHWV = RPD.STRCTURE.PCC1.Map.ChamberInWallFlowValue.ToString();


            #endregion

            #region Gauge

            cLL.PreVal = clsTool.DisplayGauge(RPD.STRCTURE.LL.Map.LLGaugeValuePH);
            txtLLTP_EV1_GaugeValueP.Text = clsTool.DisplayGauge(RPD.STRCTURE.SYS.Map.LLTP_EV1_GaugeValueP);
            txtDP_EV1_GaugeValueP.Text = clsTool.DisplayGauge(RPD.STRCTURE.SYS.Map.DP_EV1_GaugeValueP);

            cTR.PreVal = clsTool.DisplayGauge(RPD.STRCTURE.TR.Map.TRGaugeValuePH);
            cTR.PreVal_C = clsTool.DisplayGauge(RPD.STRCTURE.TR.Map.TRGaugeValueC);

            //Add by Henry 2016/1/26 新增TR C gauge 未加Offset值
            //txtTR_GaugeValueC_NoOffset.Text = clsTool.DisplayGauge(RPD.STRCTURE.TR.Map.TRGaugeValueC_NoOffset);

            txtTR_TP_EV3_GaugeValueP.Text = clsTool.DisplayGauge(RPD.STRCTURE.SYS.Map.TR_TP_EV3_GaugeValueP);

            cUL.PreVal = clsTool.DisplayGauge(RPD.STRCTURE.UL.Map.ULGaugeValuePH);
            txtUL_TP_EV2_GaugeValueP.Text = clsTool.DisplayGauge(RPD.STRCTURE.SYS.Map.UL_TP_EV2_GaugeValueP);

            pcc2.PreVal = clsTool.DisplayGauge(RPD.STRCTURE.PCC2.Map.TSS_GaugeValueP);
            //pcc2.PreVal = clsTool.DisplayGauge(0.0000013);
            txtTSSTPEV4GaugeValueP.Text = clsTool.DisplayGauge(RPD.STRCTURE.PCC2.Map.TSS_TP_EV4_GaugeValueP);
            txtDPRV4GaugeValueP.Text = clsTool.DisplayGauge(RPD.STRCTURE.PCC2.Map.TSS_DP_EV4_GaugeValueP);


            #endregion

            #region MFC
            //MFC 狀態點lamp
            cMFCLLO2.MFCstatus = RPD.STRCTURE.LL.Btn.LLMFCO2Switch.Lamp.ToString();
            cMFCLLAr.MFCstatus = RPD.STRCTURE.LL.Btn.LLMFCArSwitch.Lamp.ToString();

            cMFCTRAr_Gun.MFCstatus = RPD.STRCTURE.TR.Btn.TRMFCAr_GunSwitch.Lamp.ToString();
            cMFCTRO2.MFCstatus = RPD.STRCTURE.TR.Btn.TRMFCO2Switch.Lamp.ToString();
            cMFCTRAr_Chamber.MFCstatus = RPD.STRCTURE.TR.Btn.TRMFCAr_ChamberSwitch.Lamp.ToString();
            cMFCTRH2O.MFCstatus = RPD.STRCTURE.TR.Btn.TRMFCH2OSwitch.Lamp.ToString();

            cMFCULO2.MFCstatus = RPD.STRCTURE.UL.Btn.ULMFCO2Switch.Lamp.ToString();
            cMFCULAr.MFCstatus = RPD.STRCTURE.UL.Btn.ULMFCArSwitch.Lamp.ToString();

            //MFC 設定值
            cMFCLLO2.MFCValue = RPD.STRCTURE.LL.Map.LLMFCO2Set.ToString();
            cMFCLLAr.MFCValue = RPD.STRCTURE.LL.Map.LLMFCArSet.ToString();
            
            cMFCTRAr_Chamber.MFCValue = RPD.STRCTURE.TR.Map.TRMFCAr_ChamberSet.ToString();
            cMFCTRO2.MFCValue = RPD.STRCTURE.TR.Map.TRMFCO2Set.ToString();
            cMFCTRAr_Gun.MFCValue = RPD.STRCTURE.TR.Map.TRMFCAr_GunSet.ToString();
            cMFCTRH2O.MFCValue = RPD.STRCTURE.TR.Map.TRMFCH2OSet.ToString();

            cMFCULO2.MFCValue = RPD.STRCTURE.UL.Map.ULMFCO2Set.ToString();
            cMFCULAr.MFCValue = RPD.STRCTURE.UL.Map.ULMFCArSet.ToString();

            //MFC 現在值
            txtLLMFCO2_Flow.Text = RPD.STRCTURE.LL.Map.LLMFCO2Flow > 100 ? "100" : RPD.STRCTURE.LL.Map.LLMFCO2Flow.ToString();
            txtLLMFCAr_Flow.Text = RPD.STRCTURE.LL.Map.LLMFCArFlow > 200 ? "200" : RPD.STRCTURE.LL.Map.LLMFCArFlow.ToString();

            txtTRMFCAr_GunFlow.Text = RPD.STRCTURE.TR.Map.TRMFCAr_Gun_Flow > 100 ? "100" : RPD.STRCTURE.TR.Map.TRMFCAr_Gun_Flow.ToString();
            txtTRMFCO2_Flow.Text = RPD.STRCTURE.TR.Map.TRMFCO2Flow > 100 ? "100" : RPD.STRCTURE.TR.Map.TRMFCO2Flow.ToString();
            txtTRMFCAr_ChamberFlow.Text = RPD.STRCTURE.TR.Map.TRMFCAr_Chamber_Flow > 500 ? "500" : RPD.STRCTURE.TR.Map.TRMFCAr_Chamber_Flow.ToString();
            txtTRMFCXFlow.Text = RPD.STRCTURE.TR.Map.TRMFCH2OFlow > 200 ? "200" : RPD.STRCTURE.TR.Map.TRMFCH2OFlow.ToString();

            txtULMFCO2_Flow.Text = RPD.STRCTURE.UL.Map.ULMFCO2Flow > 100 ? "100" : RPD.STRCTURE.UL.Map.ULMFCO2Flow.ToString();
            txtULMFCAr_Flow.Text = RPD.STRCTURE.UL.Map.ULMFCArFlow > 200 ? "200" : RPD.STRCTURE.UL.Map.ULMFCArFlow.ToString();

            #endregion

            #region TP Status Des

            lblTP_LL.Text = clsTool.DisplayTPStatus(RPD.STRCTURE.LL.Map.TP_LL_Status);
            lblTP_TR.Text = clsTool.DisplayTPStatus(RPD.STRCTURE.TR.Map.TP_TR_Status);
            lblTP_UL.Text = clsTool.DisplayTPStatus(RPD.STRCTURE.UL.Map.TP_UL_Status);
            lblTP_TSS.Text = clsTool.DisplayTPStatus(RPD.STRCTURE.PCC2.Map.TP_TSS_Status);

            #endregion

            #region CV Speed 

            txtEFCVSpeedValue.Text = RPD.STRCTURE.EF.Map.EFCVSpeed.ToString();
            txtLLCVSpeedValue.Text = RPD.STRCTURE.LL.Map.LLCVSpeed.ToString();
            txtTR1CVSpeedValue.Text = RPD.STRCTURE.TR.Map.TR1CVSpeed.ToString();
            txtTR2CVSpeedValue.Text = RPD.STRCTURE.TR.Map.TR2CVSpeed.ToString();
            txtTR3CVSpeedValue.Text = RPD.STRCTURE.TR.Map.TR3CVSpeed.ToString();
            txtULCVSpeedValue.Text = RPD.STRCTURE.UL.Map.ULCVSpeed.ToString();
            txtDFCVSpeedValue.Text = RPD.STRCTURE.DF.Map.DFCVSpeed.ToString();

            #endregion

            #region water tank lamp
            if (RPD.STRCTURE.SYS.DI.SPARE13.Lamp.Equals(1))
                os_H2OTankUpperLimit.FillColor = Color.Lime;
            else
                os_H2OTankUpperLimit.FillColor = Color.Red;

            if (RPD.STRCTURE.SYS.DI.SPARE14.Lamp.Equals(1))
                os_H2OTankLowerLimit.FillColor = Color.Lime;
            else
                os_H2OTankLowerLimit.FillColor = Color.Red;
            #endregion

            #region Heater Temp
            label352.Text = RPD.STRCTURE.TR.Map.W_1_Heater_Temp.ToString();
            label354.Text = RPD.STRCTURE.TR.Map.W_2_Heater_Temp.ToString();
            label357.Text = RPD.STRCTURE.TR.Map.W_3_Heater_Temp.ToString();
            #endregion

            _RegisterEvent = true;


            #region DI/O

            if (_IsRefeshDIO)
            {
                try
                {
                    foreach (DataRow drDI in _dtDI.Rows)
                    {
                        if (RPD.STRCTURE.SYS.Data.IO_TABLE != null)
                        {
                            if ((RPD.STRCTURE.SYS.Data.IO_TABLE[(int)drDI["ARRAY_COUNT"]] & ((int)(Math.Pow(2, (int)drDI["ARRAY_INDEX"])))) != 0)
                            {
                                Control[] a = this.Controls.Find("shMain_" + drDI["SOFTADDRESS"].ToString(), true);
                                ShapeContainer shapeContainer = a[0] as ShapeContainer;
                                OvalShape light = shapeContainer.Shapes.OfType<OvalShape>().FirstOrDefault(o => o.Name == "Lamp_" + drDI["SOFTADDRESS"].ToString());
                                //this.Invoke(new EventHandler(Update_OvalShapeToLime), light);
                                light.FillColor = Color.Lime;
                                light.FillGradientColor = Color.GreenYellow;
                            }
                            else
                            {
                                Control[] a = this.Controls.Find("shMain_" + drDI["SOFTADDRESS"].ToString(), true);
                                ShapeContainer shapeContainer = a[0] as ShapeContainer;
                                OvalShape light = shapeContainer.Shapes.OfType<OvalShape>().FirstOrDefault(o => o.Name == "Lamp_" + drDI["SOFTADDRESS"].ToString());
                                //this.Invoke(new EventHandler(Update_OvalShapeToRed), light);
                                light.FillColor = Color.Red;
                                light.FillGradientColor = Color.DarkSalmon;
                            }
                        }
                    }

                    foreach (DataRow drDO in _dtDO.Rows)
                    {
                        if (RPD.STRCTURE.SYS.Data.IO_TABLE != null)
                        {
                            if ((RPD.STRCTURE.SYS.Data.IO_TABLE[(int)drDO["ARRAY_COUNT"]] & ((int)(Math.Pow(2, (int)drDO["ARRAY_INDEX"])))) != 0)
                            {
                                Control[] a = this.Controls.Find("shMain_" + drDO["SOFTADDRESS"].ToString(), true);
                                ShapeContainer shapeContainer = a[0] as ShapeContainer;
                                OvalShape light = shapeContainer.Shapes.OfType<OvalShape>().FirstOrDefault(o => o.Name == "Lamp_" + drDO["WRITEADDRESS"].ToString());

                                if ((RPD.STRCTURE.SYS.Data.INTERLOCK_TABLE[(int)drDO["ARRAY_COUNT"]] & ((int)(Math.Pow(2, (int)drDO["ARRAY_INDEX"])))) != 0)
                                    light.Tag = "INTERLOCK:1";
                                else
                                    light.Tag = "INTERLOCK:0";

                                //this.Invoke(new EventHandler(Update_OvalShapeToLime), light);
                                light.FillColor = Color.Lime;
                                light.FillGradientColor = Color.GreenYellow;
                            }
                            else
                            {
                                Control[] a = this.Controls.Find("shMain_" + drDO["SOFTADDRESS"].ToString(), true);
                                ShapeContainer shapeContainer = a[0] as ShapeContainer;
                                OvalShape light = shapeContainer.Shapes.OfType<OvalShape>().FirstOrDefault(o => o.Name == "Lamp_" + drDO["WRITEADDRESS"].ToString());

                                if ((RPD.STRCTURE.SYS.Data.INTERLOCK_TABLE[(int)drDO["ARRAY_COUNT"]] & ((int)(Math.Pow(2, (int)drDO["ARRAY_INDEX"])))) != 0)
                                    light.Tag = "INTERLOCK:1";
                                else
                                    light.Tag = "INTERLOCK:0";
                                                                
                                //this.Invoke(new EventHandler(Update_OvalShapeToRed), light);
                                light.FillColor = Color.Red;
                                light.FillGradientColor = Color.DarkSalmon;

                            }
                        }
                    }

                }
                catch (Exception Ex)
                {
                    StreamWriter sw = new StreamWriter(MyPath + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\r\n" + Ex.ToString());
                    sw.Close();
                }
                finally
                {

                }

            }


            #endregion
            
            Application.DoEvents();

        }

        #region EF Btn Event
        private void EF_CV_Btn_click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Name.ToUpper())
            {
                case "BTN_LEFT":
                    if (RPD.STRCTURE.EF.Btn.LDPortCVReverseSwitch.Interlock.Equals(1))
                        clsPLC.WriPLC_Pulse(RPD.STRCTURE.EF.Btn.LDPortCVReverseSwitch.Address);
                    else
                    {
                        string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.EF.Btn.LDPortCVReverseSwitch.Address);
                        MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "BTN_RIGHT":
                    if (RPD.STRCTURE.EF.Btn.LDPortCVForwardSwitch.Interlock.Equals(1))
                        clsPLC.WriPLC_Pulse(RPD.STRCTURE.EF.Btn.LDPortCVForwardSwitch.Address);
                    else
                    {
                        string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.EF.Btn.LDPortCVForwardSwitch.Address);
                        MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "BTN_CLOSE":
                    if (RPD.STRCTURE.EF.Btn.LDPortCVStopSwitch.Interlock.Equals(1))
                        clsPLC.WriPLC_Pulse(RPD.STRCTURE.EF.Btn.LDPortCVStopSwitch.Address);
                    else
                    {
                        string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.EF.Btn.LDPortCVStopSwitch.Address);
                        MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
            }
        }
        private void DF_CV_Btn_click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Name.ToUpper())
            {
                case "BTN_LEFT":
                    if (RPD.STRCTURE.DF.Btn.UNLDPortCVReverseSwitch.Interlock.Equals(1))
                        clsPLC.WriPLC_Pulse(RPD.STRCTURE.DF.Btn.UNLDPortCVReverseSwitch.Address);
                    else
                    {
                        string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.DF.Btn.UNLDPortCVReverseSwitch.Address);
                        MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "BTN_RIGHT":
                    if (RPD.STRCTURE.DF.Btn.UNLDPortCVForwardSwitch.Interlock.Equals(1))
                        clsPLC.WriPLC_Pulse(RPD.STRCTURE.DF.Btn.UNLDPortCVForwardSwitch.Address);
                    else
                    {
                        string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.DF.Btn.UNLDPortCVForwardSwitch.Address);
                        MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "BTN_CLOSE":
                    if (RPD.STRCTURE.DF.Btn.UNLDPortCVStopSwitch.Interlock.Equals(1))
                        clsPLC.WriPLC_Pulse(RPD.STRCTURE.DF.Btn.UNLDPortCVStopSwitch.Address);
                    else
                    {
                        string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.DF.Btn.UNLDPortCVStopSwitch.Address);
                        MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;

            }
        }

        #endregion

        #region LL Btn Event
        private void LL_CV_Btn_click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Name.ToUpper())
            {
                case "BTN_LEFT":
                    if (RPD.STRCTURE.LL.Btn.LLCVReverseSwitch.Interlock.Equals(1))
                        clsPLC.WriPLC_Pulse(RPD.STRCTURE.LL.Btn.LLCVReverseSwitch.Address);
                    else
                    {
                        string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.LL.Btn.LLCVReverseSwitch.Address);
                        MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "BTN_RIGHT":
                    if (RPD.STRCTURE.LL.Btn.LLCVForwardSwitch.Interlock.Equals(1))
                        clsPLC.WriPLC_Pulse(RPD.STRCTURE.LL.Btn.LLCVForwardSwitch.Address);
                    else
                    {
                        string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.LL.Btn.LLCVForwardSwitch.Address);
                        MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "BTN_CLOSE":
                    if (RPD.STRCTURE.LL.Btn.LLCVStopSwitch.Interlock.Equals(1))
                        clsPLC.WriPLC_Pulse(RPD.STRCTURE.LL.Btn.LLCVStopSwitch.Address);
                    else
                    {
                        string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.LL.Btn.LLCVStopSwitch.Address);
                        MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
            }

        }
        private void LL_Con_Btn_click(object sender, EventArgs e)
        {
            RectangleShape rs = (RectangleShape)sender;
            switch (rs.Name.ToUpper())
            {
                case "RSUPBTN1":
                    if (RPD.STRCTURE.LL.Btn.DV1OpenSwitch.Interlock.Equals(1))
                        clsPLC.WriPLC_Pulse(RPD.STRCTURE.LL.Btn.DV1OpenSwitch.Address);
                    else
                    {
                        string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.LL.Btn.DV1OpenSwitch.Address);
                        MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "RSDOWNBTN1":
                    if (RPD.STRCTURE.LL.Btn.DV1CloseSwitch.Interlock.Equals(1))
                        clsPLC.WriPLC_Pulse(RPD.STRCTURE.LL.Btn.DV1CloseSwitch.Address);
                    else
                    {
                        string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.LL.Btn.DV1CloseSwitch.Address);
                        MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
            }

        }
        private void LL_Auto_Btn_click(object sender, EventArgs e)
        {

        }
        #endregion

        #region TR Btn Event

        private void TR_CV_Btn_click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Tag.ToString())
            {
                case "TR1":
                    switch (btn.Name.ToUpper())
                    {
                        case "BTN_TR1_LEFT":
                            if (RPD.STRCTURE.TR.Btn.TR1CVReverseSwitch.Interlock.Equals(1))
                                clsPLC.WriPLC_Pulse(RPD.STRCTURE.TR.Btn.TR1CVReverseSwitch.Address);
                            else
                            {
                                string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.TR.Btn.TR1CVReverseSwitch.Address);
                                MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            break;
                        case "BTN_TR1_RIGHT":
                            if (RPD.STRCTURE.TR.Btn.TR1CVForwardSwitch.Interlock.Equals(1))
                                clsPLC.WriPLC_Pulse(RPD.STRCTURE.TR.Btn.TR1CVForwardSwitch.Address);
                            else
                            {
                                string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.TR.Btn.TR1CVForwardSwitch.Address);
                                MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            break;
                        case "BTN_TR1_STOP":
                            if (RPD.STRCTURE.TR.Btn.TR1CVStopSwitch.Interlock.Equals(1))
                                clsPLC.WriPLC_Pulse(RPD.STRCTURE.TR.Btn.TR1CVStopSwitch.Address);
                            else
                            {
                                string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.TR.Btn.TR1CVStopSwitch.Address);
                                MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            break;
                    }
                    break;
                case "TR2":
                    switch (btn.Name.ToUpper())
                    {
                        case "BTN_TR2_LEFT":
                            if (RPD.STRCTURE.TR.Btn.TR2CVReverseSwitch.Interlock.Equals(1))
                                clsPLC.WriPLC_Pulse(RPD.STRCTURE.TR.Btn.TR2CVReverseSwitch.Address);
                            else
                            {
                                string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.TR.Btn.TR2CVReverseSwitch.Address);
                                MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            break;
                        case "BTN_TR2_RIGHT":
                            if (RPD.STRCTURE.TR.Btn.TR2CVForwardSwitch.Interlock.Equals(1))
                                clsPLC.WriPLC_Pulse(RPD.STRCTURE.TR.Btn.TR2CVForwardSwitch.Address);
                            else
                            {
                                string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.TR.Btn.TR2CVForwardSwitch.Address);
                                MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            break;
                        case "BTN_TR2_STOP":
                            if (RPD.STRCTURE.TR.Btn.TR2CVStopSwitch.Interlock.Equals(1))
                                clsPLC.WriPLC_Pulse(RPD.STRCTURE.TR.Btn.TR2CVStopSwitch.Address);
                            else
                            {
                                string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.TR.Btn.TR2CVStopSwitch.Address);
                                MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            break;
                    }
                    break;
                case "TR3":
                    switch (btn.Name.ToUpper())
                    {
                        case "BTN_TR3_LEFT":
                            if (RPD.STRCTURE.TR.Btn.TR3CVReverseSwitch.Interlock.Equals(1))
                                clsPLC.WriPLC_Pulse(RPD.STRCTURE.TR.Btn.TR3CVReverseSwitch.Address);
                            else
                            {
                                string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.TR.Btn.TR3CVReverseSwitch.Address);
                                MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            break;
                        case "BTN_TR3_RIGHT":
                            if (RPD.STRCTURE.TR.Btn.TR3CVForwardSwitch.Interlock.Equals(1))
                                clsPLC.WriPLC_Pulse(RPD.STRCTURE.TR.Btn.TR3CVForwardSwitch.Address);
                            else
                            {
                                string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.TR.Btn.TR3CVForwardSwitch.Address);
                                MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            break;
                        case "BTN_TR3_STOP":
                            if (RPD.STRCTURE.TR.Btn.TR3CVStopSwitch.Interlock.Equals(1))
                                clsPLC.WriPLC_Pulse(RPD.STRCTURE.TR.Btn.TR3CVStopSwitch.Address);
                            else
                            {
                                string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.TR.Btn.TR3CVStopSwitch.Address);
                                MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            break;
                    }
                    break;
            }
        }

        private void TR_Con_Btn_click(object sender, EventArgs e)
        {
            RectangleShape rs = (RectangleShape)sender;
            switch (rs.Name.ToUpper())
            {
                case "RSUPBTN1":
                    if (RPD.STRCTURE.TR.Btn.DV2OpenSwitch.Interlock.Equals(1))
                        clsPLC.WriPLC_Pulse(RPD.STRCTURE.TR.Btn.DV2OpenSwitch.Address);
                    else
                    {
                        string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.TR.Btn.DV2OpenSwitch.Address);
                        MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "RSDOWNBTN1":
                    if (RPD.STRCTURE.TR.Btn.DV2CloseSwitch.Interlock.Equals(1))
                        clsPLC.WriPLC_Pulse(RPD.STRCTURE.TR.Btn.DV2CloseSwitch.Address);
                    else
                    {
                        string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.TR.Btn.DV2CloseSwitch.Address);
                        MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "RSUPBTN2":
                    if (RPD.STRCTURE.TR.Btn.DV3OpenSwitch.Interlock.Equals(1))
                        clsPLC.WriPLC_Pulse(RPD.STRCTURE.TR.Btn.DV3OpenSwitch.Address);
                    else
                    {
                        string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.TR.Btn.DV3OpenSwitch.Address);
                        MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "RSDOWNBTN2":
                    if (RPD.STRCTURE.TR.Btn.DV3CloseSwitch.Interlock.Equals(1))
                        clsPLC.WriPLC_Pulse(RPD.STRCTURE.TR.Btn.DV3CloseSwitch.Address);
                    else
                    {
                        string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.TR.Btn.DV3CloseSwitch.Address);
                        MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
            }

        }

        private void TR_Auto_Btn_click(object sender, EventArgs e)
        {

        }

        #endregion

        #region UL Btn Event
        private void UL_CV_Btn_click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Name.ToUpper())
            {
                case "BTN_LEFT":
                    if (RPD.STRCTURE.UL.Btn.ULCVReverseSwitch.Interlock.Equals(1))
                        clsPLC.WriPLC_Pulse(RPD.STRCTURE.UL.Btn.ULCVReverseSwitch.Address);
                    else
                    {
                        string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.UL.Btn.ULCVReverseSwitch.Address);
                        MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "BTN_RIGHT":
                    if (RPD.STRCTURE.UL.Btn.ULCVForwardSwitch.Interlock.Equals(1))
                        clsPLC.WriPLC_Pulse(RPD.STRCTURE.UL.Btn.ULCVForwardSwitch.Address);
                    else
                    {
                        string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.UL.Btn.ULCVForwardSwitch.Address);
                        MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "BTN_CLOSE":
                    if (RPD.STRCTURE.UL.Btn.ULCVStopSwitch.Interlock.Equals(1))
                        clsPLC.WriPLC_Pulse(RPD.STRCTURE.UL.Btn.ULCVStopSwitch.Address);
                    else
                    {
                        string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.UL.Btn.ULCVStopSwitch.Address);
                        MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
            }

        }
        private void UL_Con_Btn_click(object sender, EventArgs e)
        {
            RectangleShape rs = (RectangleShape)sender;
            switch (rs.Name.ToUpper())
            {
                case "RSUPBTN2":
                    if (RPD.STRCTURE.UL.Btn.DV4OpenSwitch.Interlock.Equals(1))
                        clsPLC.WriPLC_Pulse(RPD.STRCTURE.UL.Btn.DV4OpenSwitch.Address);
                    else
                    {
                        string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.UL.Btn.DV4OpenSwitch.Address);
                        MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "RSDOWNBTN2":
                    if (RPD.STRCTURE.UL.Btn.DV4CloseSwitch.Interlock.Equals(1))
                        clsPLC.WriPLC_Pulse(RPD.STRCTURE.UL.Btn.DV4CloseSwitch.Address);
                    else
                    {
                        string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.UL.Btn.DV4CloseSwitch.Address);
                        MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
            }

        }
        private void UL_Auto_Btn_click(object sender, EventArgs e)
        {

        }


        #endregion

        #region MFC Btn Event
        //LL
        private void LLMFC_Con_Btn_click(object sender, EventArgs e)
        {
            string chMFCName;

            if (sender.GetType().Name == "Button")
            {
                chMFCName = ((Button)sender).Tag.ToString();
            }
            else
            {
                chMFCName = ((MFC.MFC)sender).Tag.ToString();
            }

            if (!cLL.AutoMode)
            {
                switch (chMFCName)
                {
                    case "O2":
                        try
                        {
                            int T_Value = Convert.ToInt16(cMFCLLO2.MFCValue);
                            if (T_Value < 0 || T_Value > 100)
                            {
                                MessageBox.Show("LL MFC O2 Out Of Range", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                            else
                            {
                                if (RPD.STRCTURE.LL.Btn.LLMFCO2Switch.Interlock.Equals(1))
                                {
                                    clsPLC.WriPLC_Word("D35032", (T_Value).ToString());
                                    clsPLC.WriPLC_Pulse(RPD.STRCTURE.LL.Btn.LLMFCO2Switch.Address);
                                }
                                else
                                {
                                    string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.LL.Btn.LLMFCO2Switch.Address);
                                    MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch
                        {
                            break;
                        }
                        break;
                    case "Ar":
                        try
                        {
                            int T_Value = Convert.ToInt16(cMFCLLAr.MFCValue);
                            if (T_Value < 0 || T_Value > 200)
                            {
                                MessageBox.Show("LL MFC Ar Out Of Range", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                            else
                            {
                                if (RPD.STRCTURE.LL.Btn.LLMFCArSwitch.Interlock.Equals(1))
                                {
                                    clsPLC.WriPLC_Word("D35033", (T_Value).ToString());
                                    clsPLC.WriPLC_Pulse(RPD.STRCTURE.LL.Btn.LLMFCArSwitch.Address);
                                }
                                else
                                {
                                    string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.LL.Btn.LLMFCArSwitch.Address);
                                    MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch
                        {
                            break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        //UL
        private void ULMFC_Con_Btn_click(object sender, EventArgs e)
        {
            string chMFCName;

            if (sender.GetType().Name == "Button")
            {
                chMFCName = ((Button)sender).Tag.ToString();
            }
            else
            {
                chMFCName = ((MFC.MFC)sender).Tag.ToString();
            }

            if (!cUL.AutoMode)
            {
                switch (chMFCName)
                {
                    case "O2":
                        try
                        {
                            int T_Value = Convert.ToInt16(cMFCULO2.MFCValue);
                            if (T_Value < 0 || T_Value > 100)
                            {
                                MessageBox.Show("UL MFC O2 Out Of Range", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                            else
                            {
                                if (RPD.STRCTURE.UL.Btn.ULMFCO2Switch.Interlock.Equals(1))
                                {
                                    clsPLC.WriPLC_Word("D35034", (T_Value).ToString());
                                    clsPLC.WriPLC_Pulse(RPD.STRCTURE.UL.Btn.ULMFCO2Switch.Address);
                                }
                                else
                                {
                                    string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.UL.Btn.ULMFCO2Switch.Address);
                                    MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch
                        {
                            break;
                        }
                        break;
                    case "Ar":
                        try
                        {
                            int T_Value = Convert.ToInt16(cMFCULAr.MFCValue);
                            if (T_Value < 0 || T_Value > 200)
                            {
                                MessageBox.Show("UL MFC Ar Out Of Range", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                            else
                            {
                                if (RPD.STRCTURE.UL.Btn.ULMFCArSwitch.Interlock.Equals(1))
                                {
                                    clsPLC.WriPLC_Word("D35035", (T_Value).ToString());
                                    clsPLC.WriPLC_Pulse(RPD.STRCTURE.UL.Btn.ULMFCArSwitch.Address);
                                }
                                else
                                {
                                    string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.UL.Btn.ULMFCArSwitch.Address);
                                    MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch
                        {
                            break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        //TR
        private void TRMFC_Con_Btn_click(object sender, EventArgs e)
        {
            string chMFCName;

            if (sender.GetType().Name == "Button")
            {
                chMFCName = ((Button)sender).Tag.ToString();
            }
            else
            {
                chMFCName = ((MFC.MFC)sender).Tag.ToString();
            }

            if (!cTR.AutoMode)
            {
                switch (chMFCName)
                {

                    case "ArC":
                        try
                        {
                            int T_Value = Convert.ToInt16(cMFCTRAr_Chamber.MFCValue);
                            if (T_Value < 0 || T_Value > 500)
                            {
                                MessageBox.Show("TR MFC Ar Chamber Out Of Range", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                            else
                            {
                                if (RPD.STRCTURE.TR.Btn.TRMFCAr_ChamberSwitch.Interlock.Equals(1))
                                {
                                    clsPLC.WriPLC_Word("D35038", cMFCTRAr_Chamber.MFCValue);
                                    clsPLC.WriPLC_Pulse(RPD.STRCTURE.TR.Btn.TRMFCAr_ChamberSwitch.Address);
                                }
                                else
                                {
                                    string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.TR.Btn.TRMFCAr_ChamberSwitch.Address);
                                    MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch
                        {
                            break;
                        }
                        break;
                    case "O2":
                        try
                        {
                            int T_Value = Convert.ToInt16(cMFCTRO2.MFCValue);
                            if (T_Value < 0 || T_Value > 100)
                            {
                                MessageBox.Show("TR MFC O2 Out Of Range", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                            else
                            {
                                if (RPD.STRCTURE.TR.Btn.TRMFCO2Switch.Interlock.Equals(1))
                                {
                                    clsPLC.WriPLC_Word("D35037", cMFCTRO2.MFCValue);
                                    clsPLC.WriPLC_Pulse(RPD.STRCTURE.TR.Btn.TRMFCO2Switch.Address);
                                }
                                else
                                {
                                    string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.TR.Btn.TRMFCO2Switch.Address);
                                    MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch
                        {
                            break;
                        }
                        break;
                    case "ArG":
                        try
                        {
                            int T_Value = Convert.ToInt16(cMFCTRAr_Gun.MFCValue);
                            if (T_Value < 0 || T_Value > 100)
                            {
                                MessageBox.Show("TR MFC Ar Gun Out Of Range", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                            else
                            {
                                if (RPD.STRCTURE.TR.Btn.TRMFCAr_GunSwitch.Interlock.Equals(1))
                                {
                                    clsPLC.WriPLC_Word("D35036", cMFCTRAr_Gun.MFCValue);
                                    clsPLC.WriPLC_Pulse(RPD.STRCTURE.TR.Btn.TRMFCAr_GunSwitch.Address);
                                }
                                else
                                {
                                    string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.TR.Btn.TRMFCAr_GunSwitch.Address);
                                    MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch
                        {
                            break;
                        }
                        break;
                    case "H2O":
                        try
                        {
                            int T_Value = Convert.ToInt16(cMFCTRH2O.MFCValue);
                            if (T_Value < 0 || T_Value > 20)
                            {
                                MessageBox.Show("TR MFC H2O Out Of Range", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                            else
                            {
                                if (RPD.STRCTURE.TR.Btn.TRMFCH2OSwitch.Interlock.Equals(1))
                                {
                                    clsPLC.WriPLC_Word("D35041", cMFCTRH2O.MFCValue);
                                    clsPLC.WriPLC_Pulse(RPD.STRCTURE.TR.Btn.TRMFCH2OSwitch.Address);
                                }
                                else
                                {
                                    string Msg = clsTool.GetInterLockMsg(RPD.STRCTURE.TR.Btn.TRMFCH2OSwitch.Address);
                                    MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch
                        {
                            break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion


        #region ---------------- DI&DO Page ------------------------
        private void Update_OvalShapeToRed(object sender, EventArgs e)
        {
            OvalShape Lamp = (OvalShape)sender;
            Lamp.FillColor = Color.Red;
            Lamp.FillGradientColor = Color.DarkSalmon;
            Application.DoEvents();
        }
        private void Update_OvalShapeToLime(object sender, EventArgs e)
        {
            OvalShape Lamp = (OvalShape)sender;
            Lamp.FillColor = Color.Lime;
            Lamp.FillGradientColor = Color.GreenYellow;
            Application.DoEvents();
        }

        private void InitialDI()
        {
            _SQL = @"SELECT * FROM IO_TABLE WHERE DES <> '' AND DES <> 'NA' AND DES <> 'Spare' AND TYPE = 'DI'";
            _dtDI = clsDBN.GetTable(_SQL);

            int X = 15;
            int Y = 15;
            int Count = 0;
            foreach (DataRow drDI in _dtDI.Rows)
            {
                if (Count % 25 == 0 && Count != 0)
                {
                    X += 350;
                    Y = 15;
                }

                ShapeContainer shMain = new ShapeContainer();
                shMain.Name = "shMain_" + drDI["SOFTADDRESS"].ToString();
                OvalShape ovLamp = new OvalShape();
                ovLamp.Name = "Lamp_" + drDI["SOFTADDRESS"].ToString();
                ovLamp.BorderColor = Color.Black;
                ovLamp.FillColor = Color.Red;
                ovLamp.FillGradientColor = Color.DarkSalmon;
                ovLamp.FillGradientStyle = FillGradientStyle.ForwardDiagonal;
                ovLamp.FillStyle = FillStyle.Solid;
                ovLamp.Size = new System.Drawing.Size(18, 18);
                ovLamp.Location = new Point(X, Y);
                shMain.Shapes.Add(ovLamp);

                Label lblAddress = new Label();
                lblAddress.Name = "Address_" + drDI["SOFTADDRESS"].ToString();
                lblAddress.Text = drDI["HARDADDRESS"].ToString();
                lblAddress.Size = new System.Drawing.Size(50, 15);
                lblAddress.Location = new Point(X + 35, Y + 4);
                lblAddress.BackColor = Color.Yellow;

                Label lblDES = new Label();
                lblDES.Name = "Des_" + drDI["SOFTADDRESS"].ToString();
                lblDES.Text = drDI["DES"].ToString();
                lblDES.Font = new System.Drawing.Font("新細明體", 8, FontStyle.Bold);
                lblDES.Size = new System.Drawing.Size(250, 15);
                lblDES.Location = new Point(X + 100, Y + 4);

                tc_IOPage.TabPages["DI"].Controls.Add(shMain);
                tc_IOPage.TabPages["DI"].Controls.Add(lblAddress);
                tc_IOPage.TabPages["DI"].Controls.Add(lblDES);

                Count++;
                Y += 25;

            }
        }

        private void InitialDO()
        {
            _SQL = @"SELECT * FROM IO_TABLE WHERE DES <> '' AND DES <> 'NA' AND DES <> 'Spare' AND TYPE = 'DO'";

            _dtDO = clsDBN.GetTable(_SQL);

            int X = 15;
            int Y = 57;
            int Count = 0;
            foreach (DataRow drDO in _dtDO.Rows)
            {
                if (Count % 25 == 0 && Count != 0)
                {
                    X += 350;
                    Y = 57;
                }

                ShapeContainer shMain = new ShapeContainer();
                shMain.Name = "shMain_" + drDO["SOFTADDRESS"].ToString();
                OvalShape ovLamp = new OvalShape();
                ovLamp.Name = "Lamp_" + drDO["WRITEADDRESS"].ToString();
                ovLamp.BorderColor = Color.Black;
                ovLamp.FillColor = Color.Red;
                ovLamp.FillGradientColor = Color.DarkSalmon;
                ovLamp.FillGradientStyle = FillGradientStyle.ForwardDiagonal;
                ovLamp.FillStyle = FillStyle.Solid;
                ovLamp.Size = new System.Drawing.Size(18, 18);
                ovLamp.Location = new Point(X, Y);
                ovLamp.Click += new EventHandler(ovLamp_Click);
                shMain.Shapes.Add(ovLamp);

                Label lblAddress = new Label();
                lblAddress.Name = "Address_" + drDO["SOFTADDRESS"].ToString();
                lblAddress.Text = drDO["HARDADDRESS"].ToString();
                lblAddress.Size = new System.Drawing.Size(50, 15);
                lblAddress.Location = new Point(X + 35, Y + 4);
                lblAddress.BackColor = Color.Yellow;

                Label lblDES = new Label();
                lblDES.Name = "Des_" + drDO["SOFTADDRESS"].ToString();
                lblDES.Text = drDO["DES"].ToString();
                lblDES.Font = new System.Drawing.Font("新細明體", 8, FontStyle.Bold);
                lblDES.Size = new System.Drawing.Size(250, 15);
                lblDES.Location = new Point(X + 100, Y + 4);

                tc_IOPage.TabPages["DO"].Controls.Add(shMain);
                tc_IOPage.TabPages["DO"].Controls.Add(lblAddress);
                tc_IOPage.TabPages["DO"].Controls.Add(lblDES);

                Count++;
                Y += 25;

            }
        }

        private void ovLamp_Click(object sender, EventArgs e)
        {
            OvalShape ov = (OvalShape)sender;
            string SoftAddress = ov.Name.Replace("Lamp_", "");
            string InterLock = ov.Tag.ToString().Replace("INTERLOCK:", "");

            if (InterLock.Equals("0"))
            {
                MessageBox.Show("This address interlock off", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ov.FillColor.Equals(Color.Red))
                clsPLC.WriPLC_Bit(SoftAddress, true);
            else
                clsPLC.WriPLC_Bit(SoftAddress, false);
        }

        private void TimerThread_Tick()
        {
            //Timermutex.WaitOne();

           //while(true)
           //{
           //     try
           //     {
           //         foreach (DataRow drDI in _dtDI.Rows)
           //         {
           //             if (RPD.STRCTURE.SYS.Data.IO_TABLE != null)
           //             {
           //                 if ((RPD.STRCTURE.SYS.Data.IO_TABLE[(int)drDI["ARRAY_COUNT"]] & ((int)(Math.Pow(2, (int)drDI["ARRAY_INDEX"])))) != 0)
           //                 {
           //                     Control[] a = this.Controls.Find("shMain_" + drDI["SOFTADDRESS"].ToString(), true);
           //                     ShapeContainer shapeContainer = a[0] as ShapeContainer;
           //                     OvalShape light = shapeContainer.Shapes.OfType<OvalShape>().FirstOrDefault(o => o.Name == "Lamp_" + drDI["SOFTADDRESS"].ToString());
           //                     this.Invoke(new EventHandler(Update_OvalShapeToLime), light);
           //                 }
           //                 else
           //                 {
           //                     Control[] a = this.Controls.Find("shMain_" + drDI["SOFTADDRESS"].ToString(), true);
           //                     ShapeContainer shapeContainer = a[0] as ShapeContainer;
           //                     OvalShape light = shapeContainer.Shapes.OfType<OvalShape>().FirstOrDefault(o => o.Name == "Lamp_" + drDI["SOFTADDRESS"].ToString());
           //                     this.Invoke(new EventHandler(Update_OvalShapeToRed), light);
           //                 }
           //             }
           //         }

           //         foreach (DataRow drDO in _dtDO.Rows)
           //         {
           //             if (RPD.STRCTURE.SYS.Data.IO_TABLE != null)
           //             {
           //                 if ((RPD.STRCTURE.SYS.Data.IO_TABLE[(int)drDO["ARRAY_COUNT"]] & ((int)(Math.Pow(2, (int)drDO["ARRAY_INDEX"])))) != 0)
           //                 {
           //                     Control[] a = this.Controls.Find("shMain_" + drDO["SOFTADDRESS"].ToString(), true);
           //                     ShapeContainer shapeContainer = a[0] as ShapeContainer;
           //                     OvalShape light = shapeContainer.Shapes.OfType<OvalShape>().FirstOrDefault(o => o.Name == "Lamp_" + drDO["WRITEADDRESS"].ToString());

           //                     if ((RPD.STRCTURE.SYS.Data.INTERLOCK_TABLE[(int)drDO["ARRAY_COUNT"]] & ((int)(Math.Pow(2, (int)drDO["ARRAY_INDEX"])))) != 0)
           //                         light.Tag = "INTERLOCK:1";
           //                     else
           //                         light.Tag = "INTERLOCK:0";

           //                     this.Invoke(new EventHandler(Update_OvalShapeToLime), light);

           //                 }
           //                 else
           //                 {
           //                     Control[] a = this.Controls.Find("shMain_" + drDO["SOFTADDRESS"].ToString(), true);
           //                     ShapeContainer shapeContainer = a[0] as ShapeContainer;
           //                     OvalShape light = shapeContainer.Shapes.OfType<OvalShape>().FirstOrDefault(o => o.Name == "Lamp_" + drDO["WRITEADDRESS"].ToString());

           //                     if ((RPD.STRCTURE.SYS.Data.INTERLOCK_TABLE[(int)drDO["ARRAY_COUNT"]] & ((int)(Math.Pow(2, (int)drDO["ARRAY_INDEX"])))) != 0)
           //                         light.Tag = "INTERLOCK:1";
           //                     else
           //                         light.Tag = "INTERLOCK:0";

           //                     this.Invoke(new EventHandler(Update_OvalShapeToRed), light);

           //                 }
           //             }
           //         }

           //     }
           //     catch (Exception Ex)
           //     {
           //         StreamWriter sw = new StreamWriter(MyPath + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
           //         sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\r\n" + Ex.ToString());
           //         sw.Close();

           //     }
           //     finally
           //     {

           //     }

           //     Thread.Sleep(200);
           //     //Timermutex.ReleaseMutex();

           // }

        }

        private void btn_ForceOutput_Click(object sender, EventArgs e)
        {
            //全部DO點歸零 因為起始位址不為16倍數，所以取中間，前後個別推
            int[] AllYTo0 = new int[7];
            for (int i = 0; i < 7; i++)
            {
                AllYTo0[i] = 0;
            }

            clsPLC.WriPLC_Bit("M15701", false);
            clsPLC.WriPLC_Bit("M15702", false);
            clsPLC.WriPLC_Bit("M15703", false);
            clsPLC.WriPLCBatch_Word("M15704", ref AllYTo0); //起始位置 M15704 後 7個 Word 至 M15815 批次寫入0
            clsPLC.WriPLC_Bit("M15816", false);
            clsPLC.WriPLC_Bit("M15817", false);
            clsPLC.WriPLC_Bit("M15818", false);
            clsPLC.WriPLC_Bit("M15819", false);
            clsPLC.WriPLC_Bit("M15820", false);
            clsPLC.WriPLC_Bit("M15821", false);
            clsPLC.WriPLC_Bit("M15822", false);
            clsPLC.WriPLC_Bit("M15823", false);
            clsPLC.WriPLC_Bit("M15824", false);
            clsPLC.WriPLC_Bit("M15825", false);
            clsPLC.WriPLC_Bit("M15826", false);
            clsPLC.WriPLC_Bit("M15827", false);
            clsPLC.WriPLC_Bit("M15828", false);

            //Force On / Off
            /*
            if (btn_ForceOutput.BackColor.Equals(Color.Transparent))
                clsPLC.WriPLC_Bit("M15700", true);
            else
                clsPLC.WriPLC_Bit("M15700", false);
                */


        }

        #endregion

        #region ---------------- Recipe Page ------------------------

        private void GridSysInit(ref DataGridView oGrid)
        {
            try
            {
                oGrid.Rows.Clear();
                //指定 Column 的字體
                oGrid.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.WhiteSmoke;
                oGrid.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.Black;

                //指定 Row 的字體
                oGrid.RowHeadersDefaultCellStyle.BackColor = System.Drawing.Color.WhiteSmoke;
                oGrid.RowHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                oGrid.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                oGrid.Font = new Font("Tahoma", 15);

                oGrid.ReadOnly = false;               //不允許修改
                oGrid.AllowUserToAddRows = false;    //不允許使用者從 DataGridView 中增加資料列。
                oGrid.AllowUserToDeleteRows = false; //不允許使用者從 DataGridView 中刪除資料列。 
                oGrid.AllowUserToResizeRows = false; //不允許調整資料列的大小。
                oGrid.MultiSelect = false;           //不允許多選列
                //.SelectionMode = DataGridViewSelectionMode.FullRowSelect     //選擇整Row方式
                oGrid.SelectionMode = DataGridViewSelectionMode.CellSelect;    //選擇單一Row方式
                oGrid.RowHeadersVisible = false;  //hide Row Header
                oGrid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;  //字體對齊位置

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private bool ValidaCell(DataGridViewCell cell)
        {
            try
            {
                cell.ErrorText = "";

                if (cell.Value == null || cell.Value.ToString() == "")
                {
                    cell.ErrorText = "Do not allow blank";
                    btn_RPSave.Enabled = false;
                    return false;
                }

                if (cell.RowIndex >= 0 && cell.RowIndex != 14)
                {
                    System.Text.RegularExpressions.Regex regul = new System.Text.RegularExpressions.Regex(@"[0-9]");
                    if (!regul.IsMatch(cell.Value.ToString()))
                    {
                        cell.ErrorText = "Only allows numeric";
                        btn_RPSave.Enabled = false;
                        return false;
                    }


                    //判斷是否超過range
                    string sMin = dgvRcp[cell.ColumnIndex + 1, cell.RowIndex].Value.ToString();
                    string sMax = dgvRcp[cell.ColumnIndex + 2, cell.RowIndex].Value.ToString();
                    if (regul.IsMatch(sMin) && regul.IsMatch(sMax))
                    {
                        double dVaule = Convert.ToDouble(cell.Value);
                        double dMin = Convert.ToDouble(sMin);
                        double dMax = Convert.ToDouble(sMax);
                        if (dVaule < dMin || dVaule > dMax)
                        {
                            cell.ErrorText = "Value Over Range";
                            btn_RPSave.Enabled = false;
                            return false;
                        }
                    }

                    //判斷小數點
                    string sParameter = dgvRcp[cell.ColumnIndex - 2, cell.RowIndex].Value.ToString();
                    if (dgvRcp[cell.ColumnIndex, cell.RowIndex].Value.ToString().IndexOf('.') > -1)
                    {
                        int sPoint = dgvRcp[cell.ColumnIndex, cell.RowIndex].Value.ToString().Length - dgvRcp[cell.ColumnIndex, cell.RowIndex].Value.ToString().IndexOf('.') - 1;

                        switch (sParameter)
                        {
                            case "Tablet Pushup Speed (M.H.)":
                                if (sPoint > 2)
                                {
                                    cell.ErrorText = "Please enter the decimal two value";
                                    btn_RPSave.Enabled = false;
                                    return false;
                                }
                                break;
                            case "Tablet Pushup Speed (B.G.)":
                                if (sPoint > 2)
                                {
                                    cell.ErrorText = "Please enter the decimal two value";
                                    btn_RPSave.Enabled = false;
                                    return false;
                                }
                                break;
                            case "Process Chamber Pressure":
                                if (sPoint > 6)
                                {
                                    cell.ErrorText = "Please enter the decimal six value";
                                    btn_RPSave.Enabled = false;
                                    return false;
                                }
                                break;
                            case "LL/UL Target Pressure":
                                if (sPoint > 6)
                                {
                                    cell.ErrorText = "Please enter the decimal six value";
                                    btn_RPSave.Enabled = false;
                                    return false;
                                }
                                break;
                            default:
                                if (sPoint > 1)
                                {
                                    cell.ErrorText = "Please enter the decimal one value";
                                    btn_RPSave.Enabled = false;
                                    return false;
                                }
                                break;
                        }
                    }
                }
            }
            catch
            {
                cell.ErrorText = "Syntax error";
                btn_RPSave.Enabled = false;
                return false;
            }


            return true;
        }
        private void dgvRcp_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            btn_RPSave.Enabled = true;

            foreach (DataGridViewRow row in dgvRcp.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.ColumnIndex == 3)
                    {

                        ValidaCell(cell); // 您可以透過 if(ValidaCell(cell))來做您想要的事情 
                    }
                }
            }

            dgvRcp.Refresh();
        }
        private void lstRecipe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstRecipe.SelectedIndex >= 0)
            {
                string sSqlCommand = "";

                sSqlCommand = "Select * from RECIPE WHERE NAME = '" + lstRecipe.Text + "'";
                DataRow drTemp = clsDBN.GetRow(sSqlCommand);

                dgvRcp.RowCount = 0;

                string[] row = new string[] { "1", "Hearth-Coil Current", "A", drTemp["HCC"].ToString(), "15", "30" };
                dgvRcp.Rows.Add(row);
                row = new string[] { "2", "Steering-Coil Current", "A", drTemp["SCC"].ToString(), "0", "15" };
                dgvRcp.Rows.Add(row);
                row = new string[] { "3", "G2-Coil Current", "A", drTemp["G2CC"].ToString(), "0", "20" };
                dgvRcp.Rows.Add(row);
                row = new string[] { "4", "B.G. Discharge Current", "A", drTemp["BGDC"].ToString(), "0", "200" };
                dgvRcp.Rows.Add(row);
                row = new string[] { "5", "M.H. Discharge Current", "A", drTemp["MHDC"].ToString(), "0", "200" };
                dgvRcp.Rows.Add(row);
                row = new string[] { "6", "Tablet Pushup Speed (M.H.)", "mm/min", drTemp["TBPS"].ToString(), "0", "100" };
                dgvRcp.Rows.Add(row);
                row = new string[] { "7", "Hearth-Coil Current (Ignition)", "A", drTemp["CHCI"].ToString(), "1", "40" };
                dgvRcp.Rows.Add(row);
                row = new string[] { "8", "Steering-Coil Current (Ignition)", "A", drTemp["CSCI"].ToString(), "1", "40" };
                dgvRcp.Rows.Add(row);
                row = new string[] { "9", "G2-Coil Current (Ignition)", "A", drTemp["CG2CI"].ToString(), "1", "40" };
                dgvRcp.Rows.Add(row);
                row = new string[] { "10", "Tablet Pushup Speed (B.G.)", "mm/min", drTemp["TBPSBGD"].ToString(), "0", "100" };
                dgvRcp.Rows.Add(row);
                row = new string[] { "11", "Gun Ar Value", "sccm", drTemp["RGV"].ToString(), "0", "500" };
                dgvRcp.Rows.Add(row);
                row = new string[] { "12", "Process Chamber O2 Value", "sccm", drTemp["CO2V"].ToString(), "0", "500" };
                dgvRcp.Rows.Add(row);
                row = new string[] { "13", "Process Chamber Ar Value", "sccm", drTemp["PCArV"].ToString(), "0", "500" };
                dgvRcp.Rows.Add(row);
                row = new string[] { "14", "Process Count", "times", drTemp["PCT"].ToString(), "0", "10" };
                dgvRcp.Rows.Add(row);
                row = new string[] { "15", "Pressure Balance Tuning", "-", drTemp["PBT"].ToString(), "-", "-" };
                dgvRcp.Rows.Add(row);
                row = new string[] { "16", "Process Chamber Pressure", "Torr", drTemp["PCP"].ToString(), "0", "750" };
                dgvRcp.Rows.Add(row);
                row = new string[] { "17", "LL/UL Target Pressure", "Torr", drTemp["LLULP"].ToString(), "0", "750" };
                dgvRcp.Rows.Add(row);
                row = new string[] { "18", "Process Chamber H2O Value", "sccm", drTemp["TRXV"].ToString(), "0", "20" };
                dgvRcp.Rows.Add(row);

                lblRPName.Text = drTemp["Name"].ToString();
                txtRPDes.Text = drTemp["Des"].ToString();
                lblRPCreateBy.Text = drTemp["CreateUser"].ToString();
                lblRPCreateOn.Text = drTemp["CreateTime"].ToString();
                lblRPLastModifyOn.Text = drTemp["LastModifyTime"].ToString();

                dgvRcp.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                DataGridViewComboBoxCell tCell = new DataGridViewComboBoxCell();
                _SQL = @"SELECT Name FROM PBT ";
                DataTable dt_PBT = clsDBN.GetTable(_SQL);
                foreach (DataRow dr_PBT in dt_PBT.Rows)
                {
                    tCell.Items.Add(dr_PBT["Name"].ToString());
                }

                dgvRcp.Rows[14].Cells[3] = tCell;
                dgvRcp.Rows[14].Cells[3].Value = drTemp["PBT"].ToString();
                if (dgvRcp.Rows[14].Cells[3].Value.ToString() == "")
                    btn_RPSave.Enabled = false;
                else
                    btn_RPSave.Enabled = true;
            }
        }
        private void btn_RPNew_Click(object sender, EventArgs e)
        {
            frmRecipeNew f = new frmRecipeNew();
            btn_RPNew.Enabled = false;
            f.Show();
            f.FormClosed += new FormClosedEventHandler(frmRecipeNew_FormClosed);
        }
        void frmRecipeNew_FormClosed(object sender, FormClosedEventArgs e)
        {
            btn_RPNew.Enabled = true;
            FunLoadComboxItem("RECIPE", ref lstRecipe);
        }
        private void btn_RPSave_Click(object sender, EventArgs e)
        {
            if (dgvRcp.RowCount <= 0)
            {
                return;
            }

            if (FunCheckRPExist(lstRecipe.Text))
            {
                string sSqlCommand = "";

                sSqlCommand = "Update RECIPE set HCC = '" + dgvRcp[3, 0].Value.ToString() +
                    "', SCC = '" + dgvRcp[3, 1].Value.ToString() +
                    "', G2CC = '" + dgvRcp[3, 2].Value.ToString() +
                    "', BGDC = '" + dgvRcp[3, 3].Value.ToString() +
                    "', MHDC = '" + dgvRcp[3, 4].Value.ToString() +
                    "', TBPS=  '" + dgvRcp[3, 5].Value.ToString() +
                    "', CHCI =  '" + dgvRcp[3, 6].Value.ToString() +
                    "', CSCI=  '" + dgvRcp[3, 7].Value.ToString() +
                    "',  CG2CI=  '" + dgvRcp[3, 8].Value.ToString() +
                    "',  TBPSBGD =  '" + dgvRcp[3, 9].Value.ToString() +
                    "',  RGV = '" + dgvRcp[3, 10].Value.ToString() +
                    "',  CO2V =  '" + dgvRcp[3, 11].Value.ToString() +
                    "',  PCArV =  '" + dgvRcp[3, 12].Value.ToString() +
                    "', PCT =  '" + dgvRcp[3, 13].Value.ToString() +
                    "', PBT =  '" + dgvRcp[3, 14].Value.ToString() +
                    "', PCP =  '" + dgvRcp[3, 15].Value.ToString() +
                    "', LLULP =  '" + dgvRcp[3, 16].Value.ToString() +
                    "', TRXV =  '" + dgvRcp[3, 17].Value.ToString() +
                    "', LastModifyTime =  '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                    "', Enable = '1" +
                    "' Where NAME ='" + lstRecipe.Text + "'";

                if (clsDBN.ExecuteSqlCommand(sSqlCommand) == 0)
                {
                    MessageBox.Show("Update Recipe Fail");

                }
                else
                {
                    RPD.STRCTURE.SYS.Data.RefreshRecipeRequest = true;
                    MessageBox.Show("Update Recipe Success!");
                }
            }

            lstRecipe_SelectedIndexChanged(sender, null);

        }
        private void btnRP_Del_Click(object sender, EventArgs e)
        {
            if (dgvRcp.RowCount <= 0)
            {
                return;
            }

            if (FunCheckRPExist(lstRecipe.Text))
            {
                string sSqlCommand = "";

                sSqlCommand = @"DELETE FROM RECIPE WHERE NAME ='" + lstRecipe.Text + "'";

                if (clsDBN.ExecuteSqlCommand(sSqlCommand) == 0)
                {
                    MessageBox.Show("Delete Recipe fail!");

                }
                else
                {
                    RPD.STRCTURE.SYS.Data.RefreshRecipeRequest = true;
                    MessageBox.Show("Delete Recipe success!");
                }
            }

            FunLoadComboxItem("RECIPE", ref lstRecipe);

            dgvRcp.RowCount = 0;
        }
        public void FunLoadComboxItem(string Table, ref ListBox lst)
        {
            string sSqlCommand = "";
            DataTable tdtTemp;
            sSqlCommand = "Select Name from " + Table;
            tdtTemp = clsDBN.GetTable(sSqlCommand);

            if (tdtTemp == null)
            {
                MessageBox.Show("Load DB fail!");
            }
            else
            {
                lst.Items.Clear();
                for (int i = 0; i < tdtTemp.Rows.Count; i++)
                {
                    lst.Items.Add(Convert.ToString(tdtTemp.Rows[i][0]));
                }
            }

            lst.SelectedIndex = 0;
            lst.Text = "";


        }
        private bool FunCheckRPExist(string sRPName)
        {
            string sSqlCommand = "";
            sSqlCommand = "Select * from RECIPE WHERE NAME = '" + sRPName + "'";


            if (!clsDBN.ExecuteSqlCommandBool(sSqlCommand))
            {
                //DB沒資料
                return false;
            }
            else
            {
                //DB有資料
                return true;
            }
        }
        private void dgvRcp_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ////如果是點在Value欄位的話才出現
            if (e.ColumnIndex == 3)
            {
                DataGridViewRow tNowRow = dgvRcp.Rows[dgvRcp.CurrentRow.Index];

                if (e.RowIndex == 14)
                {
                    //MessageBox.Show(dgvRcp.Rows[14].Cells[3].GetType().ToString());
                    if (dgvRcp.Rows[14].Cells[3].GetType().ToString() != "System.Windows.Forms.DataGridViewComboBoxCell")
                    {
                        if (dgvRcp.Rows[14].Cells[3].Value != null)
                        {
                            DataGridViewComboBoxCell tCell = new DataGridViewComboBoxCell();

                            _SQL = @"SELECT Name FROM PBT ";
                            DataTable dt_PBT = clsDBN.GetTable(_SQL);
                            foreach (DataRow dr_PBT in dt_PBT.Rows)
                            {
                                tCell.Items.Add(dr_PBT["Name"].ToString());
                            }
                            string st = dgvRcp.Rows[14].Cells[3].Value.ToString();
                            dgvRcp[3, 14] = tCell;

                        }
                    }
                }
            }
        }

        #endregion

        #region  ---------------- PBT Page ------------------------

        private void btnPBTNew_Click(object sender, EventArgs e)
        {
            frmPBTNew f = new frmPBTNew();
            btnPBTNew.Enabled = false;
            f.Show();
            f.FormClosed += new FormClosedEventHandler(frmPBTNew_FormClosed);
        }
        void frmPBTNew_FormClosed(object sender, FormClosedEventArgs e)
        {
            FunLoadComboxItem("PBT", ref lstPBT);
            btnPBTNew.Enabled = true;
        }

        private void lstPBT_SelectedIndexChanged(object sender, EventArgs e)
        {
            _SQL = @"SELECT * FROM PBT WHERE Name = '" + lstPBT.Text + "'";
            DataRow dr_PBT = clsDBN.GetRow(_SQL);
            if (dr_PBT != null)
            {
                lblPBTName.Text = dr_PBT["Name"].ToString();
                lblPBTCreateOn.Text = dr_PBT["CreateTime"].ToString();
                lblPBTCreateBy.Text = dr_PBT["CreateUser"].ToString();
                lblPBTLastModifiedON.Text = dr_PBT["LastModifyTime"].ToString();
                txtPBTDes.Text = dr_PBT["Des"].ToString();

                txt_LLDelayTime.Text = dr_PBT["LLDelayTime"].ToString();
                txt_LLArSet1.Text = dr_PBT["LLArSet1"].ToString();
                txt_LLArSet2.Text = dr_PBT["LLArSet2"].ToString();
                txt_LLArSet3.Text = dr_PBT["LLArSet3"].ToString();
                txt_LLO2Set1.Text = dr_PBT["LLO2Set1"].ToString();
                txt_LLO2Set2.Text = dr_PBT["LLO2Set2"].ToString();
                txt_LLO2Set3.Text = dr_PBT["LLO2Set3"].ToString();

                //Add By Henry 2016/1/22
                txt_LLXSet1.Text = dr_PBT["LLXSet1"].ToString();
                txt_LLXSet2.Text = dr_PBT["LLXSet2"].ToString();
                txt_LLXSet3.Text = dr_PBT["LLXSet3"].ToString();

                txt_TRDelayTime.Text = dr_PBT["TRDelayTime"].ToString();
                txt_TRArSet1.Text = dr_PBT["TRArSet1"].ToString();
                txt_TRArSet2.Text = dr_PBT["TRArSet2"].ToString();
                txt_TRArSet3.Text = dr_PBT["TRArSet3"].ToString();
                txt_TRArSet4.Text = dr_PBT["TRArSet4"].ToString();
                txt_TRArSet5.Text = dr_PBT["TRArSet5"].ToString();
                txt_TRArSet6.Text = dr_PBT["TRArSet6"].ToString();
                txt_TRArSet7.Text = dr_PBT["TRArSet7"].ToString();

                txt_TRAr2Set1.Text = dr_PBT["TRAr2Set1"].ToString();
                txt_TRAr2Set2.Text = dr_PBT["TRAr2Set2"].ToString();
                txt_TRAr2Set3.Text = dr_PBT["TRAr2Set3"].ToString();
                txt_TRAr2Set4.Text = dr_PBT["TRAr2Set4"].ToString();
                txt_TRAr2Set5.Text = dr_PBT["TRAr2Set5"].ToString();
                txt_TRAr2Set6.Text = dr_PBT["TRAr2Set6"].ToString();
                txt_TRAr2Set7.Text = dr_PBT["TRAr2Set7"].ToString();

                txt_TRO2Set1.Text = dr_PBT["TRO2Set1"].ToString();
                txt_TRO2Set2.Text = dr_PBT["TRO2Set2"].ToString();
                txt_TRO2Set3.Text = dr_PBT["TRO2Set3"].ToString();
                txt_TRO2Set4.Text = dr_PBT["TRO2Set4"].ToString();
                txt_TRO2Set5.Text = dr_PBT["TRO2Set5"].ToString();
                txt_TRO2Set6.Text = dr_PBT["TRO2Set6"].ToString();
                txt_TRO2Set7.Text = dr_PBT["TRO2Set7"].ToString();

                txt_ULArSet1.Text = dr_PBT["ULArSet1"].ToString();
                txt_ULArSet2.Text = dr_PBT["ULArSet2"].ToString();
                txt_ULArSet3.Text = dr_PBT["ULArSet3"].ToString();
                txt_ULO2Set1.Text = dr_PBT["ULO2Set1"].ToString();
                txt_ULO2Set2.Text = dr_PBT["ULO2Set2"].ToString();
                txt_ULO2Set3.Text = dr_PBT["ULO2Set3"].ToString();

                //Add By Henry 2016/1/22
                txt_ULXSet1.Text = dr_PBT["ULXSet1"].ToString();
                txt_ULXSet2.Text = dr_PBT["ULXSet2"].ToString();
                txt_ULXSet3.Text = dr_PBT["ULXSet3"].ToString();
            }


        }

        private void btnPBTDel_Click(object sender, EventArgs e)
        {
            string sSqlCommand = "";

            sSqlCommand = @"DELETE FROM PBT WHERE NAME ='" + lstPBT.Text + "'";

            if (clsDBN.ExecuteSqlCommand(sSqlCommand) == 0)
            {
                MessageBox.Show("Delete PBT fail!");

            }
            else
            {
                MessageBox.Show("Delete PBT success!");
            }


            FunLoadComboxItem("PBT", ref lstPBT);

        }

        private void btnPBTSave_Click(object sender, EventArgs e)
        {
            _SQL = @"UPDATE PBT SET [LLDelayTime] = @LLDelayTime
                                   ,[LLArSet1] = @LLArSet1
                                   ,[LLArSet2] = @LLArSet2
                                   ,[LLArSet3] = @LLArSet3
                                   ,[LLO2Set1] = @LLO2Set1
                                   ,[LLO2Set2] = @LLO2Set2
                                   ,[LLO2Set3] = @LLO2Set3
                                   ,[LLXSet1] = @LLXSet1
                                   ,[LLXSet2] = @LLXSet2
                                   ,[LLXSet3] = @LLXSet3
                                   ,[TRDelayTime] = @TRDelayTime
                                   ,[TRArSet1] = @TRArSet1
                                   ,[TRArSet2] = @TRArSet2
                                   ,[TRArSet3] = @TRArSet3
                                   ,[TRArSet4] = @TRArSet4
                                   ,[TRArSet5] = @TRArSet5
                                   ,[TRArSet6] = @TRArSet6
                                   ,[TRArSet7] = @TRArSet7
                                   ,[TRAr2Set1] = @TRAr2Set1
                                   ,[TRAr2Set2] = @TRAr2Set2
                                   ,[TRAr2Set3] = @TRAr2Set3
                                   ,[TRAr2Set4] = @TRAr2Set4
                                   ,[TRAr2Set5] = @TRAr2Set5
                                   ,[TRAr2Set6] = @TRAr2Set6
                                   ,[TRAr2Set7] = @TRAr2Set7
                                   ,[TRO2Set1] = @TRO2Set1
                                   ,[TRO2Set2] = @TRO2Set2
                                   ,[TRO2Set3] = @TRO2Set3
                                   ,[TRO2Set4] = @TRO2Set4
                                   ,[TRO2Set5] = @TRO2Set5
                                   ,[TRO2Set6] = @TRO2Set6
                                   ,[TRO2Set7] = @TRO2Set7
                                   ,[ULArSet1] = @ULArSet1
                                   ,[ULArSet2] = @ULArSet2
                                   ,[ULArSet3] = @ULArSet3
                                   ,[ULO2Set1] = @ULO2Set1
                                   ,[ULO2Set2] = @ULO2Set2
                                   ,[ULO2Set3] = @ULO2Set3
                                   ,[ULXSet1] = @ULXSet1
                                   ,[ULXSet2] = @ULXSet2
                                   ,[ULXSet3] = @ULXSet3
                                   ,[LastModifyTime] = @LastModifyTime
                                    WHERE Name = '" + lstPBT.Text + "'";
            clsDBN.SqlParameter.Add(new SqlParameter("@LLDelayTime", txt_LLDelayTime.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@LLArSet1", txt_LLArSet1.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@LLArSet2", txt_LLArSet2.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@LLArSet3", txt_LLArSet3.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@LLO2Set1", txt_LLO2Set1.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@LLO2Set2", txt_LLO2Set2.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@LLO2Set3", txt_LLO2Set3.Text));

            //Add By Henry 2016/1/22
            clsDBN.SqlParameter.Add(new SqlParameter("@LLXSet1", txt_LLXSet1.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@LLXSet2", txt_LLXSet2.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@LLXSet3", txt_LLXSet3.Text));

            clsDBN.SqlParameter.Add(new SqlParameter("@TRDelayTime", txt_TRDelayTime.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@TRArSet1", txt_TRArSet1.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@TRArSet2", txt_TRArSet2.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@TRArSet3", txt_TRArSet3.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@TRArSet4", txt_TRArSet4.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@TRArSet5", txt_TRArSet5.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@TRArSet6", txt_TRArSet6.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@TRArSet7", txt_TRArSet7.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@TRAr2Set1", txt_TRAr2Set1.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@TRAr2Set2", txt_TRAr2Set2.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@TRAr2Set3", txt_TRAr2Set3.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@TRAr2Set4", txt_TRAr2Set4.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@TRAr2Set5", txt_TRAr2Set5.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@TRAr2Set6", txt_TRAr2Set6.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@TRAr2Set7", txt_TRAr2Set7.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@TRO2Set1", txt_TRO2Set1.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@TRO2Set2", txt_TRO2Set2.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@TRO2Set3", txt_TRO2Set3.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@TRO2Set4", txt_TRO2Set4.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@TRO2Set5", txt_TRO2Set5.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@TRO2Set6", txt_TRO2Set6.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@TRO2Set7", txt_TRO2Set7.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@ULArSet1", txt_ULArSet1.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@ULArSet2", txt_ULArSet2.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@ULArSet3", txt_ULArSet3.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@ULO2Set1", txt_ULO2Set1.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@ULO2Set2", txt_ULO2Set2.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@ULO2Set3", txt_ULO2Set3.Text));

            //Add By Henry 2016/1/22
            clsDBN.SqlParameter.Add(new SqlParameter("@ULXSet1", txt_ULXSet1.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@ULXSet2", txt_ULXSet2.Text));
            clsDBN.SqlParameter.Add(new SqlParameter("@ULXSet3", txt_ULXSet3.Text));

            clsDBN.SqlParameter.Add(new SqlParameter("@LastModifyTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            if (clsDBN.ExecuteSqlCommand(_SQL) == 0)
            {
                MessageBox.Show("Udpate PBT fail!");

            }
            else
            {
                MessageBox.Show("Udpate PBT success!");
            }

            lstPBT_SelectedIndexChanged(sender, null);

        }

        #endregion

        #region  ---------------- Process Data Page ------------------------
        private void btnDrawChart_Click(object sender, EventArgs e)
        {
            //設置X軸座標的間隔
            chtCurrentValue.ChartAreas[0].AxisX.Interval = Convert.ToDouble(txtChartCount.Text) / 60;
            chtMFCFlows.ChartAreas[0].AxisX.Interval = Convert.ToDouble(txtChartCount.Text) / 60;
            chtPressure.ChartAreas[0].AxisX.Interval = Convert.ToDouble(txtChartCount.Text) / 60;
            chtVoltage.ChartAreas[0].AxisX.Interval = Convert.ToDouble(txtChartCount.Text) / 60;

            /*string _DateTimeNow = @"";
            _DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            clsDBN.ExecuteSqlCommand(@"INSERT INTO PROCESSDATA_LOG([STR_DT]
                                                                          ,[RGunArSet]
                                                                          ,[ChamberO2Set]
                                                                          ,[ChamberArSet]
                                                                          ,[RGunArFlow]
                                                                          ,[ChamberO2Flow]
                                                                          ,[ChamberArFlow]
                                                                          ,[TRGaugeValueC]
                                                                          ,[RGunCurrentofHearthCoil]
                                                                          ,[RGunCurrentofSteeringCoil]
                                                                          ,[RGunCurrentofG2Coil]
                                                                          ,[RGunVoltageofMainHearth]
                                                                          ,[RGunVoltageofBeamGuide]
                                                                          ,[RGunDischargeVoltage]
                                                                          ,[RGunCurrentofMainHearth]
                                                                          ,[RGunCurrentofBeamGuide]
                                                                          ,[RGunMHPushUpLevel]
                                                                          ,[RECIPE_NUMBER]
                                                                  
                                                                          ,[TRGaugeValuePH]) 
                                                                           VALUES('" + _DateTimeNow + @"'
                                                                                 ,'" + RPD.STRCTURE.TR.Map.TRMFCAr_GunSet + @"'
                                                                                 ,'" + RPD.STRCTURE.TR.Map.TRMFCO2Set + @"'
                                                                                 ,'" + RPD.STRCTURE.TR.Map.TRMFCAr_ChamberSet + @"'
                                                                                 ,'" + RPD.STRCTURE.TR.Map.TRMFCAr_Gun_Flow + @"'
                                                                                 ,'" + RPD.STRCTURE.TR.Map.TRMFCO2Flow + @"'
                                                                                 ,'" + RPD.STRCTURE.TR.Map.TRMFCAr_Chamber_Flow + @"'
                                                                                 ,'" + RPD.STRCTURE.TR.Map.TRGaugeValueC + @"'
                                                                                 ,'" + RPD.STRCTURE.PCC1.Data.RGunCurrentofHearthCoil + @"'
                                                                                 ,'" + RPD.STRCTURE.PCC1.Data.RGunCurrentofSteeringCoil + @"'
                                                                                 ,'" + RPD.STRCTURE.PCC1.Data.RGunCurrentofG2Coil + @"'
                                                                                 ,'" + RPD.STRCTURE.PCC1.Data.RGunVoltageofMainHearth + @"'
                                                                                 ,'" + RPD.STRCTURE.PCC1.Data.RGunVoltageofBeamGuide + @"'
                                                                                 ,'" + RPD.STRCTURE.PCC1.Data.RGunDischargeVoltage + @"'
                                                                                 ,'" + RPD.STRCTURE.PCC1.Data.RGunCurrentofMainHearth + @"'
                                                                                 ,'" + RPD.STRCTURE.PCC1.Data.RGunCurrentofBeamGuide + @"'
                                                                                 ,'" + RPD.STRCTURE.PCC1.Data.RGunMHpush_uplevel + @"'
                                                                                 ,'" + RPD.STRCTURE.SYS.Data.RecipeNumber + @"'
                                                                                 ,'" + RPD.STRCTURE.TR.Map.TRGaugeValuePH + @"'
                                                                                 , '')");

            */
            string SQL_Chart = @"SELECT Top " + txtChartCount.Text + @" CONVERT(varchar(8), [STR_DT], 8) as 'STR_DT'
                                        ,[RGunArSet]
                                        ,[ChamberO2Set]
                                        ,[ChamberArSet]
                                        ,[RGunArFlow]
                                        ,[ChamberO2Flow]
                                        ,[ChamberArFlow]
                                        ,[TRGaugeValueC] * 1000 as 'TRGaugeValueC'
                                        ,[RGunCurrentofHearthCoil]
                                        ,[RGunCurrentofSteeringCoil]
                                        ,[RGunCurrentofG2Coil]
                                        ,[RGunVoltageofMainHearth]
                                        ,[RGunVoltageofBeamGuide]
                                        ,[RGunDischargeVoltage]
                                        ,[RGunCurrentofMainHearth]
                                        ,[RGunCurrentofBeamGuide] 
                                        ,[RGunMHPushUpLevel] 
                                        ,[TRGaugeValuePH] FROM PROCESSDATA_LOG WHERE STR_DT BETWEEN '" + dtpStart.Text + "' AND '" + dtpEnd.Text + "' ORDER BY STR_DT ASC";
            DataTable dt_Trend = clsDBN.GetTable(SQL_Chart);

            string[] XAxis = new string[dt_Trend.Rows.Count];

            double[] YAxis_RGunArSet = new double[dt_Trend.Rows.Count];
            double[] YAxis_ChamberO2Set = new double[dt_Trend.Rows.Count];
            double[] YAxis_ChamberArSet = new double[dt_Trend.Rows.Count];
            double[] YAxis_RGunArFlow = new double[dt_Trend.Rows.Count];
            double[] YAxis_ChamberO2Flow = new double[dt_Trend.Rows.Count];
            double[] YAxis_ChamberArFlow = new double[dt_Trend.Rows.Count];
            double[] YAxis_TRGaugeValueC = new double[dt_Trend.Rows.Count];
            double[] YAxis_TRGaugeValuePH = new double[dt_Trend.Rows.Count];
            double[] YAxis_RGunMHPushUpLevel = new double[dt_Trend.Rows.Count];

            double[] YAxis_RGunCurrentofHearthCoil = new double[dt_Trend.Rows.Count];
            double[] YAxis_RGunCurrentofSteeringCoil = new double[dt_Trend.Rows.Count];
            double[] YAxis_RGunCurrentofG2Coil = new double[dt_Trend.Rows.Count];
            double[] YAxis_RGunVoltageofMainHearth = new double[dt_Trend.Rows.Count];

            double[] YAxis_RGunVoltageofBeamGuide = new double[dt_Trend.Rows.Count];
            double[] YAxis_RGunDischargeVoltage = new double[dt_Trend.Rows.Count];
            double[] YAxis_RGunCurrentofMainHearth = new double[dt_Trend.Rows.Count];
            double[] YAxis_RGunCurrentofBeamGuide = new double[dt_Trend.Rows.Count];


            XAxis = dt_Trend.Rows.Cast<DataRow>().Select(row => row["STR_DT"].ToString()).ToArray();

            YAxis_RGunArSet = dt_Trend.Rows.Cast<DataRow>().Select(row => Convert.ToDouble(row["RGunArSet"])).ToArray();
            YAxis_ChamberO2Set = dt_Trend.Rows.Cast<DataRow>().Select(row => Convert.ToDouble(row["ChamberO2Set"])).ToArray();
            YAxis_ChamberArSet = dt_Trend.Rows.Cast<DataRow>().Select(row => Convert.ToDouble(row["ChamberArSet"])).ToArray();
            YAxis_RGunArFlow = dt_Trend.Rows.Cast<DataRow>().Select(row => Convert.ToDouble(row["RGunArFlow"])).ToArray();
            YAxis_ChamberO2Flow = dt_Trend.Rows.Cast<DataRow>().Select(row => Convert.ToDouble(row["ChamberO2Flow"])).ToArray();
            YAxis_ChamberArFlow = dt_Trend.Rows.Cast<DataRow>().Select(row => Convert.ToDouble(row["ChamberArFlow"])).ToArray();
            YAxis_TRGaugeValueC = dt_Trend.Rows.Cast<DataRow>().Select(row => Convert.ToDouble(row["TRGaugeValueC"])).ToArray();
            YAxis_TRGaugeValuePH = dt_Trend.Rows.Cast<DataRow>().Select(row => Convert.ToDouble(row["TRGaugeValuePH"])).ToArray();
            YAxis_RGunMHPushUpLevel = dt_Trend.Rows.Cast<DataRow>().Select(row => Convert.ToDouble(row["RGunMHPushUpLevel"])).ToArray();

            YAxis_RGunCurrentofHearthCoil = dt_Trend.Rows.Cast<DataRow>().Select(row => Convert.ToDouble(row["RGunCurrentofHearthCoil"])).ToArray();
            YAxis_RGunCurrentofSteeringCoil = dt_Trend.Rows.Cast<DataRow>().Select(row => Convert.ToDouble(row["RGunCurrentofSteeringCoil"])).ToArray();
            YAxis_RGunCurrentofG2Coil = dt_Trend.Rows.Cast<DataRow>().Select(row => Convert.ToDouble(row["RGunCurrentofG2Coil"])).ToArray();

            YAxis_RGunVoltageofMainHearth = dt_Trend.Rows.Cast<DataRow>().Select(row => Convert.ToDouble(row["RGunVoltageofMainHearth"])).ToArray();
            YAxis_RGunVoltageofBeamGuide = dt_Trend.Rows.Cast<DataRow>().Select(row => Convert.ToDouble(row["RGunVoltageofBeamGuide"])).ToArray();
            YAxis_RGunDischargeVoltage = dt_Trend.Rows.Cast<DataRow>().Select(row => Convert.ToDouble(row["RGunDischargeVoltage"])).ToArray();

            YAxis_RGunCurrentofMainHearth = dt_Trend.Rows.Cast<DataRow>().Select(row => Convert.ToDouble(row["RGunCurrentofMainHearth"])).ToArray();
            YAxis_RGunCurrentofBeamGuide = dt_Trend.Rows.Cast<DataRow>().Select(row => Convert.ToDouble(row["RGunCurrentofBeamGuide"])).ToArray();

            chtMFCFlows.Series["R-Gun Ar Flow Set Value"].Points.DataBindXY(XAxis, YAxis_RGunArSet); //資料綁定
            chtMFCFlows.Series["Chamber O2 Flow Set Value"].Points.DataBindXY(XAxis, YAxis_ChamberO2Set); //資料綁定
            chtMFCFlows.Series["Chamber Ar Flow Set Value"].Points.DataBindXY(XAxis, YAxis_ChamberArSet); //資料綁定
            chtMFCFlows.Series["R-Gun Ar Flow"].Points.DataBindXY(XAxis, YAxis_RGunArFlow); //資料綁定
            chtMFCFlows.Series["Chamber O2 Flow"].Points.DataBindXY(XAxis, YAxis_ChamberO2Flow); //資料綁定
            chtMFCFlows.Series["Chamber Ar Flow"].Points.DataBindXY(XAxis, YAxis_ChamberArFlow); //資料綁定

            chtPressure.Series["TR Gauge Value C"].YAxisType = AxisType.Primary;
            chtPressure.Series["TR Gauge Value C"].XAxisType = AxisType.Primary;
            //chtPressure.Series["TRGaugeValueC"].YAxisType = AxisType.Primary;
            //chtPressure.Series["TRGaugeValueC"].XAxisType = AxisType.Primary;
            chtPressure.Series["TR Gauge Value PH"].YAxisType = AxisType.Primary;
            chtPressure.Series["TR Gauge Value PH"].XAxisType = AxisType.Primary;
            chtPressure.Series["R-Gun MH Push Up Level"].YAxisType = AxisType.Secondary;
            chtPressure.Series["R-Gun MH Push Up Level"].XAxisType = AxisType.Primary;

            chtPressure.Series["TR Gauge Value C"].Points.DataBindXY(XAxis, YAxis_TRGaugeValueC); //資料綁定
            chtPressure.Series["TR Gauge Value PH"].Points.DataBindXY(XAxis, YAxis_TRGaugeValuePH); //資料綁定
            chtPressure.Series["R-Gun MH Push Up Level"].Points.DataBindXY(XAxis, YAxis_RGunMHPushUpLevel);

            chtCurrentValue.Series["R-Gun Current of Hearth-Coil"].Points.DataBindXY(XAxis, YAxis_RGunCurrentofHearthCoil); //資料綁定
            chtCurrentValue.Series["R-Gun Current of Steering-Coil"].Points.DataBindXY(XAxis, YAxis_RGunCurrentofSteeringCoil); //資料綁定
            chtCurrentValue.Series["R-Gun Current of G2-Coil"].Points.DataBindXY(XAxis, YAxis_RGunCurrentofG2Coil); //資料綁定
            chtCurrentValue.Series["R-Gun Current of Main Hearth"].Points.DataBindXY(XAxis, YAxis_RGunCurrentofMainHearth); //資料綁定
            chtCurrentValue.Series["R-Gun Current of Beam Guide"].Points.DataBindXY(XAxis, YAxis_RGunCurrentofBeamGuide); //資料綁定

            chtVoltage.Series["R-Gun Voltage of Main Hearth"].Points.DataBindXY(XAxis, YAxis_RGunVoltageofMainHearth); //資料綁定
            chtVoltage.Series["R-Gun Voltage of Beam Guide"].Points.DataBindXY(XAxis, YAxis_RGunVoltageofBeamGuide); //資料綁定
            chtVoltage.Series["R-Gun Discharge Voltage"].Points.DataBindXY(XAxis, YAxis_RGunDischargeVoltage); //資料綁定

        }
        private void InitChart(Chart cht)
        {
            cht.BackColor = Color.FromKnownColor(KnownColor.White);
            //設定 ChartArea----------------------------------------------------------------------
            cht.ChartAreas[0].Area3DStyle.Enable3D = false; //3D效果      
            cht.ChartAreas[0].BackColor = Color.FromKnownColor(KnownColor.White);//背景色

            cht.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            cht.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            cht.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            cht.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            cht.ChartAreas[0].AxisX.LabelStyle.Angle = 75;

            //Y軸線顏色
            cht.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(150, 150, 150);
            cht.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Solid;
            cht.ChartAreas[0].AxisY.LabelStyle.Format = @"{0:0.0}";
            //X軸線顏色
            cht.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(150, 150, 150);
            cht.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;
       
            //設置X軸座標偏移為1
            //cht.ChartAreas[0].AxisX.IntervalOffset = 1;

            cht.ChartAreas[0].AxisX.IsStartedFromZero = true;
            cht.ChartAreas[0].AxisY.IsStartedFromZero = true;

            cht.ChartAreas[0].AxisX.Minimum = 0;
            //cht.ChartAreas[0].AxisY.Minimum = 0;

            cht.Legends[0].Docking = Docking.Top;//自訂顯示位置
            cht.Legends[0].BackColor = Color.FromArgb(235, 235, 235);//背景色
            //斜線背景
            cht.Legends[0].BackHatchStyle = ChartHatchStyle.DarkDownwardDiagonal;
            cht.Legends[0].BorderWidth = 1;
            cht.Legends[0].BorderColor = Color.FromArgb(200, 200, 200);

            foreach (Series s in cht.Series)
            {
                //設定Series-----------------------------------------------------------------------
                s.ChartType = SeriesChartType.Line;//直條圖
                s.MarkerStyle = MarkerStyle.None;

                //s.MarkerColor = Color.DarkGray;
                //s.MarkerBorderColor = Color.FromKnownColor(KnownColor.Black);
                //s.MarkerBorderWidth = 1;
                //s.MarkerSize = 4; //Label範圍大小
                //s.BorderWidth = 3;

                s.ToolTip = s.Name + "\n#VALX" + "\nValue: " + "#VALY";
                s.XValueType = ChartValueType.String;

                s.LabelForeColor = Color.FromKnownColor(KnownColor.Black);
                //字體設定
                s.Font = new System.Drawing.Font("TrebuchetMS", 8, System.Drawing.FontStyle.Bold);
            }



        }
        private void Item_ResetZoom_Click(object sender, EventArgs e)
        {
            Chart Select_Chart = (Chart)Chart_Menu.SourceControl;

            Select_Chart.ChartAreas[0].AxisX.ScaleView.ZoomReset(0);
            Select_Chart.ChartAreas[0].AxisY.ScaleView.ZoomReset(0);
        }
        private void Item_SaveAsImage_Click(object sender, EventArgs e)
        {
            Chart Select_Chart = (Chart)Chart_Menu.SourceControl;
            SaveImages(Select_Chart);
        }
        public void SaveImages(Chart chart)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save as Image";
            saveFileDialog.Filter = "JPEG File(*.jpg)|*.jpg|PNG File(*.png)|*.png|GIF File(*.gif)|*.gif|all Files(*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                chart.SaveImage(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                MessageBox.Show("Save success！", "congratulation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        private void chkMFC_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                controlChartSeries(this.chtMFCFlows, sender);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void chkVoltage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                controlChartSeries(this.chtVoltage, sender);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void chkPressure_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                controlChartSeries(this.chtPressure, sender);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void chkPressure2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                controlChartSeries(this.chtPressure, sender);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void chkRGunMHPushUpLevel_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                controlChartSeries(this.chtPressure, sender);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void chkCurrentValue_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                controlChartSeries(this.chtCurrentValue, sender);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void controlChartSeries(System.Windows.Forms.DataVisualization.Charting.Chart chr, Object sender)
        {
            CheckBox chkBox = null;
            chkBox = (CheckBox)sender;
            chr.Series[chkBox.Text.Trim()].Enabled = chkBox.Checked;

        }
        #endregion

        private void btnLeakTest_Click(object sender, EventArgs e)
        {

            if (Convert.ToDouble(txtLeakTime.Text) > 360)
            {
                MessageBox.Show("Leak Time cannot over 6 hours");
                return;
            }
            else
            {
                // add by william
                txtLeakEndTorr.Text = "";
                txtLeakRate.Text = "";

                //modify by william
                txtLeakStartTorr.Text = (Convert.ToDouble(RPD.STRCTURE.SYS.Data.LeakTestStartTorr) / 1000000).ToString("E2").Replace("00", "");
                RPD.STRCTURE.SYS.Map.LeakTestTimeSetting = (int)Convert.ToDouble(txtLeakTime.Text) * 10 * 60;
                RPD.STRCTURE.SYS.Map.LeakTestWaitTimeSetting = (int)Convert.ToDouble(txtLeakWaitTime.Text) * 10 * 60;
                clsPLC.WriPLC_Pulse(RPD.STRCTURE.SYS.Btn.LeakTestSwitchTR.Address);
                RPD.STRCTURE.SYS.Data.LeakTestGUIFinish = 0;


            }


        }

        private void LeakTestRequest()
        {
            if (RPD.STRCTURE.SYS.Map.LeakTestFinishTR.Equals(1) && RPD.STRCTURE.SYS.Data.LeakTestGUIFinish.Equals(0))
            {
                //PLC 回覆收集完成
                double iStart = ((double)RPD.STRCTURE.SYS.Data.LeakTestStartTorr) / 1000000;
                double iEnd = ((double)RPD.STRCTURE.SYS.Data.LeakTestEndTorr) / 1000000;

                //modify by william
                double iLeakRate = Convert.ToDouble((iEnd - iStart) / (Convert.ToDouble(RPD.STRCTURE.SYS.Map.LeakTestTimeSetting) / 10 / 60));
                txtLeakEndTorr.Text = (Convert.ToDouble(RPD.STRCTURE.SYS.Data.LeakTestEndTorr) / 1000000).ToString("E2").Replace("00", "");

                txtLeakRate.Text = iLeakRate.ToString("E2").Replace("00", "");

                _SQL = @"INSERT INTO LEAK_TEST VALUES(@STR_DT
                                                     ,@STR_P
                                                     ,@END_P
                                                     ,@LEAKRATE
                                                     ,@DURATION)";
                clsDBN.SqlParameter.Add(new SqlParameter("@STR_DT", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")));
                clsDBN.SqlParameter.Add(new SqlParameter("@STR_P", txtLeakStartTorr.Text)); //更改紀錄工程符號
                clsDBN.SqlParameter.Add(new SqlParameter("@END_P", txtLeakEndTorr.Text));   //更改紀錄工程符號
                clsDBN.SqlParameter.Add(new SqlParameter("@LEAKRATE", iLeakRate.ToString("E2").Replace("00", "")));
                clsDBN.SqlParameter.Add(new SqlParameter("@DURATION", RPD.STRCTURE.SYS.Map.LeakTestTimeSetting / 10 / 60)); //更改為分鐘
                clsDBN.ExecuteSqlCommand(_SQL);

                RPD.STRCTURE.SYS.Data.LeakTestGUIFinish = 1;

            }


        }

        private void tabSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabSystem.SelectedIndex)
            {
                case 0:
                    //_ThreadTimer.Change(-1, -1);
                    _IsRefeshDIO = false;
                    f_Heater.ControlTimer(false);
                    break;
                case 1:

                    clsPLC.WriPLCBatch_Word("D35200", ref RPD.STRCTURE.SYS.Data.EC_TABLE);     //將EC暫存區初始化、避免值為0把設定值洗掉      

                    EC_CVSpeedSet.Text = RPD.STRCTURE.SYS.Data.CVSpeed.ToString();

                    EC_ProcessSpeedSet.Text = RPD.STRCTURE.SYS.Data.CVProcessSpeed.ToString();
                    EC_DVOpenATM.Text = RPD.STRCTURE.SYS.Data.DVOpenATM.ToString();
                    EC_TRCV1_TRCV2DelayTime.Text = RPD.STRCTURE.TR.Data.TRCV1_TRCV2DelayTime.ToString();

                    EC_EFCVSpeedSet.Text = RPD.STRCTURE.EF.Data.EFCVSpeedTime.ToString();
                    EC_EFCVDecSet.Text = RPD.STRCTURE.EF.Data.EFCVDecTime.ToString();

                    EC_LLCVSpeedSet.Text = RPD.STRCTURE.LL.Data.LLCVSpeedTime.ToString();
                    EC_LLCVDecSet.Text = RPD.STRCTURE.LL.Data.LLCVDecTime.ToString();

                    EC_TR1CVSpeedSet.Text = RPD.STRCTURE.TR.Data.TR1CVSpeedTime.ToString();
                    EC_TR1CVDecSet.Text = RPD.STRCTURE.TR.Data.TR1CVDecTime.ToString();
                    EC_TR2CVSpeedSet.Text = RPD.STRCTURE.TR.Data.TR2CVSpeedTime.ToString();
                    EC_TR2CVDecSet.Text = RPD.STRCTURE.TR.Data.TR2CVDecTime.ToString();
                    EC_TR3CVSpeedSet.Text = RPD.STRCTURE.TR.Data.TR3CVSpeedTime.ToString();
                    EC_TR3CVDecSet.Text = RPD.STRCTURE.TR.Data.TR3CVDecTime.ToString();

                    EC_ULCVSpeedSet.Text = RPD.STRCTURE.UL.Data.ULCVSpeedTime.ToString();
                    EC_ULCVDecSet.Text = RPD.STRCTURE.UL.Data.ULCVDecTime.ToString();

                    EC_DFCVSpeedSet.Text = RPD.STRCTURE.DF.Data.DFCVSpeedTime.ToString();
                    EC_DFCVDecSet.Text = RPD.STRCTURE.DF.Data.DFCVDecTime.ToString();

                    EC_EFToLLDelayTime.Text = RPD.STRCTURE.EF.Data.EFToLLDelayTime.ToString();
                    EC_TR3CVToULDelayTime.Text = RPD.STRCTURE.TR.Data.TR3CVToULDelayTime.ToString();
                    EC_ULToUNLDPortDelayTime.Text = RPD.STRCTURE.UL.Data.ULToUNLDPortDelayTime.ToString();

                    EC_Water_Higher_Limit.Text = RPD.STRCTURE.SYS.Data.MainWaterHigherLimit.ToString();
                    EC_Water_Lower_Limit.Text = RPD.STRCTURE.SYS.Data.MainWaterLowerLimit.ToString();

                    //Add By Henry 2015/7/13 新增EC 水Sensor
                    EC_LLPumpFlowLowerLimit.Text = RPD.STRCTURE.LL.Data.LLPumpFlowLowerLimit.ToString();
                    EC_BackingFlowLowerLimit.Text = RPD.STRCTURE.TR.Data.BackingFlowLowerLimit.ToString();
                    EC_ULPumpFlowLowerLimit.Text = RPD.STRCTURE.UL.Data.ULPumpFlowLowerLimit.ToString();
                    EC_LLTPPumpFlowLowerLimit.Text = RPD.STRCTURE.LL.Data.LLTPPumpFlowLowerLimit.ToString();
                    EC_TR2CHBCOVERFlowLowerLimit.Text = RPD.STRCTURE.TR.Data.TR2CHBCOVERFlowLowerLimit.ToString();
                    EC_TRTPPumpFlowLowerLimit.Text = RPD.STRCTURE.TR.Data.TRTPPumpFlowLowerLimit.ToString();
                    EC_TR3CHBCOVERFlowLowerLimit.Text = RPD.STRCTURE.TR.Data.TR3CHBCOVERFlowLowerLimit.ToString();
                    EC_TR2CoolingFlowLowerLimit.Text = RPD.STRCTURE.TR.Data.TR2CoolingFlowLowerLimit.ToString();
                    EC_PlasmaGeneratorG1FlowLowerLimit.Text = RPD.STRCTURE.PCC1.Data.PlasmaGeneratorG1FlowLowerLimit.ToString();
                    EC_PlasmaGeneratorG2FlowLowerLimit.Text = RPD.STRCTURE.PCC1.Data.PlasmaGeneratorG2FlowLowerLimit.ToString();
                    EC_PlasmaGeneratorsteeringcoilFlowLowerLimit.Text = RPD.STRCTURE.PCC1.Data.PlasmaGeneratorsteeringcoilFlowLowerLimit.ToString();
                    EC_HearthMainFlowLowerLimit.Text = RPD.STRCTURE.PCC1.Data.HearthMainFlowLowerLimit.ToString();
                    EC_HearthBeamFlowLowerLimit.Text = RPD.STRCTURE.PCC1.Data.HearthBeamFlowLowerLimit.ToString();
                    EC_RPDDoorInwallFlowLowerLimit.Text = RPD.STRCTURE.PCC1.Data.RPDDoorInwallFlowLowerLimit.ToString();
                    EC_RPDChamberInwallFlowLowerLimit.Text = RPD.STRCTURE.PCC1.Data.RPDChamberInwallFlowLowerLimit.ToString();
                    EC_ULTPPumpFlowLowerLimit.Text = RPD.STRCTURE.UL.Data.ULTPPumpFlowLowerLimit.ToString();

                    EC_IdelTime.Text = RPD.STRCTURE.PCC1.Data.IdelTime.ToString();
                    EC_ReadyTime.Text = RPD.STRCTURE.PCC1.Data.ReadyTime.ToString();

                    EC_LLULTargetPreSet.Text = RPD.STRCTURE.UL.Data.LLULTargetPreSet.ToString();

                    EC_LLSRVTime.Text = RPD.STRCTURE.LL.Data.LL_SRVSet_Sec.ToString();
                    EC_ULSRVTime.Text = RPD.STRCTURE.UL.Data.UL_SRVSet_Sec.ToString();

                    EC_TRCgaugeOffset.Text = RPD.STRCTURE.TR.Data.TRCgaugeOffset.ToString();

                    //Add by Henry 2016/1/25
                    EC_LLMFCDelayTime.Text = RPD.STRCTURE.LL.Data.LL_MFC_Pipeline_Delay_Sec.ToString();
                    EC_ULMFCDelayTime.Text = RPD.STRCTURE.UL.Data.UL_MFC_Pipeline_Delay_Sec.ToString();

                    //Add by Henry 2016/2/18 新增LL控壓精度
                    EC_LLPCAccuracy.Text = RPD.STRCTURE.LL.Data.LLPCAccuracy.ToString();
                    EC_ULPCAccuracy.Text = RPD.STRCTURE.UL.Data.ULPCAccuracy.ToString();

                    _IsRefeshDIO = false;
                    f_Heater.ControlTimer(false);
                    //_ThreadTimer.Change(-1, -1);
                    break;
                case 2:
                    //_ThreadTimer.Change(0, 400);
                    _IsRefeshDIO = true;
                    f_Heater.ControlTimer(false);
                    break;
                case 3:
                    //_ThreadTimer.Change(-1, -1);
                    FunLoadComboxItem("RECIPE", ref lstRecipe);
                    _IsRefeshDIO = false;
                    f_Heater.ControlTimer(false);
                    break;
                case 4:
                    //_ThreadTimer.Change(-1, -1);
                    FunLoadComboxItem("PBT", ref lstPBT);
                    _IsRefeshDIO = false;
                    f_Heater.ControlTimer(false);
                    break;
                case 5:
                    //_ThreadTimer.Change(-1, -1);
                    dtpStart.Value = Convert.ToDateTime(DateTime.Now.AddMinutes(-60));
                    dtpEnd.Value = DateTime.Now;
                    _IsRefeshDIO = false;
                    f_Heater.ControlTimer(false);
                    break;
                case 6:
                    //_ThreadTimer.Change(-1, -1);
                    f_Heater.ControlTimer(true);
                    f_Heater.RefreshSetValue();
                    _IsRefeshDIO = false;

                    break;
            }
        }

        private void btnTrayData_Click(object sender, EventArgs e)
        {
            if (RPD.STRCTURE.SYS.Btn.AutoModeSwitch.Lamp.Equals(1)) //自動模式不能按手動頁面功能
            {
                MessageBox.Show("Auto Mode donot accept tray data!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            frmTrayMaintain f = new frmTrayMaintain();
            btnTrayData.Enabled = false;
            f.Show();
            f.FormClosed += new FormClosedEventHandler(frmTrayMaintain_FormClosed);
        }

        void frmTrayMaintain_FormClosed(object sender, FormClosedEventArgs e)
        {
            btnTrayData.Enabled = true;

        }

        private void btnLTLog_Click(object sender, EventArgs e)
        {
            frmLeakTestLog f = new frmLeakTestLog();
            btnLTLog.Enabled = false;
            f.Show();
            f.FormClosed += new FormClosedEventHandler(frmLeakTestLog_FormClosed);
        }

        void frmLeakTestLog_FormClosed(object sender, FormClosedEventArgs e)
        {
            btnLTLog.Enabled = true;
        }

        private void btnECUpdate_Click(object sender, EventArgs e)
        {
            //Add by Henry 2015/6/26 自行判斷EC是否寫入成功!!
            int[] G2E_Temp = new int[100];
            clsPLC.ReadPLCBatch_Word("D35200", ref G2E_Temp);

            clsPLC.WriPLC_Pulse("M15053");
            System.Threading.Thread.Sleep(500);

            int[] E2G_Temp = new int[100];
            clsPLC.ReadPLCBatch_Word("D38400", ref E2G_Temp);

            for (int i = 0; i < G2E_Temp.Length; i++)
            {
                if (G2E_Temp[i].Equals(E2G_Temp[i]))
                {
                    //do nothing
                }
                else //有錯
                {
                    MessageBox.Show("Update EC Error !!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            MessageBox.Show("Update EC OK !!", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void btnUpdateMDA_Click(object sender, EventArgs e)
        {
            int Result = clsDBN.ExecuteSqlCommand("UPDATE MDATIME SET [HOUR] = '" + txtMDAHour_Set.Text + "',[MIN] = '" + txtMDAMin_Set.Text + "' WHERE [Key] = 1");
            if (Result > -1)
            {
                RefreshMDA();
                MessageBox.Show("Update Main Discharge  Accumulation Time Success !!", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Update Main Discharge  Accumulation Time Error !!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void btnAutomationLog_Click(object sender, EventArgs e)
        {
            frmAutomationLog f = new frmAutomationLog();
            btnAutomationLog.Enabled = false;
            f.Show();
            f.FormClosed += new FormClosedEventHandler(frmAutomationLog_FormClosed);
        }

        void frmAutomationLog_FormClosed(object sender, FormClosedEventArgs e)
        {
            btnAutomationLog.Enabled = true;
        }

        private void chtPressure_Click(object sender, EventArgs e)
        {

        }

        private void MainPage_Click(object sender, EventArgs e)
        {

        }

        private void groupBox11_Enter(object sender, EventArgs e)
        {

        }

        private void lblTackTime4_Click(object sender, EventArgs e)
        {

        }

        private void EC_CVSpeedSet_TextChanged(object sender, EventArgs e)
        {

        }

        private void EC_IdelTime_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
