using Assignment_2.DTO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Threading.Tasks;
using Assignment_2.Repository;

namespace Assignment_2.Services
{
    public class CheckoutService
    {
        private readonly ItemRepository _itemRepository;
        private readonly string _connectionString = "Data Source=UMAIR-CHANGE8\\SQLEXPRESS;Initial Catalog=SynexOutletStore;Integrated Security=True;Encrypt=False;";


        public CheckoutService(ItemRepository itemRepository)
        {
            _itemRepository = new ItemRepository();
        }

        public async Task<List<string>> GetItemCodesAsync()
        {
            return await _itemRepository.GetItemCodesAsync();  // Returns a list of item codes
        }

        public async Task<decimal> CalculateTotalAmountAsync(string itemCode, int quantity, decimal discount)
        {
            decimal pricePerItem = await _itemRepository.GetItemPriceAsync(itemCode);

            // Use decimal arithmetic to ensure proper precision
            decimal totalAmount = (pricePerItem * quantity) - discount;

            return totalAmount;
        }


        public void ShowBillPopUp(decimal totalAfterDiscount, decimal discount, decimal cashReceived)
        {
            decimal balance = cashReceived - totalAfterDiscount;
            string billMessage = $"--- Bill Summary ---\n" +
                                 $"Total Amount (after discount): {totalAfterDiscount:C}\n" +
                                 $"Discount Applied: {discount:C}\n" +
                                 $"Cash Received: {cashReceived:C}\n" +
                                 $"Balance: {balance:C}";

            System.Windows.MessageBox.Show(billMessage, "Bill Details", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public async Task ProcessPurchaseAsync(string itemCode, int quantityPurchased)
        {
            // Logic to update stock and handle purchase transactions
            await _itemRepository.UpdateStockAfterPurchaseAsync(itemCode, quantityPurchased);
        }
        // Method to save the bill and bill items in the database
        public async Task SaveBillAndBillItemsAsync(string serialNo, DateTime date, decimal totalPrice, decimal discount, decimal cashReceived, decimal balance, List<BillItemDTO> billItems)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Save the bill in the Bill table
                        await SaveBillAsync(connection, transaction, serialNo, date, totalPrice, discount, cashReceived, balance);

                        // Save each bill item in the BillItem table
                        foreach (var item in billItems)
                        {
                            // Retrieve the actual item name from the database before saving
                            string itemName = await _itemRepository.GetItemNameByCodeAsync(item.ItemCode, connection, transaction);
                            item.ItemName = itemName;

                            await SaveBillItemAsync(connection, transaction, serialNo, item);
                        }

                        await transaction.CommitAsync(); // Commit transaction if all updates succeed
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();   // Rollback in case of any failure
                        throw new Exception($"Error saving bill: {ex.Message}");
                    }
                }
            }
        }

        public async Task SaveBillAsync(SqlConnection connection, SqlTransaction transaction, string serialNo, DateTime date, decimal totalPrice, decimal discount, decimal cashReceived, decimal balance)
        {
            string query = "INSERT INTO Bill (serial_no, date, total_price, discount, cash_received, balance) VALUES (@SerialNo, @Date, @TotalPrice, @Discount, @CashReceived, @Balance)";
            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@SerialNo", serialNo);
                command.Parameters.AddWithValue("@Date", date);
                command.Parameters.AddWithValue("@TotalPrice", totalPrice);
                command.Parameters.AddWithValue("@Discount", discount);
                command.Parameters.AddWithValue("@CashReceived", cashReceived);
                command.Parameters.AddWithValue("@Balance", balance);
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task SaveBillItemAsync(SqlConnection connection, SqlTransaction transaction, string serialNo, BillItemDTO billItem)
        {
            string query = "INSERT INTO bill_item (bill_serial_no, item_code, item_name, quantity, price, total_price) VALUES (@SerialNo, @ItemCode, @ItemName, @Quantity, @Price, @TotalPrice)";
            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@SerialNo", serialNo);
                command.Parameters.AddWithValue("@ItemCode", billItem.ItemCode);
                command.Parameters.AddWithValue("@ItemName", billItem.ItemName);
                command.Parameters.AddWithValue("@Quantity", billItem.Quantity);
                command.Parameters.AddWithValue("@Price", billItem.Price);
                command.Parameters.AddWithValue("@TotalPrice", billItem.TotalPrice);
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
