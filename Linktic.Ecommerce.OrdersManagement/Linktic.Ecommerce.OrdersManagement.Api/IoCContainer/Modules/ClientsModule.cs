﻿using Amazon.DynamoDBv2;
using Linktic.Ecommerce.OrdersManagement.Api.LocalStack.Seeders;
using Linktic.Ecommerce.OrdersManagement.Infrastructure.Clients;
using Linktic.Ecommerce.OrdersManagement.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Linktic.Ecommerce.OrdersManagement.Api.IoCContainer.Modules;

public static class ClientsModule
{
    public static void ConfigureClients(this IServiceCollection services)
    {
        services.AddSingleton<IAmazonDynamoDB, AmazonDynamoDBClient>(_ => DynamoDbSeeder.DynamoDbClient);
        services.AddSingleton<IDatabaseClient, DynamoDbClient>();
    }
}