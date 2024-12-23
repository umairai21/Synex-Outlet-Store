using Assignment_2.DTO;
using Assignment_2.Repository;
using Assignment_2.Services;
using Moq;
using System.Net.Sockets;

namespace SYOSGUITests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task AddItemAsync_Should_AddNewItem_ToRepository()
        {
            // Arrange
            var repository = new ItemRepository();
            var itemCode = "it06";
            var itemName = "Test Item";
            var itemPrice = 10.0M;

            // Act
            await repository.AddItemAsync(itemCode, itemName, itemPrice);

            // Assert
            var items = await repository.GetItemCodesAsync();
            Assert.IsTrue(items.Contains(itemCode), "Item should be added to the repository.");
        }

        [TestMethod]
        public async Task ProcessPurchaseAsync_Should_UpdateStock_AfterPurchase()
        {
            // Arrange
            var mockRepository = new Mock<ItemRepository>();
            var service = new CheckoutService(mockRepository.Object);
            var itemCode = "it06";
            var quantity = 2;

            mockRepository.Setup(repo => repo.UpdateStockAfterPurchaseAsync(itemCode, quantity))
                          .Returns(Task.CompletedTask);

            // Act
            await service.ProcessPurchaseAsync(itemCode, quantity);

            // Assert
            mockRepository.Verify(repo => repo.UpdateStockAfterPurchaseAsync(itemCode, quantity), Times.Once);
        }

        [TestMethod]
        public async Task CalculateTotalAmountAsync_Should_CalculateCorrectTotal()
        {
            // Arrange
            var repository = new ItemRepository();
            var service = new CheckoutService(repository);
            var itemCode = "it06";
            var quantity = 3;
            var discount = 5.0M;

            // Act
            var total = await service.CalculateTotalAmountAsync(itemCode, quantity, discount);

            // Assert
            Assert.AreEqual(25.0M, total); // Assuming price is 10.0M per item
        }

        [TestMethod]
        public void DoesItemExist_Should_ReturnTrue_IfItemExists()
        {
            // Arrange
            var mockRepository = new Mock<IItemRepository>();
            var itemCode = "it06";

            mockRepository.Setup(repo => repo.DoesItemExist(itemCode)).Returns(true);

            // Act
            var result = mockRepository.Object.DoesItemExist(itemCode);

            // Assert
            Assert.IsTrue(result, "Item should exist in the repository.");
        }


        [TestMethod]
        public async Task StockExists_Should_ReturnTrue_WhenStockExists()
        {
            // Arrange
            var repository = new ItemRepository();
            var stockCode = "S01";

            // Act
            var exists = await repository.StockExistsAsync(stockCode);

            // Assert
            Assert.IsTrue(exists, "Stock should exist in the database.");
        }


        [TestMethod]
        public async Task GetItemPriceAsync_Should_ReturnCorrectPrice()
        {
            // Arrange
            var repository = new ItemRepository();
            var itemCode = "it06";

            // Act
            var price = await repository.GetItemPriceAsync(itemCode);

            // Assert
            Assert.AreEqual(10.0M, price); // Assuming the price is 10.0M for the item
        }

    }
}