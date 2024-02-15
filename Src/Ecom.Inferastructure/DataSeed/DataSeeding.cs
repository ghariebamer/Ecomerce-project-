using Ecom.Core.Entities;
using Ecom.Inferastructure.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ecom.Inferastructure.DataSeed
{
    public static class DataSeeding
    {
        public  static async void Seed(ApplicationDbContext context)
        {
            if (!context.Categories.Any())
            {
                string categoryJson = System.IO.File.ReadAllText(@"D:\Ecomerce Project\Ecom\Src\Ecom.Inferastructure\DataSeed\Category.json");
                List<Category> categories=JsonConvert.DeserializeObject<List<Category>>(categoryJson);
                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }
            if(!context.Products.Any()) {
                string productJson = System.IO.File.ReadAllText(@"D:\Ecomerce Project\Ecom\Src\Ecom.Inferastructure\DataSeed\Product.json");
                List<Product> products=JsonConvert.DeserializeObject<List<Product>>(productJson);
                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
    }
}
