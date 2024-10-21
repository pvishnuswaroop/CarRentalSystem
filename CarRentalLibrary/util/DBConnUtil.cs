using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;



namespace CarRentalLibrary.util
{
    // Utility class for handling database connections
    public class DBConnUtil
    {
        public static SqlConnection GetConnection()
        {
            
            string connectionString = UtilityPro.DBPropertyUtil.ReturnCn("CarRentalDBConnection"); 
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}

