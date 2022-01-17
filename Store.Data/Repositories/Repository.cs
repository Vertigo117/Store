using Microsoft.EntityFrameworkCore;
using Store.Data.Contexts;
using Store.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Store.Data.Repositories
{
    /// <summary>
    /// Репозиторий для работы с базой данных
    /// </summary>
    /// <typeparam name="TEntity">Сущность в базе данных</typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly StoreContext context;

        /// <summary>
        /// Создаёт новый экземпляр класса <seealso cref="Repository{TEntity}"/>
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public Repository(StoreContext context)
        {
            this.context = context;
        }

        ///<inheritdoc/>
        public void Create(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }

        ///<inheritdoc/>
        public void Delete(TEntity entity)
        {
            context.Remove(entity);
        }

        ///<inheritdoc/>
        public async Task<TEntity> GetAsync(Guid id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        ///<inheritdoc/>
        public void Update(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
        }
    }
}
