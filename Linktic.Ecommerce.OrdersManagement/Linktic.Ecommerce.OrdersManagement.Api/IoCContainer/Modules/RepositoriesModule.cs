using Linktic.Ecommerce.OrdersManagement.Infrastructure.Interfaces;
using Linktic.Ecommerce.OrdersManagement.Infrastructure.Interfaces.Repositories;
using Linktic.Ecommerce.OrdersManagement.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Linktic.Ecommerce.OrdersManagement.Api.IoCContainer.Modules;

public static class RepositoriesModule
{
    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IOrderRepository, OrderRepository>(provider =>
        {
            var databaseClient = provider.GetRequiredService<IDatabaseClient>();

            return new OrderRepository(databaseClient);
        });
    }
}