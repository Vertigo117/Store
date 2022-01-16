using MediatR;
using Store.Core.Models;
using System.Collections.Generic;

namespace Store.Core.Queries
{
    public class GetUsersQuery : IRequest<IEnumerable<UserResponse>>
    {
    }
}
