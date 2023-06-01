using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using UniversityAPI.Models.Database;

namespace UniversityAPI.Database;

public interface IUniversityContext
{
    DbSet<Cosmetics> Cosmetics { get; set; }

    DbSet<Accessories> Accessories { get; set; }

    DbSet<ProductTypes> ProductTypes { get; set; }

    DbSet<Brands> Brands { get; set; }
    
    DbSet<Products> Products { get; set; }
    
    DbSet<Users> Users { get; set; }
    
    DbSet<AuthorizationTokens> AuthorizationTokens { get; set; }
    
    DbSet<Comments> Comments { get; set; }
    
    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync();
}