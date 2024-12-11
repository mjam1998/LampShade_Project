using _0_Framework.Application;
using _0_Freamwork.Application;
using _01_LampShadeQuery.Contracts;
using _01_LampShadeQuery.Query;
using AccountManagement.Infrastructure.Configuration;
using BlogManagement.Infrastructure.Configuration;
using CommentManagement.Infrastructure.Configuration;
using DiscountManagement.Configuration;
using InventoryManagement.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopManagementConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace ServiceHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            var connectionString = Configuration.GetConnectionString("LampShadeDb");
            ShopManagementBootstrapper.Configure(services, connectionString);
            DiscountManagementBootstrapper.Configure(services, connectionString);
            InventoryManagementBootstrapper.Configure(services, connectionString);
            BlogManagementBootstrapper.Configure(services,connectionString);
            CommentManagementBootstrapper.Configure(services, connectionString);
            AccountManagementBootstrapper.Configure(services, connectionString);
            //برای ساخت کوکی
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });
            //برای ساخت کوکی
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                {
                    o.LoginPath = new PathString("/Account");
                    o.LogoutPath = new PathString("/Account");
                    o.AccessDeniedPath = new PathString("/AccessDenied");
                });

            services.AddTransient<IFileUploader,FileUploader>();
            services.AddSingleton<IPasswordHasher,PasswordHasher>();   
            services.AddTransient<ICartCalculatorService,CartCalculatorService>();
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));
            services.AddTransient<IAuthHelper,AuthHelper>();
            //اینجا یک مجوز درست میکنیم برای نقش های 1 و 2 
            services.AddAuthorization(options =>

          {
              options.AddPolicy("AdminArea", builder => builder.RequireRole(new List<string> { Roles.Admin, Roles.ContentUploader }));
              //فقط نقش شماره یک میتواند به شاپ دسترسی داشته باشد
              options.AddPolicy("Shop", builder => builder.RequireRole(new List<string> { Roles.Admin}));
              options.AddPolicy("Discount", builder => builder.RequireRole(new List<string> { Roles.Admin }));
              options.AddPolicy("Account", builder => builder.RequireRole(new List<string> { Roles.Admin }));
              options.AddPolicy("Inventory", builder => builder.RequireRole(new List<string> { Roles.Admin }));

          }); 

            //اینجایک مجوز برای ناحیه ادمینستریشن درست میکنیم که مجوز ادمین اریا
            //بالا اجازه دسترسی به ان را دارد و کل روت را برایش تعیین میکنیم در پارامتر دوم
            services.AddRazorPages().AddRazorPagesOptions(options=>
            {
                options.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminArea");
                options.Conventions.AuthorizeAreaFolder("Administration", "/Shop", "Shop");
                options.Conventions.AuthorizeAreaFolder("Administration", "/Discounts", "Discount");
                options.Conventions.AuthorizeAreaFolder("Administration", "/Accounts", "Account");
                options.Conventions.AuthorizeAreaFolder("Administration", "/Inventory", "Inventory");


            } );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
               // app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //برای احراز هویت
            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            //برای کوکی
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
