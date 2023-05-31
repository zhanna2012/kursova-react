using UniversityAPI.Database;
using UniversityAPI.Models.Database;
using UniversityAPI.Models.Products;
using UniversityAPI.Services.Interfaces;

namespace UniversityAPI.Services;

public class ImageService : IImageService
{
    private readonly IWebHostEnvironment _environment;
    private readonly IUniversityContext _context;

    public ImageService(IWebHostEnvironment environment, IUniversityContext context)
    {
        _environment = environment;
        _context = context;
    }

    public async Task<string?> UploadImage(string entityName, int itemId, IFormFile file)
    {
        var filePath = Path.Combine(_environment.WebRootPath, entityName, file.FileName);

        await using var stream = File.Create(filePath);
        await file.CopyToAsync(stream);

        switch (entityName)
        {
            case nameof(Accessories):
                var accessories = await _context.Accessories.FindAsync(itemId);
                accessories.PhotoUrl = file.FileName;
                _context.Accessories.Update(accessories);
                await _context.SaveChangesAsync();
                break;
            case nameof(Brands): 
                var brands = await _context.Brands.FindAsync(itemId);
                brands.PhotoUrl = file.FileName;
                _context.Brands.Update(brands);
                await _context.SaveChangesAsync();
                break;
            case nameof(Cosmetics): 
                var cosmetics = await _context.Cosmetics.FindAsync(itemId);
                cosmetics.PhotoUrl = file.FileName;
                _context.Cosmetics.Update(cosmetics);
                await _context.SaveChangesAsync();
                break;
            case nameof(Users): 
                var users = await _context.Users.FindAsync(itemId);
                users.PhotoUrl = file.FileName;
                _context.Users.Update(users);
                await _context.SaveChangesAsync();
                break;
        }

        return filePath;
    }
}