﻿
using AccountManagement.Application;
using AccountManagement.Application.Contract.AccountAppContract;
using AccountManagement.Application.Contract.RoleAppContract;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Infrastructure.EFCore;
using AccountManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccountManagement.Infrastructure.Configuration
{
    public class AccountManagementBootstrapper
    {
        public static void Configure(IServiceCollection services,string connectionString)
        {
            services.AddTransient<IAccountApplication,AccountApplication>();
            services.AddTransient<IAccountRepository,AccountRepository>();

            services.AddTransient<IRoleApplication, RoleApplication>();
            services.AddTransient<IRoleRepository, RoleRepository>();

            services.AddDbContext<AccountContext>(x=>x.UseSqlServer(connectionString));
        }
    }
}
 