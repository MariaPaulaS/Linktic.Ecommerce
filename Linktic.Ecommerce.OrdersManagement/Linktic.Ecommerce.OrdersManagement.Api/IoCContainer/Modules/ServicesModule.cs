using Linktic.Ecommerce.OrdersManagement.Business.Interfaces;
using Linktic.Ecommerce.OrdersManagement.Business.Services;
using Linktic.Ecommerce.OrdersManagement.Infrastructure.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Linktic.Ecommerce.OrdersManagement.Api.IoCContainer.Modules;

public static class ServicesModule
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddSingleton<IOrderService, OrderService>(provider =>
        {
            var productRepository = provider.GetRequiredService<IOrderRepository>();

            return new OrderService(productRepository);
        });
    }
}