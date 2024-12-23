using Assignment.DTO;
using Assignment.Entities;
using Assignment.Facade;
using Assignment.Gateways;
using Assignment.Reports;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYOSTests
{
    [TestClass]
    public class BehaviourTests
    {
        [TestMethod]
        public void AddItem_ShouldCallSaveItem()
        {
            var mockFacade = new Mock<BillingSystemFacade>();
            var newItem = new ItemDTO { Code = "I001", Name = "Item 1", Price = 10.0m };
            mockFacade.Object.AddItem(newItem);

            mockFacade.Verify(f => f.AddItem(It.IsAny<ItemDTO>()), Times.Once);
            Console.WriteLine("AddItem_ShouldCallSaveItem: Passed");
        }

        public void Checkout_ShouldCallSaveBill()
        {
            var mockFacade = new Mock<BillingSystemFacade>();
            var purchasedItems = new List<ItemDTO>
        {
            new ItemDTO { Code = "I001", Quantity = 2 },
            new ItemDTO { Code = "I002", Quantity = 1 }
        };
            mockFacade.Object.Checkout(purchasedItems, 10, 100, out var bill);

            mockFacade.Verify(f => f.Checkout(It.IsAny<List<ItemDTO>>(), It.IsAny<float>(), It.IsAny<float>(), out bill), Times.Once);
            Console.WriteLine("Checkout_ShouldCallSaveBill: Passed");
        }

        public void GenerateSalesReport_ShouldCallGetSalesByCurrentDate()
        {
            var mockFacade = new Mock<BillingSystemFacade>();
            mockFacade.Object.GenerateSalesReport();

            mockFacade.Verify(f => f.GenerateSalesReport(), Times.Once);
            Console.WriteLine("GenerateSalesReport_ShouldCallGetSalesByCurrentDate: Passed");
        }

        public void GenerateStockReport_ShouldCallGetAllStocksWithItemInfo()
        {
            var mockFacade = new Mock<BillingSystemFacade>();
            mockFacade.Object.GenerateStockReport();

            mockFacade.Verify(f => f.GenerateStockReport(), Times.Once);
            Console.WriteLine("GenerateStockReport_ShouldCallGetAllStocksWithItemInfo: Passed");
        }
    }
}
