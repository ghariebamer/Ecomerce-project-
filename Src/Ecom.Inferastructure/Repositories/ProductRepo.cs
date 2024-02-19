using AutoMapper;
using Ecom.Core.DTos;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Ecom.Inferastructure.Data;
using Microsoft.Extensions.FileProviders;
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
            var currentProduct = context.Products.Find(id);
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
              context.Products.Update(product);
             await context.SaveChangesAsync();
            return true;
        }
    }
}
