using Assignment.DTO;
using Assignment.Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Assignment.Gateways
{
    public class BillGateway
    {
        public void SaveBill(BillDTO bill)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var command = new SqlCommand("INSERT INTO Bill (serial_no, date, total_price, discount, cash_received, balance) VALUES (@SerialNo, @Date, @TotalPrice, @Discount, @CashReceived, @Balance)", connection);
            command.Parameters.AddWithValue("@SerialNo", bill.SerialNo);
            command.Parameters.AddWithValue("@Date", bill.Date);
            command.Parameters.AddWithValue("@TotalPrice", bill.TotalPrice);
            command.Parameters.AddWithValue("@Discount", bill.Discount);
            command.Parameters.AddWithValue("@CashReceived", bill.CashReceived);
            command.Parameters.AddWithValue("@Balance", bill.Balance);
            command.ExecuteNonQuery();
        }

        public List<BillDTO> GetBillsByCurrentDate()
        {
            var bills = new List<BillDTO>();
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var command = new SqlCommand(
                "SELECT * FROM Bill WHERE CAST(date AS DATE) = CAST(GETDATE() AS DATE)", connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                bills.Add(new BillDTO
                {
                    SerialNo = reader["serial_no"].ToString(),
                    Date = Convert.ToDateTime(reader["date"]),
                    TotalPrice = Convert.ToSingle(reader["total_price"]),
                    Discount = Convert.ToSingle(reader["discount"]),
                    CashReceived = Convert.ToSingle(reader["cash_received"]),
                    Balance = Convert.ToSingle(reader["balance"])
                });
            }
            return bills;
        }

        public List<SaleDTO> GetBillDetails(string serialNo)
        {
            var sales = new List<SaleDTO>();
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var command = new SqlCommand(
                "SELECT I.item_code, I.item_name, IST.quantity, I.item_price * IST.quantity AS revenue " +
                "FROM Item I " +
                "JOIN item_Stock IST ON I.item_code = IST.item_code " +
                "JOIN Stock S ON IST.stock_code = S.stock_code " +
                "JOIN Bill B ON S.stock_code = B.serial_no " +
                "WHERE B.serial_no = @SerialNo", connection);
            command.Parameters.AddWithValue("@SerialNo", serialNo);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                sales.Add(new SaleDTO
                {
                    ItemCode = reader["item_code"].ToString(),
                    ItemName = reader["item_name"].ToString(),
                    Quantity = Convert.ToInt32(reader["quantity"]),
                    Revenue = Convert.ToDecimal(reader["revenue"])
                });
            }
            return sales;
        }



        public List<BillDTO> GetAllBills()
        {
            var bills = new List<BillDTO>();
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var command = new SqlCommand("SELECT * FROM Bill", connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                bills.Add(new BillDTO
                {
                    SerialNo = reader["serial_no"].ToString(),
                    Date = Convert.ToDateTime(reader["date"]),
                    TotalPrice = Convert.ToSingle(reader["total_price"]),
                    Discount = Convert.ToSingle(reader["discount"]),
                    CashReceived = Convert.ToSingle(reader["cash_received"]),
                    Balance = Convert.ToSingle(reader["balance"])
                });
            }
            return bills;
        }

        public void SaveBillItem(string billSerialNo, string itemCode, string itemName, int quantity, decimal price, decimal totalPrice)
        {
            using var connection = DatabaseHelper.GetConnection();
            connection.Open();
            using var command = new SqlCommand(
                "INSERT INTO bill_item (bill_serial_no, item_code, item_name, quantity, price, total_price) VALUES (@BillSerialNo, @ItemCode, @ItemName, @Quantity, @Price, @TotalPrice)",
                connection);
            command.Parameters.AddWithValue("@BillSerialNo", billSerialNo);
            command.Parameters.AddWithValue("@ItemCode", itemCode);
            command.Parameters.AddWithValue("@ItemName", itemName);
            command.Parameters.AddWithValue("@Quantity", quantity);
            command.Parameters.AddWithValue("@Price", price);
            command.Parameters.AddWithValue("@TotalPrice", totalPrice);
            command.ExecuteNonQuery();
        }

    }
}
