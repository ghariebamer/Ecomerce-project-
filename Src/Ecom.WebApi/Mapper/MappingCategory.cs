using AutoMapper;
using Ecom.Core.DTos;
using Ecom.Core.Entities;

namespace Ecom.WebApi.Mapper
{
    public class MappingCategory:Profile
    {
        public MappingCategory()
        {
            CreateMap<CategoryDto, Category>().ForMember(des=> des.Name,opt=>opt.MapFrom(src=>src.Name)).ReverseMap();
            CreateMap<CategoryDto, Category>().ForMember(des=> des.Description,opt=>opt.MapFrom(src=>src.Descrption)).ReverseMap();
        }
    }
}
