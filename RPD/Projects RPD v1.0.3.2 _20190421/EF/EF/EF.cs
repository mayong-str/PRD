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

namespace EF
{
    public partial class EF : UserControl
    {
        #region EF 屬性
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
                    osCVAct.FillColor = Color.Chartreuse;
                    osCVAct.FillStyle = FillStyle.Solid;
                }
                else
                {
                    osCVAct.FillColor = Color.Navy;//Color.Red;
                    osCVAct.FillStyle = FillStyle.Solid;
                }
            }
        }

        //EF has trate or not
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

        //EF has trate ID
        #region EF trate ID
        private string _sTID = "";
        [Category("TID"), Description("trate ID")]
        public string TID
        {
            get { return _sTID; }
            set { _sTID = value; }
        }

        #endregion

        //EF Ready or Not
        private bool _bReady = false;
        [Category("isReady"), Description("Is EF Ready")]
        public bool Ready
        {
            get { return _bReady; }
            set
            {
                _bReady = value;

                if (_bReady) 
                {
                    lab_Ready.BackColor = Color.Chartreuse;
                }
                else
                {
                    lab_Ready.BackColor = SystemColors.Control;//Color.Red;
                }
            }
        }

        //EF show ready btn
        private bool _bReadyBtnShow = false;
        [Category("ReadyBtn"), Description("show ready btn")]
        public bool ReadyBtnShow
        {
            get { return _bReadyBtnShow; }
            set
            {
                _bReadyBtnShow = value;

                if (_bReadyBtnShow)  // show Ready button
                {
                    lab_Ready.Visible = true;
                }
                else
                {
                    lab_Ready.Visible = false;
                }
            }
        }


        //LL Auto or Not
        private bool _bAuto = false;
        [Category("AutoMode"), Description("Is LL in Auto mode")]
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


        public EF()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor
              | ControlStyles.UserPaint
              | ControlStyles.AllPaintingInWmPaint
              | ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Opaque, false);

            InitializeComponent();


            PropertyInfo info = this.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            info.SetValue(plSubstrate, true, null);
            this.DoubleBuffered = true;
            this.BackColor = Color.FromKnownColor(KnownColor.Transparent);

        }


        public delegate void CVBtnClickDelegate(object sender, EventArgs e); // 自定一個委派
        public event CVBtnClickDelegate CV_Btn_click; //使用此委派宣告event


        #region Form Event
        private void EF_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Transparent;
        }



        #endregion


        #region Function

        #region CV control

        private void btn_Close_Click(object sender, EventArgs e)
        {
            if (this.AutoMode)
            {
                ClsTool.ShowWarningMsg("Auto Mode donot accept manual operation!!");
            }
            else
            {
                CV_Btn_click(sender, e);             
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
            if (this.AutoMode)
            {
                ClsTool.ShowWarningMsg("Auto Mode donot accept manual operation!!");               
            }
            else
            {
                CV_Btn_click(sender, e);             
            }

        }

        private void btn_right_Click(object sender, EventArgs e)
        {
            if (this.AutoMode)
            {
                ClsTool.ShowWarningMsg("Auto Mode donot accept manual operation!!");
            }
            else
            {
                CV_Btn_click(sender, e);
            }
        }

        #endregion

        private void lab_Online_Click(object sender, EventArgs e)
        {

        }
        

        #endregion

    }
}
