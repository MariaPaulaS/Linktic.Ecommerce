namespace Linktic.Ecommerce.OrdersManagement.Domain.Models.Requests;

public class UpdateProductQuantityRequest
{
    public string ProductId;
    public int NumProductsToSum;
}