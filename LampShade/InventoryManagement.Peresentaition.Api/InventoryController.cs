using _01_LampShadeQuery.Contracts.Inventory;
using InventoryManagement.Application.Contract.InventoryAppContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace InventoryManagement.Peresentaition.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryQuery _inventoryQuery;
        private readonly IInventoryApplication _inventoryApplication;

        public InventoryController(IInventoryApplication inventoryApplication,IInventoryQuery inventoryQuery)
        {
            _inventoryApplication = inventoryApplication;
            _inventoryQuery = inventoryQuery;
        }

        [HttpGet("{id}")]
        public List<InventoryOperationViewModel> GetOperationsBy(long id)
        {
            return _inventoryApplication.GetOperationLog(id);
        }
        [HttpPost]
        public StockStatus CheckStock(IsInStock command)
        {
            return _inventoryQuery.CheckStock(command);
        }
    }
}
