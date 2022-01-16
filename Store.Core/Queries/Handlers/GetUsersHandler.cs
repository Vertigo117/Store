using AutoMapper;
using MediatR;
using Serilog;
using Store.Core.Models;
using Store.Data.Entities;
using Store.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Core.Queries.Handlers
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserResponse>>
    {
        private readonly IRepository<User> userRepository;
        private readonly IMapper mapper;

        public GetUsersHandler(IRepository<User> userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var users = await userRepository.GetAsync();
                var response = mapper.Map<IEnumerable<UserResponse>>(users);
                return response;
            }
            catch (Exception exception)
            {
                Log.Error(exception, "Произошла ошибка запроса на получение пользователей");
                throw;
            }
        }
    }
}
