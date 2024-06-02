using Linktic.Ecommerce.ProductsCatalog.Domain.Interfaces;
using Linktic.Ecommerce.ProductsCatalog.Domain.Models;
using Linktic.Ecommerce.ProductsCatalog.Infrastructure.Interfaces.Repositories;

namespace Linktic.Ecommerce.ProductsCatalog.Infrastructure.Repositories;

public class ProductRepository(IDatabaseClient databaseClient) : IProductRepository
{
    public async Task<List<Product>> GetAllProducts()
    {
        return await databaseClient.GetItemsList();
    }

    public async Task UpdateProductQuantity(string id, int newQuantity)
    {
        await databaseClient.UpdateItemQuantity(id, newQuantity);
    }

    public async Task<Product> GetProductById(string id)
    {
        return await databaseClient.GetItemById(id);
    }
}