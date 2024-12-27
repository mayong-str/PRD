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
    public partial class frmAutomationLog : Form
    {
        public frmAutomationLog()
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

        private void frmAutomationLog_Load(object sender, EventArgs e)
        {
            dtpStart.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            dtpEnd.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            clsDBN.GetDBtoCmb("SELECT DES,EQ FROM AUTOMATIONDATA_BT", ref cmbDES);
            cmbDES.Items.Insert(0, "All");
            cmbDES.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string SQL = @"";
            SQL = @"SELECT b.DES,a.DATA,CONVERT(varchar(20), a.INSERTTIME, 120) AS 'STR_DT',a.RECIPE_NUMBER FROM AUTOMATIONDATA_LOG a INNER JOIN AUTOMATIONDATA_BT b ON a.EQ = b.EQ WHERE a.INSERTTIME BETWEEN '" + dtpStart.Text + "' AND '" + dtpEnd.Text + "'";

            if (!cmbDES.SelectedIndex.Equals(0))
                SQL += " AND a.EQ ='" + cmbDES.SelectedIndex + "'";

            SQL += " ORDER BY STR_DT DESC";

            dgvAutomationLog.DataSource = clsDBN.GetTable(SQL);


        }



    }
}
