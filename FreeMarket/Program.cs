using CartModule.Application;
using ProductModule.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient();
builder.Services.AddSingleton<IConfiguration>(provider=> new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true).Build());

ProductServices.InjectServices(builder.Services);
CartServices.InjectServices(builder.Services);

builder.Services.AddControllers();
var app = builder.Build();

app.MapControllers();

app.Run();
