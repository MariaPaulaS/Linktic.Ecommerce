using Linktic.Ecommerce.ProductsCatalog.Business.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    
}