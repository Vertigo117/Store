using Microsoft.EntityFrameworkCore;
using Store.Data.Configurations;
using Store.Data.Entities;

namespace Store.Data.Contexts
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new ProductOrderConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
