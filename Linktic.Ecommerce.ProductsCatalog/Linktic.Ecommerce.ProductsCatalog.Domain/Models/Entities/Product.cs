using Amazon.DynamoDBv2.DataModel;

namespace Linktic.Ecommerce.ProductsCatalog.Domain.Models;

[DynamoDBTable("ProductsCatalog")]
public class Product
{
    [DynamoDBHashKey]
    public string Id { get; set; }
    
    [DynamoDBProperty]
    public string ProductName { get; set; }
    
    [DynamoDBProperty]
    public int Quantity { get; set; }
}