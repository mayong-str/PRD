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

namespace MFC
{
    public partial class MFC : UserControl
    {
        public MFC()
        {
            InitializeComponent();
        }

        #region

        public delegate void BtnClickDelegate(object sender, EventArgs e); // 自定一個委派
        public event BtnClickDelegate Co_Btn_click; //使用此委派宣告event

        private bool _bArea = true;

        private string _bOn = "1";
        [Category("MFC status"), Description("MFC status Display")]
        public string MFCstatus
        {
            get { return _bOn; }
            set
            {
                _bOn = value;

                if (_bOn != "1")
                {
                    lab_Value.BackColor = SystemColors.Control;
                    lab_UNit.BackColor = SystemColors.Control;
                    rs_MFC.FillColor = SystemColors.Control;
                    rs_MFC.FillStyle = FillStyle.Solid;   
                }
                else
                {
                    lab_Value.BackColor = Color.Chartreuse;
                    lab_UNit.BackColor = Color.Chartreuse;
                    rs_MFC.FillColor = Color.Chartreuse;
                    rs_MFC.FillStyle = FillStyle.Solid;   
                }
            }
        }

        private string _sName = "";
        [Category("MFC Name"), Description("MFC Name Display")]
        public string MFCName
        {
            get { return _sName; }
            set
            {
                _sName = value;

                if (_sName != "")
                {
                    lab_Name.Text = _sName;
                    lab_Name.Visible = true;
                    btn_Set.Tag = _sName;
                    this.Tag = _sName;
                }
                else
                {
                    lab_Name.Text = "";
                    lab_Name.Visible = false;
                    btn_Set.Tag = "";
                    this.Tag = "";
                }
            }
        }

        private string _sMFCValue = "";
        [Category("MFC Value"), Description("MFC Value Display")]
        public string MFCValue
        {
            get { return _sMFCValue; }
            set
            {
                _sMFCValue = value;

                if (IsNumeric(_sMFCValue))
                {
                    lab_Value.Text = _sMFCValue;
                    if (_sMFCValue == "0")
                    {
                        MFCstatus = "0";
                    }
                    else
                    {
                        MFCstatus = "1";
                    }
                }
                else
                {
                    lab_Value.Text = "Err";
                    lab_Value.BackColor = Color.Red;
                    lab_UNit.BackColor = Color.Red;
                    rs_MFC.FillColor = Color.Red;
                    rs_MFC.FillStyle = FillStyle.Solid;  
                }
            }
        }

        //MFC Auto or Not
        private bool _bAuto = false;
        [Category("AutoMode"), Description("Is LL in Auto mode")]
        public bool AutoMode
        {
            get { return _bAuto; }
            set
            {
                _bAuto = value;

                if (_bAuto)
                {
                    btn_Set.Enabled = false;
                    txt_Value.Enabled = false;
                }
                else
                {
                    btn_Set.Enabled = true;
                    txt_Value.Enabled = true;
                }

                
            }
        }



        //private List<string> _PLCList = new List<string>();
        //[Category("Data"), Description("點位陣列")]
        //public List<string> PLCList
        //{
        //    get { return _PLCList; }
        //    set
        //    {
        //        _PLCList = value;

        //        if (_PLCList.Count != 0)
        //        {
        //            MFCName = PLCList[0].ToString();
        //            MFCstatus = PLCList[1].ToString();
        //            MFCValue = PLCList[2].ToString();

        //            switch (_PLCList[0].ToString())
        //            {
        //                case "0":;
        //                    break;
        //                case "1":
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //    }
        //}


        #endregion

        private void MFC_DoubleClick(object sender, EventArgs e)
        {
            //this.Resize -= new System.EventHandler(this.MFC_Resize);
            _bArea = true;
            this.Height = 84;
        }

        private void MFC_Load(object sender, EventArgs e)
        {
            //initialize
            MFCValue = "0";
            MFCName = "";
            this.Width = 96;
            this.Height = 36;
            _bArea = true;

        }




        //判斷TextBox輸入值是否為數字
        public static bool IsNumeric(string sValue)
        {
            try
            {
                int i = Convert.ToInt32(sValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void btn_Set_Click(object sender, EventArgs e)
        {
            if (txt_Value.Text.Length != 0 && IsNumeric(txt_Value.Text))
            {
                //寫入PLC
                //改變顯示
                _bArea = false;
                if (txt_Value.Text == "0")
                {
                    MFCValue = txt_Value.Text.Trim();
                }
                else
                {
                    MFCValue = txt_Value.Text.TrimStart('0');
                }
                this.Height = 36;
                Co_Btn_click(sender, e); 
                //this.Resize += new System.EventHandler(this.MFC_Resize);
            }
            else
            {
                MessageBox.Show("請輸入數值！");
            }
        }

        private void MFC_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            _bArea = false;
            if (AutoMode)
            {
                ClsTool.ShowWarningMsg("Auto Mode donot accept manual operation!!");
                return;
            }
            this.Height = 84;
            txt_Value.Text = MFCValue;
            txt_Value.Focus();
            this.BringToFront();
        }

        //按下ENTER
        private void Enter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Set_Click(this, null);
            }
        }

        private void lab_Name_DoubleClick(object sender, EventArgs e)
        {
            MFC_MouseDoubleClick(this, null);
        }

        private void MFC_Resize(object sender, EventArgs e)
        {
            if (_bArea)
            {
                this.Width = 96;
                this.Height = 36;                
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_Value.Text = "";
            _bArea = true;
            this.Width = 96;
            this.Height = 36;   
        }

        private void txt_Value_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 | (int)e.KeyChar > 57) & (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }


    }
}
