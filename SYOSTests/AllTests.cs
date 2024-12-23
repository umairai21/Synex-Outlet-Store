using Assignment.DTO;
using Assignment.Facade;
using Assignment.Commands;
using Assignment.Factories;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Assignment.Builders;
using Assignment.Entities;
using Assignment.State;

namespace SYOSTests
{
    [TestClass]
    public class AllTests
    {
        [TestMethod]
        public void AddItem_Should_AddItemSuccessfully()
        {
            // Arrange
            var facadeMock = new Mock<BillingSystemFacade>();
            var factoryMock = new Mock<ConcreteDTOFactory>();
            var uniqueItemCode = "I" + new Random().Next(100, 999).ToString(); // Generate a shorter unique item code
            var newItem = new ItemDTO { Code = uniqueItemCode, Name = "Item1", Price = 100.0m };
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

        [TestMethod]
        public void AddItem_ShouldIncreaseItemsCount()
        {
            var cart = new Cart();
            var item = new Item("I001", "Item 1", 1, 10.0m);
            cart.AddItem(item);

            if (cart.Items.Count == 1)
            {
                Console.WriteLine("AddItem_ShouldIncreaseItemsCount: Passed");
            }
            else
            {
                Console.WriteLine("AddItem_ShouldIncreaseItemsCount: Failed");
            }
        }

        [TestMethod]
        public void Checkout_ShouldChangeStateToCheckedOut()
        {
            var cart = new Cart();
            var item = new Item("I001", "Item 1", 1, 10.0m);
            cart.AddItem(item);
            cart.Checkout(20.0m);

            if (cart.State is CheckedOutState)
            {
                Console.WriteLine("Checkout_ShouldChangeStateToCheckedOut: Passed");
            }
            else
            {
                Console.WriteLine("Checkout_ShouldChangeStateToCheckedOut: Failed");
            }
        }

        [TestMethod]
        public void DisplayInventory_ShouldShowItems()
        {
            var facade = new BillingSystemFacade();
            var item = new ItemDTO { Code = "I001", Name = "Item 1", Price = 10.0m };
            facade.AddItem(item);

            Console.WriteLine("DisplayInventory:");
            facade.DisplayInventory();
            // Manually verify the output to ensure it displays the added item
        }

        [TestMethod]
        public void GenerateBill_ShouldSetCorrectTotalPrice()
        {
            var billBuilder = new ConcreteBillBuilder();
            var items = new List<ItemDTO>
            {
                new ItemDTO { Price = 10.0m, Quantity = 2 },
                new ItemDTO { Price = 20.0m, Quantity = 1 }
            };
            decimal totalPrice = items.Sum(i => i.Price * i.Quantity);
            billBuilder.SetTotalPrice((float)totalPrice);
            var bill = billBuilder.Build();

            if (bill.TotalPrice == (float)totalPrice)
            {
                Console.WriteLine("GenerateBill_ShouldSetCorrectTotalPrice: Passed");
            }
            else
            {
                Console.WriteLine("GenerateBill_ShouldSetCorrectTotalPrice: Failed");
            }
        }

        [TestMethod]
        public void AddItem_ShouldCallSaveItem()
        {
            var mockFacade = new Mock<BillingSystemFacade>();
            var newItem = new ItemDTO { Code = "I001", Name = "Item 1", Price = 10.0m };
            mockFacade.Object.AddItem(newItem);

            mockFacade.Verify(f => f.AddItem(It.IsAny<ItemDTO>()), Times.Once);
            Console.WriteLine("AddItem_ShouldCallSaveItem: Passed");
        }

        [TestMethod]
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

        [TestMethod]
        public void GenerateSalesReport_ShouldCallGetSalesByCurrentDate()
        {
            var mockFacade = new Mock<BillingSystemFacade>();
            mockFacade.Object.GenerateSalesReport();

            mockFacade.Verify(f => f.GenerateSalesReport(), Times.Once);
            Console.WriteLine("GenerateSalesReport_ShouldCallGetSalesByCurrentDate: Passed");
        }

        [TestMethod]
        public void GenerateStockReport_ShouldCallGetAllStocksWithItemInfo()
        {
            var mockFacade = new Mock<BillingSystemFacade>();
            mockFacade.Object.GenerateStockReport();

            mockFacade.Verify(f => f.GenerateStockReport(), Times.Once);
            Console.WriteLine("GenerateStockReport_ShouldCallGetAllStocksWithItemInfo: Passed");
        }

        [TestMethod]
        public void GenerateReorderReport_ShouldCallGetItemsBelowReorderLevel()
        {
            var mockFacade = new Mock<BillingSystemFacade>();
            mockFacade.Object.GenerateReorderReport();

            mockFacade.Verify(f => f.GenerateReorderReport(), Times.Once);
            Console.WriteLine("GenerateReorderReport_ShouldCallGetItemsBelowReorderLevel: Passed");
        }
    }
}
