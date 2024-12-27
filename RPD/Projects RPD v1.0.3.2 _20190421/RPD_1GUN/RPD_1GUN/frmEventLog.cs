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
    public partial class frmEventLog : Form
    {
        string _SQL = @"";

        public frmEventLog()
        {
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
        private void frmEventLog_Load(object sender, EventArgs e)
        {
            _SQL = @"SELECT DISTINCT EQ FROM ALARM_DEF";
            DataTable dt_EQ = clsDBN.GetTable(_SQL);

            foreach(DataRow dr_EQ in dt_EQ.Rows)
            {
                lstModule.Items.Add(dr_EQ["EQ"]);
            }

            SubSetHour(ref cboStartTime);
            SubSetHour(ref cboEndTime);

            CalStartDate.TodayDate = DateTime.Now;
            CalEndDate.TodayDate = DateTime.Now;
        }

        private void SubSetHour(ref ComboBox objCboBox)
        {
            objCboBox.Items.Clear();
            objCboBox.Items.Add("");

            for (int i = 0; i <= 23; i++)
            {
                if (i < 10)
                {
                    objCboBox.Items.Add("0" + i.ToString());
                }
                else
                {
                    objCboBox.Items.Add(i.ToString());
                }
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            _SQL = @"SELECT EQ,ALARM_TYPE,ALARM_DESC,CONVERT(varchar(100), STR_DT, 120) AS STR_DT,[USER_ID],RECIPE_NUMBER as 'RecipeNumber' FROM ALARM_LOG WHERE 1 =1 ";

            string sTemp_Type = "";

            if (lstModule.SelectedItems.Count > 0)
            {
                string sTmp = "";
                for (int i = 0; i <= lstModule.SelectedItems.Count - 1; i++)
                {
                    if (sTmp == "")
                    {
                        sTmp = "'" + lstModule.SelectedItems[i].ToString() + "'";
                    }
                    else
                    {
                        sTmp = sTmp + ",'" + lstModule.SelectedItems[i].ToString() + "'";
                    }
                }
                _SQL = _SQL + "AND EQ IN (" + sTmp + ") ";
            }

            if(chkAlarm.Checked)
            {
                if (sTemp_Type == "")
                    sTemp_Type = "'A'";
                else
                    sTemp_Type += ",'A'";
            }

            if(chkEvent.Checked)
            {
                if (sTemp_Type == "")
                    sTemp_Type = "'E'";
                else
                    sTemp_Type += ",'E'";
            }

            if (chkOp.Checked)
            {
                if (sTemp_Type == "")
                    sTemp_Type = "'O'";
                else
                    sTemp_Type += ",'O'";
            }

            if (chkWarning.Checked)
            {
                if (sTemp_Type == "")
                    sTemp_Type = "'W'";
                else
                    sTemp_Type += ",'W'";
            }

            if (sTemp_Type != "")
                _SQL += "AND ALARM_TYPE IN (" + sTemp_Type + ") ";


            #region Start-End Time
            string sStartDate = ""; string sStartTime = "";
            string sYear = CalStartDate.SelectionRange.Start.Year.ToString();
            string sMonth = CalStartDate.SelectionRange.Start.Month.ToString().PadLeft(2, Convert.ToChar("0"));
            string sDay = CalStartDate.SelectionRange.Start.Day.ToString().PadLeft(2, Convert.ToChar("0"));
            sStartDate = sYear + "-" + sMonth + "-" + sDay + " ";
            sStartTime = cboStartTime.Text + ":00:00";

            string sEndDate = ""; string sEndTime = "";
            sYear = CalEndDate.SelectionRange.Start.Year.ToString();
            sMonth = CalEndDate.SelectionRange.Start.Month.ToString().PadLeft(2, Convert.ToChar("0"));
            sDay = CalEndDate.SelectionRange.Start.Day.ToString().PadLeft(2, Convert.ToChar("0"));
            sEndDate = sYear + "-" + sMonth + "-" + sDay + " ";
            sEndTime = cboEndTime.Text + ":00:00"; 

            sStartDate = sStartDate + sStartTime;
            sEndDate = sEndDate + sEndTime;

            string sTemp_Date = "AND STR_DT BETWEEN '" + sStartDate + "' AND '" + sEndDate + "' ";
            #endregion

            _SQL += sTemp_Date;
            _SQL += " ORDER BY STR_DT DESC";
            dgvEventLog.DataSource = clsDBN.GetTable(_SQL);


        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            try
            {
                clsDBN.ExportDataTableToCsv((DataTable)dgvEventLog.DataSource, "Log_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv");
                MessageBox.Show("Export Suecessed", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Export Error", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void frmEventLog_Leave(object sender, EventArgs e)
        {
            CalStartDate.SetDate(DateTime.Now);
            CalEndDate.SetDate(DateTime.Now);
            CalStartDate.TodayDate = DateTime.Now;
            CalEndDate.TodayDate = DateTime.Now;

        }
    }
}
