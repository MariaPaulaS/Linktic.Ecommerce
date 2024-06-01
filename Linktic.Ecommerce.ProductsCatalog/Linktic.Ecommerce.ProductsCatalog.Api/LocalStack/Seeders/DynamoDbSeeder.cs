using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

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
        var request = new CreateTableRequest()
        {
            TableName = "ProductsCatalog",
            KeySchema = [new KeySchemaElement("Id", KeyType.HASH)],
            AttributeDefinitions =
            [
                new AttributeDefinition { AttributeName = "Id", AttributeType = ScalarAttributeType.S },
                new AttributeDefinition { AttributeName = "Productname", AttributeType = ScalarAttributeType.S }
            ],
            ProvisionedThroughput = new ProvisionedThroughput { ReadCapacityUnits = 5, WriteCapacityUnits = 5 }
        };

        await DynamoDbClient.CreateTableAsync(request);
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
                { "Productname", new AttributeValue() { S = productname } }
            }
        };
        
        await DynamoDbClient.PutItemAsync(request);
    }
}