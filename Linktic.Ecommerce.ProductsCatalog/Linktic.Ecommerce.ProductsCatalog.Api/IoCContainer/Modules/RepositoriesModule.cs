using Linktic.Ecommerce.ProductsCatalog.Infrastructure.Interfaces.Clients;
using Linktic.Ecommerce.ProductsCatalog.Infrastructure.Interfaces.Repositories;
using Linktic.Ecommerce.ProductsCatalog.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Linktic.Ecommerce.ProductsCatalog.Api.IoCContainer.Modules;

public static class RepositoriesModule
{
    public static void ConfigureRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IProductRepository, ProductRepository>(provider =>
        {
            var databaseClient = provider.GetRequiredService<IDatabaseClient>();
            var dynamoTableName = configuration.GetRequiredSection("productsCatalog")["tableName"]!;


            return new ProductRepository(databaseClient, dynamoTableName);
        });
    }
}