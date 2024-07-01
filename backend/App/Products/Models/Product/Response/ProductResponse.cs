using backend.Entities;

namespace backend.Products.Models.Product;

public class ProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ProductStatus Status { get; set; }
    public string Description { get; set; }
}