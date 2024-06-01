using Linktic.Ecommerce.ProductsCatalog.Business.Interfaces;
using Linktic.Ecommerce.ProductsCatalog.Business.Services;
using Linktic.Ecommerce.ProductsCatalog.Infrastructure.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Linktic.Ecommerce.ProductsCatalog.Api.IoCContainer.Modules;

public static class ServicesModule
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddSingleton<IProductService, ProductService>(provider =>
        {
            var productRepository = provider.GetRequiredService<IProductRepository>();

            return new ProductService(productRepository);
        });
    }
}