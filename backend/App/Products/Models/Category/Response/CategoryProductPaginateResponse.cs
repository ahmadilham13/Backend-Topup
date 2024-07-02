using backend.BaseModule.Models.Base;
using backend.Products.Models.Product;

namespace backend.Products.Models.Category;

public class CategoryProductPaginateResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public PaginatedResponse<CategoryProductResponse> Products { get; set; }
}