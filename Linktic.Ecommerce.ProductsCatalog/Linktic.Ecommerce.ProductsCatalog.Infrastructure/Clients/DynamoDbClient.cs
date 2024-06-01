using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Linktic.Ecommerce.ProductsCatalog.Domain.Interfaces;
using Linktic.Ecommerce.ProductsCatalog.Domain.Models;

namespace Linktic.Ecommerce.ProductsCatalog.Infrastructure.Clients;

public class DynamoDbClient(IAmazonDynamoDB dynamoDbClient) : IDatabaseClient
{
    public async Task<List<Product>> GetItemsList()
    {
        var listProducts = new List<Product>();
        try
        {
            var context = new DynamoDBContext(dynamoDbClient);
            var conditions = new List<ScanCondition>();
            listProducts = await context.ScanAsync<Product>(conditions).GetRemainingAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error scaning the table, details: {ex.Message}");
        }

        return listProducts;
    }
}