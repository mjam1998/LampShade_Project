using System;
using System.Collections.Generic;
using System.Linq;
using _0_Freamwork.Application;
using _01_LampShadeQuery.Contracts.Product;
using _01_LampShadeQuery.Contracts.ProductCategory;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure;

namespace _01_LampShadeQuery.Query
{
    public class ProductCategoryQuery:IProductCategoryQuery
    {
        private readonly ShopContext _context;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        public ProductCategoryQuery(ShopContext context,InventoryContext inventoryContext,DiscountContext discountContext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
            _discountContext=discountContext;
        }

        public List<ProductCategoryQueryModel> GetProductCategories()
        {
            return _context.ProductCategories.Select(x => new ProductCategoryQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).ToList();

        }

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
        {
            var inventory = _inventoryContext.Inventory.Select(x => new {x.ProductId,x.UnitPrice}).ToList();
            var discount=_discountContext.CustomerDiscounts.Where(x=>x.StartDate<DateTime.Now && x.EndDate>DateTime.Now).Select(x=>new {x.ProductId, x.DiscountRate,x.EndDate}).ToList();
            var categories= _context.ProductCategories.Include(x => x.Products).ThenInclude(x=>x.Category).Select(x=>new ProductCategoryQueryModel
            {
               Id=x.Id,
               Name=x.Name,
              Products=MapProduts(x.Products)
            }).ToList();
            foreach (var category in categories)
            {
                foreach (var product in category.Products)
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
                            product.DiscountExpireDate=disc.EndDate.ToDiscountFormat();
                            product.HasDiscount = discountRate > 0;
                            var discountAmount = Math.Round((price * discountRate) / 100);
                            product.PriceWithDiscount = (price - discountAmount).ToMoney();
                        }
                    }
                   

                   
                   
                }
            }

            return categories;
        }

        private static List<ProductQueryModel> MapProduts(List<Product> products)
        {
          
            return products.Select(product=>new ProductQueryModel
            {
                Id = product.Id,
                Category = product.Category.Name,
                Name = product.Name,
                Picture = product.Picture,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                Slug = product.Slug

            }).ToList();
           
           
        }

        public ProductCategoryQueryModel GetProductCategoryWithProductsBy(string slug)
        {
            var inventory = _inventoryContext.Inventory.Select(x => new { x.ProductId, x.UnitPrice }).ToList();
            var discount = _discountContext.CustomerDiscounts.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now).Select(x => new { x.ProductId, x.DiscountRate }).ToList();
            var category = _context.ProductCategories.Include(x => x.Products).ThenInclude(x => x.Category).Select(x => new ProductCategoryQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Products = MapProduts(x.Products),
                Keywords=x.Keywords,
                MetaDescription=x.MetaDescription,
                Slug=x.Slug,
                Description=x.Description
            }).FirstOrDefault(x=>x.Slug==slug);
            
                foreach (var product in category.Products)
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
           

            return category;
        }
    }
}
