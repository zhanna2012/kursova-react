using Microsoft.AspNetCore.Mvc;
using UniversityAPI.Models.Users;
using UniversityAPI.Services.Interfaces;

namespace UniversityAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
    {
        return Ok(await _usersService.RegisterUser(request));
    }

    [HttpGet("{userId:int}")]
    public async Task<IActionResult> GetUser(int userId)
    {
        return Ok(await _usersService.GetUserAsync(userId));
    }
}