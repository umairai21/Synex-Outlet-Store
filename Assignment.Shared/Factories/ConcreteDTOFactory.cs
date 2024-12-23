using Assignment.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Factories
{
    // Concrete factory class for creating various DTO objects
    public class ConcreteDTOFactory : DTOFactory
    {
        // Creates and returns a new ItemDTO object
        public override ItemDTO CreateItemDTO()
        {
            return new ItemDTO();
        }

        // Creates and returns a new StockDTO object
        public override StockDTO CreateStockDTO()
        {
            return new StockDTO();
        }

        // Creates and returns a new BillDTO object
        public override BillDTO CreateBillDTO()
        {
            return new BillDTO();
        }
    }
}
