﻿
using _0_Freamwork.Application;
using _0_Freamwork.Infrastructure;
using AccountManagement.Infrastructure.EFCore;
using InventoryManagement.Application.Contract.InventoryAppContract;
using InventoryManagement.Domain.InventoryAgg;
using ShopManagement.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Infrastructure.EFCore.Repository
{
    public class InventortRepository : RepositoryBase<long, Inventory>, IInventoryRepository
    {
        private readonly AccountContext _accountContext;
        private readonly InventoryContext _inventoryContext;
        private readonly ShopContext _shopContext;

        public InventortRepository(InventoryContext context,ShopContext shopContext,AccountContext accountContext):base(context) 
        {
            _inventoryContext = context;
            _shopContext = shopContext;
            _accountContext = accountContext;
        }

        public Inventory GetBy(long productId)
        {
           return _inventoryContext.Inventory.FirstOrDefault(x => x.ProductId == productId);
        }

        public EditInventory GetDetails(long id)
        {
            return _inventoryContext.Inventory.Select(x=>new EditInventory
            { 
              Id=x.Id,
              ProductId=x.ProductId,
               UnitPrice=x.UnitPrice,
            }).FirstOrDefault(x=>x.Id == id);
        }

        public List<InventoryOperationViewModel> GetOperationLog(long inventoryId)
        {
            var accounts = _accountContext.Accounts.Select(x => new {x.Id,x.FullName}).ToList();    
            var inventory=_inventoryContext.Inventory.FirstOrDefault(x=>x.Id==inventoryId);
            var log= inventory.Operations.Select(x=>new InventoryOperationViewModel
            {
                Id = x.Id,
                Count=x.Count,
                CurrentCount=x.CurrentCount,
                Description=x.Description,
                Operation=x.Operation,
                OperationDate=x.OperationDate.ToFarsi(),
                
                  OperatorId=x.OperatorId,
                   OrderId=x.OrderId
                   
            }).OrderByDescending(x=>x.Id).ToList();

            foreach (var item in log)
            {
                item.Operator=accounts.FirstOrDefault(x=>x.Id==item.OperatorId).FullName;
                
            }

            return log;
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x=>new {x.Id,x.Name}).ToList();
            var query = _inventoryContext.Inventory.Select(x=>new InventoryViewModel
            {
                 Id= x.Id,
                 UnitPrice = x.UnitPrice,
                 InStock=x.InStock,
                 ProductId = x.ProductId,
                 CurrentCount=x.CalculateCurrentCount(),
                 CreationDate=x.CreationDate.ToFarsi()

            });
            if(searchModel.ProductId>0)
                query=query.Where(x=>x.ProductId==searchModel.ProductId);
            if (searchModel.InStock)
                query = query.Where(x =>! x.InStock);
            var inventory=query.OrderByDescending(x=>x.Id).ToList();
            inventory.ForEach(item => item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name);
            return inventory;
        }
    }
}
