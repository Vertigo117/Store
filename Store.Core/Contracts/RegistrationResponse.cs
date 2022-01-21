using System;

namespace Store.Core.Contracts
{
    /// <summary>
    /// Результат выполнения запроса на регистрацию нового пользователя
    /// </summary>
    public class RegistrationResponse
    {
        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Роль пользователя в системе
        /// </summary>
        public string Role { get; set; }
    }
}
