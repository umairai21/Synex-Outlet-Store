using Assignment.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Builders
{
    // Concrete implementation of the IBillBuilder interface for building BillDTO objects
    public class ConcreteBillBuilder : IBillBuilder
    {
        private BillDTO _bill = new BillDTO();

        // Sets the SerialNo of the BillDTO
        public void SetSerialNo(string serialNo)
        {
            _bill.SerialNo = serialNo;
        }

        // Sets the Date of the BillDTO
        public void SetDate(DateTime date)
        {
            _bill.Date = date;
        }

        // Sets the TotalPrice of the BillDTO
        public void SetTotalPrice(float totalPrice)
        {
            _bill.TotalPrice = totalPrice;
        }

        // Sets the Discount of the BillDTO
        public void SetDiscount(float discount)
        {
            _bill.Discount = discount;
        }

        // Sets the CashReceived of the BillDTO
        public void SetCashReceived(float cashReceived)
        {
            _bill.CashReceived = cashReceived;
        }

        // Sets the Balance of the BillDTO
        public void SetBalance(float balance)
        {
            _bill.Balance = balance;
        }

        // Builds and returns the constructed BillDTO object
        public BillDTO Build()
        {
            return _bill;
        }
    }
}
