using Assignment.DTO;
using Assignment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Visitors
{
    public class InventoryVisitor : IVisitor
    {
        // Method to visit and display details of an item
        public void Visit(ItemDTO item)
        {
            Console.WriteLine($"Item: {item.Name}, Code: {item.Code}, Price: {item.Price}, Quantity: {item.Quantity}");
        }

        // Method to visit and display details of a stock
        public void Visit(StockDTO stock)
        {
            Console.WriteLine($"Stock: {stock.StockCode}, Expiry Date: {stock.ExpiryDate}, Quantity Received: {stock.QuantityReceived}");
        }
    }
}
