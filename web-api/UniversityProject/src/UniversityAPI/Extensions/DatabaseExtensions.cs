using Microsoft.EntityFrameworkCore;
using UniversityAPI.Database;

namespace UniversityAPI.Extensions;

public static class DatabaseExtensions
{
    public static void AddMsSqlServer(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<IUniversityContext, UniversityContext>(o =>
        {
            o.UseSqlServer(configuration.GetConnectionString("MSSQL"));
            o.EnableSensitiveDataLogging();
        });
}