﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Freamwork.Application;
using _0_Freamwork.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPictureAppContract;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Infrastructure.Repository
{
    public class ProductPictureRepository:RepositoryBase<long,ProductPicture>,IProductPictureRepository
    {
        private readonly ShopContext _context;

        public ProductPictureRepository( ShopContext context): base(context) 
        {
            _context=context;
        }

        public EditProductPicture GetDetails(long id)
        {
            return _context.ProductPictures.Select(x => new EditProductPicture
            {
                Id = x.Id,
                
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ProductId = x.ProductId
            }).FirstOrDefault(x => x.Id == id);
        }

        public ProductPicture GetWithProductAndCategory(long id)
        {
           return _context.ProductPictures.Include(x=>x.Product).ThenInclude(x=>x.Category).FirstOrDefault(x=>x.Id == id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            var query = _context.ProductPictures.Include(x=>x.Product).Select(x => new ProductPictureViewModel
            {
                Id = x.Id,
                Product = x.Product.Name,
                CreationDate = x.CreationDate.ToFarsi(),
                Picture = x.Picture,
                ProductId=x.ProductId,
                IsRemoved = x.IsRemoved

            });
            if (searchModel.ProductId != 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
