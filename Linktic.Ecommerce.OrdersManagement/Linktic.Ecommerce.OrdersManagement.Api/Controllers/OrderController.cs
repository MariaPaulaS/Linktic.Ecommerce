﻿using Linktic.Ecommerce.OrdersManagement.Business.Interfaces;
using Linktic.Ecommerce.OrdersManagement.Domain.Models.Exceptions;
using Linktic.Ecommerce.OrdersManagement.Domain.Models.Requests;
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
    
    [HttpPost]
    public async Task<IActionResult> CreateNewOrder([FromBody] CreateOrderRequest createOrderRequest)
    {
        try
        {
            if (createOrderRequest.ProductDetails.Count == 0)
            {
                return BadRequest("The products details in the order cannot be empty");
            }
            await _orderService.CreateNewOrder(createOrderRequest);

            return Ok();

        }
        catch (NotEnoughProductsException e)
        {
            Log.Error(e,"{StackTrace} {Message}",e.StackTrace, e.Message);
            return StatusCode(500, "There are not enough products available to continue with the order");
        }
        catch (Exception e)
        {
            Log.Error(e,"{StackTrace} {Message}",e.StackTrace, e.Message);
            return StatusCode(500);
        }
    }
}