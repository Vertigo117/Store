using AutoMapper;
using MediatR;
using Store.Core.Exceptions;
using Store.Data.Entities;
using Store.Data.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Features.Commands.Handlers
{
    /// <summary>
    /// Обработчик запроса на обновление пользователя
    /// </summary>
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IRepository<User> repository;
        private readonly IMapper mapper;

        /// <summary>
        /// Создаёт новый экземпляр класса <seealso cref="UpdateUserHandler"/> с репозиторием 
        /// и автомаппером
        /// </summary>
        /// <param name="repository">Репозиторий</param>
        /// <param name="mapper">Автомаппер</param>
        /// <exception cref="ArgumentNullException"></exception>
        public UpdateUserHandler(IRepository<User> repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Обработать запрос на обновление пользователя
        /// </summary>
        /// <param name="request">Запрос на обновление пользователя</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Результат выполнения асинхронной операции</returns>
        /// <exception cref="CustomCoreException">Кастомное исключение</exception>
        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await repository.GetAsync(request.Id);

            if (user == null)
            {
                throw new CustomCoreException($"Пользователя с идентификатором \"{request.Id}\" не существует");
            }

            mapper.Map(request, user);
            repository.Update(user);
            await repository.SaveAsync();
            return Unit.Value;
        }
    }
}
