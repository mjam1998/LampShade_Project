

using CommentManagement.Application;
using CommentManagement.Application.Contracts.CommentAppContract;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure.Efcore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Infrastructure.Repository;

namespace CommentManagement.Infrastructure.Configuration
{
    public class CommentManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectonString)
        {
            services.AddTransient<ICommentApplication,CommentApplication>();
            services.AddTransient<ICommentRepository,CommentRepository>();

            services.AddDbContext<CommentContext>(x=>x.UseSqlServer(connectonString));  
        }
    }
}
