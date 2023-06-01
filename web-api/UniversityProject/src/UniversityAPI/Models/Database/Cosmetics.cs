using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityAPI.Models.Database;

[Table("Cosmetics")]
public class Cosmetics
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string? Name { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("photo_url")]
    public string? PhotoUrl { get; set; }

    [Column("weight")]
    public int Weight { get; set; }

    [Column("price")]
    public double Price { get; set; }

    [Column("product_usage")]
    public string ProductUsage { get; set; }
    
    [Column("color")]
    public string Color { get; set; }

    [Column("country")]
    public string Country { get; set; }
    
    [Column("ingredients")]
    public string Ingredients { get; set; }
}