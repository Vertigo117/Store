using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
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
        private readonly IRepository<User> userRepository;
        private readonly IMapper mapper;
        private readonly AuthSettings authSettings;

        /// <summary>
        /// Создаёт новый экземпляр класса <seealso cref="LoginHandler"/> с репозиторием пользователя,
        /// автомаппером и настройками аутентификации
        /// </summary>
        /// <param name="userRepository">Репозиторий пользователей</param>
        /// <param name="mapper">Автомаппер</param>
        /// <param name="authSettings">настройки аутентификации</param>
        public LoginHandler(IRepository<User> userRepository, IMapper mapper, IOptions<AuthSettings> authSettings)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
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
            try
            {
                var user = (await userRepository.GetAsync(user => user.Login == request.Login))
                .FirstOrDefault(user => BCryptNet.Verify(request.Password, user.Password));

                if (user == null)
                {
                    throw new CustomCoreException("Неправильный логин или пароль");
                }

                ClaimsIdentity identity = GetIdentity(user);
                var token = GenerateJwtToken(identity);
                var response = mapper.Map<AuthenticateResponse>(user);
                response.Token = token;
                return response;
            }
            catch (Exception exception)
            {
                Log.Error(exception, "При выполнении запроса на аутентификацию произошла ошибка");
                throw;
            }
        }

        private static ClaimsIdentity GetIdentity(User user)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };
            return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, 
                ClaimsIdentity.DefaultRoleClaimType);
        }

        private string GenerateJwtToken(ClaimsIdentity claimsIdentity)
        {
            var jwt = new JwtSecurityToken(
                notBefore: DateTime.UtcNow,
                claims: claimsIdentity.Claims,
                expires: DateTime.UtcNow.AddDays(authSettings.LifeTimeDays),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authSettings.Secret)), 
                    SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
