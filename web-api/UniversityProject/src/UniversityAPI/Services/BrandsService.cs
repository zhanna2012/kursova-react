using Microsoft.EntityFrameworkCore;
using UniversityAPI.Database;
using UniversityAPI.Extensions;
using UniversityAPI.Models.Brands;
using UniversityAPI.Models.Database;
using UniversityAPI.Services.Interfaces;

namespace UniversityAPI.Services;

public class BrandsService : IBrandsService
{
    private readonly IUniversityContext _context;
    private readonly IConfiguration _configuration;

    public BrandsService(IUniversityContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<IEnumerable<BrandResponse>> GetBrandsAsync()
    {
        return await _context.Brands.Select(b => new BrandResponse
            {
                Id = b.Id,
                Title = b.Title,
                PhotoUrl = b.PhotoUrl.GetImageUrl(nameof(Brands), _configuration)
            })
            .ToListAsync();
    }


    public async Task<BrandResponse> AddBrandsAsync(AddBrandRequest request)
    {
        var brands = new Brands
        {
            Title = request.Title,
            PhotoUrl = request.PhotoUrl.GetImageUrl(nameof(Brands), _configuration)
        };

        await _context.Brands.AddAsync(brands);
        await _context.SaveChangesAsync();

        return new BrandResponse
        {
            Id = brands.Id,
            Title = brands.Title,
            PhotoUrl = brands.PhotoUrl
        };
    }
}