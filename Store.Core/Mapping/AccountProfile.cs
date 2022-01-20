using AutoMapper;
using Store.Core.Features.Commands;
using Store.Core.Models;
using Store.Data.Entities;

namespace Store.Core.Mapping
{
    /// <summary>
    /// Настройки для маппинга AutoMapper
    /// </summary>
    public class AccountProfile : Profile
    {
        /// <summary>
        /// Создаёт новый экземпляр класса <seealso cref="AccountProfile"/>
        /// </summary>
        public AccountProfile()
        {
            CreateMap<User, RegisterCommand>().ReverseMap();
            CreateMap<User, RegistrationResponse>().ReverseMap();
            CreateMap<User, AuthenticateResponse>().ReverseMap();
            CreateMap<User, UserResponse>().ReverseMap();
            CreateMap<User, UpdateUserCommand>().ReverseMap();
        }
    }
}
