
using _0_Framework.Application;
using _0_Freamwork.Application;
using Microsoft.Extensions.Configuration;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAuthHelper _authHelper;
        //میتوانستیم از appsetting  بخوانیم
       // private readonly IConfiguration _configuration;

        public OrderApplication(IOrderRepository orderRepository,IAuthHelper authHelper)
        {
            _orderRepository = orderRepository;
            _authHelper = authHelper;
           // _configuration = configuration;
        }

        

        public void PaymentSucceeded(long orderId, long refId)
        {
            var order = _orderRepository.Get(orderId);
            order.PaymentSucceeded(refId);
            //  var symbol = _configuration.GetValue<string>("Symbol");
            // var issueTracking = CodeGenerator.Generate(symbol);
            //codeGenerate  یک کد شیش رقمی ترکیبی از عدد و حرف درست میکند
            var issueTracking = CodeGenerator.Generate("S");
            order.SetIssueTrackingNo(issueTracking);
            _orderRepository.SaveChanges();

        }

        public long PlaceOrder(Cart cart)
        {
            var currentAccountId = _authHelper.CurrentAccountId();
           
            var order = new Order(currentAccountId, cart.TotalAmount, cart.DiscountAmount, cart.PayAmount);
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
    }
}
