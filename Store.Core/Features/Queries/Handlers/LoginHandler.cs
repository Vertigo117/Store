using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Store.Core.Contracts;
using Store.Core.ErrorHandling.Exceptions;
using Store.Core.Models;
using Store.Data.Entities;
using Store.Data.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Store.Core.Features.Queries.Handlers
{
    /// <summary>
    /// Обработчик запроса на аутентификацию
    /// </summary>
    public class LoginHandler : IRequestHandler<LoginQuery, AuthenticateResponse>
    {
        private readonly IRepositoryWrapper repository;
        private readonly IMapper mapper;
        private readonly AuthSettings authSettings;

        /// <summary>
        /// Создаёт новый экземпляр класса <seealso cref="LoginHandler"/> с репозиторием бд,
        /// автомаппером и настройками аутентификации
        /// </summary>
        /// <param name="repository">Репозиторий бд</param>
        /// <param name="mapper">Автомаппер</param>
        /// <param name="authSettings">настройки аутентификации</param>
        /// <exception cref="ArgumentNullException"/>
        public LoginHandler(IRepositoryWrapper repository, IMapper mapper, IOptions<AuthSettings> authSettings)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.authSettings = authSettings.Value;
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
            User user = (await repository.Users.GetAsync(user => user.Login == request.Login))
                .FirstOrDefault(user => BCryptNet.Verify(request.Password, user.Password));

            if (user == null)
            {
                throw new CustomCoreException("Неправильный логин или пароль");
            }

            var response = mapper.Map<AuthenticateResponse>(user);
            response.Token = GenerateToken(user);
            return response;
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(authSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(authSettings.LifeTimeDays),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
