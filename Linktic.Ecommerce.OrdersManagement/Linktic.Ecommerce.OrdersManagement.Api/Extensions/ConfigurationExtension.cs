using Linktic.Ecommerce.OrdersManagement.Api.LocalStack.Seeders;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Linktic.Ecommerce.OrdersManagement.Api.Extensions;

public static class ConfigurationExtension
{
    public static async Task ConfigureLocalRunning(this IConfigurationBuilder configuration)
    {
        await DynamoDbSeeder.CreateTable();
        await DynamoDbSeeder.PopulateOrdersTable();
        Log.Information("Ready to run!");
    }

}