using backend.BaseModule.Models.Base;
using backend.Products.Models.Category;

namespace backend.Products.Interfaces.Shared;

public interface ICategoryService
{
    Task<List<SelectDataResponse>> GetListCategory(ListFilter filter);
    Task<PaginatedResponse<CategoryResponse>> GetPaginatedCategory(CategoryFilter filter);
    Task<CategoryResponse> GetCategoryById(Guid id);
    Task<CategoryResponse> CreateCategory(CreateCategoryRequest model, string ipAddress);
    Task<CategoryResponse> UpdateCategory(Guid id, UpdateCategoryRequest model, string ipAddress);
    Task DeleteCategory(Guid id, string ipAddress);
}