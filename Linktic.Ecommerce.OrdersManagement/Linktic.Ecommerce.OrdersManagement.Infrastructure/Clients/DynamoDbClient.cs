using System.Reflection.Metadata;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Linktic.Ecommerce.OrdersManagement.Infrastructure.Interfaces;

namespace Linktic.Ecommerce.OrdersManagement.Infrastructure.Clients;

public class DynamoDbClient(IAmazonDynamoDB dynamoDbClient) : IDatabaseClient
{
    public IAmazonDynamoDB GetConnection()
    {
        return dynamoDbClient;
    }

    public DynamoDBContext GetContext()
    {
        return new DynamoDBContext(dynamoDbClient);
    }
}