﻿using _01_LampShadeQuery.Contracts.Inventory;
using InventoryManagement.Application.Contract.InventoryAppContract;
using InventoryManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure;
using System;
using System.Linq;


namespace _01_LampShadeQuery.Query
{
    public class InventoryQuery : IInventoryQuery
    {
        private readonly InventoryContext _inventoryContext; 
        private readonly ShopContext _shopContext;

        public InventoryQuery(InventoryContext inventoryContext, ShopContext shopContext)
        {
            _inventoryContext = inventoryContext;
            _shopContext = shopContext;
        }

        public StockStatus CheckStock(IsInStock command)
        {
            var inventory=_inventoryContext.Inventory.FirstOrDefault(x=>x.ProductId==command.ProductId);
            if(inventory==null || inventory.CalculateCurrentCount() < command.Count)
            {
                var product = _shopContext.Products.Select(x => new {x.Id,x.Name}).FirstOrDefault(x=>x.Id==command.ProductId);
                return new StockStatus
                {
                     IsStock=false,
                      ProductName=product?.Name
                };
            }
            return new StockStatus
            {
                IsStock = true
            };
        }
    }
}
