using CartModule.Domain;
using CartModule.Infrastructure;
using FreeMarket.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CartModule.Application
{
    public class CartInjector
    {
        public static void InjectServices(IServiceCollection services)
        {
            services.AddSingleton<IRepository<Cart>, SqlClientCartRepository>();
            services.AddSingleton<IRepository<CartItem>, SqlClientCartItemRepository>();
            services.AddSingleton<IModuleRepository<Cart>,CartRepository>();
            services.AddSingleton<IModuleRepository<CartItem>,CartItemRepository>();
            services.AddSingleton<IService<Cart>,CartService>();
        }
    }
}
