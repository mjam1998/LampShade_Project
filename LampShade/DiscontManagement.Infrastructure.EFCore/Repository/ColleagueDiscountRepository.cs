﻿

using _0_Freamwork.Application;
using _0_Freamwork.Infrastructure;
using DiscountManagement.Application.Contract.ColleagueDiscountAppContract;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using ShopManagement.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class ColleagueDiscountRepository : RepositoryBase<long, ColleagueDiscount>, IColleagueDiscountRepository
    {
        private readonly DiscountContext _context;
        private readonly ShopContext _shopContext;
        public ColleagueDiscountRepository(DiscountContext context,ShopContext shopContext) : base(context) 
        {
            _context = context;
            _shopContext=shopContext;
        }
        public EditColleagueDiscount GetDetails(long id)
        {
            return _context.ColleagueDiscounts.Select(x=>new EditColleagueDiscount
            {
                Id=x.Id,
                ProductId=x.ProductId,
                DiscountRate=x.DiscountRate
            }).FirstOrDefault(x=>x.Id == id);
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = _context.ColleagueDiscounts.Select(x => new ColleagueDiscountViewModel
            {
                Id=x.Id,
                ProductId=x.ProductId,
                DiscountRate= x.DiscountRate,
                CreationDate=x.CreationDate.ToFarsi(),
                IsRemoved=x.IsRemoved
            });
            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            var discounts=query.OrderByDescending(x=>x.Id).ToList();
            discounts.ForEach(discount => discount.Product = products.FirstOrDefault(x => x.Id == discount.ProductId)?.Name);
            return discounts;
        }
    }
}
