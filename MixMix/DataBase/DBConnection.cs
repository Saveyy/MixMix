using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public static class DBConnection
    {
        public static string CONNECTION_STRING
        {
            ///
            // Der er en eller anden bug med at vi ikke kan få Connection strengen ud fra app.config
            ///
            get
            {
                //string conn = @"Data Source = DESKTOP-LKPVQ3R; Initial Catalog = MixMix; Integrated Security = True;";
                string conn = ConfigurationManager.ConnectionStrings["MixMixConnectionString"].ToString();
                SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder(conn);
                return stringBuilder.ToString();
            }
        }

        public static SqlConnection GetSqlConnection()
        {
            SqlConnection connection = new SqlConnection(CONNECTION_STRING);
            connection.Open();
            return connection;
        }
    }
}
