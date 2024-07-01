using AutoMapper;
using backend.BaseModule.Models.Base;
using backend.Entities;
using backend.Helpers;
using backend.Vouchers.Interfaces.Repositories;
using backend.Vouchers.Interfaces.Shared;
using backend.Vouchers.Models.Request;
using backend.Vouchers.Models.Response;
using Microsoft.Extensions.Localization;

namespace backend.Vouchers.Services;

public class VoucherService : IVoucherService
{
    private Account _account;
    private readonly IMapper _mapper;
    private readonly IVoucherRepo _voucherRepo;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IStringLocalizer<VoucherService> _localizer;

    public VoucherService(
        IMapper mapper,
        IVoucherRepo voucherRepo,
        IHttpContextAccessor httpContextAccessor,
        IStringLocalizer<VoucherService> localizer
    )
    {
        _mapper = mapper;
        _voucherRepo = voucherRepo;
        _httpContextAccessor = httpContextAccessor;
        _localizer = localizer;
        _account = (Account)_httpContextAccessor.HttpContext.Items["Account"];
    }

    public async Task<PaginatedResponse<VoucherResponse>> GetPaginatedVoucher(VoucherFilter filter)
    {
        int pageSize = SiteHelper.ValidatePageSize(filter.pageSize);
        int pageIndex = filter.page > 1 ? filter.page : 1;

        (List<Voucher> result, int count, int totalPages) = await _voucherRepo.GetPaginatedVouchers(filter, pageIndex, pageSize);

        return mappingVoucherPaginatedResponse(result, pageIndex, totalPages, count);
    }

    public async Task<VoucherSingleResponse> GetVoucherById(Guid id)
    {
        Voucher voucher = await getVoucher(id);
        return _mapper.Map<VoucherSingleResponse>(voucher);
    }

    public async Task<VoucherResponse> CreateVoucher(CreateVoucherRequest model, string ipAddress)
    {
        // Check if category Name already exists on database
        if (await _voucherRepo.CheckVoucherCodeExist(model.Code))
        {
            throw new AppException(_localizer["duplicate_voucher_code"], "duplicate_voucher_code");
        }

        Voucher voucher = _mapper.Map<Voucher>(model);
        voucher.AuthorId = _account.Id;

        await _voucherRepo.CreateVoucher(voucher);

        return _mapper.Map<VoucherResponse>(voucher);
    }

    public async Task<VoucherResponse> UpdateVoucher(Guid id, UpdateVoucherRequest model, string ipAddress)
    {
        Voucher voucher = await getVoucher(id);

        if (!string.IsNullOrEmpty(model.Code))
        {
            voucher.Code = model.Code;
        }

        voucher.Percentage = model.Percentage;
        voucher.Stock = model.Stock;
        voucher.Updated = DateTime.UtcNow;

        await _voucherRepo.UpdateVoucher(voucher);

        return _mapper.Map<VoucherResponse>(voucher);
    }

    public async Task DeleteVoucher(Guid id , string ipAddress)
    {
        Voucher voucher = await getVoucher(id);

        voucher.Code = "[DELETED]_" + voucher.Code;
        voucher.Updated = DateTime.UtcNow;
        voucher.DeletedAt = DateTime.UtcNow;

        await _voucherRepo.UpdateVoucher(voucher);
    }

    private PaginatedResponse<VoucherResponse> mappingVoucherPaginatedResponse(IEnumerable<Voucher> categories, int page, int totalPage, int totalCount)
    {
        return new PaginatedResponse<VoucherResponse>()
        {
            Message = "success",
            Data = _mapper.Map<IEnumerable<VoucherResponse>>(categories),
            Page = page,
            TotalPage = totalPage,
            TotalCount = totalCount
        };
    }

    private async Task<Voucher> getVoucher(Guid id)
    {
        Voucher voucher = await _voucherRepo.GetVoucher(id) ?? throw new KeyNotFoundException(_localizer["voucher_not_found"]);
        return voucher;
    }
}