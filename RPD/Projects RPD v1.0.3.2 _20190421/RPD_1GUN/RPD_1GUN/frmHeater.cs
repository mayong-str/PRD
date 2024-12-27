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
    public partial class frmHeater : Form
    {
        bool _RegisterEvent = false;
        string StartTime_Heater1_Inside="";

        public bool _HeaterSetTimeCheck = false;

        public frmHeater()
        {
            InitializeComponent();

            
        }
        public void ControlTimer(bool OnOFF)
        {
            this.DisplayTimer.Enabled = OnOFF;
        }



        public void RefreshSetValue()
        {

            //txtSetValue_W1.Text = RPD.STRCTURE.SYS.Heater.W_1_Heater_Temp_SetValue.ToString();
            txtSetValue_W1.Text = (Convert.ToDouble(clsPLC.ReadPLC_Word("D35920")) / 10).ToString();
            txtSetValue_W2.Text = (Convert.ToDouble(clsPLC.ReadPLC_Word("D35921")) / 10).ToString();
            txtSetValue_W3.Text = (Convert.ToDouble(clsPLC.ReadPLC_Word("D35922")) / 10).ToString();
            txtWaterSetTime.Text = (Convert.ToDouble(clsPLC.ReadPLC_Word("D35910")) / 10).ToString();
        }


        private void DisplayTimer_Tick(object sender, EventArgs e)
        {
            #region TR

            clsTool.ButtonStatusHeater(btnW1_Inside_Open, RPD.STRCTURE.TR.Btn.W_1_Heater_Open, 'D', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level, txtWaterSetTime.Text);

            clsTool.ButtonStatusSameLamp(btnW1_Inside_Close, RPD.STRCTURE.TR.Btn.W_1_Heater_Close, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);

            clsTool.ButtonStatusHeater(btnW2_Inside_Open, RPD.STRCTURE.TR.Btn.W_2_Heater_Open, 'D', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level, txtWaterSetTime.Text);
            clsTool.ButtonStatusSameLamp(btnW2_Inside_Close, RPD.STRCTURE.TR.Btn.W_2_Heater_Close, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);

            clsTool.ButtonStatusHeater(btnW3_Inside_Open, RPD.STRCTURE.TR.Btn.W_3_Heater_Open, 'D', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level, txtWaterSetTime.Text);
            clsTool.ButtonStatusSameLamp(btnW3_Inside_Close, RPD.STRCTURE.TR.Btn.W_3_Heater_Close, 'A', _RegisterEvent, 3, RPD.STRCTURE.SYS.Data.Level);



            #endregion

            #region Heater Set Time
            clsTool.txtStatus(txtWaterSetTime, "D35910", "Time", _RegisterEvent);   //*10 toPLC
            #endregion

            #region Heater 讀值

            lblNowValue_W1.Text = RPD.STRCTURE.TR.Map.W_1_Heater_Temp.ToString();
            lblNowValue_W2.Text = RPD.STRCTURE.TR.Map.W_2_Heater_Temp.ToString();
            lblNowValue_W3.Text = RPD.STRCTURE.TR.Map.W_3_Heater_Temp.ToString();

            #endregion

            #region Heater Set
            // Albert 201905
            clsTool.txtStatus(txtSetValue_W1, "D35920", "Time", 25, 40, _RegisterEvent);      //*10 toPLC
            clsTool.txtStatus(txtSetValue_W2, "D35921", "Time", 25, 50, _RegisterEvent);      //*10 toPLC
            clsTool.txtStatus(txtSetValue_W3, "D35922", "Time", 25, 75, _RegisterEvent);      //*10 toPLC

            #endregion


            #region Check SetTime
            btnW1_Inside_Open.Tag = RecordStartTime(btnW1_Inside_Open, txtSetValue_W1.Text, lblNowValue_W1.Text, txtWaterSetTime.Text);
            lbl_W1Status.Text = lblStatus(btnW1_Inside_Open);
            btnW2_Inside_Open.Tag = RecordStartTime(btnW2_Inside_Open, txtSetValue_W2.Text, lblNowValue_W2.Text, txtWaterSetTime.Text);
            lbl_W2Status.Text = lblStatus(btnW2_Inside_Open);
            btnW3_Inside_Open.Tag = RecordStartTime(btnW3_Inside_Open, txtSetValue_W3.Text, lblNowValue_W3.Text, txtWaterSetTime.Text);
            lbl_W3Status.Text = lblStatus(btnW3_Inside_Open);

            #endregion



            _RegisterEvent = true;

            Application.DoEvents();


        }

        private static string RecordStartTime(Button btnHeaterOpen, string txtSetTemp, string lblNowTemp, string txtSetTime)
        {
            string StartTime = "";
            string _ElapsedTime = "";
            string lblStatus;
            //string _HeaterOpenName;
            //_HeaterOpenName = HeaterOpen.Name;
            string[] Tag = btnHeaterOpen.Tag.ToString().Split(',');
            //string[] Tag = btntagHeaterOpen.ToString().Split(',');



            /////////////////////First Check if Click Button and Record StartTime /////////////////////////


            if (Tag[2] == "true")
            {
                //if (lblNowTemp == txtSetTemp && lblStatus != "Ready" && StartTime == "")

                if (lblNowTemp == txtSetTemp)
                {

                    StartTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    Tag[2] = "";

                    ////////////Check If Arrive SetTime and Return Tag to Show LabelStatus and Keep Message//////////////////////

                    TimeSpan _PassTime = DateTime.Now - Convert.ToDateTime(StartTime);
                    int _ElapsedSec = _PassTime.Seconds % 60;
                    int _ElapsedMin = _PassTime.Minutes;

                    if (_ElapsedMin < Convert.ToInt32(txtSetTime))
                    {
                        _ElapsedTime = _ElapsedMin.ToString() + " min " + _ElapsedSec.ToString() + " sec ";

                        //_ElapsedTime = Convert.ToString(DateTime.Now - Convert.ToDateTime(_StartTime));
                        lblStatus = _ElapsedTime + "Passed";
                        return Tag[0] + "," + Tag[1] + "," + Tag[2] + "," + StartTime + "," + lblStatus;

                    }
                    else
                    {
                        lblStatus = "Ready";
                        //btntagHeaterOpen = "" + "," + "" + "," + "stop"; //back to origin
                        return Tag[0] + "," + Tag[1] + ",ready," + "" + "," + lblStatus;
                    }


                    ////////////Check If Arrive SetTime and Return Tag to Show LabelStatus and Keep Message//////////////////////

                    //btntagHeaterOpen = "" + "," + "" + "," + "" + ","; //back to origin
                    //return btntagHeaterOpen.ToString() + StartTime;

                }
                else
                {
                    StartTime = "";
                    Tag[2] = "";

                    ////////////Check If Arrive SetTime and Return Tag to Show LabelStatus and Keep Message//////////////////////

                    lblStatus = "Not Ready";
                    return Tag[0] + "," + Tag[1] + "," + Tag[2] + "," + StartTime + "," + lblStatus;

                    ////////////Check If Arrive SetTime and Return Tag to Show LabelStatus and Keep Message//////////////////////

                    //btntagHeaterOpen = "" + "," + "" + "," + "" + ","; //back to origin
                    //return btntagHeaterOpen.ToString() + StartTime;
                }

            }
            else if (Tag[2] == "stop")
            {
                StartTime = "stop";
                lblStatus = "Stop";
                return Tag[0] + "," + Tag[1] + "," + Tag[2] + "," + StartTime + "," + lblStatus;
                //btntagHeaterOpen = "" + "," + "" + "," + "stop" + ","; //back to origin
                //return btntagHeaterOpen.ToString() + StartTime;

            }

            else if (Tag[2] == "") ////check temp
            {
                if (lblNowTemp == txtSetTemp)
                {
                    if (Tag[3] == "")
                    {
                        StartTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    }
                    else
                    {
                        StartTime = Tag[3];
                    }

                    //Tag[2] = "";
                    ////////////Check If Arrive SetTime and Return Tag to Show LabelStatus and Keep Message//////////////////////

                    TimeSpan _PassTime = DateTime.Now - Convert.ToDateTime(StartTime);
                    int _ElapsedSec = _PassTime.Seconds % 60;
                    int _ElapsedMin = _PassTime.Minutes;

                    if (_ElapsedMin < Convert.ToInt32(txtSetTime))
                    {
                        _ElapsedTime = _ElapsedMin.ToString() + " min " + _ElapsedSec.ToString() + " sec ";

                        //_ElapsedTime = Convert.ToString(DateTime.Now - Convert.ToDateTime(_StartTime));
                        lblStatus = _ElapsedTime + "Passed";
                        return Tag[0] + "," + Tag[1] + "," + Tag[2] + "," + StartTime + "," + lblStatus;
                    }
                    else
                    {
                        lblStatus = "Ready";
                        //btntagHeaterOpen = "" + "," + "" + "," + "stop"; //back to origin
                        return Tag[0] + "," + Tag[1] + ",ready," + "" + "," + lblStatus;
                    }
                    ////////////Check If Arrive SetTime and Return Tag to Show LabelStatus and Keep Message//////////////////////
                }
                else
                {
                    StartTime = "";
                    //Tag[2] = "";

                    ////////////Check If Arrive SetTime and Return Tag to Show LabelStatus and Keep Message//////////////////////

                    lblStatus = "Not Ready";
                    return Tag[0] + "," + Tag[1] + "," + Tag[2] + "," + StartTime + "," + lblStatus;

                    ////////////Check If Arrive SetTime and Return Tag to Show LabelStatus and Keep Message//////////////////////

                }
            }

            else if (Tag[2] == "ready")
            {
                if (lblNowTemp == txtSetTemp)
                {
                    /////stay "ready"/////
                    StartTime = Tag[3];
                    lblStatus = "Ready";
                    return Tag[0] + "," + Tag[1] + "," + Tag[2] + "," + StartTime + "," + lblStatus;
                    /////stay "ready"/////

                }
                else
                {
                    /////if change SetTemp/////
                    StartTime = "";
                    Tag[2] = "";

                    ////////////back to check temp//////////////////////

                    lblStatus = "Not Ready";
                    return Tag[0] + "," + Tag[1] + "," + Tag[2] + "," + StartTime + "," + lblStatus;

                    ////////////back to check temp//////////////////////
                }
            }


            else
            {
                StartTime = Tag[2];
                lblStatus = "Error-" + StartTime + "is not defined";
                //MessageBox.Show(StartTime + "is not defined", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return Tag[0] + "," + Tag[1] + "," + Tag[2] + "," + StartTime + "," + lblStatus;
            }

        }

        private static string lblStatus(Button btnHeaterOpen)
        {
            //string[] Tag = btntagHeaterOpen.ToString().Split(',');
            string[] Tag = btnHeaterOpen.Tag.ToString().Split(',');
            return Tag[4];
        }
    }
}
