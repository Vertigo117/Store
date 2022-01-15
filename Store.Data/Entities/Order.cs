using System;
using System.Collections.Generic;

namespace Store.Data.Entities
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class Order : BaseEntity
    {
        /// <summary>
        /// Дата заказа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Пользователь, совершивший заказ
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Товары, который заказал пользователь
        /// </summary>
        public List<ProductOrder> ProductOrders { get; set; }
    }
}
