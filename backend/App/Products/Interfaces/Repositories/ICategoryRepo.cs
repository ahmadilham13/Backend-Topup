using backend.BaseModule.Models.Base;
using backend.Entities;
using backend.Products.Models.Category;

namespace backend.Products.Interfaces.Repositories;

public interface ICategoryRepo
{
    Task<(List<Category>, int, int)> GetPaginatedCategories(CategoryFilter filter, int pageIndex, int pageSize);
    Task<List<Category>> GetAllCategories(ListFilter filter);
    Task<Category> GetCategory(Guid id);
    Task<bool> CheckCategoryNameExist(string name);
    Task<bool> CheckCategoryExist(Guid id);
    Task CreateCategory(Category category);
    Task UpdateCategory(Category category);

    Task<List<Category>> GetAllCategories();
    Task<Category> GetFullCategory(Guid id);

}