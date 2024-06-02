using Linktic.Ecommerce.OrdersManagement.Business.Interfaces;
using Linktic.Ecommerce.OrdersManagement.Domain.Models;
using Linktic.Ecommerce.OrdersManagement.Domain.Models.Requests;
using Linktic.Ecommerce.OrdersManagement.Infrastructure.Interfaces.Repositories;

namespace Linktic.Ecommerce.OrdersManagement.Business.Services;

public class OrderService(IOrderRepository orderRepository): IOrderService
{
    public Task<List<Order>> GetAllOrders()
    {
        throw new NotImplementedException();
    }

    public Task CreateNewOrder(CreateOrderRequest createOrderRequest)
    {
        throw new NotImplementedException();
    }
}