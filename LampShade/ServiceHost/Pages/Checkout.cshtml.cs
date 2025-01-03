using _0_Framework.Application.Sms;
using _0_Framework.Application.ZarinPal;
using _0_Freamwork.Application;
using _01_LampShadeQuery.Contracts;
using _01_LampShadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;
using System.Collections.Generic;
using System.Linq;

namespace ServiceHost.Pages
{
  [Authorize]
    public class CheckoutModel : PageModel
    {
        public Cart Cart;
        public const string CookieName = "cart-items";
        private readonly ICartCalculatorService _cartCalculatorService;
        private readonly ICartService _cartService;
        private readonly IProductQuery _productQuery;
        private readonly IOrderApplication _orderApplication;
        private readonly IZarinPalFactory _zarinPalFactory;
        private readonly IAuthHelper _authHelper;
     

        public CheckoutModel(ICartCalculatorService cartCalculatorService, ICartService cartService,
            IProductQuery productQuery,IOrderApplication orderApplication,IZarinPalFactory zarinPalFactory,IAuthHelper authHelper)
        {
            _cartCalculatorService = cartCalculatorService;
            _cartService = cartService;
            _productQuery = productQuery;
            _orderApplication = orderApplication;
            _zarinPalFactory = zarinPalFactory;
            _authHelper = authHelper;
           
            Cart=new Cart();
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
        public IActionResult OnPostPay(int paymentMethod)
        {
            var cart = _cartService.Get();
            cart.SetPaymentMethod(paymentMethod);
            var result=_productQuery.CheckInventoryStatus(cart.Items);
            if (result.Any(x => x.IsInStock == false))
                return RedirectToPage("./Cart");
           
            var orderId=_orderApplication.PlaceOrder(cart);
            if(paymentMethod == 1)
            {
                cart.PayAmount = cart.PayAmount * 10;//چون درگاه ریال میگیرد
                var paymentResponse = _zarinPalFactory.CreatePaymentRequest(cart.PayAmount.ToString(), "", "", "خرید از درگاه لوازم خانگی و دکوری ", orderId);

                return Redirect($"https://{_zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Authority}");
            }
            var paymentResult = new PaymentResult();
            Response.Cookies.Delete("cart-items");
            return RedirectToPage("/PaymentResult",
                paymentResult.Succeeded(
                    "سفارش شما با موفقیت ثبت شد. پس از تماس کارشناسان ما و پرداخت وجه، سفارش ارسال خواهد شد.", null));
        }
        public IActionResult OnGetCallBack([FromQuery] string authority, [FromQuery] string status,
            [FromQuery] long oId)
        {
            var orderAmount=_orderApplication.GetAmountBy(oId);
            var amount = orderAmount * 10;
            var verficationResponse = _zarinPalFactory.CreateVerificationRequest(authority, amount.ToString());

            var result = new PaymentResult();
            if(status=="OK" && verficationResponse.Status == 100)
            {
               
                var issuetrackingNo=_orderApplication.PaymentSucceeded(oId,verficationResponse.RefID);
                Response.Cookies.Delete("cart-items");
                return RedirectToPage("./PaymentResult",result.Succeeded("پرداخت با موفقیت انجام شد.",issuetrackingNo));
            }

            return RedirectToPage("./PaymentResult", result.Failed("پرداخت با موفقیت انجام نشد  در صورت کسر از حساب مبلغ تا 24 ساعت اینده به حسابتان بازگردادنده میشود.."));
        }
    }
}
