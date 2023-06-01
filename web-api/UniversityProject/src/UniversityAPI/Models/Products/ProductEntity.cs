using System.Runtime.Serialization;

namespace UniversityAPI.Models.Products;

[KnownType(typeof(Cosmetic))]
[KnownType(typeof(Accessory))]
public class ProductEntity
{
    public int Id { get; set; }
}