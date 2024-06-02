namespace Linktic.Ecommerce.OrdersManagement.Domain.Models.Entities;

public class ProductDetail
{
    public string ProductId { get; set; }
    public string? ProductName { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
}