namespace UniversityAPI.Models.Products;

public class Accessory : ProductEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string? PhotoUrl { get; set; }

    public double Price { get; set; }
}