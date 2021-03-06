using System;

namespace Store.Core.Contracts
{
    /// <summary>
    /// Результат авторизации
    /// </summary>
    public class AuthenticateResponse
    {
        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Jwt-токен
        /// </summary>
        public string Token { get; set; }
    }
}
