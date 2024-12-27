using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.VisualBasic.PowerPacks;
using Microsoft.VisualCSharp.PowerPacks.Valve;
using Microsoft.VisualCSharp.PowerPacks.Pump;
using Microsoft.VisualCSharp.PowerPacks.Gauge;

namespace RPD_1GUN
{
    class clsTool
    {

        //異常視窗
        public static void ShowErrMsg(string sMessage)
        {
            System.Windows.Forms.MessageBox.Show(sMessage,
                                                 "RPD",
                                                 System.Windows.Forms.MessageBoxButtons.OK,
                                                 System.Windows.Forms.MessageBoxIcon.Error);
        }

        //警告視窗
        public static void ShowWarningMsg(string sMessage)
        {

            //Msg.ShowDialog();
            System.Windows.Forms.MessageBox.Show(sMessage,
                                                 "RPD",
                                                 System.Windows.Forms.MessageBoxButtons.OK,
                                                 System.Windows.Forms.MessageBoxIcon.Warning);
        }

        //提示視窗
        public static void ShowInformationMsg(string sMessage)
        {
            System.Windows.Forms.MessageBox.Show(sMessage,
                                             "RPD",
                                             System.Windows.Forms.MessageBoxButtons.OK,
                                             System.Windows.Forms.MessageBoxIcon.Information);

        }

        //選擇視窗 
        public static bool ShowQuestionMsg(string sMessage)
        {
            if (System.Windows.Forms.MessageBox.Show(sMessage,
                                                     "RPD",
                                                     System.Windows.Forms.MessageBoxButtons.OKCancel,
                                                     System.Windows.Forms.MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string DisplayGauge(double Value)
        {
            return Value.ToString("E2").Replace("00", "");
        }

        #region 各種筏件幫浦開關Function
        public static void ButtonStatus(Button btn, RPD.Btns btnTag, char Type, bool RegisterEvent, int BtnLevel, int UserLevel)
        {
            /* Button ----------- User
             1.Admin            1.Admin
             2.Manager          2.Manager
             3.User             3.User             
             */
            if (BtnLevel >= UserLevel)
                btn.Visible = true;
            else
                btn.Visible = false;

            if (btnTag == null) //若沒有Tag就不做下列判斷
                return;

            if (btnTag.Interlock.Equals(1))
                btn.Enabled = true;
            else
                btn.Enabled = false;

            if (btnTag.Lamp.Equals(1))
                btn.BackColor = Color.Lime;
            else
                btn.BackColor = Color.Transparent;


            if (!RegisterEvent)
            {
                switch (Type)
                {
                    case 'A':
                        btn.Tag = btnTag.Address;
                        btn.Click += new EventHandler(A_Btn_Click);
                        break;
                    case 'B':
                        btn.Tag = btnTag.Address;
                        btn.Click += new EventHandler(B_Btn_Click);
                        break;
                    case 'C':
                        btn.Tag = btnTag.Address;
                        btn.Click += new EventHandler(C_Btn_Click);
                        break;
                }
            }

        }
        private static void A_Btn_Click(object sender, EventArgs e)
        {
            Button SBtn = (Button)sender;

            //一般按鈕On 一個 Pulse
            clsPLC.WriPLC_Pulse(SBtn.Tag.ToString());
        }
        private static void B_Btn_Click(object sender, EventArgs e)
        {
            Button SBtn = (Button)sender;

            //0->1 1->0 按鈕
            if (SBtn.BackColor.Equals(Color.Transparent))
                clsPLC.WriPLC_Bit(SBtn.Tag.ToString(), true);
            else
                clsPLC.WriPLC_Bit(SBtn.Tag.ToString(), false);
        }
        //Add by Henry 2016/2/23
        private static void C_Btn_Click(object sender, EventArgs e)
        {
            Button SBtn = (Button)sender;

            DialogResult myResult = MessageBox.Show("Please make sure recipe is correct ,\n click 'Yes' button to start process.", "observe", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (myResult == DialogResult.Yes)
            {
                //一般按鈕On 一個 Pulse
                clsPLC.WriPLC_Pulse(SBtn.Tag.ToString());
            }
            else if (myResult == DialogResult.No)
            {
                //Nothing
            }
        }
        public static void PVStatus(PneumaticValve PV, RPD.Btns OpenTag, RPD.Btns CloseTag, bool RegisterEvent, int BtnLevel, int UserLevel)
        {
            /* Button ----------- User
            1.Admin            1.Admin
            2.Manager          2.Manager
            3.User             3.User             
            */
            string sLevel = "LEVEL:";
            if (BtnLevel >= UserLevel)
                sLevel += "1";
            else
                sLevel += "0";

            string sOpen = "OPEN:";
            if (OpenTag.Interlock.Equals(1))
                sOpen += "1";
            else
                sOpen += "0";

            string sClose = "CLOSE:";
            if (CloseTag.Interlock.Equals(1))
                sClose += "1";
            else
                sClose += "0";

            if (OpenTag.Lamp.Equals(1) && CloseTag.Lamp.Equals(0))
                PV.Signal = 1;
            else
                PV.Signal = 0;

            PV.Tag = OpenTag.Address + "," + CloseTag.Address + "," + sOpen + "," + sClose + "," + sLevel;

            if (!RegisterEvent)
            {
                PV.Click += new EventHandler(PV_Btn_Click);
            }

        }
        private static void PV_Btn_Click(object sender, EventArgs e)
        {
            PneumaticValve SPV = (PneumaticValve)sender;
            string[] Tag = SPV.Tag.ToString().Split(',');

            if (RPD.STRCTURE.SYS.Btn.AutoModeSwitch.Lamp.Equals(1))
            {
                MessageBox.Show("Auto Mode donot accept manual operation!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Tag[4].Equals("LEVEL:0"))
            {
                MessageBox.Show("You can not access this item \nPlease call manager to give you authority", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (SPV.Signal.Equals(0) && Tag[2].Equals("OPEN:0"))
            {
                string Msg = GetInterLockMsg(Tag[0]);
                MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (SPV.Signal.Equals(1) && Tag[3].Equals("CLOSE:0"))
            {
                string Msg = GetInterLockMsg(Tag[1]);
                MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            if (SPV.Signal.Equals(1))
                clsPLC.WriPLC_Pulse(Tag[1]); // Close
            else
                clsPLC.WriPLC_Pulse(Tag[0]); // Open

            OPInsertLog("SYS", "O", SPV.Name.Replace("pv_", "") + " is Click");
        }
        public static void VPStatus(VaccumPump VP, RPD.Btns OpenTag, RPD.Btns CloseTag, bool RegisterEvent, int BtnLevel, int UserLevel)
        {
            /* Button ----------- User
            1.Admin            1.Admin
            2.Manager          2.Manager
            3.User             3.User             
            */
            string sLevel = "LEVEL:";
            if (BtnLevel >= UserLevel)
                sLevel += "1";
            else
                sLevel += "0";

            string sOpen = "OPEN:";
            if (OpenTag.Interlock.Equals(1))
                sOpen += "1";
            else
                sOpen += "0";

            string sClose = "CLOSE:";
            if (CloseTag.Interlock.Equals(1))
                sClose += "1";
            else
                sClose += "0";

            if (OpenTag.Lamp.Equals(1) && CloseTag.Lamp.Equals(0))
                VP.Signal = 1;
            else
                VP.Signal = 0;

            VP.Tag = OpenTag.Address + "," + CloseTag.Address + "," + sOpen + "," + sClose + "," + sLevel;

            if (!RegisterEvent)
            {
                VP.Click += new EventHandler(VP_Btn_Click);
            }

        }
        private static void VP_Btn_Click(object sender, EventArgs e)
        {
            VaccumPump SVP = (VaccumPump)sender;

            string[] Tag = SVP.Tag.ToString().Split(',');

            if (RPD.STRCTURE.SYS.Btn.AutoModeSwitch.Lamp.Equals(1))
            {
                MessageBox.Show("Auto Mode donot accept manual operation!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Tag[4].Equals("LEVEL:0"))
            {
                MessageBox.Show("You can not access this item \nPlease call manager to give you authority", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (SVP.Signal.Equals(0) && Tag[2].Equals("OPEN:0"))
            {
                string Msg = GetInterLockMsg(Tag[0]);
                MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (SVP.Signal.Equals(1) && Tag[3].Equals("CLOSE:0"))
            {
                string Msg = GetInterLockMsg(Tag[1]);
                MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            if (SVP.Signal.Equals(1))
                clsPLC.WriPLC_Pulse(Tag[1]);
            else
                clsPLC.WriPLC_Pulse(Tag[0]);

            OPInsertLog("SYS", "O", SVP.Name.Replace("c", "") + " is Click");

        }

        /* 加压阀状态更新：
            - 根据lamp更新signal
            - 更新open tag地址，close tag地址，interlock开闭，用户是否有按钮权限
            - 更新click event
        */
        public static void BPStatus(BoostPump BP, RPD.Btns OpenTag, RPD.Btns CloseTag, bool RegisterEvent, int BtnLevel, int UserLevel)
        {

            /* Button ----------- User
            1.Admin            1.Admin
            2.Manager          2.Manager
            3.User             3.User             
            */
            string sLevel = "LEVEL:";
            if (BtnLevel >= UserLevel)
                sLevel += "1";
            else
                sLevel += "0";

            string sOpen = "OPEN:";
            if (OpenTag.Interlock.Equals(1))
                sOpen += "1";
            else
                sOpen += "0";

            string sClose = "CLOSE:";
            if (CloseTag.Interlock.Equals(1))
                sClose += "1";
            else
                sClose += "0";

            if (OpenTag.Lamp.Equals(1) && CloseTag.Lamp.Equals(0))
                BP.Signal = 1;
            else
                BP.Signal = 0;

            BP.Tag = OpenTag.Address + "," + CloseTag.Address + "," + sOpen + "," + sClose + "," + sLevel;

            if (!RegisterEvent)
            {
                BP.Click += new EventHandler(BP_Btn_Click);
            }

        }
        private static void BP_Btn_Click(object sender, EventArgs e)
        {
            BoostPump SBP = (BoostPump)sender;

            string[] Tag = SBP.Tag.ToString().Split(',');


            if (RPD.STRCTURE.SYS.Btn.AutoModeSwitch.Lamp.Equals(1))
            {
                MessageBox.Show("Auto Mode donot accept manual operation!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Tag[4].Equals("LEVEL:0"))
            {
                MessageBox.Show("You can not access this item \nPlease call manager to give you authority", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (SBP.Signal.Equals(0) && Tag[2].Equals("OPEN:0"))
            {
                string Msg = GetInterLockMsg(Tag[0]);
                MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (SBP.Signal.Equals(1) && Tag[3].Equals("CLOSE:0"))
            {
                string Msg = GetInterLockMsg(Tag[1]);
                MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (SBP.Signal.Equals(1))
                clsPLC.WriPLC_Pulse(Tag[1]);
            else
                clsPLC.WriPLC_Pulse(Tag[0]);

            OPInsertLog("SYS", "O", SBP.Name.Replace("c", "") + " is Click");

        }

        /* 门阀状态更新：
            - 根据lamp更新signal
            - 更新open tag地址，close tag地址，interlock开闭，用户是否有按钮权限
            - 更新click event
        */
        public static void GVStatus(GateValve GV, RPD.Btns OpenTag, RPD.Btns CloseTag, bool RegisterEvent, int BtnLevel, int UserLevel)
        {
            /* Button ----------- User
            1.Admin            1.Admin
            2.Manager          2.Manager
            3.User             3.User             
            */
            string sLevel = "LEVEL:";
            if (BtnLevel >= UserLevel)
                sLevel += "1";
            else
                sLevel += "0";

            string sOpen = "OPEN:";
            if (OpenTag.Interlock.Equals(1))
                sOpen += "1";
            else
                sOpen += "0";

            string sClose = "CLOSE:";
            if (CloseTag.Interlock.Equals(1))
                sClose += "1";
            else
                sClose += "0";

            if (OpenTag.Lamp.Equals(1) && CloseTag.Lamp.Equals(0))
                GV.Signal = 1;
            else
                GV.Signal = 0;

            GV.Tag = OpenTag.Address + "," + CloseTag.Address + "," + sOpen + "," + sClose + "," + sLevel;

            if (!RegisterEvent)
            {

                GV.Click += new EventHandler(GV_Btn_Click);
            }

        }
        private static void GV_Btn_Click(object sender, EventArgs e)
        {

            GateValve SGV = (GateValve)sender;

            string[] Tag = SGV.Tag.ToString().Split(',');

            if (RPD.STRCTURE.SYS.Btn.AutoModeSwitch.Lamp.Equals(1))
            {
                MessageBox.Show("Auto Mode do not accept manual operation!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Tag[4].Equals("LEVEL:0"))
            {
                MessageBox.Show("You can not access this item \nPlease call manager to give you authority", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (SGV.Signal.Equals(0) && Tag[2].Equals("OPEN:0"))
            {
                string Msg = GetInterLockMsg(Tag[0]);
                MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (SGV.Signal.Equals(1) && Tag[3].Equals("CLOSE:0"))
            {
                string Msg = GetInterLockMsg(Tag[1]);
                MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (SGV.Signal.Equals(1))
                clsPLC.WriPLC_Pulse(Tag[1]);
            else
                clsPLC.WriPLC_Pulse(Tag[0]);

            OPInsertLog("SYS", "O", SGV.Name.Substring(SGV.Name.IndexOf('_') + 1, SGV.Name.Length - SGV.Name.IndexOf('_') - 1) + " is Click");

        }

        public static void TPStatus(TurboPump TP, RPD.Btns btnTag, RPD.Btns CloseTag, bool RegisterEvent, int BtnLevel, int UserLevel)
        {
            /* Button ----------- User
            1.Admin            1.Admin
            2.Manager          2.Manager
            3.User             3.User             
            */
            string sLevel = "LEVEL:";
            if (BtnLevel >= UserLevel)
                sLevel += "1";
            else
                sLevel += "0";

            string sClose = "CLOSE:";
            if (CloseTag.Interlock.Equals(1))
                sClose += "1";
            else
                sClose += "0";


            if (btnTag.Lamp.Equals(1))
                TP.Signal = 1;
            else
                TP.Signal = 0;

            TP.Tag = CloseTag.Address + "," + sClose + "," + sLevel;

            if (!RegisterEvent)
            {
                TP.Click += new EventHandler(TP_Btn_Click);
            }


        }

        private static void TP_Btn_Click(object sender, EventArgs e)
        {

            TurboPump TP = (TurboPump)sender;

            string[] Tag = TP.Tag.ToString().Split(',');


            if (RPD.STRCTURE.SYS.Btn.AutoModeSwitch.Lamp.Equals(1))
            {
                MessageBox.Show("Auto Mode donot accept manual operation!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Tag[2].Equals("LEVEL:0"))
            {
                MessageBox.Show("You can not access this item \nPlease call manager to give you authority", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Tag[1].Equals("CLOSE:0"))
            {
                string Msg = GetInterLockMsg(Tag[0]);
                MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            clsPLC.WriPLC_Pulse(Tag[0]);


            OPInsertLog("SYS", "O", TP.Name.Replace("c", "").Replace("_", " ") + " is Click");

        }


        public static void txtStatus(TextBox txt, string Address, string Type, bool RegisterEvent)
        {
            if (!RegisterEvent)
            {
                txt.Tag = Type + "$" + Address;
                txt.KeyPress += new KeyPressEventHandler(Txt_KeyPress);
                txt.KeyDown += new KeyEventHandler(Txt_KeyDown);
                txt.Leave += new EventHandler(Txt_Leave);
            }

        }
        public static void txtStatus(TextBox txt, string Address, string Type, double Low, double High, bool RegisterEvent)
        {
            if (!RegisterEvent)
            {
                txt.Tag = Type + "$" + Address + "$" + Low + "$" + High;
                txt.KeyPress += new KeyPressEventHandler(Txt_KeyPress);
                txt.KeyDown += new KeyEventHandler(Txt_KeyDown);
                txt.Leave += new EventHandler(Txt_Leave);
            }

        }
        private static void Txt_Leave(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;

            if (txt.Text != "")
            {
                string[] TagAndType = txt.Tag.ToString().Split('$');
                string Value = "";
                double Low, High;
                if (TagAndType.Length > 2)
                {
                    try
                    {
                        switch (TagAndType[0])
                        {
                            case "Min":
                                Low = Convert.ToDouble(TagAndType[2]);
                                High = Convert.ToDouble(TagAndType[3]);
                                if (!(Low.Equals(0) && High.Equals(0))) //上下限設定為0
                                {
                                    double T_Value = Convert.ToDouble(txt.Text);

                                    if (T_Value < Low || T_Value > High)
                                    {
                                        MessageBox.Show("Please Enter Range " + Low + "-" + High, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        txt.Text = Low.ToString();
                                        txt.Focus();
                                        return;
                                    }
                                }
                                break;
                            case "Normal":
                                Low = Convert.ToDouble(TagAndType[2]);
                                High = Convert.ToDouble(TagAndType[3]);
                                if (!(Low.Equals(0) && High.Equals(0))) //上下限設定為0
                                {
                                    double T_Value = Convert.ToDouble(txt.Text);

                                    if (T_Value < Low || T_Value > High)
                                    {
                                        MessageBox.Show("Please Enter Range " + Low + "-" + High, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        txt.Text = Low.ToString();
                                        txt.Focus();
                                        return;
                                    }
                                }
                                break;
                            case "Time":
                                Low = Convert.ToDouble(TagAndType[2]);
                                High = Convert.ToDouble(TagAndType[3]);
                                if (!(Low.Equals(0) && High.Equals(0))) //上下限設定為0
                                {
                                    double T_Value = Convert.ToDouble(txt.Text);

                                    if (T_Value < Low || T_Value > High)
                                    {
                                        MessageBox.Show("Please Enter Range " + Low + "-" + High, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        txt.Text = Low.ToString();
                                        txt.Focus();
                                        return;
                                    }
                                }
                                break;
                            case "Time-2":
                                Low = Convert.ToDouble(TagAndType[2]);
                                High = Convert.ToDouble(TagAndType[3]);
                                if (!(Low.Equals(0) && High.Equals(0))) //上下限設定為0
                                {
                                    double T_Value = Convert.ToDouble(txt.Text);

                                    if (T_Value < Low || T_Value > High)
                                    {
                                        MessageBox.Show("Please Enter Range " + Low + "-" + High, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        txt.Text = Low.ToString();
                                        txt.Focus();
                                        return;
                                    }
                                }
                                break;
                            default:
                                Low = Convert.ToDouble(TagAndType[3]);
                                High = Convert.ToDouble(TagAndType[4]);
                                if (!(Low.Equals(0) && High.Equals(0))) //上下限設定為0
                                {
                                    double T_Value = Convert.ToDouble(txt.Text);

                                    if (T_Value < Low || T_Value > High)
                                    {
                                        MessageBox.Show("Please Enter Range " + Low + "-" + High, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        txt.Text = Low.ToString();
                                        txt.Focus();
                                        return;
                                    }
                                }
                                break;
                        }
                    }
                    catch
                    {
                        return;
                    }
                }

                switch (TagAndType[0])
                {
                    case "Min":
                        Value = (Convert.ToDouble(txt.Text) * 10 * 60).ToString();
                        clsPLC.WriPLC_Word(TagAndType[1], Value);
                        break;
                    case "Time": //時間*10
                        Value = (Convert.ToDouble(txt.Text) * 10).ToString();
                        clsPLC.WriPLC_Word(TagAndType[1], Value);
                        break;
                    case "Time-2": //時間*100
                        Value = (Convert.ToDouble(txt.Text) * 100).ToString();
                        clsPLC.WriPLC_Word(TagAndType[1], Value);
                        break;
                    case "Normal":
                        Value = txt.Text;
                        clsPLC.WriPLC_Word(TagAndType[1], Value);
                        break;
                    case "Dword-2":
                        Value = txt.Text;
                        clsPLC.IntToWord(Convert.ToInt32((Convert.ToDouble(Value) * 100)), TagAndType[1], TagAndType[2]);
                        break;
                    case "Dword-3":
                        Value = txt.Text;
                        clsPLC.IntToWord(Convert.ToInt32((Convert.ToDouble(Value) * 1000)), TagAndType[1], TagAndType[2]);
                        break;
                    case "Dword-6":
                        Value = txt.Text;
                        clsPLC.IntToWord(Convert.ToInt32((Convert.ToDouble(Value) * 1000000)), TagAndType[1], TagAndType[2]);
                        break;
                }
            }

        }


        private static void Txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Txt_Leave(sender, null);
            }

        }

        public static void Txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Delete)
            {
                e.Handled = false;
                int j = 0;                  //紀錄小數點個數  
                int k = 0;                  //紀錄小數點位數  
                int dotloc = -1;            //紀錄小數點位置  
                bool flag = false;          //如果有小數點為true  


                TextBox txt = (TextBox)sender;
                int Count = 1; //指定可輸入的位數

                string[] TagAndType = txt.Tag.ToString().Split('$');
                switch (TagAndType[0])
                {
                    case "Min":
                        Count = 1;
                        break;
                    case "Time": //時間*10
                        Count = 1;
                        break;
                    case "Time-2":
                        Count = 2;
                        break;
                    case "Normal":
                        Count = 5;
                        break;
                    case "Dword-2":
                        Count = 2;
                        break;
                    case "Dword-3":
                        Count = 3;
                        break;
                    case "Dword-6":
                        Count = 6;
                        break;
                }

                //小數點不可在第一位
                if (txt.Text.Length == 0)
                {
                    if (e.KeyChar == '.')
                    {
                        e.Handled = true;
                    }
                }

                for (int i = 0; i < txt.Text.Length; i++)
                {
                    if (txt.Text[i] == '.')
                    {
                        j++;
                        flag = true;
                        dotloc = i;
                    }

                    if (flag)
                    {
                        if (char.IsNumber(txt.Text[i]) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Delete)
                        {
                            k++;
                        }
                    }

                    if (j >= 1)
                    {
                        if (e.KeyChar == '.')
                        {
                            if (txt.SelectedText.IndexOf('.') == -1)
                                e.Handled = true;
                        }
                    }

                    if (!flag)                  //小數點是否已為一位小數  
                    {

                        if (e.KeyChar == '.')
                        {
                            if (txt.Text.Length - txt.SelectionStart - txt.SelectedText.Length > 2)        //the condition also can be instead of "textBox1.Text.Substring(textBox1.SelectionStart).Length-textBox1.SelectionLength>2"   
                                e.Handled = true;
                        }
                    }

                    if (k == Count)
                    {
                        if (txt.SelectionStart > txt.Text.IndexOf('.') && txt.SelectedText.Length == 0 && e.KeyChar != (char)Keys.Delete && e.KeyChar != (char)Keys.Back)
                            e.Handled = true;
                    }
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        public static void OPInsertLog(string EQ, string Type, string Des)
        {
            string _SQL = @"INSERT INTO ALARM_LOG VALUES('" + EQ + "','" + Type + "','" + Des + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + RPD.STRCTURE.SYS.Data.UserID + "','" + RPD.STRCTURE.SYS.Data.RecipeNumber + "')";

            clsDBN.ExecuteSqlCommand(_SQL);

        }

        public static string GetInterLockMsg(string sAddress)
        {
            string SQL = @"SELECT REASON FROM INTERLOCK_TABLE WHERE ADDRESS ='" + sAddress.Replace("M15", "M17") + "'";
            object objMsg = clsDBN.GetObject(SQL);
            if (objMsg != null)
                return objMsg.ToString().Replace("\\n", "\n");
            else
                return sAddress.Replace("M15", "M17") + "No Interlock Available";

        }


        #endregion

        public static void ButtonStatusHeater(Button btn, RPD.Btns btnTag, char Type, bool RegisterEvent, int BtnLevel, int UserLevel, string SetTime)
        {
            /* Button ----------- User
             1.Admin            1.Admin
             2.Manager          2.Manager
             3.User             3.User             
             */
            if (BtnLevel >= UserLevel)
                btn.Visible = true;
            else
                btn.Visible = false;

            if (btnTag == null) //若沒有Tag就不做下列判斷
                return;

            //if (btnTag.Interlock.Equals(1))
                btn.Enabled = true;
            //else
                //btn.Enabled = false;

            if (btnTag.Lamp.Equals(1))
                btn.BackColor = Color.Lime;
            else
                btn.BackColor = Color.Transparent;

            string[] Tag = btn.Tag.ToString().Split(',');

            btn.Tag = btnTag.Address + "," + SetTime + "," + Tag[2] + "," + Tag[3] + "," + Tag[4] + ",";

            if (!RegisterEvent)
            {
                switch (Type)
                {
                    case 'A':
                        btn.Tag = btnTag.Address;
                        btn.Click += new EventHandler(A_Btn_Click);
                        break;
                    case 'B':
                        btn.Tag = btnTag.Address;
                        btn.Click += new EventHandler(B_Btn_Click);
                        break;
                    case 'D':

                        btn.Click += new EventHandler(D_Btn_Click);

                        break;


                }
            }

        }

        public static void ButtonStatusSameLamp(Button btn, RPD.Btns btnTag, char Type, bool RegisterEvent, int BtnLevel, int UserLevel)
        {
            /* Button ----------- User
             1.Admin            1.Admin
             2.Manager          2.Manager
             3.User             3.User             
             */
            if (BtnLevel >= UserLevel)
                btn.Visible = true;
            else
                btn.Visible = false;

            if (btnTag == null) //若沒有Tag就不做下列判斷
                return;

            //if (btnTag.Interlock.Equals(1))
                btn.Enabled = true;
            //else
                //btn.Enabled = false;

            if (btnTag.Lamp.Equals(0))
                btn.BackColor = Color.Lime;
            else
                btn.BackColor = Color.Transparent;


            if (!RegisterEvent)
            {
                switch (Type)
                {
                    case 'A':
                        btn.Tag = btnTag.Address;
                        btn.Click += new EventHandler(A_Btn_Click);
                        break;
                    case 'B':
                        btn.Tag = btnTag.Address;
                        btn.Click += new EventHandler(B_Btn_Click);
                        break;

                }
            }

        }

        public static string DisplayTPStatus(int sStatus)
        {
            string r_Status;
            switch (sStatus)
            {
                case 1:
                    r_Status = "Stop";
                    break;
                case 2:
                    r_Status = "Decelerate";
                    break;
                case 3:
                    r_Status = "Accelerate";
                    break;
                case 4:
                    r_Status = "Running";
                    break;
                default:
                    r_Status = "NA";
                    break;
            }

            return r_Status;

        }
        public static void D_Btn_Click(object sender, EventArgs e)
        {
            Button SBtn = (Button)sender;
            string[] Tag = SBtn.Tag.ToString().Split(',');
            //bool _HeaterSetTimeCheck;

            if (Tag[1].Equals(""))
            {
                MessageBox.Show("Not Allowed Blank!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }

            else if (Convert.ToInt16(Tag[1]) < 0 || Convert.ToInt16(Tag[1]) > 120)
            {
                MessageBox.Show("Range Between 0-120 min !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;


            }
            else
            {

                //一般按鈕On 一個 Pulse
                clsPLC.WriPLC_Pulse(Tag[0]); // Open
                //clsPLC.WriPLC_Pulse(SBtn.Tag.ToString());

                SBtn.Tag = Tag[0] + "," + Tag[1] + ",true," + Tag[3] + "," + Tag[4];

                //frmHeater.RecordBtnName(SBtn);

                //_HeaterSetTimeCheck = true;
            }

            //if(_HeaterSetTimeCheck==true)
            //{
            //    SBtn.Tag = Tag[0] + "," + Tag[1] + ",true";
            //}


        }

    }
}
