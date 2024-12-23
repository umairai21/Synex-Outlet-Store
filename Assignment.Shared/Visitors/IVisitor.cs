using Assignment.DTO;
using Assignment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Visitors
{
    public interface IVisitor
    {
        // Method to visit an item
        void Visit(ItemDTO item);

        // Method to visit a stock
        void Visit(StockDTO stock);
    }
}

