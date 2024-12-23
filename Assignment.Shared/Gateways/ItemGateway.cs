using Assignment.DTO;
using Assignment.Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Assignment.Gateways
{
    public class ItemGateway : IItemGateway
    {
        public void SaveItem(ItemDTO item)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var command = new SqlCommand("INSERT INTO Item (item_code, item_name, item_price) VALUES (@Code, @Name, @Price)", connection);
            command.Parameters.AddWithValue("@Code", item.Code);
            command.Parameters.AddWithValue("@Name", item.Name);
            command.Parameters.AddWithValue("@Price", item.Price);
            command.ExecuteNonQuery();
        }

        public ItemDTO GetItemByCode(string code)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var command = new SqlCommand("SELECT * FROM Item WHERE item_code = @Code", connection);
            command.Parameters.AddWithValue("@Code", code);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new ItemDTO
                {
                    Code = reader["item_code"].ToString(),
                    Name = reader["item_name"].ToString(),
                    Price = Convert.ToDecimal(reader["item_price"])
                };
            }
            return null;
        }

        public List<ItemDTO> GetAllItems()
        {
            var items = new List<ItemDTO>();
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var command = new SqlCommand("SELECT * FROM Item", connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                items.Add(new ItemDTO
                {
                    Code = reader["item_code"].ToString(),
                    Name = reader["item_name"].ToString(),
                    Price = Convert.ToDecimal(reader["item_price"])
                });
            }
            return items;
        }

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
    }
}
