using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Data.Contexts;
using Store.Data.Interfaces;
using Store.Data.Repositories;

namespace Store.Data.Extensions
{
    /// <summary>
    /// Содержит методы расширения для сервисов коллекции <seealso cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Конфигурирует сервисы, необходимые для работы с базой данных
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        /// <param name="configuration">Настройки конфигурации</param>
        public static void RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<StoreContext>(options => options.UseNpgsql(connectionString));

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
    }
}
