using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Entities;

namespace Assignment.Factories
{
    // Abstract factory class for creating Item objects
    public abstract class ItemFactory
    {
        // Abstract method to create an Item object based on the provided code
        public abstract Item CreateItem(string code);
    }
}
