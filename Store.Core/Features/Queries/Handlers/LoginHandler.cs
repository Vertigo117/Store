using AutoMapper;
using MediatR;
using Store.Core.Contracts;
using Store.Core.Exceptions;
using Store.Core.Interfaces;
using Store.Data.Entities;
using Store.Data.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Features.Queries.Handlers
{
    /// <summary>
    /// Обработчик запроса на аутентификацию
    /// </summary>
    public class LoginHandler : IRequestHandler<LoginQuery, AuthenticateResponse>
    {
        private readonly IMapper mapper;
        private readonly IRepository<User> userRepository;
        private readonly IJwtGeneratorService jwtGenerator;
        private readonly IUserCredentialsService userCredentialsVerifier;

        /// <summary>
        /// Создаёт новый экземпляр класса <seealso cref="LoginHandler"/> с репозиторием бд,
        /// автомаппером, генератором jwt-токенов и верификацией пользовательских данных
        /// </summary>
        /// /// <param name="mapper">Автомаппер</param>
        /// <param name="userRepository">Репозиторий бд</param>
        ///<param name="jwtGenerator">Генератор jwt-токенов</param>
        ///<param name="userCredentialsVerifier">Класс для верификации данных пользователя</param>
        /// <exception cref="ArgumentNullException"/>
        public LoginHandler(
            IMapper mapper, 
            IRepository<User> userRepository, 
            IJwtGeneratorService jwtGenerator,
            IUserCredentialsService userCredentialsVerifier)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.jwtGenerator = jwtGenerator ?? throw new ArgumentNullException(nameof(jwtGenerator));
            this.userCredentialsVerifier = userCredentialsVerifier ?? throw new ArgumentNullException(nameof(userCredentialsVerifier));
        }

        /// <summary>
        /// Обработать запрос на аутентификацию
        /// </summary>
        /// <param name="request">Запрос на аутентификацию</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Задача, которая содержит результат выполнения авторизации</returns>
        /// <exception cref="CustomCoreException">Ошибка аутентификации</exception>
        public async Task<AuthenticateResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var users = await userRepository.GetAllAsync();
            User user = users.FirstOrDefault(user => VerifyRequestedCredentials(request, user));

            if (user == null)
            {
                throw new CustomCoreException("Неправильный логин или пароль");
            }

            var response = mapper.Map<AuthenticateResponse>(user);
            response.Token = jwtGenerator.GenerateForUser(user);
            return response;
        }

        private bool VerifyRequestedCredentials(LoginQuery request, User user)
        {
            return userCredentialsVerifier.VerifyRequestedCredentials(request, user);
        }
    }
}
