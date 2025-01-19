using FreeMarket.Domain.Interfaces;
using ProductModule.Application;
using ProductModule.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IService<Product>,ProductService>();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
