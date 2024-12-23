using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.DTO
{
    // Data Transfer Object (DTO) for representing a bill
    public class BillDTO
    {
        // Serial number of the bill
        public string SerialNo { get; set; }

        // Date of the bill
        public DateTime Date { get; set; }

        // Total price of the bill before any discounts
        public float TotalPrice { get; set; }

        // Discount applied to the bill
        public float Discount { get; set; }

        // Cash received from the customer
        public float CashReceived { get; set; }

        // Balance amount to be returned to the customer
        public float Balance { get; set; }
    }
}

