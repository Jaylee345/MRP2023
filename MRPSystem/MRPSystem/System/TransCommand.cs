using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MRPSystem
{
    public class TransCommand
    {
        public static bool SqlCommit(SqlCommand command)
        {
            bool result = false;
            try
            {
                command.Transaction.Commit();
                result = true;
            }
            catch
            {
                result = false;
            }
            finally
            {
                command.Connection.Close();
                command.Connection = null;
                command = null;
            }
            return result;
        }

        public static bool SqlRollback(SqlCommand command)
        {
            bool result = false;
            try
            {
                command.Transaction.Rollback();
                result = true;
            }
            catch
            {
                result = false;
            }
            finally
            {
                command.Connection.Close();
                command.Connection = null;
                command = null;
            }
            return result;
        }


        public static SqlCommand GetTranCommand(SqlConnection sqlConnection)
        {

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            int commandTimeout = 1800;
            object obj = ConfigurationManager.AppSettings["CommandTimeout"];
            if (obj != null)
            {
                commandTimeout = (int)Convert.ToInt16(obj);
            }
            sqlCommand.CommandTimeout = commandTimeout;
            sqlCommand.Transaction = sqlConnection.BeginTransaction();
            return sqlCommand;
        }

        //
        public static SqlCommand GetTranCommand(string connString)
        {
            SqlConnection sqlConnection = new SqlConnection(connString);

            sqlConnection.Open();

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            int commandTimeout = 1800;
            object obj = ConfigurationManager.AppSettings["CommandTimeout"];
            if (obj != null)
            {
                commandTimeout = (int)Convert.ToInt16(obj);
            }
            sqlCommand.CommandTimeout = commandTimeout;
            sqlCommand.Transaction = sqlConnection.BeginTransaction();
            return sqlCommand;
        }

    }
}
