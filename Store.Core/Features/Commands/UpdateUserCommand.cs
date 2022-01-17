using MediatR;
using System;

namespace Store.Core.Features.Commands
{
    /// <summary>
    /// Запрос на обновление пользователя
    /// </summary>
    public class UpdateUserCommand : IRequest
    {
        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Роль пользователя в системе
        /// </summary>
        public string Role { get; set; }
    }
}
