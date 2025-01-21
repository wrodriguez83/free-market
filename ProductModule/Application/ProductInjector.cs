using FreeMarket.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using ProductModule.Domain;
using ProductModule.Infrastructure;
using StoreApiRepository;

namespace ProductModule.Application
{
    public class ProductInjector
    {
        public static void InjectServices(IServiceCollection services)
        {
            services.AddSingleton<IRepository<Product>,FakeStoreApiRepository<Product>>();
            services.AddSingleton<IModuleRepository<Product>, ProductRepository>();
            services.AddSingleton<IService<Product>,ProductService>();
        }
    }
}
