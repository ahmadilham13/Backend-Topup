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
        return await _context.Products.Include(x => x.Category).Include(x => x.ProductItems).Include(x => x.Thumbnail).Include(x => x.Icon).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ProductItem> GetProductItem(Guid id)
    {
        return await _context.ProductItems.FirstOrDefaultAsync( x => x.Id == id );
    }

    public async Task<List<ProductItem>> GetProductItemByProductExcept(Guid productId, List<Guid> productItemIds)
    {
        var query = _context.ProductItems.AsQueryable();

        query = query.Where( x => x.ProductId == productId );
        query = query.Where( x => productItemIds.All( a => a != x.Id ) );
        
        return await query.ToListAsync();
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

    public async Task CreateProductItem(ProductItem productItem)
    {
        await _context.ProductItems.AddAsync(productItem);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProductItem(ProductItem productItem)
    {
        _context.ProductItems.Update(productItem);
        await _context.SaveChangesAsync();
    }


    public async Task<(List<Product>, int, int)> GetPaginatedProductsByCategory(Guid id, ProductFilter filter, int pageIndex, int pageSize)
    {
        var query = _context.Products.AsQueryable();

        query = query.Where(x => x.CategoryId == id);
        query = query.Include(x => x.Thumbnail);
        query = query.Include(x => x.Icon);

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
}