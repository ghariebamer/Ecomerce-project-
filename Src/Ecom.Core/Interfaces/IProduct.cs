using Ecom.Core.DTos;
using Ecom.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Interfaces
{
    public interface IProduct:IGenericRepo<Product>
    {
        //for add behaviour for product Only
        Task<bool> AddProduct(CreateProductDto productDto);
        Task<bool> UpdateProduct(int id,UpdateProductDto productDto);
       
    }
}
