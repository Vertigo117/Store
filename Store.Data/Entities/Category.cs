using System.Collections.Generic;

namespace Store.Data.Entities
{
    /// <summary>
    /// Категория товара
    /// </summary>
    public class Category : BaseEntity
    {
        /// <summary>
        /// Название категории
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание категории
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Товары, входящие в категорию
        /// </summary>
        public List<Product> Products { get; set; }
    }
}
