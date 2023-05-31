using UniversityAPI.Models.Brands;

namespace UniversityAPI.Services.Interfaces;

public interface IBrandsService
{
    Task<IEnumerable<BrandResponse>> GetBrandsAsync();

    Task<BrandResponse> AddBrandsAsync(AddBrandRequest request);
}