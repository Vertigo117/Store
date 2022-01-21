using AutoMapper;
using MediatR;
using Store.Core.Contracts;
using Store.Data.Entities;
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
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, List<CategoryResponse>>
    {
        private readonly IRepository<Category> repository;
        private readonly IMapper mapper;

        /// <summary>
        /// Создаёт новый экземпляр класса <seealso cref="GetCategoriesHandler"/>
        /// с репозиторием и автомаппером
        /// </summary>
        /// <param name="repository">Репозиторий</param>
        /// <param name="mapper">Автомаппер</param>
        /// <exception cref="ArgumentNullException"></exception>
        public GetCategoriesHandler(IRepository<Category> repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<CategoryResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await repository.GetAllAsync();
            var response = mapper.Map<List<CategoryResponse>>(categories);
            return response;
        }
    }
}
