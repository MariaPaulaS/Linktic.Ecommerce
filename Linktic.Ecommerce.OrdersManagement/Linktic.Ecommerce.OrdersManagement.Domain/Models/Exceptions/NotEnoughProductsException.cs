using System.Net;

namespace Linktic.Ecommerce.OrdersManagement.Domain.Models.Exceptions;

public class NotEnoughProductsException : Exception
{
    public readonly string CustomMessage;
    public readonly HttpStatusCode StatusCode;
    
    public NotEnoughProductsException()
    {
        StatusCode = HttpStatusCode.NotFound;
        CustomMessage = "There aren't enough units availables for this product";
    }
}