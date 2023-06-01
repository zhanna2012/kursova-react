using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityAPI.Models.Authorization;
using IAuthorizationService = UniversityAPI.Services.Interfaces.IAuthorizationService;

namespace UniversityAPI.Controllers;

[AllowAnonymous]
[ApiController]
[Route("[controller]")]
public class AuthorizationController : ControllerBase
{
    private readonly IAuthorizationService _authorizationService;

    public AuthorizationController(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    [HttpPost]
    public async Task<IActionResult> GetAllTokens([FromBody] AuthorizeUserRequest request)
    {
        return Ok(await _authorizationService.GetTokensAsync(request));
    }

    [HttpPost("{refreshToken}/refresh")]
    public async Task<IActionResult> RefreshTokens([FromQuery] string refreshToken)
    {
        return Ok(await _authorizationService.RefreshTokenAsync(refreshToken));
    }

    [HttpPost("{refreshToken}/revoke")]
    public async Task<IActionResult> RevokeTokens([FromQuery] string refreshToken)
    {
        return Ok(await _authorizationService.RevokeTokenAsync(refreshToken));
    }
}