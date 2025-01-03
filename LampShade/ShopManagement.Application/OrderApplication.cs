
using _0_Framework.Application;
using _0_Framework.Application.Sms;
using _0_Freamwork.Application;
using Microsoft.Extensions.Configuration;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Service;
using System.Collections.Generic;

namespace ShopManagement.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IShopAccountAcl _shopAccountAcl;
        private readonly ISmsService _smsService;
        private readonly IOrderRepository _orderRepository;
        private readonly IAuthHelper _authHelper;
        private readonly IShopInventoryAcl _shopInventoryAcl;
        //میتوانستیم از appsetting  بخوانیم
       // private readonly IConfiguration _configuration;

        public OrderApplication(IOrderRepository orderRepository,IAuthHelper authHelper,IShopInventoryAcl shopInventoryAcl,ISmsService smsService,IShopAccountAcl shopAccountAcl)
        {
            _orderRepository = orderRepository;
            _authHelper = authHelper;
            _shopInventoryAcl = shopInventoryAcl;
            _smsService = smsService;
            _shopAccountAcl = shopAccountAcl;
           // _configuration = configuration;
        }

        public double GetAmountBy(long id)
        {
            return _orderRepository.GetAmountBy(id);
        }

        public List<OrderItemViewModel> GetItems(long OrderId)
        {
           return _orderRepository.GetItems(OrderId);
        }

        public string PaymentSucceeded(long orderId, long refId)
        {
            var order = _orderRepository.Get(orderId);
            order.PaymentSucceeded(refId);
            //  var symbol = _configuration.GetValue<string>("Symbol");
            // var issueTracking = CodeGenerator.Generate(symbol);
            //codeGenerate  یک کد شیش رقمی ترکیبی از عدد و حرف درست میکند
            var issueTracking = CodeGenerator.Generate("S");
            order.SetIssueTrackingNo(issueTracking);

            if (_shopInventoryAcl.ReduceFromInventory(order.Items))
            {
                _orderRepository.SaveChanges();

                var account=_shopAccountAcl.GetAccountBy(order.AccountId);
                _smsService.Send(account.mobile,account.name,issueTracking);
                return issueTracking;
            }
            return "";
        

        }

        public long PlaceOrder(Cart cart)
        {
            var currentAccountId = _authHelper.CurrentAccountId();
           
            var order = new Order(currentAccountId, cart.TotalAmount, cart.DiscountAmount, cart.PayAmount,cart.PaymentMethod);
            foreach (var cartItem in cart.Items)
            {
                //چون جدول orderitem  جزو order  بود نیازی به ایدی نداشتیم برای ساختش خودش مپ میکندس
                var orderItem = new OrderItem(cartItem.Id, cartItem.Count, cartItem.UnitPrice, cartItem.DisCountRate);
                order.AddItems(orderItem);
            }
            _orderRepository.Create(order);
            _orderRepository.SaveChanges();

            return order.Id;


        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel)
        {
            return _orderRepository.Search(searchModel);
        }
    }
}
