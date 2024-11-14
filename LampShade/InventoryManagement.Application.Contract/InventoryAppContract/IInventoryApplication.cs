

using _0_Freamwork.Application;
using System.Collections.Generic;

namespace InventoryManagement.Application.Contract.InventoryAppContract
{
    public interface IInventoryApplication
    {
        OperationResult Create(CreateInventory ccommand);
        OperationResult Edit(EditInventory ccommand);
        OperationResult Increase(IncreaseInventory ccommand);
        OperationResult ReDuce(ReduceInventory ccommand);
        OperationResult ReDuce(List<ReduceInventory> ccommand);
        EditInventory GetDetails(long id);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
    }
}
