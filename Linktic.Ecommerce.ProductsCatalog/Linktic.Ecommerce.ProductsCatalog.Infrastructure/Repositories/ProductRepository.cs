using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Linktic.Ecommerce.ProductsCatalog.Domain.Models.Entities;
using Linktic.Ecommerce.ProductsCatalog.Infrastructure.Interfaces.Clients;
using Linktic.Ecommerce.ProductsCatalog.Infrastructure.Interfaces.Repositories;

namespace Linktic.Ecommerce.ProductsCatalog.Infrastructure.Repositories;

public class ProductRepository(IDatabaseClient databaseClient) : IProductRepository
{
    private readonly DynamoDBContext _dynamoDbContext = databaseClient.GetContext();
    
    public async Task<List<Product>> GetAllProducts()
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

    public async Task UpdateProductQuantity(string id, int newQuantity)
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

        await databaseClient.GetConnection().UpdateItemAsync(request);
    }

    public async Task<Product> GetProductById(string id)
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