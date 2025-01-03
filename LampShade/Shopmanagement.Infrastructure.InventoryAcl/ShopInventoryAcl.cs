

using InventoryManagement.Application.Contract.InventoryAppContract;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Service;
using System.Collections.Generic;

namespace Shopmanagement.Infrastructure.InventoryAcl
{
    public class ShopInventoryAcl : IShopInventoryAcl
    {
        private readonly IInventoryApplication _inventoryApplication;

        public ShopInventoryAcl(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }

        public bool ReduceFromInventory(List<OrderItem> items)
        {
            var command = new List<ReduceInventory>();
            foreach (var orderItem in items)
            {
                var item = new ReduceInventory(orderItem.ProductId, orderItem.Count, "خرید مشتری", orderItem.OredrId);
                command.Add(item);
            }

            return _inventoryApplication.ReDuce(command).IsSuccedded;
        }
    }
}
