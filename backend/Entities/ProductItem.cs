using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entities;

public class ProductItem : BaseEntity
{
    public string Name { get; set; }
    public string Provider { get; set; }
    public long Price { get; set; }
    #nullable enable
    public long SalePrice { get; set; }
    #nullable disable
    public ProductItemStatus Status { get; set; }
    public Guid ProductId { get; set; }
    [ForeignKey("ProductId")]
    public Product Product { get; set; }
}