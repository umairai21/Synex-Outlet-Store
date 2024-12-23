using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.DTO
{
    // Data Transfer Object (DTO) for representing a shelf
    public class ShelfDTO
    {
        // Number of the shelf
        public string ShelfNo { get; set; }

        // Number of stacks on the shelf
        public int StacksNo { get; set; }
    }
}
