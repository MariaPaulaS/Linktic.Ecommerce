using Linktic.Ecommerce.OrdersManagement.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Linktic.Ecommerce.OrdersManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        try
        {
            var productsCatalog = await _orderService.GetAllOrders();

            return Ok(productsCatalog);

        }
        catch (Exception e)
        {
            Log.Error(e,"{StackTrace} {Message}",e.StackTrace, e.Message);
            return StatusCode(500);
        }
    }
}