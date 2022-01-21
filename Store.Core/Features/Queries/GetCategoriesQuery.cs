using MediatR;
using Store.Core.Contracts;
using System.Collections.Generic;

namespace Store.Core.Features.Queries
{
    public class GetCategoriesQuery : IRequest<List<CategoryResponse>>
    {
    }
}
