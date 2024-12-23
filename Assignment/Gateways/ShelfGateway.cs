using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.DTO;
using Assignment.Helpers;
using Microsoft.Data.SqlClient;

namespace Assignment.Gateways
{
    // Singleton gateway class for managing Shelf-related database operations
    public class ShelfGateway
    {
        private static ShelfGateway _instance;

        // Private constructor to prevent direct instantiation
        private ShelfGateway() { }

        // Public property to access the singleton instance
        public static ShelfGateway Instance => _instance ?? (_instance = new ShelfGateway());

        // Retrieves all shelves from the database
        public List<ShelfDTO> GetAllShelves()
        {
            var shelves = new List<ShelfDTO>();
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var command = new SqlCommand("SELECT * FROM Shelf", connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                shelves.Add(new ShelfDTO
                {
                    ShelfNo = reader["shelf_no"].ToString(),
                    StacksNo = Convert.ToInt32(reader["stacks_no"])
                });
            }
            return shelves;
        }
    }
}
