using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Linktic.Ecommerce.ProductsCatalog.Api.LocalStack.Containers;
using Serilog;

namespace Linktic.Ecommerce.ProductsCatalog.Api.LocalStack.Seeders;

public class DynamoDbSeeder
{
    private static readonly AmazonDynamoDBConfig _dynamoDbConfig = new AmazonDynamoDBConfig
    {
        RegionEndpoint = RegionEndpoint.USEast1,
        UseHttp = true,
        ServiceURL = LocalStackContainer.GetConnectionString()
    };
    
    public static AmazonDynamoDBClient DynamoDbClient => new AmazonDynamoDBClient("123", "123", _dynamoDbConfig);

    public static async Task CreateTable()
    {
        try
        {
            var request = new CreateTableRequest()
            {
                TableName = "ProductsCatalog",
                KeySchema = [new KeySchemaElement("Id", KeyType.HASH)],
                AttributeDefinitions =
                [
                    new AttributeDefinition { AttributeName = "Id", AttributeType = ScalarAttributeType.S }
                ],
                ProvisionedThroughput = new ProvisionedThroughput { ReadCapacityUnits = 5, WriteCapacityUnits = 5 }
            };

             await DynamoDbClient.CreateTableAsync(request);
        }
        catch (Exception e)
        {
            Log.Error($"Something was wrong creating the table in DynamoDB, details: {e.Message}");
        }

    }

    public static async Task PopulateProductsCatalogTable()
    {
        await PutItemIntoProductsCatalogTable("1", "Star necklace");
        await PutItemIntoProductsCatalogTable("2", "Leaf earrings");
    }
    
    
    private static async Task PutItemIntoProductsCatalogTable(string id, string productname)
    {

        var request = new PutItemRequest()
        {
            TableName = "ProductsCatalog",
            Item = new Dictionary<string, AttributeValue>()
            {
                { "Id", new AttributeValue() { S = id } },
                { "ProductName", new AttributeValue() { S = productname } }
            }
        };
        
        await DynamoDbClient.PutItemAsync(request);
    }
}