using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using Assignment.Entities;
using Assignment.Facade;
using Assignment.State;
using Assignment.Visitors;
using System.Threading.Tasks;
using Assignment.Builders;
using Assignment.DTO;
using Assignment.Gateways;

namespace SYOSTests
{
    [TestClass]
    public class StateTests
    {
        [TestMethod]
        public void AddItem_ShouldIncreaseItemsCount()
        {
            var cart = new Cart();
            var item = new Item("I001", "Item 1", 1, 10.0m);
            cart.AddItem(item);

            if (cart.Items.Count == 1)
            {
                Console.WriteLine("AddItem_ShouldIncreaseItemsCount: Passed");
            }
            else
            {
                Console.WriteLine("AddItem_ShouldIncreaseItemsCount: Failed");
            }
        }



        [TestMethod]
        public void Checkout_ShouldChangeStateToCheckedOut()
        {
            var cart = new Cart();
            var item = new Item("I001", "Item 1", 1, 10.0m);
            cart.AddItem(item);
            cart.Checkout(20.0m);

            if (cart.State is CheckedOutState)
            {
                Console.WriteLine("Checkout_ShouldChangeStateToCheckedOut: Passed");
            }
            else
            {
                Console.WriteLine("Checkout_ShouldChangeStateToCheckedOut: Failed");
            }
        }




        [TestMethod]
        public void DisplayInventory_ShouldShowItems()
        {
            var facade = new BillingSystemFacade();
            var item = new ItemDTO { Code = "I001", Name = "Item 1", Price = 10.0m };
            facade.AddItem(item);

            Console.WriteLine("DisplayInventory:");
            facade.DisplayInventory();
            // Manually verify the output to ensure it displays the added item
        }




        public void GenerateBill_ShouldSetCorrectTotalPrice()
        {
            var billBuilder = new ConcreteBillBuilder();
            var items = new List<ItemDTO>
             {
                 new ItemDTO { Price = 10.0m, Quantity = 2 },
                 new ItemDTO { Price = 20.0m, Quantity = 1 }
             };
            decimal totalPrice = items.Sum(i => i.Price * i.Quantity);
            billBuilder.SetTotalPrice((float)totalPrice);
            var bill = billBuilder.Build();

            if (bill.TotalPrice == (float)totalPrice)
            {
                Console.WriteLine("GenerateBill_ShouldSetCorrectTotalPrice: Passed");
            }
            else
            {
                Console.WriteLine("GenerateBill_ShouldSetCorrectTotalPrice: Failed");
            }
        }


    }
}
