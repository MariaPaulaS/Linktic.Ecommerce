using Linktic.Ecommerce.OrdersManagement.Api.IoCContainer.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace Linktic.Ecommerce.OrdersManagement.Api.IoCContainer;

public class IoCServiceCollection
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.ConfigureClients();
        services.ConfigureRepositories();
        services.ConfigureServices();
    }
}
