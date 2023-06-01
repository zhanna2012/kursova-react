using UniversityAPI.Models.Authorization;

namespace UniversityAPI.Services.Interfaces;

public interface IAuthorizationService
{
    Task<Tokens> GetTokensAsync(AuthorizeUserRequest request);

    Task<Tokens> RefreshTokenAsync(string refreshToken);

    Task<bool> RevokeTokenAsync(string refreshToken);
}