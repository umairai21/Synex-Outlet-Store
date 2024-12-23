using Assignment.Builders;
using Assignment.DTO;
using Assignment.Gateways;
using Assignment.Reports;
using System;
using System.Collections.Generic;

namespace Assignment.Facade
{
    public class BillingSystemFacade : IBillingSystemFacade
    {
        private readonly ItemGateway _itemGateway;
        private readonly StockGateway _stockGateway;
        private readonly BillGateway _billGateway;

        public BillingSystemFacade()
        {
            _itemGateway = new ItemGateway();
            _stockGateway = new StockGateway();
            _billGateway = new BillGateway();
        }

        public void AddItem(ItemDTO item)
        {
            _itemGateway.SaveItem(item);
        }

        public void DisplayInventory()
        {
            var stocks = _stockGateway.GetAllStocksWithItemInfo();
            Console.WriteLine("Stock Code | Item Code | Item Name        | Item Price | Quantity | Expiry Date  | Shelf No");
            foreach (var stock in stocks)
            {
                Console.WriteLine($"{stock.StockCode,-10} | {stock.ItemCode,-9} | {stock.ItemName,-15} | {stock.ItemPrice,-10} | {stock.QuantityReceived,-8} | {stock.ExpiryDate.ToShortDateString(),-11} | {stock.ShelfNo}");
            }
        }

        public void AddStock(StockDTO stock, string itemCode, int quantity, string shelfNo)
        {
            stock.QuantityReceived = quantity; // Ensure quantity is set in StockDTO
            _itemGateway.SaveStock(stock); // Save stock in the Stock table
            _itemGateway.SaveItemStock(stock.StockCode, itemCode, quantity); // Save item-stock relation in the item_Stock table
            _stockGateway.AddStockToShelf(stock, itemCode, quantity, shelfNo); // Save stock-shelf relation in the Stock_Shelf table
        }

        public void GenerateSalesReport()
        {
            var reportGenerator = new ReportGenerator(_stockGateway, _billGateway);
            reportGenerator.GenerateSalesReport();
        }

        public void GenerateReshelveReport()
        {
            var report = new ReshelveReport(_stockGateway);
            report.GenerateReport();
        }

        public void GenerateReorderReport()
        {
            var report = new ReorderReport(_stockGateway);
            report.GenerateReport();
        }

        public void GenerateStockReport()
        {
            var report = new StockReport(_stockGateway);
            report.GenerateReport();
        }

        public void GenerateBillReport()
        {
            var report = new BillReport(_billGateway);
            report.GenerateReport();
        }

        public void Checkout(List<ItemDTO> purchasedItems, float discount, float cashReceived, out BillDTO bill)
        {
            decimal totalAmount = 0;
            foreach (var item in purchasedItems)
            {
                totalAmount += item.Price * item.Quantity;
                _stockGateway.UpdateStockAfterPurchase(item.Code, item.Quantity);
                _stockGateway.UpdateItemStockAfterPurchase(item.Code, item.Quantity);
            }
            decimal totalAfterDiscount = totalAmount - (decimal)discount;

            var billBuilder = new ConcreteBillBuilder();
            billBuilder.SetSerialNo(Guid.NewGuid().ToString().Substring(0, 4));
            billBuilder.SetDate(DateTime.Now);
            billBuilder.SetTotalPrice((float)totalAfterDiscount);
            billBuilder.SetDiscount(discount);
            billBuilder.SetCashReceived(cashReceived);
            billBuilder.SetBalance(cashReceived - (float)totalAfterDiscount);
            bill = billBuilder.Build();

            _billGateway.SaveBill(bill);

            // Insert items into bill_item table
            foreach (var item in purchasedItems)
            {
                var totalPrice = item.Price * item.Quantity;
                _billGateway.SaveBillItem(bill.SerialNo, item.Code, item.Name, item.Quantity, item.Price, totalPrice);
            }
        }

        public List<StockWithItemDTO> GetInventory()
        {
            // Retrieve stocks with item information
            var stocks = _stockGateway.GetAllStocksWithItemInfo();
            return stocks; // Return the list of stocks with item info
        }



        public void GenerateBill(BillDTO bill)
        {
            Console.WriteLine("Bill Generated:");
            Console.WriteLine($"Serial No: {bill.SerialNo}");
            Console.WriteLine($"Date: {bill.Date}");
            Console.WriteLine($"Total Price: {bill.TotalPrice}");
            Console.WriteLine($"Discount: {bill.Discount}");
            Console.WriteLine($"Cash Received: {bill.CashReceived}");
            Console.WriteLine($"Balance: {bill.Balance}");
        }

        public ItemDTO GetItemByCode(string itemCode)
        {
            return _itemGateway.GetItemByCode(itemCode);
        }

        public List<ShelfDTO> GetShelves()
        {
            return _stockGateway.GetAllShelves();
        }
    }
}
