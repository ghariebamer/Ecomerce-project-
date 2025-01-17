﻿using Ecom.WebApi.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Ecom.WebApi.Extenions
{
    public static class ApiRegesterion
    {
     
        public static IServiceCollection AddRegestionServices(this IServiceCollection services,IConfiguration config)
        {
            // add Auto Mapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //////////////////////////////////
            // configur IFileprovider
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
                );


            ////////////////////////////////////
            services.Configure<ApiBehaviorOptions>(
            o =>
            {
                o.InvalidModelStateResponseFactory = option =>
                {
                    var resonse = new ValidtionApiResponse()
                    {
                        Errors = option.ModelState.Where(x => x.Value.Errors.Count() > 0).
                        SelectMany(a => a.Value.Errors).Select(e => e.ErrorMessage).ToList()
                    };
                    return new BadRequestObjectResult(resonse);
                };
            }
            );

            // add Cors origin 

            services.AddCors(o =>
            {
                o.AddPolicy("CorsPolicy", op =>
                {
                    op.AllowAnyMethod();
                    op.AllowAnyHeader();
                    op.WithOrigins("http://localhost:4200");
                });
            });
         

            return services;
        }
    }
}
