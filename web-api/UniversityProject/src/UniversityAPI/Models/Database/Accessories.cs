using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityAPI.Models.Database;

[Table("Accessories")]
public class Accessories
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("photo_url")]
    public string? PhotoUrl { get; set; }

    [Column("price")]
    public double Price { get; set; }
}