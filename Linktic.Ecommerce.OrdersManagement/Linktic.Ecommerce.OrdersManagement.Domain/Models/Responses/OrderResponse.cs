namespace Linktic.Ecommerce.OrdersManagement.Domain.Models.Responses;

public class OrderResponse
{
    public string Id { get; set; }
    public string CustomerName { get; set; }
    public List<Dictionary<string, string>> Details { get; set; }
    public int Total { get; set; }
}