using Assignment.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.NullObject
{
    // Represents a Null Object pattern for an ItemDTO.
    public class NullItem : ItemDTO
    {
        // Name property returns "No Item" to indicate the absence of an item.
        public override string Name => "No Item";

        // Price property returns 0 to indicate no cost.
        public override decimal Price => 0;

        // Quantity property returns 0 to indicate no quantity.
        public override int Quantity => 0;
    }
}
