using System.ComponentModel.DataAnnotations;
using backend.Entities;

namespace backend.Products.Models.ProductItem;

public class CreateProductItemRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Provider { get; set; }
    [Required]
    public long Price { get; set; }
    #nullable enable
    public long? SalePrice { get; set; }
    #nullable disable
    public ProductItemStatus Status { get; set; }
}