using Assignment.DTO;
using Assignment.Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Assignment.Gateways
{
    // Gateway class for managing Stock-related database operations
    public class StockGateway
    {
        // Adds stock information to a specific shelf
        public void AddStockToShelf(StockDTO stock, string itemCode, int quantity, string shelfNo)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var command = new SqlCommand("INSERT INTO Stock_Shelf (stock_code, shelf_no, item_code, Quantity_transferred) VALUES (@StockCode, @ShelfNo, @ItemCode, @Quantity)", connection);
            command.Parameters.AddWithValue("@StockCode", stock.StockCode);
            command.Parameters.AddWithValue("@ShelfNo", shelfNo);
            command.Parameters.AddWithValue("@ItemCode", itemCode);
            command.Parameters.AddWithValue("@Quantity", quantity);
            command.ExecuteNonQuery();
        }

        // Retrieves all shelves from the database
        public List<ShelfDTO> GetAllShelves()
        {
            var shelves = new List<ShelfDTO>();
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var command = new SqlCommand("SELECT * FROM Shelf", connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                shelves.Add(new ShelfDTO
                {
                    ShelfNo = reader["shelf_no"].ToString(),
                    StacksNo = Convert.ToInt32(reader["stacks_no"])
                });
            }
            return shelves;
        }

        // Updates stock quantity after purchase
        public void UpdateStockAfterPurchase(string itemCode, int quantity)
        {
            var stocks = GetStockByItemCode(itemCode);
            foreach (var stock in stocks)
            {
                if (stock.QuantityReceived >= quantity)
                {
                    using var connection = DatabaseHelper.GetConnection();
                    connection.Open();
                    using var command = new SqlCommand("UPDATE Stock SET Quantity_Received = Quantity_Received - @Quantity WHERE stock_code = @StockCode", connection);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@StockCode", stock.StockCode);
                    command.ExecuteNonQuery();
                    break;
                }
                else
                {
                    quantity -= stock.QuantityReceived;
                    using var connection = DatabaseHelper.GetConnection();
                    connection.Open();
                    using var command = new SqlCommand("UPDATE Stock SET Quantity_Received = 0 WHERE stock_code = @StockCode", connection);
                    command.Parameters.AddWithValue("@StockCode", stock.StockCode);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Updates item stock quantity after purchase
        public void UpdateItemStockAfterPurchase(string itemCode, int quantity)
        {
            var stocks = GetStockByItemCode(itemCode);
            foreach (var stock in stocks)
            {
                if (stock.QuantityReceived >= quantity)
                {
                    using var connection = DatabaseHelper.GetConnection();
                    connection.Open();
                    using var command = new SqlCommand("UPDATE item_Stock SET quantity = quantity - @Quantity WHERE stock_code = @StockCode AND item_code = @ItemCode", connection);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@StockCode", stock.StockCode);
                    command.Parameters.AddWithValue("@ItemCode", itemCode);
                    command.ExecuteNonQuery();
                    break;
                }
                else
                {
                    quantity -= stock.QuantityReceived;
                    using var connection = DatabaseHelper.GetConnection();
                    connection.Open();
                    using var command = new SqlCommand("UPDATE item_Stock SET quantity = 0 WHERE stock_code = @StockCode AND item_code = @ItemCode", connection);
                    command.Parameters.AddWithValue("@StockCode", stock.StockCode);
                    command.Parameters.AddWithValue("@ItemCode", itemCode);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Retrieves stock information by item code
        public List<StockDTO> GetStockByItemCode(string itemCode)
        {
            var stocks = new List<StockDTO>();
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var command = new SqlCommand("SELECT * FROM Stock WHERE stock_code IN (SELECT stock_code FROM item_Stock WHERE item_code = @ItemCode) ORDER BY expiry_date, stock_code", connection);
            command.Parameters.AddWithValue("@ItemCode", itemCode);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                stocks.Add(new StockDTO
                {
                    StockCode = reader["stock_code"].ToString(),
                    ExpiryDate = Convert.ToDateTime(reader["expiry_date"]),
                    QuantityReceived = Convert.ToInt32(reader["Quantity_Received"])
                });
            }
            return stocks;
        }

        // Retrieves all stocks with item information
        public List<StockWithItemDTO> GetAllStocksWithItemInfo()
        {
            var stocks = new List<StockWithItemDTO>();
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var command = new SqlCommand(
                "SELECT i.item_code, i.item_name, i.item_price, s.stock_code, s.expiry_date, is_t.quantity AS quantity_received, ss.shelf_no " +
                "FROM Item i " +
                "JOIN item_Stock is_t ON i.item_code = is_t.item_code " +
                "JOIN Stock s ON is_t.stock_code = s.stock_code " +
                "JOIN Stock_Shelf ss ON s.stock_code = ss.stock_code", connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                stocks.Add(new StockWithItemDTO
                {
                    ItemCode = reader["item_code"].ToString(),
                    ItemName = reader["item_name"].ToString(),
                    ItemPrice = Convert.ToDecimal(reader["item_price"]),
                    StockCode = reader["stock_code"].ToString(),
                    ExpiryDate = Convert.ToDateTime(reader["expiry_date"]),
                    QuantityReceived = Convert.ToInt32(reader["quantity_received"]),
                    ShelfNo = reader["shelf_no"].ToString()
                });
            }
            return stocks;
        }

        // Retrieves items below reorder level
        public List<StockWithItemDTO> GetItemsBelowReorderLevel(int reorderLevel)
        {
            var items = new List<StockWithItemDTO>();
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var command = new SqlCommand(
                "SELECT i.item_code, i.item_name, SUM(is_t.quantity) AS total_quantity " +
                "FROM Item i " +
                "JOIN item_Stock is_t ON i.item_code = is_t.item_code " +
                "JOIN Stock s ON is_t.stock_code = s.stock_code " +
                "JOIN Stock_Shelf ss ON s.stock_code = ss.stock_code " +
                "GROUP BY i.item_code, i.item_name " +
                "HAVING SUM(is_t.quantity) < @ReorderLevel", connection);
            command.Parameters.AddWithValue("@ReorderLevel", reorderLevel);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                items.Add(new StockWithItemDTO
                {
                    ItemCode = reader["item_code"].ToString(),
                    ItemName = reader["item_name"].ToString(),
                    QuantityReceived = Convert.ToInt32(reader["total_quantity"])
                });
            }
            return items;
        }

        // Retrieves items to be reshelved
        public List<StockWithItemDTO> GetItemsToBeReshelved()
        {
            var items = new List<StockWithItemDTO>();
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var command = new SqlCommand(
                "SELECT i.item_code, i.item_name, s.stock_code, s.expiry_date, ss.Quantity_transferred AS quantity_received, ss.shelf_no " +
                "FROM Item i " +
                "JOIN item_Stock is_t ON i.item_code = is_t.item_code " +
                "JOIN Stock s ON is_t.stock_code = s.stock_code " +
                "JOIN Stock_Shelf ss ON s.stock_code = ss.stock_code " +
                "WHERE ss.Quantity_transferred > 0", connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                items.Add(new StockWithItemDTO
                {
                    ItemCode = reader["item_code"].ToString(),
                    ItemName = reader["item_name"].ToString(),
                    StockCode = reader["stock_code"].ToString(),
                    ExpiryDate = Convert.ToDateTime(reader["expiry_date"]),
                    QuantityReceived = Convert.ToInt32(reader["quantity_received"]),
                    ShelfNo = reader["shelf_no"].ToString()
                });
            }
            return items;
        }

        // Saves stock information to the database
        public void SaveStock(StockDTO stock)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var command = new SqlCommand("INSERT INTO Stock (stock_code, expiry_date, Quantity_Received) VALUES (@StockCode, @ExpiryDate, @QuantityReceived)", connection);
            command.Parameters.AddWithValue("@StockCode", stock.StockCode);
            command.Parameters.AddWithValue("@ExpiryDate", stock.ExpiryDate);
            command.Parameters.AddWithValue("@QuantityReceived", stock.QuantityReceived);
            command.ExecuteNonQuery();
        }

        // Saves item-stock relation to the database
        public void SaveItemStock(string stockCode, string itemCode, int quantity)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var command = new SqlCommand("INSERT INTO item_Stock (stock_code, item_code, quantity) VALUES (@StockCode, @ItemCode, @Quantity)", connection);
            command.Parameters.AddWithValue("@StockCode", stockCode);
            command.Parameters.AddWithValue("@ItemCode", itemCode);
            command.Parameters.AddWithValue("@Quantity", quantity);
            command.ExecuteNonQuery();
        }

        public List<ItemReshelveDTO> GetItemsToBeReshelvedForEndOfDay()
        {
            var items = new List<ItemReshelveDTO>();
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var command = new SqlCommand(
                "SELECT I.item_code, I.item_name, SUM(itemStock.quantity) AS quantity " +
                "FROM Item I " +
                "JOIN item_Stock itemStock ON I.item_code = itemStock.item_code " +
                "GROUP BY I.item_code, I.item_name " +
                "HAVING SUM(itemStock.quantity) > 0", connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                items.Add(new ItemReshelveDTO
                {
                    ItemCode = reader["item_code"].ToString(),
                    ItemName = reader["item_name"].ToString(),
                    Quantity = Convert.ToInt32(reader["quantity"])
                });
            }
            return items;
        }
    }
}
