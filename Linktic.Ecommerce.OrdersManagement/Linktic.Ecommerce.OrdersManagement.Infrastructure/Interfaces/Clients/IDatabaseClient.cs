using System.Reflection.Metadata;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace Linktic.Ecommerce.OrdersManagement.Infrastructure.Interfaces;

public interface IDatabaseClient
{
    public IAmazonDynamoDB GetConnection();
    public DynamoDBContext GetContext();
}