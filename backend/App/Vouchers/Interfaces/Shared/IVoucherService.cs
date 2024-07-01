using backend.BaseModule.Models.Base;
using backend.Vouchers.Models.Request;
using backend.Vouchers.Models.Response;

namespace backend.Vouchers.Interfaces.Shared;

public interface IVoucherService
{
    Task<PaginatedResponse<VoucherResponse>> GetPaginatedVoucher(VoucherFilter filter);
    Task<VoucherSingleResponse> GetVoucherById(Guid id);
    Task<VoucherResponse> CreateVoucher(CreateVoucherRequest model, string ipAddress);
    Task<VoucherResponse> UpdateVoucher(Guid id, UpdateVoucherRequest model, string ipAddress);
    Task DeleteVoucher(Guid id, string ipAddress);
}
