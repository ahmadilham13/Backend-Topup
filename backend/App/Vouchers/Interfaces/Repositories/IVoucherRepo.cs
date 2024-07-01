using backend.Entities;
using backend.Vouchers.Models.Request;

namespace backend.Vouchers.Interfaces.Repositories;

public interface IVoucherRepo
{
    Task<(List<Voucher>, int, int)> GetPaginatedVouchers(VoucherFilter filter, int pageIndex, int pageSize);
    Task<bool> CheckVoucherCodeExist(string code);
    Task<Voucher> GetVoucher(Guid id);
    Task CreateVoucher(Voucher voucher);
    Task UpdateVoucher(Voucher voucher);
}
