using Microsoft.EntityFrameworkCore;
using UniversityAPI.Database;
using UniversityAPI.Extensions;
using UniversityAPI.Models.Database;
using UniversityAPI.Models.Products;
using UniversityAPI.Services.Interfaces;

namespace UniversityAPI.Services;

public class ProductsService : IProductsService
{
    private readonly UniversityContext _context;
    private readonly IConfiguration _configuration;

    public ProductsService(UniversityContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<IEnumerable<ProductResponse>> GetProductsAsync()
    {
        var results = new List<ProductResponse>();

        var products = await _context.Products.Include(p => p.Brand)
            .Include(p => p.ProductType)
            .ToListAsync();

        foreach (var product in products)
        {
            var result = new ProductResponse
            {
                Id = product.Id,
                ProductType = new ProductType { Id = product.ProductType.Id, Title = product.ProductType.Title },
                Brand = new Brand
                {
                    Id = product.Brand.Id,
                    Title = product.Brand.Title,
                    PhotoUrl = product.Brand.PhotoUrl.GetImageUrl(nameof(Brands), _configuration)
                },
                Amount = product.Amount,
                Item = await GetProductItem(product)
            };

            results.Add(result);
        }

        return results;
    }

    public async Task<ProductResponse> GetProductAsync(int productId)
    {
        var product = await _context.Products
            .Include(p => p.ProductType)
            .Include(p => p.Brand)
            .FirstOrDefaultAsync(p => p.Id == productId);

        return new ProductResponse
        {
            Id = product.Id,
            ProductType = new ProductType { Id = product.ProductType.Id, Title = product.ProductType.Title },
            Brand = new Brand
            {
                Id = product.Brand.Id,
                Title = product.Brand.Title,
                PhotoUrl = product.Brand.PhotoUrl.GetImageUrl(nameof(Brands), _configuration)
            },
            Amount = product.Amount,
            Item = await GetProductItem(product)
        };
    }

    private async Task<ProductEntity> GetProductItem(Products product)
    {
        if (product.ProductTypeId == 1)
        {
            var cosmetics = await _context.Cosmetics.FirstAsync(p => p.Id == product.CosmeticsId);
            return new Cosmetic
            {
                Color = cosmetics.Color,
                Name = cosmetics.Name,
                PhotoUrl = cosmetics.PhotoUrl.GetImageUrl(nameof(Cosmetics), _configuration),
                Id = cosmetics.Id,
                Country = cosmetics.Country,
                Description = cosmetics.Description,
                Ingredients = cosmetics.Ingredients,
                Price = cosmetics.Price,
                Weight = cosmetics.Weight,
                ProductUsage = cosmetics.ProductUsage
            };
        }

        var accessory = await _context.Accessories.FirstAsync(p => p.Id == product.AccessoryId);

        return new Accessory
        {
            Name = accessory.Name,
            PhotoUrl = accessory.PhotoUrl.GetImageUrl(nameof(Accessories), _configuration),
            Id = accessory.Id,
            Description = accessory.Description,
            Price = accessory.Price
        };
    }
}