using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Store.Data.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория базы данных
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности в базе данных</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
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
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// Создать новый экземпляр сущности
        /// </summary>
        /// <param name="entity">Экземпляр сущности</param>
        void Create(TEntity entity);

        /// <summary>
        /// Обновить существующий экземпляр сущности
        /// </summary>
        /// <param name="entity">Данные для обновления</param>
        void Update(TEntity entity);

        /// <summary>
        /// Удалить экземпляр сущности из базы данных
        /// </summary>
        /// <param name="entity">Экземпляр сущности</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Сохранить изменения в базе данных
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();
    }
}
