using Amazon.DynamoDBv2.DocumentModel;
using Linktic.Ecommerce.ProductsCatalog.Domain.Models;

namespace Linktic.Ecommerce.ProductsCatalog.Domain.Interfaces;

public interface IDatabaseClient
{
    Task<List<Product>> GetItemsList();

}