

using ShopManagement.Domain.OrderAgg;
using System.Collections.Generic;

namespace ShopManagement.Domain.Service
{
    public interface IShopInventoryAcl
    {
        bool ReduceFromInventory(List<OrderItem> items);
    }
}
