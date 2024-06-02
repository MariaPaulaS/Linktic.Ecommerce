using Linktic.Ecommerce.ProductsCatalog.Business.Interfaces;
using Linktic.Ecommerce.ProductsCatalog.Domain.Models;
using Linktic.Ecommerce.ProductsCatalog.Domain.Models.Exceptions;
using Linktic.Ecommerce.ProductsCatalog.Domain.Models.Requests;
using Linktic.Ecommerce.ProductsCatalog.Infrastructure.Interfaces.Repositories;

namespace Linktic.Ecommerce.ProductsCatalog.Business.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task<List<Product>> GetAllProducts()
    {
        return await productRepository.GetAllProducts();
    }

    public async Task<List<Product>> GetAvailableProducts()
    {
        var allProducts = await GetAllProducts();
        return allProducts.Where(p => p.Quantity > 0).ToList();
    }

    public async Task UpdateProductQuantity(UpdateQuantityRequest request)
    {
        var product = await GetProductById(request.productId);
        var actualQuantity = product.Quantity;
        var newQuantity = actualQuantity + request.numProductsToSum;
        if (newQuantity >= 0)
        {
            await productRepository.UpdateProductQuantity(request.productId, newQuantity);
        }
    }

    public async Task<Product> GetProductById(string id)
    {
        var product = await productRepository.GetProductById(id);
        if (product == null) throw new ProductNotFoundException();
        return product;
    }
}