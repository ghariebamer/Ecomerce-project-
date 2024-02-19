using AutoMapper;
using Ecom.Core.DTos;
using Ecom.Core.Entities;

namespace Ecom.WebApi.Helper
{
    public class ProductURlReslover : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration config;

        public ProductURlReslover(IConfiguration config)
        {
            this.config = config;
        }


        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.ImageName)) { 

                return config["ApiURL"]+source.ImageName;
            }
            return null;
        }
    }
}
