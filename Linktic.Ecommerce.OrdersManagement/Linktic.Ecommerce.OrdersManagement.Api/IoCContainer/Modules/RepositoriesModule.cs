using Linktic.Ecommerce.OrdersManagement.Infrastructure.Interfaces;
using Linktic.Ecommerce.OrdersManagement.Infrastructure.Interfaces.Repositories;
using Linktic.Ecommerce.OrdersManagement.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Linktic.Ecommerce.OrdersManagement.Api.IoCContainer.Modules;

public static class RepositoriesModule
{
    public static void ConfigureRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IOrderRepository, OrderRepository>(provider =>
        {
            var databaseClient = provider.GetRequiredService<IDatabaseClient>();
            var dynamoTableName = configuration.GetRequiredSection("ordersManagement")["tableName"]!;

            return new OrderRepository(databaseClient, dynamoTableName);
        });

        services.AddSingleton<IProductCatalogRepository, ProductCatalogRepository>(provider =>
        {
            var productsHttpClient = new HttpClient()
            {
                BaseAddress = new Uri(configuration.GetRequiredSection("ordersManagement")["productCatalogUrl"]!),
            };
            return new ProductCatalogRepository(productsHttpClient);
        });
    }
}