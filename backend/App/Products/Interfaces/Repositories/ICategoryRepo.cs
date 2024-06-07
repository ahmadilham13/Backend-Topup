using backend.Entities;
using backend.Products.Models.Category;

namespace backend.Products.Interfaces.Repositories;

public interface ICategoryRepo
{
    Task<(List<Category>, int, int)> GetPaginatedCategories(CategoryFilter filter, int pageIndex, int pageSize);

}