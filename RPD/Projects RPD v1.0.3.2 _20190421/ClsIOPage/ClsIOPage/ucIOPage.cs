﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClsIOPage
{
    public partial class ucIOPage : UserControl
    {

        string _SQL = @"";
        DataTable _dtIO = null;
        private static string MyPath = Application.StartupPath + "\\ThreadTimerError\\";

        System.Threading.Timer _ThreadTimer = null;
        private static System.Threading.Mutex Timermutex = new System.Threading.Mutex();

       
        public ucIOPage()
        {
            InitializeComponent();
        }

        private void ucIOPage_Load(object sender, EventArgs e)
        {
            _SQL = @"SELECT * FROM INTERLOCK_TABLE WHERE DES <> '' AND DES <> 'NA' AND DES <> 'Spare' ";
            _dtIO = clsDBN.GetTable(_SQL);

            int X = 15;
            int Y = 15;
            int Count = 0;
            foreach (DataRow drInterlock in _dtInterlock.Rows)
            {
                if (Count % 35 == 0 && Count != 0)
                {
                    X += 250;
                    Y = 15;
                }

                Label lblLamp = new Label();
                lblLamp.Name = "Lamp_" + drInterlock["ADDRESS"].ToString();
                lblLamp.Size = new System.Drawing.Size(24, 14);
                lblLamp.Location = new Point(X, Y);
                lblLamp.BackColor = Color.Lime;

                Label lblDES = new Label();
                lblDES.Name = "Des_" + drInterlock["ADDRESS"].ToString();
                lblDES.Text = drInterlock["DES"].ToString();
                lblDES.Font = new System.Drawing.Font("新細明體", 9, FontStyle.Bold);
                lblDES.Size = new System.Drawing.Size(200, 15);
                lblDES.Location = new Point(X + 45, Y);

                this.Controls.Add(lblLamp);
                this.Controls.Add(lblDES);

                Count++;
                Y += 25;

            }

            _ThreadTimer = new System.Threading.Timer(new System.Threading.TimerCallback(TimerThread_Tick), "Timer", 0, 1000); //宣告Timer



        }


        private void TimerThread_Tick(object sender)
        {
            Timermutex.WaitOne();

            try
            {
                foreach (DataRow drInterlock in _dtInterlock.Rows)
                {
                    if ((RPD.STRCTURE.SYS.Data.INTERLOCK_TABLE[(int)drInterlock["ARRAY_COUNT"]] & ((int)(Math.Pow(2, (int)drInterlock["ARRAY_INDEX"])))) != 0)
                    {
                        Control[] a = this.Controls.Find("Lamp_" + drInterlock["ADDRESS"].ToString(), true);
                        a[0].BackColor = Color.Red;
                    }
                    else
                    {
                        Control[] a = this.Controls.Find("Lamp_" + drInterlock["ADDRESS"].ToString(), true);
                        a[0].BackColor = Color.Lime;
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

            Timermutex.ReleaseMutex();

        }








    }
}
