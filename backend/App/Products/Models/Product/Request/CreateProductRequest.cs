using System.ComponentModel.DataAnnotations;

namespace backend.Products.Models.Product;

public class CreateProductRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public Guid CategoryId { get; set; }
    public string Description { get; set; }
}