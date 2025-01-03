

using _0_Freamwork.Application;
using _0_Freamwork.Infrastructure;
using AccountManagement.Infrastructure.EFCore;
using ShopManagement.Application.Contracts;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.Repository
{
    public class OrderRepository:RepositoryBase<long,Order>, IOrderRepository
    {
        private readonly AccountContext _accountContext;
        private readonly ShopContext _shopContext;

        public OrderRepository(ShopContext shopContext,AccountContext accountContext):base(shopContext) 
        {
            _shopContext = shopContext;
            _accountContext = accountContext;
        }

        public double GetAmountBy(long id)
        {
           return _shopContext.Orders.Select(x=> new {x.Id,x.PayAmount}).FirstOrDefault(x=>x.Id==id).PayAmount;
        }

        public List<OrderItemViewModel> GetItems(long OrderId)
        {
            var products = _shopContext.Products.Select(x => new {x.Id,x.Name}).ToList();
            var order =_shopContext.Orders.FirstOrDefault(x=>x.Id == OrderId);
            if(order == null)
                return new List<OrderItemViewModel>();
            var items=order.Items.Select(x=> new OrderItemViewModel
            {
                Id = x.Id,
                ProductId= x.ProductId,
                Count= x.Count, 
                DiscountRate= x.DiscountRate, 
                UnitPrice= x.UnitPrice,
                OredrId= x.OredrId

            }).ToList();
            foreach (var orderItem in items)
            {
                orderItem.Product = products.FirstOrDefault(x => x.Id == orderItem.ProductId)?.Name;
            }
            return items;
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel)
        {
            var accounts=_accountContext.Accounts.Select(x=> new {x.Id,x.FullName}).ToList();
            var query = _shopContext.Orders.Select(x => new OrderViewModel
            {
                Id= x.Id,
                AccountId= x.AccountId,
                DiscountAmount= x.DiscountAmount,
                IsCanceled= x.IsCanceled,
                IsPaid= x.IsPaid,
                IssueTrackingNo= x.IssueTrackingNo,
                PayAmount= x.PayAmount, 
                PaymentMethodId=x.PaymentMethod,
                RefId= x.RefId,
                TotalAmount= x.TotalAmount,
                CreationDate=x.CreationDate.ToFarsi()
            });
            
            if(searchModel.AccountId>0)
                query=query.Where(x=>x.AccountId==searchModel.AccountId);
            if (searchModel.IsPaid)
                query = query.Where(x => x.IsPaid);
            var orders = query.OrderByDescending(x => x.Id).ToList();

            foreach (var item in orders)
            {
                item.AccountFullName = accounts.FirstOrDefault(x => x.Id == item.AccountId).FullName;
                item.PaymentMethod=PaymentMethod.GetBy(item.PaymentMethodId)?.Name;
            }

            return orders;
        }
    }
}
