using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Assignment.Entities
{
    // Entity class representing a null item, used to avoid null references
    public class NullItem : Item
    {
        // Constructor to initialize a NullItem with default values
        public NullItem() : base("N/A", "No Item", 0, 0.0m) { }

        // Override the Display method to indicate that the item was not found
        public override void Display()
        {
            Console.WriteLine("Item not found.");
        }
    }
}
