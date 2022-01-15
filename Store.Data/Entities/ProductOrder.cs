using System;

namespace Store.Data.Entities
{
    /// <summary>
    /// Заказанные товары
    /// </summary>
    public class ProductOrder
    {
        /// <summary>
        /// Уникальный идентификатор товара
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Уникальный идентификатор заказа
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// Товар
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Количество товара в заказе
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Заказ
        /// </summary>
        public Order Order { get; set; }
    }
}
