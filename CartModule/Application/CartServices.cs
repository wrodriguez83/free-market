using CartModule.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace CartModule.Application
{
    public class CartServices
    {

        public static void InjectServices(IServiceCollection services)
        {
            services.AddSingleton<CartRepository>();
            services.AddSingleton<CartItemRepository>();
            services.AddSingleton<CartService>();
            services.AddSingleton<CartItemService>();
        }
    }
}
