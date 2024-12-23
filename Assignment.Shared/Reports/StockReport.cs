using Assignment.DTO;
using Assignment.Gateways;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Reports
{
    public class StockReport: ReportTemplate
    {
        private readonly StockGateway _stockGateway;
        private List<StockWithItemDTO> _stocks;

        // Constructor that initializes the StockGateway
        public StockReport(StockGateway stockGateway)
        {
            _stockGateway = stockGateway;
        }

        // Fetches the stock data
        protected override void FetchData()
        {
            _stocks = _stockGateway.GetAllStocksWithItemInfo();
        }

        // Formats the stock report header
        protected override void FormatReport()
        {
            Console.WriteLine("Stock Report");
            Console.WriteLine("------------");
        }

        // Prints the stock report details
        protected override void PrintReport()
        {
            Console.WriteLine("Stock Code | Item Code | Item Name        | Item Price | Quantity | Expiry Date  | Shelf No");
            foreach (var stock in _stocks)
            {
                Console.WriteLine($"{stock.StockCode,-10} | {stock.ItemCode,-9} | {stock.ItemName,-15} | {stock.ItemPrice,-10} | {stock.QuantityReceived,-8} | {stock.ExpiryDate.ToShortDateString(),-11} | {stock.ShelfNo}");
            }
        }
    }
}
