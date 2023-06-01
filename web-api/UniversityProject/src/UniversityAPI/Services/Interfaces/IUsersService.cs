using UniversityAPI.Models.Users;

namespace UniversityAPI.Services.Interfaces;

public interface IUsersService
{
    Task<UserSnapshot> RegisterUser(RegisterUserRequest request);

    Task<UserSnapshot> GetUserAsync(int userId);
}