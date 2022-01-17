using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Data.Contexts;
using Store.Data.Interfaces;
using Store.Data.Repositories;

namespace Store.Data.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<StoreContext>(options => options.UseNpgsql(connectionString));

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
    }
}
