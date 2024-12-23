using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.DTO
{
    // Data Transfer Object (DTO) for representing a sale
    public class SaleDTO
    {
        // Code of the item sold
        public string ItemCode { get; set; }

        // Name of the item sold
        public string ItemName { get; set; }

        // Quantity of the item sold
        public int Quantity { get; set; }

        // Revenue generated from the sale of the item
        public decimal Revenue { get; set; }

        // Date and time of the sale
        public DateTime Date { get; set; }
    }
}
