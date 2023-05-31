namespace UniversityAPI.Models.Products;

public class Cosmetic : ProductEntity
{
    public string? Name { get; set; }

    public string Description { get; set; }

    public string? PhotoUrl { get; set; }

    public int Weight { get; set; }

    public double Price { get; set; }

    public string ProductUsage { get; set; }
    
    public string Color { get; set; }

    public string Country { get; set; }
    
    public string Ingredients { get; set; }
}