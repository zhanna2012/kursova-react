using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityAPI.Models.Database;

[Table("Products")]
public class Products
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("cosmetics_id")]
    public int? CosmeticsId { get; set; }

    [Column("accessory_id")]
    public int? AccessoryId { get; set; }

    [Column("product_type_id")]
    public int ProductTypeId { get; set; }

    [Column("brand_id")]
    public int BrandId { get; set; }

    [Column("amount")]
    public int Amount { get; set; }

    public Brands Brand { get; set; }

    public Accessories? Accessory { get; set; }

    public Cosmetics? Cosmetic { get; set; }

    public ProductTypes ProductType { get; set; }
}