﻿using AutoMapper;
using MediatR;
using Serilog;
using Store.Core.Models;
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
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserResponse>>
    {
        private readonly IRepositoryWrapper repository;
        private readonly IMapper mapper;

        /// <summary>
        /// Создаёт новый экземпляр класса <seealso cref="GetUsersHandler"/> с репозиторием бд
        /// и автомаппером
        /// </summary>
        /// <param name="repository">Репозиторий бд</param>
        /// <param name="mapper">Автомаппер</param>
        public GetUsersHandler(IRepositoryWrapper repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Обработать запрос на получение пользователей
        /// </summary>
        /// <param name="request">Запрос на получение пользователей</param>
        /// <param name="cancellationToken">Токен для отмены операции</param>
        /// <returns>Задача, которая содержит результат выполнения операции</returns>
        public async Task<IEnumerable<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var users = await repository.Users.GetAsync();
                var response = mapper.Map<IEnumerable<UserResponse>>(users);
                return response;
            }
            catch (Exception exception)
            {
                Log.Error(exception, "Произошла ошибка запроса на получение пользователей");
                throw;
            }
        }
    }
}
