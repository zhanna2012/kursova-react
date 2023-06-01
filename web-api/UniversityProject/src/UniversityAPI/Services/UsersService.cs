using UniversityAPI.Database;
using UniversityAPI.Extensions;
using UniversityAPI.Models.Database;
using UniversityAPI.Models.Users;
using UniversityAPI.Services.Interfaces;

namespace UniversityAPI.Services;

public class UsersService : IUsersService
{
    private readonly IUniversityContext _context;
    private readonly IConfiguration _configuration;

    public UsersService(IUniversityContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<UserSnapshot> RegisterUser(RegisterUserRequest request)
    {
        var users = new Users
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhotoUrl = request.PhotoUrl.GetImageUrl(nameof(Users), _configuration),
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        await _context.Users.AddAsync(users);
        await _context.SaveChangesAsync();

        return MapUser(users);
    }

    public async Task<UserSnapshot> GetUserAsync(int userId)
    {
        var users = await _context.Users.FindAsync(userId);

        return MapUser(users);
    }
    
    private UserSnapshot MapUser(Users user)
    {
        return new UserSnapshot
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhotoUrl = user.PhotoUrl.GetImageUrl(nameof(Users), _configuration)
        };
    }
}