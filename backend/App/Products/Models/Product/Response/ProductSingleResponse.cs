using backend.BaseModule.Models.Media;
using backend.Entities;
using backend.Products.Models.Category;
using backend.Products.Models.ProductItem;

namespace backend.Products.Models.Product;

public class ProductSingleResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string SubName { get; set; }
    public string Description { get; set; }
    public ProductStatus Status { get; set; }
    public CategoryResponse Category { get; set; }
    #nullable enable
    public ImageUploadResponse? Thumbnail { get; set; }
    public ImageUploadResponse? Icon { get; set; }
    #nullable disable
    public List<ProductItemResponse> ProductItems { get; set; }
}