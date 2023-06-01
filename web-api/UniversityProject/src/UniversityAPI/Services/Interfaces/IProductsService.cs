using UniversityAPI.Models.Products;

namespace UniversityAPI.Services.Interfaces;

public interface IProductsService
{
    Task<IEnumerable<ProductResponse>> GetProductsAsync();

    Task<ProductResponse> GetProductAsync(int productId);
}