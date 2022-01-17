using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Store.Core.ErrorHandling.Exceptions;
using Store.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.ErrorHandling
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

        private static async Task HandleCustomException(CustomCoreException customCoreException, HttpContext httpContext)
        {
            var error = new Error
            {
                UserMessage = customCoreException.Message
            };
            var statusCode = (int)HttpStatusCode.BadRequest;
            await WriteErrorToContext(error, httpContext, statusCode);
        }

        private static async Task HandleDefaultException(Exception defaultException, HttpContext httpContext)
        {
            var error = new Error
            {
                UserMessage = "Произошла непредвиденная ошибка",
                ExceptionMessage = defaultException.Message,
                StackTrace = defaultException.StackTrace
            };
            var statusCode = (int)HttpStatusCode.InternalServerError;
            await WriteErrorToContext(error, httpContext, statusCode);
        }

        private static async Task WriteErrorToContext(Error error, HttpContext httpContext, int statusCode)
        {
            string json = JsonConvert.SerializeObject(error);
            HttpResponse response = httpContext.Response;
            response.StatusCode = statusCode;
            response.ContentType = "application/json";
            await response.WriteAsync(json);
        }
    }
}
