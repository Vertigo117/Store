using AutoMapper;
using Store.Core.Contracts;
using Store.Data.Entities;

namespace Store.Core.MappingProfiles
{
    /// <summary>
    /// Настройки маппинга для категории товаров
    /// </summary>
    public class CategoryProfile : Profile
    {
        /// <summary>
        /// Создаёт новый экземпляр класса <seealso cref="CategoryProfile"/>
        /// </summary>
        public CategoryProfile()
        {
            CreateMap<Category, CategoryResponse>().ReverseMap();
        }
    }
}
