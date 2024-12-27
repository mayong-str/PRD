using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;

namespace RPD_1GUN
{
    class clsDB
    {
        public static SqlConnection SqlDBConn;
        public static SqlTransaction SqlDBTran;
        private static string MyPath = Application.StartupPath + "\\SQLError\\";
        public static bool bConnDB;
               
        // ***************************************
        // Open DataBase
        // ***************************************
        public static bool FunOpenDB()
        {
            if (!Directory.Exists(MyPath))
                Directory.CreateDirectory(MyPath);

            try
            {
                string sConnect = "";
                sConnect = "Initial Catalog=" + clsParam.gsDB_DSN + ";Password=" + clsParam.gsDB_Pwd + ";";
                sConnect = sConnect + "User ID=" + clsParam.gsDB_User + ";Data Source=" + clsParam.gsDB_Server;

                SqlDBConn = new SqlConnection(sConnect);
                SqlDBConn.Open();
                bConnDB = true;
                return true;
            }
            catch (Exception Ex)
            {
                StreamWriter sw = new StreamWriter(MyPath + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\r\n" + Ex.ToString());
                sw.Close();      

                bConnDB = false;
                SqlDBConn.Close();
                return false;
            }
        }


        //***************************************************************************************************
        //Function: Close DB
        //***************************************************************************************************
        public static bool FunClsDB()
        {
            if (!Directory.Exists(MyPath))
                Directory.CreateDirectory(MyPath);

            try
            {
                SqlDBConn.Close();
                return true;
            }
            catch (Exception Ex)
            {
                StreamWriter sw = new StreamWriter(MyPath + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\r\n" + Ex.ToString());
                sw.Close();      
                return false;
            }
        }


        //***************************************************************************************************
        //Function: BEGIN / COMMIT / ROLLBACK
        //***************************************************************************************************
        public static bool FunCommitCtrl(string sCommitLvl)
        {
            if (!Directory.Exists(MyPath))
                Directory.CreateDirectory(MyPath);

            try
            {
                switch (sCommitLvl)
                {
                    case "BEGIN":
                        SqlDBTran = SqlDBConn.BeginTransaction();
                        break;
                    case "COMMIT":
                        SqlDBTran.Commit();
                        break;
                    case "ROLLBACK":
                        SqlDBTran.Rollback();
                        break;
                    case "END":
                        SqlDBTran.Dispose();
                        break;
                }
                return true;
            }
            catch (Exception Ex)
            {
                StreamWriter sw = new StreamWriter(MyPath + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\r\n" + Ex.ToString());
                sw.Close();      

                SqlDBTran.Rollback();
                return false;
            }
        }


        //***************************************************************************************************
        //Function: Query (無資料時直接關閉物件,並回傳False)
        //***************************************************************************************************
        public static bool FunRsSql(string pSql, ref System.Data.Common.DbDataReader SqlRs)
        {
            if (!Directory.Exists(MyPath))
                Directory.CreateDirectory(MyPath);

            try
            {
                SqlCommand SqlDbCmd = new SqlCommand(pSql);
                SqlDbCmd.Connection = SqlDBConn;
                SqlRs = SqlDbCmd.ExecuteReader();

                if (!(SqlRs.HasRows))   //無資料則Close
                {
                    //cls_Param.FunWriteLog( pSql + " (No Data)");
                    SqlRs.Close();
                    return false;
                }
                else
                {
                    //cls_Param.FunWriteLog(pSql);
                    return true;
                }
            }
            catch (Exception Ex)
            {
                StreamWriter sw = new StreamWriter(MyPath + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\r\n" + pSql + "\r\r\n" + Ex.ToString());
                sw.Close();      
                return false;
            }
        }

        //傳回DataTable
        public static bool FunRsSql(string pSql, ref System.Data.DataTable SqlDbDt)
        {
            if (!Directory.Exists(MyPath))
                Directory.CreateDirectory(MyPath);

            try
            {
                SqlCommand SqlDbCmd = new SqlCommand(pSql);
                SqlDbCmd.Connection = SqlDBConn;
                SqlDataAdapter SqlDbDa = new SqlDataAdapter(SqlDbCmd);
                System.Data.DataSet SqlDbDs = new System.Data.DataSet();
                SqlDbDs.Clear();
                SqlDbDa.Fill(SqlDbDs);
                SqlDbDt = SqlDbDs.Tables[0];

                if (!(SqlDbDt.Rows.Count > 0))   //無資料則Close
                {
                    //cls_Param.FunWriteLog( pSql + " (No Data)");
                    return false;
                }
                else
                {
                    //cls_Param.FunWriteLog(pSql);
                    return true;
                }
            }
            catch (Exception Ex)
            {
                StreamWriter sw = new StreamWriter(MyPath + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\r\n" + pSql + "\r\r\n" + Ex.ToString());
                sw.Close();      
                return false;
            }
        }

        //***************************************************************************************************
        //Function: Insert / Update / Delete
        //***************************************************************************************************
        public static bool FunExecSql(string pSql)
        {
            if (!Directory.Exists(MyPath))
                Directory.CreateDirectory(MyPath);

            try
            {
                SqlCommand SqlDbCmd = new SqlCommand(pSql);
                SqlDbCmd.CommandType = System.Data.CommandType.Text;
                SqlDbCmd.CommandText = pSql;
                SqlDbCmd.Connection = SqlDBConn;
                SqlDbCmd.Transaction = SqlDBTran;
                bool bReturnValue;
                bReturnValue = (SqlDbCmd.ExecuteNonQuery() <= 0 ? false : true);

                return bReturnValue;
            }
            catch (Exception Ex)
           {
                StreamWriter sw = new StreamWriter(MyPath + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\r\n" + pSql + "\r\r\n" + Ex.ToString());
                sw.Close();                             

                
                return false;
            }
        }  






    }
}
