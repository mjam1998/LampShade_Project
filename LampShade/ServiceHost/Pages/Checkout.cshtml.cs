using _01_LampShadeQuery.Contracts;
using _01_LampShadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;
using System.Collections.Generic;
using System.Linq;

namespace ServiceHost.Pages
{
    public class CheckoutModel : PageModel
    {
        public Cart Cart;
        public const string CookieName = "cart-items";
        private readonly ICartCalculatorService _cartCalculatorService;
        private readonly ICartService _cartService;
        private readonly IProductQuery _productQuery;

        public CheckoutModel(ICartCalculatorService cartCalculatorService, ICartService cartService,IProductQuery productQuery)
        {
            _cartCalculatorService = cartCalculatorService;
            _cartService = cartService;
            _productQuery = productQuery;
        }

        public void OnGet()
        {
            var serialaizer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];

            var cartItems = serialaizer.Deserialize<List<CartItem>>(value);
            foreach (var item in cartItems)
            {
                item.CalculateTotalItemPrice();
            }
            Cart=_cartCalculatorService.ComputeCart(cartItems);
            //برای اینکه میخواهیم کوکی که برای پرداخت نهایی استفاده میکنیم
            //را تغییر ندهند و در طول استفاده برنامه ثابت بماند  در رجیسترش از  singlton  استفاده کردیم
            _cartService.Set(Cart);
        }
        public IActionResult OnGetPay()
        {
            var cart = _cartService.Get();
            var result=_productQuery.CheckInventoryStatus(cart.Items);
            if (result.Any(x => x.IsInStock == false))
                return RedirectToPage("./Cart");
            return RedirectToPage("./Checkout");
        }
    }
}
