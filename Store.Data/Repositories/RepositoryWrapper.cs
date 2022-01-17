using Store.Data.Contexts;
using Store.Data.Entities;
using Store.Data.Interfaces;
using System.Threading.Tasks;

namespace Store.Data.Repositories
{
    /// <summary>
    /// Реализация паттерна UnitOfWork для репозитория базы данных
    /// </summary>
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly StoreContext dbContext;

        ///<inheritdoc/>
        public IRepository<User> Users { get; }

        ///<inheritdoc/>
        public IRepository<Category> Categories { get; }

        ///<inheritdoc/>
        public IRepository<Order> Orders { get; }

        ///<inheritdoc/>
        public IRepository<Product> Products { get; }

        ///<inheritdoc/>
        public IRepository<ProductOrder> ProductOrders { get; }

        /// <summary>
        /// Создаёт новый экземпляр класса <seealso cref="RepositoryWrapper"/> с контекстом базы данных
        /// </summary>
        /// <param name="dbContext">Контекст базы данных</param>
        public RepositoryWrapper(StoreContext dbContext)
        {
            this.dbContext = dbContext;
            Users = new Repository<User>(dbContext);
            Categories = new Repository<Category>(dbContext);
            Orders = new Repository<Order>(dbContext);
            Products = new Repository<Product>(dbContext);
            ProductOrders = new Repository<ProductOrder>(dbContext);
        }

        ///<inheritdoc/>
        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
