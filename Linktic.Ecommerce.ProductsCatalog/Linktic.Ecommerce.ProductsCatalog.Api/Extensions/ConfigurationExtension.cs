using Linktic.Ecommerce.ProductsCatalog.Api.LocalStack.Containers;
using Linktic.Ecommerce.ProductsCatalog.Api.LocalStack.Seeders;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Linktic.Ecommerce.ProductsCatalog.Api.Extensions;

public static class ConfigurationExtension
{
    private static LocalStackContainer? _localStackTestContainer;
    private static LocalStackContainer LocalStackTestContainer => _localStackTestContainer ??= new LocalStackContainer();
    
    public static async Task ConfigureLocalRunning(this IConfigurationBuilder configuration)
    {
        await LocalStackTestContainer.InitializeAsync();
        await DynamoDbSeeder.CreateTable();
        await DynamoDbSeeder.PopulateProductsCatalogTable();
        Log.Information("Ready to run!");
    }

}