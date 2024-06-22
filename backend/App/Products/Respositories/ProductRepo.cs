using backend.Entities;
using backend.Helpers;
using backend.Products.Interfaces.Repositories;
using backend.Products.Models.Product;
using Microsoft.EntityFrameworkCore;

namespace backend.Products.Repositories;

public class ProductRepo(
    DataContext context
) : IProductRepo
{
    private readonly DataContext _context = context;

    public async Task<(List<Product>, int, int)> GetPaginatedProducts(ProductFilter filter, int pageIndex, int pageSize)
    {
        var query = _context.Products.AsQueryable();

        query = query.Include(x => x.Category);

        if (filter.query != null)
        {
            query = query.Where(x => EF.Functions.Like(x.Name, $"%{filter.query}%"));
        }

        query = filter.order switch
        {
            ProductOrder.Name => query.OrderBy(x => x.Name),
            _ => query.OrderByDescending(x => x.Created),
        };

        var result = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        var count = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(count / (double)pageSize);

        return (result, count, totalPages);
    }

    public async Task<Product> GetProduct(Guid id)
    {
        return await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> CheckProductNameExist(string name)
    {
        return await _context.Products.AnyAsync(x => x.Name == name);
    }

    public async Task CreateProduct(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProduct(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }
}