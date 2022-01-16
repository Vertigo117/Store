using MediatR;
using Store.Core.Models;

namespace Store.Core.Commands
{
    /// <summary>
    /// Команда для регистрации нового пользователя
    /// </summary>
    public class RegisterCommand : IRequest<RegistrationResponse>
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }
    }
}
