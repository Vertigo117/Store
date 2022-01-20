using MediatR;
using System;

namespace Store.Core.Features.Commands
{
    /// <summary>
    /// Запрос на удаление пользователя из базы данных
    /// </summary>
    public class DeleteUserCommand : IRequest
    {
        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Создаёт новый экземпляр класса <seealso cref="DeleteUserCommand"/>
        /// с уникальным идентификатором пользователя
        /// </summary>
        /// <param name="userId">Уникальный идентификатор пользователя</param>
        public DeleteUserCommand(string userId)
        {
            UserId = Guid.Parse(userId);
        }
    }
}
