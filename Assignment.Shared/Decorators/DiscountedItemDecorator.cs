using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.DTO;
using Assignment.Entities;


namespace Assignment.Decorators
{
    // Decorator class for adding discount functionality to an item
    public class DiscountedItemDecorator : ItemDecorator
    {
        public decimal Discount { get; set; }

        // Constructor to initialize the DiscountedItemDecorator with an item and discount
        public DiscountedItemDecorator(ItemDTO item, decimal discount) : base(item)
        {
            Discount = discount;
        }

        // Overrides the Price property to apply the discount
        public override decimal Price
        {
            get => base.Price - Discount;
            set => base.Price = value;
        }
    }
}
