using Ecom.Inferastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.InferaRegesteration(builder.Configuration);
// add Auto Mapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// configur IFileprovider
builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(
    Path.Combine( Directory.GetCurrentDirectory(),"wwwroot" ))
    );
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
