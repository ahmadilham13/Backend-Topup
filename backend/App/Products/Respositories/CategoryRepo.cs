using backend.Entities;
using backend.Helpers;
using backend.Products.Interfaces.Repositories;
using backend.Products.Models.Category;
using Microsoft.EntityFrameworkCore;

namespace backend.Products.Repositories;

public class CategoryRepo(
    DataContext context
 )  : ICategoryRepo
{
    private readonly DataContext _context = context;

    public async Task<(List<Category>, int, int)> GetPaginatedCategories(CategoryFilter filter, int pageIndex, int pageSize)
    {
        var query = _context.Categories.AsQueryable();

        if (filter.query != null)
        {
            query = query.Where(x => EF.Functions.Like(x.Name, $"%{filter.query}%"));
        }

        query = filter.order switch
        {
            CategoryOrder.Name => query.OrderBy(x => x.Name),
            _ => query.OrderByDescending(x => x.Created),
        };

        var result = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        var count = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(count / (double)pageSize);

        return (result, count, totalPages);
    }
}