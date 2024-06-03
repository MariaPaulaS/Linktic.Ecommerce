using Linktic.Ecommerce.OrdersManagement.Api.IoCContainer.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Linktic.Ecommerce.OrdersManagement.Api.IoCContainer;

public class IoCServiceCollection
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureClients();
        services.ConfigureRepositories(configuration);
        services.ConfigureServices();
    }

}
