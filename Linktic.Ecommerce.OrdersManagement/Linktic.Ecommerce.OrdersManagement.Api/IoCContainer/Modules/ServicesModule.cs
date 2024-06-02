using Linktic.Ecommerce.OrdersManagement.Business.Interfaces;
using Linktic.Ecommerce.OrdersManagement.Business.Services;
using Linktic.Ecommerce.OrdersManagement.Infrastructure.Interfaces;
using Linktic.Ecommerce.OrdersManagement.Infrastructure.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Linktic.Ecommerce.OrdersManagement.Api.IoCContainer.Modules;

public static class ServicesModule
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddSingleton<IOrderService, OrderService>(provider =>
        {
            var orderRepository = provider.GetRequiredService<IOrderRepository>();
            var productRepository = provider.GetRequiredService<IProductCatalogRepository>();

            return new OrderService(orderRepository, productRepository);
        });
    }
}