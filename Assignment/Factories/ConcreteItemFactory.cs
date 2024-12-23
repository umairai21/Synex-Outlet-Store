using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Entities;
using Assignment.Gateways;


namespace Assignment.Factories
{
    // Concrete factory class for creating Item objects
    public class ConcreteItemFactory : ItemFactory
    {
        private readonly IItemGateway _itemGateway;

        // Constructor to inject the IItemGateway dependency
        public ConcreteItemFactory(IItemGateway itemGateway)
        {
            _itemGateway = itemGateway;
        }

        // Creates and returns an Item object based on the provided code
        public override Item CreateItem(string code)
        {
            var itemDTO = _itemGateway.GetItemByCode(code);
            if (itemDTO == null)
            {
                Console.WriteLine($"Item with code {code} not found.");
                return new NullItem();
            }

            return new Item(itemDTO.Code, itemDTO.Name, itemDTO.Quantity, itemDTO.Price);
        }
    }
}
