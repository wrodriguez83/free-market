using Microsoft.Extensions.DependencyInjection;
using ProductModule.Infrastructure;

namespace ProductModule.Application
{
    public class ProductServices
    {

        public static void InjectServices(IServiceCollection services)
        {
            services.AddSingleton<ProductRepository>();
            services.AddSingleton<ProductService>();
        }
    }
}
