using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Linktic.Ecommerce.ProductsCatalog.Domain.Interfaces;
using Linktic.Ecommerce.ProductsCatalog.Domain.Models;

namespace Linktic.Ecommerce.ProductsCatalog.Infrastructure.Clients;

public class DynamoDbClient(IAmazonDynamoDB dynamoDbClient) : IDatabaseClient
{
    private DynamoDBContext _dynamoDbContext = new DynamoDBContext(dynamoDbClient);
    
    public async Task<List<Product>> GetItemsList()
    {
        var listProducts = new List<Product>();
        try
        {
            var conditions = new List<ScanCondition>();
            listProducts = await _dynamoDbContext.ScanAsync<Product>(conditions).GetRemainingAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error scaning the table, details: {ex.Message}");
        }

        return listProducts;
    }

    public async Task UpdateItemQuantity(string id, int newQuantity)
    {
        var request = new UpdateItemRequest
        {
            TableName = "ProductsCatalog",
            Key = new Dictionary<string, AttributeValue>
            {
                { "Id", new AttributeValue { S = id } }
            },
            AttributeUpdates = new Dictionary<string, AttributeValueUpdate>
            {
                { "Quantity", new AttributeValueUpdate
                    {
                        Action = AttributeAction.PUT,
                        Value = new AttributeValue { N = newQuantity.ToString() }
                    }
                }
            }
        };

        await dynamoDbClient.UpdateItemAsync(request);
    }

    public async Task<Product> GetItemById(string id)
    {
        var conditions = new List<ScanCondition>
        {
            new ScanCondition("Id", ScanOperator.Equal, id),
        };

        var search = _dynamoDbContext.ScanAsync<Product>(conditions);
        var products = await search.GetRemainingAsync();

        if (products.Count == 0) return null;
        
        return products[0];
    }
}