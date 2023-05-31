using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityAPI.Services.Interfaces;

namespace UniversityAPI.Controllers;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class ImagesController : ControllerBase
{
    private readonly IImageService _imageService;

    public ImagesController(IImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpGet("{entityName}/{itemId:int}")]
    public async Task<IActionResult> UploadImage(string entityName, int itemId, [FromForm] IFormFile file)
    {
        return Ok(await _imageService.UploadImage(entityName, itemId, file));
    }
}