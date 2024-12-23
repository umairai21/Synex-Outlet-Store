using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.DTO;

namespace Assignment.Builders
{
    // Interface for building BillDTO objects
    public interface IBillBuilder
    {
        // Sets the SerialNo of the BillDTO
        void SetSerialNo(string serialNo);

        // Sets the Date of the BillDTO
        void SetDate(DateTime date);

        // Sets the TotalPrice of the BillDTO
        void SetTotalPrice(float totalPrice);

        // Sets the Discount of the BillDTO
        void SetDiscount(float discount);

        // Sets the CashReceived of the BillDTO
        void SetCashReceived(float cashReceived);

        // Sets the Balance of the BillDTO
        void SetBalance(float balance);

        // Builds and returns the constructed BillDTO object
        BillDTO Build();
    }
}
