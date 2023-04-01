using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactExplorer
{
    public class DataBase : IDisposable
    {
        private string _dataSource = @"Data Source=DESKTOP-AVGELME\STP;Initial Catalog=Contacts;Integrated Security=True";
        public SqlConnection Connection { get; private set; }
        public bool IsConnected { get; private set; }

        public DataBase()
        {
            Connection = new SqlConnection(_dataSource);
            OpenConnection();
        }

        private void OpenConnection()
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
                IsConnected = true;
            }
        }
        private void CloseConnection()
        {
            if (Connection.State == ConnectionState.Open)
            {
                Connection.Close();
                IsConnected = false;
            }
        }

        public DataTable ExecuteSql(string sql)
        {
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand(sql, Connection);
            var reader = command.ExecuteReader();
            dt.Load(reader);
            return dt;
        }

        public void ExecuteNonQuery(string sql)
        {
            SqlCommand command = new SqlCommand(sql, Connection);
            command.ExecuteNonQuery();
        }

        public void Dispose()
        {
            CloseConnection();
        }
    }
}
