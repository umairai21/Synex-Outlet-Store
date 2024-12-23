using Assignment.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Builders
{
    public class BillBuilder
    {
        private readonly BillDTO _bill = new BillDTO();

        // Sets the SerialNo of the BillDTO
        public BillBuilder SetSerialNo(string serialNo)
        {
            _bill.SerialNo = serialNo;
            return this;
        }

        // Sets the Date of the BillDTO
        public BillBuilder SetDate(DateTime date)
        {
            _bill.Date = date;
            return this;
        }

        // Sets the TotalPrice of the BillDTO
        public BillBuilder SetTotalPrice(float totalPrice)
        {
            _bill.TotalPrice = totalPrice;
            return this;
        }

        // Sets the Discount of the BillDTO
        public BillBuilder SetDiscount(float discount)
        {
            _bill.Discount = discount;
            return this;
        }

        // Sets the CashReceived of the BillDTO
        public BillBuilder SetCashReceived(float cashReceived)
        {
            _bill.CashReceived = cashReceived;
            return this;
        }

        // Sets the Balance of the BillDTO
        public BillBuilder SetBalance(float balance)
        {
            _bill.Balance = balance;
            return this;
        }

        // Builds and returns the constructed BillDTO object
        public BillDTO Build()
        {
            return _bill;
        }
    }
}
