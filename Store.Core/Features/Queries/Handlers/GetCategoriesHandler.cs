using AutoMapper;
using MediatR;
using Store.Core.Contracts;
using Store.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Features.Queries.Handlers
{
    /// <summary>
    /// Обработчик запроса на получение списка категорий
    /// </summary>
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryResponse>>
    {
        private readonly IRepositoryWrapper repository;
        private readonly IMapper mapper;

        /// <summary>
        /// Создаёт новый экземпляр класса <seealso cref="GetCategoriesHandler"/>
        /// с репозиторием и автомаппером
        /// </summary>
        /// <param name="repository">Репозиторий</param>
        /// <param name="mapper">Автомаппер</param>
        /// <exception cref="ArgumentNullException"></exception>
        public GetCategoriesHandler(IRepositoryWrapper repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<CategoryResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await repository.Categories.GetAsync();
            var response = mapper.Map<IEnumerable<CategoryResponse>>(categories);
            return response;
        }
    }
}
