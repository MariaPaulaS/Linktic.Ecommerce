using Linktic.Ecommerce.ProductsCatalog.Api.LocalStack.Seeders;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Linktic.Ecommerce.ProductsCatalog.Api.Extensions;

public static class ConfigurationExtension
{
    public static async Task ConfigureLocalRunning(this IConfigurationBuilder configuration)
    {
        await DynamoDbSeeder.CreateTable();
        await DynamoDbSeeder.PopulateProductsCatalogTable();
        Log.Information("Ready to run!");
    }
    
    public static void ConfigureParameterStore(this IConfigurationBuilder configuration)
    {
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Testing")
            return;

        configuration.AddSystemsManager("/ecommerce", TimeSpan.FromSeconds(500));
    }

}