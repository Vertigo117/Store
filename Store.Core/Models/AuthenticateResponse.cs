using Store.Data.Entities;
using System;

namespace Store.Core.Models
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
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Jwt-токен
        /// </summary>
        public string Token { get; }

        /// <summary>
        /// Создаёт новый экземпляр класса <seealso cref="AuthenticateResponse"/> с данными о пользователе и jwt-токеном
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="token">Jwt-токен</param>
        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Login = user.Login;
            Email = user.Email;
            Token = token;
        }
    }
}
