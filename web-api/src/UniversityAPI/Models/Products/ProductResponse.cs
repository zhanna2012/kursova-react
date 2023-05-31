namespace UniversityAPI.Models.Products;

public class ProductResponse
{
    public int Id { get; set; }

    public Brand Brand { get; set; }

    public int Amount { get; set; }

    public ProductType ProductType { get; set; }
    
    public ProductEntity Item { get; set; }
}