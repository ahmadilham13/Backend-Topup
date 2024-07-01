using backend.BaseModule.Models.Base;
using backend.Entities;
using backend.Helpers;
using backend.Products.Interfaces.Repositories;
using backend.Products.Models.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

    public async Task<List<Category>> GetAllCategories(ListFilter filter)
    {
        IQueryable<Category> query = _context.Categories.AsQueryable();

        if (!filter.Query.IsNullOrEmpty())
        {
            query = query.Where(x => EF.Functions.Like(x.Name, $"%{filter.Query}%"));
        }

        if (filter.Limit != null && filter.Limit > 0)
        {
            query = query.Take((int) filter.Limit);
        }

        List<Category> categories = await query.ToListAsync();

        if (!filter.Includes.IsNullOrEmpty())
        {
            List<Guid> guids = [];

            if (filter.Includes.Contains(','))
            {
                List<string> ids = [.. filter.Includes.Split(",")];

                foreach (string item in ids)
                {
                    guids.Add(new Guid(item));
                }
            }

            else
            {
                guids.Add(new Guid(filter.Includes));
            }

            categories.AddRange(await getCategories(guids));
        }

        return categories.Distinct().ToList();
    }

    public async Task<Category> GetCategory(Guid id)
    {
        return await _context.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> CheckCategoryNameExist(string name)
    {
        return await _context.Categories.AnyAsync(x => x.Name == name);
    }

    public async Task<bool> CheckCategoryExist(Guid id)
    {
        return await _context.Categories.AnyAsync(x => x.Id == id);
    }

    public async Task CreateCategory(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCategory(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }

    private async Task<List<Category>> getCategories(List<Guid> ids)
    {
        return await _context.Categories.Where(x => ids.Contains(x.Id)).ToListAsync();
    }
}