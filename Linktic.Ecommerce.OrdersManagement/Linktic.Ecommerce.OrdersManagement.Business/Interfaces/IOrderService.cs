using Linktic.Ecommerce.OrdersManagement.Domain.Models;
using Linktic.Ecommerce.OrdersManagement.Domain.Models.Requests;

namespace Linktic.Ecommerce.OrdersManagement.Business.Interfaces;

public interface IOrderService
{
    Task<List<Order>> GetAllOrders();
    Task CreateNewOrder(CreateOrderRequest createOrderRequest);
}