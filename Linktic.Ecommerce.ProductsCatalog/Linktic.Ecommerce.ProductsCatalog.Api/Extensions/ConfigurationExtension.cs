using Microsoft.Extensions.Configuration;

namespace Linktic.Ecommerce.ProductsCatalog.Api.Extensions;

public static class ConfigurationExtension
{
    private static LocalStackContainer? _localStackTestContainer;
    private static LocalStackContainer LocalStackTestContainer => _localStackTestContainer ??= new LocalStackContainer();
    
    public static async Task ConfigureLocalRunning(this IConfigurationBuilder configuration)
    {
        await LocalStackTestContainer.InitializeAsync();
        
        configuration.SetBasePath(System.AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables() 
            .Build();

        configuration.AddSystemsManager("/ecommerce", TimeSpan.FromSeconds(500));
    }
    
    public static async Task ConfigureParameterStore(this IConfigurationBuilder configuration)
    {
        configuration.AddSystemsManager("/ecommerce", TimeSpan.FromSeconds(500));
    }
}