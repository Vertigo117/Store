using Microsoft.AspNetCore.Builder;
using Store.Api.Middlware;

namespace Store.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseCustomErrorHandlingMiddlware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
