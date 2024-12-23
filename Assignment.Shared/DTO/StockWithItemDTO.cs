using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.DTO
{
    // Data Transfer Object (DTO) for representing stock along with item details
    public class StockWithItemDTO
    {
        // Code of the stock
        public string StockCode { get; set; }

        // Expiry date of the stock
        public DateTime ExpiryDate { get; set; }

        // Quantity of the stock received
        public int QuantityReceived { get; set; }

        // Code of the item
        public string ItemCode { get; set; }

        // Name of the item
        public string ItemName { get; set; }

        // Price of the item
        public decimal ItemPrice { get; set; }

        // Number of the shelf where the stock is stored
        public string ShelfNo { get; set; }
    }
}
