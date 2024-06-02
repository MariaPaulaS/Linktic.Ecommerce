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
        
        foreach (var product in createOrderRequest.ProductDetails)
        {
            var availableProduct = availableProducts.FirstOrDefault(p => p.Id == product.Id);
            if(availableProduct != null)
            {
                if (availableProduct.Quantity - product.Quantity < 0)
                {
                    Log.Error("There aren't enough units availables for this product");
                }
                totalOrder = GenerateTotalOrder(product, availableProduct, listOrderProducts, totalOrder);
            }
        }
        return (totalOrder, listOrderProducts);
    }

    private int GenerateTotalOrder(ProductDetail product, ProductDetail availableProduct, List<ProductDetail> listOrderProducts, int totalOrder)
    {
        product.UnitPrice = availableProduct.UnitPrice;
        product.ProductName = availableProduct.ProductName;
        listOrderProducts.Add(product);

        var totalProduct = ValidateProductInOrder(availableProduct, product);
        totalOrder += totalProduct;
        return totalOrder;
    }
}