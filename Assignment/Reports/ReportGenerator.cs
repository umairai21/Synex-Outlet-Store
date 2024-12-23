using Assignment.DTO;
using Assignment.Gateways;
using Assignment.Helpers;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Assignment.Reports
{
    public class ReportGenerator
    {
        private readonly StockGateway _stockGateway;
        private readonly BillGateway _billGateway;

        public ReportGenerator(StockGateway stockGateway, BillGateway billGateway)
        {
            _stockGateway = stockGateway;
            _billGateway = billGateway;
        }

        public void GenerateSalesReport()
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var command = new SqlCommand(
                "SELECT bill_serial_no, item_code, item_name, SUM(quantity) AS total_quantity, SUM(total_price) AS total_revenue " +
                "FROM bill_item " +
                "GROUP BY bill_serial_no, item_code, item_name",
                connection);
            using var reader = command.ExecuteReader();
            Console.WriteLine("Sales Report");
            Console.WriteLine("------------");
            Console.WriteLine("Bill Serial No | Item Code | Item Name        | Total Quantity | Total Revenue");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["bill_serial_no"],-14} | {reader["item_code"],-9} | {reader["item_name"],-15} | {reader["total_quantity"],-14} | {reader["total_revenue"],-12}");
            }
        }



        public void GenerateReshelveReport()
        {
            var itemsToBeReshelved = _stockGateway.GetItemsToBeReshelved();
            Console.WriteLine("Reshelve Report");
            Console.WriteLine("---------------");
            Console.WriteLine("Item Code | Item Name        | Stock Code | Quantity | Expiry Date  | Shelf No");
            foreach (var item in itemsToBeReshelved)
            {
                Console.WriteLine($"{item.ItemCode,-9} | {item.ItemName,-15} | {item.StockCode,-10} | {item.QuantityReceived,-8} | {item.ExpiryDate.ToShortDateString(),-11} | {item.ShelfNo}");
            }
        }

        public void GenerateReorderReport()
        {
            var itemsBelowReorderLevel = _stockGateway.GetItemsBelowReorderLevel(50); // Example threshold
            Console.WriteLine("Reorder Report");
            Console.WriteLine("--------------");
            Console.WriteLine("Item Code | Item Name        | Quantity");
            foreach (var item in itemsBelowReorderLevel)
            {
                Console.WriteLine($"{item.ItemCode,-9} | {item.ItemName,-15} | {item.QuantityReceived}");
            }
        }

        public void GenerateStockReport()
        {
            var stocks = _stockGateway.GetAllStocksWithItemInfo();
            Console.WriteLine("Stock Report");
            Console.WriteLine("------------");
            Console.WriteLine("Stock Code | Item Code | Item Name        | Item Price | Quantity | Expiry Date  | Shelf No");
            foreach (var stock in stocks)
            {
                Console.WriteLine($"{stock.StockCode,-10} | {stock.ItemCode,-9} | {stock.ItemName,-15} | {stock.ItemPrice,-10} | {stock.QuantityReceived,-8} | {stock.ExpiryDate.ToShortDateString(),-11} | {stock.ShelfNo}");
            }
        }

        public void GenerateBillReport()
        {
            var bills = _billGateway.GetAllBills();
            Console.WriteLine("Bill Report");
            Console.WriteLine("-----------");
            Console.WriteLine("Serial No | Date       | Total Price | Discount | Cash Received | Balance");
            foreach (var bill in bills)
            {
                Console.WriteLine($"{bill.SerialNo,-9} | {bill.Date.ToShortDateString(),-10} | {bill.TotalPrice,-11} | {bill.Discount,-8} | {bill.CashReceived,-13} | {bill.Balance}");
            }
        }
    }
}
