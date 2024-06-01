using Linktic.Ecommerce.ProductsCatalog.Domain.Interfaces;
using Linktic.Ecommerce.ProductsCatalog.Domain.Models;
using Linktic.Ecommerce.ProductsCatalog.Infrastructure.Interfaces.Repositories;

namespace Linktic.Ecommerce.ProductsCatalog.Infrastructure.Repositories;

public class ProductRepository(IDatabaseClient databaseClient) : IProductRepository
{
    public Task<List<Product>> GetAllProducts()
    {
        return databaseClient.GetItemsList();
    }
}