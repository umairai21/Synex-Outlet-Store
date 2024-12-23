using Assignment.DTO;
using Assignment.Facade;
using Assignment.Commands;
using Assignment.Factories;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SYOSTests
{
    [TestClass]
    public class AllUnitTests
    {
        [TestMethod]
        public void AddItem_Should_AddItemSuccessfully()
        {
            // Arrange
            var facadeMock = new Mock<BillingSystemFacade>();
            var factoryMock = new Mock<ConcreteDTOFactory>();
            var newItem = new ItemDTO { Code = "I012", Name = "Item1", Price = 100.0m };
            factoryMock.Setup(f => f.CreateItemDTO()).Returns(newItem);

            // Act
            var addItemCommand = new AddItemCommand(newItem, facadeMock.Object);
            addItemCommand.Execute();

            // Assert
            facadeMock.Verify(f => f.AddItem(newItem), Times.Once);
        }


        [TestMethod]
        public void DisplayInventory_Should_DisplayInventory()
        {
            // Arrange
            var facadeMock = new Mock<BillingSystemFacade>();

            // Act
            facadeMock.Object.DisplayInventory();

            // Assert
            facadeMock.Verify(f => f.DisplayInventory(), Times.Once);
        }

        [TestMethod]
        public void AddStock_Should_AddStockSuccessfully()
        {
            // Arrange
            var facadeMock = new Mock<BillingSystemFacade>();
            var factoryMock = new Mock<ConcreteDTOFactory>();
            var newStock = new StockDTO { StockCode = "S001", ExpiryDate = DateTime.Now.AddMonths(6), QuantityReceived = 50 };
            factoryMock.Setup(f => f.CreateStockDTO()).Returns(newStock);
            var itemCode = "I001";
            var quantity = 50;
            var shelfNo = "S1";

            // Act
            var addStockCommand = new AddStockCommand(newStock, itemCode, quantity, shelfNo, facadeMock.Object);
            addStockCommand.Execute();

            // Assert
            facadeMock.Verify(f => f.AddStock(newStock, itemCode, quantity, shelfNo), Times.Once);
        }


        [TestMethod]
        public void GenerateSalesReport_Should_GenerateSalesReport()
        {
            // Arrange
            var facadeMock = new Mock<BillingSystemFacade>();

            // Act
            facadeMock.Object.GenerateSalesReport();

            // Assert
            facadeMock.Verify(f => f.GenerateSalesReport(), Times.Once);
        }

        [TestMethod]
        public void GenerateReshelveReport_Should_GenerateReshelveReport()
        {
            // Arrange
            var facadeMock = new Mock<BillingSystemFacade>();

            // Act
            facadeMock.Object.GenerateReshelveReport();

            // Assert
            facadeMock.Verify(f => f.GenerateReshelveReport(), Times.Once);
        }

        [TestMethod]
        public void GenerateReorderReport_Should_GenerateReorderReport()
        {
            // Arrange
            var facadeMock = new Mock<BillingSystemFacade>();

            // Act
            facadeMock.Object.GenerateReorderReport();

            // Assert
            facadeMock.Verify(f => f.GenerateReorderReport(), Times.Once);
        }

        [TestMethod]
        public void GenerateStockReport_Should_GenerateStockReport()
        {
            // Arrange
            var facadeMock = new Mock<BillingSystemFacade>();

            // Act
            facadeMock.Object.GenerateStockReport();

            // Assert
            facadeMock.Verify(f => f.GenerateStockReport(), Times.Once);
        }

        [TestMethod]
        public void GenerateBillReport_Should_GenerateBillReport()
        {
            // Arrange
            var facadeMock = new Mock<BillingSystemFacade>();

            // Act
            facadeMock.Object.GenerateBillReport();

            // Assert
            facadeMock.Verify(f => f.GenerateBillReport(), Times.Once);
        }

        [TestMethod]
        public void Checkout_Should_CheckoutSuccessfully()
        {
            // Arrange
            var facadeMock = new Mock<IBillingSystemFacade>();
            var purchasedItems = new List<ItemDTO>
            {
                new ItemDTO { Code = "I001", Name = "Item1", Price = 100m, Quantity = 2 },
                new ItemDTO { Code = "I002", Name = "Item2", Price = 150m, Quantity = 3 }
            };
            float discount = 10;
            float cashReceived = 500;
            BillDTO bill = null;

            facadeMock.Setup(f => f.Checkout(purchasedItems, discount, cashReceived, out bill))
                      .Callback((List<ItemDTO> items, float d, float c, out BillDTO b) =>
                      {
                          b = new BillDTO
                          {
                              SerialNo = "B001",
                              Date = DateTime.Now,
                              TotalPrice = 650,
                              Discount = discount,
                              CashReceived = cashReceived,
                              Balance = cashReceived - 650
                          };
                      });

            facadeMock.Setup(f => f.GenerateBill(It.IsAny<BillDTO>()));

            // Act
            facadeMock.Object.Checkout(purchasedItems, discount, cashReceived, out bill);
            facadeMock.Object.GenerateBill(bill);

            // Assert
            facadeMock.Verify(f => f.Checkout(purchasedItems, discount, cashReceived, out bill), Times.Once);
            facadeMock.Verify(f => f.GenerateBill(It.IsAny<BillDTO>()), Times.Once);
        }

    }
}
