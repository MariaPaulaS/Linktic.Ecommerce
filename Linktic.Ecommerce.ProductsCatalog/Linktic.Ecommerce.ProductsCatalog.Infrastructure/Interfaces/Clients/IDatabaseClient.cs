using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace Linktic.Ecommerce.ProductsCatalog.Infrastructure.Interfaces.Clients;

public interface IDatabaseClient
{
    public IAmazonDynamoDB GetConnection();
    public DynamoDBContext GetContext();
}