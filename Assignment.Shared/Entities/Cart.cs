using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.State;

namespace Assignment.Entities
{
    // Entity class representing a shopping cart
    public class Cart
    {
        // List of items in the cart
        public List<Item> Items { get; }

        // Current state of the cart
        public CartState State { get; set; }

        // Constructor to initialize the Items list and set the initial state
        public Cart()
        {
            Items = new List<Item>();
            State = new ActiveState();
        }

        // Method to add an item to the cart, delegates to the state
        public void AddItem(Item item)
        {
            State.AddItem(this, item);
        }

        // Method to checkout the cart, delegates to the state
        public void Checkout(decimal cashTendered)
        {
            State.Checkout(this, cashTendered);
        }

        // Method to calculate the total amount of the cart
        public void CalculateTotal()
        {
            // Calculate total
        }

        // Method to generate a bill for the cart
        public void GenerateBill(decimal cashTendered)
        {
            // Generate bill
        }
    }
}
