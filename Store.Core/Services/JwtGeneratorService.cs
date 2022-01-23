using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Store.Core.Interfaces;
using Store.Core.Models;
using Store.Data.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Store.Core.Services
{
    /// <summary>
    /// Генератор jwt-токенов
    /// </summary>
    public class JwtGeneratorService : IJwtGeneratorService
    {
        private readonly AuthSettings authSettings;

        /// <summary>
        /// Создаёт новый экземпляр класса <seealso cref="JwtGeneratorService"/>
        /// с настройками авторизации
        /// </summary>
        /// <param name="authSettings">Настройки авторизации</param>
        public JwtGeneratorService(IOptions<AuthSettings> authSettings)
        {
            this.authSettings = authSettings.Value;
        }

        ///<inheritdoc/>
        public string GenerateForUser(User user)
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
