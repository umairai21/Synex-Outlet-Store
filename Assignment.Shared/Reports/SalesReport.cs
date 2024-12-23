using Assignment.DTO;
using Assignment.Gateways;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment.Reports
{
    public class SalesReport : ReportTemplate
    {
        private readonly BillGateway _billGateway;
        private List<SaleDTO> _sales;

        public SalesReport(BillGateway billGateway)
        {
            _billGateway = billGateway;
        }

        protected override void FetchData()
        {
            var bills = _billGateway.GetBillsByCurrentDate();
            _sales = new List<SaleDTO>();

            foreach (var bill in bills)
            {
                var billDetails = _billGateway.GetBillDetails(bill.SerialNo);
                _sales.AddRange(billDetails);
            }
        }

        protected override void FormatReport()
        {
            Console.WriteLine("Sales Report");
            Console.WriteLine("------------");
        }

        protected override void PrintReport()
        {
            foreach (var sale in _sales)
            {
                Console.WriteLine($"{sale.ItemCode} - {sale.ItemName} - {sale.Quantity} - {sale.Revenue}");
            }

            decimal totalRevenue = _sales.Sum(s => s.Revenue);
            Console.WriteLine($"Total Revenue for Today: {totalRevenue}");
        }
    }
}
