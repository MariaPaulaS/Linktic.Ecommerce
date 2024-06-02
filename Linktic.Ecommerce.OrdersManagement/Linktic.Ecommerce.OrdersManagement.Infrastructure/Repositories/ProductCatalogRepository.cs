using Linktic.Ecommerce.OrdersManagement.Domain.Models.Entities;
using Linktic.Ecommerce.OrdersManagement.Infrastructure.Interfaces;
using Newtonsoft.Json;
using Serilog;

namespace Linktic.Ecommerce.OrdersManagement.Infrastructure.Repositories;

public class ProductCatalogRepository (HttpClient httpClient): IProductCatalogRepository
{
    public async Task<List<ProductDetail>?> FindAvailableProducts()
    {
        var uri = $"{httpClient.BaseAddress}/available";
        var response = await httpClient.GetAsync(uri);
        Log.Information("Available products response: {EmailResponse}",await response.Content.ReadAsStringAsync());
        var stringResponse = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<ProductDetail>>(stringResponse);
    }
    
    public async void UpdateProductQuantity(StringContent content)
    {
        var uri = $"{httpClient.BaseAddress}/quantity";
        var response = await httpClient.PutAsync(uri, content);
        if (response.IsSuccessStatusCode)
        {
            Log.Information("The product quantity was updated succesfully");

        }
    }
}