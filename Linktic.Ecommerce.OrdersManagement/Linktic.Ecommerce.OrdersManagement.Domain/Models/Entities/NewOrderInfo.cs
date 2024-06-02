namespace Linktic.Ecommerce.OrdersManagement.Domain.Models.Entities;

public class NewOrderInfo
{
    public string Id { get; set; }
    public string CustomerName { get; set; }
    public List<ProductDetail> Details { get; set; }
    public int Total { get; set; }
}