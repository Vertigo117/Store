using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Models
{
    /// <summary>
    /// Роли пользователей в системе
    /// </summary>
    internal class UserRoles
    {
        /// <summary>
        /// Администратор, имеет доступ к изменению данных
        /// </summary>
        public const string Admin = "Admin";

        /// <summary>
        /// Пользователь, имеет доступ к чтению данных и осуществлению заказов
        /// </summary>
        public const string User = "User";
    }
}
