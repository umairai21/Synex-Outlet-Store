using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Helpers
{
    public class DatabaseHelper
    {
        private static readonly string connectionString = "Data Source=UMAIR-CHANGE8\\SQLEXPRESS;Initial Catalog=SynexOutletStore;Integrated Security=True;Encrypt=False;";

        public static SqlConnection GetConnection()
        {
            try
            {
                return new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting the database connection: {ex.Message}");
                throw;
            }
        }
    }
}
