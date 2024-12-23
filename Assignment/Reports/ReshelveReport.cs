using Assignment.DTO;
using Assignment.Gateways;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Reports
{
    public class ReshelveReport: ReportTemplate
    {
        private readonly StockGateway _stockGateway;
        private List<ItemReshelveDTO> _itemsToBeReshelved;

        // Constructor to initialize the StockGateway dependency
        public ReshelveReport(StockGateway stockGateway)
        {
            _stockGateway = stockGateway;
        }

        // Fetches data from the StockGateway
        protected override void FetchData()
        {
            _itemsToBeReshelved = _stockGateway.GetItemsToBeReshelvedForEndOfDay();
        }

        // Formats the header for the report
        protected override void FormatReport()
        {
            Console.WriteLine("Reshelve Report");
            Console.WriteLine("---------------");
        }

        // Prints the report details
        protected override void PrintReport()
        {
            Console.WriteLine("Item Code | Item Name        | Quantity");
            foreach (var item in _itemsToBeReshelved)
            {
                Console.WriteLine($"{item.ItemCode,-9} | {item.ItemName,-15} | {item.Quantity}");
            }
        }
    }
}
