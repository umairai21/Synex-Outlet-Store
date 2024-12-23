using Assignment.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Composite
{
    // Concrete class representing an item in the inventory hierarchy
    public class ItemComponent : InventoryComponent
    {
        private readonly ItemDTO _item;

        // Constructor to initialize the ItemComponent with an ItemDTO
        public ItemComponent(ItemDTO item)
        {
            _item = item;
        }

        // Displays the item with the given depth for indentation
        public override void Display(int depth)
        {
            Console.WriteLine(new string('-', depth) + _item.Name);
        }
    }
}
