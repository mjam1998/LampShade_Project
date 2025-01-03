﻿

using _0_Freamwork.Domain;
using ShopManagement.Application.Contracts.Order;
using System.Collections.Generic;

namespace ShopManagement.Domain.OrderAgg
{
    public interface IOrderRepository:IRepository<long,Order>
    {
       double GetAmountBy(long id);
        List<OrderViewModel> Search(OrderSearchModel searchModel);
        List<OrderItemViewModel> GetItems(long OrderId);
    }
}
