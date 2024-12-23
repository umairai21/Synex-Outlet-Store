using Assignment.DTO;
using Assignment.Gateways;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Reports
{
    // Concrete implementation of the ReportTemplate for generating reorder reports.
    public class ReorderReport : ReportTemplate
    {
        // Reference to the StockGateway for data access.
        private readonly StockGateway _stockGateway;
        // List to store fetched items that are below the reorder level.
        private List<StockWithItemDTO> _itemsBelowReorderLevel;
        // Constant for the reorder level threshold.
        private const int ReorderLevelThreshold = 50;

        // Constructor that initializes the StockGateway.
        public ReorderReport(StockGateway stockGateway)
        {
            _stockGateway = stockGateway;
        }

        // Method to fetch data from the database.
        protected override void FetchData()
        {
            _itemsBelowReorderLevel = _stockGateway.GetItemsBelowReorderLevel(ReorderLevelThreshold);
        }

        // Method to format the report header.
        protected override void FormatReport()
        {
            Console.WriteLine("Reorder Report");
            Console.WriteLine("--------------");
        }

        // Method to print the report details.
        protected override void PrintReport()
        {
            Console.WriteLine("Item Code | Item Name        | Quantity");
            foreach (var item in _itemsBelowReorderLevel)
            {
                Console.WriteLine($"{item.ItemCode,-9} | {item.ItemName,-15} | {item.QuantityReceived}");
            }
        }
    }

}
