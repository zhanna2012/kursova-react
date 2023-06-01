using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityAPI.Models.Database;

[Table("Comments")]
public class Comments
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }
    
    [Column("product_id")]
    public int ProductId { get; set; }

    [Column("text")]
    public string Text { get; set; }

    [Column("rating")]
    public byte Rating { get; set; }

    public Products Product { get; set; }

    public Users User { get; set; }
}