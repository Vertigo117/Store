using MediatR;
using Store.Core.Contracts;

namespace Store.Core.Features.Queries
{
    /// <summary>
    /// Запрос на авторизацию
    /// </summary>
    public class LoginQuery : IRequest<AuthenticateResponse>
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
