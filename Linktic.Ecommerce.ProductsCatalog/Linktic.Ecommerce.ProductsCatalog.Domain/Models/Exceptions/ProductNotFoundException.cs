using System.Net;

namespace Linktic.Ecommerce.ProductsCatalog.Domain.Models.Exceptions;

public class ProductNotFoundException : Exception
{
    public readonly string CustomMessage;
    public readonly HttpStatusCode StatusCode;
    
    public ProductNotFoundException()
    {
        StatusCode = HttpStatusCode.NoContent;
        CustomMessage = "The product doesn't exist in the catalog";
    }
}