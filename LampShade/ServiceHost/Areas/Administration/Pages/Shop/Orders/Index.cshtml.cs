using System.Collections.Generic;
using _0_Freamwork.Application;
using AccountManagement.Application.Contract.AccountAppContract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Application.Contracts.ProductCategoryAppContract;

namespace ServiceHost.Areas.Administration.Pages.Shop.Orders
{
    public class IndexModel : PageModel
    {
        public List<OrderViewModel> Orders;
        public OrderSearchModel SearchModel;
        public SelectList Accounts;
        
        private readonly IOrderApplication _orderApplication;
        private readonly IAccountApplication _accountApplication;

        public IndexModel(IOrderApplication orderApplication,IAccountApplication accountApplication)
        {
            _orderApplication = orderApplication;
            _accountApplication = accountApplication;
        }

        public void OnGet(OrderSearchModel searchModel)
        {
            Orders = _orderApplication.Search(searchModel);
            Accounts = new SelectList(_accountApplication.GetAccounts(), "Id", "FullName");
        }
        public IActionResult OnGetConfirm(long id)
        {
            _orderApplication.PaymentSucceeded(id, 0);
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetItems(long id)
        {
            var items=_orderApplication.GetItems(id);
            return Partial("Items",items);
        }


    }
}
