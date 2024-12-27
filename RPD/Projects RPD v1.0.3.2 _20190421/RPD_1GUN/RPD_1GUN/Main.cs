using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace RPD_1GUN
{
    public partial class Main : Form
    {
        bool _RegisterEvent = false;
        int _PLCAliveIndex = 0;
        int _PLCAliveReCount = 0;
        private static string MyPath = Application.StartupPath + "\\SystemError\\";
        string _PD_Folder = Application.StartupPath + "\\ProcessData\\";
        string _PD_MFC = Application.StartupPath + "\\ProcessData\\MFC\\";
        string _PD_Rgun = Application.StartupPath + "\\ProcessData\\R-Gun\\";
        string _PD_Pressure = Application.StartupPath + "\\ProcessData\\Pressure\\";
        string _PD_PCW = Application.StartupPath + "\\ProcessData\\PCW\\";
        string _PD_Transfer = Application.StartupPath + "\\ProcessData\\Transfer\\";


        string _SQL = @"";
        string _DateTimeNow = @"";
        string _t_DateTimeNow = @"";

        string _DateTimeNow_Automation = @"";
        string _t_DateTimeNow_Automation = @"";

        clsPLC _ClsPLC = new clsPLC(1);

        #region 宣告Form
        frmSystem frmP1 = new frmSystem();
        frmEventLog frmEvent = new frmEventLog();
        frmAlarm frmAlarms = new frmAlarm();
        frmInterLock frmInterLock = new frmInterLock();
        #endregion

        //System.Threading.Timer _ThreadTimer = null;
        //private static System.Threading.Mutex _Timermutex = new System.Threading.Mutex();

        System.Threading.Timer _ThreadTimer_PD = null;
        private static System.Threading.Mutex _Timermutex_PD = new System.Threading.Mutex();

        //Main 勿加 protected override CreateParams CreateParams 會造成畫面閃爍

        public Main()
        {
            InitializeComponent();

            if (!Directory.Exists(MyPath))
                Directory.CreateDirectory(MyPath);


            if (!Directory.Exists(_PD_Folder))
            {
                Directory.CreateDirectory(_PD_Folder);
            }

            if (!Directory.Exists(_PD_MFC))
            {
                Directory.CreateDirectory(_PD_MFC);
            }

            if (!Directory.Exists(_PD_Rgun))
            {
                Directory.CreateDirectory(_PD_Rgun);
            }

            if (!Directory.Exists(_PD_Pressure))
            {
                Directory.CreateDirectory(_PD_Pressure);
            }

            if (!Directory.Exists(_PD_PCW))
            {
                Directory.CreateDirectory(_PD_PCW);
            }

            if (!Directory.Exists(_PD_Transfer))
            {
                Directory.CreateDirectory(_PD_Transfer);
            }


            try
            {
                //DB
                clsDBN.clsDBNew();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR");
            }

        }

        private void Main_Load(object sender, EventArgs e)
        {
            lblVersion.Text = "GUI Ver. " + Application.ProductVersion;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;

            ChkAppIsAlreadyRunning();           //防止程式重覆開啓
            btnLogin_Click(this, null);

            SubShowForm(frmP1);

            //_ThreadTimer = new System.Threading.Timer(new System.Threading.TimerCallback(TimerThread_Tick), "Timer", 0, 400); //宣告Timer
            _ThreadTimer_PD = new System.Threading.Timer(new System.Threading.TimerCallback(ProcessDataTimer_Tick), "Timer", 0, 1000); //宣告Timer
            
            Thread T_Main = new Thread(new ThreadStart(TimerThread_Tick));
            T_Main.Start();


            RefreshRecipe();

            //初始化AlarmDisplay
            RPD.STRCTURE.SYS.Data.dt_AlarmDisplay.Columns.Add("EQ");
            RPD.STRCTURE.SYS.Data.dt_AlarmDisplay.Columns.Add("Type");
            RPD.STRCTURE.SYS.Data.dt_AlarmDisplay.Columns.Add("Des");
            RPD.STRCTURE.SYS.Data.dt_AlarmDisplay.Columns.Add("Time");
            //初始化WarningDisplay
            RPD.STRCTURE.SYS.Data.dt_WarningDisplay.Columns.Add("EQ");
            RPD.STRCTURE.SYS.Data.dt_WarningDisplay.Columns.Add("Type");
            RPD.STRCTURE.SYS.Data.dt_WarningDisplay.Columns.Add("Des");
            RPD.STRCTURE.SYS.Data.dt_WarningDisplay.Columns.Add("Time");

            this.Invoke(new EventHandler(UpdateAlarmLamp), "N");

            DisplayTimer.Start();

            


        }

        public void RefreshRecipe()
        {
            _SQL = @"SELECT NAME FROM RECIPE WHERE Enable = 1";
            clsDBN.GetDBtoCmb(_SQL, ref cmbRecipe);
            RPD.STRCTURE.SYS.Data.RefreshRecipeRequest = false;
        }


        private void TimerThread_Tick()
        {
            //_Timermutex.WaitOne();

            //lock (_ThreadTimer)
            //{
            while (true)
            {
                try
                {
                    EventRequest();
                    AlarmRequest();
                    WarningRequest();
                    AutomationDataRequest();
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

                Thread.Sleep(400);

            }

            //}

            //_Timermutex.ReleaseMutex();

        }

        private void ProcessDataTimer_Tick(object sender)
        {
            
            _Timermutex_PD.WaitOne();
            lock (_ThreadTimer_PD)
            {
                try
                {
                    _DateTimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    if (_t_DateTimeNow != _DateTimeNow)
                        _t_DateTimeNow = _DateTimeNow;
                    else
                        return;

                    if (RPD.STRCTURE.SYS.Btn.AutoModeSwitch.Lamp.Equals(1) || ckbForceRecord.Checked) //Auto或是強制紀錄時才寫DB
                    {
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
                                                                          
                                                                          ,[TRGaugeValuePH]
                                                                          ,[TRAY_ID]) 
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
                    }



                    //設定日期資料夾

                    string sPD_MFC_DateFolder = _PD_MFC + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
                    string sPD_Rgun_DateFolder = _PD_Rgun + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
                    string sPD_Pressure_DateFolder = _PD_Pressure + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
                    string sPD_PCW_DateFolder = _PD_PCW + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
                    string sPD_Transfer_DateFolder = _PD_Transfer + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";

                    if (!Directory.Exists(sPD_MFC_DateFolder))
                    {
                        Directory.CreateDirectory(sPD_MFC_DateFolder);
                    }
                    if (!Directory.Exists(sPD_Rgun_DateFolder))
                    {
                        Directory.CreateDirectory(sPD_Rgun_DateFolder);
                    }
                    if (!Directory.Exists(sPD_Pressure_DateFolder))
                    {
                        Directory.CreateDirectory(sPD_Pressure_DateFolder);
                    }
                    if (!Directory.Exists(sPD_PCW_DateFolder))
                    {
                        Directory.CreateDirectory(sPD_PCW_DateFolder);
                    }
                    if (!Directory.Exists(sPD_Transfer_DateFolder))
                    {
                        Directory.CreateDirectory(sPD_Transfer_DateFolder);
                    }

                    //設定csv file

                    string sPD_MFC = sPD_MFC_DateFolder + "MFC" + DateTime.Now.ToString("yyyy-MM-dd(HH)") + ".csv";
                    string sPD_Rgun = sPD_Rgun_DateFolder + "R-Gun" + DateTime.Now.ToString("yyyy-MM-dd(HH)") + ".csv";
                    string sPD_Pressure = sPD_Pressure_DateFolder + "Pressure" + DateTime.Now.ToString("yyyy-MM-dd(HH)") + ".csv";
                    string sPD_PCW = sPD_PCW_DateFolder + "PCW" + DateTime.Now.ToString("yyyy-MM-dd(HH)") + ".csv";
                    string sPD_Transfer = sPD_Transfer_DateFolder + "Transfer" + DateTime.Now.ToString("yyyy-MM-dd(HH)") + ".csv";

                    string sFileHeader = @"";
                    string sFileData = @"";

                    #region MFC
                    if (!File.Exists(sPD_MFC)) //建立檔案標頭
                    {
                        using (StreamWriter sw = new StreamWriter(sPD_MFC, false))
                        {
                            System.IO.FileInfo FileAttribute = new FileInfo(sPD_MFC);
                            FileAttribute.Attributes = FileAttributes.Normal;

                            sFileHeader = "";
                            sFileHeader += "Time,";
                            sFileHeader += "R-Gun Ar Flow Set Value,";
                            sFileHeader += "R-Gun Ar Flow,";
                            sFileHeader += "Chamber O2 Flow Set Value,";
                            sFileHeader += "Chamber O2 Flow,";
                            sFileHeader += "Chamber Ar Flow Set Value,";
                            sFileHeader += "Chamber Ar Flow,";

                            sFileHeader += "Chamber X Flow Set Value,";
                            sFileHeader += "Chamber X Flow,";

                            sFileHeader += "LL Ar Flow Set Value,";
                            sFileHeader += "LL Ar Flow,";
                            sFileHeader += "LL O2 Flow Set Value,";
                            sFileHeader += "LL O2 Flow,";

                            sFileHeader += "LL X Flow Set Value,";
                            sFileHeader += "LL X Flow,";

                            sFileHeader += "UL Ar Flow Set Value,";
                            sFileHeader += "UL Ar Flow,";
                            sFileHeader += "UL O2 Flow Set Value,";
                            sFileHeader += "UL O2 Flow,";

                            sFileHeader += "UL X Flow Set Value,";
                            sFileHeader += "UL X Flow,";

                            sw.Write(sFileHeader + Environment.NewLine);
                            sw.Close();
                        }
                    }
                    else
                    {
                        using (StreamWriter sw = new StreamWriter(sPD_MFC, true))
                        {
                            System.IO.FileInfo FileAttribute = new FileInfo(sPD_MFC);
                            FileAttribute.Attributes = FileAttributes.Normal;

                            sFileData = "";
                            sFileData += _DateTimeNow + ",";
                            sFileData += RPD.STRCTURE.TR.Map.TRMFCAr_GunSet + ",";
                            sFileData += RPD.STRCTURE.TR.Map.TRMFCAr_Gun_Flow + ",";
                            sFileData += RPD.STRCTURE.TR.Map.TRMFCO2Set + ",";
                            sFileData += RPD.STRCTURE.TR.Map.TRMFCO2Flow + ",";
                            sFileData += RPD.STRCTURE.TR.Map.TRMFCAr_ChamberSet + ",";
                            sFileData += RPD.STRCTURE.TR.Map.TRMFCAr_Chamber_Flow + ",";

                            sFileData += RPD.STRCTURE.TR.Map.TRMFCH2OSet + ",";
                            sFileData += RPD.STRCTURE.TR.Map.TRMFCH2OFlow + ",";

                            sFileData += RPD.STRCTURE.LL.Map.LLMFCArSet + ",";
                            sFileData += RPD.STRCTURE.LL.Map.LLMFCArFlow + ",";
                            sFileData += RPD.STRCTURE.LL.Map.LLMFCO2Set + ",";
                            sFileData += RPD.STRCTURE.LL.Map.LLMFCO2Flow + ",";

                            sFileData += RPD.STRCTURE.LL.Map.LLMFCXSet + ",";
                            sFileData += RPD.STRCTURE.LL.Map.LLMFCXFlow + ",";

                            sFileData += RPD.STRCTURE.UL.Map.ULMFCArSet + ",";
                            sFileData += RPD.STRCTURE.UL.Map.ULMFCArFlow + ",";
                            sFileData += RPD.STRCTURE.UL.Map.ULMFCO2Set + ",";
                            sFileData += RPD.STRCTURE.UL.Map.ULMFCO2Flow + ",";

                            sFileData += RPD.STRCTURE.UL.Map.ULMFCXSet + ",";
                            sFileData += RPD.STRCTURE.UL.Map.ULMFCXFlow + ",";

                            sw.Write(sFileData + Environment.NewLine);
                            sw.Close();
                        }
                    }
                    #endregion

                    #region R-Gun
                    if (!File.Exists(sPD_Rgun)) //建立檔案標頭
                    {
                        using (StreamWriter sw = new StreamWriter(sPD_Rgun, false))
                        {
                            System.IO.FileInfo FileAttribute = new FileInfo(sPD_Rgun);
                            FileAttribute.Attributes = FileAttributes.Normal;

                            sFileHeader = "";
                            sFileHeader += "Time,";
                            sFileHeader += "R-Gun Set Current of Hearth-Coil,";
                            sFileHeader += "R-Gun Current of Hearth-Coil,";
                            sFileHeader += "R-Gun Set Current of Streering-Coil,";
                            sFileHeader += "R-Gun Current of Streering-Coil,";
                            sFileHeader += "R-Gun Set Current of G2-Coil,";
                            sFileHeader += "R-Gun Current of G2-Coil,";
                            sFileHeader += "R-Gun Current of Main Hearth,";
                            sFileHeader += "R-Gun Current of Beam Guide,";
                            sFileHeader += "R-Gun Voltage of Main Hearth,";
                            sFileHeader += "R-Gun Voltage of Beam Guide,";
                            sFileHeader += "R-Gun Discharge Voltage,";
                            sFileHeader += "R-Gun MH Push Up Level,";



                            sw.Write(sFileHeader + Environment.NewLine);
                            sw.Close();
                        }
                    }
                    else
                    {
                        using (StreamWriter sw = new StreamWriter(sPD_Rgun, true))
                        {
                            System.IO.FileInfo FileAttribute = new FileInfo(sPD_Rgun);
                            FileAttribute.Attributes = FileAttributes.Normal;

                            sFileData = "";

                            sFileData += _DateTimeNow + ",";
                            sFileData += (Convert.ToInt32(clsPLC.ReadPLC_Word("D35400")) / 10) + ",";
                            sFileData += RPD.STRCTURE.PCC1.Data.RGunCurrentofHearthCoil + ",";
                            sFileData += (Convert.ToInt32(clsPLC.ReadPLC_Word("D35401")) / 10) + ",";
                            sFileData += RPD.STRCTURE.PCC1.Data.RGunCurrentofSteeringCoil + ",";
                            sFileData += (Convert.ToInt32(clsPLC.ReadPLC_Word("D35402")) / 10) + ",";
                            sFileData += RPD.STRCTURE.PCC1.Data.RGunCurrentofG2Coil + ",";
                            sFileData += RPD.STRCTURE.PCC1.Data.RGunCurrentofMainHearth + ",";
                            sFileData += RPD.STRCTURE.PCC1.Data.RGunCurrentofBeamGuide + ",";
                            sFileData += RPD.STRCTURE.PCC1.Data.RGunVoltageofMainHearth + ",";
                            sFileData += RPD.STRCTURE.PCC1.Data.RGunVoltageofBeamGuide + ",";
                            sFileData += RPD.STRCTURE.PCC1.Data.RGunDischargeVoltage + ",";
                            sFileData += RPD.STRCTURE.PCC1.Data.RGunMHpush_uplevel + ",";
                            sw.Write(sFileData + Environment.NewLine);
                            sw.Close();
                        }
                    }
                    #endregion

                    #region Pressure
                    if (!File.Exists(sPD_Pressure)) //建立檔案標頭
                    {
                        using (StreamWriter sw = new StreamWriter(sPD_Pressure, false))
                        {
                            System.IO.FileInfo FileAttribute = new FileInfo(sPD_Pressure);
                            FileAttribute.Attributes = FileAttributes.Normal;

                            sFileHeader = "";
                            sFileHeader += "Time,";
                            sFileHeader += "LL Pressure,";
                            sFileHeader += "LL Between TP And Ev-1 Pressure,";
                            sFileHeader += "LL Between exhaust DP And EV Pressure,";
                            sFileHeader += "TR(C) Pressure,";
                            sFileHeader += "TR(C) No-Offset Pressure,";
                            sFileHeader += "TR(PH) Pressure,";
                            sFileHeader += "TR Between TP And EV-3 Pressure,";    
                            sFileHeader += "UL Pressure,";
                            sFileHeader += "UL Between TP And EV-2 Pressure,";
                            sFileHeader += "SHI TSS Pressure,";
                            sFileHeader += "SHI Between TP And EV-4 Pressure,";
                            sFileHeader += "SHI Between DP And RV-4 Pressure,";
                            sw.Write(sFileHeader + Environment.NewLine);
                            sw.Close();
                        }
                    }
                    else
                    {
                        using (StreamWriter sw = new StreamWriter(sPD_Pressure, true))
                        {
                            System.IO.FileInfo FileAttribute = new FileInfo(sPD_Pressure);
                            FileAttribute.Attributes = FileAttributes.Normal;

                            sFileData = "";
                            sFileData += _DateTimeNow + ",";
                            sFileData += RPD.STRCTURE.LL.Map.LLGaugeValuePH + ",";
                            sFileData += RPD.STRCTURE.SYS.Map.LLTP_EV1_GaugeValueP + ",";
                            sFileData += RPD.STRCTURE.SYS.Map.DP_EV1_GaugeValueP + ",";
                            sFileData += RPD.STRCTURE.TR.Map.TRGaugeValueC + ",";
                            sFileData += RPD.STRCTURE.TR.Map.TRGaugeValueC_NoOffset + ",";
                            sFileData += RPD.STRCTURE.TR.Map.TRGaugeValuePH + ",";
                            sFileData += RPD.STRCTURE.SYS.Map.TR_TP_EV3_GaugeValueP + ",";   
                            sFileData += RPD.STRCTURE.UL.Map.ULGaugeValuePH + ",";
                            sFileData += RPD.STRCTURE.SYS.Map.UL_TP_EV2_GaugeValueP + ",";
                            sFileData += RPD.STRCTURE.PCC2.Map.TSS_GaugeValueP + ",";
                            sFileData += RPD.STRCTURE.PCC2.Map.TSS_TP_EV4_GaugeValueP + ",";
                            sFileData += RPD.STRCTURE.PCC2.Map.TSS_DP_EV4_GaugeValueP + ",";


                            sw.Write(sFileData + Environment.NewLine);
                            sw.Close();
                        }
                    }
                    #endregion

                    #region PCW
                    if (!File.Exists(sPD_PCW)) //建立檔案標頭
                    {
                        using (StreamWriter sw = new StreamWriter(sPD_PCW, false))
                        {
                            System.IO.FileInfo FileAttribute = new FileInfo(sPD_PCW);
                            FileAttribute.Attributes = FileAttributes.Normal;

                            sFileHeader = "";
                            sFileHeader += "Time,";
                            sFileHeader += "LL Dry Pump PCW Flow,";
                            sFileHeader += "LL Turbo Pump PCW Flow,";
                            sFileHeader += "Backing Dry Pump PCW Flow,";
                            sFileHeader += "PCC Door Inwall PCW Flow,";
                            sFileHeader += "RPD G1 Electrode PCW Flow,";
                            sFileHeader += "RPD G2 Electrode PCW Flow,";
                            sFileHeader += "RPD Streering Coil PCW Flow,";
                            sFileHeader += "RPD Main Hearth PCW Flow,";
                            sFileHeader += "RPD Beam Guide PCW Flow,";
                            sFileHeader += "PCC Chamber Inwall PCW Flow,";
                            sFileHeader += "PCC Turbo Pump PCW Flow,";
                            sFileHeader += "TR1 Cooling Plate PCW Flow,";
                            sFileHeader += "TR2 Chamber PCW Flow,";
                            sFileHeader += "TR3 Chamber PCW Flow,";
                            sFileHeader += "UL Turbo Pump PCW Flow,";
                            sFileHeader += "UL Dry Pump PCW Flow,";
                            sFileHeader += "PCW Temperature,";

                            sw.Write(sFileHeader + Environment.NewLine);
                            sw.Close();
                        }
                    }
                    else
                    {
                        using (StreamWriter sw = new StreamWriter(sPD_PCW, true))
                        {
                            System.IO.FileInfo FileAttribute = new FileInfo(sPD_PCW);
                            FileAttribute.Attributes = FileAttributes.Normal;

                            sFileData = "";
                            sFileData += _DateTimeNow + ",";
                            sFileData += RPD.STRCTURE.LL.Map.LLPumpFlowValue + ",";
                            sFileData += RPD.STRCTURE.LL.Map.LL_TP_PumpFlowValue + ",";
                            sFileData += RPD.STRCTURE.LL.Map.BackingFlowValue + ",";
                            sFileData += RPD.STRCTURE.PCC1.Map.DoorInWallFlowValue + ",";
                            sFileData += RPD.STRCTURE.PCC1.Map.PlasmaGeneratorG1FlowValue + ",";
                            sFileData += RPD.STRCTURE.PCC1.Map.PlasmaGeneratorG2FlowValue + ",";
                            sFileData += RPD.STRCTURE.PCC1.Map.PlasmaGeneratorSteeringCoilFlowValue + ",";
                            sFileData += RPD.STRCTURE.PCC1.Map.HearthMainFlowValue + ",";
                            sFileData += RPD.STRCTURE.PCC1.Map.HearthBeamFlowValue + ",";
                            sFileData += RPD.STRCTURE.PCC1.Map.ChamberInWallFlowValue + ",";
                            sFileData += RPD.STRCTURE.TR.Map.TR_TP_PumpFlowValue + ",";
                            sFileData += RPD.STRCTURE.TR.Map.TR2_CoolerFlowValue + ",";
                            sFileData += RPD.STRCTURE.TR.Map.TR2_CHB_COVERFlowValue + ",";
                            sFileData += RPD.STRCTURE.TR.Map.TR3_CHB_COVERFlowValue + ",";
                            sFileData += RPD.STRCTURE.UL.Map.ULTPPumpFlowValue + ",";
                            sFileData += RPD.STRCTURE.UL.Map.ULPumpFlowValue + ",";
                            sFileData += RPD.STRCTURE.SYS.Map.MainWaterTemp + ",";
                            sw.Write(sFileData + Environment.NewLine);
                            sw.Close();
                        }
                    }
                    #endregion

                    #region Transfer
                    if (!File.Exists(sPD_Transfer)) //建立檔案標頭
                    {
                        using (StreamWriter sw = new StreamWriter(sPD_Transfer, false))
                        {
                            System.IO.FileInfo FileAttribute = new FileInfo(sPD_Transfer);
                            FileAttribute.Attributes = FileAttributes.Normal;

                            sFileHeader = "";
                            sFileHeader += "Time,";
                            sFileHeader += "LL Transfer Speed,";
                            sFileHeader += "TR1 Transfer Speed,";
                            sFileHeader += "TR2 Transfer Speed,";
                            sFileHeader += "TR3 Transfer Speed,";
                            sFileHeader += "UL Transfer Speed,";
                            sFileHeader += "TR2 Tray In Detect Sensor,";
                            sFileHeader += "TR2 Tray Out Detect Sensor,";

                            sw.Write(sFileHeader + Environment.NewLine);
                            sw.Close();
                        }
                    }
                    else
                    {
                        using (StreamWriter sw = new StreamWriter(sPD_Transfer, true))
                        {
                            System.IO.FileInfo FileAttribute = new FileInfo(sPD_Transfer);
                            FileAttribute.Attributes = FileAttributes.Normal;

                            sFileData = "";
                            sFileData += _DateTimeNow + ",";
                            sFileData += RPD.STRCTURE.LL.Map.LLCVSpeed + ",";
                            sFileData += RPD.STRCTURE.TR.Map.TR1CVSpeed + ",";
                            sFileData += RPD.STRCTURE.TR.Map.TR2CVSpeed + ",";
                            sFileData += RPD.STRCTURE.TR.Map.TR3CVSpeed + ",";
                            sFileData += RPD.STRCTURE.UL.Map.ULCVSpeed + ",";
                            sFileData += RPD.STRCTURE.SYS.DI.TR2TrayExistBWD.Lamp + ","; //左邊
                            sFileData += RPD.STRCTURE.SYS.DI.TR2TrayExistFWD.Lamp + ","; //右邊

                            sw.Write(sFileData + Environment.NewLine);
                            sw.Close();
                        }
                    }
                    #endregion


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


            _Timermutex_PD.ReleaseMutex();

        }

        private void DisplayTimer_Tick(object sender, EventArgs e)
        {
            if (RPD.STRCTURE.SYS.Data.RefreshRecipeRequest)
                RefreshRecipe();

            lblTimer.Text = RPD.STRCTURE.SYS.Data.SystemYear.ToString("0000") + "/" + RPD.STRCTURE.SYS.Data.SystemMonth.ToString("00") + "/" + RPD.STRCTURE.SYS.Data.SystemDate.ToString("00")
                          + " " + RPD.STRCTURE.SYS.Data.SystemHour.ToString("00") + ":" + RPD.STRCTURE.SYS.Data.SystemMin.ToString("00") + ":" + RPD.STRCTURE.SYS.Data.SystemSec.ToString("00");

            switch (RPD.STRCTURE.SYS.Map.EQStatus)
            {
                case 1:
                    lblEQStatus.Text = "Run";
                    lblEQStatus.ForeColor = Color.Lime;
                    break;
                case 2:
                    lblEQStatus.Text = "Stop";
                    lblEQStatus.ForeColor = Color.Red;
                    break;
                case 3:
                    lblEQStatus.Text = "Idle";
                    lblEQStatus.ForeColor = Color.LightSteelBlue;
                    break;
                case 4:
                    lblEQStatus.Text = "Down";
                    lblEQStatus.ForeColor = Color.DarkGray;
                    break;
            }


            #region SYS Status Button

            if (_PLCAliveIndex.Equals(RPD.STRCTURE.SYS.Data.PLCAliveIndex))
                _PLCAliveReCount++;
            else
            {
                _PLCAliveReCount = 0;
                _PLCAliveIndex = RPD.STRCTURE.SYS.Data.PLCAliveIndex;
            }

            if (_PLCAliveReCount > 25)
            {
                os_PLC.FillColor = Color.Red;

                string Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                if (!RPD.STRCTURE.SYS.Data.PLCAliveAlarmRequest)
                {
                    RPD.STRCTURE.SYS.Data.PLCAliveAlarmRequest = true;
                    string SQL = @"INSERT INTO ALARM_LOG VALUES('SYS','A','PLC is not connectioned','" + Time + "','" + RPD.STRCTURE.SYS.Data.UserID + "','" + RPD.STRCTURE.SYS.Data.RecipeNumber + "')";
                    clsDBN.ExecuteSqlCommand(SQL);
                    this.Invoke(new EventHandler(Update_AlarmDes), "PLC is not connectioned");
                    this.Invoke(new EventHandler(UpdateAlarmLamp), "A");
                    DataRow dr_AlarmDisplay = RPD.STRCTURE.SYS.Data.dt_AlarmDisplay.NewRow();
                    dr_AlarmDisplay["EQ"] = "SYS";
                    dr_AlarmDisplay["Type"] = "A";
                    dr_AlarmDisplay["Des"] = "PLC is not connectioned";
                    dr_AlarmDisplay["Time"] = Time;
                    RPD.STRCTURE.SYS.Data.dt_AlarmDisplay.Rows.Add(dr_AlarmDisplay);
                    RPD.STRCTURE.SYS.Data.AlarmRequest = true;  //等Alarm Table 新增完後才能On
                }

                if (_PLCAliveReCount >= 32700)
                    _PLCAliveReCount = 26;
            }
            else
            {
                os_PLC.FillColor = Color.Lime;
                RPD.STRCTURE.SYS.Data.PLCAliveAlarmRequest = false;
            }


            int MDATimeSetTotal = Convert.ToInt16(frmP1.MDAHourSet) * 60 + Convert.ToInt16(frmP1.MDAMinSet);
            int MDATimeNowTotal = RPD.STRCTURE.PCC1.Data.RGunAccumulationTimeMainDischargehour * 60 + RPD.STRCTURE.PCC1.Data.RGunAccumulationTimeMainDischargemin;

            if (MDATimeSetTotal < MDATimeNowTotal) //已經超過
            {
                if (!RPD.STRCTURE.PCC1.Data.MDAWarningRequest)
                {
                    RPD.STRCTURE.PCC1.Data.MDAWarningRequest = true;
                    clsTool.OPInsertLog("SYS", "W", "Warning! Accumulation time is exceeded");
                    this.Invoke(new EventHandler(Update_AlarmDes), "Warning! Accumulation time is exceeded");
                    this.Invoke(new EventHandler(UpdateAlarmLamp), "W");
                    DataRow dr_WarningDisplay = RPD.STRCTURE.SYS.Data.dt_WarningDisplay.NewRow();
                    dr_WarningDisplay["EQ"] = "PCC";
                    dr_WarningDisplay["Type"] = "W";
                    dr_WarningDisplay["Des"] = "Warning! Accumulation time is exceeded";
                    dr_WarningDisplay["Time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    RPD.STRCTURE.SYS.Data.dt_WarningDisplay.Rows.Add(dr_WarningDisplay);
                    RPD.STRCTURE.SYS.Data.WarningRequest = true;  //等Warning Table 新增完後才能On

                }

            }
            else if (MDATimeSetTotal - 30 < MDATimeNowTotal) //即將過期
            {
                if (!RPD.STRCTURE.PCC1.Data.MDAWarningRequest)
                {
                    RPD.STRCTURE.PCC1.Data.MDAWarningRequest = true;
                    clsTool.OPInsertLog("SYS", "W", "Warning! Accumulation time almost reached");
                    this.Invoke(new EventHandler(Update_AlarmDes), "Warning! Accumulation time almost reached");
                    this.Invoke(new EventHandler(UpdateAlarmLamp), "W");
                    DataRow dr_WarningDisplay = RPD.STRCTURE.SYS.Data.dt_WarningDisplay.NewRow();
                    dr_WarningDisplay["EQ"] = "PCC";
                    dr_WarningDisplay["Type"] = "W";
                    dr_WarningDisplay["Des"] = "Warning! Accumulation time almost reached";
                    dr_WarningDisplay["Time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    RPD.STRCTURE.SYS.Data.dt_WarningDisplay.Rows.Add(dr_WarningDisplay);
                    RPD.STRCTURE.SYS.Data.WarningRequest = true; //等Warning Table 新增完後才能On
                }
            }
            else
            {
                RPD.STRCTURE.PCC1.Data.MDAWarningRequest = false;

            }

            if (RPD.STRCTURE.SYS.Btn.AlarmResetSwitch.Interlock.Equals(0) && RPD.STRCTURE.SYS.Data.PLCAliveAlarmRequest.Equals(false) && RPD.STRCTURE.PCC1.Data.MDAWarningRequest.Equals(false)) //表示無Alarm發生
                this.Invoke(new EventHandler(UpdateAlarmLamp), "N");

            clsTool.ButtonStatus(btnAuto, RPD.STRCTURE.SYS.Btn.AutoModeSwitch, 'B', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);

            clsTool.ButtonStatus(btnManual, RPD.STRCTURE.SYS.Btn.ManualModeSwitch, 'B', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            //clsTool.ButtonStatus(btnDummy, RPD.STRCTURE.SYS.Btn.DummyModeSwitch, 'B', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);  //取消此按鈕
            clsTool.ButtonStatus(btnStart, RPD.STRCTURE.SYS.Btn.StartSwitch, 'C', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.ButtonStatus(btnStop, RPD.STRCTURE.SYS.Btn.StopSwitch, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.ButtonStatus(btnInitial, RPD.STRCTURE.SYS.Btn.InitialSwitch, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.ButtonStatus(btnAbort, RPD.STRCTURE.SYS.Btn.AbortSwitch, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.ButtonStatus(btnBuzzerOff, RPD.STRCTURE.SYS.Btn.BuzzerOffSwitch, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.ButtonStatus(btnClear, RPD.STRCTURE.SYS.Btn.AlarmResetSwitch, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.ButtonStatus(btnTactTimeReset, RPD.STRCTURE.SYS.Btn.TactTimeReaetSwitch, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.ButtonStatus(btnLoadRecipe, RPD.STRCTURE.SYS.Btn.CurrentRecipeChangeSwitch, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            clsTool.ButtonStatus(btnForceExclude, RPD.STRCTURE.SYS.Btn.ForcedExcludeSwitch, 'B', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
            if (RPD.STRCTURE.SYS.Data.Level > 1)
            {
                ckbForceRecord.Checked = false;
                ckbForceRecord.Visible = false;
            }

            lblCurrentRecipe.Text = RPD.STRCTURE.SYS.Data.RecipeNumber;


            _RegisterEvent = true; //初始化事件


            //if (RPD.STRCTURE.SYS.Btn.AutoModeSwitch.Lamp.Equals(1) || ckbForceRecord.Checked) 
            //    _ThreadTimer_PD.Change(0, 800);
            //else
            //    _ThreadTimer_PD.Change(-1, -1);


            if (DateTime.Now.ToString("HHmmss") == "000000") //刪除六個月前的資料
            {
                clsDBN.ExecuteSqlCommand("DELETE FROM PROCESSDATA_LOG WHERE STR_DT < '" + DateTime.Now.AddMonths(-6).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }

            #endregion


        }

        #region Main Screen Button Event
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
            System.Environment.Exit(0);
        }

        private void btnEventLog_Click(object sender, EventArgs e)
        {
            //frmEventLog frmEvent = new frmEventLog();        
            SubShowForm(frmEvent);
        }

        private void btnMain_Click(object sender, EventArgs e)
        {
            SubShowForm(frmP1);
        }

        private void btnAlarm_Click(object sender, EventArgs e)
        {
            //frmAlarm frmAlarms = new frmAlarm();
            SubShowForm(frmAlarms);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            /* Button ----------- User
             1.Admin            1.Admin
             2.Manager          2.Manager
             3.User             3.User             
             */

            frmLogin frmLogins = new frmLogin();
            frmLogins.ShowDialog();
            if (frmLogins.DialogResult.Equals(DialogResult.Yes))
            {

                lblLoginID.Text = frmLogins.ID;
                RPD.STRCTURE.SYS.Data.UserID = frmLogins.ID;
                switch (frmLogins.Level.ToUpper())
                {
                    case "ADMIN":
                        RPD.STRCTURE.SYS.Data.Level = 1;
                        break;
                    case "MANAGER":
                        RPD.STRCTURE.SYS.Data.Level = 2;
                        break;
                    case "USER":
                        RPD.STRCTURE.SYS.Data.Level = 3;
                        break;
                    default:
                        RPD.STRCTURE.SYS.Data.Level = 3;
                        break;
                }

                clsTool.OPInsertLog("SYS", "O", "Login Successed");
                lblLoginID.ForeColor = Color.Red;
            }
            else
            {
                Application.Exit();
                System.Environment.Exit(0);

            }

        }

        private void btnLoadRecipe_Click(object sender, EventArgs e)
        {
            if (cmbRecipe.SelectedItem == null)
            {
                MessageBox.Show("Please Select Recipe ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                _SQL = @"SELECT * FROM RECIPE WHERE Name = '" + cmbRecipe.SelectedItem.ToString() + "'";
                DataRow dr_Recipe = clsDBN.GetRow(_SQL);
                if (dr_Recipe != null)
                {
                    //Recipe Data Load

                    //Add by Henry 2016/2/23 取消寫入名稱
                    //clsPLC.WriPLC_Word("D35399", dr_Recipe["Name"].ToString());
                    RPD.STRCTURE.SYS.Data.RecipeNumber = dr_Recipe["Name"].ToString();
                    clsPLC.WriPLC_Word("D35400", (Convert.ToDouble(dr_Recipe["HCC"]) * 10).ToString());
                    clsPLC.WriPLC_Word("D35401", (Convert.ToDouble(dr_Recipe["SCC"]) * 10).ToString());
                    clsPLC.WriPLC_Word("D35402", (Convert.ToDouble(dr_Recipe["G2CC"]) * 10).ToString());
                    clsPLC.WriPLC_Word("D35403", (Convert.ToDouble(dr_Recipe["BGDC"]) * 10).ToString());
                    clsPLC.WriPLC_Word("D35404", (Convert.ToDouble(dr_Recipe["MHDC"]) * 10).ToString());
                    clsPLC.WriPLC_Word("D35405", (Convert.ToDouble(dr_Recipe["TBPS"]) * 100).ToString());
                    clsPLC.WriPLC_Word("D35408", (Convert.ToDouble(dr_Recipe["CHCI"]) * 10).ToString());
                    clsPLC.WriPLC_Word("D35409", (Convert.ToDouble(dr_Recipe["CSCI"]) * 10).ToString());
                    clsPLC.WriPLC_Word("D35410", (Convert.ToDouble(dr_Recipe["CG2CI"]) * 10).ToString());
                    clsPLC.WriPLC_Word("D35411", (Convert.ToDouble(dr_Recipe["TBPSBGD"]) * 100).ToString());
                    clsPLC.WriPLC_Word("D35413", (Convert.ToDouble(dr_Recipe["RGV"]) * 10).ToString());
                    clsPLC.WriPLC_Word("D35414", (Convert.ToDouble(dr_Recipe["CO2V"]) * 10).ToString());
                    clsPLC.WriPLC_Word("D35415", (Convert.ToDouble(dr_Recipe["PCArV"]) * 10).ToString());

                    //Add by Henry 2016/1/22 新增Recipe TR X MFC 流量
                    clsPLC.WriPLC_Word("D35486", (Convert.ToDouble(dr_Recipe["TRXV"]) * 10).ToString());

                    clsPLC.IntToWord(Convert.ToInt32((Convert.ToDouble(dr_Recipe["PCP"]) * 1000000)), "D35435", "D35436");
                    clsPLC.WriPLC_Word("D35432", dr_Recipe["PCT"].ToString());

                    //Add by Henry 2016/1/22 新增Recipe LL/UL 目標壓力
                    clsPLC.IntToWord(Convert.ToInt32((Convert.ToDouble(dr_Recipe["LLULP"]) * 1000000)), "D35476", "D35477");


                    //PBT Data Load
                    _SQL = @"SELECT * FROM PBT WHERE Name = '" + dr_Recipe["PBT"].ToString() + "'";
                    DataRow dr_PBT = clsDBN.GetRow(_SQL);
                    if (dr_PBT != null)
                    {

                        clsPLC.WriPLC_Word("D35440", CheckZero((double)dr_PBT["LLO2Set1"] * 10));
                        clsPLC.WriPLC_Word("D35441", CheckZero((double)dr_PBT["LLArSet1"] * 10));
                        clsPLC.WriPLC_Word("D35442", CheckZero((double)dr_PBT["LLO2Set2"] * 10));
                        clsPLC.WriPLC_Word("D35443", CheckZero((double)dr_PBT["LLArSet2"] * 10));
                        clsPLC.WriPLC_Word("D35444", CheckZero((double)dr_PBT["LLO2Set3"] * 10));
                        clsPLC.WriPLC_Word("D35445", CheckZero((double)dr_PBT["LLArSet3"] * 10));

                        clsPLC.WriPLC_Word("D35467", CheckZero((double)dr_PBT["ULO2Set1"] * 10));
                        clsPLC.WriPLC_Word("D35468", CheckZero((double)dr_PBT["ULArSet1"] * 10));
                        clsPLC.WriPLC_Word("D35469", CheckZero((double)dr_PBT["ULO2Set2"] * 10));
                        clsPLC.WriPLC_Word("D35470", CheckZero((double)dr_PBT["ULArSet2"] * 10));
                        clsPLC.WriPLC_Word("D35471", CheckZero((double)dr_PBT["ULO2Set3"] * 10));
                        clsPLC.WriPLC_Word("D35472", CheckZero((double)dr_PBT["ULArSet3"] * 10));

                        clsPLC.WriPLC_Word("D35474", (Convert.ToDouble(dr_PBT["LLDelayTime"]) * 10).ToString()); //LLDelay Time
                        clsPLC.WriPLC_Word("D35475", (Convert.ToDouble(dr_PBT["TRDelayTime"]) * 10).ToString()); //TRDelay Time

                        clsPLC.WriPLC_Word("D35480", CheckZero((double)dr_PBT["LLXSet1"] * 10));
                        clsPLC.WriPLC_Word("D35481", CheckZero((double)dr_PBT["LLXSet2"] * 10));
                        clsPLC.WriPLC_Word("D35482", CheckZero((double)dr_PBT["LLXSet3"] * 10));
                        clsPLC.WriPLC_Word("D35483", CheckZero((double)dr_PBT["ULXSet1"] * 10));
                        clsPLC.WriPLC_Word("D35484", CheckZero((double)dr_PBT["ULXSet2"] * 10));
                        clsPLC.WriPLC_Word("D35485", CheckZero((double)dr_PBT["ULXSet3"] * 10));


                        clsPLC.WriPLC_Pulse(RPD.STRCTURE.SYS.Btn.CurrentRecipeChangeSwitch.Address);
                    }

                }

                else
                {
                    MessageBox.Show("Load Recipe Error", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        private string CheckZero(object objValue)
        {
            int ReturnValue = 0;
            try
            {
                ReturnValue = Convert.ToInt16(objValue);
            }
            catch
            {

            }

            return ReturnValue.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //_ThreadTimer.Change(-1, -1);

            this.Invoke(new EventHandler(UpdateAlarmLamp), "N");

            frmAlarms.btn_Clear_Click(sender, null);

            //_ThreadTimer.Change(500, 400);
        }

        #endregion

        #region Methods

        //Show Form
        private void SubShowForm(Form sForm)
        {
            if (sForm.Name != "frmInterLock")
                frmInterLock._IsRefreshIn = false;

            #region Set Button's Color
            Color SelColor = Color.LightGreen;
            Color UnSelColor = Color.LightSteelBlue;

            #endregion

            //Show Form
            #region Show Form
            bool bFlag = false;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == sForm.Name)
                {

                    f.Visible = true;
                    f.Activate();
                    f.Focus();
                    //return;
                    bFlag = true;

                }
                else
                {
                    f.Hide();
                }
            }

            if (bFlag == true) { return; }

            sForm.MdiParent = this;
            sForm.StartPosition = FormStartPosition.CenterScreen;
            sForm.Width = 3102;
            sForm.Height = 1847;
            sForm.Show();
            sForm.Focus();

            #endregion
        }

        //防止程式重覆開啓
        public static void ChkAppIsAlreadyRunning()
        {
            string aFormName = System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName;
            string aProcName = System.IO.Path.GetFileNameWithoutExtension(aFormName);
            if (System.Diagnostics.Process.GetProcessesByName(aProcName).Length > 1)
            {
                MessageBox.Show("RPD GUI was opened!!", "RPD", MessageBoxButtons.OK);
                Application.Exit();
                System.Environment.Exit(0);
            }
        }

        #endregion

        private void btnHandShake_Click(object sender, EventArgs e)
        {
            frmInterLock._IsRefreshIn = true;
            SubShowForm(frmInterLock);
        }

        private void Update_EventDes(object sender, EventArgs e)
        {
            string DES = (string)sender;
            txtEventMsg.Text = DES;
            Application.DoEvents();
        }

        private void Update_AlarmDes(object sender, EventArgs e)
        {
            string DES = (string)sender;
            txtAlarmMsg.Text = DES;
            Application.DoEvents();
        }

        private void UpdateAlarmLamp(object sender, EventArgs e)
        {
            string Lamp = (string)sender;
            switch (Lamp.ToUpper())
            {
                case "N":
                    os_Alarm.FillColor = Color.Lime;
                    os_Warn.FillColor = Color.Lime;
                    pnlAlarm.BackColor = Color.Lime;
                    txtAlarmMsg.Text = "";
                    lblAlarm.Text = "";
                    break;
                case "A":
                    os_Alarm.FillColor = Color.Red;
                    pnlAlarm.BackColor = Color.Red;
                    lblAlarm.Text = "Alarm";
                    break;
                case "W":
                    os_Warn.FillColor = Color.Red;
                    pnlAlarm.BackColor = Color.Yellow;
                    lblAlarm.Text = "Warning";
                    break;
                default:
                    os_Alarm.FillColor = Color.Red;
                    os_Warn.FillColor = Color.Red;
                    pnlAlarm.BackColor = Color.Red;
                    txtAlarmMsg.Text = "";
                    lblAlarm.Text = "";
                    break;
            }

            Application.DoEvents();

        }
        
        public void EventRequest()
        {
            RPD.STRCTURE.SYS.Map.EventLogFinish = clsPLC.ReadPLC_Bit("M16000");

            if (RPD.STRCTURE.SYS.Map.EventLogRequest.Equals(1) && RPD.STRCTURE.SYS.Map.EventLogFinish == 0)
            {
                string SQL = @"SELECT * FROM EVENT_DEF";
                DataTable dt_Event = clsDBN.GetTable(SQL);

                for (int i = 0; i < RPD.STRCTURE.SYS.Data.EventLog.Length; i++)
                {
                    if (RPD.STRCTURE.SYS.Data.EventLog[i] > 0)
                    {
                        for (int k = 0; k < 16; k++)
                        {
                            int Result = ((RPD.STRCTURE.SYS.Data.EventLog[i] & ((int)(Math.Pow(2, k)))) == 0 ? 0 : 1);
                            if (Result.Equals(1))
                            {
                                DataRow[] EventDes = dt_Event.Select("ARRAY_COUNT='" + i + "' AND ARRAY_INDEX ='" + k + "'");
                                if (EventDes.Length != 0)
                                {
                                    string Des = EventDes[0].ItemArray[3].ToString();
                                    SQL = @"INSERT INTO ALARM_LOG VALUES('" + EventDes[0].ItemArray[0] + "','E','" + EventDes[0].ItemArray[3] + "',GETDATE(),'" + RPD.STRCTURE.SYS.Data.UserID + "','" + RPD.STRCTURE.SYS.Data.RecipeNumber + "')";
                                    clsDBN.ExecuteSqlCommand(SQL);
                                    if (Des != "")
                                    {
                                        this.Invoke(new EventHandler(Update_EventDes), EventDes[0].ItemArray[3].ToString());
                                    }
                                    else
                                    {
                                        this.Invoke(new EventHandler(Update_EventDes), "This event has yet to be described");
                                    }
                                }
                            }
                        }
                    }
                }

                RPD.STRCTURE.SYS.Map.EventLogFinish = 1;
                clsPLC.WriPLC_Bit("M16000", true);

            }
            else if (RPD.STRCTURE.SYS.Map.EventLogRequest.Equals(0) && RPD.STRCTURE.SYS.Map.EventLogFinish == 1)
            {
                RPD.STRCTURE.SYS.Map.EventLogFinish = 0;
                clsPLC.WriPLC_Bit("M16000", false);
            }


        }

        public void AlarmRequest()
        {

            string SQL = @"SELECT * FROM ALARM_DEF";
            DataTable dt_Alarm = clsDBN.GetTable(SQL);

            for (int i = 0; i < RPD.STRCTURE.SYS.Data.Alarm.Length; i++)
            {
                if (RPD.STRCTURE.SYS.Data.Alarm[i] > 0)
                {

                    for (int k = 0; k < 16; k++)
                    {
                        int Result = ((RPD.STRCTURE.SYS.Data.Alarm[i] & ((int)(Math.Pow(2, k)))) == 0 ? 0 : 1);

                        if (RPD.STRCTURE.SYS.Data.AlarmTemp[i, k].Equals(0) && Result.Equals(1))
                        {
                            RPD.STRCTURE.SYS.Data.AlarmTemp[i, k] = Result;
                            DataRow[] AlarmDes = dt_Alarm.Select("ARRAY_COUNT='" + i + "' AND ARRAY_INDEX ='" + k + "'");
                            if (AlarmDes.Length != 0)
                            {
                                string Des = AlarmDes[0].ItemArray[3].ToString();
                                string Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                
                                DataRow dr_AlarmDisplay = RPD.STRCTURE.SYS.Data.dt_AlarmDisplay.NewRow();
                                dr_AlarmDisplay["EQ"] = AlarmDes[0].ItemArray[0];
                                dr_AlarmDisplay["Type"] = "A";
                                if (Des != "")
                                {
                                    dr_AlarmDisplay["Des"] = Des;
                                }
                                else
                                {
                                    int sNo = (Convert.ToInt16(AlarmDes[0].ItemArray[1]) * 16 + Convert.ToInt16(AlarmDes[0].ItemArray[2])) + 1;
                                    Des = "No." + sNo + " ,This alarm no description yet.";
                                    dr_AlarmDisplay["Des"] = Des;
                                }
                                dr_AlarmDisplay["Time"] = Time;

                                //Modify by Henry 2016/3/3 修改警報未顯示字
                                SQL = @"INSERT INTO ALARM_LOG VALUES('" + AlarmDes[0].ItemArray[0] + "','A','" + Des + "','" + Time + "','" + RPD.STRCTURE.SYS.Data.UserID + "','" + RPD.STRCTURE.SYS.Data.RecipeNumber + "')";
                                clsDBN.ExecuteSqlCommand(SQL);
                                this.Invoke(new EventHandler(Update_AlarmDes), Des);

                                RPD.STRCTURE.SYS.Data.dt_AlarmDisplay.Rows.Add(dr_AlarmDisplay);
                                RPD.STRCTURE.SYS.Data.AlarmRequest = true;  //等Alarm Table 新增完後才能On

                                this.Invoke(new EventHandler(UpdateAlarmLamp), "A");

                            }
                        }
                        else if (RPD.STRCTURE.SYS.Data.AlarmTemp[i, k].Equals(1) && Result.Equals(0))
                        {
                            RPD.STRCTURE.SYS.Data.AlarmTemp[i, k] = Result;
                        }

                    }
                }
            }



        }

        public void WarningRequest()
        {
            string SQL = @"SELECT * FROM WARNING_DEF";
            DataTable dt_Warning = clsDBN.GetTable(SQL);

            for (int i = 0; i < RPD.STRCTURE.SYS.Data.Warning.Length; i++)
            {
                if (RPD.STRCTURE.SYS.Data.Warning[i] > 0)
                {
                    for (int k = 0; k < 16; k++)
                    {
                        int Result = ((RPD.STRCTURE.SYS.Data.Warning[i] & ((int)(Math.Pow(2, k)))) == 0 ? 0 : 1);

                        if (RPD.STRCTURE.SYS.Data.WarningTemp[i, k].Equals(0) && Result.Equals(1))
                        {
                            RPD.STRCTURE.SYS.Data.WarningTemp[i, k] = Result;
                            DataRow[] WarningDes = dt_Warning.Select("ARRAY_COUNT='" + i + "' AND ARRAY_INDEX ='" + k + "'");
                            if (WarningDes.Length != 0)
                            {
                                string Des = WarningDes[0].ItemArray[3].ToString();
                                string Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                             
                                DataRow dr_WarningDisplay = RPD.STRCTURE.SYS.Data.dt_WarningDisplay.NewRow();
                                dr_WarningDisplay["EQ"] = WarningDes[0].ItemArray[0];
                                dr_WarningDisplay["Type"] = "W";
                                if (Des != "")
                                {
                                    dr_WarningDisplay["Des"] = Des;
                                }
                                else
                                {
                                    int sNo = (Convert.ToInt16(WarningDes[0].ItemArray[1]) * 16 + Convert.ToInt16(WarningDes[0].ItemArray[2])) + 1;
                                    Des = "No." + sNo + " ,This warning no description yet.";
                                    dr_WarningDisplay["Des"] = Des;
                                  
                                }
                                dr_WarningDisplay["Time"] = Time;

                                //Modify by Henry 2016/3/3 修改警報未顯示字
                                SQL = @"INSERT INTO ALARM_LOG VALUES('" + WarningDes[0].ItemArray[0] + "','W','" + Des + "','" + Time + "','" + RPD.STRCTURE.SYS.Data.UserID + "','" + RPD.STRCTURE.SYS.Data.RecipeNumber + "')";
                                clsDBN.ExecuteSqlCommand(SQL);
                                this.Invoke(new EventHandler(Update_AlarmDes), Des);

                                RPD.STRCTURE.SYS.Data.dt_WarningDisplay.Rows.Add(dr_WarningDisplay);
                                RPD.STRCTURE.SYS.Data.WarningRequest = true;  //等Warning Table 新增完後才能On
                                this.Invoke(new EventHandler(UpdateAlarmLamp), "W");

                            }
                        }
                        else if (RPD.STRCTURE.SYS.Data.WarningTemp[i, k].Equals(1) && Result.Equals(0))
                        {
                            RPD.STRCTURE.SYS.Data.WarningTemp[i, k] = Result;
                        }

                    }
                }
            }


        }

        public void AutomationDataRequest()
        {
            _DateTimeNow_Automation = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (_t_DateTimeNow_Automation != _DateTimeNow_Automation)
                _t_DateTimeNow_Automation = _DateTimeNow_Automation;
            else
                return;

            string SQL_A = "";

            if (RPD.STRCTURE.LL.Map.Record_LLReadyToGo.Equals(1))
            {
                clsPLC.WriPLC_Bit("M20010", false);

                string InserTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SQL_A = @"INSERT INTO AUTOMATIONDATA_LOG(EQ,DATA,INSERTTIME,RECIPE_NUMBER,TRAY_ID) VALUES('1','" + clsTool.DisplayGauge(RPD.STRCTURE.LL.Data.Record_LLReadyToGo_P) + "','" + InserTime + "','" + RPD.STRCTURE.SYS.Data.RecipeNumber + "','')";
                clsDBN.ExecuteSqlCommand(SQL_A);

                SQL_A = @"INSERT INTO AUTOMATIONDATA_LOG(EQ,DATA,INSERTTIME,RECIPE_NUMBER,TRAY_ID) VALUES('2','" + RPD.STRCTURE.LL.Data.Record_LLMFCO2_Flow + "','" + InserTime + "','" + RPD.STRCTURE.SYS.Data.RecipeNumber + "','')";
                clsDBN.ExecuteSqlCommand(SQL_A);

                SQL_A = @"INSERT INTO AUTOMATIONDATA_LOG(EQ,DATA,INSERTTIME,RECIPE_NUMBER,TRAY_ID) VALUES('3','" + RPD.STRCTURE.LL.Data.Record_LLMFCAr_Flow + "','" + InserTime + "','" + RPD.STRCTURE.SYS.Data.RecipeNumber + "','')";
                clsDBN.ExecuteSqlCommand(SQL_A);
            }

            if (RPD.STRCTURE.LL.Map.Record_LLReadyToVent.Equals(1))
            {
                clsPLC.WriPLC_Bit("M20014", false);
                SQL_A = @"INSERT INTO AUTOMATIONDATA_LOG(EQ,DATA,INSERTTIME,RECIPE_NUMBER,TRAY_ID) VALUES('4','" + clsTool.DisplayGauge(RPD.STRCTURE.LL.Data.Record_LLReadyToVent_P) + "',GETDATE(),'" + RPD.STRCTURE.SYS.Data.RecipeNumber + "','')";
                clsDBN.ExecuteSqlCommand(SQL_A);

            }

            if (RPD.STRCTURE.LL.Map.Record_LLPumpDownTo50m.Equals(1))
            {
                clsPLC.WriPLC_Bit("M20016", false);
                SQL_A = @"INSERT INTO AUTOMATIONDATA_LOG(EQ,DATA,INSERTTIME,RECIPE_NUMBER,TRAY_ID) VALUES('5','" + RPD.STRCTURE.LL.Data.Record_LLPumpDownTo50m_Time + "',GETDATE(),'" + RPD.STRCTURE.SYS.Data.RecipeNumber + "','')";
                clsDBN.ExecuteSqlCommand(SQL_A);

            }

            if (RPD.STRCTURE.LL.Map.Record_LLPumpDownTo1m.Equals(1))
            {
                clsPLC.WriPLC_Bit("M20017", false);
                SQL_A = @"INSERT INTO AUTOMATIONDATA_LOG(EQ,DATA,INSERTTIME,RECIPE_NUMBER,TRAY_ID) VALUES('6','" + RPD.STRCTURE.LL.Data.Record_LLPumpDownTo1m_Time + "',GETDATE(),'" + RPD.STRCTURE.SYS.Data.RecipeNumber + "','')";
                clsDBN.ExecuteSqlCommand(SQL_A);

            }


            string SQL_B = "";

            if (RPD.STRCTURE.UL.Map.Record_ULReadyToGo.Equals(1))
            {
                clsPLC.WriPLC_Bit("M20020", false);
                string InserTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                SQL_B = @"INSERT INTO AUTOMATIONDATA_LOG(EQ,DATA,INSERTTIME,RECIPE_NUMBER,TRAY_ID) VALUES('7','" + clsTool.DisplayGauge(RPD.STRCTURE.UL.Data.Record_ULReadyToGo_P) + "','" + InserTime + "','" + RPD.STRCTURE.SYS.Data.RecipeNumber + "','')";
                clsDBN.ExecuteSqlCommand(SQL_B);

                SQL_B = @"INSERT INTO AUTOMATIONDATA_LOG(EQ,DATA,INSERTTIME,RECIPE_NUMBER,TRAY_ID) VALUES('8','" + RPD.STRCTURE.UL.Data.Record_ULMFCO2_Flow + "','" + InserTime + "','" + RPD.STRCTURE.SYS.Data.RecipeNumber + "','')";
                clsDBN.ExecuteSqlCommand(SQL_B);

                SQL_B = @"INSERT INTO AUTOMATIONDATA_LOG(EQ,DATA,INSERTTIME,RECIPE_NUMBER,TRAY_ID) VALUES('9','" + RPD.STRCTURE.UL.Data.Record_ULMFCAr_Flow + "','" + InserTime + "','" + RPD.STRCTURE.SYS.Data.RecipeNumber + "','')";
                clsDBN.ExecuteSqlCommand(SQL_B);


            }

            if (RPD.STRCTURE.UL.Map.Record_ULReadyToVent.Equals(1))
            {
                clsPLC.WriPLC_Bit("M20024", false);
                SQL_B = @"INSERT INTO AUTOMATIONDATA_LOG(EQ,DATA,INSERTTIME,RECIPE_NUMBER,TRAY_ID) VALUES('10','" + clsTool.DisplayGauge(RPD.STRCTURE.UL.Data.Record_ULReadyToVent_P) + "',GETDATE(),'" + RPD.STRCTURE.SYS.Data.RecipeNumber + "','')";
                clsDBN.ExecuteSqlCommand(SQL_B);

            }

            if (RPD.STRCTURE.UL.Map.Record_ULPumpDownTo50m.Equals(1))
            {
                clsPLC.WriPLC_Bit("M20026", false);
                SQL_B = @"INSERT INTO AUTOMATIONDATA_LOG(EQ,DATA,INSERTTIME,RECIPE_NUMBER,TRAY_ID) VALUES('11','" + RPD.STRCTURE.UL.Data.Record_ULPumpDownTo50m_Time + "',GETDATE(),'" + RPD.STRCTURE.SYS.Data.RecipeNumber + "','')";
                clsDBN.ExecuteSqlCommand(SQL_B);

            }

            if (RPD.STRCTURE.UL.Map.Record_ULPumpDownTo1m.Equals(1))
            {
                clsPLC.WriPLC_Bit("M20027", false);
                SQL_B = @"INSERT INTO AUTOMATIONDATA_LOG(EQ,DATA,INSERTTIME,RECIPE_NUMBER,TRAY_ID) VALUES('12','" + RPD.STRCTURE.UL.Data.Record_ULPumpDownTo1m_Time + "',GETDATE(),'" + RPD.STRCTURE.SYS.Data.RecipeNumber + "','')";
                clsDBN.ExecuteSqlCommand(SQL_B);

            }

        }

        private void btnManualPage_Click(object sender, EventArgs e)
        {
            if (RPD.STRCTURE.SYS.Btn.AutoModeSwitch.Lamp.Equals(1)) //自動模式不能按手動頁面功能
            {
                MessageBox.Show("Auto Mode donot accept manual operation!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            frmManualPage f = new frmManualPage();
            btnManualPage.Enabled = false;
            f.FormClosed += new FormClosedEventHandler(frmManualPage_FormClosed);
            f.Show();
        }

        void frmManualPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            btnManualPage.Enabled = true;
        }

        private void picArchers_Click(object sender, EventArgs e)
        {
            frmArchersTest f = new frmArchersTest();
            f.ShowDialog();
        }


    }
}
