namespace UniversityAPI.Services.Interfaces;

public interface IImageService
{
    Task<string?> UploadImage(string entityName, int itemId, IFormFile file);
}