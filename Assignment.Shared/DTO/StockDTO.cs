using Assignment.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.DTO
{
    // Data Transfer Object (DTO) for representing stock
    public class StockDTO
    {
        // Code of the stock
        public string StockCode { get; set; }

        // Expiry date of the stock
        public DateTime ExpiryDate { get; set; }

        // Quantity of the stock received
        public int QuantityReceived { get; set; }

        // Accept method for the Visitor pattern (commented out)
        public void Accept(IVisitor visitor)
        {
             visitor.Visit(this);
        }
    }
}
