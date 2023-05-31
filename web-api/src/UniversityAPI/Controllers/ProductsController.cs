using Microsoft.AspNetCore.Mvc;
using UniversityAPI.Services.Interfaces;

namespace UniversityAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _productsService;

    public ProductsController(IProductsService productsService)
    {
        _productsService = productsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        return Ok(await _productsService.GetProductsAsync());
    }
    
    [HttpGet("{productId:int}")]
    public async Task<IActionResult> GetProduct(int productId)
    {
        return Ok(await _productsService.GetProductAsync(productId));
    }
}