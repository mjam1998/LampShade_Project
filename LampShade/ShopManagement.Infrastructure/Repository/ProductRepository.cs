using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Freamwork.Application;
using _0_Freamwork.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductAppContract;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.Repository
{
    public class ProductRepository:RepositoryBase<long,Product>,IProductRepository
    {
        private readonly ShopContext _context;

        public ProductRepository( ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditProduct GetDetails(long id)
        {
            return _context.Products.Select(x => new EditProduct
            {
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.CategoryId,
                Code = x.Code,
                Description = x.Description,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                UnitPrice = x.UnitPrice,
                ShortDescription = x.ShortDescription

            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _context.Products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            var query = _context.Products.Include(x => x.Category).Select(x=>new ProductViewModel
            {
                Id = x.Id,
                Category = x.Category.Name,
                Code = x.Code,
                Name = x.Name,
                UnitPrice = x.UnitPrice,
                CategoryId=x.CategoryId,
                Picture = x.Picture,
                IsInStock = x.IsInStock,
                CreationDate = x.CreationDate.ToFarsi()
                
            });
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            if (!string.IsNullOrWhiteSpace(searchModel.Code))
                query = query.Where(x => x.Code.Contains(searchModel.Code));
            if (searchModel.CategoryId != 0)
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);
            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
