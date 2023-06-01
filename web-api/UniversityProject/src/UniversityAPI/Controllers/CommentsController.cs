using Microsoft.AspNetCore.Mvc;
using UniversityAPI.Models.Comments;
using UniversityAPI.Services.Interfaces;

namespace UniversityAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentsService _commentsService;

    public CommentsController(ICommentsService commentsService)
    {
        _commentsService = commentsService;
    }
    
    [HttpGet("{productId:int}")]
    public async Task<IActionResult> GetComments(int productId)
    {
        return Ok(await _commentsService.GetCommentsForProductAsync(productId));
    }

    [HttpPost]
    public async Task<IActionResult> AddComments([FromBody] AddCommentRequest request)
    {
        return Ok(await _commentsService.AddCommentAsync(request));
    }
}