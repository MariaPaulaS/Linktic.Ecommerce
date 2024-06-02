using Linktic.Ecommerce.OrdersManagement.Domain.Models;
using Linktic.Ecommerce.OrdersManagement.Domain.Models.Requests;
using Linktic.Ecommerce.OrdersManagement.Domain.Models.Responses;

namespace Linktic.Ecommerce.OrdersManagement.Business.Interfaces;

public interface IOrderService
{
    Task<List<OrderResponse>> GetAllOrders();
    Task CreateNewOrder(CreateOrderRequest createOrderRequest);
}