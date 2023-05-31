using Microsoft.AspNetCore.Mvc;
using UniversityAPI.Models.Brands;
using UniversityAPI.Services.Interfaces;

namespace UniversityAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BrandsController : ControllerBase
{
    private readonly IBrandsService _brandsService;

    public BrandsController(IBrandsService brandsService)
    {
        _brandsService = brandsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBrands()
    {
        return Ok(await _brandsService.GetBrandsAsync());
    }
    
    [HttpPost]
    public async Task<IActionResult> AddBrand(AddBrandRequest request)
    {
        return Ok(await _brandsService.AddBrandsAsync(request));
    }
}