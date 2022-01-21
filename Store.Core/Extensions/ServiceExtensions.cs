using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Store.Core.Mapping;
using Store.Core.Models;
using System.Reflection;
using System.Text;

namespace Store.Core.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(typeof(AccountProfile));

            var authConfigSection = configuration.GetSection(nameof(AuthSettings));
            services.Configure<AuthSettings>(authConfigSection);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;

                    var authSettings = authConfigSection.Get<AuthSettings>();
                    byte[] key = Encoding.ASCII.GetBytes(authSettings.Secret);
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuerSigningKey = true
                    };
                });
        }
    }
}
