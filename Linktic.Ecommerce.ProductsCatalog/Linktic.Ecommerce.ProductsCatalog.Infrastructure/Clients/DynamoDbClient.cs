using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Linktic.Ecommerce.ProductsCatalog.Domain.Models;
using Linktic.Ecommerce.ProductsCatalog.Infrastructure.Interfaces.Clients;

namespace Linktic.Ecommerce.ProductsCatalog.Infrastructure.Clients;

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