using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Composite
{
    // Abstract class representing a component in the inventory hierarchy
    public abstract class InventoryComponent
    {
        // Adds a component to the inventory (default implementation does nothing)
        public virtual void Add(InventoryComponent component) { }

        // Removes a component from the inventory (default implementation does nothing)
        public virtual void Remove(InventoryComponent component) { }

        // Displays the inventory component (default implementation does nothing)
        public virtual void Display(int depth) { }
    }
}
