using System.Text;
using Linktic.Ecommerce.OrdersManagement.Business.Interfaces;
using Linktic.Ecommerce.OrdersManagement.Domain.Models;
using Linktic.Ecommerce.OrdersManagement.Domain.Models.Entities;
using Linktic.Ecommerce.OrdersManagement.Domain.Models.Requests;
using Linktic.Ecommerce.OrdersManagement.Domain.Models.Responses;
using Linktic.Ecommerce.OrdersManagement.Infrastructure.Interfaces;
using Linktic.Ecommerce.OrdersManagement.Infrastructure.Interfaces.Repositories;
using Newtonsoft.Json;
using Serilog;

namespace Linktic.Ecommerce.OrdersManagement.Business.Services;

public class OrderService(IOrderRepository orderRepository, IProductCatalogRepository productCatalogRepository): IOrderService
{
    public async Task<List<OrderResponse>> GetAllOrders()
    {
        var ordersList = await orderRepository.GetAllOrders();
        List<OrderResponse> orderResponses = new List<OrderResponse>();
        foreach (var order in ordersList)
        {
            OrderResponse orderResponse = new OrderResponse();
            orderResponse.Details = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(order.Details);
            orderResponse.Id = order.Id;
            orderResponse.CustomerName = order.CustomerName;
            orderResponse.Total = order.Total;
            orderResponses.Add(orderResponse);
        }

        return orderResponses;
    }

    public async Task CreateNewOrder(CreateOrderRequest createOrderRequest)
    {
        var newOrderInfo = new NewOrderInfo()
        {
            Id = Guid.NewGuid().ToString(),
            CustomerName = createOrderRequest.CustomerName,
            ProductDetails = createOrderRequest.ProductDetails,
            Total = 0
        };
        
        var result = ValidateDataFromOrder(createOrderRequest).Result;
        newOrderInfo.Total = result.Item1;
        newOrderInfo.ProductDetails = result.Item2;

        if (newOrderInfo.ProductDetails.Count > 0)
        {
            await orderRepository.CreateNewOrder(newOrderInfo);
        }
    }

    private int ValidateProductInOrder(ProductDetail availableProduct, ProductDetail orderProduct)
    {
        var updateQuantityRequest = new UpdateProductQuantityRequest()
        {
            ProductId = orderProduct.Id,
            NumProductsToSum = -orderProduct.Quantity
        };
        var serializedRequest =  JsonConvert.SerializeObject(updateQuantityRequest);
        var content = new StringContent(serializedRequest, Encoding.UTF8, "application/json");
        productCatalogRepository.UpdateProductQuantity(content);
        return orderProduct.Quantity * availableProduct.UnitPrice;
    }
    
    private async Task<(int, List<ProductDetail>)> ValidateDataFromOrder(CreateOrderRequest createOrderRequest)
    {
        var availableProducts = await productCatalogRepository.FindAvailableProducts();
        var totalOrder = 0;
        var listOrderProducts = new List<ProductDetail>();
        
        foreach (var orderProduct in createOrderRequest.ProductDetails)
        {
            var availableProduct = availableProducts.FirstOrDefault(p => p.Id == orderProduct.Id);
            if(availableProduct != null)
            {
                if (availableProduct.Quantity - orderProduct.Quantity < 0)
                {
                    Log.Error("There aren't enough units availables for this product");
                }
                totalOrder = GenerateTotalOrder(orderProduct, availableProduct, listOrderProducts, totalOrder);
            }
        }
        return (totalOrder, listOrderProducts);
    }

    private int GenerateTotalOrder(ProductDetail orderProduct, ProductDetail availableProduct, List<ProductDetail> listOrderProducts, int totalOrder)
    {
        orderProduct.UnitPrice = availableProduct.UnitPrice;
        orderProduct.ProductName = availableProduct.ProductName;
        listOrderProducts.Add(orderProduct);

        var totalProduct = ValidateProductInOrder(availableProduct, orderProduct);
        totalOrder += totalProduct;
        return totalOrder;
    }
}