using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RPD_1GUN
{
    public partial class frmPCC : Form
    {
        DataTable _dtPCC = new DataTable();
        string _SQL = "";
        private static string MyPath = Application.StartupPath + "\\ThreadTimerError\\";

        public bool IsShow 
        {
            set { DisplayTimer.Enabled = value; }
            get { return DisplayTimer.Enabled; }        
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

        public frmPCC()
        {
            InitializeComponent();
        }

        private void frmPCC_Load(object sender, EventArgs e)
        {
            _SQL = @"SELECT * FROM PCC_TABLE ORDER BY [Type],NAME ASC";
            _dtPCC = clsDBN.GetTable(_SQL);

            int X = 15;
            int Y = 15;
            int Count = 0;
            foreach (DataRow drPCC in _dtPCC.Rows)
            {
                if (Count % 30 == 0 && Count != 0)
                {
                    X += 410;
                    Y = 15;
                }

                switch (drPCC["Type"].ToString())
                {
                    case "Text":
                        Label lblTitle = new Label();
                        lblTitle.Text = drPCC["SIGNALNAME"].ToString();
                        lblTitle.Font = new System.Drawing.Font("新細明體", 9, FontStyle.Bold);
                        lblTitle.Size = new System.Drawing.Size(280, 15);
                        lblTitle.Location = new Point(X, Y);

                        TextBox txtValue = new TextBox();
                        txtValue.Enabled = false;
                        txtValue.Name = "Value_" + drPCC["NAME"].ToString();
                        txtValue.Text = "0";
                        txtValue.BackColor = Color.WhiteSmoke;
                        txtValue.Font = new System.Drawing.Font("新細明體", 9, FontStyle.Bold);
                        txtValue.Size = new System.Drawing.Size(50, 15);
                        txtValue.Location = new Point(X + 280, Y -5);

                        Label lblUnit = new Label();
                        lblUnit.Text = drPCC["UNIT"].ToString();
                        lblUnit.Font = new System.Drawing.Font("新細明體", 9, FontStyle.Bold);
                        lblUnit.Size = new System.Drawing.Size(75, 15);
                        lblUnit.Location = new Point(X + 335, Y );

                        this.Controls.Add(lblTitle);
                        this.Controls.Add(txtValue);
                        this.Controls.Add(lblUnit);

                        Count++;
                        Y += 25;

                        break;
                    case "Lamp":
                        Label lblLamp = new Label();
                        lblLamp.Name = "Lamp_" + drPCC["NAME"].ToString();
                        lblLamp.Size = new System.Drawing.Size(24, 14);
                        lblLamp.Location = new Point(X, Y);
                        lblLamp.BackColor = Color.Lime;

                        Label lblDES = new Label();
                        lblDES.Text = drPCC["SIGNALNAME"].ToString();
                        lblDES.Font = new System.Drawing.Font("新細明體", 9, FontStyle.Bold);
                        lblDES.Size = new System.Drawing.Size(280, 15);
                        lblDES.Location = new Point(X + 50, Y);

                        this.Controls.Add(lblLamp);
                        this.Controls.Add(lblDES);

                        Count++;
                        Y += 25;
                        break;

                }

            }

           
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
            DisplayTimer.Enabled = false;
        }
        
         private void DisplayTimer_Tick(object sender, EventArgs e)
        {

            try
            {
                foreach (DataRow drPCC in _dtPCC.Rows)
                {
                    switch (drPCC["Type"].ToString())
                    {
                        case "Text":
                            if (RPD.STRCTURE.PCC1.Data.PCC_TABLE != null)
                            {
                                Control[] txt = this.Controls.Find("Value_" + drPCC["NAME"].ToString(), true);
                                int Name = Convert.ToInt16(drPCC["NAME"]);
                                if (txt.Length != 0)
                                {
                                    if ((Name >= 59 && Name <= 66) || (Name >= 71 && Name <= 78) || Name == 85 || (Name >= 87 && Name <= 98))
                                    {
                                        if ((Name >= 62 && Name <= 64))
                                        {
                                            //V 值有帶正負數，故先轉換二進制再轉回十進制
                                            int V_Temp = RPD.STRCTURE.PCC1.Data.PCC_TABLE[(int)drPCC["ARRAY_COUNT"]];
                                            V_Temp = Convert.ToInt16(Convert.ToString(V_Temp, 2), 2);
                                            txt[0].Text = ((double)V_Temp / 10).ToString();
                                        }
                                        else
                                            txt[0].Text = ((double)RPD.STRCTURE.PCC1.Data.PCC_TABLE[(int)drPCC["ARRAY_COUNT"]] / 10).ToString();
                                    }
                                    else if (Name == 83 || Name == 84)
                                        txt[0].Text = ((double)RPD.STRCTURE.PCC1.Data.PCC_TABLE[(int)drPCC["ARRAY_COUNT"]] / 100).ToString();
                                    else
                                        txt[0].Text = RPD.STRCTURE.PCC1.Data.PCC_TABLE[(int)drPCC["ARRAY_COUNT"]].ToString();

                                }
                            }
                            break;
                        case "Lamp":
                            if (RPD.STRCTURE.PCC1.Data.PCC_TABLE != null)
                            {
                                if ((RPD.STRCTURE.PCC1.Data.PCC_TABLE[(int)drPCC["ARRAY_COUNT"]] & ((int)(Math.Pow(2, (int)drPCC["ARRAY_INDEX"])))) != 0)
                                {
                                    Control[] a = this.Controls.Find("Lamp_" + drPCC["NAME"].ToString(), true);
                                    if (a.Length != 0)
                                        a[0].BackColor = Color.Lime;
                                }
                                else
                                {
                                    Control[] a = this.Controls.Find("Lamp_" + drPCC["NAME"].ToString(), true);
                                    if (a.Length != 0)
                                        a[0].BackColor = Color.Red;
                                }
                            }
                            break;
                    }
                }

            }
            catch (Exception Ex)
            {
                StreamWriter sw = new StreamWriter(MyPath + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\r\n" + Ex.ToString());
                sw.Close();
            }
            finally
            {

            }

        }

    }
}
