using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2.Repository
{
    public interface IItemRepository
    {
        bool DoesItemExist(string itemCode);
    }


}
