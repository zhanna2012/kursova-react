using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UniversityAPI.Models;

namespace UniversityAPI.Extensions;

public static class AuthenticationExtensions
{
    public static void AddJwtBearerAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfigurationOptions = new JwtConfigurationOptions();
        configuration.GetSection("Jwt").Bind(jwtConfigurationOptions);
        services.AddAuthorization();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = jwtConfigurationOptions.Issuer,
                ValidAudience = jwtConfigurationOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfigurationOptions.Key)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true
            };
        });
    }
}