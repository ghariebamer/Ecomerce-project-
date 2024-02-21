using Ecom.Inferastructure;
using Ecom.WebApi.Errors;
using Ecom.WebApi.Extenions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.InferaRegesteration(builder.Configuration);
        builder.Services.AddRegestionServices(builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        //app.UseMiddleware<ExpectionMiddleware>();
        app.ConfigureCustomExceptionMiddleware();

        app.UseStatusCodePagesWithReExecute("/errors/{0}");
        app.UseStaticFiles();
        app.UseHttpsRedirection();
        app.UseCors("CorsPolicy");
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}