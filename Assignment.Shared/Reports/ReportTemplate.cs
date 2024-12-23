using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Reports
{
    public abstract class ReportTemplate
    {
        // Template method to generate a report.
        // It defines the steps for generating a report, with each step being an abstract method to be implemented by subclasses.
       public void GenerateReport()
        {
            FetchData();
            FormatReport();
            PrintReport();
        }

        // Abstract method to fetch data
        protected abstract void FetchData();

        // Abstract method to format the report
        protected abstract void FormatReport();

        // Abstract method to print the report
        protected abstract void PrintReport();
    }
}
