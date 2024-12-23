using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2.DTO
{
    public class StockWithItemDTO
    {
        public string StockCode { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int QuantityReceived { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public decimal ItemPrice { get; set; }
        public string ShelfNo { get; set; }
    }
}
