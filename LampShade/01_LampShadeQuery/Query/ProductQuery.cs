﻿

using _0_Freamwork.Application;
using _01_LampShadeQuery.Contracts.Product;
using _01_LampShadeQuery.Contracts.ProductCategory;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_LampShadeQuery.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _context;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public ProductQuery(ShopContext context, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public List<ProductQueryModel> GetLastArrivals()
        {
            var inventory = _inventoryContext.Inventory.Select(x => new { x.ProductId, x.UnitPrice }).ToList();
            var discount = _discountContext.CustomerDiscounts.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now).Select(x => new { x.ProductId, x.DiscountRate }).ToList();
            var products = _context.Products.Include(x => x.Category).Select(product => new ProductQueryModel
            {
                Id = product.Id,
                Category = product.Category.Name,
                Name = product.Name,
                Picture = product.Picture,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                Slug = product.Slug
            }).OrderByDescending(x=>x.Id).Take(6).ToList();

            
                foreach (var product in products)
                {
                    var inv = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                    if (inv != null)
                    {
                        var price = inv.UnitPrice;
                        product.Price = price.ToMoney();
                        var disc = discount.FirstOrDefault(x => x.ProductId == product.Id);
                        if (disc != null)
                        {
                            int discountRate = disc.DiscountRate;
                            product.DiscountRate = discountRate;
                            product.HasDiscount = discountRate > 0;
                            var discountAmount = Math.Round((price * discountRate) / 100);
                            product.PriceWithDiscount = (price - discountAmount).ToMoney();
                        }
                    }




                }
            

            return products;
        }

        public List<ProductQueryModel> Search(string value)
        {
            var inventory = _inventoryContext.Inventory.Select(x => new { x.ProductId, x.UnitPrice }).ToList();
            var discount = _discountContext.CustomerDiscounts.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now).Select(x => new { x.ProductId, x.DiscountRate }).ToList();
            var query = _context.Products.Include(x => x.Category).
                Select(product => new ProductQueryModel
            {
                    Id = product.Id,
                    Category = product.Category.Name,
                    Name = product.Name,
                    Picture = product.Picture,
                    PictureAlt = product.PictureAlt,
                    PictureTitle = product.PictureTitle,
                    Slug = product.Slug,
                    CategorySlug=product.Category.Slug,
                    ShortDescription=product.ShortDescription
                }).AsNoTracking();

            if(!string.IsNullOrWhiteSpace(value))
                query=query.Where(x=>x.Name.Contains(value) || x.ShortDescription.Contains(value));

            var products=query.OrderByDescending(x=>x.Id).ToList();

            foreach (var product in products)
            {
                var inv = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                if (inv != null)
                {
                    var price = inv.UnitPrice;
                    product.Price = price.ToMoney();
                    var disc = discount.FirstOrDefault(x => x.ProductId == product.Id);
                    if (disc != null)
                    {
                        int discountRate = disc.DiscountRate;
                        product.DiscountRate = discountRate;
                        product.HasDiscount = discountRate > 0;
                        var discountAmount = Math.Round((price * discountRate) / 100);
                        product.PriceWithDiscount = (price - discountAmount).ToMoney();
                    }
                }




            }


            return products;
        }
    }
}
