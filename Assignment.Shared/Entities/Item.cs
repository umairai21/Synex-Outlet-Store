using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Assignment.Entities
{
    // Entity class representing an item
    public class Item
    {
        // Code of the item
        public string Code { get; }

        // Name of the item
        public string Name { get; }

        // Quantity of the item
        public int Quantity { get; set; }

        // Price of the item
        public virtual decimal Price { get; set; }

        // Constructor to initialize an item with code, name, quantity, and price
        public Item(string code, string name, int quantity, decimal price)
        {
            Code = code;
            Name = name;
            Quantity = quantity;
            Price = price;
        }

        // Method to display the item details
        public virtual void Display()
        {
            Console.WriteLine($"{Name}: {Price:C2} x {Quantity}");
        }
    }
}
