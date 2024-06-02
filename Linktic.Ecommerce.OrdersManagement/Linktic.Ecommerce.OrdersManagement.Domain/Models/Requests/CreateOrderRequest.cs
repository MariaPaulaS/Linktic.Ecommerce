using Linktic.Ecommerce.OrdersManagement.Domain.Models.Entities;

namespace Linktic.Ecommerce.OrdersManagement.Domain.Models.Requests;

public class CreateOrderRequest
{
    private string CustomerName { get; set; }
    private List<ProductDetail> Details { get; set; }
}