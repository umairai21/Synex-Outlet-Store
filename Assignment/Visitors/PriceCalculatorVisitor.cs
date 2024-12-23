using Assignment.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Visitors
{
    public class PriceCalculatorVisitor: IVisitor
    {
        // Property to store the total price
        public decimal TotalPrice { get; private set; }

        // Visit method to calculate the total price of an item
        public void Visit(ItemDTO item)
        {
            TotalPrice += item.Price * item.Quantity;
        }

        // Visit method for stock, implementation not needed in this visitor
        public void Visit(StockDTO stock)
        {
            // Implement if needed
        }
    }
}
