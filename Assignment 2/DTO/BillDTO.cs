using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2.DTO
{
    public class BillDTO
    {
        public string SerialNo { get; set; }
        public DateTime Date { get; set; }
        public float TotalPrice { get; set; }
        public float Discount { get; set; }
        public int CashReceived { get; set; }
        public float Balance { get; set; }
    }
}
