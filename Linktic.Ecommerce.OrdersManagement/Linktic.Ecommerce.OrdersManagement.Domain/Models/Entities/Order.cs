using Amazon.DynamoDBv2.DataModel;

namespace Linktic.Ecommerce.OrdersManagement.Domain.Models;

[DynamoDBTable("Orders")]
public class Order
{
    [DynamoDBHashKey]
    public string Id { get; set; }
    
    [DynamoDBProperty]
    public string CustomerName { get; set; }
    
    [DynamoDBProperty]
    public string Details { get; set; }
    
    [DynamoDBProperty]
    public int Total { get; set; }
}