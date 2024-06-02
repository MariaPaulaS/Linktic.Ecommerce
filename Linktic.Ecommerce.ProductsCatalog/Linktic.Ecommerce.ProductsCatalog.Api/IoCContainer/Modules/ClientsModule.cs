using Amazon.DynamoDBv2;
using Linktic.Ecommerce.ProductsCatalog.Api.LocalStack.Seeders;
using Linktic.Ecommerce.ProductsCatalog.Infrastructure.Clients;
using Linktic.Ecommerce.ProductsCatalog.Infrastructure.Interfaces.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace Linktic.Ecommerce.ProductsCatalog.Api.IoCContainer.Modules;

public static class ClientsModule
{
    public static void ConfigureClients(this IServiceCollection services)
    {
        services.AddSingleton<IAmazonDynamoDB, AmazonDynamoDBClient>(_ => DynamoDbSeeder.DynamoDbClient);
        services.AddSingleton<IDatabaseClient, DynamoDbClient>();
    }
}