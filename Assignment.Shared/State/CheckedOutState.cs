using Assignment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.State
{
    public class CheckedOutState : CartState
    {
        // Throws an exception when attempting to add an item to a checked-out cart
        public override void AddItem(Cart cart, Item item)
        {
            throw new InvalidOperationException("Cannot add items to a checked out cart.");
        }

        // Throws an exception when attempting to checkout an already checked-out cart
        public override void Checkout(Cart cart, decimal cashTendered)
        {
            throw new InvalidOperationException("Cart already checked out.");
        }
    }
}
