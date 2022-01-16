using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Commands;
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
        /// <returns>Задача, которая содержит результат выполнения аутентификации</returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthenticateResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] LoginQuery query, CancellationToken token)
        {
            AuthenticateResponse response = await mediator.Send(query, token);
            return Ok(response);
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="command">Запрос на регистрацию</param>
        /// <param name="token">Токен для отмены операции</param>
        /// <returns>Задача, которая содержит результат регистрации</returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(RegistrationResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Post([FromBody] RegisterCommand command, CancellationToken token)
        {
            RegistrationResponse response = await mediator.Send(command, token);
            return Ok(response);
        }
    }
}
