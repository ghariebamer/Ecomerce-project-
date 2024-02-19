using Ecom.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.DTos
{
    public  class baseProduct
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0, 5000, ErrorMessage = "Price must be between 0 and 5000")]
        [RegularExpression(@"[0-9]*")] // for accept only number
        public double Price { get; set; }
    }
    public  class ProductDto:baseProduct
    {
       
        public string CategoryName { get; set; }
        public string productPicture {  get; set; }

    }

    public class CreateProductDto : baseProduct
    {
        public int CategoryId { get; set; }

        public IFormFile ImageName { get; set; }
    }
    public class UpdateProductDto : baseProduct
    {
        public int CategoryId { get; set; }

        public IFormFile ImageName { get; set; }
    }
}
