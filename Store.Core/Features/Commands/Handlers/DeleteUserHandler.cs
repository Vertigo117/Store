using MediatR;
using Store.Core.Exceptions;
using Store.Data.Entities;
using Store.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Features.Commands.Handlers
{
    /// <summary>
    /// Обработчик команды удаления пользователя
    /// </summary>
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IRepository<User> repository;

        /// <summary>
        /// Создаёт новый экземпляр класса <seealso cref="DeleteUserHandler"/> с репозиторием
        /// </summary>
        /// <param name="repository">Репозиторий</param>
        /// <exception cref="ArgumentNullException"></exception>
        public DeleteUserHandler(IRepository<User> repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Обработать запрос на удаление
        /// </summary>
        /// <param name="request">Запрос на удаление</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Результат выполнения асинхронной операции</returns>
        /// <exception cref="CustomCoreException"></exception>
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await repository.GetAsync(request.UserId);

            if (user == null)
            {
                throw new CustomCoreException($"Пользователя с идентификатором \"{request.UserId}\" не существует");
            }

            repository.Delete(user);
            await repository.SaveAsync();
            return Unit.Value;
        }
    }
}
