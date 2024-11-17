

using _0_Freamwork.Domain;
using InventoryManagement.Application.Contract.InventoryAppContract;
using System.Collections.Generic;

namespace InventoryManagement.Domain.InventoryAgg
{
    public interface IInventoryRepository:IRepository<long,Inventory>
    {
        EditInventory GetDetails(long id);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
        Inventory GetBy(long productId);
        List<InventoryOperationViewModel> GetOperationLog(long inventoryId);

    }
}
