using UniversityAPI.Models;

namespace UniversityAPI.Extensions;

public static class ConfigurationExtension
{
    public static void BindConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();

        var section = configuration.GetSection("Jwt");
        services.Configure<JwtConfigurationOptions>(section);
    }
}