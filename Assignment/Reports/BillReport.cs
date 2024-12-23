using Assignment.DTO;
using Assignment.Gateways;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Reports
{
    // Concrete implementation of the ReportTemplate for generating bill reports.
    public class BillReport : ReportTemplate
    {
        // Reference to the BillGateway for data access.
        private readonly BillGateway _billGateway;
        // List to store fetched bill data.
        private List<BillDTO> _bills;

        // Constructor that initializes the BillGateway.
        public BillReport(BillGateway billGateway)
        {
            _billGateway = billGateway;
        }

        // Method to fetch data from the database.
        protected override void FetchData()
        {
            _bills = _billGateway.GetAllBills();
        }

        // Method to format the report header.
        protected override void FormatReport()
        {
            Console.WriteLine("Bill Report");
            Console.WriteLine("-----------");
        }

        // Method to print the report details.
        protected override void PrintReport()
        {
            Console.WriteLine("Serial No | Date       | Total Price | Discount | Cash Received | Balance");
            foreach (var bill in _bills)
            {
                Console.WriteLine($"{bill.SerialNo,-9} | {bill.Date.ToShortDateString(),-10} | {bill.TotalPrice,-11} | {bill.Discount,-8} | {bill.CashReceived,-13} | {bill.Balance}");
            }
        }
    }
}
