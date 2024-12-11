

using _0_Freamwork.Application;
using _01_LampShadeQuery.Contracts;
using DiscountManagement.Infrastructure.EFCore;
using ShopManagement.Application.Contracts.Order;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_LampShadeQuery.Query
{
    public class CartCalculatorService : ICartCalculatorService
    {
        private readonly IAuthHelper _authHelper;
        private readonly DiscountContext _discountContext;

        public CartCalculatorService(DiscountContext discountContext,IAuthHelper authHelper)
        {
            _discountContext = discountContext;
            _authHelper = authHelper;
        }

        public Cart ComputeCart(List<CartItem> cartItems)
        {
            var result= new Cart();
            var colleagueDiscount = _discountContext.ColleagueDiscounts.Where(x=>!x.IsRemoved).Select(x => new { x.DiscountRate,x.ProductId}).ToList();
            var customerDiscount=_discountContext.CustomerDiscounts.Where(x=>x.StartDate< DateTime.Now && x.EndDate>DateTime.Now)
                .Select(x=> new {x.ProductId,x.DiscountRate}).ToList();
            var currentAccountRole = _authHelper.CurrentAccountRole();
            if(currentAccountRole == Roles.Colleague)
            {
                foreach (var item in cartItems)
                {
                    var colleague = colleagueDiscount.FirstOrDefault(x => x.ProductId == item.Id);
                    if (colleague != null) {
                    item.DisCountRate=colleague.DiscountRate;
                    item.DiscountAmount=((item.TotalItemPrice*colleague.DiscountRate)/100);
                    item.ItemPayAmount=item.TotalItemPrice-item.DiscountAmount;
                    result.AddItem(item);
                    }
                    else
                    {
                        item.ItemPayAmount = item.TotalItemPrice - item.DiscountAmount;
                        result.AddItem(item);
                    }
                }

            }
            else
            {
                foreach (var item in cartItems)
                {
                    var customer = customerDiscount.FirstOrDefault(x => x.ProductId == item.Id);
                    if (customer != null) { 
                    item.DisCountRate = customer.DiscountRate;
                    item.DiscountAmount = ((item.TotalItemPrice * customer.DiscountRate) / 100);
                    item.ItemPayAmount = item.TotalItemPrice - item.DiscountAmount;
                    result.AddItem(item);
                    }
                    else
                    {
                        item.ItemPayAmount = item.TotalItemPrice - item.DiscountAmount;
                        result.AddItem(item);
                    }

                }

            }
            return result;
              
        }
    }
}
