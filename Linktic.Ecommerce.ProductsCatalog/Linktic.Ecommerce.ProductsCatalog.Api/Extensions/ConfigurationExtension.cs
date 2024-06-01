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

}