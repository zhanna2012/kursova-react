namespace UniversityAPI.Models.Authorization;

public class Tokens
{
    public string Email { get; set; }

    public string RefreshToken { get; set; }

    public string AccessToken { get; set; }
}