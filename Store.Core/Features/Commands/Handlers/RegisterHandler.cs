using AutoMapper;
using MediatR;
using Serilog;
using Store.Core.ErrorHandling.Exceptions;
using Store.Core.Models;
using Store.Data.Entities;
using Store.Data.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Store.Core.Features.Commands.Handlers
{
    /// <summary>
    /// Обработчик запроса на регистрацию
    /// </summary>
    internal class RegisterHandler : IRequestHandler<RegisterCommand, RegistrationResponse>
    {
        private readonly IRepositoryWrapper repository;
        private readonly IMapper mapper;

        /// <summary>
        /// Создаёт новый экземпляр класса <seealso cref="RegisterHandler"/> с репозиторием пользователей
        /// и автомаппером
        /// </summary>
        /// <param name="userRepository">Репозиторий пользователей</param>
        /// <param name="mapper">Автомаппер</param>
        public RegisterHandler(IRepositoryWrapper repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Обрабатывает запрос на регистрацию пользователя
        /// </summary>
        /// <param name="command">Запрос на регистрацию</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Задача, которая содержит результат выполнения регистрации</returns>
        /// <exception cref="CustomCoreException">Ошибка регистрации</exception>
        public async Task<RegistrationResponse> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            try
            {
                bool loginExists = (await repository.Users.GetAsync()).Any(user => user.Login == command.Login);

                if (loginExists)
                {
                    throw new CustomCoreException($"Пользователь с логином '{command.Login}' уже существует");
                }

                command.Password = BCryptNet.HashPassword(command.Password);

                var user = mapper.Map<User>(command);
                user.Role = UserRoles.User;
                repository.Users.Create(user);
                await repository.SaveAsync();

                var response = mapper.Map<RegistrationResponse>(user);
                return response;
            }
            catch (Exception exception)
            {
                Log.Error("При попытке регистрации пользователя возникла непредвиденная ошибка:", exception);
                throw;
            }
        }
    }
}
