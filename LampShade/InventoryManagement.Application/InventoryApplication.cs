using _0_Freamwork.Application;
using InventoryManagement.Application.Contract.InventoryAppContract;
using InventoryManagement.Domain.InventoryAgg;

using System.Collections.Generic;

namespace InventoryManagement.Application
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IInventoryRepository _inventortRepository;

        public InventoryApplication(IInventoryRepository inventortRepository)
        {
            _inventortRepository = inventortRepository;
        }

        public OperationResult Create(CreateInventory ccommand)
        {
            var operation= new OperationResult();
            if(_inventortRepository.Exists(x=>x.ProductId==ccommand.ProductId))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            var inventory = new Inventory(ccommand.ProductId, ccommand.UnitPrice);
            _inventortRepository.Create(inventory);
            _inventortRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditInventory ccommand)
        {
            var operation = new OperationResult();
            var inventory=_inventortRepository.Get(ccommand.Id);
            if(inventory==null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            if (_inventortRepository.Exists(x => x.ProductId == ccommand.ProductId && x.Id!= ccommand.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            inventory.Edit(ccommand.ProductId, ccommand.UnitPrice);
            _inventortRepository.SaveChanges();
            return operation.Succedded();
        }

        public EditInventory GetDetails(long id)
        {
           return _inventortRepository.GetDetails(id);
        }

        public List<InventoryOperationViewModel> GetOperationLog(long inventoryId)
        {
           return _inventortRepository.GetOperationLog(inventoryId);
        }

        public OperationResult Increase(IncreaseInventory ccommand)
        {
            var operation = new OperationResult();
            var inventory = _inventortRepository.Get(ccommand.InventoryId);
            if (inventory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            const long operatorId = 1;
            inventory.Increase(ccommand.Count, operatorId, ccommand.Description);
            _inventortRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult ReDuce(ReduceInventory ccommand)
        {
            var operation = new OperationResult();
            var inventory = _inventortRepository.Get(ccommand.InventoryId);
            if (inventory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            const long operatorId = 1;
            inventory.Reduce(ccommand.Count, operatorId, ccommand.Description,0);
            _inventortRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult ReDuce(List<ReduceInventory> ccommand)
        {
            var operation = new OperationResult();
            const long operatorId = 1;
            foreach (var item in ccommand)
            {
                var inventory=_inventortRepository.GetBy(item.ProductId);
                inventory.Reduce(item.Count, operatorId, item.Description, item.OrderId);
            }
            _inventortRepository.SaveChanges();
            return operation.Succedded();
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            return _inventortRepository.Search(searchModel);
        }
    }
}
