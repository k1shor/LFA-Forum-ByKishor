using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFA.ConsoleForum.Ado
{
    public class SqlHelper : IDisposable
    {
        private String _connString;
        private SqlConnection _conn;
        protected bool _disposed = false;

        protected void Connect()
        {
            _conn = new SqlConnection(_connString);
            _conn.Open();
        }

        public SqlCommand CreateCommand(String sqlQuerry, CommandType type, params object[] args)
        {

            SqlCommand cmd=new SqlCommand(); 
            cmd.CommandText = sqlQuerry;
            cmd.Connection = _conn;
            cmd.CommandType = type;
                
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] is string && i < (args.Length - 1))
                    {
                        SqlParameter param = new SqlParameter((string)args[i].ToString(), args[++i].ToString());
                        cmd.Parameters.Add(param);
                    }
                    else if (args[i] is SqlParameter)
                    {
                        cmd.Parameters.Add(args[i]);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid number of type of arguments supplied use @value value");
                    }
                }
                return cmd;
        }
        
        public SqlHelper()
        {
            _connString = ConfigurationManager.ConnectionStrings["LFAFORUMConnectionString"].ToString();
            Connect();
        }

        public SqlDataReader ExecuteReader(String sqlQuerry, params object[] args)
        {
            using (SqlCommand cmd = CreateCommand(sqlQuerry, CommandType.Text, args))
            {
                return cmd.ExecuteReader();
            }
        }

        public SqlDataReader ExecReaderProcedure(String sqlQuerry, params object[] args)
        {
            using (SqlCommand cmd = CreateCommand(sqlQuerry, CommandType.StoredProcedure, args))
            {
                return cmd.ExecuteReader();
            }
        }

        public int ExecuteNonQuery(String sqlQuerry, params object[] args)
        {
            using (SqlCommand cmd= CreateCommand(sqlQuerry, CommandType.Text, args))
            {
                return cmd.ExecuteNonQuery();
            }
        }

        public int ExecNonQueryProcedure(String sqlQuerry, params object[] args)
        {
            using (SqlCommand cmd= CreateCommand(sqlQuerry, CommandType.StoredProcedure, args))
            {
                return cmd.ExecuteNonQuery();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_conn != null)
                    {
                        _conn.Dispose();
                        _conn = null;
                    }
                }
                _disposed = true;
            }
        }
    }
}
