using AutoMapper;
using Store.Core.Commands;
using Store.Core.Models;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            CreateMap<User, RegistrationResponse>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Type))
                .ReverseMap();
            CreateMap<User, AuthenticateResponse>();
        }
    }
}
