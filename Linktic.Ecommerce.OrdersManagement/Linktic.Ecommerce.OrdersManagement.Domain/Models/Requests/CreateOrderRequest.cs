using Linktic.Ecommerce.OrdersManagement.Domain.Models.Entities;

namespace Linktic.Ecommerce.OrdersManagement.Domain.Models.Requests;

public class CreateOrderRequest
{
    public string CustomerName { get; set; }
    public List<ProductDetail> ProductDetails { get; set; }
}