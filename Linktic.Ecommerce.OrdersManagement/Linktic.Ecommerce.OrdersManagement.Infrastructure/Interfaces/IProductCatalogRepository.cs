using Linktic.Ecommerce.OrdersManagement.Domain.Models.Entities;

namespace Linktic.Ecommerce.OrdersManagement.Infrastructure.Interfaces;

public interface IProductCatalogRepository
{
    Task<List<ProductDetail>?> FindAvailableProducts();
    void UpdateProductQuantity(StringContent content);
}