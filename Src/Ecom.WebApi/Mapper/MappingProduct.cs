using AutoMapper;
using Ecom.Core.DTos;
using Ecom.Core.Entities;
using Ecom.WebApi.Helper;

namespace Ecom.WebApi.Mapper
{
    public class MappingProduct:Profile
    {
        public MappingProduct()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(des=>des.productPicture,o =>o.MapFrom<ProductURlReslover>())
                .ReverseMap();  
            CreateMap<Product,CreateProductDto>().ReverseMap();
            CreateMap<Product,UpdateProductDto>().ReverseMap();
            CreateMap<UpdateProductDto, ProductDto>().ReverseMap();
        }
    }
}
