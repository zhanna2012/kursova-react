namespace UniversityAPI.Models.Authorization;

public class AuthorizeUserRequest
{
    public string Email { get; set; }

    public string Password { get; set; }
}