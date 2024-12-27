using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Xml;
using System.Xml.Linq;


namespace ClsDBN
{

    public class ClsDBN
    {
        private XDocument xmlDoc;
        private string XML_Path = System.Net.Mime.MediaTypeNames.Application.StartupPath + "\\DBSetting.xml";
        private string MyPath = Application.StartupPath + "\\SQLError\\";
        private string ConnectionString = @""; 
        private List<SqlParameter> sSqlParameter = new List<SqlParameter>();

        public ClsDBN()
        {
            if (!Directory.Exists(MyPath))
                Directory.CreateDirectory(MyPath);

            sSqlParameter.Clear();
            LoadXml();
        }

        public System.Collections.Generic.List<SqlParameter> SqlParameter
        {
            set { sSqlParameter = value; }
            get { return sSqlParameter; }
        }

        /// 執行SQL指令並回傳受影響的資料行數目
        /// <summary> 執行SQL指令並回傳受影響的資料行數目</summary>
        /// <param name="SqlCommand">要執行的SQL指令</param>
        /// <returns>回傳受影響的資料行數目</returns>      
        public int ExecuteSqlCommand(string SqlCommand)
        {

            int MyRow = 0;
            SqlCommand sSqlCommand = new SqlCommand();
            sSqlCommand.CommandTimeout = 300; //設置SqlCommand TimeOut 300
            sSqlCommand.Connection = new SqlConnection(ConnectionString);

            lock (sSqlCommand.Connection)
            {
                try
                {
                    sSqlCommand.Connection.Open();


                    sSqlCommand.CommandText = SqlCommand;
                    sSqlCommand.CommandType = CommandType.Text;
                    sSqlCommand.Parameters.Clear();
                    if (sSqlParameter.Count > 0)
                    {
                        foreach (SqlParameter MySqlParameter in sSqlParameter)
                        {
                            sSqlCommand.Parameters.Add(MySqlParameter);
                        }
                    }
                    MyRow = sSqlCommand.ExecuteNonQuery();


                }
                catch (Exception Ex)
                {
                    StreamWriter sw = new StreamWriter(MyPath + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\r\n" + Ex.ToString());
                    sw.Close();
                    MyRow = -1;


                }
                finally
                {

                    sSqlParameter.Clear();
                    sSqlCommand.Connection.Close();
                }

            }
            return MyRow;

        }
        public int ExecuteSqlCommand(string SqlCommand, string ReplaceConStr)
        {

            int MyRow = 0;
            SqlCommand sSqlCommand = new SqlCommand();
            sSqlCommand.CommandTimeout = 300; //設置SqlCommand TimeOut 300
            sSqlCommand.Connection = new SqlConnection(ReplaceConStr);

            lock (sSqlCommand.Connection)
            {
                try
                {
                    sSqlCommand.Connection.Open();


                    sSqlCommand.CommandText = SqlCommand;
                    sSqlCommand.CommandType = CommandType.Text;
                    sSqlCommand.Parameters.Clear();
                    if (sSqlParameter.Count > 0)
                    {
                        foreach (SqlParameter MySqlParameter in sSqlParameter)
                        {
                            sSqlCommand.Parameters.Add(MySqlParameter);
                        }
                    }
                    MyRow = sSqlCommand.ExecuteNonQuery();


                }
                catch (Exception Ex)
                {
                    StreamWriter sw = new StreamWriter(MyPath + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\r\n" + Ex.ToString());
                    sw.Close();
                    MyRow = -1;

                }
                finally
                {

                    sSqlParameter.Clear();
                    sSqlCommand.Connection.Close();
                }

            }
            return MyRow;

        }

        /// 執行SQL指令並回傳查詢結果的第一個資料行的第一個資料列Object
        /// <summary> 執行SQL指令並回傳查詢結果的第一個資料行的第一個資料列Object</summary>
        /// <param name="SqlCommand">要執行的SQL指令</param>
        /// <returns>回傳查詢結果的第一個資料行的第一個資料列</returns>
        public object GetObject(string SqlCommand)
        {
            object obj = new object();
            SqlCommand sSqlCommand = new SqlCommand();
            sSqlCommand.CommandTimeout = 300; //設置SqlCommand TimeOut 300
            sSqlCommand.Connection = new SqlConnection(ConnectionString);

            lock (sSqlCommand.Connection)
            {
                try
                {
                    sSqlCommand.Connection.Open();

                    sSqlCommand.CommandText = SqlCommand;
                    sSqlCommand.CommandType = CommandType.Text;
                    sSqlCommand.Parameters.Clear();

                    if (sSqlParameter.Count > 0)
                    {
                        foreach (SqlParameter MySqlParameter in sSqlParameter)
                        {
                            sSqlCommand.Parameters.Add(MySqlParameter);
                        }
                    }
                    obj = sSqlCommand.ExecuteScalar();

                }

                catch (Exception Ex)
                {
                    StreamWriter sw = new StreamWriter(MyPath + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n" + Ex.ToString());
                    sw.Close();
                    obj = null;

                }
                finally
                {

                    sSqlParameter.Clear();
                    sSqlCommand.Connection.Close();
                }
            }
            return obj;
        }
        public object GetObject(string SqlCommand, string ReplaceConStr)
        {
            object obj = new object();
            obj = null;
            SqlCommand sSqlCommand = new SqlCommand();
            sSqlCommand.CommandTimeout = 300; //設置SqlCommand TimeOut 300
            sSqlCommand.Connection = new SqlConnection(ReplaceConStr);

            lock (sSqlCommand.Connection)
            {
                try
                {
                    sSqlCommand.Connection.Open();

                    sSqlCommand.CommandText = SqlCommand;
                    sSqlCommand.CommandType = CommandType.Text;
                    sSqlCommand.Parameters.Clear();

                    if (sSqlParameter.Count > 0)
                    {
                        foreach (SqlParameter MySqlParameter in sSqlParameter)
                        {
                            sSqlCommand.Parameters.Add(MySqlParameter);
                        }
                    }
                    obj = sSqlCommand.ExecuteScalar();

                }

                catch (Exception Ex)
                {
                    StreamWriter sw = new StreamWriter(MyPath + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n" + Ex.ToString());
                    sw.Close();
                    obj = null;

                }
                finally
                {

                    sSqlParameter.Clear();
                    sSqlCommand.Connection.Close();
                }
            }
            return obj;
        }

        /// 執行SQL指令並回傳資料Table
        /// <summary>執行SQL指令並回傳資料Table</summary>
        /// <param name="SqlCommand">要執行的SQL指令</param>
        /// <returns>查詢後的Table</returns>
        public DataTable GetTable(string SqlCommand)
        {
            DataTable MyTable = new DataTable();
            SqlCommand sSqlCommand = new SqlCommand();
            sSqlCommand.CommandTimeout = 300; //設置SqlCommand TimeOut 300
            sSqlCommand.Connection = new SqlConnection(ConnectionString);

            lock (sSqlCommand.Connection)
            {
                try
                {
                    sSqlCommand.Connection.Open();

                    sSqlCommand.CommandText = SqlCommand;
                    sSqlCommand.CommandType = CommandType.Text;
                    sSqlCommand.Parameters.Clear();

                    if (sSqlParameter.Count > 0)
                    {
                        foreach (SqlParameter MySqlParameter in sSqlParameter)
                        {
                            sSqlCommand.Parameters.Add(MySqlParameter);
                        }
                    }
                    SqlDataAdapter sSqlDataAdapter = new SqlDataAdapter(sSqlCommand);
                    sSqlDataAdapter.Fill(MyTable);

                    sSqlDataAdapter.Dispose();

                }
                catch (Exception Ex)
                {

                    StreamWriter sw = new StreamWriter(MyPath + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n" + Ex.ToString());
                    sw.Close();
                    MyTable = null;


                }
                finally
                {

                    sSqlParameter.Clear();
                    sSqlCommand.Connection.Close();
                }
            }
            return MyTable;
        }
        public DataTable GetTable(string SqlCommand, string ReplaceConStr)
        {
            DataTable MyTable = new DataTable();
            SqlCommand sSqlCommand = new SqlCommand();
            sSqlCommand.CommandTimeout = 300; //設置SqlCommand TimeOut 300
            sSqlCommand.Connection = new SqlConnection(ReplaceConStr);

            lock (sSqlCommand.Connection)
            {
                try
                {
                    sSqlCommand.Connection.Open();

                    sSqlCommand.CommandText = SqlCommand;
                    sSqlCommand.CommandType = CommandType.Text;
                    sSqlCommand.Parameters.Clear();

                    if (sSqlParameter.Count > 0)
                    {
                        foreach (SqlParameter MySqlParameter in sSqlParameter)
                        {
                            sSqlCommand.Parameters.Add(MySqlParameter);
                        }
                    }
                    SqlDataAdapter sSqlDataAdapter = new SqlDataAdapter(sSqlCommand);
                    sSqlDataAdapter.Fill(MyTable);

                    sSqlDataAdapter.Dispose();

                }
                catch (Exception Ex)
                {
                    StreamWriter sw = new StreamWriter(MyPath + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n" + Ex.ToString());
                    sw.Close();
                    MyTable = null;

                }
                finally
                {

                    sSqlParameter.Clear();
                    sSqlCommand.Connection.Close();
                }
            }
            return MyTable;
        }

        /// 執行SQL指令並回傳資料Row
        /// <summary>執行SQL指令並回傳資料Row</summary>
        /// <param name="SqlCommand">要執行的SQL指令</param>
        /// <returns>查詢後的Row</returns>
        public DataRow GetRow(string SqlCommand)
        {
            DataTable MyRow = new DataTable();
            SqlCommand sSqlCommand = new SqlCommand();
            sSqlCommand.CommandTimeout = 300; //設置SqlCommand TimeOut 300
            sSqlCommand.Connection = new SqlConnection(ConnectionString);

            lock (sSqlCommand.Connection)
            {
                try
                {
                    sSqlCommand.Connection.Open();

                    sSqlCommand.CommandText = SqlCommand;
                    sSqlCommand.CommandType = CommandType.Text;
                    sSqlCommand.Parameters.Clear();

                    if (sSqlParameter.Count > 0)
                    {
                        foreach (SqlParameter MySqlParameter in sSqlParameter)
                        {
                            sSqlCommand.Parameters.Add(MySqlParameter);
                        }
                    }

                    SqlDataAdapter sSqlDataAdapter = new SqlDataAdapter(sSqlCommand);
                    sSqlDataAdapter.Fill(MyRow);

                    sSqlDataAdapter.Dispose();
                }

                catch (Exception Ex)
                {
                    StreamWriter sw = new StreamWriter(MyPath + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n" + Ex.ToString());
                    sw.Close();
                    MyRow = null;

                }
                finally
                {

                    sSqlParameter.Clear();
                    sSqlCommand.Connection.Close();
                }
            }
            if (MyRow.Rows.Count != 0)
                return MyRow.Rows[0];
            else
                return null;
        }
        public DataRow GetRow(string SqlCommand, string ReplaceConStr)
        {
            DataTable MyRow = new DataTable();
            SqlCommand sSqlCommand = new SqlCommand();
            sSqlCommand.CommandTimeout = 300; //設置SqlCommand TimeOut 300
            sSqlCommand.Connection = new SqlConnection(ReplaceConStr);

            lock (sSqlCommand.Connection)
            {
                try
                {
                    sSqlCommand.Connection.Open();

                    sSqlCommand.CommandText = SqlCommand;
                    sSqlCommand.CommandType = CommandType.Text;
                    sSqlCommand.Parameters.Clear();

                    if (sSqlParameter.Count > 0)
                    {
                        foreach (SqlParameter MySqlParameter in sSqlParameter)
                        {
                            sSqlCommand.Parameters.Add(MySqlParameter);
                        }
                    }

                    SqlDataAdapter sSqlDataAdapter = new SqlDataAdapter(sSqlCommand);
                    sSqlDataAdapter.Fill(MyRow);

                    sSqlDataAdapter.Dispose();
                }

                catch (Exception Ex)
                {
                    StreamWriter sw = new StreamWriter(MyPath + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n" + Ex.ToString());
                    sw.Close();
                    MyRow = null;

                }
                finally
                {
                    sSqlParameter.Clear();
                    sSqlCommand.Connection.Close();
                }
            }
            if (MyRow.Rows.Count != 0)
                return MyRow.Rows[0];
            else
                return null;
        }

        /// 執行SQL指令並將值填入ComboBox
        /// <summary>執行SQL指令並將值填入ComboBox</summary>
        /// <param name="SqlCommand">要執行的SQL指令</param>
        /// <param name="ComboBox">要填值的ComboBox</param>
        public void GetDBtoCmb(string SqlCommand, ref ComboBox ComboBox)
        {
            ComboBox.Items.Clear();
            SqlCommand sSqlCommand = new SqlCommand();
            sSqlCommand.CommandTimeout = 300; //設置SqlCommand TimeOut 300
            sSqlCommand.Connection = new SqlConnection(ConnectionString);

            lock (sSqlCommand.Connection)
            {
                try
                {
                    sSqlCommand.Connection.Open();

                    sSqlCommand.CommandText = SqlCommand;
                    sSqlCommand.CommandType = CommandType.Text;
                    sSqlCommand.Parameters.Clear();

                    if (sSqlParameter.Count > 0)
                    {
                        foreach (SqlParameter MySqlParameter in sSqlParameter)
                        {
                            sSqlCommand.Parameters.Add(MySqlParameter);
                        }
                    }
                    SqlDataReader DataReader = sSqlCommand.ExecuteReader();
                    while (DataReader.Read())
                    {
                        if (!DataReader[0].Equals(DBNull.Value))
                        {
                            ComboBox.Items.Add(DataReader[0]);
                        }
                    }

                    DataReader.Close();

                }

                catch (Exception Ex)
                {
                    StreamWriter sw = new StreamWriter(MyPath + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n" + Ex.ToString());
                    sw.Close();
                 

                }
                finally
                {

                    sSqlParameter.Clear();
                    sSqlCommand.Connection.Close();
                }
            }
        }
        public void GetDBtoCmb(string SqlCommand, string ReplaceConStr, ref ComboBox ComboBox)
        {
            ComboBox.Items.Clear();
            SqlCommand sSqlCommand = new SqlCommand();
            sSqlCommand.CommandTimeout = 300; //設置SqlCommand TimeOut 300
            sSqlCommand.Connection = new SqlConnection(ReplaceConStr);

            lock (sSqlCommand.Connection)
            {
                try
                {
                    sSqlCommand.Connection.Open();

                    sSqlCommand.CommandText = SqlCommand;
                    sSqlCommand.CommandType = CommandType.Text;
                    sSqlCommand.Parameters.Clear();

                    if (sSqlParameter.Count > 0)
                    {
                        foreach (SqlParameter MySqlParameter in sSqlParameter)
                        {
                            sSqlCommand.Parameters.Add(MySqlParameter);
                        }
                    }
                    SqlDataReader DataReader = sSqlCommand.ExecuteReader();
                    while (DataReader.Read())
                    {
                        if (!DataReader[0].Equals(DBNull.Value))
                        {
                            ComboBox.Items.Add(DataReader[0]);
                        }
                    }
                    DataReader.Close();

                }

                catch (Exception Ex)
                {
                    StreamWriter sw = new StreamWriter(MyPath + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n" + Ex.ToString());
                    sw.Close();
              

                }
                finally
                {

                    sSqlParameter.Clear();
                    sSqlCommand.Connection.Close();
                }
            }
        }

        /// 執行SQL指令並將值填入CheckedListBox
        /// <summary>執行SQL指令並將值填入CheckedListBox</summary>
        /// <param name="SqlCommand">要執行的SQL指令</param>
        /// <param name="CheckListBox">要填值的CheckedListBox</param>
        public void GetDBtoCklbx(string SqlCommand, ref CheckedListBox CheckListBox)
        {
            CheckListBox.Items.Clear();
            SqlCommand sSqlCommand = new SqlCommand();
            sSqlCommand.CommandTimeout = 300; //設置SqlCommand TimeOut 300
            sSqlCommand.Connection = new SqlConnection(ConnectionString);

            lock (sSqlCommand.Connection)
            {
                try
                {
                    sSqlCommand.Connection.Open();

                    sSqlCommand.CommandText = SqlCommand;
                    sSqlCommand.CommandType = CommandType.Text;
                    sSqlCommand.Parameters.Clear();
                    if (sSqlParameter.Count > 0)
                    {
                        foreach (SqlParameter MySqlParameter in sSqlParameter)
                        {
                            sSqlCommand.Parameters.Add(MySqlParameter);
                        }
                    }
                    SqlDataReader DataReader = sSqlCommand.ExecuteReader();
                    while (DataReader.Read())
                    {
                        if (!DataReader[0].Equals(DBNull.Value))
                        {
                            CheckListBox.Items.Add(DataReader[0]);
                        }
                    }
                    DataReader.Close();

                }
                catch (Exception Ex)
                {
                    StreamWriter sw = new StreamWriter(MyPath + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n" + Ex.ToString());
                    sw.Close();
                 

                }
                finally
                {

                    sSqlParameter.Clear();
                    sSqlCommand.Connection.Close();
                }
            }
        }
        public void GetDBtoCklbx(string SqlCommand, string ReplaceConStr, ref CheckedListBox CheckListBox)
        {
            CheckListBox.Items.Clear();
            SqlCommand sSqlCommand = new SqlCommand();
            sSqlCommand.CommandTimeout = 300; //設置SqlCommand TimeOut 300
            sSqlCommand.Connection = new SqlConnection(ReplaceConStr);

            lock (sSqlCommand.Connection)
            {
                try
                {
                    sSqlCommand.Connection.Open();

                    sSqlCommand.CommandText = SqlCommand;
                    sSqlCommand.CommandType = CommandType.Text;
                    sSqlCommand.Parameters.Clear();
                    if (sSqlParameter.Count > 0)
                    {
                        foreach (SqlParameter MySqlParameter in sSqlParameter)
                        {
                            sSqlCommand.Parameters.Add(MySqlParameter);
                        }
                    }
                    SqlDataReader DataReader = sSqlCommand.ExecuteReader();
                    while (DataReader.Read())
                    {
                        if (!DataReader[0].Equals(DBNull.Value))
                        {
                            CheckListBox.Items.Add(DataReader[0]);
                        }
                    }
                    DataReader.Close();

                }
                catch (Exception Ex)
                {
                    StreamWriter sw = new StreamWriter(MyPath + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n" + Ex.ToString());
                    sw.Close();
                

                }
                finally
                {

                    sSqlParameter.Clear();
                    sSqlCommand.Connection.Close();
                }
            }
        }

        /// 執行SQL指令並將值回傳字串陣列
        /// <summary> 執行SQL指令並將值回傳字串陣列</summary>
        /// <param name="SqlCommand">要執行的SQL指令</param>
        /// <returns>查詢後字串陣列</returns>
        public string[,] GetDBtoDR(string SqlCommand)
        {
            SqlCommand sSqlCommand = new SqlCommand();
            sSqlCommand.CommandTimeout = 300; //設置SqlCommand TimeOut 300
            sSqlCommand.Connection = new SqlConnection(ConnectionString);

            lock (sSqlCommand.Connection)
            {
                try
                {
                    sSqlCommand.Connection.Open();

                    sSqlCommand.CommandText = SqlCommand;
                    sSqlCommand.CommandType = CommandType.Text;
                    sSqlCommand.Parameters.Clear();
                    if (sSqlParameter.Count > 0)
                    {
                        foreach (SqlParameter MySqlParameter in sSqlParameter)
                        {
                            sSqlCommand.Parameters.Add(MySqlParameter);
                        }
                    }
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(sSqlCommand);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0) //如果包含多個DataRow
                    {
                        string[,] sDr = new string[dt.Rows.Count, dt.Columns.Count];

                        for (int nRow = 0; nRow < dt.Rows.Count; nRow++)
                        {
                            for (int nCol = 0; nCol < dt.Columns.Count; nCol++)
                            {
                                sDr[nRow, nCol] = Convert.ToString(dt.Rows[nRow][nCol]);
                            }
                        }
                        da.Dispose();

                        return sDr;
                    }
                    else
                    {
                        da.Dispose();
                        return null;
                    }
                }
                catch (Exception Ex)
                {
                    StreamWriter sw = new StreamWriter(MyPath + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n" + Ex.ToString());
                    sw.Close();
              
                    return null;

                }
                finally
                {

                    sSqlParameter.Clear();
                    sSqlCommand.Connection.Close();
                }
            }
        }
        public string[,] GetDBtoDR(string SqlCommand, string ReplaceConStr)
        {
            SqlCommand sSqlCommand = new SqlCommand();
            sSqlCommand.CommandTimeout = 300; //設置SqlCommand TimeOut 300
            sSqlCommand.Connection = new SqlConnection(ReplaceConStr);

            lock (sSqlCommand.Connection)
            {
                try
                {
                    sSqlCommand.Connection.Open();

                    sSqlCommand.CommandText = SqlCommand;
                    sSqlCommand.CommandType = CommandType.Text;
                    sSqlCommand.Parameters.Clear();
                    if (sSqlParameter.Count > 0)
                    {
                        foreach (SqlParameter MySqlParameter in sSqlParameter)
                        {
                            sSqlCommand.Parameters.Add(MySqlParameter);
                        }
                    }
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(sSqlCommand);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0) //如果包含多個DataRow
                    {
                        string[,] sDr = new string[dt.Rows.Count, dt.Columns.Count];

                        for (int nRow = 0; nRow < dt.Rows.Count; nRow++)
                        {
                            for (int nCol = 0; nCol < dt.Columns.Count; nCol++)
                            {
                                sDr[nRow, nCol] = Convert.ToString(dt.Rows[nRow][nCol]);
                            }
                        }
                        da.Dispose();

                        return sDr;
                    }
                    else
                    {
                        da.Dispose();
                        return null;
                    }
                }
                catch (Exception Ex)
                {
                    StreamWriter sw = new StreamWriter(MyPath + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n" + Ex.ToString());
                    sw.Close();
               

                    return null;
                }
                finally
                {

                    sSqlParameter.Clear();
                    sSqlCommand.Connection.Close();
                }
            }
        }

        /// 使用Gmail發E-Mail
        /// <summary>使用Gmail發E-Mail</summary>
        /// <param name="ServerAddress">主機帳號:XXXXX@gmail.com</param>
        /// <param name="ServerPassword">主機密碼:*******</param>
        /// <param name="FromUser">發信者名稱</param>
        /// <param name="ToUser">收信人名稱</param>
        /// <param name="ToEmail">收信人Mail:XXXXX@xxx.com</param>
        /// <param name="Subject">主旨</param>
        /// <param name="Content">內容</param>
        public void SendEmail(string ServerAddress, string ServerPassword, string FromUser, string ToUser, string ToEmail, string Subject, string Content)
        {
            //建立 SmtpClient 物件 並設定 Gmail的smtp主機及Port 
            System.Net.Mail.SmtpClient MySmtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);

            //設定你的帳號密碼
            MySmtp.Credentials = new System.Net.NetworkCredential(ServerAddress, ServerPassword);

            //Gmial 的 smtp 使用 SSL
            MySmtp.EnableSsl = true;
            //發送Email
            MySmtp.Send(FromUser + "<" + ServerAddress + ">", ToUser + "<" + ToEmail + ">", Subject, Content);
        }

        /// 匯出Datatable to CSV 可指定目的地位置
        /// <summary>匯出Datatable to CSV.</summary>
        /// <param name="dt">Datatable</param>
        /// <param name="FileName">檔案名稱</param>
        /// <param name="ColumnName">檔案裡標題內容.</param>
        /// <param name="HasColumnName">如果設定 <c>true</c> 產生標題.</param>
        /// <param name="DestPath">指定目的地路徑</param>x
        public void ExportDataTableToCsv(DataTable dt, string FileName, string[] ColumnName, bool HasColumnName, string DestPath)
        {
            string strValue = string.Empty;

            if (HasColumnName == true)
                strValue = string.Join(",", ColumnName);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][j].ToString()))
                    {
                        if (j > 0)
                            strValue = strValue + "," + dt.Rows[i][j].ToString();
                        else
                        {
                            if (string.IsNullOrEmpty(strValue))
                                strValue = dt.Rows[i][j].ToString();
                            else
                                strValue = strValue + Environment.NewLine + dt.Rows[i][j].ToString();
                        }
                    }
                    else
                    {
                        if (j > 0)
                            strValue = strValue + ",";
                        else
                            strValue = strValue + Environment.NewLine;
                    }
                }

            }
            //存成檔案
            string strFile = FileName;
            if (!string.IsNullOrEmpty(strValue))
            {
                File.WriteAllText(DestPath + strFile, strValue, Encoding.Default);
            }
        }

        /// 匯出Datatable to CSV 預存在執行檔下
        /// <summary>匯出Datatable to CSV.</summary>
        /// <param name="dt">Datatable</param>
        /// <param name="FileName">檔案名稱</param>     
        public void ExportDataTableToCsv(DataTable dt, string FileName)
        {
            string strValue = string.Empty;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][j].ToString()))
                    {
                        if (j > 0)
                            strValue = strValue + "," + dt.Rows[i][j].ToString();
                        else
                        {
                            if (string.IsNullOrEmpty(strValue))
                                strValue = dt.Rows[i][j].ToString();
                            else
                                strValue = strValue + Environment.NewLine + dt.Rows[i][j].ToString();
                        }
                    }
                    else
                    {
                        if (j > 0)
                            strValue = strValue + ",";
                        else
                            strValue = strValue + Environment.NewLine;
                    }
                }

            }
            //存成檔案
            string strFile = FileName;
            if (!string.IsNullOrEmpty(strValue))
            {
                File.WriteAllText(strFile, strValue, Encoding.Default);
            }
        }

        /// 台積電使用寄信
        /// <summary>台積電使用寄信</summary>
        /// <param name="FromMail"></param>
        /// <param name="sName"></param>
        /// <param name="ToMail"></param>
        /// <param name="sSubject"></param>
        /// <param name="sMessage"></param>
        public void SendMail(string FromMail, string sName, string ToMail, string sSubject, string sMessage)
        {

            MailMessage u_Mail = new MailMessage();

            u_Mail.From = new MailAddress(FromMail, sName);
            u_Mail.To.Add(ToMail);
            u_Mail.Subject = sSubject;
            u_Mail.Body = sMessage;
            u_Mail.IsBodyHtml = true;

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();

            client.Credentials = new System.Net.NetworkCredential();

            client.Host = ""; //Setting Mail Server

            try
            {
                client.Send(u_Mail);
            }
            catch (Exception Ex)
            {
                StreamWriter sw = new StreamWriter(MyPath + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "\r\n" + Ex.ToString());
                sw.Close();
            

            }

        }

        /// 讀取 XML for Connection String
        /// <summary>讀取資料庫連線字串</summary>
        /// <param name="FromMail"></param>
        /// <param name="sName"></param>
        /// <param name="ToMail"></param>
        /// <param name="sSubject"></param>
        /// <param name="sMessage"></param>
        private void LoadXml()
        {
            try
            {
                xmlDoc = XDocument.Load(XML_Path);
                XElement rootNode = xmlDoc.Element("Setting");

                foreach (XElement node in rootNode.Elements("MsSQL"))
                {
                    ConnectionString = @"server=" + node.Element("ip").Value + ";"
                                        + "database=" + node.Element("database").Value + ";"
                                        + "user id=" + node.Element("account").Value + ";"
                                        + "password=" + node.Element("password").Value + ";"
                                        + "pooling=false";

                }
            }
            catch
            {
                MessageBox.Show("Read DB Setting Error", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// 寫入 XML 檔 for Connection String
        /// <summary>寫入資料庫連線字串</summary>
        /// <param name="FromMail"></param>
        /// <param name="sName"></param>
        /// <param name="ToMail"></param>
        /// <param name="sSubject"></param>
        /// <param name="sMessage"></param>
        public void WriteXml(string IP, string DATABASE, string ACCOUNT, string PASSWORD)
        {
            try
            {
                XDocument xdoc = new XDocument(
                      new XElement("Setting",
                              new XElement("MsSQL", null,
                                  new XElement("ip", IP),
                                  new XElement("database", DATABASE),
                                  new XElement("account", ACCOUNT),
                                  new XElement("password", PASSWORD)
                                  )
                             )
                      );
                xdoc.Save(XML_Path);
                MessageBox.Show("Save Success", "Congratulation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch
            {
                MessageBox.Show("Save Error", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

    }
}
