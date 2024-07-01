using backend.BaseModule.Models.Base;
using backend.BaseModule.Models.Media;
using backend.Products.Models.Product;

namespace backend.Products.Interfaces.Shared;

public interface IProductService
{
    Task<PaginatedResponse<ProductResponse>> GetPaginatedProduct(ProductFilter filter);
    Task<ProductSingleResponse> GetProductById(Guid id);
    Task<ProductSingleResponse> CreateProduct(CreateProductRequest model, string ipAddress);
    Task<ProductSingleResponse> UpdateProduct(Guid id, UpdateProductRequest model, string ipAddress);
    Task DeleteProduct(Guid id, string ipAddress);
    Task<ImageFullUploadResponse> UploadMedia(ImageUploadRequest model);
}