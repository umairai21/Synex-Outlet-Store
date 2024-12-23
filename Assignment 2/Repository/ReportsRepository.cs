using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Assignment_2.DTO;
using Assignment_2.ViewModels;
using Microsoft.Data.SqlClient;

namespace Assignment_2.Repository
{
    public class ReportsRepository
    {
        private readonly string _connectionString = "Data Source=UMAIR-CHANGE8\\SQLEXPRESS;Initial Catalog=SynexOutletStore;Integrated Security=True;Encrypt=False;";

        // Method to get Total Sales Report
        public async Task<List<BillItemDTO>> GetTotalSalesReportAsync()
        {
            var billItems = new List<BillItemDTO>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT bill_serial_no, item_code, item_name, quantity, price, total_price FROM bill_item";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            billItems.Add(new BillItemDTO
                            {
                                BillSerialNo = reader["bill_serial_no"].ToString(),
                                ItemCode = reader["item_code"].ToString(),
                                ItemName = reader["item_name"].ToString(),
                                Quantity = Convert.ToInt32(reader["quantity"]),
                                Price = Convert.ToDecimal(reader["price"]),
                                TotalPrice = Convert.ToDecimal(reader["total_price"])
                            });
                        }
                    }
                }
            }

            return billItems;
        }

        // Method to get Reshelve Report
        public async Task<List<StockWithItemDTO>> GetReshelveReportAsync()
        {
            var reshelveItems = new List<StockWithItemDTO>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    SELECT i.item_code, i.item_name, ss.Quantity_transferred AS quantity_received
                    FROM Item i
                    JOIN Item_Stock [is] ON i.item_code = [is].item_code
                    JOIN Stock s ON [is].stock_code = s.stock_code
                    JOIN Stock_Shelf ss ON s.stock_code = ss.stock_code
                    WHERE ss.Quantity_transferred > 0";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            reshelveItems.Add(new StockWithItemDTO
                            {
                                ItemCode = reader["item_code"].ToString(),
                                ItemName = reader["item_name"].ToString(),
                                QuantityReceived = Convert.ToInt32(reader["quantity_received"])
                            });
                        }
                    }
                }
            }

            return reshelveItems;
        }

        // Method to get Reorder Report
        public async Task<List<StockWithItemDTO>> GetReorderReportAsync()
        {
            var reorderItems = new List<StockWithItemDTO>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    SELECT i.item_code, i.item_name, SUM(istock.quantity) AS total_quantity
                    FROM Item i
                    JOIN Item_Stock istock ON i.item_code = istock.item_code
                    JOIN Stock s ON istock.stock_code = s.stock_code
                    JOIN Stock_Shelf ss ON s.stock_code = ss.stock_code
                    GROUP BY i.item_code, i.item_name
                    HAVING SUM(istock.quantity) <= @ReorderLevel";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReorderLevel", 50);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            reorderItems.Add(new StockWithItemDTO
                            {
                                ItemCode = reader["item_code"].ToString(),
                                ItemName = reader["item_name"].ToString(),
                                QuantityReceived = Convert.ToInt32(reader["total_quantity"])
                            });
                        }
                    }
                }
            }

            return reorderItems;
        }

        // Method to get Stock Report
        public async Task<List<StockWithItemDTO>> GetStockReportAsync()
        {
            var stockItems = new List<StockWithItemDTO>();

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
                            stockItems.Add(new StockWithItemDTO
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

            return stockItems;
        }

        // Add a method to fetch all the bill data from the Bill table.
        // In ReportsRepository.cs

        public async Task<List<BillDTO>> GetBillReportAsync()
        {
            List<BillDTO> bills = new List<BillDTO>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT serial_no, date, total_price, discount, cash_received, balance FROM Bill";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            bills.Add(new BillDTO
                            {
                                SerialNo = reader["serial_no"].ToString(),
                                Date = Convert.ToDateTime(reader["date"]),
                                TotalPrice = Convert.ToSingle(reader["total_price"]),
                                Discount = Convert.ToSingle(reader["discount"]),
                                CashReceived = Convert.ToInt32(reader["cash_received"]),
                                Balance = Convert.ToSingle(reader["balance"])
                            });
                        }
                    }
                }
            }

            return bills;
        }
    }
}
