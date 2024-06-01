using Linktic.Ecommerce.ProductsCatalog.Domain.Interfaces;
using Linktic.Ecommerce.ProductsCatalog.Infrastructure.Interfaces.Repositories;
using Linktic.Ecommerce.ProductsCatalog.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Linktic.Ecommerce.ProductsCatalog.Api.IoCContainer.Modules;

public static class RepositoriesModule
{
    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IProductRepository, ProductRepository>(provider =>
        {
            var databaseClient = provider.GetRequiredService<IDatabaseClient>();

            return new ProductRepository(databaseClient);
        });
    }
}