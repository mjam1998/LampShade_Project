using _01_LampShadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace ServiceHost.Pages
{
    public class CartModel : PageModel
    {
        public List<CartItem> CartItems;
        public const string CookieName = "cart-items";

        private readonly IProductQuery _productQuery;

        public CartModel(IProductQuery productQuery)
        {
            CartItems = new List<CartItem>();
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

            CartItems=_productQuery.CheckInventoryStatus(cartItems);

        }

        public IActionResult OnGetRemoveFromCart(long id)
        {
            var serialaize = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];

            Response.Cookies.Delete(CookieName);
            var cartItems = serialaize.Deserialize<List<CartItem>>(value);
            var itemRemove = cartItems.FirstOrDefault(x => x.Id == id);
            cartItems.Remove(itemRemove);
            var cokkieOption = new CookieOptions { Expires = DateTime.Now.AddDays(2) };

            Response.Cookies.Append(CookieName, serialaize.Serialize(cartItems), cokkieOption);
            return RedirectToPage("./Cart");

        }
        public IActionResult OnGetGoToCheckout()
        {
            var serialaizer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];

            var cartItems = serialaizer.Deserialize<List<CartItem>>(value);
            foreach (var item in cartItems)
            {
                item.TotalItemPrice = item.UnitPrice * item.Count;
            }

            CartItems = _productQuery.CheckInventoryStatus(cartItems);

            if (CartItems.Any(x => !x.IsInStock))
                return RedirectToPage("./Cart");

            return RedirectToPage("./Checkout");



        }
    }
    //public class CartModel : PageModel
    //{
    //    public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    //    public const string CookieName = "cart-items";

    //    public void OnGet()
    //    {
    //        var serializer = new JavaScriptSerializer();
    //        var value = Request.Cookies[CookieName];

    //        if (!string.IsNullOrEmpty(value))
    //        {
    //            CartItems = serializer.Deserialize<List<CartItem>>(value);
    //            foreach (var item in CartItems)
    //            {
    //                item.TotalItemPrice = item.UnitPrice * item.Count;
    //            }
    //        }
    //    }

    //    public IActionResult OnGetRemoveFromCart(long id)
    //    {
    //        var serializer = new JavaScriptSerializer();
    //        var value = Request.Cookies[CookieName];

    //        List<CartItem> cartItems = new List<CartItem>();

    //        if (!string.IsNullOrEmpty(value))
    //        {
    //            cartItems = serializer.Deserialize<List<CartItem>>(value);
    //        }

    //        var itemRemove = cartItems.FirstOrDefault(x => x.Id == id);

    //        if (itemRemove != null)
    //        {
    //            cartItems.Remove(itemRemove);
    //        }

    //        var cookieOption = new CookieOptions { Expires = DateTime.Now.AddDays(2) };

    //        Response.Cookies.Delete(CookieName);
    //        Response.Cookies.Append(CookieName,"", new CookieOptions { Expires=DateTime.Now.AddDays(-1)});
    //        Response.Cookies.Append(CookieName, serializer.Serialize(cartItems), cookieOption);

    //        return RedirectToPage("./Cart");
    //    }
    //}

}
