using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Store.Core.Interfaces;
using Store.Core.Services;
using System.Reflection;

namespace Store.Core.Extensions
{
    /// <summary>
    /// Метод расширения для регистрации сервисов
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Регистрирует ключевые зависимости для проекта
        /// </summary>
        /// <param name="services"></param>
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IUserCredentialsVerifier, UserCredentialsVerifier>();
        }
    }
}
