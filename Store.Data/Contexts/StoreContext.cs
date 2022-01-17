using Microsoft.EntityFrameworkCore;
using Store.Data.Entities;
using System.Reflection;

namespace Store.Data.Contexts
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public class StoreContext : DbContext
    {
        /// <summary>
        /// Создаёт новый экземпляр класса <seealso cref="StoreContext"/> с опциями контекста данных
        /// </summary>
        /// <param name="options">Опции контекста данных</param>
        public StoreContext(DbContextOptions options) : base(options)
        { }

        /// <summary>
        /// Категории
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Товары
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Заказанные товары
        /// </summary>
        public DbSet<ProductOrder> ProductOrders { get; set; }

        /// <summary>
        /// Заказы
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Пользователи
        /// </summary>
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
