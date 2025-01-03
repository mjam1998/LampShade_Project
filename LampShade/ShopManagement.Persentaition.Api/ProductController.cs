﻿using _01_LampShadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ShopManagement.Persentaition.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductQuery _productQuery;

        public ProductController(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public List<ProductQueryModel> GetLatestArrivals()
        {
            return _productQuery.GetLastArrivals();
        }
    }
}
