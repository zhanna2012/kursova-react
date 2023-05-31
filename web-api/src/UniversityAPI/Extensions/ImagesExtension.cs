namespace UniversityAPI.Extensions;

public static class ImagesExtension
{
    public static string? GetImageUrl(this string? imageId, string entityName, IConfiguration configuration)
    {
        return imageId != null ? string.Format(configuration["ImagesUrl"]!, entityName, imageId) : null;
    }
}