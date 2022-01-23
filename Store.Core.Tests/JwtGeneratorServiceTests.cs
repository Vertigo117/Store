using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;
using Store.Core.Interfaces;
using Store.Core.Models;
using Store.Core.Services;
using Store.Core.Tests.Helpers;
using Store.Core.Tests.Models;
using Store.Data.Entities;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Store.Core.Tests
{
    /// <summary>
    /// Тесты для сервиса генерации jwt-токенов
    /// </summary>
    [TestFixture]
    public class JwtGeneratorServiceTests
    {
        private readonly AuthSettings authSettings;
        private readonly IJwtGeneratorService jwtGenerator;

        public JwtGeneratorServiceTests()
        {
            authSettings = CreateAuthSettings();
            jwtGenerator = new JwtGeneratorService(Options.Create(authSettings));
        }

        private static AuthSettings CreateAuthSettings()
        {
            return new AuthSettings { Secret = "MySecretKey123455", LifeTimeDays = 1 };
        }

        [Test]
        public void GenerateForUser_NullArgumentPassed_ExceptionThrown()
        {
            //Arrange
            User user = null;
            Type expectedException = typeof(NullReferenceException);

            //Act
            TestDelegate action = () => jwtGenerator.GenerateForUser(user);

            //Assert
            Assert.Throws(expectedException, action);
        }

        [Test]
        public void GenerateForUser_JwtGenerated_JwtHasThreeParts()
        {
            //Arrange
            int expectedCount = 3;
            User user = CreateUser();

            //Act
            string jwt = jwtGenerator.GenerateForUser(user);
            int actualCount = jwt.Split('.').Length;

            //Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void GenerateForUser_JwtGenerated_JwtHeaderIsValid()
        {
            //Arrange
            User user = CreateUser();
            var expectedHeader = new Header { Alg = "HS256", Typ = "JWT" };

            //Act
            string jwtToken = jwtGenerator.GenerateForUser(user);
            Header actualHeader = TokenHelper.GetHeaderObject(jwtToken);

            //Assert
            Assert.AreEqual(expectedHeader, actualHeader);

        }

        [Test]
        public void GenerateForUser_JwtGenerated_JwtPayloadIsValid()
        {
            //Arrange
            var user = CreateUser();
            Payload expectedPayload = CreatePayload();

            //Act
            string jwt = jwtGenerator.GenerateForUser(user);
            Payload actualPayload = TokenHelper.GetPayloadObject(jwt);

            //Assert
            Assert.AreEqual(expectedPayload, actualPayload);
        }

        [Test]
        public void GenerateForUser_JwtGenerated_JwtSignatureIsValid()
        {
            //Arrange
            var user = CreateUser();

            //Act
            string jwt = jwtGenerator.GenerateForUser(user);
            var dataToSign = $"{TokenHelper.GetHeader(jwt)}.{TokenHelper.GetPayload(jwt)}";
            string expectedSignature = GetSha256Hash(dataToSign);
            string actualSignature = TokenHelper.GetSignature(jwt);

            //Assert
            Assert.AreEqual(expectedSignature, actualSignature);
        }

        private string GetSha256Hash(string text)
        {
            byte[] textBytes = Encoding.ASCII.GetBytes(text);

            using (var hmacSha256 = new HMACSHA256(Encoding.ASCII.GetBytes(authSettings.Secret)))
            {
                return Base64UrlEncoder.Encode(hmacSha256.ComputeHash(textBytes));
            }
        }

        private static User CreateUser()
        {
            return new User { Login = "testLogin", Role = "testRole" };
        }

        private Payload CreatePayload()
        {
            var timeNow = DateTimeOffset.UtcNow;
            return new Payload
            {
                Unique_name = "testLogin",
                Role = "testRole",
                Nbf = timeNow.ToUnixTimeSeconds().ToString(),
                Exp = timeNow.AddDays(authSettings.LifeTimeDays).ToUnixTimeSeconds().ToString(),
                Iat = timeNow.ToUnixTimeSeconds().ToString()
            };
        }
    }
}