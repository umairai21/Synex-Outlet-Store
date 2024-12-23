using System;
using Microsoft.Data.SqlClient;

namespace Assignment_2.Repository

{
    public class ItemRepository
    {
        private readonly string _connectionString = "Data Source=UMAIR-CHANGE8\\SQLEXPRESS;Initial Catalog=SynexOutletStore;Integrated Security=True;Encrypt=False;";

        public async Task AddItemAsync(string itemCode, string itemName, decimal itemPrice)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    string query = "INSERT INTO Item (item_code, item_name, item_price) VALUES (@Code, @Name, @Price)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Code", itemCode);
                        command.Parameters.AddWithValue("@Name", itemName);
                        command.Parameters.AddWithValue("@Price", itemPrice);
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        // Retrieve item codes from the Item table
        public async Task<List<string>> GetItemCodesAsync()
        {
            var itemCodes = new List<string>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT item_code FROM Item";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            itemCodes.Add(reader["item_code"].ToString());
                        }
                    }
                }
            }
            return itemCodes;
        }

        // Method to check if the item exists
        public virtual bool DoesItemExist(string itemCode)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Item WHERE item_code = @ItemCode";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ItemCode", itemCode);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        // Method to save or update stock information
        public async Task SaveOrUpdateStockAsync(string stockCode, string itemCode, int quantity, DateTime expiryDate, string shelfNo)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (SqlTransaction transaction = connection.BeginTransaction()) // Start a transaction
                {
                    try
                    {
                        // Check if stock exists
                        if (await DoesItemExist(stockCode, connection, transaction))
                        {
                            // Update existing stock quantity
                            await UpdateStockQuantityAsync(stockCode, quantity, connection, transaction);
                        }
                        else
                        {
                            // Insert new stock entry
                            await InsertNewStockAsync(stockCode, expiryDate, quantity, connection, transaction);
                        }

                        // Update or insert item-stock relationship
                        await SaveOrUpdateItemStockAsync(stockCode, itemCode, quantity, connection, transaction);

                        // Update Stock_Shelf table
                        await SaveOrUpdateStockShelfAsync(stockCode, itemCode, quantity, shelfNo, connection, transaction);

                        await transaction.CommitAsync(); // Commit if all operations succeed
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); // Rollback if any operation fails
                        throw new Exception($"Transaction failed: {ex.Message}");
                    }
                }
            }
        }


        private async Task<bool> DoesItemExist(string stockCode, SqlConnection connection, SqlTransaction transaction)
        {
            string query = "SELECT COUNT(*) FROM Stock WHERE stock_code = @StockCode";
            using (SqlCommand command = new SqlCommand(query, connection, transaction)) // Pass connection and transaction
            {
                command.Parameters.AddWithValue("@StockCode", stockCode);
                int count = (int)await command.ExecuteScalarAsync();
                return count > 0;
            }
        }

        public async Task<decimal> GetItemPriceAsync(string itemCode)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT item_price FROM Item WHERE item_code = @ItemCode";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ItemCode", itemCode);
                    var result = await command.ExecuteScalarAsync();
                    if (result != null)
                    {
                        return Convert.ToDecimal(result); // Ensure it's converted to decimal
                    }
                }
            }
            return 0; // Return 0 if the item code doesn't exist or there is no price
        }



        // Method to update stock after a purchase
        public async Task UpdateStockAfterPurchaseAsync(string itemCode, int quantity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "UPDATE Item_Stock SET quantity = quantity - @Quantity WHERE item_code = @ItemCode";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@ItemCode", itemCode);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<string> GetItemNameByCodeAsync(string itemCode, SqlConnection connection, SqlTransaction transaction)
        {
            string query = "SELECT item_name FROM Item WHERE item_code = @ItemCode";
            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@ItemCode", itemCode);
                object result = await command.ExecuteScalarAsync();
                return result?.ToString() ?? "Unknown Item"; // Return "Unknown Item" if not found
            }
        }



        // Method to insert new stock
        private async Task InsertNewStockAsync(string stockCode, DateTime expiryDate, int quantity, SqlConnection connection, SqlTransaction transaction)
        {
            string query = "INSERT INTO Stock (stock_code, expiry_date, Quantity_Received) VALUES (@StockCode, @ExpiryDate, @Quantity)";
            using (SqlCommand command = new SqlCommand(query, connection, transaction)) // Pass transaction object
            {
                command.Parameters.AddWithValue("@StockCode", stockCode);
                command.Parameters.AddWithValue("@ExpiryDate", expiryDate);
                command.Parameters.AddWithValue("@Quantity", quantity);
                await command.ExecuteNonQueryAsync();
            }
        }

        // Method to update stock quantity
        private async Task UpdateStockQuantityAsync(string stockCode, int quantity, SqlConnection connection, SqlTransaction transaction)
        {
            string query = "UPDATE Stock SET Quantity_Received = Quantity_Received + @Quantity WHERE stock_code = @StockCode";
            using (SqlCommand command = new SqlCommand(query, connection, transaction)) // Pass transaction object
            {
                command.Parameters.AddWithValue("@Quantity", quantity);
                command.Parameters.AddWithValue("@StockCode", stockCode);
                await command.ExecuteNonQueryAsync();
            }
        }

        // Method to update or insert item-stock relationship
        private async Task SaveOrUpdateItemStockAsync(string stockCode, string itemCode, int quantity, SqlConnection connection, SqlTransaction transaction)
        {
            string query = @"
    IF EXISTS (SELECT * FROM item_Stock WHERE stock_code = @StockCode AND item_code = @ItemCode)
    BEGIN
        UPDATE item_Stock SET quantity = quantity + @Quantity WHERE stock_code = @StockCode AND item_code = @ItemCode
    END
    ELSE
    BEGIN
        INSERT INTO item_Stock (stock_code, item_code, quantity) VALUES (@StockCode, @ItemCode, @Quantity)
    END";
            using (SqlCommand command = new SqlCommand(query, connection, transaction)) // Pass transaction object
            {
                command.Parameters.AddWithValue("@StockCode", stockCode);
                command.Parameters.AddWithValue("@ItemCode", itemCode);
                command.Parameters.AddWithValue("@Quantity", quantity);
                await command.ExecuteNonQueryAsync();
            }
        }

        // Method to update Stock_Shelf table
        private async Task SaveOrUpdateStockShelfAsync(string stockCode, string itemCode, int quantity, string shelfNo, SqlConnection connection, SqlTransaction transaction)
        {
            string query = @"
    IF EXISTS (SELECT * FROM Stock_Shelf WHERE stock_code = @StockCode AND shelf_no = @ShelfNo AND item_code = @ItemCode)
    BEGIN
        UPDATE Stock_Shelf SET Quantity_transferred = Quantity_transferred + @Quantity WHERE stock_code = @StockCode AND shelf_no = @ShelfNo AND item_code = @ItemCode
    END
    ELSE
    BEGIN
        INSERT INTO Stock_Shelf (stock_code, shelf_no, item_code, Quantity_transferred) VALUES (@StockCode, @ShelfNo, @ItemCode, @Quantity)
    END";
            using (SqlCommand command = new SqlCommand(query, connection, transaction)) // Pass transaction object
            {
                command.Parameters.AddWithValue("@StockCode", stockCode);
                command.Parameters.AddWithValue("@ShelfNo", shelfNo);
                command.Parameters.AddWithValue("@ItemCode", itemCode);
                command.Parameters.AddWithValue("@Quantity", quantity);
                await command.ExecuteNonQueryAsync();
            }
        }

        // Get all stock data along with item information
        public async Task<List<StockWithItemDTO>> GetAllStocksWithItemInfoAsync()
        {
            List<StockWithItemDTO> stocks = new List<StockWithItemDTO>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"
                SELECT i.item_code, i.item_name, i.item_price, s.stock_code, s.expiry_date, istock.quantity AS quantity_received, ss.shelf_no
                FROM Item i
                JOIN Item_Stock istock ON i.item_code = istock.item_code
                JOIN Stock s ON istock.stock_code = s.stock_code
                JOIN Stock_Shelf ss ON s.stock_code = ss.stock_code";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
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
                    }
                }
            }

            return stocks;
        }

        public async Task<bool> StockExistsAsync(string stockCode)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT COUNT(*) FROM Stock WHERE stock_code = @StockCode";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StockCode", stockCode);
                    int count = (int)await command.ExecuteScalarAsync();
                    return count > 0;
                }
            }
        }
    }




    // DTO class to represent stock data
    public class StockWithItemDTO
    {
        public string StockCode { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public decimal ItemPrice { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int QuantityReceived { get; set; }
        public string ShelfNo { get; set; }
    }
}
