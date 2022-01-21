using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Store.Core.MappingProfiles;
using System.Reflection;

namespace Store.Core.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
