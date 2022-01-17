using MediatR;
using Store.Core.Models;
using System.Collections.Generic;

namespace Store.Core.Features.Queries
{
    /// <summary>
    /// Запрос на получение пользователей
    /// </summary>
    public class GetUsersQuery : IRequest<IEnumerable<UserResponse>>
    {
    }
}
