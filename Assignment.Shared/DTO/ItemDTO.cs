using Assignment.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Assignment.DTO
{
    // Data Transfer Object (DTO) for representing an item
    public class ItemDTO
    {
        // Code of the item
        public virtual string Code { get; set; }

        // Name of the item
        public virtual string Name { get; set; }

        // Price of the item
        public virtual decimal Price { get; set; }

        // Quantity of the item
        public virtual int Quantity { get; set; }

        // Accept method for the Visitor pattern
        public void Accept(IVisitor visitor)
         {
             visitor.Visit(this);
         }
    }
}
