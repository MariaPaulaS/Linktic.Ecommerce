using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Serilog;

namespace Linktic.Ecommerce.OrdersManagement.Api.LocalStack.Seeders;

public class DynamoDbSeeder
{
    private static readonly AmazonDynamoDBConfig _dynamoDbConfig = new AmazonDynamoDBConfig
    {
        RegionEndpoint = RegionEndpoint.USEast1,
        UseHttp = true,
        ServiceURL = "http://localhost:4566"
    };
    
    public static AmazonDynamoDBClient DynamoDbClient => new AmazonDynamoDBClient("123", "123", _dynamoDbConfig);

    public static async Task CreateTable()
    {
        try
        {
            var request = new CreateTableRequest()
            {
                TableName = "Orders",
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

    public static async Task PopulateOrdersTable()
    {
        await PutItemIntoOrdersTable("1", "Maria Sabogal", "", 10000);
        await PutItemIntoOrdersTable("2", "Mateo Aponte", "", 10000);
    }
    
    private static async Task PutItemIntoOrdersTable(string id, string customerName, string details, int total)
    {
        var request = new PutItemRequest()
        {
            TableName = "Orders",
            Item = new Dictionary<string, AttributeValue>()
            {
                { "Id", new AttributeValue() { S = id } },
                { "CustomerName", new AttributeValue() { S = customerName } },
                { "Details", new AttributeValue() { S = details } },
                { "Total", new AttributeValue() { N = total.ToString() } }
            }
        };
        
        await DynamoDbClient.PutItemAsync(request);
    }
}