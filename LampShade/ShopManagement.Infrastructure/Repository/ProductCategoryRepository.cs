﻿using System.Collections.Generic;
using System.Linq;
using _0_Freamwork.Infrastructure;
using ShopManagement.Application.Contracts.ProductCategoryAppContract;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Infrastructure.Repository
{
    public class ProductCategoryRepository:RepositoryBase<long,ProductCategory>,IProductCategoryRepository
    {
        private readonly ShopContext _context;

        public ProductCategoryRepository(ShopContext context) :base(context) 
        {
            _context = context;
        }

        

        

        public EditProductCategory GetDetails(long id)
        {
            return _context.ProductCategories.Select(x => new EditProductCategory
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug

            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel serModel)
        {
            var query = _context.ProductCategories.Select(x => new ProductCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                CreationDate = x.CreationDate.ToString()

            });
            query = query.Where(x => x.Name.Contains(serModel.Name));
            return query.OrderByDescending(x => x.Id).ToList();
        }

        
    }
}