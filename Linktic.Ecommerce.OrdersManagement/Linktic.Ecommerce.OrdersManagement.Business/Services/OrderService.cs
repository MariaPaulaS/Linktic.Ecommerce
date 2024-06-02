using System.Text;
using Linktic.Ecommerce.OrdersManagement.Business.Interfaces;
using Linktic.Ecommerce.OrdersManagement.Domain.Models;
using Linktic.Ecommerce.OrdersManagement.Domain.Models.Entities;
using Linktic.Ecommerce.OrdersManagement.Domain.Models.Requests;
using Linktic.Ecommerce.OrdersManagement.Infrastructure.Interfaces;
using Linktic.Ecommerce.OrdersManagement.Infrastructure.Interfaces.Repositories;
using Newtonsoft.Json;
using Serilog;

namespace Linktic.Ecommerce.OrdersManagement.Business.Services;

public class OrderService(IOrderRepository orderRepository, IProductCatalogRepository productCatalogRepository): IOrderService
{
    public async Task<List<Order>> GetAllOrders()
    {
        return await orderRepository.GetAllOrders();
    }

    public async Task CreateNewOrder(CreateOrderRequest createOrderRequest)
    {
        var newOrderInfo = new NewOrderInfo()
        {
            Id = Guid.NewGuid().ToString(),
            CustomerName = createOrderRequest.CustomerName,
            Details = createOrderRequest.Details,
            Total = 0
        };
        
        newOrderInfo.Total = await GetTotalFromOrder(createOrderRequest, newOrderInfo);
    }

    private int ValidateProductInOrder(ProductDetail product)
    {
        var updateQuantityRequest = new UpdateProductQuantityRequest()
        {
            ProductId = product.ProductId,
            NumProductsToSum = product.Quantity
        };
        var serializedRequest =  JsonConvert.SerializeObject(updateQuantityRequest);
        var content = new StringContent(serializedRequest, Encoding.UTF8, "application/json");
        productCatalogRepository.UpdateProductQuantity(content);
        return product.Quantity * product.Price;
    }


    private async Task<int> GetTotalFromOrder(CreateOrderRequest createOrderRequest, NewOrderInfo newOrderInfo)
    {
        var availableProducts = await productCatalogRepository.FindAvailableProducts();
        var totalOrder = 0;
        
        foreach (var product in createOrderRequest.Details)
        {
            if (availableProducts.Contains(product))
            {
                var availableProduct = availableProducts.FirstOrDefault(p => p.ProductId == product.ProductId);

                if (availableProduct.Quantity - product.Quantity < 0)
                {
                    Log.Error("There aren't enough units availables for this product");
                    newOrderInfo.Details.Remove(product);
                }

                var totalProduct = ValidateProductInOrder(product);
                totalOrder += totalProduct;

            }
            else
            {
                newOrderInfo.Details.Remove(product);
            }
        }

        return totalOrder;
    }
}