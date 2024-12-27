using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace RPD_1GUN
{
    class clsParam
    {

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        #region Define Database Parameters
        public static string gsDB_DBMS = "";
        public static string gsDB_Server = "";
        public static string gsDB_DSN = "";
        public static string gsDB_User = "";
        public static string gsDB_Pwd = "";
        public static DataTable TMAlarmDefTable = new DataTable();
        public static DataTable PCAlarmDefTable = new DataTable();

        public static Dictionary<string, string> RPDAlarm = new Dictionary<string, string>();
        public static Dictionary<string, string> SHIAlarm = new Dictionary<string, string>();

        public static DataTable RPDEventDefTable = new DataTable();
        public static DataTable SHIEventDefTable = new DataTable();

        public static Dictionary<string, string> RPDEvent = new Dictionary<string, string>();
        public static Dictionary<string, string> SHIEvent = new Dictionary<string, string>();

        public static Dictionary<string, string> RPDAlarmTemp = new Dictionary<string, string>();
        public static Dictionary<string, string> SHIAlarmTemp = new Dictionary<string, string>();

        public static DataTable RPDOperationDefTable = new DataTable();
        public static DataTable SHIOperationDefTable = new DataTable();
        public static Dictionary<string, string> RPDOperation = new Dictionary<string, string>();
        public static Dictionary<string, string> SHICOperation = new Dictionary<string, string>();



        #endregion

      

        public static int giStationNumer_PLC1 = 0;

        public static void SubLoadSysIni()
        {
            string sFileName = null;

            sFileName = System.Windows.Forms.Application.StartupPath + "\\RPD.ini";
            if (!System.IO.File.Exists(sFileName))
            {
                System.Windows.Forms.MessageBox.Show("System error，contact your vendor administrator!!", "RPD", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                System.Environment.Exit(0);
            }

            //Get PLC's StationNumber
            string sAppName = "";

            //Get Project's Title
            sAppName = "PROJECT";

            //Get DataBase
            sAppName = "DATABASE INFO";
            gsDB_DBMS = FunReadParam(sFileName, sAppName, "DBMS");
            gsDB_Server = FunReadParam(sFileName, sAppName, "DBSERVER");
            gsDB_DSN = FunReadParam(sFileName, sAppName, "DBDSN");
            gsDB_User = FunReadParam(sFileName, sAppName, "DBUSER");
            gsDB_Pwd = FunReadParam(sFileName, sAppName, "DBPWD");
            //clsASRS.gsFailover = FunReadParam(sFileName, sAppName, "FAILOVER");



            //Get PLC's StationNumber
            string sValue = "";
            sAppName = "PLC INFO";
            sValue = FunReadParam(sFileName, sAppName, "StationNumber");
            giStationNumer_PLC1 = int.Parse(sValue);

        }



        //***************************************************************************************************
        //Function: 讀取ini檔的單一欄位
        //***************************************************************************************************
        public static string FunReadParam(string sFileName, string sAppName, string sKeyName)
        {
            string sDefault = null;
            StringBuilder sResult = new StringBuilder(80);
            int lResult = 0;
            sDefault = "";

            lResult = GetPrivateProfileString(sAppName, sKeyName, sDefault, sResult, 255, sFileName);

            string temp = sResult.ToString().Trim();
            return temp.Trim();
        }

        //***************************************************************************************************
        //Function: Write Log Function
        //***************************************************************************************************
        public static void FunWriteLog(string sTemp)
        {
            try
            {
                string sFileName = null;
                sFileName = System.Windows.Forms.Application.StartupPath;
                sFileName = sFileName + "\\log";
                if (System.IO.Directory.Exists(sFileName) == false)
                {
                    System.IO.Directory.CreateDirectory(sFileName);
                }

                string sFile = "";
                sFile = System.DateTime.Now.ToString("yyyyMMdd") + ".log";
                sFileName = sFileName + "\\" + sFile;

                sTemp = "[" + System.DateTime.Now.ToString("HH:mm:ss") + "] " + sTemp;
                if (System.IO.File.Exists(sFileName) == false)
                {
                    using (System.IO.StreamWriter sw = System.IO.File.CreateText(sFileName))
                    {
                        sw.WriteLine(sTemp);
                        sw.Close();
                    }
                }
                else
                {
                    using (System.IO.StreamWriter sw = System.IO.File.AppendText(sFileName))
                    {
                        sw.WriteLine(sTemp);
                        sw.Flush();
                        sw.Close();
                    }
                }
            }
            catch
            {

            }
        }

        //***************************************************************************************************
        // Load AlarmDef Function
        //***************************************************************************************************
        public static DataTable FunReadAlarmDef(string sEQ)
        {
            DataTable sTable = new DataTable();
            if (clsDB.FunOpenDB() == false) { return sTable; };
            string strSql = "";
            strSql = "SELECT * FROM ALARM_DEF WHERE EQ='" + sEQ + "'";
            clsDB.FunRsSql(strSql, ref sTable);

            clsDB.FunClsDB();
            return sTable;

        }

        //***************************************************************************************************
        // Load EventDef Function
        //***************************************************************************************************
        public static DataTable FunReadEventDef(string sEQ)
        {
            DataTable sTable = new DataTable();
            if (clsDB.FunOpenDB() == false) { return sTable; };
            string strSql = "";
            strSql = "SELECT * FROM EVENT_DEF WHERE EQ='" + sEQ + "'";
            clsDB.FunRsSql(strSql, ref sTable);

            clsDB.FunClsDB();
            return sTable;

        }

        //***************************************************************************************************
        // Load OperationDef Function
        //***************************************************************************************************

        public static DataTable FunReadOpDef(string sEQ)
        {
            DataTable sTable = new DataTable();
            if (clsDB.FunOpenDB() == false) { return sTable; };
            string strSql = "";
            strSql = "SELECT * FROM OPERATION_DEF WHERE EQ ='" + sEQ + "'";
            clsDB.FunRsSql(strSql, ref sTable);

            clsDB.FunClsDB();
            return sTable;

        }


    }
}
