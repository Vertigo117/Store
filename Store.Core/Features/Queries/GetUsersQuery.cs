using MediatR;
using Store.Core.Contracts;
using System.Collections.Generic;

namespace Store.Core.Features.Queries
{
    /// <summary>
    /// Запрос на получение пользователей
    /// </summary>
    public class GetUsersQuery : IRequest<List<UserResponse>>
    {
    }
}
