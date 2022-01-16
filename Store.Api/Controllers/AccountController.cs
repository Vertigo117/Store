using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Models;
using Store.Core.Queries;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator mediator;

        public AccountController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="query">Аутентификационный запрос</param>
        /// <param name="token">Токен для отмены операции</param>
        /// <returns>Задачу, которая содержит результат выполнения аутентификации</returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthenticateResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] LoginQuery query, CancellationToken token)
        {
            AuthenticateResponse response = await mediator.Send(query, token);
            return Ok(response);
        }
    }
}
