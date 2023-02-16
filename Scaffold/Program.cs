using Microsoft.EntityFrameworkCore;
using Scaffold;
using System.Text.Json.Serialization;
using Scaffold.Controllers;
using AutoMapper;
using Mapster;
using Scaffold.Models;
using Scaffold.DTO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("ScaffoldConnectionString")));

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); // LTPE
//builder.Services.AddAutoMapper(typeof(Program)); // LTPE

//TypeAdapterConfig<Product, ProductDTO>.NewConfig();
//TypeAdapterConfig<Product, ProductDTO>.NewConfig().Map(dest => dest.ProductID, src => src.ProductId);
TypeAdapterConfig<Product, ProductDTOPlusCategory>.NewConfig().Map(dest => dest.ProductID, src => src.ProductId);
TypeAdapterConfig<ProductDTOForUpdate, Product>.NewConfig().Map(dest => dest.ProductId, src => src.ProductID);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapProductEndpoints();

app.MapCategoryEndpoints();

app.Run();
