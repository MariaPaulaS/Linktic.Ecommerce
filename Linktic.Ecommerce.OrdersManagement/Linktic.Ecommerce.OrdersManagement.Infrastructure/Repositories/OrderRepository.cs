using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Linktic.Ecommerce.OrdersManagement.Domain.Models;
using Linktic.Ecommerce.OrdersManagement.Domain.Models.Entities;
using Linktic.Ecommerce.OrdersManagement.Infrastructure.Interfaces;
using Linktic.Ecommerce.OrdersManagement.Infrastructure.Interfaces.Repositories;
using Newtonsoft.Json;

namespace Linktic.Ecommerce.OrdersManagement.Infrastructure.Repositories;

public class OrderRepository(IDatabaseClient databaseClient) : IOrderRepository
{
    private readonly DynamoDBContext _dynamoDbContext = databaseClient.GetContext();

    public async Task<List<Order>> GetAllOrders()
    {
        var listProducts = new List<Order>();
        try
        {
            var conditions = new List<ScanCondition>();
            listProducts = await _dynamoDbContext.ScanAsync<Order>(conditions).GetRemainingAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error scaning the table, details: {ex.Message}");
        }

        return listProducts;
    }

    public async Task CreateNewOrder(NewOrderInfo newOrderInfo)
    {
        var newOrderDetails = JsonConvert.SerializeObject(newOrderInfo.Details);
        
        var request = new PutItemRequest()
        {
            TableName = "Orders",
            Item = new Dictionary<string, AttributeValue>()
            {
                { "Id", new AttributeValue() { S = newOrderInfo.Id } },
                { "CustomerName", new AttributeValue() { S = newOrderInfo.CustomerName } },
                { "Details", new AttributeValue() { S = newOrderDetails } },
                { "Total", new AttributeValue() { N = newOrderInfo.Total.ToString() } }
            }
        };

        await databaseClient.GetConnection().PutItemAsync(request);    
    }
}