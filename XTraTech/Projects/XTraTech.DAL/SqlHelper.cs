using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace XTraTech.DAL
{
    public class SqlHelper
    {
        private static string connectionString;
        public static string ConnectionString
        {
            get
            {
                if (SqlHelper.connectionString == null)
                {
                    SqlHelper.connectionString = ConfigurationManager.ConnectionStrings["XTraTechConnection"].ConnectionString;
                }
                return SqlHelper.connectionString;
            }
        }
        private static DataSet Fill(string query, SqlParameter[] paramList, CommandType cmdType, string connString)
        {
            DataSet dataSet = new DataSet();
            SqlConnection connection = SqlHelper.GetConnection(connString);
            try
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandTimeout = 60;
                command.CommandType = cmdType;
                command.Parameters.AddRange(paramList);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                int rowsAdded = adapter.Fill(dataSet);
            }
            catch (SqlException sqEx)
            {
                throw sqEx;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return dataSet;
        }
        public static DataTable FillDataTableSP(string sproc)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = SqlHelper.GetConnection(SqlHelper.ConnectionString);
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(new SqlCommand(sproc, connection)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 15
                });
                int rowsAdded = adapter.Fill(dataTable);
            }
            catch (SqlException sqEx)
            {
                throw sqEx;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return dataTable;
        }
        public static DataTable FillDataTableSP(string sproc, XtraTechDBType mystiflyDBType)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = SqlHelper.GetConnection(mystiflyDBType);
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(new SqlCommand(sproc, connection)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 15
                });
                int rowsAdded = adapter.Fill(dataTable);
            }
            catch (SqlException sqEx)
            {
                throw sqEx;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return dataTable;
        }
        public static DataTable FillDataTableSP(string sproc, SqlParameter[] paramList)
        {
            return SqlHelper.FillDataTable(sproc, paramList, CommandType.StoredProcedure, SqlHelper.connectionString);
        }
        public static DataTable FillDataTableSP(string sproc, SqlParameter[] paramList, XtraTechDBType mystiflyDBType)
        {
            return SqlHelper.FillDataTable(sproc, paramList, CommandType.StoredProcedure, mystiflyDBType);
        }
        private static DataTable FillDataTable(string query, SqlParameter[] paramList, CommandType cmdType, string connString)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = SqlHelper.GetConnection();
            try
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = cmdType;
                command.CommandTimeout = 60;
                command.Parameters.AddRange(paramList);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                int rowsAdded = adapter.Fill(dataTable);
            }
            catch (SqlException sqEx)
            {
                throw sqEx;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return dataTable;
        }
        private static DataTable FillDataTable(string query, SqlParameter[] paramList, CommandType cmdType, XtraTechDBType mystiflyDBType)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = SqlHelper.GetConnection(mystiflyDBType);
            try
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = cmdType;
                command.CommandTimeout = 60;
                command.Parameters.AddRange(paramList);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                int rowsAdded = adapter.Fill(dataTable);
            }
            catch (SqlException sqEx)
            {
                throw sqEx;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return dataTable;
        }
        public static SqlDataReader ExecuteReader(string query, SqlParameter[] paramList, SqlConnection connection)
        {
            return SqlHelper.ExecuteReader(query, paramList, connection, CommandType.Text);
        }
        public static SqlDataReader ExecuteReaderSP(string sproc, SqlParameter[] paramList, SqlConnection connection)
        {
            return SqlHelper.ExecuteReader(sproc, paramList, connection, CommandType.StoredProcedure);
        }
        public static SqlDataReader ExecuteReaderSP(string sproc, SqlConnection connection)
        {
            return SqlHelper.ExecuteReader(sproc, connection, CommandType.StoredProcedure);
        }
        private static SqlDataReader ExecuteReader(string sproc, SqlConnection connection, CommandType cmdType)
        {
            SqlDataReader result;
            try
            {
                SqlCommand command = new SqlCommand(sproc, connection);
                command.CommandType = cmdType;
                command.CommandTimeout = 60;
                string detail = string.Empty;
                SqlDataReader dataReader = command.ExecuteReader();
                result = dataReader;
            }
            catch (SqlException sqEx)
            {
                throw sqEx;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return result;
        }
        private static SqlDataReader ExecuteReader(string sproc, SqlParameter[] paramList, SqlConnection connection, CommandType cmdType)
        {
            SqlDataReader result;
            try
            {
                SqlCommand command = new SqlCommand(sproc, connection);
                command.CommandType = cmdType;
                command.CommandTimeout = 60;
                command.Parameters.AddRange(paramList);
                string detail = string.Empty;
                SqlDataReader dataReader = command.ExecuteReader();
                result = dataReader;
            }
            catch (SqlException sqEx)
            {
                throw sqEx;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
            return result;
        }
        private static int ExecuteNonQuery(string sproc, SqlParameter[] paramList, CommandType cmdType, string connString)
        {
            SqlConnection connection = SqlHelper.GetConnection(connString);
            int result;
            try
            {
                SqlCommand command = new SqlCommand(sproc, connection);
                command.CommandType = cmdType;
                command.CommandTimeout = 60;
                command.Parameters.AddRange(paramList);
                string detail = string.Empty;
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                result = rowsAffected;
            }
            catch (SqlException sqEx)
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                    connection.Dispose();
                }
                throw sqEx;
            }
            return result;
        }
        public static int ExecuteNonQuery(string query, SqlParameter[] paramList)
        {
            return SqlHelper.ExecuteNonQuery(query, paramList, CommandType.StoredProcedure);
        }
        public static int ExecuteNonQuery(string query, SqlParameter[] paramList, XtraTechDBType mystiflyDBType)
        {
            return SqlHelper.ExecuteNonQuery(query, paramList, CommandType.StoredProcedure, mystiflyDBType);
        }
        private static int ExecuteNonQuery(string sproc, SqlParameter[] paramList, CommandType cmdType)
        {
            SqlConnection connection = SqlHelper.GetConnection();
            int result;
            try
            {
                SqlCommand command = new SqlCommand(sproc, connection);
                command.CommandType = cmdType;
                command.CommandTimeout = 60;
                command.Parameters.AddRange(paramList);
                string detail = string.Empty;
                DateTime timeBeforeQuery = DateTime.UtcNow;
                int rowsAffected = command.ExecuteNonQuery();
                DateTime timeAfterQuery = DateTime.UtcNow;
                result = rowsAffected;
            }
            catch (SqlException sqEx)
            {
                throw sqEx;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return result;
        }
        private static int ExecuteNonQuery(string sproc, SqlParameter[] paramList, CommandType cmdType, XtraTechDBType mystiflyDBType)
        {
            SqlConnection connection = SqlHelper.GetConnection(mystiflyDBType);
            int result;
            try
            {
                SqlCommand command = new SqlCommand(sproc, connection);
                command.CommandType = cmdType;
                command.CommandTimeout = 60;
                command.Parameters.AddRange(paramList);
                string detail = string.Empty;
                DateTime timeBeforeQuery = DateTime.UtcNow;
                int rowsAffected = command.ExecuteNonQuery();
                DateTime timeAfterQuery = DateTime.UtcNow;
                result = rowsAffected;
            }
            catch (SqlException sqEx)
            {
                throw sqEx;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return result;
        }
        private static SqlConnection GetConnection(string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
        public static SqlConnection GetConnection()
        {
            SqlConnection connection;
            try
            {
                connection = SqlHelper.GetConnection(SqlHelper.ConnectionString);
            }
            catch (SqlException sqEx)
            {
                throw sqEx;
            }
            return connection;
        }
        public static SqlConnection GetConnection(XtraTechDBType mystiflyDBType)
        {
            SqlConnection result;
            try
            {
                SqlConnection sqlConnection = null;
                if (mystiflyDBType == XtraTechDBType.XtraTech)
                {
                    sqlConnection = SqlHelper.GetConnection(SqlHelper.ConnectionString);
                }
                result = sqlConnection;
            }
            catch (SqlException sqEx)
            {
                throw sqEx;
            }
            return result;
        }
        private static object ExecuteScalar(string sproc, SqlParameter[] paramList, CommandType cmdType, string connString)
        {
            SqlConnection connection = SqlHelper.GetConnection(connString);
            object result;
            try
            {
                SqlCommand command = new SqlCommand(sproc, connection);
                command.CommandType = cmdType;
                command.CommandTimeout = 60;
                command.Parameters.AddRange(paramList);
                string detail = string.Empty;
                object obj = command.ExecuteScalar();
                connection.Close();
                result = obj;
            }
            catch (SqlException sqEx)
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                    connection.Dispose();
                }
                throw sqEx;
            }
            return result;
        }
        public static object ExecuteScalar(string query, SqlParameter[] paramList)
        {
            return SqlHelper.ExecuteScalar(query, paramList, CommandType.StoredProcedure);
        }
        public static object ExecuteScalar(string query, SqlParameter[] paramList, XtraTechDBType mystiflyDBType)
        {
            return SqlHelper.ExecuteScalar(query, paramList, CommandType.StoredProcedure, mystiflyDBType);
        }
        public static object ExecuteScalar(string query)
        {
            return SqlHelper.ExecuteScalar(query, CommandType.StoredProcedure);
        }
        private static object ExecuteScalar(string sproc, CommandType cmdType)
        {
            SqlConnection connection = SqlHelper.GetConnection();
            object result;
            try
            {
                SqlCommand command = new SqlCommand(sproc, connection);
                command.CommandType = cmdType;
                command.CommandTimeout = 60;
                string detail = string.Empty;
                DateTime timeBeforeQuery = DateTime.UtcNow;
                object obj = command.ExecuteScalar();
                DateTime timeAfterQuery = DateTime.UtcNow;
                result = obj;
            }
            catch (SqlException sqEx)
            {
                throw sqEx;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return result;
        }
        private static object ExecuteScalar(string sproc, SqlParameter[] paramList, CommandType cmdType)
        {
            SqlConnection connection = SqlHelper.GetConnection();
            object result;
            try
            {
                SqlCommand command = new SqlCommand(sproc, connection);
                command.CommandType = cmdType;
                command.CommandTimeout = 60;
                command.Parameters.AddRange(paramList);
                string detail = string.Empty;
                DateTime timeBeforeQuery = DateTime.UtcNow;
                object obj = command.ExecuteScalar();
                DateTime timeAfterQuery = DateTime.UtcNow;
                result = obj;
            }
            catch (SqlException sqEx)
            {
                throw sqEx;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return result;
        }
        private static object ExecuteScalar(string sproc, SqlParameter[] paramList, CommandType cmdType, XtraTechDBType mystiflyDBType)
        {
            SqlConnection connection = SqlHelper.GetConnection(mystiflyDBType);
            object result;
            try
            {
                SqlCommand command = new SqlCommand(sproc, connection);
                command.CommandType = cmdType;
                command.CommandTimeout = 60;
                command.Parameters.AddRange(paramList);
                string detail = string.Empty;
                DateTime timeBeforeQuery = DateTime.UtcNow;
                object obj = command.ExecuteScalar();
                DateTime timeAfterQuery = DateTime.UtcNow;
                result = obj;
            }
            catch (SqlException sqEx)
            {
                throw sqEx;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return result;
        }
    }
    public enum XtraTechDBType
    {
        XtraTech = 1
    }
}
