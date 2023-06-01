using Microsoft.EntityFrameworkCore;
using UniversityAPI.Models.Database;

namespace UniversityAPI.Database;

public class UniversityContext : DbContext, IUniversityContext
{
    public UniversityContext(DbContextOptions<UniversityContext> options) : base(options) { }

    public DbSet<Cosmetics> Cosmetics { get; set; }

    public DbSet<Accessories> Accessories { get; set; }

    public DbSet<ProductTypes> ProductTypes { get; set; }

    public DbSet<Brands> Brands { get; set; }

    public DbSet<Products> Products { get; set; }

    public DbSet<Users> Users { get; set; }

    public DbSet<AuthorizationTokens> AuthorizationTokens { get; set; }
    
    public DbSet<Comments> Comments { get; set; }

    public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthorizationTokens>()
            .HasKey(p => new { p.UserId, p.RefreshToken });
    }
}