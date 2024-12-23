using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.DTO
{
    // Data Transfer Object (DTO) for representing an item that needs to be reshelved
    public class ItemReshelveDTO
    {
        // Code of the item
        // Property for the item code
        public string ItemCode { get; set; }

        // Property for the item name
        public string ItemName { get; set; }

        // Property for the quantity of items to be reshelved
        public int Quantity { get; set; }
    }
}
