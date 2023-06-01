using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityAPI.Models.Database;

[Table("Brands")]
public class Brands
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    public string? Title { get; set; }

    [Column("photo_url")]
    public string? PhotoUrl { get; set; }
}