using AutoMapper;
using Ecom.Core.DTos;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Ecom.Core.Shared;
using Ecom.Inferastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Inferastructure.Repositories
{
    public class ProductRepo : GenericRepo<Product>, IProduct
    {
        private readonly ApplicationDbContext context;
        private readonly IFileProvider fileProvider;
        private readonly IMapper imapper;

        public ProductRepo(ApplicationDbContext context , IFileProvider fileProvider, IMapper imapper) : base(context)
        {
            this.context = context;
            this.fileProvider = fileProvider;
            this.imapper = imapper;
        }

        public async Task<bool> AddProduct(CreateProductDto productDto)
        {
            var root = "/mages/Products/";
            if(!Directory.Exists("wwwroot" + root)) {
                Directory.CreateDirectory("wwwroot" + root);
                }
            if (productDto != null & productDto.ImageName != null)
            {
                var productImage = $"{Guid.NewGuid()}" + productDto.ImageName.FileName;
                var src = root + productImage;
                var pathInfo=fileProvider.GetFileInfo(src);
                var rootPath = pathInfo.PhysicalPath;
                using(var fileStream=new FileStream(rootPath,FileMode.Create))
                {
                    await productDto.ImageName.CopyToAsync(fileStream);
                }

                // create product with new path Image 
                Product product = imapper.Map<Product>(productDto);
                product.ImageName = src;
                await  context.Products.AddAsync(product);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public  async Task<bool> UpdateProduct(int id,UpdateProductDto productDto)
        {
            // for remove current Image
            var currentProduct = context.Products.AsNoTracking().FirstOrDefault(e=>e.Id==id);
            var src = "";
            var root = "/mages/Products/";
            if (!Directory.Exists("wwwroot" + root))
            {
                Directory.CreateDirectory("wwwroot" + root);
            }
            if (productDto.ImageName != null)
            {
                var productImage = $"{Guid.NewGuid()}" + productDto.ImageName.FileName;
                 src = root + productImage;
                var pathInfo = fileProvider.GetFileInfo(src);
                var rootPath = pathInfo.PhysicalPath;
                using (var fileStream = new FileStream(rootPath, FileMode.Create))
                {
                     await productDto.ImageName.CopyToAsync(fileStream);
                }            
            }
            // to delete old Image before update
            if (currentProduct.ImageName != null)
            {
                var pictureInfo=fileProvider.GetFileInfo(currentProduct.ImageName);
                var picturePath=pictureInfo.PhysicalPath;
                System.IO.File.Delete(picturePath);
            }
            Product product = imapper.Map<Product>(productDto);
            product.ImageName = src;
            product.Id = id;
              context.Products.Update(product);
             await context.SaveChangesAsync();
            return true;
        }
        
        public (IReadOnlyList<ProductDto>,int count) GetAll(ProductParams productParams)
        {
            var products = context.Products.AsNoTracking().Include(x=>x.Category).ToList();
            List<Product> Allproducts = context.Products.AsNoTracking().Include(x=>x.Category).ToList();
            if (productParams.CategoryId.HasValue)
                products=products.Where(x=>x.CategoryId== productParams.CategoryId.Value).ToList();
                if ( ! string.IsNullOrEmpty( productParams.Search))
                products=products.Where(e=>e.Name.ToLower()==productParams.Search).ToList();
          
            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                products = productParams.Sort switch
                {
                    "PriceAsc" => products.OrderBy(x => x.Price).ToList(),
                    "PriceDesc" => products.OrderByDescending(x => x.Price).ToList(),
                       _ => products.OrderBy(x => x.Name).ToList()
                };
            }
            products = products.Skip(productParams.PageSize * (productParams.PageNumber - 1)).Take(productParams.PageSize).ToList();
            var result = imapper.Map<IReadOnlyList<ProductDto>>(products);
            int  count = GetTotalCount(Allproducts);
            return (result,count);

        } 
    
    }
}
