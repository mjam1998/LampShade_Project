

using _01_LampShadeQuery.Contracts.Inventory;
using _01_LampShadeQuery.Query;
using InventoryManagement.Application;
using InventoryManagement.Application.Contract.InventoryAppContract;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Infrastructure.Configuration
{
    public class InventoryManagementBootstrapper
    {
        public static void Configure(IServiceCollection services,string connectionString)
        {
            services.AddTransient<IInventoryRepository,InventortRepository>();
            services.AddTransient<IInventoryApplication, InventoryApplication>();

            services.AddTransient<IInventoryQuery, InventoryQuery>();

            services.AddDbContext<InventoryContext>(x=>x.UseSqlServer(connectionString));

        }
    }
}
