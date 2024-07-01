using backend.Entities;
using backend.Products.Models.Category;

namespace backend.Products.Models.Product;

public class ProductSingleResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ProductStatus Status { get; set; }
    public CategoryResponse Category { get; set; }
}