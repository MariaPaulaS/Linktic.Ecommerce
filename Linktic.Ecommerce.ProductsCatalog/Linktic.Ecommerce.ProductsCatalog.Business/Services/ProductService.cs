using Linktic.Ecommerce.ProductsCatalog.Business.Interfaces;
using Linktic.Ecommerce.ProductsCatalog.Domain.Models;
using Linktic.Ecommerce.ProductsCatalog.Infrastructure.Interfaces.Repositories;

namespace Linktic.Ecommerce.ProductsCatalog.Business.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task<List<Product>> GetAllProducts()
    {
        return await productRepository.GetAllProducts();
    }
}