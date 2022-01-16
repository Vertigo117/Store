using MediatR;
using Store.Core.Models;

namespace Store.Core.Queries
{
    /// <summary>
    /// Запрос на авторизацию
    /// </summary>
    public class LoginQuery : IRequest<UserResponse>
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
    }
}
