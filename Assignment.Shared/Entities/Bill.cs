using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Assignment.Entities
{
    // Entity class representing a bill
    public class Bill
    {
        // List of items and their quantities in the bill
        public List<(Item Item, int Quantity)> Items { get; }

        // Total amount of the bill
        public decimal Total { get; set; }

        // Constructor to initialize the Items list
        public Bill()
        {
            Items = new List<(Item, int)>();
        }

        // Method to add an item and its quantity to the bill
        public void AddItem(Item item, int quantity)
        {
            Items.Add((item, quantity));
        }
    }
}
