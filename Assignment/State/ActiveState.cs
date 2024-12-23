using Assignment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.State
{
    public class ActiveState : CartState
    {
        // Adds an item to the cart
        public override void AddItem(Cart cart, Item item)
        {
            cart.Items.Add(item);
        }

        // Changes the state of the cart to CheckedOutState, calculates the total, and generates the bill
        public override void Checkout(Cart cart, decimal cashTendered)
        {
            cart.State = new CheckedOutState();
            cart.CalculateTotal();
            cart.GenerateBill(cashTendered);
        }
    }

}
