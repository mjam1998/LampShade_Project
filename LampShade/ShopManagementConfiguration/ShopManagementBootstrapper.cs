﻿
using _01_LampShadeQuery.Contracts.Product;
using _01_LampShadeQuery.Contracts.ProductCategory;
using _01_LampShadeQuery.Contracts.Slide;
using _01_LampShadeQuery.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using ShopManagement.Application;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Application.Contracts.ProductAppContract;
using ShopManagement.Application.Contracts.ProductCategoryAppContract;
using ShopManagement.Application.Contracts.ProductPictureAppContract;
using ShopManagement.Application.Contracts.SlideAppContract;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.Service;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastructure;
using ShopManagement.Infrastructure.AccountAcl;
using ShopManagement.Infrastructure.Repository;

namespace ShopManagementConfiguration
{
    public class ShopManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();

            services.AddTransient<IProductApplication, ProductApplication>();
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddTransient<IProductPictureApplication, ProductPictureApplication>();
            services.AddTransient<IProductPictureRepository, ProductPictureRepository>();

            services.AddTransient<ISlideApplication, SlideApplication>();
            services.AddTransient<ISlideRepository,SlideRepository>();

            services.AddTransient<IOrderApplication,OrderApplication>();
            services.AddTransient<IOrderRepository,OrderRepository>();
           
            //برای اینکه میخواهیم کوکی که برای پرداخت نهایی استفاده میکنیم را تغییر ندهند و در طول استفاده برنامه ثابت بماند
            services.AddSingleton<ICartService,CartService>();

            services.AddTransient<IShopAccountAcl, ShopAccountAcl>();
           
            services.AddTransient<ISlideQuery, SlideQuery>();
            services.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();
            services.AddTransient<IProductQuery, ProductQuery>();
           

            services.AddDbContext<ShopContext>(x=>x.UseSqlServer(connectionString));
        }
    }
}
