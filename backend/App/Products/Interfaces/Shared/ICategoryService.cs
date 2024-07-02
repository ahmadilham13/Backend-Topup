using backend.BaseModule.Models.Base;
using backend.Products.Models.Category;
using backend.Products.Models.Product;

namespace backend.Products.Interfaces.Shared;

public interface ICategoryService
{
    Task<List<SelectDataResponse>> GetListCategory(ListFilter filter);
    Task<PaginatedResponse<CategoryResponse>> GetPaginatedCategory(CategoryFilter filter);
    Task<CategorySingleResponse> GetCategoryById(Guid id);
    Task<CategoryResponse> CreateCategory(CreateCategoryRequest model, string ipAddress);
    Task<CategoryResponse> UpdateCategory(Guid id, UpdateCategoryRequest model, string ipAddress);
    Task DeleteCategory(Guid id, string ipAddress);

    Task<List<CategoryResponse>> GetAllCategory();
    Task<CategoryProductPaginateResponse> GetFullCategory(Guid id, ProductFilter filter);
}