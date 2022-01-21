using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using Store.Api.Contracts;
using Store.Core.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Store.Api.Middlware
{
    /// <summary>
    /// Компонент middleware для кастомной обработки ошибок
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        /// <summary>
        /// Создаёт новый экземпляр класса <seealso cref="ErrorHandlingMiddleware"/>
        /// c делегатом, который содержит следующий компонент в пайплайне
        /// </summary>
        /// <param name="next">Делегат со следующим компонентом в пайплайне</param>
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next?.Invoke(httpContext);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(exception, httpContext);
                Log.Error(exception, $"При выполнении запроса {httpContext.Request.Path} произошла ошибка: ");
            }
        }

        private static async Task HandleExceptionAsync(Exception exception, HttpContext httpContext)
        {
            switch (exception)
            {
                case CustomCoreException customException:
                    await HandleCustomException(customException, httpContext);
                    break;

                default:
                    await HandleDefaultException(exception, httpContext);
                    break;
            }
        }

        private static async Task HandleCustomException(
            CustomCoreException customCoreException, 
            HttpContext httpContext)
        {
            var error = new ErrorResponse
            {
                UserMessage = customCoreException.Message
            };
            var statusCode = (int)HttpStatusCode.BadRequest;
            await WriteErrorToContext(error, httpContext, statusCode);
        }

        private static async Task HandleDefaultException(Exception defaultException, HttpContext httpContext)
        {
            var error = new ErrorResponse
            {
                UserMessage = "Произошла непредвиденная ошибка",
                ExceptionMessage = defaultException.Message,
                StackTrace = defaultException.StackTrace
            };
            var statusCode = (int)HttpStatusCode.InternalServerError;
            await WriteErrorToContext(error, httpContext, statusCode);
        }

        private static async Task WriteErrorToContext(ErrorResponse error, HttpContext httpContext, int statusCode)
        {
            string json = JsonConvert.SerializeObject(error);
            HttpResponse response = httpContext.Response;
            response.StatusCode = statusCode;
            response.ContentType = "application/json";
            await response.WriteAsync(json);
        }
    }
}
