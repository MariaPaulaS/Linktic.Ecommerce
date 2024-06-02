using Linktic.Ecommerce.ProductsCatalog.Domain.Models;

namespace Linktic.Ecommerce.ProductsCatalog.Infrastructure.Interfaces.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAllProducts();
    Task UpdateProductQuantity(string id, int newQuantity);
    Task<Product> GetProductById(string id);
    
}