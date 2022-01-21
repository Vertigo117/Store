using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;
using Store.Core.Interfaces;
using Store.Core.Models;
using Store.Core.Services;
using Store.Data.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Store.Core.Tests
{
    /// <summary>
    /// Тесты для сервиса генерации jwt-токенов
    /// </summary>
    [TestFixture]
    public class JwtTokenGeneratorTests
    {
        private readonly IOptions<AuthSettings> options;
        private readonly IJwtTokenGenerator jwtTokenGenerator;

        public JwtTokenGeneratorTests()
        {
            options = Options.Create(CreateAuthSettings());
            jwtTokenGenerator = new JwtTokenGenerator(options);
        }

        [Test]
        public void CheckJwtTokenClaims()
        {
            ///Arrange
            var user = new User { Login = "testLogin", Role = "testRole" };

            ///Act
            var jwtToken = jwtTokenGenerator.GenerateForUser(user);
            ClaimsPrincipal claims = GetTokenClaims(jwtToken);
            string actualLogin = claims.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
            string actualRole = claims.Claims.First(claim => claim.Type == ClaimTypes.Role).Value;

            ///Assert
            Assert.AreEqual(user.Login, actualLogin);
            Assert.AreEqual(user.Role, actualRole);
        }

        private AuthSettings CreateAuthSettings()
        {
            return new AuthSettings { Secret = "MySecretKey123455", LifeTimeDays = 1 };
        }

        private ClaimsPrincipal GetTokenClaims(string jwtToken)
        {
            var authSettings = CreateAuthSettings();
            var key = Encoding.ASCII.GetBytes(authSettings.Secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claims = handler.ValidateToken(jwtToken, validations, out var tokenSecure);
            return claims;
        }
    }
}