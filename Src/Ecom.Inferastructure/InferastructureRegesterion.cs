using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Ecom.Inferastructure.Data;
using Ecom.Inferastructure.DataSeed;
using Ecom.Inferastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Inferastructure
{
    public static class InferastructureRegesterion
    {
        // use this to be extention method for services Class
        public static IServiceCollection InferaRegesteration(this IServiceCollection services,IConfiguration config)
        {
            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            //services.AddScoped<ICategory,CategoryRepo>();
            //services.AddScoped<IProduct,ProductRepo>();
            services.AddScoped(typeof(IUnitOfWork),typeof(UnitOfWork));

            // for Add DbContext to connect to SQLserver
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            // for seed Data

            var context=services.BuildServiceProvider().GetRequiredService<ApplicationDbContext>();
            DataSeeding.Seed(context); 

            return services;
        }
    }
}
