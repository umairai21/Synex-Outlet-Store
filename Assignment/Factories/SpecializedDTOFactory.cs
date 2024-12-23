using Assignment.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Factories
{
    // Specialized factory class for creating specific DTO objects with default values
    public class SpecializedDTOFactory : DTOFactory
    {
        // Creates and returns a new specialized ItemDTO object with default values
        public override ItemDTO CreateItemDTO()
        {
            return new ItemDTO { Code = "DefaultCode", Name = "DefaultName", Price = 0.0m, Quantity = 0 };
        }

        // Creates and returns a new specialized StockDTO object with default values
        public override StockDTO CreateStockDTO()
        {
            return new StockDTO { StockCode = "DefaultStockCode", ExpiryDate = DateTime.Now.AddYears(1), QuantityReceived = 0 };
        }

        // Creates and returns a new specialized BillDTO object with default values
        public override BillDTO CreateBillDTO()
        {
            return new BillDTO { SerialNo = "DefaultSerialNo", Date = DateTime.Now, TotalPrice = 0.0f, Discount = 0.0f, CashReceived = 0.0f, Balance = 0.0f };
        }
    }
}
