using AutoMapper;
using MediatR;
using Store.Core.Contracts;
using Store.Data.Entities;
using Store.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Features.Queries.Handlers
{
    /// <summary>
    /// Обработчик запроса на получение пользователей
    /// </summary>
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, List<UserResponse>>
    {
        private readonly IRepository<User> repository;
        private readonly IMapper mapper;

        /// <summary>
        /// Создаёт новый экземпляр класса <seealso cref="GetUsersHandler"/> с репозиторием бд
        /// и автомаппером
        /// </summary>
        /// <param name="repository">Репозиторий бд</param>
        /// <param name="mapper">Автомаппер</param>
        /// <exception cref="ArgumentNullException"/>
        public GetUsersHandler(IRepository<User> repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Обработать запрос на получение пользователей
        /// </summary>
        /// <param name="request">Запрос на получение пользователей</param>
        /// <param name="cancellationToken">Токен для отмены операции</param>
        /// <returns>Задача, которая содержит результат выполнения операции</returns>
        public async Task<List<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await repository.GetAllAsync();
            var response = mapper.Map<List<UserResponse>>(users);
            return response;
        }
    }
}
