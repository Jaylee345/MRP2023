using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


using System.Configuration;
using System.Data.OracleClient;
using System.Net;
using System.Threading;

namespace MRPSystem
{
    public class DAOMSSQL
    {
        public static string Comp = ConfigurationManager.ConnectionStrings["Comp"].ConnectionString;
        

        
        public static int GetNO(string stBar, string rPk, string Con)
        {
            int iCount = 1;
            using (SqlConnection conn = new SqlConnection(Con))
            {
                conn.Open();

                string sql = "";
                sql += "SELECT top 1 max(sn) as ct FROM WeighRecord with(nolock)  WHERE Barcode LIKE '" + stBar + "%' and PackageCode='" + rPk + "' ";
                SqlCommand command = new SqlCommand(sql, conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        //var kk = (int)reader[0];
                        iCount = reader[0] == DBNull.Value ? 1 : (int)reader[0] + 1;

                    }
                }
            }
            return iCount;
        }

        public static List<T> GetQueryList<T>(string sqlStr, string connStr)
        {

            List<T> lists = new List<T>();

            string tempName = string.Empty, val = "";
            //Common.Writetxt("Start login");

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlConnection con = new SqlConnection(connStr);
                    SqlCommand cmd = new SqlCommand(sqlStr, con);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //string a1 = reader[0].ToString();
                            //string a2 = reader[1].ToString();
                            //string a3 = reader[2].ToString();
                            //string a4 = reader[3].ToString();
                            //string a5 = reader[4].ToString();
                            T t = (T)Activator.CreateInstance(typeof(T));
                            PropertyInfo[] propertys = t.GetType().GetProperties();
                            foreach (PropertyInfo pi in propertys)
                            {
                                tempName = pi.Name;
                                if (readerExists(reader, tempName))
                                {

                                    if (!pi.CanWrite)
                                    {
                                        continue;
                                    }
                                    val = reader[tempName].ToString();
                                    var value = reader[tempName];
                                    if (value != DBNull.Value)
                                    {
                                        pi.SetValue(t, value, null);
                                    }
                                }
                            }
                            lists.Add(t);
                        }
                    }

                    return lists;
                }
            }
            catch (Exception ee)
            {
                Common.Writetxt(sqlStr + connStr + tempName + val, ee);
                return null;
            }
        }

        public static int InsertVal(string sql, string connStr)
        {
            Int32 newProdID = 0;
            //string sql =
            //    "INSERT INTO Production.ProductCategory (Name) VALUES (@Name); "
            //    + "SELECT CAST(scope_identity() AS int)";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                //cmd.Parameters.Add("@Name", SqlDbType.VarChar);
                //cmd.Parameters["@name"].Value = newName;
                try
                {
                    conn.Open();
                    newProdID = (Int32)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return (int)newProdID;
        }
        public static T GetQuery<T>(string sqlStr, string connStr)
        {

            T t = (T)Activator.CreateInstance(typeof(T));

            string tempName = string.Empty, val = "";
            //Common.Writetxt("Start login");

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand(sqlStr, conn);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //T t = (T)Activator.CreateInstance(typeof(T));
                            PropertyInfo[] propertys = t.GetType().GetProperties();
                            foreach (PropertyInfo pi in propertys)
                            {
                                tempName = pi.Name;
                                if (readerExists(reader, tempName))
                                {

                                    if (!pi.CanWrite)
                                    {
                                        continue;
                                    }
                                    val = reader[tempName].ToString();
                                    var value = reader[tempName];
                                    if (value != DBNull.Value)
                                    {
                                        pi.SetValue(t, value, null);
                                    }
                                }
                            }

                        }
                    }

                    return t;
                }
            }
            catch (Exception ee)
            {
                Common.Writetxt(sqlStr + connStr + tempName + val, ee);
                return t;
            }
        }

        public static List<T> GetQueryListwithXml<T>(string sqlStr, string connStr, string xml)
        {

            List<T> lists = new List<T>();

            string tempName = string.Empty, val = "";
            //Common.Writetxt("Start login");

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand(sqlStr, conn);
                    cmd.Parameters.AddWithValue("@Xmlstr", xml);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            T t = (T)Activator.CreateInstance(typeof(T));
                            PropertyInfo[] propertys = t.GetType().GetProperties();
                            foreach (PropertyInfo pi in propertys)
                            {
                                tempName = pi.Name;
                                if (readerExists(reader, tempName))
                                {

                                    if (!pi.CanWrite)
                                    {
                                        continue;
                                    }
                                    val = reader[tempName].ToString();
                                    var value = reader[tempName];
                                    if (value != DBNull.Value)
                                    {
                                        pi.SetValue(t, value, null);
                                    }
                                }
                            }
                            lists.Add(t);
                        }
                    }

                    return lists;
                }
            }
            catch (Exception ee)
            {
                Common.Writetxt(sqlStr + connStr + tempName + val, ee);
                return null;
            }
        }

        public static bool readerExists(SqlDataReader dr, string columnName)
        {

            dr.GetSchemaTable().DefaultView.RowFilter = "ColumnName= '" + columnName + "'";
            return (dr.GetSchemaTable().DefaultView.Count > 0);
        }
      
        public static string SQLReader(string ssql, string connstr)
        {
            string val = "";
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                SqlCommand cmd = new SqlCommand(ssql, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        val = reader[0].ToString();
                }
            }

            return val;
        }

        
        public static void InsertLogs(string Remark, string logText)
        {

            string sql = string.Format("Insert into LogRecord(Remark,logText) values('{0}','{1}')", Remark.Replace("'", ""), logText.Replace("'", ""));

            SqlCommand transCommand = TransCommand.GetTranCommand(Comp);
            try
            {

                transCommand.CommandText = sql;
                transCommand.ExecuteNonQuery();
                //Common.Writetxt(string.Format(" SQL :{0}\r\n   ", sql));
                TransCommand.SqlCommit(transCommand);
            }

            catch (Exception ex)
            {
                Common.Writetxt(Comp, ex);
                TransCommand.SqlRollback(transCommand);
            }

            // return (dr.GetSchemaTable().DefaultView.Count > 0);
        }
        public static string Ins_ITFILE_M(string sql, string connStr)
        {
            int modified = 0;

            if (!string.IsNullOrEmpty(sql))
            {
                //insert ITFILE_M get id.
                //string sql = "INSERT INTO dbo.ITFILE_M(FACTORY_NO, USERNO, UP_DATE, ITF01) VALUES('G', 'king001', Getdate(), 'API-21021134') " +
                //        "SELECT @@IDENTITY as id";
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        //cmd.Parameters.AddWithValue("@na", Mem_NA);
                        //cmd.Parameters.AddWithValue("@occ", Mem_Occ);
                        conn.Open();

                        modified = Convert.ToInt32(cmd.ExecuteScalar());

                        return modified.ToString();
                    }
                }
            }
            return "";
        }
        public static string UpdateCommand(List<string> sqlCollect, string connStr)
        {
            string errorsql = "";
            SqlCommand transCommandS = null;
            int count = 0;
            try
            {
                transCommandS = TransCommand.GetTranCommand(connStr);
                foreach (string insertSQL in sqlCollect)
                {
                    errorsql = insertSQL;
                    transCommandS.CommandText = insertSQL;
                    transCommandS.ExecuteNonQuery();
                    count++;
                    //Common.Writetxt(string.Format(" SQL :{0}\r\n   ", insertSQL));
                }
                if (count > 0)
                    TransCommand.SqlCommit(transCommandS);
            }
            catch (Exception ex)
            {
                Common.Writetxt(connStr, ex);
                TransCommand.SqlRollback(transCommandS);
                return "F";
            }

            return "S";
            // return (dr.GetSchemaTable().DefaultView.Count > 0);
        }
        public static string UpdateCommand(string sqlstr, string connStr)
        {
            string errorsql = "";
            SqlCommand transCommandS = null;
            int count = 0;
            try
            {
                transCommandS = TransCommand.GetTranCommand(connStr);
                //foreach (string insertSQL in sqlCollect)
                //{
                errorsql = sqlstr;
                transCommandS.CommandText = sqlstr;
                transCommandS.ExecuteNonQuery();
                count++;
                //Common.Writetxt(string.Format(" SQL :{0}\r\n   ", insertSQL));
                //}
                if (count > 0)
                    TransCommand.SqlCommit(transCommandS);
            }
            catch (Exception ex)
            {
                Common.Writetxt(connStr, ex);
                TransCommand.SqlRollback(transCommandS);
                return "F";
            }

            return "S";
            // return (dr.GetSchemaTable().DefaultView.Count > 0);
        }


        public static string UpdateCommand(List<string> sqlCollect)
        {
            //connectStr = ConfigurationManager.ConnectionStrings["WT"].ConnectionString;
            string errorsql = "";
            int count = 0;
            SqlCommand transCommandS = null;
            try
            {
                transCommandS = TransCommand.GetTranCommand(Comp);
                foreach (string insertSQL in sqlCollect)
                {
                    errorsql = insertSQL;
                    transCommandS.CommandText = insertSQL;
                    transCommandS.ExecuteNonQuery();
                    count++;
                    //Common.Writetxt(string.Format(" SQL :{0}\r\n   ", insertSQL));
                }
                if (count > 0)
                    TransCommand.SqlCommit(transCommandS);

            }

            catch (Exception ex)
            {
                Common.Writetxt(Comp, ex);
                TransCommand.SqlRollback(transCommandS);
                return "F";
            }

            return "S";
            // return (dr.GetSchemaTable().DefaultView.Count > 0);
        }

        public static List<T> Ora_QueryList<T>(string sqlStr, string connStr)
        {
            //string connStr = ConfigurationManager.ConnectionStrings["ConnString"].ToString(); // ConfigurationSettings.AppSettings["ConnString"];

            List<T> lists = new List<T>();

            string tempName = string.Empty;
            string info = string.Empty;
            try
            {
                using (OracleConnection conn = new OracleConnection(connStr))
                {
                    //OracleConnection con = new OracleConnection(connStr);
                    OracleCommand cmd = new OracleCommand(sqlStr, conn);
                    conn.Open();
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            info = reader[0].ToString();
                            T t = (T)Activator.CreateInstance(typeof(T));
                            PropertyInfo[] propertys = t.GetType().GetProperties();
                            foreach (PropertyInfo pi in propertys)
                            {
                                tempName = pi.Name;
                                if (Ora_readerExists(reader, tempName))
                                {

                                    if (!pi.CanWrite)
                                    {
                                        continue;
                                    }
                                    var value = reader[tempName];
                                    if (value != DBNull.Value)
                                    {
                                        pi.SetValue(t, value, null);
                                    }
                                }
                            }
                            lists.Add(t);
                        }
                    }
                    return lists;
                }
            }
            catch (Exception ex)
            {
                Common.Writetxt(tempName, ex);
                return null;
            }
        }
        public static bool Ora_readerExists(OracleDataReader dr, string columnName)
        {
            dr.GetSchemaTable().DefaultView.RowFilter = "ColumnName= '" + columnName + "'";
            return (dr.GetSchemaTable().DefaultView.Count > 0);
        }

        
    }
}
