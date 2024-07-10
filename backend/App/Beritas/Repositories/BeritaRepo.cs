using backend.Beritas.Interfaces.Repositories;
using backend.Beritas.Models.Request;
using backend.Entities;
using backend.Helpers;
using Microsoft.EntityFrameworkCore;

namespace backend.Beritas.Repositories;

public class BeritaRepo(
    DataContext context
    ) : IBeritaRepo
{
    private readonly DataContext _context = context;
    
    public async Task<(List<Berita>, int, int)> GetPaginatedBeritas(BeritaFilter filter, int pageIndex, int pageSize)
    {
        var query = _context.Beritas.AsQueryable();

        var result = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        var count = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(count / (double)pageSize);

        return (result, count, totalPages);
    }
}
