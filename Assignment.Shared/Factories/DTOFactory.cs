using Assignment.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Factories
{
    // Abstract factory class for creating various DTO objects
    public abstract class DTOFactory
    {
        // Abstract method to create an ItemDTO object
        public abstract ItemDTO CreateItemDTO();

        // Abstract method to create a StockDTO object
        public abstract StockDTO CreateStockDTO();

        // Abstract method to create a BillDTO object
        public abstract BillDTO CreateBillDTO();
    }
}
