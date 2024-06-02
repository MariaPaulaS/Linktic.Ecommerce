using Linktic.Ecommerce.OrdersManagement.Domain.Models;
using Linktic.Ecommerce.OrdersManagement.Domain.Models.Entities;

namespace Linktic.Ecommerce.OrdersManagement.Infrastructure.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<List<Order>> GetAllOrders();
    Task CreateNewOrder(NewOrderInfo newOrderInfo);
}