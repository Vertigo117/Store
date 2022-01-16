using System.Collections.Generic;

namespace Store.Data.Entities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Роль пользователя для авторизации в системе
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Заказы, сделанные пользователем
        /// </summary>
        public List<Order> Orders { get; set; }
    }
}
