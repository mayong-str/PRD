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
    public partial class frmLeakTestLog : Form
    {
        public frmLeakTestLog()
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

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string SQL = @"";
            SQL = @"SELECT STR_DT as Date,
                           STR_P as 'Start Pressure(Torr)',
                           END_P as 'End Pressure(Torr)',
                           LEAKRATE as 'Leak Rate(Torr/Min)',
                           DURATION as 'Duration(Min)'
                           FROM LEAK_TEST WHERE STR_DT BETWEEN '" + dtpStart.Text + "' AND '" + dtpEnd.Text + "'";
            dgvLeakTestLog.DataSource = clsDBN.GetTable(SQL);

        


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLeakTestLog_Load(object sender, EventArgs e)
        {
            dtpStart.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            dtpEnd.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
        }
    }
}
