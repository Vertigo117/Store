using AutoMapper;
using Store.Core.Commands;
using Store.Core.Models;
using Store.Data.Entities;
using System.Collections.Generic;

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
        }
    }
}
