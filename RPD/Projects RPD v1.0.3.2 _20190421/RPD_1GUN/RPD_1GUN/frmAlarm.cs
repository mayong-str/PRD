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
    public partial class frmAlarm : Form
    {
        bool _RegisterEvent = false;
        int _dtAlarmCount = 0;
        int _dtWarningCount = 0;
    
       
        public frmAlarm()
        {
            InitializeComponent();
        }

        private void frmAlarm_Load(object sender, EventArgs e)
        {
            DisplayTimer.Enabled = true;
            
        }

        public void btn_Clear_Click(object sender, EventArgs e)
        {
            DisplayTimer.Enabled = false;

            RPD.STRCTURE.SYS.Data.dt_AlarmDisplay.Rows.Clear();
            RPD.STRCTURE.SYS.Data.dt_WarningDisplay.Rows.Clear();

            RPD.STRCTURE.SYS.Data.AlarmTemp = new int[32, 16]; //陣列歸0 
            RPD.STRCTURE.SYS.Data.WarningTemp = new int[32, 16]; //陣列歸0 
            RPD.STRCTURE.SYS.Data.PLCAliveAlarmRequest = false;
            RefreshDisplayAlarm();       


            clsPLC.WriPLC_Pulse(RPD.STRCTURE.SYS.Btn.AlarmResetSwitch.Address);

            DisplayTimer.Enabled = true;
        }

        private void DisplayTimer_Tick(object sender, EventArgs e)
        {
            if (!_RegisterEvent)
            {
                RefreshDisplayAlarm();
            }
            else
            {
                if (RPD.STRCTURE.SYS.Data.AlarmRequest || RPD.STRCTURE.SYS.Data.WarningRequest)
                {
                    RefreshDisplayAlarm();
                }
            }


            clsTool.ButtonStatus(btn_Clear, RPD.STRCTURE.SYS.Btn.AlarmResetSwitch, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);
         
            _RegisterEvent = true;



        }

        private void RefreshDisplayAlarm()
        {
            _dtAlarmCount = RPD.STRCTURE.SYS.Data.dt_AlarmDisplay.Rows.Count;
            _dtWarningCount = RPD.STRCTURE.SYS.Data.dt_WarningDisplay.Rows.Count;

            dgvSYS.Rows.Clear();
            dgvEF.Rows.Clear();
            dgvLL.Rows.Clear();
            dgvTR.Rows.Clear();
            dgvUL.Rows.Clear();
            dgvDF.Rows.Clear();
            dgvPCC.Rows.Clear();

            int[] AlarmCount = new int[8];
            if (RPD.STRCTURE.SYS.Data.dt_AlarmDisplay != null && RPD.STRCTURE.SYS.Data.dt_AlarmDisplay.Rows.Count != 0)
            {
                try
                {
                    foreach (DataRow dr_AlarmDisplay in RPD.STRCTURE.SYS.Data.dt_AlarmDisplay.Rows)
                    {
                        switch (dr_AlarmDisplay["EQ"].ToString().ToUpper())
                        {
                            case "SYS":
                                string[] row = new string[] { dr_AlarmDisplay["EQ"].ToString(), dr_AlarmDisplay["Type"].ToString(), dr_AlarmDisplay["Des"].ToString(), dr_AlarmDisplay["Time"].ToString() };
                                dgvSYS.Rows.Add(row);
                                AlarmCount[0]++;
                                break;
                            case "EF":
                                row = new string[] { dr_AlarmDisplay["EQ"].ToString(), dr_AlarmDisplay["Type"].ToString(), dr_AlarmDisplay["Des"].ToString(), dr_AlarmDisplay["Time"].ToString() };
                                dgvEF.Rows.Add(row);
                                AlarmCount[1]++;
                                break;
                            case "LL":
                                row = new string[] { dr_AlarmDisplay["EQ"].ToString(), dr_AlarmDisplay["Type"].ToString(), dr_AlarmDisplay["Des"].ToString(), dr_AlarmDisplay["Time"].ToString() };
                                dgvLL.Rows.Add(row);
                                AlarmCount[2]++;
                                break;
                            case "TR":
                                row = new string[] { dr_AlarmDisplay["EQ"].ToString(), dr_AlarmDisplay["Type"].ToString(), dr_AlarmDisplay["Des"].ToString(), dr_AlarmDisplay["Time"].ToString() };
                                dgvTR.Rows.Add(row);
                                AlarmCount[3]++;
                                break;
                            case "UL":
                                row = new string[] { dr_AlarmDisplay["EQ"].ToString(), dr_AlarmDisplay["Type"].ToString(), dr_AlarmDisplay["Des"].ToString(), dr_AlarmDisplay["Time"].ToString() };
                                dgvUL.Rows.Add(row);
                                AlarmCount[4]++;
                                break;
                            case "DF":
                                row = new string[] { dr_AlarmDisplay["EQ"].ToString(), dr_AlarmDisplay["Type"].ToString(), dr_AlarmDisplay["Des"].ToString(), dr_AlarmDisplay["Time"].ToString() };
                                dgvDF.Rows.Add(row);
                                AlarmCount[5]++;
                                break;
                            case "PCC":
                                row = new string[] { dr_AlarmDisplay["EQ"].ToString(), dr_AlarmDisplay["Type"].ToString(), dr_AlarmDisplay["Des"].ToString(), dr_AlarmDisplay["Time"].ToString() };
                                dgvPCC.Rows.Add(row);
                                AlarmCount[6]++;
                                break;

                        }
                    }
                }
                catch
                {

                }

            }

            if (RPD.STRCTURE.SYS.Data.dt_WarningDisplay != null && RPD.STRCTURE.SYS.Data.dt_WarningDisplay.Rows.Count != 0)
            {
                try
                {
                    foreach (DataRow dr_WarningDisplay in RPD.STRCTURE.SYS.Data.dt_WarningDisplay.Rows)
                    {
                        switch (dr_WarningDisplay["EQ"].ToString().ToUpper())
                        {
                            case "SYS":
                                string[] row = new string[] { dr_WarningDisplay["EQ"].ToString(), dr_WarningDisplay["Type"].ToString(), dr_WarningDisplay["Des"].ToString(), dr_WarningDisplay["Time"].ToString() };
                                dgvSYS.Rows.Add(row);
                                AlarmCount[0]++;
                                break;
                            case "EF":
                                row = new string[] { dr_WarningDisplay["EQ"].ToString(), dr_WarningDisplay["Type"].ToString(), dr_WarningDisplay["Des"].ToString(), dr_WarningDisplay["Time"].ToString() };
                                dgvEF.Rows.Add(row);
                                AlarmCount[1]++;
                                break;
                            case "LL":
                                row = new string[] { dr_WarningDisplay["EQ"].ToString(), dr_WarningDisplay["Type"].ToString(), dr_WarningDisplay["Des"].ToString(), dr_WarningDisplay["Time"].ToString() };
                                dgvLL.Rows.Add(row);
                                AlarmCount[2]++;
                                break;
                            case "TR":
                                row = new string[] { dr_WarningDisplay["EQ"].ToString(), dr_WarningDisplay["Type"].ToString(), dr_WarningDisplay["Des"].ToString(), dr_WarningDisplay["Time"].ToString() };
                                dgvTR.Rows.Add(row);
                                AlarmCount[3]++;
                                break;
                            case "UL":
                                row = new string[] { dr_WarningDisplay["EQ"].ToString(), dr_WarningDisplay["Type"].ToString(), dr_WarningDisplay["Des"].ToString(), dr_WarningDisplay["Time"].ToString() };
                                dgvUL.Rows.Add(row);
                                AlarmCount[4]++;
                                break;
                            case "DF":
                                row = new string[] { dr_WarningDisplay["EQ"].ToString(), dr_WarningDisplay["Type"].ToString(), dr_WarningDisplay["Des"].ToString(), dr_WarningDisplay["Time"].ToString() };
                                dgvDF.Rows.Add(row);
                                AlarmCount[5]++;
                                break;
                            case "PCC":
                                row = new string[] { dr_WarningDisplay["EQ"].ToString(), dr_WarningDisplay["Type"].ToString(), dr_WarningDisplay["Des"].ToString(), dr_WarningDisplay["Time"].ToString() };
                                dgvPCC.Rows.Add(row);
                                AlarmCount[6]++;
                                break;

                        }
                    }
                }
                catch
                {

                }
            }

            tabAlarm.TabPages[0].Text = ("SYS(" + AlarmCount[0] + ")").Replace("(0)", "");
            tabAlarm.TabPages[1].Text = ("EF(" + AlarmCount[1] + ")").Replace("(0)", "");
            tabAlarm.TabPages[2].Text = ("LL(" + AlarmCount[2] + ")").Replace("(0)", "");
            tabAlarm.TabPages[3].Text = ("TR(" + AlarmCount[3] + ")").Replace("(0)", "");
            tabAlarm.TabPages[4].Text = ("UL(" + AlarmCount[4] + ")").Replace("(0)", "");
            tabAlarm.TabPages[5].Text = ("DF(" + AlarmCount[5] + ")").Replace("(0)", "");
            tabAlarm.TabPages[6].Text = ("PCC(" + AlarmCount[6] + ")").Replace("(0)", "");

            dgvSYS.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEF.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLL.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTR.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUL.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDF.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPCC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                                 

            if(_dtAlarmCount == RPD.STRCTURE.SYS.Data.dt_AlarmDisplay.Rows.Count)
                RPD.STRCTURE.SYS.Data.AlarmRequest = false;

            if(_dtWarningCount == RPD.STRCTURE.SYS.Data.dt_WarningDisplay.Rows.Count)
                RPD.STRCTURE.SYS.Data.WarningRequest = false;


        }
       
        private bool CompareDataTable(DataTable dtA, DataTable dtB)
        {
            if (dtA.Rows.Count == dtB.Rows.Count)
            {                
                    for (int i = 0; i < dtA.Rows.Count; i++)
                    {
                        for (int j = 0; j < dtA.Columns.Count; j++)
                        {
                            if (!dtA.Rows[i][j].Equals(dtB.Rows[i][j]))
                            {
                                return false;
                            }
                        }
                    }
                    return true;                
            }
            else
            {
                return false;
            }
        }
        


    }
}
