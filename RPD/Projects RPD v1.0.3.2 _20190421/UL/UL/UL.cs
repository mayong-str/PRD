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

namespace UL
{
    public partial class UL : UserControl
    {

        #region UL 屬性
        //Front position sensor
        private bool _bFPS = false;
        [Category("FPS"), Description("Front position sensor")]
        public bool FPS
        {
            get { return _bFPS; }
            set
            {
                _bFPS = value;

                if (_bFPS)
                {
                    osFPS.FillColor = Color.Chartreuse;
                }
                else
                {
                    osFPS.FillColor = SystemColors.Control;//Color.Red;
                }
            }
        }

        //Rear position sensor
        private bool _bRPS = false;
        [Category("RPS"), Description("Rear position sensor")]
        public bool RPS
        {
            get { return _bRPS; }
            set
            {
                _bRPS = value;

                if (_bRPS)
                {
                    osRPS.FillColor = Color.Chartreuse;
                }
                else
                {
                    osRPS.FillColor = SystemColors.Control;//Color.Red;
                }
            }
        }

        //DV1 is open or not
        private bool _bDV4Open = false;
        [Category("DV4Open"), Description("DV4 is open or not")]
        public bool DV4Open
        {
            get { return _bDV4Open; }
            set
            {
                _bDV4Open = value;

                if (_bDV4Open)
                {
                    nsDoor2.Height = 10;
                }
                else
                {
                    nsDoor2.Height = 150;
                }
            }
        }


        //CV activate or not 
        private bool _bCVAct = false;
        [Category("CVAct"), Description("CV activate states")]
        public bool CVAct
        {
            get { return _bCVAct; }
            set
            {
                _bCVAct = value;

                if (_bCVAct)
                {
                    osCV1.FillColor = Color.Chartreuse;
                    osCV1.FillStyle = FillStyle.Solid;
                }
                else
                {
                    osCV1.FillColor = Color.Navy; ;//Color.Red;
                    osCV1.FillStyle = FillStyle.Solid;
                }
            }
        }

        //UL has trate or not
        private bool _bTrateVisible = false;
        [Category("Substrate Visible"), Description("Substrate Display")]
        public bool TrateVisible
        {
            get { return _bTrateVisible; }
            set
            {
                    _bTrateVisible = value;

                    if (!_bTrateVisible)
                    {
                        plSubstrate.BackColor = Color.WhiteSmoke;
                        plSubstrate.BorderStyle = BorderStyle.None;
                        lab_STID.Visible = false;
                    }
                    else
                    {
                        plSubstrate.BackColor = Color.MediumBlue;
                        plSubstrate.BorderStyle = BorderStyle.FixedSingle;
                        lab_STID.Visible = true;

                        if (TID.Length == 0)
                        {
                            TID = "tray";
                            lab_STID.Text = TID;
                            //return;
                        }

                        _sTID = _sTID.Trim();
                        lab_STID.Text = TID;
                    }
            }
        }

        //UL has trate ID
        #region UL trate ID
        private string _sTID = "";
        [Category("TID"), Description("UL trate ID")]
        public string TID
        {
            get { return _sTID; }
            set { _sTID = value;}
        }

        #endregion

        //UL Auto or Not
        private bool _bAuto = false;
        [Category("AutoMode"), Description("Is UL in Auto mode")]
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
                    lab_Online.BackColor = SystemColors.Control;//Color.Red;
                }
            }
        }

        #region is UL in vacuum
        private bool _bIsVac = false;
        [Category("IsVacuum"), Description("Is UL in vacuum")]
        public bool IsVac
        {
            get { return _bIsVac; }
            set
            {
                _bIsVac = value;

                if (_bIsVac) //in vacuum change Back color
                {
                    pal_BackG.BackColor = Color.Wheat;
                    pal_BackG.SendToBack();
                    lab_ATMG.BackColor = SystemColors.Control;
                    lab_STID.BackColor = Color.Wheat;
                    os_ULATMG.FillColor = SystemColors.Control;
                    ULPHGauge.Signal = 1;
                }
                else
                {
                    pal_BackG.BackColor = Color.Transparent;
                    pal_BackG.SendToBack();
                    lab_ATMG.BackColor = Color.Chartreuse;
                    lab_STID.BackColor = Color.Transparent;
                    os_ULATMG.FillColor = Color.Chartreuse;
                    ULPHGauge.Signal = 2;
                }
            }
        }



        #endregion

        #region is UL in ATM
        private bool _bIsATM = false;
        [Category("IsATM"), Description("Is UL in ATM")]
        public bool IsATM
        {
            get { return _bIsATM; }
            set
            {
                _bIsATM = value;

                if (!_bIsATM) //in vacuum change Back color
                {
                    lab_ATMG.BackColor = SystemColors.Control;
                    os_ULATMG.FillColor = SystemColors.Control;
                    ULPHGauge.Signal = 1;
                }
                else
                {
                    lab_ATMG.BackColor = Color.Chartreuse;
                    os_ULATMG.FillColor = Color.Chartreuse;
                    ULPHGauge.Signal = 2;
                }
            }
        }

        #endregion

        #region UL PH single
        private bool _bPHS = false;
        [Category("PHSingle"), Description("UL PH single")]
        public bool PHSingle
        {
            get { return _bPHS; }
            set
            {
                _bPHS = value;

                if (_bPHS) //in vacuum change  color
                {
                    ULPHGauge.Signal = 1;
                }
                else
                {
                    ULPHGauge.Signal = 0;
                }
            }
        }

        #endregion

        #region is UL pressure value
        private string _sPreVal = "";
        [Category("PreVal"), Description("UL pressure value")]
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
                    ULPHGauge.Signal = 0;
                    return;
                }

                //if (!IsNumeric(_sPreVal))
                //{
                //    _sPreVal = "Err";
                //    lab_PHGValue.Text = _sPreVal;
                //    ULPHGauge.Signal = 0;
                //    return;
                //}                    
                _sPreVal = _sPreVal.Trim();

                lab_PHGValue.Text = _sPreVal;
                ULPHGauge.Signal = 1;

                //if (Convert.ToDouble(_sPreVal) < 760)
                //{
                //    ULPHGauge.Signal = 1;
                //}
                //else
                //{
                //    ULPHGauge.Signal = 2;
                //}

            }
        }

        #endregion

        #region is UL 門變色
        private string _DV4_Up_InterLock = "";
        [Category("DV4_Up_InterLock"), Description("DV4 Up InterLock")]
        public string DV4_Up_InterLock
        {
            get { return _DV4_Up_InterLock; }
            set
            {
                _DV4_Up_InterLock = value;
                if (_DV4_Up_InterLock.Equals("1"))
                    rsUpBtn2.BackgroundImage = Properties.Resources.green_up;
                else
                    rsUpBtn2.BackgroundImage = Properties.Resources.red_up;

            }
        }

        private string _DV4_Down_InterLock = "";
        [Category("DV4_Down_InterLock"), Description("DV4 Down InterLock")]
        public string DV4_Down_InterLock
        {
            get { return _DV4_Down_InterLock; }
            set
            {
                _DV4_Down_InterLock = value;
                if (_DV4_Down_InterLock.Equals("1"))
                    rsDownBtn2.BackgroundImage = Properties.Resources.green_down;
                else
                    rsDownBtn2.BackgroundImage = Properties.Resources.red_down;

            }
        }
        #endregion

        //Button Left display
        private bool _bleft = false;
        [Category("BtnLeft"), Description("Button Left Display")]
        public bool BtnLeft
        {
            get { return _bleft; }
            set
            {
                _bleft = value;

                if (_bleft)
                {
                    btn_left.BackColor = Color.Lime;
                }
                else
                {
                    btn_left.BackColor = Color.Transparent;
                }
            }
        }

        //Button Right display
        private bool _bright = false;
        [Category("BtnRight"), Description("Button Right Display")]
        public bool BtnRight
        {
            get { return _bright; }
            set
            {
                _bright = value;

                if (_bright)
                {
                    btn_right.BackColor = Color.Lime;
                }
                else
                {
                    btn_right.BackColor = Color.Transparent;
                }
            }
        }

        //Button Close display
        private bool _bclose = false;
        [Category("BtnClose"), Description("Button Close Display")]
        public bool BtnClose
        {
            get { return _bclose; }
            set
            {
                _bclose = value;

                if (_bclose)
                {
                    btn_Close.BackColor = Color.Lime;
                }
                else
                {
                    btn_Close.BackColor = Color.Transparent;
                }
            }
        }

        #endregion


        ThreadingTimer _ThreadTimerC = null;
        TimersTimer _TimersTimer = null;
        private string _sDoorName;
        private string _sCDoorName;


        public UL()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor
              | ControlStyles.UserPaint
              | ControlStyles.AllPaintingInWmPaint
              | ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Opaque, false);
            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            InitializeComponent();


            PropertyInfo info = this.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            info.SetValue(plSubstrate, true, null);
            this.DoubleBuffered = true;
            this.BackColor = Color.FromKnownColor(KnownColor.Transparent);


            //CV control pal
            pal_CVS.Width = 1;
            pal_CVS.Height = 1;
            pal_CVS.Visible = false;
        }




        #region Form Event
        private void LL_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Transparent;
            pal_BackG.SendToBack();
        }

        private void rsUpBtn1_Click(object sender, EventArgs e)
        {
            //funOpenDoor("nsDoor1");
        }


        public delegate void BtnClickDelegate(object sender, EventArgs e); // 自定一個委派
        public event BtnClickDelegate Co_Btn_click; //使用此委派宣告event
        public delegate void AutoBtnClickDelegate(object sender, EventArgs e); // 自定一個委派
        public event AutoBtnClickDelegate Auto_Btn_click; //使用此委派宣告event
        public delegate void CVBtnClickDelegate(object sender, EventArgs e); // 自定一個委派
        public event CVBtnClickDelegate CV_Btn_click; //使用此委派宣告event
       
        private void rsUpBtn2_Click(object sender, EventArgs e)
        {
            //funOpenDoor("nsDoor2");


            if (this.AutoMode != true )
            {
                if (DV4Open == true)
                {
                    MessageBox.Show("DV4 is already open ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                DV4Open = true;
                Co_Btn_click(sender, e);
            }
            else
            {
                //MessageBox.Show("Auto Mode cannot accept manual operation!!",
                //                             "RPD",
                //                             System.Windows.Forms.MessageBoxButtons.OK,
                //                             System.Windows.Forms.MessageBoxIcon.Information);

                ClsTool.ShowWarningMsg("Auto Mode donot accept manual operation!!");
            }
        }

        private void rsDownBtn1_Click(object sender, EventArgs e)
        {
            //funCloseDoor("nsDoor1");
        }

        private void rsDownBtn2_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(AutoMode.ToString());
 
            //funCloseDoor("nsDoor2");
            if (this.AutoMode != true)
            {
                if (DV4Open == false)
                {
                    MessageBox.Show("DV4 is already close ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                DV4Open = false;
                Co_Btn_click(sender, e);
            }          
            else
            {
                //MessageBox.Show("Auto Mode cannot accept manual operation!!",
                //              "RPD",
                //              System.Windows.Forms.MessageBoxButtons.OK,
                //              System.Windows.Forms.MessageBoxIcon.Information);

                ClsTool.ShowWarningMsg("Auto Mode donot accept manual operation!!");
            }
        }

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

        #region CV Control

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
                pal_CVS.Width = 98;
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


        #region Function

        #region Open Gate Funtion
        public void funOpenDoor(string sDoorName)
        {
            if (AutoMode != true)
            {
                if (sDoorName == "nsDoor1")
                {
                    if (nsDoor1.Height < 150 && nsDoor1.Height > 10)
                    {
                        MessageBox.Show("入口閘門動作中");
                        return;
                    }
                    else if (nsDoor2.Height < 150 && nsDoor2.Height > 10)
                    {
                        MessageBox.Show("出口閘門開啟中");
                        return;
                    }
                }
                else
                {
                    if (nsDoor2.Height < 150 && nsDoor2.Height > 10)
                    {
                        MessageBox.Show("出口閘門開啟中");
                        return;
                    }
                    else if (nsDoor1.Height < 150 && nsDoor1.Height > 10)
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
            if (AutoMode != true)
            {
                //之後要加ATM switch的判斷
                if (sDoorName == "nsDoor1")
                {
                    if (nsDoor1.Height > 10 && nsDoor1.Height < 150)
                    {
                        MessageBox.Show("入口閘門動作中");
                        return;
                    }
                    else if (nsDoor2.Height > 10 && nsDoor2.Height < 150)
                    {
                        MessageBox.Show("出口閘門動作中");
                        return;
                    }
                }
                else
                {
                    if (nsDoor2.Height > 10 && nsDoor2.Height < 150)
                    {
                        MessageBox.Show("出口閘門動作中");
                        return;
                    }
                    else if (nsDoor1.Height > 10 && nsDoor1.Height < 150)
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
                if (nsDoor2.Height < 150)
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

        private void LL_Resize(object sender, EventArgs e)
        {
            this.Width = 290;
            this.Height = 275;
            pal_BackG.SendToBack();
        }


        
        //判斷值是否為數字 double
        public static bool IsNumeric(string sValue)
        {
            try
            {
                double d = Convert.ToDouble(sValue);
                return true;
            }
            catch
            {
                return false;
            }
        }


        #endregion

        
    }
}
