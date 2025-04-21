using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models.Entities.Context
{
    public class DataBaseContext : DbContext
    {
        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DataBaseContext( DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<ToDo>().HasQueryFilter(x=>!x.IsRemoved);
        }
    }
}
