using Linktic.Ecommerce.ProductsCatalog.Domain.Models;

namespace Linktic.Ecommerce.ProductsCatalog.Business.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAllProducts();
}