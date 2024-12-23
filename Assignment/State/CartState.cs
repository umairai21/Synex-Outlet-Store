using Assignment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.State
{
    public abstract class CartState
    {
        // Abstract method to add an item to the cart
        public abstract void AddItem(Cart cart, Item item);

        // Abstract method to checkout the cart
        public abstract void Checkout(Cart cart, decimal cashTendered);
    }
}
