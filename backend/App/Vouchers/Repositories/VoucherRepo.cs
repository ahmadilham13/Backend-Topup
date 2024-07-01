using backend.Entities;
using backend.Helpers;
using backend.Vouchers.Interfaces.Repositories;
using backend.Vouchers.Models.Request;
using Microsoft.EntityFrameworkCore;

namespace backend.Vouchers.Repositories;

public class VoucherRepo(
    DataContext context
    ) : IVoucherRepo
{
    private readonly DataContext _context = context;

    public async Task<(List<Voucher>, int, int)> GetPaginatedVouchers(VoucherFilter filter, int pageIndex, int pageSize)
    {
        var query = _context.Vouchers.AsQueryable();

        if (filter.query != null)
        {
            query = query.Where(x => EF.Functions.Like(x.Code, $"%{filter.query}%"));
        }

        query = filter.order switch
        {
            VoucherOrder.Code => query.OrderBy(x => x.Code),
            _ => query.OrderByDescending(x => x.Created),
        };

        var result = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        var count = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(count / (double)pageSize);

        return (result, count, totalPages);
    }

    public async Task<Voucher> GetVoucher(Guid id)
    {
        return await _context.Vouchers.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> CheckVoucherCodeExist(string code)
    {
        return await _context.Vouchers.AnyAsync(x => x.Code == code);
    }

    public async Task CreateVoucher(Voucher voucher)
    {
        await _context.Vouchers.AddAsync(voucher);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateVoucher(Voucher voucher)
    {
        _context.Vouchers.Update(voucher);
        await _context.SaveChangesAsync();
    }
}
