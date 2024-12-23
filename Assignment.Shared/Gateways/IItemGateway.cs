using Assignment.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Gateways
{
    public interface IItemGateway
    {
        void SaveItem(ItemDTO item);
        ItemDTO GetItemByCode(string code);
        List<ItemDTO> GetAllItems();
        void SaveStock(StockDTO stock);
        void SaveItemStock(string stockCode, string itemCode, int quantity);
    }
}
