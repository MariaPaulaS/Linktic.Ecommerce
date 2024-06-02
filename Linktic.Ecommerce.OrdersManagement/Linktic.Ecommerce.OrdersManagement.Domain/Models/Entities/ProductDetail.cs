namespace Linktic.Ecommerce.OrdersManagement.Domain.Models.Entities;

public class ProductDetail
{
    public string Id { get; set; }
    public string? ProductName { get; set; }
    public int Quantity { get; set; }
    public int UnitPrice { get; set; }
}