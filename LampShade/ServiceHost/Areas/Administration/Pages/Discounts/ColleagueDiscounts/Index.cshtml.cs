using System.Collections.Generic;
using DiscountManagement.Application.Contract.ColleagueDiscountAppContract;
using DiscountManagement.Application.Contract.CustomerDiscountAppContract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductAppContract;



namespace ServiceHost.Areas.Administration.Pages.Discounts.ColleagueDiscounts
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<ColleagueDiscountViewModel> ColleagueDiscounts;
        public ColleagueDiscountSearchModel SearchModel;
        public SelectList Products;
        private readonly IProductApplication _productApplication;
        private readonly IColleagueDiscountApplication _colleagueDiscountApplication;

        public IndexModel(IProductApplication productApplication, IColleagueDiscountApplication colleagueDiscountApplication)
        {
            _productApplication = productApplication;
            _colleagueDiscountApplication = colleagueDiscountApplication;
        }


        public void OnGet(ColleagueDiscountSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(),"Id","Name");
            ColleagueDiscounts = _colleagueDiscountApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new DefineColleagueDiscount();
            command.Products = _productApplication.GetProducts();
            return Partial("./Create",command);
        }

        public JsonResult OnPostCreate(DefineColleagueDiscount command)
        {
            var result = _colleagueDiscountApplication.Define(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var editColleagueDiscount = _colleagueDiscountApplication.GetDetails(id);
            editColleagueDiscount.Products=_productApplication.GetProducts();
            return Partial("Edit", editColleagueDiscount);
        }

        public JsonResult OnPostEdit(EditColleagueDiscount command)
        {
            var result = _colleagueDiscountApplication.Edit(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetRemove(long id)
        {
            var result = _colleagueDiscountApplication.Remove(id);
            if (result.IsSuccedded)
                return RedirectToPage("./Index");
            Message = result.Message;
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetReStore(long id)
        {
            var result = _colleagueDiscountApplication.ReStore(id);
            if (result.IsSuccedded)
                return RedirectToPage("./Index");
            Message = result.Message;
            return RedirectToPage("./Index");
        }
      
    }
}
