using AutoMapper;
using Store.Core.Features.Commands;
using Store.Core.Models;
using Store.Data.Entities;

namespace Store.Core.Configuration
{
    /// <summary>
    /// Настройки для маппинга AutoMapper
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Создаёт новый экземпляр класса <seealso cref="MappingProfile"/>
        /// </summary>
        public MappingProfile()
        {
            CreateMap<User, RegisterCommand>().ReverseMap();
            CreateMap<User, RegistrationResponse>().ReverseMap();
            CreateMap<User, AuthenticateResponse>().ReverseMap();
            CreateMap<User, UserResponse>().ReverseMap();
            CreateMap<User, UpdateUserCommand>().ReverseMap();
        }
    }
}
