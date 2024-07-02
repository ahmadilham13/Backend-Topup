using backend.BaseModule.Models.Media;
using backend.Entities;

namespace backend.Products.Models.Product;

public class CategoryProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string SubName { get; set; }
    public ProductStatus Status { get; set; }
    public string Description { get; set; }
    #nullable enable
    public ImageUploadResponse? Thumbnail { get; set; }
    public ImageUploadResponse? Icon { get; set; }
    #nullable disable
}