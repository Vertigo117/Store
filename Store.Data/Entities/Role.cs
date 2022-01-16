using System.Collections.Generic;

namespace Store.Data.Entities
{
    /// <summary>
    /// Роль пользователя для авторизации в системе
    /// </summary>
    public class Role : BaseEntity
    {
        /// <summary>
        /// Тип роли
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Пользователи роли
        /// </summary>
        public List<User> Users { get; set; }
    }
}
