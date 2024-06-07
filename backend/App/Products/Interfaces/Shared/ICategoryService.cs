using backend.BaseModule.Models.Base;
using backend.Products.Models.Category;

namespace backend.Products.Interfaces.Shared;

public interface ICategoryService
{
    Task<PaginatedResponse<CategoryResponse>> GetPaginatedCategory(CategoryFilter filter);
}