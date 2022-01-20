using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Features.Commands;
using Store.Core.Features.Queries;
using Store.Core.Models;
using System;
using System.Collections.Generic;
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
        public async Task<IActionResult> Authenticate([FromBody] LoginQuery query, CancellationToken token)
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
        [HttpPost("{command}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(RegistrationResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Register([FromBody] RegisterCommand command, CancellationToken token)
        {
            RegistrationResponse response = await mediator.Send(command, token);
            return Ok(response);
        }

        /// <summary>
        /// Получить пользователей
        /// </summary>
        /// <returns>Задача, которая содержит результат выполнения запроса на получение пользователей</returns>
        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(IEnumerable<UserResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult> Get()
        {
            var users = await mediator.Send(new GetUsersQuery(), new CancellationToken());
            return Ok(users);
        }

        /// <summary>
        /// Обновить данные пользователя
        /// </summary>
        /// <param name="command">Запрос на обновление</param>
        /// <param name="token">Токен для отмены операции</param>
        /// <returns>Задача, которая содержит результат выполнения запроса на обновление пользователя</returns>
        /// <response code="403">У текущего пользователя нет прав для доступа к данной операции</response>
        [HttpPut("{command}")]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Put([FromBody] UpdateUserCommand command, CancellationToken token)
        {
            await mediator.Send(command, token);
            return Ok();
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        /// <param name="id">Уникальный идентификатор пользователя</param>
        /// <param name="token">Токен отмены операции</param>
        /// <returns>Результат выполнения запроса на удаление пользователя</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete([FromRoute] string id, CancellationToken token)
        {
            await mediator.Send(new DeleteUserCommand(id), token);
            return Ok();
        }
    }
}
