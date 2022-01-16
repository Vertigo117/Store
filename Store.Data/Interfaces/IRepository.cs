using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Store.Data.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория базы данных
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Получить все экземпляры сущности, которые удовлетворяют условиям для отбора
        /// </summary>
        /// <param name="predicate">Условия для отбора</param>
        /// <returns>Задача, которая содержит экземпляр сущности, удовлетворяющий заданным условиям</returns>
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Возвращает экземпляр сущности с заданным уникальным идентификатором
        /// </summary>
        /// <param name="id">Уникальный идентификатор сущности</param>
        /// <returns>Задача, которая содержит экземпляр сущности с заданным уникальным идентификатором</returns>
        Task<TEntity> GetAsync(Guid id);

        /// <summary>
        /// Получить все экземпляры сущности
        /// </summary>
        /// <returns>Задача, которая содержит коллекцию экземпляров сущности</returns>
        Task<IEnumerable<TEntity>> GetAsync();

        /// <summary>
        /// Создать новый экземпляр сущности
        /// </summary>
        /// <param name="entity">Экземпляр сущности</param>
        /// <returns>Результат выполнения асинхронной операции</returns>
        Task CreateAsync(TEntity entity);

        /// <summary>
        /// Обновить существующий экземпляр сущности
        /// </summary>
        /// <param name="entity">Данные для обновления</param>
        /// <returns>Задача, которая содержит результат выполнения асинхронной операции</returns>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Удалить экземпляр сущности из базы данных
        /// </summary>
        /// <param name="entity">Экземпляр сущности</param>
        /// <returns>Задача, которая содержит результат выполнения асинхронной операции</returns>
        Task DeleteAsync(TEntity entity);
    }
}
