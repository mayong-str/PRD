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

namespace PCC_Two
{
    public partial class PCC : UserControl
    {
        #region PCC Sub 屬性


        #region is PCC two in vacuum
        private bool _bIsVac = false;
        [Category("IsVacuum"), Description("Is PCC two in vacuum")]
        public bool IsVac
        {
            get { return _bIsVac; }
            set
            {
                _bIsVac = value;

                if (_bIsVac) //in vacuum change Back color
                {
                    rs_BackG.FillColor = Color.Wheat;
                    rs_BackG.SendToBack();
                    rectangleShape27.SendToBack();
                    rs_cover.FillColor = Color.Wheat;
                    //lab_ATMG.BackColor = SystemColors.Control;
                    os_TRATMG.FillColor = SystemColors.Control;
                    TRPHGauge.Signal = 1;
                }
                else
                {
                    rs_BackG.FillColor = Color.White;
                    rs_BackG.SendToBack();
                    rectangleShape27.SendToBack();
                    //lab_ATMG.BackColor = Color.Chartreuse;
                    rs_cover.FillColor = Color.White;
                    os_TRATMG.FillColor = Color.Chartreuse;
                    TRPHGauge.Signal = 2;
                }
            }
        }

        #endregion

        #region is PCC two in ATM
        private bool _bIsATM = false;
        [Category("IsATM"), Description("Is PCC two in ATM")]
        public bool IsATM
        {
            get { return _bIsATM; }
            set
            {
                _bIsATM = value;

                if (!_bIsATM) //in vacuum change Back color
                {
                    //lab_ATMG.BackColor = SystemColors.Control;
                    os_TRATMG.FillColor = SystemColors.Control;

                    TRPHGauge.Signal = 1;
                }
                else
                {
                    //lab_ATMG.BackColor = Color.Chartreuse;
                    os_TRATMG.FillColor = Color.Chartreuse;
                    TRPHGauge.Signal = 1;
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
                    return;
                }

                if (!ClsTool.IsNumeric(_sPreVal))
                {
                    _sPreVal = "Err";
                    lab_PHGValue.Text = _sPreVal;
                    TRPHGauge.Signal = 0;
                    return;
                }

                _sPreVal = _sPreVal.Trim();

                lab_PHGValue.Text = _sPreVal;

                if (Convert.ToDouble(_sPreVal) < 760)
                {
                    TRPHGauge.Signal = 1;
                }
                else
                {
                    TRPHGauge.Signal = 2;
                }
            }
        }

        #endregion



        #endregion


        public PCC()
        {
            InitializeComponent();
        }


    }

}
