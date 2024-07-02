using System.ComponentModel.DataAnnotations;
using backend.Entities;
using backend.Products.Models.ProductItem;

namespace backend.Products.Models.Product;

public class CreateProductRequest
{
    [Required]
    public string Name { get; set; }
    #nullable enable
    public string? SubName { get; set; }
    #nullable disable
    [Required]
    public Guid CategoryId { get; set; }
    public ProductStatus Status { get; set; }
    public string Description { get; set; }
    #nullable enable
    public Guid? ThumbnailId { get; set; }
    public Guid? Iconid { get; set; }
    public List<CreateProductItemRequest>? ProductItems { get; set; }
    #nullable disable
}