using CartModule.Application;
using ProductModule.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<HttpClient>();
builder.Services.AddSingleton<IConfiguration>(provider=> new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true).Build());

ProductInjector.InjectServices(builder.Services);
CartInjector.InjectServices(builder.Services);

builder.Services.AddControllers();
var app = builder.Build();

app.MapControllers();

app.Run();
