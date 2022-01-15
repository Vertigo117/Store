using System.Collections.Generic;

namespace Store.Data.Entities
{
    /// <summary>
    /// Товар
    /// </summary>
    public class Product : BaseEntity
    {
        /// <summary>
        /// Наименование товара
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание товара
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Категория товара
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// Стоимость товара
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Количество товара на складе
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Заказы товара
        /// </summary>
        public List<ProductOrder> ProductOrders { get; set; }
    }
}
