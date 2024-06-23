using backend.BaseModule.Models.Base;
using backend.Products.Models.Category;
using backend.Products.Models.Product;

namespace backend.Products.Interfaces.Shared;

public interface IProductService
{
    Task<PaginatedResponse<ProductResponse>> GetPaginatedProduct(ProductFilter filter);
    Task<ProductSingleResponse> GetProductById(Guid id);
    Task<ProductResponse> CreateProduct(CreateProductRequest model, string ipAddress);
    Task<ProductResponse> UpdateProduct(Guid id, UpdateProductRequest model, string ipAddress);
    Task DeleteProduct(Guid id, string ipAddress);
}