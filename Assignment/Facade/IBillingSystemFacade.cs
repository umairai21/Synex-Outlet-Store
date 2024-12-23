using Assignment.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Facade
{
    public interface IBillingSystemFacade
    {
        void AddItem(ItemDTO item);
        void DisplayInventory();
        void AddStock(StockDTO stock, string itemCode, int quantity, string shelfNo);
        void GenerateSalesReport();
        void GenerateReshelveReport();
        void GenerateReorderReport();
        void GenerateStockReport();
        void GenerateBillReport();
        void Checkout(List<ItemDTO> purchasedItems, float discount, float cashReceived, out BillDTO bill);
        void GenerateBill(BillDTO bill);
        ItemDTO GetItemByCode(string itemCode);
        List<ShelfDTO> GetShelves();
    }
}
