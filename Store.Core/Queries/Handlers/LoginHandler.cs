﻿using AutoMapper;
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

namespace Store.Core.Queries.Handlers
{
    /// <summary>
    /// Обработчик запроса на аутентификацию
    /// </summary>
    public class LoginHandler : IRequestHandler<LoginQuery, UserResponse>
    {
        private readonly IRepository<User> userRepository;
        private readonly IMapper mapper;
        private readonly AuthSettings appSettings;

        /// <summary>
        /// Создаёт новый экземпляр класса <seealso cref="LoginHandler"/> с репозиторием пользователя,
        /// автомаппером и настройками аутентификации
        /// </summary>
        /// <param name="userRepository">Репозиторий пользователей</param>
        /// <param name="mapper">Автомаппер</param>
        /// <param name="appSettings">настройки аутентификации</param>
        public LoginHandler(IRepository<User> userRepository, IMapper mapper, IOptions<AuthSettings> appSettings)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.appSettings = appSettings.Value;
        }

        /// <summary>
        /// Обработать запрос на аутентификацию
        /// </summary>
        /// <param name="request">Запрос на аутентификацию</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Задача, которая содержит результат выполнения авторизации</returns>
        /// <exception cref="CustomCoreException">Ошибка аутентификации</exception>
        public async Task<UserResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
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
                var response = mapper.Map<UserResponse>(user);
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
            return new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.Role)
            });
        }

        private string GenerateJwtToken(ClaimsIdentity claimsIdentity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddDays(appSettings.LifeTimeDays),
                SigningCredentials = signingCredentials
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
