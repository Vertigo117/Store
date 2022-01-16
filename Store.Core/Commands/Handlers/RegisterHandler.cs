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

namespace Store.Core.Commands.Handlers
{
    /// <summary>
    /// Обработчик запроса на регистрацию
    /// </summary>
    internal class RegisterHandler : IRequestHandler<RegisterCommand, RegistrationResponse>
    {
        private readonly IRepository<User> userRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// Создаёт новый экземпляр класса <seealso cref="RegisterHandler"/> с репозиторием пользователей
        /// и автомаппером
        /// </summary>
        /// <param name="userRepository">Репозиторий пользователей</param>
        /// <param name="mapper">Автомаппер</param>
        public RegisterHandler(IRepository<User> userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Обрабатывает запрос на регистрацию пользователя
        /// </summary>
        /// <param name="request">Запрос на регистрацию</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Задача, которая содержит результат выполнения регистрации</returns>
        /// <exception cref="CustomCoreException">Ошибка регистрации</exception>
        public async Task<RegistrationResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                bool isLoginExists = (await userRepository.GetAsync()).Any(user => user.Login == request.Login);

                if (isLoginExists)
                {
                    throw new CustomCoreException($"Пользователь с логином '{request.Login}' уже существует");
                }

                request.Password = BCryptNet.HashPassword(request.Password);

                var user = mapper.Map<User>(request);
                await userRepository.CreateAsync(user);

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
