using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.DTO;
using Assignment.Entities;


namespace Assignment.Decorators
{
    // Abstract decorator class for decorating ItemDTO objects
    public abstract class ItemDecorator : ItemDTO
    {
        protected ItemDTO _item;

        // Constructor to initialize the ItemDecorator with an ItemDTO
        public ItemDecorator(ItemDTO item)
        {
            _item = item;
        }

        // Overrides the Code property to get and set the value from the decorated item
        public override string Code
        {
            get => _item.Code;
            set => _item.Code = value;
        }

        // Overrides the Name property to get and set the value from the decorated item
        public override string Name
        {
            get => _item.Name;
            set => _item.Name = value;
        }

        // Overrides the Price property to get and set the value from the decorated item
        public override decimal Price
        {
            get => _item.Price;
            set => _item.Price = value;
        }

        // Overrides the Quantity property to get and set the value from the decorated item
        public override int Quantity
        {
            get => _item.Quantity;
            set => _item.Quantity = value;
        }
    }
}
