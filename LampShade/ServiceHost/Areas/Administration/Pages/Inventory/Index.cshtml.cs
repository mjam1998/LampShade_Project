using System.Collections.Generic;
using InventoryManagement.Application.Contract.InventoryAppContract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductAppContract;



namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<InventoryViewModel> Inventory;
        public InventorySearchModel SearchModel;
        public SelectList Products;
        private readonly IProductApplication _productApplication;
        private readonly IInventoryApplication _inventoryApplication;

        public IndexModel(IProductApplication productApplication, IInventoryApplication inventoryApplication)
        {
            _productApplication = productApplication;
            _inventoryApplication = inventoryApplication;
        }


        public void OnGet(InventorySearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(),"Id","Name");
            Inventory = _inventoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateInventory();
            command.Products = _productApplication.GetProducts();
            return Partial("./Create",command);
        }

        public JsonResult OnPostCreate(CreateInventory command)
        {
            var result = _inventoryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var editInventory = _inventoryApplication.GetDetails(id);
            editInventory.Products=_productApplication.GetProducts();
            return Partial("Edit", editInventory);
        }

        public JsonResult OnPostEdit(EditInventory command)
        {
            var result = _inventoryApplication.Edit(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetIncrease(long id)
        {
            var command = new IncreaseInventory()
            {
                InventoryId = id

            };
            return Partial("Increase", command);
        }
        public JsonResult OnPostIncrease(IncreaseInventory command)
        {
            var result= _inventoryApplication.Increase(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetReduce(long id)
        {
            var command = new ReduceInventory()
            {
                InventoryId = id

            };
            return Partial("Reduce", command);
        }
        public JsonResult OnPostReduce(ReduceInventory command)
        {
            var result = _inventoryApplication.ReDuce(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetOperationLog(long id)
        {
            var logs=_inventoryApplication.GetOperationLog(id);
            return Partial("OperationLog",logs);
        }

    }
}
