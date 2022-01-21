using System;

namespace Store.Core.Contracts
{
    /// <summary>
    /// Результат запроса на получение категории
    /// </summary>
    public class CategoryResponse
    {
        /// <summary>
        /// Уникальный идентификатор категории
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название категории
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание категории
        /// </summary>
        public string Description { get; set; }
    }
}
