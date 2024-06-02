using Linktic.Ecommerce.ProductsCatalog.Domain.Models;
using Linktic.Ecommerce.ProductsCatalog.Domain.Models.Entities;
using Linktic.Ecommerce.ProductsCatalog.Domain.Models.Requests;

namespace Linktic.Ecommerce.ProductsCatalog.Business.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAllProducts();
    Task<List<Product>> GetAvailableProducts();
    Task UpdateProductQuantity(UpdateQuantityRequest request);
    Task<Product> GetProductById(string id);
}