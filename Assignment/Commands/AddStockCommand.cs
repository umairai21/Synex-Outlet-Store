using Assignment.DTO;
using Assignment.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Commands
{
    // Concrete command to add stock
    public class AddStockCommand : IUndoableCommand
    {
        private readonly StockDTO _stock;
        private readonly string _itemCode;
        private readonly int _quantity;
        private readonly string _shelfNo;
        private readonly BillingSystemFacade _facade;

        // Constructor to initialize the AddStockCommand
        public AddStockCommand(StockDTO stock, string itemCode, int quantity, string shelfNo, BillingSystemFacade facade)
        {
            _stock = stock;
            _itemCode = itemCode;
            _quantity = quantity;
            _shelfNo = shelfNo;
            _facade = facade;
        }

        // Execute method to add the stock
        public void Execute()
        {
            _facade.AddStock(_stock, _itemCode, _quantity, _shelfNo);
        }

        // Undo method to undo the stock addition
        public void Undo()
        {
            // Implement logic to undo the stock addition
            // For example, removing the stock from the inventory
        }
    }
}
