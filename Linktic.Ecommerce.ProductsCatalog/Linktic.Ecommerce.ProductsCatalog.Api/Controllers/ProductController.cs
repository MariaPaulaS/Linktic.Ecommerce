using Amazon.AppConfigData.Model;
using Linktic.Ecommerce.ProductsCatalog.Business.Interfaces;
using Linktic.Ecommerce.ProductsCatalog.Domain.Models.Exceptions;
using Linktic.Ecommerce.ProductsCatalog.Domain.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Linktic.Ecommerce.ProductsCatalog.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllProductCatalog()
    {
        try
        {
            var productsCatalog = await _productService.GetAllProducts();

            return Ok(productsCatalog);

        }
        catch (Exception e)
        {
            Log.Error(e,"{StackTrace} {Message}",e.StackTrace, e.Message);
            return StatusCode(500);
        }
    }
    
    [HttpGet("available")]
    public async Task<IActionResult> GetAvailableProductCatalog()
    {
        try
        {
            var productsCatalog = await _productService.GetAvailableProducts();

            return Ok(productsCatalog);

        }
        catch (Exception e)
        {
            Log.Error(e,"{StackTrace} {Message}",e.StackTrace, e.Message);
            return StatusCode(500);
        }
    }
    
    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProductById(string productId)
    {
        try
        {
            var product = await _productService.GetProductById(productId);
            
            return Ok(product);

        }
        catch (ProductNotFoundException e)
        {
            Log.Error(e,"{StackTrace} {Message}",e.StackTrace, e.Message);
            return NotFound("The product was not found in the catalog");
        }
        catch (Exception e)
        {
            Log.Error(e,"{StackTrace} {Message}",e.StackTrace, e.Message);
            return StatusCode(500);
        }
    }
    
    
    [HttpPut("quantity")]
    public async Task<IActionResult> UpdateProductQuantity([FromBody] UpdateQuantityRequest request)
    {
        try
        {
            await _productService.UpdateProductQuantity(request);

            return Ok();

        }
        catch (ProductNotFoundException e)
        {
            Log.Error(e,"{StackTrace} {Message}",e.StackTrace, e.Message);
            return NotFound("The product was not found in the catalog");
        }
        catch (Exception e)
        {
            Log.Error(e,"{StackTrace} {Message}",e.StackTrace, e.Message);
            return StatusCode(500);
        }
    }
    
}