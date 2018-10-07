using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public static class Extension
    {
        public static string GetString(this SqlDataReader t, string input)
        {
            return t.GetString(t.GetOrdinal(input));
        }
        public static int GetInt32(this SqlDataReader t, string input)
        {
            return t.GetInt32(t.GetOrdinal(input));
        }
        public static bool GetBoolean(this SqlDataReader t, string input)
        {
            return t.GetBoolean(t.GetOrdinal(input));
        }
        public static float GetFloat(this SqlDataReader t, string input)
        {
            //data type is returned as a boxed double
            return (float)t.GetDouble(t.GetOrdinal(input));
        }
        public static double GetDouble(this SqlDataReader t, string input)
        {
           return t.GetDouble(t.GetOrdinal(input));
        }
        public static decimal GetDecimal(this SqlDataReader t, string input)
        {
            return t.GetDecimal(t.GetOrdinal(input));
        }
    }
}
