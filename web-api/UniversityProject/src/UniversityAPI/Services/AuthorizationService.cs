using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UniversityAPI.Database;
using UniversityAPI.Models;
using UniversityAPI.Models.Authorization;
using UniversityAPI.Models.Database;
using UniversityAPI.Services.Interfaces;

namespace UniversityAPI.Services;

public class AuthorizationService : IAuthorizationService
{
    private readonly IUniversityContext _context;
    private readonly JwtConfigurationOptions _jwtConfiguration;

    public AuthorizationService(IUniversityContext context, IOptions<JwtConfigurationOptions> jwtOptions)
    {
        _context = context;
        _jwtConfiguration = jwtOptions.Value;
    }

    public async Task<Tokens> GetTokensAsync(AuthorizeUserRequest request)
    {
        var users = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

        if (!BCrypt.Net.BCrypt.Verify(request.Password, users.Password))
        {
            return null;
        }

        var expirationTime = DateTime.UtcNow.AddMilliseconds(_jwtConfiguration.Lifetime);
        var authorizationTokens = new AuthorizationTokens
        {
            UserId = users.Id,
            IsRevoked = false,
            AccessToken = GetAccessToken(users, expirationTime),
            RefreshToken = GetRefreshToken(users),
            ExpirationDatetime = expirationTime
        };

        await _context.AuthorizationTokens.AddAsync(authorizationTokens);

        await _context.SaveChangesAsync();

        return new Tokens
        {
            Email = users.Email,
            AccessToken = authorizationTokens.AccessToken,
            RefreshToken = authorizationTokens.RefreshToken
        };
    }

    public Task<Tokens> RefreshTokenAsync(string refreshToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RevokeTokenAsync(string refreshToken)
    {
        throw new NotImplementedException();
    }
    
    private string GetAccessToken(Users user, DateTime expirationTime)
    {
        var securityToken = new JwtSecurityToken(
            issuer: _jwtConfiguration.Issuer,
            audience: _jwtConfiguration.Audience,
            claims: CreateClaims(user),
            expires: expirationTime,
            signingCredentials: GetSigningCredentials()
        );
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(securityToken);
    }

    private string GetRefreshToken(Users user)
    {
        var securityToken = new JwtSecurityToken(
            issuer: _jwtConfiguration.Issuer,
            audience: _jwtConfiguration.Audience,
            claims: CreateClaims(user),
            signingCredentials: GetSigningCredentials()
        );
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(securityToken);
    }

    private SigningCredentials GetSigningCredentials() =>
        new(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Key)),
            SecurityAlgorithms.HmacSha512Signature);

    private Claim[] CreateClaims(Users user)
    {
        return new Claim[]
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            new(ClaimTypes.NameIdentifier, user.Email),
            new(ClaimTypes.Name, user.FirstName),
            new(ClaimTypes.Surname, user.LastName),
            new(ClaimTypes.Email, user.Email)
        };
    }
}