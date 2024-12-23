using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Composite
{
    // Composite class representing a collection of inventory components
    public class StockComposite : InventoryComponent
    {
        private readonly List<InventoryComponent> _children = new List<InventoryComponent>();

        // Adds a component to the composite
        public override void Add(InventoryComponent component)
        {
            _children.Add(component);
        }

        // Removes a component from the composite
        public override void Remove(InventoryComponent component)
        {
            _children.Remove(component);
        }

        // Displays the components in the composite with the given depth for indentation
        public override void Display(int depth)
        {
            foreach (var component in _children)
            {
                component.Display(depth + 2);
            }
        }
    }
}
