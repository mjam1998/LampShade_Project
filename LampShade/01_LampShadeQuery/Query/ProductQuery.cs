

using _0_Freamwork.Application;
using _01_LampShadeQuery.Contracts.Product;
using _01_LampShadeQuery.Contracts.ProductCategory;
using CommentManagement.Infrastructure.Efcore;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.ProductPictureAgg;
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
        private readonly CommentContext _commentContext;

        public ProductQuery(ShopContext context, InventoryContext inventoryContext, DiscountContext discountContext,CommentContext commentContext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
            _commentContext = commentContext;
        }

        public ProductQueryModel GetDetails(string slug)
        {
            var inventory = _inventoryContext.Inventory.Select(x => new { x.ProductId, x.UnitPrice,x.InStock }).ToList();
            var discount = _discountContext.CustomerDiscounts.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now).Select(x => new { x.ProductId, x.DiscountRate,x.EndDate }).ToList();
            var product = _context.Products.Include(x => x.Category).Include(x=>x.ProductPictures).Select(product => new ProductQueryModel
            {
                Id = product.Id,
                Category = product.Category.Name,
                Name = product.Name,
                Picture = product.Picture,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                Slug = product.Slug,
                CategorySlug=product.Category.Slug,
                Code = product.Code,
                Description = product.Description,
                ShortDescription = product.ShortDescription,
                Keywords = product.Keywords,
               
                ProductPictures=MapProductPictures(product.ProductPictures)
            }).FirstOrDefault(x=>x.Slug==slug);


            
                var inv = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                if (inv != null)
                {
                    product.InStock = inv.InStock;
                    var price = inv.UnitPrice;
                    product.Price = price.ToMoney();
                product.DoublePrice=price;
                    var disc = discount.FirstOrDefault(x => x.ProductId == product.Id);
                    if (disc != null)
                    {
                        int discountRate = disc.DiscountRate;
                        product.DiscountRate = discountRate;
                    product.DiscountExpireDate = disc.EndDate.ToDiscountFormat();
                    product.HasDiscount = discountRate > 0;
                        var discountAmount = Math.Round((price * discountRate) / 100);
                        product.PriceWithDiscount = (price - discountAmount).ToMoney();
                    }
                }


                product.Comments=_commentContext.Comments
                .Where(x=>x.Type==CommentTypes.Product)
                .Where(x=>!x.IsCanceled)
                .Where(x=>x.IsConfirmed)
                .Where(x=>x.OwnerId==product.Id)
                .Select(x=> new CommentQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Message = x.Message

                }).OrderByDescending(x=>x.Id).ToList();


           


            return product;
        }

   

        private static List<ProductPictureQueryModel> MapProductPictures(List<ProductPicture> pictures)
        {
           return pictures.Select(x=>new ProductPictureQueryModel
           {
               IsRemoved = x.IsRemoved,
               Picture=x.Picture,
               PictureAlt=x.PictureAlt,
               PictureTitle=x.PictureTitle,
               ProductId=x.ProductId,

           }).Where(x=>!x.IsRemoved).ToList();
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
            var discount = _discountContext.CustomerDiscounts.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now).Select(x => new { x.ProductId, x.DiscountRate,x.EndDate }).ToList();
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
                        product.DiscountExpireDate = disc.EndDate.ToDiscountFormat();
                        product.HasDiscount = discountRate > 0;
                        var discountAmount = Math.Round((price * discountRate) / 100);
                        product.PriceWithDiscount = (price - discountAmount).ToMoney();
                    }
                }




            }


            return products;
        }

        public List<CartItem> CheckInventoryStatus(List<CartItem> cartItems)
        {
            var inventory=_inventoryContext.Inventory.ToList();
            foreach (var cartitem in cartItems.Where(cartItem=> inventory.Any(x=>x.ProductId==cartItem.Id && x.InStock)))
            {
                var itemInventory=inventory.Find(x=>x.ProductId==cartitem.Id);
                cartitem.IsInStock = itemInventory.CalculateCurrentCount()>=cartitem.Count;
            }
            return cartItems;
        }
    }
}
