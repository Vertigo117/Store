using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Api.Contracts;
using Store.Core.Contracts;
using Store.Core.Features.Queries;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Api.Controllers
{
    /// <summary>
    /// Управление категориями товаров
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator mediator;

        public CategoriesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Получить все категории
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Результат выполнения запроса на получение всех категорий</returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<CategoryResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new GetCategoriesQuery(), cancellationToken);
            return Ok(response);
        }
    }
}
