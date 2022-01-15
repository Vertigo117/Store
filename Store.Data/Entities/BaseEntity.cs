using System;

namespace Store.Data.Entities
{
    /// <summary>
    /// Родительский класс для сущностей бд
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
