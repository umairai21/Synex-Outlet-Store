using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.DTO;
using Assignment.Entities;
using Assignment.Facade;


namespace Assignment.Commands
{
    // Command class for checking out using the BillingSystemFacade
    public class CheckoutCommand : ICommand
    {
        private readonly List<ItemDTO> _purchasedItems;
        private readonly float _discount;
        private readonly float _cashReceived;
        private readonly BillingSystemFacade _facade;

        // Constructor for CheckoutCommand, initializes the purchased items, discount, cash received, and facade
        public CheckoutCommand(List<ItemDTO> purchasedItems, float discount, float cashReceived, BillingSystemFacade facade)
        {
            _purchasedItems = purchasedItems;
            _discount = discount;
            _cashReceived = cashReceived;
            _facade = facade;
        }

        // Executes the command to checkout using the facade and generate a bill
        public void Execute()
        {
            _facade.Checkout(_purchasedItems, _discount, _cashReceived, out var bill);
            _facade.GenerateBill(bill);
        }
    }
}
