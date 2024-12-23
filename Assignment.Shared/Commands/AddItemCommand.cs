using Assignment.DTO;
using Assignment.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Commands
{
    // Concrete command to add an item
    public class AddItemCommand : ICommand
    {
        private readonly ItemDTO _item;
        private readonly BillingSystemFacade _facade;

        // Constructor to initialize the AddItemCommand
        public AddItemCommand(ItemDTO item, BillingSystemFacade facade)
        {
            _item = item;
            _facade = facade;
        }

        // Execute method to add the item
        public void Execute()
        {
            _facade.AddItem(_item);
        }
    }
}
