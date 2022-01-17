using Store.Data.Entities;
using System.Threading.Tasks;

namespace Store.Data.Interfaces
{
    /// <summary>
    /// Интерфейс для реализации паттерна UnitOfWork
    /// </summary>
    public interface IRepositoryWrapper
    {
        /// <summary>
        /// Пользователи
        /// </summary>
        public IRepository<User> Users { get; }

        /// <summary>
        /// Категории товаров
        /// </summary>
        public IRepository<Category> Categories { get; }

        /// <summary>
        /// Заказы
        /// </summary>
        public IRepository<Order> Orders { get; }

        /// <summary>
        /// Товары
        /// </summary>
        public IRepository<Product> Products { get; }

        /// <summary>
        /// Информация о заказах
        /// </summary>
        public IRepository<ProductOrder> ProductOrders { get; }

        /// <summary>
        /// Сохранить изменения в базе данных
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();
    }
}
